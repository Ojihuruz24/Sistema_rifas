using Openpay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Logic.PasarelasPagos.OpenPay
{
	public class ObtenerINfoOpenPay
	{

		public string Token(string token)
		{
			OpenpayAPI openpayAPI = new OpenpayAPI("sk_b0d84d541b2644059d6d2354b6514ea8", "mpdjvxwhembzerqeqfyb");
			openpayAPI.Production = false; // Default value = false

			return openpayAPI.TokenService.ToString();

		}

	}
}
