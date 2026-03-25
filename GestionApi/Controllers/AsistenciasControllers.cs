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
		[HttpGet("{AlumnoID}/{Fecha}")]
		public async Task<ActionResult<Asistencia>> GetAsistencia(int AlumnoID, DateTime Fecha){
			var asistencia = await _context.Asistencias
				.FirstOrDefaultAsync(a => a.AlumnoID == AlumnoID && a.Fecha.Date == Fecha.Date);
			if (asistencia == null){
				return NotFound(new {message = "No se encontró ese alumno o esa fecha"});
			}
			return Ok(asistencia);

		}
		[HttpPost("{AlumnoID}/{Fecha}/{Presente}")]
		public async Task<ActionResult> PostAsistencia(int AlumnoID, DateTime Fecha, bool Presente){
			return Ok();
		}
	}
}
