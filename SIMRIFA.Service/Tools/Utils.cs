using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SIMRIFA.DataAccess.UnitOfWork;
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

				} while (NumerosExistentes().Contains(numero));

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

		public (string referencia, string has) has(int amount, string fechaExpiracion)
		{
			Random random = new Random();

			var valorrandom = random.Next(900000000);
			var valorrandom1 = random.Next(900000000);
			var valorrandom2 = random.Next(900000000);
			var valorrandom3 = random.Next(900000000);

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

		private List<int> NumerosExistentes()
		{
			var list = _unitOfWork._context.Comprador.Select(x => x.Numeros).ToList();
			var listnum = new List<int>();

			foreach (var item in list)
			{
				var conver = item.Split('-');

				foreach (var numero in conver)
				{
					listnum.Add(int.Parse(numero));
				}
			}

			return listnum;
		}
	}
}
