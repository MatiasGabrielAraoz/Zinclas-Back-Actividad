using System.ComponentModel.DataAnnotations;

namespace GestionApi.Dtos;

public class CursosGetDto{
	public int ID {get; set;}
	[Range(1,7)]
	public int año {get; set;}
	[Range(1,7)]
	public int division {get; set;}
}

public class CursosCreateDto{
	[Range(1,7)]
	public int año {get; set;}
	[Range(1,7)]
	public int division {get; set;}
}

public class CursosUpdateDto{
	[Range(1,7)]
	public int? año {get; set;}

	[Range(1,7)]
	public int? division {get; set;}
	public int id {get; set;}
}

public class CursosDeleteDto{
	public int año {get; set;}
	public int division {get; set;}
	public string password {get; set;} = "";
}

