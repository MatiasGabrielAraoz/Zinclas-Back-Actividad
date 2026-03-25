namespace GestionApi.Models;

public class Asistencia{
	public DateTime Fecha {get; set;}
	public bool Presente {get; set;}
	public int AlumnoID {get; set;}
	public Alumno? Alumno {get; set;}
	
}
