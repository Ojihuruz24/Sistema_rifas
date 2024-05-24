using SIMRIFA.Model.Config;
using SIMRIFA.Model.Models.Wompi;
using System.Text.Json;

namespace SIMRIFA.Service.Tools
{
	public interface IUtils
	{
		decimal Calcular(decimal valor, decimal cantidad);
		Task<(string referencia, string has)> has(int amount, int cantidad, string fechaExpiracion);
		Task<bool> ValidacionInfo(string TransaccionID, string TransaccionStatus, string TransaccionAmount, string TransaccionChecksum, string timestamp = default);

		Task<T> GetValueFromJsonElement<T>(JsonElement jsonElement, params string[] keys);

		Task<CorreoConfig> GetConfiguracionCorreoAsync();
	}
}