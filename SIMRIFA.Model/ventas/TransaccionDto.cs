using SIMRIFA.Model.core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Model.ventas
{
	[Table("TRANSACCION", Schema = "ventas")]
	public class TransaccionDto
	{
		[Key]
		[Column("ID_TRANSACCION")]
		[Required]
		public int IdTransaccion { get; set; }

		[Column("ID_CLIENTE")]
		[Required]
		public int IdCliente { get; set; }

		public virtual Cliente Cliente { get; set; }


		[Column("TRANSACCION")]
		[Required]
		public string Transaccion { get; set; }


		[Column("FECHA_INICIO_PAGO")]
		[Required]
		public string FechaInicioPago { get; set; }


		[Column("FECHA_FINAL_PAGO")]
		[Required]
		public string FechaFinalPago { get; set; }


		[Column("MONTO")]
		[Required]
		public string Monto { get; set; }


		[Column("MONEDA")]
		[Required]
		public string Moneda { get; set; }


		[Column("ESTADO")]
		[Required]
		public string Estado { get; set; }


		[Column("TIPO_PAGO")]
		[Required]
		public string TipoPago { get; set; }


		[Column("METODO_PAGO")]
		[Required]
		public string MetodoPago { get; set; }


		[Column("REFERENCIA")]
		public string? Referencia { get; set; }


		[Column("EMAIL")]
		[Required]
		public string Email { get; set; }


		[Column("FECHA_TRANSACCION")]
		public string FechaTransaccion { get; set; }


	
	}
}
