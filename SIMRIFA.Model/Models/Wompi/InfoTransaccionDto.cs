using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMRIFA.Model.core;
using System.Text.Json.Serialization;

namespace SIMRIFA.Model.Models.Wompi
{
	[Table("InfoTransaccion", Schema = "wompi")]
	public class InfoTransaccionDto
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public string? ExternalIdentifier { get; set; }

		public string? BusinessAgreementCode { get; set; }

		public string? PaymentIntentionIdentifier { get; set; }

		public string? TransactionId { get; set; }

		public string? AliasChecksum { get; set; }

		public string? FullName { get; set; }

		public string? PhoneNumber { get; set; }

		public string? AliasType { get; set; }

		public string? SandboxStatus { get; set; }

		public string? PaymentDescription { get; set; }

		public string? CreatedAt { get; set; }

		public string? FinalizedAt { get; set; }

		public string? AmountInCents { get; set; }


		public string? Reference { get; set; }


		public string? CustomerEmail { get; set; }

		public string? Currency { get; set; }

		public string? PaymentMethodType { get; set; }

		public string? AliasStatus { get; set; }

		public string? StatusMessage { get; set; }

		public string? RedirectUrl { get; set; }

		public string? PaymentSourceId { get; set; }

		public string? PaymentLinkId { get; set; }

		public string? Billing_data_AddressLine1 { get; set; }

		public string? Billing_data_AddressLine2 { get; set; }

		public string? Billing_data_City { get; set; }

		public string? Billing_data_State { get; set; }

		public string? Billing_data_Country { get; set; }

		public string? Billing_data_PostalCode { get; set; }

		public string? AliasEvent { get; set; }

		public string? SentAt { get; set; }

		public string? AliasTimestamp { get; set; }

		public string? Environment { get; set; }

		public string? Property_transaction_id { get; set; }

		public string? Property_transaction_status { get; set; }

		public string? Property_amount_in_cents { get; set; }

		public int idCliente { get; set; }

		public string? ResponseJson { get; set; }

		public virtual Cliente Cliente { get; set; }
	}
}
