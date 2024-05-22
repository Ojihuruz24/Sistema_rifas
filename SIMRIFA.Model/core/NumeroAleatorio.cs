using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Model.core
{
	[Table("NUMERO_ALEATORIO", Schema = "core")]
	public class NumeroAleatorio
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("ID_NUMERO")]
		public int IdNumero { get; set; }

		[Column("ID_CLIENTE")]
		[Required]
		public int IdCliente { get; set; }

		[Column("ID_SERIE")]
		[Required]
		public int IdSerie { get; set; }

		[Column("NUMERO")]
		[Required]
		public string Numero { get; set; }

		[Column("VENDIDO")]
		public bool Vendido { get; set; } = false;

		[Column("FECHA_CREACION")]
		public DateTime? FechaCreacion { get; set; }

		[ForeignKey("IdCliente")]
		public virtual Cliente Cliente { get; set; }

		[ForeignKey("IdSerie")]
		public virtual Serie Serie { get; set; }
	}
}
