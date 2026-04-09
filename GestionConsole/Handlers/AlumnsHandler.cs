using GestionApiClient;

namespace GestionConsole.Handlers.AlumnsHandler;

public class AlumnoHandler{
	private readonly ZinclasClient _client;

	public AlumnoHandler(ZinclasClient client){
		_client = client;
	}

	public async Task Handle(string accion, Dictionary<string, string> flags){

		switch (accion){
			case "post" or "create":
				try {
					await _client.CreateAlumnAsync(flags["name"], int.Parse(flags["cursoid"]));
					Console.WriteLine("Alumno creado correctamente.");
				}
				catch {
					Console.WriteLine("Formato incorrecto, debes usar las flags --name=nombre y --id=numero");
				}
				break;

			case "get":
				try {
					var alumn = await _client.GetAlumnAsync(int.Parse(flags["id"]));
					Console.WriteLine(
						"----------------------\n"+
						$"Nombre: {alumn.Name}\n"+
						$"ID: {alumn.ID}\n"+ 
						$"CursoID: {alumn.CursoID}\n"+
						$"Año: {alumn.año}\n" +
						$"Division: {alumn.division}\n" +
						"----------------------\n"
					);
				}
				catch {
					var alumns = await _client.GetAlumnsAsync();
					foreach (var alumn in alumns){
						Console.WriteLine(
							"----------------------\n"+
							$"CursoID: {alumn.CursoID}\n"+
							$"Nombre: {alumn.Name}\n"+
							$"ID: {alumn.ID}\n"+ 
							$"Año: {alumn.año}\n" +
							$"Division: {alumn.division}\n" +
							"----------------------\n"
						);
					}
				}
				break;

			case "update" or "put":
				try {
					await _client.UpdateAlumnAsync(int.Parse(flags["id"]), int.Parse(flags["cursoid"]), flags["name"]);
					Console.WriteLine("Alumno actualizado correctamente");
				}
				catch {
					Console.WriteLine("Formato incorrecto, debes usar las flags --name=nombre, --cursoid=numero y --id=numero");
				}
				break;

			case "delete":
				try {
					await _client.DeleteAlumnAsync(int.Parse(flags["id"]), flags["pass"]);
				}
				catch {
					Console.WriteLine("Formato incorrecto, debes usar las flags --id=numero, y --pass=contraseña por seguridad");
					
				}
				break;

			default:
				Console.WriteLine($"acción '{accion}' no es válida para alumnos");
				break;
		}
	}
}
