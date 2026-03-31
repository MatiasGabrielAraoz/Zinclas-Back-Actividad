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

	public class CursosController : ControllerBase{
		private readonly AppDbContext _context;
		private readonly IConfiguration _config;
		public CursosController(AppDbContext context, IConfiguration config){
			_context = context;
			_config = config;
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Curso>> GetCurso([FromRoute] int id){
			var curso = await _context.Cursos
				.Include(a => a.Alumnos)
				.FirstOrDefaultAsync(a => a.ID == id);
			if (curso == null){
				return NotFound(new { message = "No se encontró un curso con esa id"});
			}
			CursosGetDto cursoDto = new CursosGetDto{
				año = curso.año,
				ID = curso.ID
			};
			return Ok(cursoDto);
		}

		[HttpPost("curso")]
		public async Task<ActionResult<Curso>> CreateCurso([FromBody] CursosCreateDto dto){
			var curso = await _context.Cursos.FirstOrDefaultAsync(
				c => c.año == dto.año && c.division == dto.division 
			);

			if (curso != null){
				return BadRequest(new { message = "Ya existe un curso con ese año o división"});
			}
			
			Curso cursocreated = new Curso{
				año = dto.año,
				division = dto.division
			};

			await _context.Cursos.AddAsync(cursocreated);
			await _context.SaveChangesAsync();

			return Ok(cursocreated);
			
		}
		[HttpDelete]
		public async Task<IActionResult> DeleteCurso([FromQuery] CursosDeleteDto request){
			if (string.IsNullOrEmpty(request.password) || !BCrypt.Net.BCrypt.Verify(request.password, _config["AdminConfig:AdminHash"])){
				return Unauthorized(new { message = "Contraseña incorrecta, no podés eliminar un curso"});
			}
			var curso = await _context.Cursos.FindAsync(request.año, request.division);
			if (curso == null){
				return BadRequest(new { message = "No se encontró ningún curso con ese año y división"});
			}

			_context.Cursos.Remove(curso);
			await _context.SaveChangesAsync();
			return NoContent();
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<Curso>> UpdateCurso(int id, [FromQuery] CursosUpdateDto dto){
			if (dto.id != id) return BadRequest(new { message = "La id indicada por el cuerpo no coincide con el ID de la solicitud"});
			var curso = await _context.Cursos.FindAsync(dto.id);
			if (curso == null){
				return BadRequest(new { message = "No se encontró ningún curso con esa id"});
			}
			curso.año = dto.año ?? curso.año;
			curso.division = dto.division ?? curso.division;

			await _context.SaveChangesAsync();
			return Ok();


		}
	}
}
