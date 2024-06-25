using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SIMRIFA.Service.Transaccion;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SIMRIFA.Logic.Tools
{
	public class UtilisLogic : IUtilisLogic
	{
		private ITransaccionService _transaccionService;
		private readonly IConfiguration _configuration;

		public UtilisLogic(ITransaccionService transaccionService, IConfiguration configuration)
		{
			_transaccionService = transaccionService;
			_configuration = configuration;
		}

		public decimal Calcular(decimal valor, decimal cantidad)
		{
			//var porcentajeAdiccional = MargenError(valor);

			decimal info = Decimal.Truncate((Convert.ToDecimal(valor) / Convert.ToDecimal(cantidad)) * Convert.ToDecimal(100));

			return info;
		}

		public async Task<(string referencia, string has)> has(int amount, int cantidad, string fechaExpiracion)
		{
			Random random = new Random();

			var valorrandom = await GenerarReferenciaUnica(cantidad);

			var publicKey = "test_integrity_YcpIHCiQvGKc01VK9kpTNo3wvb4vFV8g";
			var currency = "COP";
			var reference = $"{valorrandom}";

			var conca = $"{reference}{amount}{currency}{fechaExpiracion}{publicKey}";
			//var conca = $"{reference}{amount}{currency}{publicKey}";

			using (SHA256 sha256Hash = SHA256.Create())
			{
				byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(conca));

				StringBuilder builder = new StringBuilder();
				for (int i = 0; i < bytes.Length; i++)
				{
					builder.Append(bytes[i].ToString("x2"));
				}
				return (reference.ToString(), builder.ToString());
			}
		}



		public async Task<T> GetValueFromJsonElement<T>(JsonElement jsonElement, params string[] keys)
		{
			JsonElement currentElement = jsonElement;

			foreach (var key in keys)
			{
				if (!currentElement.TryGetProperty(key, out currentElement))
				{
					return default(T);
				}
			}

			// Convertir el valor a tipo T
			T value;
			if (typeof(T) == typeof(string))
			{
				value = (T)(object)currentElement.GetString();
			}
			else if (typeof(T) == typeof(int))
			{
				value = (T)(object)currentElement.GetInt32();
			}
			else if (typeof(T) == typeof(bool))
			{
				value = (T)(object)currentElement.GetBoolean();
			}
			else
			{
				return default(T);
			}


			await Task.CompletedTask;

			return value;
		}

		public async Task<bool> ValidacionInfo(string TransaccionID, string TransaccionStatus, string TransaccionAmount, string TransaccionChecksum, string timestamp = default)
		{
			//var TransaccionID = response?.Data?.Transaction?.Id;
			//var TransaccionStatus = response?.Data?.Transaction?.Status;
			//var TransaccionAmount = response?.Data?.Transaction?.AmountInCents.ToString();
			//var timestamp = response?.Timestamp.ToString();

			var clave = "test_events_QeLdcWGI7qBSTtMoSSLt5hB8PmEERLTY";

			string concat = $"{TransaccionID}{TransaccionStatus}{TransaccionAmount}{clave}";

			if (!string.IsNullOrEmpty(timestamp))
			{
				concat = $"{TransaccionID}{TransaccionStatus}{TransaccionAmount}{timestamp}{clave}";
			}

			StringBuilder builder = new StringBuilder();

			using (SHA256 sha256Hash = SHA256.Create())
			{
				byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(concat));

				for (int i = 0; i < bytes.Length; i++)
				{
					builder.Append(bytes[i].ToString("x2"));
				}
			}

			var encryp = builder.ToString();

			if (encryp == TransaccionChecksum)
			{
				return true;
			}

			return false;
		}

		private async Task<string> GenerarReferenciaUnica(int cantidad)
		{
			var referenciaUnique = await _transaccionService.ObtenerReferencias();
			var valorrandom = string.Empty;
			do
			{
				Random random = new Random();
				valorrandom = $"{random.Next(100000000)}_{cantidad}";
			} while (referenciaUnique.Contains(valorrandom));

			return valorrandom;

		}


		public int AddTrailingZeros(int number, int minZeros)
		{
			var result = Convert.ToInt32(number.ToString().PadRight(number.ToString().Length + minZeros, '0'));

			return result;

			//return number.ToString().PadRight(number.ToString().Length + minZeros, '0');
		}


		public async Task<string> GenerateJwtToken(string username)
		{
			var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]); // Asegúrate de definir la clave en appsettings.json
			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, username)
				}),
				Expires = DateTime.UtcNow.AddHours(int.Parse(_configuration["Jwt:ExpireHours"])),
				Issuer = _configuration["Jwt:Issuer"],
				Audience = _configuration["Jwt:Audience"],
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token); 
		}
	}
}
