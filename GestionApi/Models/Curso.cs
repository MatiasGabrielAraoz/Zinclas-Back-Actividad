namespace GestionApi.Models;

public class Curso{
	public int ID {get; set;}
	public String Name {get; set;} = "";
	public ICollection<Alumno> Alumnos {get; set;} = new List<Alumno>();

}
