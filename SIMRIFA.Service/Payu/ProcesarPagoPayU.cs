using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SIMRIFA.Model.Models.PayU;

namespace SIMRIFA.Service.Payu
{
    public class ProcesarPagoPayU
	{
		private const string ApiBaseUrl = "https://api.payulatam.com/payments-api/4.0/service.cgi";
		private readonly HttpClient _client;

		public ProcesarPagoPayU()
		{
			_client = new HttpClient();
			_client.BaseAddress = new Uri(ApiBaseUrl);
		}

		public async Task<string> ProcessPaymentAsync(ProcesarPagoPayUDto paymentRequest)
		{
			try
			{
				// Construir la solicitud de procesamiento de pago
				var requestData = new
				{
					command = "SUBMIT_TRANSACTION",
					merchant = new
					{
						apiKey = "TuAPIKey",
						apiLogin = "TuAPILogin"
					},
					transaction = new
					{
						order = new
						{
							id = paymentRequest.OrderId
						},
						type = "AUTHORIZATION_AND_CAPTURE",
						paymentMethod = "VISA", // Método de pago (por ejemplo, VISA, MASTERCARD)
						paymentCountry = "CO", // País de la transacción (por ejemplo, CO para Colombia)
						deviceSessionId = "v2asdrw3ffwef51e836v3f6",
						ipAddress = "127.0.0.1",
						cookie = "pt1t38347bs6jc9ruv2ecpv7o2",
						userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.85 Safari/537.36",
						creditCard = new
						{
							number = paymentRequest.CardNumber,
							securityCode = paymentRequest.SecurityCode,
							expirationDate = paymentRequest.ExpirationDate,
							name = paymentRequest.CardHolderName
						},
						additionalValues = new
						{
							TX_VALUE = new
							{
								value = paymentRequest.Amount,
								currency = "USD" // Moneda (ejemplo)
							}
						}
					}
				};

				// Convertir el objeto de solicitud a JSON
				var requestJson = JsonConvert.SerializeObject(requestData);
				var requestContent = new StringContent(requestJson, Encoding.UTF8, "application/json");

				// Agregar el token de acceso en las cabeceras de la solicitud
				_client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", paymentRequest.AccessToken);

				// Enviar la solicitud HTTP POST para procesar el pago
				var response = await _client.PostAsync(ApiBaseUrl, requestContent);
				response.EnsureSuccessStatusCode();

				// Leer y retornar la respuesta
				var responseContent = await response.Content.ReadAsStringAsync();
				return responseContent;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error al procesar el pago: {ex.Message}");
				return null;
			}
		}

	}
}
