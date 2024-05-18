using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Model.Models.Wompi
{
	public class EventoWompiResponse
	{
		[JsonProperty("event")]
		public string Event { get; set; }

		[JsonProperty("data")]
		public DataEvent Data { get; set; }

		[JsonProperty("sent_at")]
		public DateTime SentAt { get; set; }

		[JsonProperty("timestamp")]
		public string Timestamp { get; set; }

		[JsonProperty("signature")]
		public SignatureEvent Signature { get; set; }

		[JsonProperty("environment")]
		public string Environment { get; set; }
	}

	public class CustomerDataEvent
	{
		[JsonProperty("full_name")]
		public string FullName { get; set; }

		[JsonProperty("phone_number")]
		public string PhoneNumber { get; set; }
	}

	public class DataEvent
	{
		[JsonProperty("transaction")]
		public TransactionEvent Transaction { get; set; }
	}

	public class Extra
	{
		[JsonProperty("external_identifier")]
		public string ExternalIdentifier { get; set; }

		[JsonProperty("business_agreement_code")]
		public string BusinessAgreementCode { get; set; }

		[JsonProperty("payment_intention_identifier")]
		public string PaymentIntentionIdentifier { get; set; }
	}

	public class PaymentMethodEvent
	{
		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("sandbox_status")]
		public string SandboxStatus { get; set; }

		[JsonProperty("payment_description")]
		public string PaymentDescription { get; set; }

		[JsonProperty("extra")]
		public Extra Extra { get; set; }
	}

	public class PropertiesEvent
	{
		[JsonProperty("transaction.id")]
		public string TransactionId { get; set; }

		[JsonProperty("transaction.status")]
		public string TransactionStatus { get; set; }

		[JsonProperty("transaction.amount_in_cents")]
		public string TransactionAmountInCents { get; set; }
	}

	public class SignatureEvent
	{
		[JsonProperty("checksum")]
		public string Checksum { get; set; }

		[JsonProperty("properties")]
		public PropertiesEvent Properties { get; set; }
	}

	public class TransactionEvent
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("created_at")]
		public DateTime CreatedAt { get; set; }

		[JsonProperty("finalized_at")]
		public DateTime FinalizedAt { get; set; }

		[JsonProperty("amount_in_cents")]
		public int AmountInCents { get; set; }

		[JsonProperty("reference")]
		public string Reference { get; set; }

		[JsonProperty("customer_email")]
		public string CustomerEmail { get; set; }

		[JsonProperty("currency")]
		public string Currency { get; set; }

		[JsonProperty("payment_method_type")]
		public string PaymentMethodType { get; set; }

		[JsonProperty("payment_method")]
		public PaymentMethodEvent PaymentMethod { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("status_message")]
		public object StatusMessage { get; set; }

		[JsonProperty("shipping_address")]
		public object ShippingAddress { get; set; }

		[JsonProperty("redirect_url")]
		public string RedirectUrl { get; set; }

		[JsonProperty("payment_source_id")]
		public object PaymentSourceId { get; set; }

		[JsonProperty("payment_link_id")]
		public object PaymentLinkId { get; set; }

		[JsonProperty("customer_data")]
		public CustomerDataEvent CustomerData { get; set; }

		[JsonProperty("billing_data")]
		public object BillingData { get; set; }
	}
}
