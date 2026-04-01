using Microsoft.AspNetCore.Mvc;
using System.Linq;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using GestionApi.Data;
using GestionApi.Models;
using GestionApi.Dtos;

namespace GestionApi.Controllers{
	[Route("api/[controller]")]
	[ApiController]
	public class AlumnosController : ControllerBase{
		private readonly AppDbContext _context;
		private readonly IConfiguration _config;

		public AlumnosController(AppDbContext context, IConfiguration config){
			_context = context; 
			_config = config;
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Alumno>> GetAlumnos([FromRoute] int id){
			var alumno = await _context.Alumnos
				.Include(a => a.Curso)
				.FirstOrDefaultAsync(a => a.ID == id);
			if (alumno == null){
				return NotFound(new { message = "No se encontró el alumno"});
			}
			
			var alumnoDto = new AlumnoGetDto{
				Name = alumno.Name,
				ID = alumno.ID
			};
						
			return Ok(alumnoDto);
		}
		[HttpDelete("reset-database")]
		public async Task<ActionResult> ResetAlumnos(){
			await _context.Database.ExecuteSqlRawAsync($"ALTER SEQUENCE \"Alumnos_ID_Seq\" RESTART WITH 1;");

			return Ok(new { message = "Tabla limpia y contador = 1"});
		}

		[HttpGet]
		public async Task<ActionResult<List<Alumno>>> ListAllAlumnos(){
			var alumnosDto = await _context.Alumnos
				.Select(alumno => new AlumnoGetDto{
					ID = alumno.ID,
					Name = alumno.Name
				}).ToListAsync();
			if (alumnosDto == null){
				return NotFound(new { message = "No existe ningún alumno"});
			}

			return Ok(alumnosDto);
		}

		[HttpPost]
		public async Task<ActionResult<Alumno>> PostAlumno([FromBody] AlumnoCreateDto dto){
			var alumno = new Alumno{
				Name = dto.Name,
				CursoID = dto.CursoID
			};
			if (alumno.CursoID == null || alumno.CursoID == 0){
				return NotFound(new { message = "No se encontró la id del curso"});
			}

			_context.Alumnos.Add(alumno);
			await _context.SaveChangesAsync();
			return CreatedAtAction(nameof(GetAlumnos), new {id = alumno.ID}, alumno);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutAlumno([FromRoute] int id, [FromBody] AlumnoUpdateDto dto){
			var alumno = await _context.Alumnos.FindAsync(id);
			if (alumno == null){
				return NotFound(new { message = "No se encontró ningún alumno con esa ID"});
			}
			if (dto.Name != null){
				if (string.IsNullOrWhiteSpace(dto.Name)){
					return BadRequest("Nombre Inválido");
				}
				alumno.Name = dto.Name;
			}
			else{
				dto.Name = alumno.Name;
			}

			if (dto.CursoID.HasValue){
				alumno.CursoID = dto.CursoID.Value;
			}

			await _context.SaveChangesAsync();
			return Ok(alumno);

			
		}
		
		[HttpDelete]
		public async Task<IActionResult> DeleteAlumno([FromQuery] AlumnoDeleteDto request){
			if (string.IsNullOrEmpty(request.Password) || !BCrypt.Net.BCrypt.Verify(request.Password, _config["AdminConfig:AdminHash"])){
				return Unauthorized(new { message = "Acceso denegado, te faltan permisos."});
			}

			var alumno = await _context.Alumnos.FindAsync(request.ID);
			if (alumno == null){
				return NotFound(new { message = "No se encontró ningún alumno con esa ID"});
			}
			_context.Alumnos.Remove(alumno);

			await _context.SaveChangesAsync();

			return NoContent();
		}

	}
}

