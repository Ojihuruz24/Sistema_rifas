using SIMRIFA.Model.Models.Wompi;

namespace SIMRIFA.Service.Tools
{
	public interface IUtils
	{
		decimal Calcular(decimal valor, decimal cantidad);

		Task<List<string>> GenerarNumeroAletorio(int valor);

		decimal MargenError(decimal valor);

		Task<(string referencia, string has)> has(int amount, int cantidad, string fechaExpiracion);
		Task<bool> ValidacionInfo(string TransaccionID, string TransaccionStatus, string TransaccionAmount, string TransaccionChecksum, string timestamp = default);
	}
}