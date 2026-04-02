using GestionApiClient;
using GestionConsole.Handlers.AlumnsHandler;

public class GestionApiConsole{

	public static async Task Main(){
		Console.CancelKeyPress += (sender, e) => {
			Console.Write("\nSaliendo... \n");
		};

		Console.WriteLine("Iniciando Cliente");

		ZinclasClient client = new ZinclasClient("http://localhost:5019/");
		CourseHandler courseHandler = new CourseHandler(client);
		AsistenciaHandler asistenciaHandler = new AsistenciaHandler(client);

		Console.WriteLine("Cliente iniciado");
		while (true){
			Console.Write("> ");
			String? input = Console.ReadLine();
			if (string.IsNullOrWhiteSpace(input)) {
				DisplayExample();
				continue;
			}
			String[] inputSplitted = input.ToString().Split(" ", StringSplitOptions.RemoveEmptyEntries);
		
			string tabla = inputSplitted[0].ToLower();
			if (tabla == "exit" || tabla == "quit"){ 
				Console.WriteLine("Saliendo...");
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
				case ("estudiantes"):
					break;
				case ("curso"):
					await courseHandler.Handle(accion, flags);
					break;
				default:
					Console.WriteLine("Esa tabla no existe");
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


