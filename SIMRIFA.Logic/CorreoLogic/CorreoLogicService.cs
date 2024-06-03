using SIMRIFA.Model.Models;
using SIMRIFA.Service.Correo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Logic.CorreoLogic
{
	public class CorreoLogicService : ICorreoLogicService
	{
		private readonly ICorreoServicio _correoServicio;

		public CorreoLogicService(ICorreoServicio correoServicio)
		{
			_correoServicio = correoServicio;
		}

		public async Task EnvioCorreoSendGrid(List<string> destinatarios, string asunto, string contenido)
		{
			await _correoServicio.EnvioCorreoSendGrid(destinatarios, asunto, contenido);
		}

		public async Task<bool> EnvioCorreoMailNet(CompradorDto comprador)
		{
			return await _correoServicio.EnvioCorreoMailNet(comprador);
		}


	}
}
