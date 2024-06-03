using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using SIMRIFA.DataAccess.Repository;
using SIMRIFA.DataAccess.UnitOfWork;
using SIMRIFA.Model.Config;
using SIMRIFA.Model.Models.Wompi;
using SIMRIFA.Service.NumeroAleatorio;
using SIMRIFA.Service.Transaccion;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SIMRIFA.Service.Tools
{
	public class Utils : IUtils
	{
		private List<int> numeros;
		private IUnitOfWork _unitOfWork;
		private INumeroAleatorioService _numeroAleatorioService;
		private ITransaccionService _transaccionService;
		private readonly IRepository<SIMRIFA.Model.Config.CorreoConfig> _correConfig;

		public Utils(IUnitOfWork unitOfWork, INumeroAleatorioService numeroAleatorioService, ITransaccionService transaccionService,
			IRepository<SIMRIFA.Model.Config.CorreoConfig> correoConfig)
		{
			numeros = new List<int>();
			_unitOfWork = unitOfWork;
			_numeroAleatorioService = numeroAleatorioService;
			_transaccionService = transaccionService;
			_correConfig = correoConfig;
		}

		public async Task<CorreoConfig> GetConfiguracionCorreoAsync()
		{
			var correoConfig = await _correConfig.GetOneOrAll(x => x.Estado == true);

			var correo = correoConfig.FirstOrDefault();

			return correo;
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
	}
}
