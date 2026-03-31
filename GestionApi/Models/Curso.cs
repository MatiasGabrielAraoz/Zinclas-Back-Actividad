namespace GestionApi.Models;

public class Curso{
	public int ID {get; set;}
	public int año {get; set;}
	public int division {get; set;}
	public ICollection<Alumno> Alumnos {get; set;} = new List<Alumno>();
}
