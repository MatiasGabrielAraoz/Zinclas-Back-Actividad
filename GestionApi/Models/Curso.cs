namespace GestionApi.Models;

public class Curso{
	String Name {get; set;} = "";
	ICollection<Alumno> Alumnos {get; set;} = new List<Alumno>();

}
