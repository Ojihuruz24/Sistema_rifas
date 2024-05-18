using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Model.Models
{
	[Table("SERIE", Schema = "dbo")]
	public class Serie
	{
		[Key]
		[Column("ID_SERIE")]
		[Required]
		public int IdSerie { get; set; }

		[Column("NUMERO_SERIE")]
		[Required]
		public int NumeroSerie { get; set; }

		[Column("ESTADO")]
		[Required]
		public bool Estado { get; set; }

		[Column("NUMERO_MAXIMO")]
		[Required]
		public int NumeroMaximo { get; set; }

		[Column("CONTADOR")]
		[Required]
		public int Contador { get; set; }

		[Column("MARGEN")]
		[Required]
		public int Margen { get; set; }

		[Column("FECHA_CREACION")]
		[Required]
		public DateTime FechaCreacion { get; set; }

	}
}
