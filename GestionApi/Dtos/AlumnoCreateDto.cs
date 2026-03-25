namespace GestionApi.Dtos
{
	public class AlumnoCreateDto{
		public string Name {get; set;}
		public int CursoID {get; set;}
	}
	public class AlumnoGetDto{
		public string Name {get; set;} = "";
		public int ID {get; set;}
	}
	public class AlumnoDeleteDto{
		required public int ID {get; set;}
		required public string Password {get; set;} = "";
	}
}
