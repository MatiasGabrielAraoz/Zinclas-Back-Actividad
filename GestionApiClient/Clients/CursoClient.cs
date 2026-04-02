using System.Net.Http.Json;
using GestionApi.Dtos;

namespace GestionApiClient;

public partial class ZinclasClient{
	
	public async Task<List<String>> GetCoursesAsync(){
		// TODO: Hacer llamada a la api para conseguir una lista con todos los cursos
		try {
			var courses = await _http.GetFromJsonAsync<List<String>>("api/cursos");
			return courses ?? new List<string>();
		}
		catch (Exception ex){
			Console.WriteLine($"Error al conectar: {ex.Message}");
			return new List<String>();
		}
	}
	public async Task<CursosGetDto> GetCourseAsync(int id){
		try {
			var course = await _http.GetFromJsonAsync<CursosGetDto>($"api/cursos/{id}");
			Console.WriteLine($"Año: {course.año} \ndivision: {course.division}");
			return course;
			
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
}
