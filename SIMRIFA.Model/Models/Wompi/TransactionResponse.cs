using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Model.Models.Wompi
{
	public class TransactionResponse
	{
		[JsonProperty("data")]
		public DataEvent Data { get; set; }
	}
		

	public class DataEvent
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("created_at")]
		public string CreatedAt { get; set; }

		[JsonProperty("amount_in_cents")]
		public int AmountInCents { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("reference")]
		public string Reference { get; set; }

		[JsonProperty("customer_email")]
		public string CustomerEmail { get; set; }

		[JsonProperty("currency")]
		public string Currency { get; set; }

		[JsonProperty("payment_method_type")]
		public string PaymentMethodType { get; set; }

		[JsonProperty("payment_method")]
		public PaymentMethod PaymentMethod { get; set; }

		[JsonProperty("shipping_address")]
		public ShippingAddress ShippingAddress { get; set; }

		[JsonProperty("redirect_url")]
		public string RedirectUrl { get; set; }

		[JsonProperty("payment_link_id")]
		public object PaymentLinkId { get; set; }
	}

	public class PaymentMethod
	{
		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("phone_number")]
		public long PhoneNumber { get; set; }
	}

	public class ShippingAddress
	{
		[JsonProperty("address_line_1")]
		public string AddressLine1 { get; set; }

		[JsonProperty("country")]
		public string Country { get; set; }

		[JsonProperty("region")]
		public string Region { get; set; }

		[JsonProperty("city")]
		public string City { get; set; }

		[JsonProperty("phone_number")]
		public long PhoneNumber { get; set; }
	}

}
