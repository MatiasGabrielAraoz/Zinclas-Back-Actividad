namespace GestionApi.Dtos;

public class AsistenciasCreateDto{
	public DateTime fecha {get; set;}
	public int alumnoID {get; set;}
	public bool presente {get; set;}
}
public class AsistenciasGetDto{
	public int alumnoID {get; set;}
	public DateTime fecha {get; set;}
	public bool presente {get; set;}
}
public class AsistenciasDeleteDto{
	public int alumnoID {get; set;}
	public DateTime fecha {get; set;}
	public string password {get; set;} = "";
}
public class AsistenciasUpdateDto{
	public DateTime fecha {get; set;}
	public int alumnoID {get; set;}
	public bool? presente {get; set;}
}
public class AsistenciasResumenDto{
	public int presentes {get; set;}
	public int ausentes {get; set;}
	public int promedioPresencias {get; set;}
}
