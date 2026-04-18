using Microsoft.AspNetCore.Mvc;
using DotNetEnv;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using GestionApi.Data;
using GestionApi.Models;
using GestionApi.Dtos;

namespace GestionApi.Controllers{
	[Route("api/[controller]")]
	[ApiController]
	public class AsistenciasController : ControllerBase{
		private readonly AppDbContext _context;
		private readonly IConfiguration _config;

		public AsistenciasController(AppDbContext context, IConfiguration config){
			_context = context;
			_config = config;
		}


		[HttpGet]
		public async Task<ActionResult<AsistenciasGetDto>> GetAsistencia([FromQuery] int AlumnoID, DateTime Fecha){
			var asistencia = await _context.Asistencias
				.FirstOrDefaultAsync(a => a.AlumnoID == AlumnoID && a.Fecha.Date == Fecha.Date);

			if (asistencia == null){
				return NotFound(new {message = "No se encontró ese alumno o esa fecha"});
			}

			return Ok(asistencia);

		}
		[HttpGet("{id}")]
		public async Task<ActionResult<AsistenciasResumenDto>> GetResumenAlumno([FromRoute] int alumnoId){
			var asistencias = await _context.Asistencias
				.Where(a => a.AlumnoID == alumnoId)
				.ToListAsync();
			int Presentes = 0;
			int Ausentes = 0;
			var alumno = await _context.Alumnos
				.Where(a => a.ID == alumnoId)
				.Select(a => new AlumnoGetDto {
				ID = a.ID,
				Name = a.Name
			}).FirstOrDefaultAsync();
			
			foreach (var asistencia in asistencias){
				if (asistencia.Presente){
					Presentes++;
				}				
				else {
					Ausentes++;
				}
			}

			int total = Presentes + Ausentes;
			int porcentajePromedio = (Presentes / total) * 100;
			if (alumno == null) return BadRequest(new { message = "No se encontró un alumno con esa id"});
			
			AsistenciasResumenDto resumen = new AsistenciasResumenDto{
				nombre = alumno.Name,
				presentes = Presentes,
				ausentes = Ausentes,
				porcentajePresencias = porcentajePromedio
			};

			return Ok(resumen);
		}


		

		[HttpPost]
		public async Task<ActionResult> PostAsistencia([FromBody] AsistenciasCreateDto asistenciaDto){

			if (!ModelState.IsValid){
				return BadRequest(new { message = "El formato de la fecha no es válido, se usa YYYY-MM-DD"});
			}

			var alumno = await _context.Alumnos.FindAsync(asistenciaDto.alumnoID);
			if (alumno == null){
				return NotFound(new { message = "No se encontró ningún alumno con esa ID"});
			}

			var AlreadyExistsThisDate = await _context.Asistencias.AnyAsync(a =>
				a.AlumnoID == asistenciaDto.alumnoID && a.Fecha.Date == asistenciaDto.fecha.Date
			);
			if (AlreadyExistsThisDate){
				return BadRequest(new { message = "Ya está definida una asistencia en esta fecha"});
			}

			var asistencia = new Asistencia{
				Fecha = asistenciaDto.fecha,
				AlumnoID = asistenciaDto.alumnoID,
				Presente = asistenciaDto.presente
			};

			_context.Asistencias.Add(asistencia);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetAsistencia),
				new {
				alumnoID = asistencia.AlumnoID,
				Fecha = asistencia.Fecha.ToString("yyyy-MM-dd"), 
				},
				asistencia
			);
		}
		[HttpDelete]
		public async Task<ActionResult<Asistencia>> DeleteAsistencia([FromQuery] AsistenciasDeleteDto request){
			string adminHash = Env.GetString("ADMIN_PASSWORD");

			if (string.IsNullOrWhiteSpace(request.password)) return Unauthorized(new { message = "Necesitas enviar una contraseña"});
			if (!BCrypt.Net.BCrypt.Verify(request.password, adminHash)){
				return Unauthorized(new { message = "La contraseña es incorrecta" });
			}
			var asistencia = await _context.Asistencias.FirstOrDefaultAsync(a =>
				a.AlumnoID == request.alumnoID &&
				a.Fecha == request.fecha
			);
			if (asistencia == null) return NotFound(new { message = "No se registró ninguna asistencia de ese alumno ese día"});
			_context.Asistencias.Remove(asistencia);
	
			await _context.SaveChangesAsync();
			return NoContent();
		}
		[HttpPut]
		public async Task<ActionResult<Asistencia>> UpdateAsistencia([FromBody] AsistenciasUpdateDto dto){
			var asistenciaCambiar = await _context.Asistencias.FirstOrDefaultAsync(a => 
				a.AlumnoID == dto.alumnoID && 
				a.Fecha == dto.fecha
			);
			if (asistenciaCambiar == null) return NotFound(new {message = "No se registró ninguna asistencia de ese alumno ese dia para cambiar"});
			if (dto.presente == null) return BadRequest(new { message = "Necesitas especificar el valor a cambiar"});

			asistenciaCambiar = new Asistencia{
				AlumnoID = asistenciaCambiar.AlumnoID,
				Fecha = asistenciaCambiar.Fecha,
				Presente = dto.presente.Value,
			};

			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}
