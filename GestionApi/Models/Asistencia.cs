using System.ComponentModel.DataAnnotations;

namespace GestionApi.Models;

public class Asistencia{
	public int ID {get; set;}

	[Required]
	public DateTime Fecha {get; set;}
	public bool Presente {get; set;}
	[Required]
	public int AlumnoID {get; set;}
	public Alumno? Alumno {get; set;}
	
}
