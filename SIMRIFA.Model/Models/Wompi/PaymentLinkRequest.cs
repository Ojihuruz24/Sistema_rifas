using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Model.Models.Wompi
{
	public class PaymentLinkRequest
	{
		public string reference { get; set; }
		public string currency { get; set; }
		public int amount_in_cents { get; set; }
		public string customer_email { get; set; }
		public string redirect_url { get; set; }
		// Agrega otros campos necesarios según la documentación de Wompi
	}

	public class PaymentLinkResponse
	{
		public PaymentLinkData Data { get; set; }
	}

	public class PaymentLinkData
	{
		public string Id { get; set; }
		public string ShortUrl { get; set; }
		// Agrega otros campos según la respuesta de Wompi
	}

}
