using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using SIMRIFA.DataAccess.UnitOfWork;
using SIMRIFA.Model.Models.Wompi;
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


		public Utils(IUnitOfWork unitOfWork)
		{
			numeros = new List<int>();
			_unitOfWork = unitOfWork;
		}

		public decimal Calcular(decimal valor, decimal cantidad)
		{
			//var porcentajeAdiccional = MargenError(valor);

			decimal info = Decimal.Truncate((Convert.ToDecimal(valor) / Convert.ToDecimal(cantidad)) * Convert.ToDecimal(100));

			return info;
		}

		public List<int> GenerarNumeroAletorio(int valor)
		{
			for (int i = 1; i <= 3; i++)
			{
				int numero;
				do
				{
					numero = GenerarNumeroAleatorio();

				} while (NumerosExistentes().Contains(numero.ToString()));

				numeros.Add(numero);
			}

			return numeros;
		}

		public decimal MargenError(decimal valor)
		{
			var margen = _unitOfWork._context.Serie.Where(x => x.Estado == true)?.FirstOrDefault()?.Margen;

			var porcentajeAdiccional = Decimal.Truncate(Convert.ToDecimal(valor) * Convert.ToDecimal(margen)) / 100;

			return porcentajeAdiccional;
		}

		public (string referencia, string has) has(int amount, int cantidad, string fechaExpiracion)
		{
			Random random = new Random();

			var valorrandom = random.Next(900000000);
			var valorrandom1 = random.Next(900000000);
			var valorrandom2 = random.Next(900000000);
			var valorrandom3 = $"{random.Next(900000000)}_{cantidad}";

			var publicKey = "test_integrity_YcpIHCiQvGKc01VK9kpTNo3wvb4vFV8g";
			var currency = "COP";
			var reference = $"{valorrandom}-{valorrandom1}-{valorrandom2}-{valorrandom3}";

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

		private int GenerarNumeroAleatorio()
		{
			Random rnd = new Random();
			return rnd.Next(0, 999); // Puedes ajustar el rango según tus necesidades
		}

		private List<string> NumerosExistentes()
		{
			var list = _unitOfWork._context.NumeroAleatorio.Select(x => x.Numero).ToList();

			return list;
		}

		public async Task<bool> ValidacionInfo(EventoWompiResponse response)
		{
			var TransaccionID = response?.Data?.Transaction?.Id;
			var TransaccionStatus = response?.Data?.Transaction?.Status;
			var TransaccionAmount = response?.Data?.Transaction?.AmountInCents.ToString();
			var timestamp = response?.Timestamp;

			string concat = $"{TransaccionID}{TransaccionStatus}{TransaccionAmount}";

			if (string.IsNullOrEmpty(timestamp))
			{
				concat = $"{TransaccionID}{TransaccionStatus}{TransaccionAmount}{timestamp}test_events_QeLdcWGI7qBSTtMoSSLt5hB8PmEERLTY";
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

			if (encryp == response?.Signature?.Checksum)
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
	}
}
