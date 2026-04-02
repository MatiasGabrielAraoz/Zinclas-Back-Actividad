using GestionApiClient;

namespace GestionConsole.Handlers.AlumnsHandler;


public class AsistenciaHandler{
	private readonly ZinclasClient _client;

	public AsistenciaHandler(ZinclasClient client){
		_client = client;
	}
	public async Task Handle(string accion, Dictionary<string, string> flags){
		switch (accion){
			case "post":
				//TODO: extraer diccionario y llamar a _client.PostAsistenciaAsync
				break;
			case "get":
				//TODO: extraer diccionario y llamar a _client.GetAsistenciaAsync
				break;
			default:
				Console.WriteLine($"acción '{accion}' no es válida para asistencias");
				break;
		}
	}
}
