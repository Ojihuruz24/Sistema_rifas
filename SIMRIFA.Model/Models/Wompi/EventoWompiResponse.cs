using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SIMRIFA.Model.Models.Wompi
{
	public class EventoWompiResponse
	{
		[JsonPropertyName("event")]
		public string? Event { get; set; }

		[JsonPropertyName("data")]
		public Data Data { get; set; }

		[JsonPropertyName("sent_at")]
		public string? SentAt { get; set; }

		[JsonPropertyName("timestamp")]
		[Column("alias_Timestamp")]
		public string? Timestamp { get; set; }

		[JsonPropertyName("signature")]
		public SignatureEvent? Signature { get; set; }

		[JsonPropertyName("environment")]
		public string? Environment { get; set; }
	}

	public class Data
	{
		[JsonPropertyName("transaction")]
		public Transaction Transaction { get; set; }
	}

	public class CustomerDataEvent
	{
		[JsonPropertyName("full_name")]
		public string? FullName { get; set; }

		[JsonPropertyName("phone_number")]
		public string? PhoneNumber { get; set; }
	}

	public class Transaction
	{
		[JsonPropertyName("id")]
		public string? Id { get; set; }

		[JsonPropertyName("created_at")]
		public string? CreatedAt { get; set; }

		[JsonPropertyName("finalized_at")]
		public string? FinalizedAt { get; set; }

		[JsonPropertyName("amount_in_cents")]
		public decimal? AmountInCents { get; set; }

		[JsonPropertyName("reference")]
		public string? Reference { get; set; }

		[JsonPropertyName("customer_email")]
		public string? CustomerEmail { get; set; }

		[JsonPropertyName("currency")]
		public string? Currency { get; set; }

		[JsonPropertyName("payment_method_type")]
		public string? PaymentMethodType { get; set; }

		[JsonPropertyName("payment_method")]
		public PaymentMethodEvent? PaymentMethod { get; set; }

		[JsonPropertyName("status")]
		[Column("Alias_Status")]
		public string? Status { get; set; }

		[JsonPropertyName("status_message")]
		public string? StatusMessage { get; set; }

		[JsonPropertyName("shipping_address")]
		public object? ShippingAddress { get; set; }

		[JsonPropertyName("redirect_url")]
		public string? RedirectUrl { get; set; }

		[JsonPropertyName("payment_source_id")]
		public string? PaymentSourceId { get; set; }

		[JsonPropertyName("payment_link_id")]
		public string? PaymentLinkId { get; set; }

		[JsonPropertyName("customer_data")]
		public CustomerDataEvent? CustomerData { get; set; }

		[JsonPropertyName("billing_data")]
		public billingData? BillingData { get; set; }
	}

	public class Extra
	{
		[JsonPropertyName("external_identifier")]
		public string? ExternalIdentifier { get; set; }

		[JsonPropertyName("business_agreement_code")]
		public string? BusinessAgreementCode { get; set; }

		[JsonPropertyName("payment_intention_identifier")]
		public string? PaymentIntentionIdentifier { get; set; }
	}

	public class PaymentMethodEvent
	{
		[JsonPropertyName("type")]
		[Column("alias_type")]
		public string? Type { get; set; }

		[JsonPropertyName("sandbox_status")]
		public string? SandboxStatus { get; set; }

		[JsonPropertyName("payment_description")]
		public string? PaymentDescription { get; set; }

		[JsonPropertyName("extra")]
		public Extra? Extra { get; set; }
	}


	public class SignatureEvent
	{
		[JsonPropertyName("checksum")]
		[Column("alias_Timestamp")]
		public string? Checksum { get; set; }

		[JsonPropertyName("properties")]
		public PropertiesEvent? Properties { get; set; }
	}

	public class PropertiesEvent
	{
		[JsonPropertyName("transaction.id")]
		public string? TransactionId { get; set; }

		[JsonPropertyName("transaction.status")]
		public string? TransactionStatus { get; set; }

		[JsonPropertyName("transaction.amount_in_cents")]
		public string? TransactionAmountInCents { get; set; }
	}

	public class billingData
	{
		[MaxLength(255)]
		[JsonPropertyName("address_line_1")]
		public string? AddressLine1 { get; set; }

		[JsonPropertyName("address_line_2")]
		public string? AddressLine2 { get; set; }

		[JsonPropertyName("city")]
		public string? City { get; set; }

		[JsonPropertyName("state")]
		public string? State { get; set; }

		[JsonPropertyName("country")]
		public string? Country { get; set; }

		[JsonPropertyName("postal_code")]
		public string? PostalCode { get; set; }

		[JsonPropertyName("full_name")]
		public string? FullName { get; set; }
	}
}
