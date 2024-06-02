using SIMRIFA.Model.Models;

namespace SIMRIFA.Logic.Correo
{
	public interface ICorreoLogicService
	{
		Task EnvioCorreoMailNet(CompradorDto comprador);
		Task EnvioCorreoSendGrid(List<string> destinatarios, string asunto, string contenido);
	}
}