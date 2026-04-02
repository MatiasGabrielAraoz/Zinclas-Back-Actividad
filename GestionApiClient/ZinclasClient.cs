using System.Net.Http.Json;

namespace GestionApiClient;

public partial class ZinclasClient
{
	private readonly HttpClient _http;

	public ZinclasClient(string baseUrl){
		_http = new HttpClient { BaseAddress = new Uri(baseUrl)};
	}
}

