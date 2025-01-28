using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Model.ventas
{
	[Table("COMPRA_PAG_INFO_PAGINA", Schema = "vta")] // 2
	public class CompraPagInfoPagina : Auditoria
	{
		[Key]
		[Column("ID_COMPRA_PAG_INFO_PAGINA")]
		public int IdDatosInfoPag { get; set; }

		[Column("BANER_UNO")]
		[Required]
		public string BannerUno { get; set; }


		[Column("BANER_DOS")]
		[Required]
		public string BannerDos { get; set; }


		[Column("BANNER_TRES")]
		[Required]
		public string BannerTres { get; set; }


		[Column("NUMERO_MAXIMO")]
		[Required]
		public int CantidadNumerosMaximo { get; set; }


		[Column("PRECIO_TICKET")]
		[Required]
		public string PrecioTicket { get; set; }


		[Column("MONEDA")]
		[Required]
		public string Moneda { get; set; }


		[Column("FECHA_SORTEO")]
		public DateTime? FechaSorteo { get; set; }


		[Column("HORA_SORTEO")]
		public TimeSpan? HoraEvento { get; set; }

	}
}
