using Microsoft.AspNetCore.Mvc;
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


		[HttpGet("asistencia")]
		public async Task<ActionResult<Asistencia>> GetAsistencia([FromQuery] int AlumnoID, DateTime Fecha){
			var asistencia = await _context.Asistencias
				.FirstOrDefaultAsync(a => a.AlumnoID == AlumnoID && a.Fecha.Date == Fecha.Date);

			if (asistencia == null){
				return NotFound(new {message = "No se encontró ese alumno o esa fecha"});
			}

			return Ok(asistencia);

		}

		

		[HttpPost("asistencia")]
		public async Task<ActionResult> PostAsistencia([FromQuery] AsistenciasCreateDto asistenciaDto){

			if (!ModelState.IsValid){
				return BadRequest(new { message = "El formato de la fecha no es válido, se usa YYYY-MM-DD"});
			}

			var alumno = await _context.Alumnos.FindAsync(asistenciaDto.AlumnoID);
			if (alumno == null){
				return NotFound(new { message = "No se encontró ningún alumno con esa ID"});
			}

			var AlreadyExistsThisDate = await _context.Asistencias.AnyAsync(a =>
				a.AlumnoID == asistenciaDto.AlumnoID && a.Fecha.Date == asistenciaDto.Fecha.Date
			);
			if (AlreadyExistsThisDate){
				return BadRequest(new { message = "Ya está definida una asistencia en esta fecha"});
			}

			var asistencia = new Asistencia{
				Fecha = asistenciaDto.Fecha,
				AlumnoID = asistenciaDto.AlumnoID,
				Presente = asistenciaDto.Presente
			};

			_context.Asistencias.Add(asistencia);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetAsistencia),
				new {
				
				Fecha = asistencia.Fecha.ToString("yyyy-MM-dd"), 
				
				},
				asistencia
			);
		}
		[HttpDelete]
		public async Task<ActionResult<Asistencia>> DeleteAsistencia([]){

		}
	}
}
