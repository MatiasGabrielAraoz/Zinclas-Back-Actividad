using System.Net.Http.Json;
using GestionApi.Dtos;

namespace GestionApiClient;

public partial class ZinclasClient{
	
	// si el no se especifica una id devolver todos los alumnos
	public async Task<List<AsistenciasGetDto>> GetAsistancesAsync(){
		try {
			var response = await _http.GetAsync("api/asistencias");
			if (response.IsSuccessStatusCode){
				return await response.Content.ReadFromJsonAsync<List<AsistenciasGetDto>>() ??
					new List<AsistenciasGetDto>();
			}
			Console.WriteLine($"Api error: {response.StatusCode}");
			return  new List<AsistenciasGetDto>();
		}
		catch (Exception ex){
			Console.WriteLine($"Error al conectar: {ex.Message}");
			return new List<AsistenciasGetDto>();
		}
	}
	public async Task<AsistenciasGetDto> GetAsistanceAsync(int id){
		try {
			var response = await _http.GetAsync($"api/asistencias/{id}");
			if (response.IsSuccessStatusCode){
				return await response.Content.ReadFromJsonAsync<AsistenciasGetDto>() ??
					new AsistenciasGetDto();
			}

			return  new AsistenciasGetDto();
		}
		catch (Exception ex){
			Console.WriteLine($"Error al conectar: {ex.Message}");
			return new AsistenciasGetDto();
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
	public async Task PostAsistanceAsync(int id, DateTime date, bool presente){
		try {
			AsistenciasCreateDto asistencia = new AsistenciasCreateDto {
				fecha = date,
				alumnoID = id,
				presente = presente				
			};
			var request = await _http.PostAsJsonAsync("api/asistencia",asistencia);
			if (!request.IsSuccessStatusCode){
				var errorbody = await request.Content.ReadAsStringAsync();
				throw new Exception(errorbody);
			}
		}	

		catch (Exception ex){
			Console.WriteLine($"Error: {ex.Message}");
		}
	}
	public async Task UpdateAsistanceAsync(int alumnoID, DateTime date, bool? presente){
		try{
			if (presente == null) return;
			AsistenciasUpdateDto asistencia = new AsistenciasUpdateDto{
				fecha = date,
				alumnoID = alumnoID,
				presente = presente
			};
			AsistenciasGetDto asistenciaDto = await GetAsistanceAsync(alumnoID);
			if (asistenciaDto == null) return;
			var request = new HttpRequestMessage(HttpMethod.Put, $"api/asistencias/{alumnoID}"){
				Content = JsonContent.Create(asistencia)
			};
			var response = await _http.SendAsync(request);

			if (!response.IsSuccessStatusCode){
				String errorbody = await response.Content.ReadAsStringAsync();
				throw new Exception(errorbody);
			}
			
		}

		catch (Exception ex){
			Console.WriteLine($"Error {ex.Message}");
		}
	}


	public async Task DeleteAsistanceAsync(int alumnoID, DateTime date, string password){
		AsistenciasDeleteDto alumnoEliminar = new AsistenciasDeleteDto(){
			alumnoID = alumnoID,
			fecha = date,
			password = password
		};
		var request = new HttpRequestMessage(HttpMethod.Delete, $"api/asistencias/"){
			Content = JsonContent.Create(alumnoEliminar)
		};
		var response = await _http.SendAsync(request);
		if (!response.IsSuccessStatusCode){
			var error = await response.Content.ReadAsStringAsync();
			throw new Exception(error);
		}
		
	}
}

