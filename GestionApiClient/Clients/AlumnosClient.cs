using System.Net.Http.Json;
using GestionApi.Dtos;

namespace GestionApiClient;

public partial class ZinclasClient{
	
	// si el no se especifica una id devolver todos los alumnos
	public async Task<List<AlumnoGetDto>> GetAlumnsAsync(){
		// TODO: Hacer llamada a la api para conseguir una lista con todos los cursos
		try {
			var courses = await _http.GetFromJsonAsync<List<AlumnoGetDto>>("api/alumnos");
			return courses ?? new List<AlumnoGetDto>();
		}
		catch (Exception ex){
			Console.WriteLine($"Error al conectar: {ex.Message}");
			return new List<AlumnoGetDto>();
		}
	}

	// si se especifica una id devolver el alumno con esa id
	public async Task<AlumnoGetDto> GetAlumnAsync(int id){
		try {
			var alumn = await _http.GetFromJsonAsync<AlumnoGetDto>($"api/alumnos/{id}");
			return alumn ?? new AlumnoGetDto();
			
		}
		catch (Exception ex){
			Console.WriteLine($"Error al conectar: {ex.Message}");
			return new AlumnoGetDto();
		}
	}

	// Crear un alumno con nombre e id del curso
	public async Task<bool> CreateAlumnAsync(String name, int cursoID){
		try {
			var newAlumn = new AlumnoCreateDto{
				Name = name,
				CursoID = cursoID
			};
			var request = new HttpRequestMessage(HttpMethod.Post, "api/alumnos"){
				Content = JsonContent.Create(newAlumn)	
			};

			var response = await _http.SendAsync(request);
			bool created = true;

			if (!response.IsSuccessStatusCode){
				Console.WriteLine($"Error creando el alumno: {response.StatusCode}");
				var errorbody = await response.Content.ReadAsStringAsync();
				Console.WriteLine($"Error eliminando: {errorbody}");
				created = false;
			}
			return created;
		}
		catch (Exception ex){
			Console.WriteLine($"Error al conectar: {ex.Message}");
			return false;
		}
	}

	// eliminar un alumno requiere una id y contraseña especificada en una variable de entorno de la api
	public async Task<bool> DeleteAlumnAsync(int id, String password){
		try {
			AlumnoDeleteDto alumnToDelete = new AlumnoDeleteDto {
				ID = id,				
				Password = password
			};
			var request = new HttpRequestMessage(HttpMethod.Delete, "api/alumnos")
			{
				Content = JsonContent.Create(alumnToDelete)
			};
			var response = await _http.SendAsync(request);
			bool eliminado = true;

			// si no anduvo devolver el error 
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

	// toma una id del alumno q queres cambiar y el nuevo nombre, también deberia de hacer q acepte una nueva id de curso
	public async Task UpdateAlumnAsync(int id, int? cursoID, String? name){
		try{
			AlumnoUpdateDto alumn = new AlumnoUpdateDto {
				CursoID = cursoID,
				Name = name
			};

			var request = new HttpRequestMessage(HttpMethod.Put, $"api/alumnos/{id}"){
				Content = JsonContent.Create(alumn)
			};
			var response = await _http.SendAsync(request);
			if (!response.IsSuccessStatusCode){
				String errorbody = await response.Content.ReadAsStringAsync();
				Console.WriteLine($"Error {response.StatusCode}");

				if (errorbody.Contains("FK_Alumnos_Cursos_CursoID")){
					Console.WriteLine("El curso no existe en la base de datos");
				}
				else {
					Console.WriteLine($"{errorbody}");
				}
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

