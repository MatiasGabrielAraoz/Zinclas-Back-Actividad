using GestionApiClient;

namespace GestionConsole.Handlers.AlumnsHandler;


public class CourseHandler{
	private readonly ZinclasClient _client;

	public CourseHandler(ZinclasClient client){
		_client = client;
	}
	public async Task Handle(string accion, Dictionary<string, string> flags){
		switch (accion){
			case "post":
				await _client.CreateCoursesAsync(int.Parse(flags["año"]), int.Parse(flags["division"]));
				//TODO: extraer diccionario y llamar a _client.PostAsistenciaAsync
				break;
			case "get":
				//TODO: extraer diccionario y llamar a _client.GetAsistenciaAsync
				if (flags["id"] == null){
					await _client.GetCoursesAsync();
				}
				else {
					await _client.GetCourseAsync(int.Parse(flags["id"]) );
				}
				break;
			default:
				Console.WriteLine($"acción '{accion}' no es válida para asistencias");
				break;
		}
	}
}
