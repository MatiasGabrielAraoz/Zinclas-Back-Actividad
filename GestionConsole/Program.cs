using System.Text.Json;
using GestionApiClient;
using GestionConsole.ConfigManager;
using GestionConsole.Handlers.AlumnsHandler;
using GestionConsole.Handlers.CoursesHandler;


public class AppConfig{
	public string ApiUrl {get; set;} = string.Empty;
}

public class GestionApiConsole{

	public static async Task Main(){
		Console.CancelKeyPress += (sender, e) => {
			Console.Write("\nSaliendo... \n");
		};
		Console.WriteLine("Iniciando Cliente");

		AppConfig appConfig = ConfigManager.Load();

		ZinclasClient client = new ZinclasClient(appConfig.ApiUrl);

		CourseHandler courseHandler = new CourseHandler(client);
		AlumnoHandler alumnHandler = new AlumnoHandler(client);

		Console.WriteLine("Cliente iniciado");

		bool running = true;

		while (running){
			bool urlIsValid = await client.CheckUrlHealth();
			if (!urlIsValid){
				Console.WriteLine("No se pudo conectar con la api en la url configurada, iniciando en modo seguro, puedes cambiar la url con apiurl --url=");
			}
			Console.Write("> ");
			String? input = Console.ReadLine();
			if (string.IsNullOrWhiteSpace(input)) {
				DisplayExample();
				continue;
			}
			String[] inputSplitted = input.ToString().Split(" ", StringSplitOptions.RemoveEmptyEntries);
		
			string tabla = inputSplitted[0].ToLower();
			List<String> exitCommands = ["exit", "quit", "salir"];
			foreach (String exitCommand in exitCommands){
				if (tabla == exitCommand){
				Console.WriteLine("Saliendo...");
				running = false;
				break;
				}
			}
			if (running == false){
				break;
			}

			if (inputSplitted.Length < 2){
				Console.WriteLine("Falta especificar la acción (get, post, put, delete)");
				DisplayExample();
				continue;
			}

			string accion = inputSplitted[1].ToLower();
			var flags = ParseFlags(inputSplitted);
			switch (tabla){
				case "alumno" or "alumnos":
					await alumnHandler.Handle(accion, flags);
					break;

				case "curso" or "cursos":
					await courseHandler.Handle(accion, flags);
					break;

				case "asistencia" or "asistencias":
					Console.WriteLine("Work in progress");
					break;
				case "apiurl":
					try{
						
					}
					catch{
				
					}


				default:
					Console.WriteLine("Esa tabla no existe, las tablas disponibles son: alumnos, cursos y asistencias");
					break;

			}		
		}
	}

	private static void DisplayExample(){
			Console.WriteLine("Uso: «tabla» «acción» «valores»");
			Console.WriteLine("Ej: estudiante PUT --AlumnoID=10 --CursoID=2");
			Console.WriteLine("Ej: curso PUT --Año=1 --Division=2 ");
	}
	private static Dictionary<string, string> ParseFlags(String[] args){
		var flags = new Dictionary<string, string>();

		foreach (var arg in args.Skip(2)){
			if (arg.StartsWith("--") && arg.Contains("=")){
				var part = arg.Split("=", 2);

				string key = part[0].Replace("--", "").ToLower();
				string value = part[1];

				flags[key] = value;
			}
		}
		return flags;
	}
			
	}


