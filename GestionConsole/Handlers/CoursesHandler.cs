using GestionApiClient;

namespace GestionConsole.Handlers.CoursesHandler;


public class CourseHandler{
	private readonly ZinclasClient _client;

	public CourseHandler(ZinclasClient client){
		_client = client;
	}
	public async Task Handle(string accion, Dictionary<string, string> flags){
		switch (accion){
			case "post":
				try {
					await _client.CreateCoursesAsync(int.Parse(flags["año"]), int.Parse(flags["division"]));
				}
				catch(Exception ex){
					Console.WriteLine($"Error {ex.Message}");

				}
				//TODO: extraer diccionario y llamar a _client.PostAsistenciaAsync
				break;
			case "get":
				//TODO: extraer diccionario y llamar a _client.GetAsistenciaAsync
				if (!flags.ContainsKey("id")){
					var courses = await _client.GetCoursesAsync();
					foreach (var course in courses){
						Console.WriteLine(
								"---------------------------------- \n" +
								$"Año: {course.año} \n" +
								$"Division: {course.division} \n" +
								$"ID: {course.ID} \n" +
								"---------------------------------- \n" 
						);
					}

				}
				else {
					var course = await _client.GetCourseAsync(int.Parse(flags["id"]) );
					Console.WriteLine(
							"---------------------------------- \n" +
							$"Año: {course.año} \n" + 
							$"Division: {course.division} \n" + 
							$"ID: {course.ID} \n" +
							"---------------------------------- \n" 

					);

				}
				break;
			case "delete":
				try {
					var delete = await _client.DeleteCourseAsync(int.Parse(flags["año"]), int.Parse(flags["division"]), flags["pass"]);
					if (delete){
						Console.WriteLine("Curso eliminado correctamente.");
					}
					else{
						Console.WriteLine("Error eliminando el curso");
					}
				}
				catch {
					Console.WriteLine("Error, debes de especificar un año y división usando --año=numero --division=numero --pass=texto");
				}
				break;
			case "put":
				try {
					await _client.UpdateCourseAsync(int.Parse(flags["id"]), int.Parse(flags["año"]), int.Parse(flags["division"]));
				}
				catch {
					Console.WriteLine("Error, debes especificar una id, año y división usando --id=numero --año=numero --division=numero");
				}
				break;
				
			default:
				Console.WriteLine($"acción '{accion}' no es válida para asistencias");
				break;
		}
	}
}
