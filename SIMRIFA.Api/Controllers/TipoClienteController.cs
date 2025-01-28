using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMRIFA.Logic.Municipio;
using SIMRIFA.Logic.TipoCliente;
using SIMRIFA.Model.core;

namespace SIMRIFA.Api.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class TipoClienteController : Controller
	{
		private readonly ITipoClienteService _service;

		public TipoClienteController(ITipoClienteService service)
		{
			_service = service;
		}


		[HttpGet]
		[Authorize]
		public async Task<ActionResult> Get()
		{
			try
			{
				IEnumerable<TipoCliente> slit = await _service.ObtenerAsync(x => x.ESTADO == true);
				return Ok(slit);
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{criterio}")]
		[Authorize]
		public async Task<ActionResult<IEnumerable<TipoCliente>>> Get(string criterio)
		{
			return Ok((await _service.ObtenerAsync(x => (x.NOMBRE).Contains(criterio))));
		}


		[HttpPost]
		[Authorize]
		public async Task<ActionResult> Agregar([FromBody] TipoCliente tipoCliente)
		{
			try
			{
				return Ok(await _service.AgregarAsync(tipoCliente));
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}


		[HttpPut]
		[Authorize]
		public async Task<ActionResult> Actualizar([FromBody] TipoCliente tipoCliente)
		{
			try
			{
				return Ok(await _service.ActualizarAsync(tipoCliente));
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}

		[HttpDelete]
		[Authorize]
		public async Task<ActionResult> Eliminar([FromBody] TipoCliente tipoCliente)
		{
			try
			{
				return Ok(await _service.EliminarAsync(tipoCliente));
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}

		}
	}
}
