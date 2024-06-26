using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIMRIFA.Logic.TipoIdentificacion;
using SIMRIFA.Model.core;

namespace SIMRIFA.Api.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class TipoIdentificacionController : ControllerBase
	{
		private readonly ITipoIdentificacionService _service;

		public TipoIdentificacionController(ITipoIdentificacionService service)
		{
			_service = service;
		}

		[HttpGet]
		[Authorize]
		public async Task<ActionResult> Get()
		{
			try
			{
				IEnumerable<TipoIdentificacion> slit = await _service.ObtenerAsync(x => x.ESTADO == true);

				return Ok(slit);
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}


		[HttpPost]
		[Authorize]
		public async Task<ActionResult> Agregar([FromBody] TipoIdentificacion tipoIdentificacion)
		{
			try
			{
				return Ok(await _service.AgregarAsync(tipoIdentificacion));
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}


		[HttpPut]
		[Authorize]
		public async Task<ActionResult> Actualizar([FromBody] TipoIdentificacion tipoIdentificacion)
		{
			try
			{
				return Ok(await _service.ActualizarAsync(tipoIdentificacion));
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}

		[HttpDelete]
		[Authorize]
		public async Task<ActionResult> Eliminar([FromBody] TipoIdentificacion tipoIdentificacion)
		{
			try
			{
				return Ok(await _service.EliminarAsync(tipoIdentificacion));
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}

		}

	}
}
