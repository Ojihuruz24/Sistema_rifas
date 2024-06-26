using System.Text.Json;

namespace SIMRIFA.Logic.Tools
{
	public interface IUtilisLogic
	{
		decimal Calcular(decimal valor, decimal cantidad);
		Task<T> GetValueFromJsonElement<T>(JsonElement jsonElement, params string[] keys);
		Task<(string referencia, string has)> has(int amount, int cantidad, string fechaExpiracion);
		Task<bool> ValidacionInfo(string TransaccionID, string TransaccionStatus, string TransaccionAmount, string TransaccionChecksum, string timestamp = null);

		Task<string> GenerateJwtToken();
	}
}