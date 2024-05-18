using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMRIFA.Model.Models.Wompi;
using SIMRIFA.Service.Wompi;

namespace SIMRIFA.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EventApiWompiController : ControllerBase
	{
		public EventApiWompiController()
		{
		}


		[HttpGet]
		public IActionResult Get()
		{
			return Ok("Hello, world!");
		}

		[HttpPost]
		public IActionResult Post(EventoWompiResponse eventoWompiResponse)
		{



			// Lógica para manejar la solicitud POST
			return Ok();
		}

		[HttpPost("{key}")]
		public async Task<ActionResult> PostTest(string key)
		{
			// Lógica para manejar la solicitud POST
			return Ok("OK");
		}
	}
}
