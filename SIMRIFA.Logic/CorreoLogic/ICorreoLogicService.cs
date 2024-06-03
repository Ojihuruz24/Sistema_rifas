﻿using SIMRIFA.Model.Models;

namespace SIMRIFA.Logic.CorreoLogic
{
	public interface ICorreoLogicService
	{
		Task<bool> EnvioCorreoMailNet(CompradorDto comprador);
		Task EnvioCorreoSendGrid(List<string> destinatarios, string asunto, string contenido);
	}
}