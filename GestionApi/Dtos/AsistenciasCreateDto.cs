namespace GestionApi.Dtos;

public class AsistenciasCreateDto{
	public DateTime Fecha {get; set;}
	public int AlumnoID {get; set;}
	public bool Presente {get; set;}
}
public class AsistenciasGetDto{
	public int AlumnoID {get; set;}
	public DateTime Fecha {get;set;}
	public bool Presente {get;set;}
}
