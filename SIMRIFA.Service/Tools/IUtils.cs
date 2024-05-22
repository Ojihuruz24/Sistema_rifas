using SIMRIFA.Model.Models.Wompi;

namespace SIMRIFA.Service.Tools
{
	public interface IUtils
	{
		decimal Calcular(decimal valor, decimal cantidad);

		List<int> GenerarNumeroAletorio(int valor);

		decimal MargenError(decimal valor);

		(string referencia, string has) has(int amount,int cantidad, string fechaExpiracion);
		Task<bool> ValidacionInfo(EventoWompiResponse response);
	}
}