﻿using SIMRIFA.Model.Models;
using SIMRIFA.Service.Correo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Logic.Correo
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

		public async Task EnvioCorreoMailNet(CompradorDto comprador)
		{
			await _correoServicio.EnvioCorreoMailNet(comprador);
		}


	}
}
