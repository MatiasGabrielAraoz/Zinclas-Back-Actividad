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
				Name = curso.Name,
				ID = curso.ID
			};
			return Ok(cursoDto);
		}

		[HttpPost]
		public async Task<ActionResult<Curso>> CrearCurso([FromQuery] CursosCreateDto dto){
			
			var cursoexistente = await _context.Cursos.AnyAsync(
				a => a.ID == dto.ID
			);

			Curso cursocreated = new Curso{
				Name = dto.Name,
				ID = dto.ID
			};

			await _context.Cursos.AddAsync(cursocreated);
			await _context.SaveChangesAsync();
			return Ok(cursocreated);
			
		}
	}




}
