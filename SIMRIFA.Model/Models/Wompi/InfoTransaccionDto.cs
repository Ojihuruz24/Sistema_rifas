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

		public string? TransactionId { get; set; }

		public string? AliasChecksum { get; set; }

		public string? FullName { get; set; }

		public string? PhoneNumber { get; set; }

		public string? AliasType { get; set; }

		public string? CreatedAt { get; set; }

		public string? FinalizedAt { get; set; }

		public string? AmountInCents { get; set; }


		public string? Reference { get; set; }


		public string? CustomerEmail { get; set; }

		public string? Currency { get; set; }

		public string? PaymentMethodType { get; set; }

		public string? AliasStatus { get; set; }

		public string? StatusMessage { get; set; }

		public string? AliasEvent { get; set; }

		public string? SentAt { get; set; }

		public string? AliasTimestamp { get; set; }

		public string? Environment { get; set; }

		public int idCliente { get; set; }

		public string? ResponseJson { get; set; }

		public virtual Cliente Cliente { get; set; }
	}
}
