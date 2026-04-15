using GestionApiClient;

namespace GestionConsole.Handlers.CoursesHandler;


public class AsistenciaHandler{
	private readonly ZinclasClient _client;

	public AsistenciaHandler(ZinclasClient client){
		_client = client;
	}
	public async Task Handle(string accion, Dictionary<string, string> flags){
		switch (accion){
			case "get":
				try{
					await _client.GetAsistancesResumeAsync(int.Parse(flags["id"]));
				}
				catch{
					await _client.GetAsistancesAsync();
				}
				break;
			case "post":
				break;

			default:
				Console.WriteLine("No es una acción válida");
				break;
		}
	}
}
