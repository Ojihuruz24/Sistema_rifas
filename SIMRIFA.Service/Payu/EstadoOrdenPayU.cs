using Newtonsoft.Json;
using SIMRIFA.Model.Models.PayU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Service.Payu
{
    public class EstadoOrdenPayU
	{
		private const string ApiBaseUrl = "https://api.payulatam.com/payments-api/4.0/service.cgi";
		private readonly HttpClient _client;

		public EstadoOrdenPayU()
		{
			_client = new HttpClient();
			_client.BaseAddress = new Uri(ApiBaseUrl);
		}

		public async Task<string> GetOrderStatusAsync(EstadoOrdenPayUDto orderStatusRequest, string apiKey, string apiLogin)
		{
			try
			{
				// Construir la solicitud de consulta de estado de la orden
				var requestData = new
				{
					command = "ORDER_DETAIL",
					merchant = new
					{
						apiKey = apiKey,
						apiLogin = apiLogin
					},
					details = new
					{
						orderId = orderStatusRequest.OrderId
					}
				};

				// Convertir el objeto de solicitud a JSON
				var requestJson = JsonConvert.SerializeObject(requestData);
				var requestContent = new StringContent(requestJson, Encoding.UTF8, "application/json");

				// Agregar el token de acceso en las cabeceras de la solicitud
				_client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", orderStatusRequest.AccessToken);

				// Enviar la solicitud HTTP POST para consultar el estado de la orden
				var response = await _client.PostAsync(ApiBaseUrl, requestContent);
				response.EnsureSuccessStatusCode();

				// Leer y retornar la respuesta
				var responseContent = await response.Content.ReadAsStringAsync();
				return responseContent;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error al consultar el estado de la orden: {ex.Message}");
				return null;
			}
		}
	}
}
