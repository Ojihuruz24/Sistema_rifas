using SIMRIFA.Model.Models;

namespace SIMRIFA.Service.Correo
{
	public interface ICorreoServicio
	{
		Task EnvioCorreoSendGrid(List<string> destinatarios, string asunto, string contenido);

		Task<bool> EnvioCorreoMailNet(Comprador comprador);
	}
}