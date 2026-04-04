using GestionApiClient;

namespace GestionConsole.Handlers.AlumnsHandler;

public class AlumnoHandler{
	private readonly ZinclasClient _client;

	public AlumnoHandler(ZinclasClient client){
		_client = client;
	}
	public async Task Handle(string accion, Dictionary<string, string> flags){
		switch (accion){
			case "post":
				//TODO: extraer diccionario y llamar a _client.PostAsistenciaAsync
				try {
					await _client.CreateAlumnAsync(flags["name"], int.Parse(flags["cursoid"]));
				}
				catch {
					Console.WriteLine("Formato incorrecto, debes usar las flags --name=nombre y --id=numero");
				}
				break;

			case "get":
				//TODO: extraer diccionario y llamar a _client.GetAsistenciaAsync
				try {
					var alumn = await _client.GetAlumnAsync(int.Parse(flags["id"]));
					Console.WriteLine($"IDCurso: {alumn.ID}");
				}
				catch {
					var alumns = await _client.GetAlumnsAsync();
					foreach (var alumn in alumns){
						Console.WriteLine(
								"----------------------\n"+
								$"CursoID: {alumn.CursoID}\n"+
								$"Nombre: {alumn.Name}\n"+
								"----------------------\n"
						);
					}
				}
				break;
			case "update":
				try {
					await _client.UpdateAlumnAsync(int.Parse(flags["id"]), int.Parse(flags["cursoid"]), flags["name"]);
					
				}
				catch {

				}
				break;
			default:
				Console.WriteLine($"acción '{accion}' no es válida para asistencias");
				break;
		}
	}
}
