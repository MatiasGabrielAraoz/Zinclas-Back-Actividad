using System.Text.Json;
using GestionConsole;
using GestionApiClient;

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
	public static void Save(AppConfig config, String filename = "config.json"){
		try{
			var options = new JsonSerializerOptions { WriteIndented = true};
			string jsonString = JsonSerializer.Serialize(config, options);
			File.WriteAllText(filename, jsonString);
		}
		catch (Exception ex){
			Console.WriteLine($"Error al guardar la config: {ex.Message}");
		}
	}
	public async Task<bool> ApiUrl(AppConfig config, ZinclasClient client, string url){
		try{
			client.UpdateClient(url);
			config.ApiUrl = url ?? config.ApiUrl;
			Save(config);
			bool IsValid = await client.CheckUrlHealth();

			if (IsValid){
				Console.WriteLine("Se pudo conectar correctamente");
			}
			else {
				Console.WriteLine("No se pudo conectar");
			}
			return true;
		}
		catch{
			Console.WriteLine("Debes especificar la flag \"url \"");
			return false;
		}
	}
}

