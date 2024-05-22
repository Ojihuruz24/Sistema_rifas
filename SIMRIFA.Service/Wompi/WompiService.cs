using Newtonsoft.Json;
using SIMRIFA.Model.core;
using SIMRIFA.Model.Models.Wompi;
using SIMRIFA.Model.ventas;
using SIMRIFA.Service.Tools;
using System.Net.Http.Headers;
using System.Text;

namespace SIMRIFA.Service.Wompi
{
	public class WompiService : IWompiService
	{
		private readonly HttpClient _httpClient;
		private readonly IUtils _tools;
		private readonly string _publicKey = "pub_test_EKiDBdydoVychiwazPqDsm52168IjGmQ"; // Obtén esto desde Wompi
		private readonly string _privateKey = "prv_test_sHWSp1TiMQffQ60Z7vzzFZYnI8rvXv7e"; // Obtén esto desde Wompi
		private readonly string _baseUrl = "https://sandbox.wompi.co/v1"; // Usa el sandbox para pruebas
		private readonly string _wompiUrl = "https://checkout.wompi.co/p";


		public WompiService(HttpClient httpClient, IUtils utils)
		{
			_httpClient = httpClient;
			_tools = utils;
		}

		public async Task<TransactionResponse> GetTransactionAsync(string transactionId)
		{
			var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/transactions/{transactionId}");
			request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _privateKey);

			var response = await _httpClient.SendAsync(request);
			response.EnsureSuccessStatusCode();

			var responseData = await response.Content.ReadAsStringAsync();

			TransactionResponse con = JsonConvert.DeserializeObject<TransactionResponse>(responseData);

			return con;
		}

		public async Task<PaymentLinkResponse> CreatePaymentLinkAsync(PaymentLinkRequest request)
		{
			var jsonRequest = JsonConvert.SerializeObject(request);

			var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _privateKey);

			var response = await _httpClient.PostAsync($"{_baseUrl}/payment_links", httpContent);
			response.EnsureSuccessStatusCode();

			var responseData = await response.Content.ReadAsStringAsync();

			var sdsd = JsonConvert.DeserializeObject<PaymentLinkResponse>(responseData);

			return sdsd;
		}

		public async Task<EventoWompiResponse> GuardarEvento(EventoWompiResponse eventoWompiResponse)
		{
			try
			{
				//var result = 
			}
			catch (Exception ex)
			{
				await Console.Out.WriteLineAsync("ERROR");
			}

			return eventoWompiResponse;
		}

	}
}
