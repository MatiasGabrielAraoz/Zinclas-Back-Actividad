using System.Net.Http.Json;
using GestionApi.Dtos;

namespace GestionApiClient;

public partial class ZinclasClient{
	
	// si el no se especifica una id devolver todos los alumnos
	public async Task<List<AsistenciasGetDto>> GetAsistancesAsync(){
		// TODO: Hacer llamada a la api para conseguir una lista con todos los cursos
		try {
			var asistencias = await _http.GetFromJsonAsync<List<AsistenciasGetDto>>($"api/asistencias");
			return asistencias ?? new List<AsistenciasGetDto>();
		}
		catch (Exception ex){
			Console.WriteLine($"Error al conectar: {ex.Message}");
			return new List<AsistenciasGetDto>();
		}
	}
	public async Task<AsistenciasResumenDto> GetAsistancesResumeAsync(int id){
		try {
			AsistenciasResumenDto? resumen = await _http.GetFromJsonAsync<AsistenciasResumenDto>($"api/asistencias/{id}");
			if (resumen == null) {
				Console.WriteLine("No existe un resumen para un alumno con esa id");
				return new AsistenciasResumenDto {};
			}
			return resumen;
		
		}
		catch (Exception ex){
			Console.WriteLine($"Error: {ex.Message}");
			return new AsistenciasResumenDto {};
		}
		
	}
}

