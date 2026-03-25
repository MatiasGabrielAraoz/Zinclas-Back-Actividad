using Microsoft.AspNetCore.Mvc;
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

		// GET: /api/Alumnos/{id}
		[HttpGet("{id}")]
		public async Task<ActionResult<Alumno>> GetAlumnos(int id){
			var alumno = await _context.Alumnos
				.Include(a => a.Curso)
				.FirstOrDefaultAsync(a => a.ID == id);
			if (alumno == null){
				return NotFound(new { message = "No se encontró ningún alumno con esa ID"});
			}
			return Ok(new AlumnoGetDto {
				ID = alumno.ID,
				Name = alumno.Name,
				NombreCurso = alumno.Curso?.Name ?? "Sin curso",
			});
		}

		[HttpPost]
		public async Task<ActionResult<Alumno>> PostAlumno(AlumnoCreateDto dto){
			var alumno = new Alumno{
				Name = dto.Name,
				CursoID = dto.CursoID
			};

			_context.Alumnos.Add(alumno);
			await _context.SaveChangesAsync();
			return CreatedAtAction(nameof(GetAlumnos), new {id = alumno.ID}, alumno);
		}
		
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAlumno(int id, [FromBody] AlumnoDeleteDto request){

			if (string.IsNullOrEmpty(request.Password) || !BCrypt.Net.BCrypt.Verify(request.Password, _config["AdminConfig:AdminHash"])){
				return Unauthorized(new { message = "Acceso denegado, te faltan permisos."});
			}
			var alumno = await _context.Alumnos.FindAsync(id);
			if (alumno == null){
				return NotFound(new { message = "No se encontró ningún alumno con esa ID"});
			}
			_context.Alumnos.Remove(alumno);

			await _context.SaveChangesAsync();

			return NoContent();
		}

	}
}

