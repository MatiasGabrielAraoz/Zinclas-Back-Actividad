namespace GestionApi.Dtos
{
	public class AlumnoCreateDto{
		public string Name {get; set;}
		public int CursoID {get; set;}
	}
	public class AlumnoGetDto{
		public string Name {get; set;} = "";
		public int ID {get; set;}
		public string NombreCurso {get; set;} 
	}
	public class AlumnoDeleteDto{
		public int ID {get; set;}
		public string Password {get; set;}
	}
}
