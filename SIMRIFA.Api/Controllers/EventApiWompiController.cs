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

		public EventApiWompiController(IUnitOfWork iUnitOfWork, IClienteService clienteService, ITransaccionService transaccionService
			, ITransaccionesService transaccionesWompiService, IUtils utils, ISerieService serieService)
		{
			_clienteService = clienteService;
			_transaccionService = transaccionService;
			_IUnitOfWork = iUnitOfWork;
			_transaccionesWompiService = transaccionesWompiService;
			_utils = utils;
			_serieService = serieService;
		}


		[HttpGet]
		public IActionResult Get()
		{
			return Ok("Hello, world!");
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] EventoWompiResponse response)
		{
			bool dataCorrecta = await _utils.ValidacionInfo(response);

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
								SandboxStatus = transation?.PaymentMethod?.SandboxStatus,
								PaymentDescription = transation?.PaymentMethod?.PaymentDescription,
								ExternalIdentifier = transation?.PaymentMethod?.Extra?.ExternalIdentifier,
								BusinessAgreementCode = transation?.PaymentMethod?.Extra?.BusinessAgreementCode,
								PaymentIntentionIdentifier = transation?.PaymentMethod?.Extra?.PaymentIntentionIdentifier,
								AliasStatus = transation?.Status,
								StatusMessage = transation?.StatusMessage,
								RedirectUrl = transation?.RedirectUrl,
								PaymentSourceId = transation?.PaymentSourceId,
								PaymentLinkId = transation?.PaymentLinkId,
								FullName = transation?.CustomerData?.FullName,
								PhoneNumber = transation?.CustomerData?.PhoneNumber,
								Billing_data_AddressLine1 = transation?.BillingData?.AddressLine1,
								Billing_data_AddressLine2 = transation?.BillingData?.AddressLine2,
								Billing_data_State = transation?.BillingData?.State,
								Billing_data_City = transation?.BillingData?.City,
								Billing_data_Country = transation?.BillingData?.Country,
								Billing_data_PostalCode = transation?.BillingData?.PostalCode,
								SentAt = response?.SentAt,
								AliasTimestamp = response?.Timestamp,
								AliasChecksum = response?.Signature?.Checksum,
								Property_transaction_id = response?.Signature?.Properties?.TransactionId,
								Property_transaction_status = response?.Signature?.Properties?.TransactionStatus,
								Property_amount_in_cents = response?.Signature?.Properties?.TransactionAmountInCents,
								Environment = response?.Environment,
								idCliente = cliente.IdCliente,
								ResponseJson = toJson
							};

							var transationDto = await _transaccionesWompiService.AgregarAsync(infoTransaccionDto);


							if (cliente != null && transaccion  != null && infoTransaccionDto != null)
							{
								// se le entregan los numeros
								var serie = await _serieService.ObtenerSerieActiva();

							}

							await _IUnitOfWork.Commit();

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

		[HttpPost("{key}")]
		public async Task<ActionResult> PostTest(string key)
		{

			return Ok("OK");
		}


	}
}
