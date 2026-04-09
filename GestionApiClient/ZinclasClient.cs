using System.Net.Http.Json;

namespace GestionApiClient;

public partial class ZinclasClient
{
	private HttpClient _http;

	public ZinclasClient(string baseUrl){
		_http = CreateClient(baseUrl);
	}
	private HttpClient CreateClient(string url){
		return new HttpClient {
			BaseAddress = new Uri(url.EndsWith("/") ? url : url + "/")
		};
	}
	public void UpdateClient(string baseUrl){
		_http.Dispose();
		_http = CreateClient(baseUrl);
	}

	public async Task<bool> CheckUrlHealth(){
		bool isValid = false;
		try{
			var response = await _http.GetAsync("/health");
			if (response.IsSuccessStatusCode){
				isValid = true;
			}
		}
		catch{
			isValid = false;
		}
		return isValid;
	}
}

