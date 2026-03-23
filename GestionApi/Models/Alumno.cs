namespace GestionApi.Models;

public class Alumno{
	public int ID  {get; set;}
	public string Name {get; set;} = "";

	public int CursoID {get; set;}

	public Curso? Curso {get; set;}
		
}
