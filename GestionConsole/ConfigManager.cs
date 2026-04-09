using System.Text.Json;
using GestionConsole;

namespace GestionConsole.ConfigManager;

public class ConfigManager{
	public static AppConfig Load(string filename = "config.json"){
		string configFile = "config.json";
		AppConfig appConfig;
		if (File.Exists(configFile)){
			string jsonString = File.ReadAllText(configFile);
			appConfig = JsonSerializer.Deserialize<AppConfig>(jsonString) ?? new AppConfig();
			Console.WriteLine("Archivo de configuración cargado");
		}
		else {
			Console.WriteLine("No se encontró ningun archivo de configuración, intentando usar default (http://localhost:5019)");
			appConfig = new AppConfig{
				ApiUrl = "http://localhost:5019"
			};
		}
		return appConfig;
	}
}

