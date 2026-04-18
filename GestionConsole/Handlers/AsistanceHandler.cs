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
					var asistenciaDto = await _client.GetAsistancesResumeAsync(int.Parse(flags["id"]));
					Console.WriteLine(
						$"Nombre: {asistenciaDto.nombre} \n"+
						$"Presentes: {asistenciaDto.presentes} \n"+
						$"Ausentes: {asistenciaDto.ausentes} \n"+
						$"Asistió el {asistenciaDto.porcentajePresencias}% de las clases"
					);
				}
				catch{
					await _client.GetAsistancesAsync();
				}
				break;
			case "post":
				try {
					int id = int.TryParse(flags.GetValueOrDefault("id"), out int id1) ? id1 :
						int.TryParse(flags.GetValueOrDefault("alumnoid"), out int id2) ? id2 : 0;

					if (!DateTime.TryParse(flags.GetValueOrDefault("fecha"), out DateTime fechaValida)){
						return;
					}

					string presenteVal = flags.GetValueOrDefault("presente")?.ToLower() ?? "";
					if (presenteVal == ""){
						Console.WriteLine("No Especificaste si el alumno estaba presente en ese día");
						return;
					}

					bool presente = presenteVal == "1" || presenteVal == "presente" || presenteVal == "si" || presenteVal == "true";
					await _client.PostAsistanceAsync(id, fechaValida, presente);
				}
				catch (Exception ex){
					Console.WriteLine($"Error: {ex.Message}");
				}
				break;
			case "update":
				try {
					int id = int.TryParse(flags.GetValueOrDefault("id"), out int id1) ? id1 :
						int.TryParse(flags.GetValueOrDefault("alumnoid"), out int id2) ? id2 : 0;

					if (!DateTime.TryParse(flags.GetValueOrDefault("fecha"), out DateTime fechaValida)){
						return;
					}

					string presenteVal = flags.GetValueOrDefault("presente")?.ToLower() ?? "";
					if (presenteVal == ""){
						Console.WriteLine("No Especificaste si el alumno estaba presente en ese día");
						return;
					}

					if (presenteVal != null){
						bool presente = presenteVal == "1" || presenteVal == "presente" || presenteVal == "si" || presenteVal == "true";
						await _client.UpdateAsistanceAsync(id, fechaValida, presente);
					}
					else {
						bool? presente = null;
						await _client.UpdateAsistanceAsync(id, fechaValida, presente);
					}
					Console.WriteLine("Asistencia actualizada");
				}
				catch (Exception ex){
					Console.WriteLine($"Error: {ex.Message}");
				}
				break;
			case "delete":
				try {
					int id = int.TryParse(flags.GetValueOrDefault("id"), out int id1) ? id1 :
						int.TryParse(flags.GetValueOrDefault("alumnoid"), out int id2) ? id2 : 0;

					if (!DateTime.TryParse(flags.GetValueOrDefault("fecha"), out DateTime fechaValida)){
						return;
					}
					await _client.DeleteAsistanceAsync(id, fechaValida, flags["pass"]);
					
				}
				catch (Exception ex){
					Console.WriteLine($"Error: {ex.Message}");
				}
				break;

			default:
				Console.WriteLine("No es una acción válida");
				break;
		}
	}
}
