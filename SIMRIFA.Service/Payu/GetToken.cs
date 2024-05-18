using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SIMRIFA.Service.Payu
{
	public class GetToken
	{
		private const string ApiBaseUrl = "https://api.payulatam.com/payments-api/4.0/service.cgi";
		private readonly HttpClient _client;

		public GetToken()
		{
			_client = new HttpClient();
			_client.BaseAddress = new Uri(ApiBaseUrl);
		}

		public async Task<string> GetAccessTokenAsync(string apiKey, string apiLogin)
		{
			try
			{
				var requestData = new
				{
					apiKey = apiKey,
					login = apiLogin
				};

				var requestJson = JsonConvert.SerializeObject(requestData);
				var requestContent = new StringContent(requestJson, Encoding.UTF8, "application/json");

				var response = await _client.PostAsync("security/api/v1/token", requestContent);
				response.EnsureSuccessStatusCode();

				var responseJson = await response.Content.ReadAsStringAsync();


				dynamic responseData = JsonConvert.DeserializeObject(responseJson);
				string accessToken = responseData.access_token;

				return accessToken;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error al obtener el token de acceso: {ex.Message}");
				return null;
			}
		}
	}
}
