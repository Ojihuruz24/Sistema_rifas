using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMRIFA.Model.core;

namespace SIMRIFA.Model.ventas
{
	[Table("COMPRA_PAG_INFO_CLIENTE", Schema = "vta")] // 1
	public class CompraPagInfoCliente : Auditoria
	{
		[Column("ID_COMPRA_PAG_INFO_CLIENTE")]
		[Key]
		public int IdCompraPagInfoCliente { get; set; }

		[Column("LOGO")]
		[Required]
		public string Logo { get; set; }


		[Column("ORGANIZADOR")]
		[Required]
		public string Organizador { get; set; }


		[Column("NOMBRE_RESPONSABLE")]
		[Required]
		public string NombreResponsable { get; set; }


		[Column("PAIS")]
		[Required]
		public string Pais { get; set; }

		[Column("MUNICIPIO")]
		[Required]
		public string Municipio { get; set; }


		[Column("CORREO")]
		[Required]
		public string Correo { get; set; }


		[Column("CELULAR")]
		[Required]
		public string Celular { get; set; }


		[Column("TIPO_IDENTIFICACION")]
		[Required]
		public int IdTipoIdentificacion { get; set; }

		public virtual TipoIdentificacion  TipoIdentificacion { get; set; }


		[Column("IDENTIFICACION")]
		[Required]
		public string NumeroIdentificacion { get; set; }


		[Column("FACEBOOK")]
		public string? Facebook { get; set; }


		[Column("INSTAGRAM")]
		public string? Intagram { get; set; }


		[Column("TELEGRAM")]
		public string? Telegram { get; set; }


		[Column("TIKTOK")]
		public string? Tiktok { get; set; }

	}
}
