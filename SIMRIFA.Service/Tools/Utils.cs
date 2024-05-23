using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using SIMRIFA.DataAccess.UnitOfWork;
using SIMRIFA.Model.Models.Wompi;
using SIMRIFA.Service.NumeroAleatorio;
using SIMRIFA.Service.Transaccion;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Service.Tools
{
	public class Utils : IUtils
	{
		private List<int> numeros;
		private IUnitOfWork _unitOfWork;
		private INumeroAleatorioService _numeroAleatorioService;
		private ITransaccionService _transaccionService;


		public Utils(IUnitOfWork unitOfWork, INumeroAleatorioService numeroAleatorioService, ITransaccionService transaccionService)
		{
			numeros = new List<int>();
			_unitOfWork = unitOfWork;
			_numeroAleatorioService = numeroAleatorioService;
			_transaccionService = transaccionService;
		}

		public decimal Calcular(decimal valor, decimal cantidad)
		{
			//var porcentajeAdiccional = MargenError(valor);

			decimal info = Decimal.Truncate((Convert.ToDecimal(valor) / Convert.ToDecimal(cantidad)) * Convert.ToDecimal(100));

			return info;
		}

		public async Task<List<string>> GenerarNumeroAletorio(int valor)
		{
			var litnumTemp = new List<string>();

			litnumTemp.AddRange(await NumerosExistentes());

			var nuevoNumeros = new List<string>();

			for (int i = 0; i < valor; i++)
			{
				string numero;
				do
				{
					numero = await GenerarNumeroAleatorio();

				} while (litnumTemp.Contains(numero));

				litnumTemp.Add(numero);
				nuevoNumeros.Add(numero);
			}

			return nuevoNumeros;
		}

		public decimal MargenError(decimal valor)
		{
			var margen = _unitOfWork._context.Serie.Where(x => x.Estado == true)?.FirstOrDefault()?.Margen;

			var porcentajeAdiccional = Decimal.Truncate(Convert.ToDecimal(valor) * Convert.ToDecimal(margen)) / 100;

			return porcentajeAdiccional;
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

		private async Task<string> GenerarNumeroAleatorio()
		{
			Random rnd = new Random();

			var temp = await Task.Run(() => rnd.Next(0, 999));

			var numeroFormateado = temp.ToString("000");

			return numeroFormateado;
		}

		private async Task<List<string>> NumerosExistentes()
		{
			return await _numeroAleatorioService.ObtenerNumerosAsync();
		}

		//public async Task<bool> ValidacionInfo(EventoWompiResponse response)
		public async Task<bool> ValidacionInfo(string TransaccionID, string TransaccionStatus, string TransaccionAmount,string TransaccionChecksum, string timestamp = default)
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

		#region Buscador de un json

		private Dictionary<string, object> ToDictionary(JObject jObject)
		{
			var result = new Dictionary<string, object>();
			foreach (var property in jObject.Properties())
			{
				var value = property.Value;
				if (value is JObject)
				{
					result[property.Name] = ToDictionary((JObject)value);
				}
				else if (value is JArray)
				{
					result[property.Name] = ToList((JArray)value);
				}
				else
				{
					result[property.Name] = ((JValue)value).Value;
				}
			}
			return result;
		}

		private List<object> ToList(JArray jArray)
		{
			var result = new List<object>();
			foreach (var value in jArray)
			{
				if (value is JObject)
				{
					result.Add(ToDictionary((JObject)value));
				}
				else if (value is JArray)
				{
					result.Add(ToList((JArray)value));
				}
				else
				{
					result.Add(((JValue)value).Value);
				}
			}
			return result;
		}

		private void PrintDictionary(Dictionary<string, object> dictionary, int indent = 0)
		{
			string indentString = new string(' ', indent);
			foreach (var kvp in dictionary)
			{
				Console.WriteLine($"{indentString}{kvp.Key}: {kvp.Value}");
				if (kvp.Value is Dictionary<string, object>)
				{
					PrintDictionary((Dictionary<string, object>)kvp.Value, indent + 2);
				}
				else if (kvp.Value is List<object>)
				{
					PrintList((List<object>)kvp.Value, indent + 2);
				}
			}
		}

		private void PrintList(List<object> list, int indent = 0)
		{
			string indentString = new string(' ', indent);
			foreach (var item in list)
			{
				if (item is Dictionary<string, object>)
				{
					PrintDictionary((Dictionary<string, object>)item, indent + 2);
				}
				else
				{
					Console.WriteLine($"{indentString}{item}");
				}
			}
		}


		private object FindValueByKey(Dictionary<string, object> dictionary, string key)
		{
			foreach (var kvp in dictionary)
			{
				if (kvp.Key.Equals(key, StringComparison.OrdinalIgnoreCase))
				{
					return kvp.Value;
				}

				if (kvp.Value is Dictionary<string, object> nestedDict)
				{
					var result = FindValueByKey(nestedDict, key);
					if (result != null)
					{
						return result;
					}
				}

				if (kvp.Value is List<object> list)
				{
					foreach (var item in list)
					{
						if (item is Dictionary<string, object> nestedListDict)
						{
							var result = FindValueByKey(nestedListDict, key);
							if (result != null)
							{
								return result;
							}
						}
					}
				}
			}

			return null;
		}


		#endregion

		public string EncryptString(string plainText, string key)
		{
			using (Aes aesAlg = Aes.Create())
			{
				aesAlg.Key = Encoding.UTF8.GetBytes(key);
				aesAlg.IV = new byte[16];

				var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

				using (var msEncrypt = new MemoryStream())
				{
					using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
					{
						using (var swEncrypt = new StreamWriter(csEncrypt))
						{
							swEncrypt.Write(plainText);
						}
						return Convert.ToBase64String(msEncrypt.ToArray());
					}
				}
			}
		}

		public string DecryptString(string cipherText, string key)
		{
			using (Aes aesAlg = Aes.Create())
			{
				aesAlg.Key = Encoding.UTF8.GetBytes(key);
				aesAlg.IV = new byte[16]; // Inicializa el vector de inicialización a ceros.

				var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

				using (var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
				{
					using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
					{
						using (var srDecrypt = new StreamReader(csDecrypt))
						{
							return srDecrypt.ReadToEnd();
						}
					}
				}
			}
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
	}
}
