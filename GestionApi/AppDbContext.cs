using Microsoft.EntityFrameworkCore;
using GestionApi.Models;

namespace GestionApi.Data;

public class AppDbContext : DbContext{

	public AppDbContext(DbContextOptions<AppDbContext> options): base(options){
	}
	
	public DbSet<Alumno> Alumnos {get; set;}
	public DbSet<Asistencia> Asistencias {get; set;}
	
}
