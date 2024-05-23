using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMRIFA.Model.core;
using SIMRIFA.Model.Models;
using SIMRIFA.Model.Models.Wompi;
using Newtonsoft.Json.Linq;
using SIMRIFA.Model.ventas;
using SIMRIFA.Service.Cliente;
using SIMRIFA.Service.Compra;
using SIMRIFA.Service.Transaccion;
using SIMRIFA.Service.Wompi;
using SIMRIFA.DataAccess.UnitOfWork;
using SIMRIFA.Service.TransaccionesWompi;
using System;
using System.Text.Json;
using SIMRIFA.Service.Tools;
using SIMRIFA.Service.Series;
using SIMRIFA.Service.NumeroAleatorio;
using System.Text;
//using Newtonsoft.Json;
using System.Text.Json;
using SendGrid;
using System.Threading;

namespace SIMRIFA.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EventApiWompiController : ControllerBase
	{
		private readonly IClienteService _clienteService;
		private readonly ITransaccionService _transaccionService;
		private readonly IUnitOfWork _IUnitOfWork;
		private readonly ITransaccionesService _transaccionesWompiService;
		private readonly IUtils _utils;
		private readonly ISerieService _serieService;
		private readonly INumeroAleatorioService _numeroAleatorioService;
		private readonly ICorreo _correo;

		private readonly SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

		public EventApiWompiController(IUnitOfWork iUnitOfWork, IClienteService clienteService, ITransaccionService transaccionService
			, ITransaccionesService transaccionesWompiService, IUtils utils, ISerieService serieService,
			INumeroAleatorioService numeroAleatorioService, ICorreo correo)
		{
			_clienteService = clienteService;
			_transaccionService = transaccionService;
			_IUnitOfWork = iUnitOfWork;
			_transaccionesWompiService = transaccionesWompiService;
			_utils = utils;
			_serieService = serieService;
			_numeroAleatorioService = numeroAleatorioService;
			_correo = correo;
		}


		[HttpGet]
		public IActionResult Get()
		{
			return Ok("Hello, world!");
		}

		#region ORIGINAL

		[HttpPost("response")]
		public async Task<IActionResult> Post([FromBody] EventoWompiResponse response)
		{
			bool dataCorrecta = true; // await _utils.ValidacionInfo(response); se le cambiaron los parametros de entrada

			if (dataCorrecta)
			{
				switch (response.Event)
				{
					case "transaction.updated":

						Cliente clienteResult = new Cliente();
						TransaccionDto transaccion;
						InfoTransaccionDto infoTransaccionDto;

						var toJson = JsonSerializer.Serialize(response);

						try
						{
							var transation = response.Data.Transaction;

							var cliente = new Cliente
							{
								Nombre = response.Data.Transaction.CustomerData.FullName,
								Correo = response.Data.Transaction.CustomerEmail,
								Direccion = "Sin direccion",
								FechaCreacion = DateTime.Now,
								Telefono = transation.CustomerData.PhoneNumber,
							};

							clienteResult = await _clienteService.AgregarAsync(cliente);

							await _IUnitOfWork.Commit();

							transaccion = new TransaccionDto
							{
								IdCliente = clienteResult.IdCliente,
								Transaccion = transation.Id,
								Email = transation.CustomerEmail,
								TipoPago = transation.PaymentMethodType,
								Estado = transation.Status,
								FechaInicioPago = transation.CreatedAt,
								FechaFinalPago = transation.FinalizedAt,
								FechaTransaccion = response.SentAt,
								MetodoPago = transation.PaymentMethodType,
								Moneda = transation.Currency,
								Monto = transation.AmountInCents.ToString(),
								Referencia = transation.Reference,
							};

							var transaccionResult = await _transaccionService.AgregarTransaccion(transaccion);

							await _IUnitOfWork.Commit();

							infoTransaccionDto = new InfoTransaccionDto
							{
								AliasEvent = response.Event,
								TransactionId = transation?.Id,
								CreatedAt = transation?.CreatedAt,
								FinalizedAt = transation?.FinalizedAt,
								AmountInCents = transation?.AmountInCents?.ToString(),
								Reference = transation?.Reference,
								CustomerEmail = transation?.CustomerEmail,
								Currency = transation?.Currency,
								PaymentMethodType = transation?.PaymentMethodType,
								AliasType = transation?.PaymentMethod?.Type,
								AliasStatus = transation?.Status,
								StatusMessage = transation?.StatusMessage,
								FullName = transation?.CustomerData?.FullName,
								PhoneNumber = transation?.CustomerData?.PhoneNumber,
								SentAt = response?.SentAt,
								AliasTimestamp = response?.Timestamp.ToString(),
								AliasChecksum = response?.Signature?.Checksum,
								Environment = response?.Environment,
								idCliente = cliente.IdCliente,
								ResponseJson = toJson
							};

							var transationDto = await _transaccionesWompiService.AgregarAsync(infoTransaccionDto);

							await _IUnitOfWork.Commit();


							if (cliente != null && transaccion != null && infoTransaccionDto != null)
							{
								// se le entregan los numeros
								var serie = (await _serieService.ObtenerSerieActiva()).FirstOrDefault();

								int.TryParse(response.Data.Transaction.Reference.Substring(response.Data.Transaction.Reference.Length - 1), out int cantidad);

								var cantNumeros = await _numeroAleatorioService.GenerarNumeroAletorio(cantidad);

								var strinbulder = new StringBuilder();

								foreach (var item in cantNumeros)
								{
									strinbulder.Append($" {item} ");

									var result = await _numeroAleatorioService.AgregarAsync(new NumeroAleatorio
									{
										IdCliente = cliente.IdCliente,
										IdSerie = serie.IdSerie,
										Numero = item,
										Vendido = true,
										FechaCreacion = DateTime.Now
									});
								}


								serie.Contador = cantNumeros.Count;

								await _serieService.ActualizarSerie(serie);

								await _IUnitOfWork.Commit();

								var comprador = new CompradorDto()
								{
									Nombre = cliente.Nombre,
									Correo = cliente.Correo,
									Referencia = transaccionResult.Referencia,
									Valor = transaccion.Monto,
									Cantidad = cantNumeros.Count().ToString(),
									Numeros = strinbulder.ToString(),
									FechaCreacion = DateTime.Now
								};

								await _correo.EnvioCorreoNetMail(comprador);

							}


						}
						catch (Exception ex)
						{
							//await _clienteService.EliminarAsync(clienteResult);

							//await _IUnitOfWork.Commit();

							await _IUnitOfWork.Rollback();

							throw;
						}

						break;

					case "nequi_token.updated":

						break;

					default:
						break;
				}
			}

			return Ok();
		}
		#endregion


		#region Prueba de que reicba un dato dinamico

		[HttpPost()]
		public async Task<ActionResult> PostTest([FromBody] JsonElement eventData)
		{

			// Ejemplo de cómo usar un semáforo para controlar la concurrencia
			await semaphoreSlim.WaitAsync();

			if (eventData.ValueKind == JsonValueKind.Undefined)
			{
				return BadRequest();
			}

			var TransaccionId = await GetValueFromJsonElement<string>(eventData, "data", "transaction", "id");
			var TransaccionStatus = await GetValueFromJsonElement<string>(eventData, "data", "transaction", "status");
			var TransaccionAmount = await GetValueFromJsonElement<int>(eventData, "data", "transaction", "amount_in_cents");
			var Transacciontimestamp = await GetValueFromJsonElement<int>(eventData, "timestamp");
			var TransaccionChecksum = await GetValueFromJsonElement<string>(eventData, "signature", "checksum");
			string eventType = await GetValueFromJsonElement<string>(eventData, "event");

			bool dataCorrecta = await _utils.ValidacionInfo(TransaccionId, TransaccionStatus, TransaccionAmount.ToString(), TransaccionChecksum, Transacciontimestamp.ToString());

			if (dataCorrecta)
			{
				string jsonString = eventData.GetRawText();
				var TransaccionCreatedAt = await GetValueFromJsonElement<string>(eventData, "data", "transaction", "created_at");
				var TransaccionFechaFinalPago = await GetValueFromJsonElement<string>(eventData, "data", "transaction", "finalized_at");
				var TransaccionCustomerEmail = await GetValueFromJsonElement<string>(eventData, "data", "transaction", "customer_email");
				var TransaccionTipoPago = await GetValueFromJsonElement<string>(eventData, "data", "transaction", "payment_method", "type");
				var TransaccionReference = await GetValueFromJsonElement<string>(eventData, "data", "transaction", "reference");
				var TransaccionCurrency = await GetValueFromJsonElement<string>(eventData, "data", "transaction", "currency");
				var TransaccionPaymentMethodType = await GetValueFromJsonElement<string>(eventData, "data", "transaction", "payment_method_type");
				var TransaccionSentAt = await GetValueFromJsonElement<string>(eventData, "sent_at");

				switch (eventType)
				{
					case "transaction.updated":


						Cliente clienteResult = new Cliente();
						TransaccionDto transaccion;
						InfoTransaccionDto infoTransaccionDto;

						try
						{
							var cliente = new Cliente
							{
								Nombre = await GetValueFromJsonElement<string>(eventData, "data", "transaction", "customer_data", "full_name"),
								Correo = await GetValueFromJsonElement<string>(eventData, "data", "transaction", "customer_email"),
								Direccion = "Sin direccion",
								FechaCreacion = DateTime.Now,
								Telefono = await GetValueFromJsonElement<string>(eventData, "data", "transaction", "customer_data", "phone_number"),
							};

							clienteResult = await _clienteService.AgregarAsync(cliente);

							await _IUnitOfWork.Commit();

							transaccion = new TransaccionDto
							{
								IdCliente = clienteResult.IdCliente,
								Transaccion = TransaccionId,
								Email = TransaccionCustomerEmail,
								TipoPago = TransaccionTipoPago,// transation.PaymentMethodType,
								Estado = TransaccionStatus,//transation.Status,
								FechaInicioPago = TransaccionCreatedAt, //transation.CreatedAt,
								FechaFinalPago = TransaccionFechaFinalPago, //transation.FinalizedAt,
								FechaTransaccion = TransaccionSentAt, //response.SentAt,
								MetodoPago = TransaccionPaymentMethodType,//transation.PaymentMethodType,
								Moneda = TransaccionCurrency,//transation.Currency,
								Monto = TransaccionAmount.ToString(),//transation.AmountInCents.ToString(),
								Referencia = TransaccionReference//transation.Reference,
							};

							var transaccionResult = await _transaccionService.AgregarTransaccion(transaccion);

							await _IUnitOfWork.Commit();

							infoTransaccionDto = new InfoTransaccionDto
							{
								idCliente = clienteResult.IdCliente,
								AliasEvent = eventType,
								TransactionId = TransaccionId,
								CreatedAt = TransaccionCreatedAt,
								FinalizedAt = TransaccionFechaFinalPago,
								AmountInCents = TransaccionAmount.ToString(),
								Reference = TransaccionReference,
								CustomerEmail = TransaccionCustomerEmail,
								Currency = TransaccionCurrency,
								PaymentMethodType = TransaccionPaymentMethodType,
								AliasType = TransaccionTipoPago,
								AliasStatus = TransaccionStatus,
								StatusMessage = await GetValueFromJsonElement<string>(eventData, "data", "transaction", "status_message"),
								FullName = await GetValueFromJsonElement<string>(eventData, "data", "transaction", "customer_data", "full_name"),
								PhoneNumber = await GetValueFromJsonElement<string>(eventData, "data", "transaction", "customer_data", "phone_number"),
								SentAt = TransaccionSentAt,
								AliasTimestamp = Transacciontimestamp.ToString(),
								AliasChecksum = TransaccionChecksum,
								Environment = await GetValueFromJsonElement<string>(eventData, "environment"),
								ResponseJson = jsonString
							};

							var transationDto = await _transaccionesWompiService.AgregarAsync(infoTransaccionDto);

							await _IUnitOfWork.Commit();

							if (cliente != null && transaccion != null && transationDto != null)
							{
								// se le entregan los numeros
								var serie = (await _serieService.ObtenerSerieActiva()).FirstOrDefault();

								int.TryParse(TransaccionReference.Substring(TransaccionReference.Length - 1), out int cantidad);

								var cantNumeros = await _numeroAleatorioService.GenerarNumeroAletorio(cantidad);

								var strinbulder = new StringBuilder();

								foreach (var item in cantNumeros)
								{
									strinbulder.Append($" {item} ");

									var result = await _numeroAleatorioService.AgregarAsync(new NumeroAleatorio
									{
										IdCliente = cliente.IdCliente,
										IdSerie = serie.IdSerie,
										Numero = item,
										Vendido = true,
										FechaCreacion = DateTime.Now
									});
								}

								var contador = await _numeroAleatorioService.ObtenerNumerosAsync();

								serie.Contador = contador.Count;

								await _serieService.ActualizarSerie(serie);

								await _IUnitOfWork.Commit();

								var comprador = new CompradorDto()
								{
									Nombre = cliente.Nombre,
									Correo = cliente.Correo,
									Referencia = transaccionResult.Referencia,
									Valor = transaccion.Monto,
									Cantidad = cantNumeros.Count().ToString(),
									Numeros = strinbulder.ToString(),
									FechaCreacion = DateTime.Now
								};

								await _correo.EnvioCorreoNetMail(comprador);
							}

						}
						catch (Exception ex)
						{

							throw;
						}

						break;
					default:
						break;
				}
			}

			semaphoreSlim.Release();

			return Ok("OK");
		}
		#endregion


		private async Task<T> GetValueFromJsonElement<T>(JsonElement jsonElement, params string[] keys)
		{
			JsonElement currentElement = jsonElement;

			foreach (var key in keys)
			{
				if (!currentElement.TryGetProperty(key, out currentElement))
				{
					throw new ArgumentException($"Key '{key}' not found in JSON.");
				}
			}

			// Convertir el valor a tipo T
			T value;
			if (typeof(T) == typeof(string))
			{
				value = (T)(object)currentElement.GetString();
			}
			else if (typeof(T) == typeof(int))
			{
				value = (T)(object)currentElement.GetInt32();
			}
			else if (typeof(T) == typeof(bool))
			{
				value = (T)(object)currentElement.GetBoolean();
			}
			else
			{
				throw new ArgumentException($"Unsupported type '{typeof(T)}'.");
			}


			await Task.CompletedTask;

			return value;
		}




	}
}
