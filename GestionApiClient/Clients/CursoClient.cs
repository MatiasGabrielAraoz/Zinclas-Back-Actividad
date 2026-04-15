using System.Net.Http.Json;
using GestionApi.Dtos;

namespace GestionApiClient;

public partial class ZinclasClient{
	
	public async Task<List<CursosGetDto>> GetCoursesAsync(){
		// TODO: Hacer llamada a la api para conseguir una lista con todos los cursos
		try {
			var courses = await _http.GetFromJsonAsync<List<CursosGetDto>>("api/cursos");
			return courses ?? new List<CursosGetDto>();
		}
		catch (Exception ex){
			Console.WriteLine($"Error al conectar: {ex.Message}");
			return new List<CursosGetDto>();
		}
	}
	public async Task<CursosGetDto> GetCourseAsync(int id){
		try {
			var course = await _http.GetFromJsonAsync<CursosGetDto>($"api/cursos/{id}");
			return course ?? new CursosGetDto();
			
		}
		catch (Exception ex){
			Console.WriteLine($"Error al conectar: {ex.Message}");
			return new CursosGetDto();
		}
	}

	public async Task CreateCoursesAsync(int año, int division){
		try {
			var newCourse = new { año = año, division = division};
			var courses = await _http.PostAsJsonAsync("api/cursos", newCourse);
		}
		catch (Exception ex){
			Console.WriteLine($"Error al conectar: {ex.Message}");
			return;
		}
	}

	public async Task<bool> DeleteCourseAsync(int año, int division, String password){
		try {
			CursosDeleteDto courseToDelete = new CursosDeleteDto {
				año = año,
				division = division,
				password = password,
			};
			var request = new HttpRequestMessage(HttpMethod.Delete, "api/cursos")
			{
				Content = JsonContent.Create(courseToDelete)
			};
			var response = await _http.SendAsync(request);
			bool eliminado = true;

			if (!response.IsSuccessStatusCode){
				var errorbody = await response.Content.ReadAsStringAsync();
				Console.WriteLine($"Error eliminando: {errorbody}");
				eliminado = false;
			}
			return eliminado;
		}
		catch (Exception ex){
			Console.WriteLine($"Error al conectar: {ex.Message}");
			return false;
		}
	}

	public async Task UpdateCourseAsync(int id, int año, int division){
		try{
			CursosUpdateDto curso = new CursosUpdateDto {
				año = año,
				division = division,
				id = id
			};

			var request = new HttpRequestMessage(HttpMethod.Put, $"api/cursos/{id}"){
				Content = JsonContent.Create(curso)
			};
			var response = await _http.SendAsync(request);
			if (!response.IsSuccessStatusCode){
				Console.WriteLine($"Error {response.StatusCode}");
			}
			else {
				Console.WriteLine($"se actualizaron los valores correctamente.");
			}

		}
		catch (Exception ex) {
			Console.WriteLine($"Error al conectar: {ex.Message}");
			return;

		}
		}
		
	}

