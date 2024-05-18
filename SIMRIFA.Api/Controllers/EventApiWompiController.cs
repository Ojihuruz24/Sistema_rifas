using Microsoft.AspNetCore.Mvc;
using SIMRIFA.Service.Wompi;

namespace SIMRIFA.Api.Controllers
{
	public class EventApiWompiController : Controller
	{
		public EventApiWompiController()
		{
			
		}


		public IActionResult Index(string dynamic)
		{
			//actualizar el contador de la barra progresiva (toca guardar la compra en la BD y que la vista jale la info de la BD)

			//guardar informacion del comprador 


			//envio del correo




			return Ok(dynamic);
		}
	}
}
