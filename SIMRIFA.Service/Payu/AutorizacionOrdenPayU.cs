using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Service.Payu
{
	public class AutorizacionOrdenPayU
	{
		private const string ApiBaseUrl = "https://api.payulatam.com/payments-api/4.0/service.cgi";
		private readonly HttpClient _client;

		public AutorizacionOrdenPayU()
		{
			_client = new HttpClient();
			_client.BaseAddress = new Uri(ApiBaseUrl);
		}

		public async Task<string> AuthorizeOrderAsync(string accessToken, string orderId, string APIKey, string TuAPILogin)
		{
			try
			{
				// Construir la solicitud de autorización de la orden
				var requestData = new
				{
					command = "SUBMIT_TRANSACTION",
					merchant = new
					{
						apiKey = APIKey,
						apiLogin = TuAPILogin
					},
					transaction = new
					{
						order = new
						{
							id = orderId
						}
					}
				};

				// Convertir el objeto de solicitud a JSON
				var requestJson = JsonConvert.SerializeObject(requestData);
				var requestContent = new StringContent(requestJson, Encoding.UTF8, "application/json");

				// Agregar el token de acceso en las cabeceras de la solicitud
				_client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

				// Enviar la solicitud HTTP POST para autorizar la orden
				var response = await _client.PostAsync(ApiBaseUrl, requestContent);
				response.EnsureSuccessStatusCode();

				// Leer y retornar la respuesta
				var responseContent = await response.Content.ReadAsStringAsync();


				return responseContent;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error al autorizar la orden de compra: {ex.Message}");
				return null;
			}
		}

	}
}
