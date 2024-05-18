using EllipticCurve.Utils;
using Newtonsoft.Json;
using SIMRIFA.Model.Models.PayU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Service.Payu
{
    public class CreateOrdenPayU
	{
		private const string ApiBaseUrl = "https://api.payulatam.com/payments-api/4.0/service.cgi";
		private readonly HttpClient _client;

		public CreateOrdenPayU()
		{
			_client = new HttpClient();
			_client.BaseAddress = new Uri(ApiBaseUrl);
		}

		public async Task<string> CreateOrderAsync(string accessToken, OrderPayUDto order)
		{
			try
			{
				// Construir la solicitud de creación de la orden
				var requestData = new
				{
					merchant = new
					{
						apiKey = order.ApiKey,
						apiLogin = order.ApiLogin
					},
					transaction = new
					{
						order = new
						{
							accountId = order.AccountId,
							referenceCode = order.ReferenceCode,
							description = order.Description,
							language = order.Language,
							notifyUrl = order.NotifyUrl
						},
						paymentMethod = order.PaymentMethod,
						paymentCountry = order.PaymentCountry,
						payer = new
						{
							fullName = order.PayerFullName,
							emailAddress = order.PayerEmailAddress,
							contactPhone = order.PayerContactPhone
						},
						extraParameters = new
						{
							BANK_CODE = order.BankCode // Solo se proporciona en caso de PSE
						},
						creditCard = new // Solo se proporciona en caso de tarjeta de crédito
						{
							number = order.CreditCardNumber,
							securityCode = order.CreditCardSecurityCode,
							expirationDate = order.CreditCardExpirationDate,
							name = order.CreditCardHolderName
						},
						additionalValues = new
						{
							TX_VALUE = new
							{
								value = order.Amount,
								currency = order.Currency
							}
						}
					}
				};

				// Convertir el objeto de solicitud a JSON
				var requestJson = JsonConvert.SerializeObject(requestData);
				var requestContent = new StringContent(requestJson, Encoding.UTF8, "application/json");

				// Agregar el token de acceso en las cabeceras de la solicitud
				_client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

				// Enviar la solicitud HTTP POST para crear la orden de compra
				var response = await _client.PostAsync(ApiBaseUrl, requestContent);
				response.EnsureSuccessStatusCode();

				// Leer y retornar la respuesta
				var responseContent = await response.Content.ReadAsStringAsync();
				return responseContent;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error al crear la orden de compra: {ex.Message}");
				return null;
			}
		}

	}
}
