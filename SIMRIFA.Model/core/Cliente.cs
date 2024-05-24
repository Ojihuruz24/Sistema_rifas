using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Model.core
{
	[Table("CLIENTE", Schema = "core")]
	public class Cliente
	{
		[Key]
		[Column("ID_CLIENTE")]
		[Required]
		public int IdCliente { get; set; }

		[Column("NOMBRE")]
		[Required]
		public string Nombre { get; set; }

		[Column("TELEFONO")]
		public string? Telefono { get; set; }

		[Column("DIRECCION")]
		public string? Direccion { get; set; }

		[Column("CORREO")]
		[Required]
		public string Correo { get; set; }

		[Column("FECHA_CREACION")]
		[Required]
		public DateTime? FechaCreacion { get; set; }

		[Column("ENVIO_CORREO")]
		[Required]
		public bool EnvioCorreo { get; set; } = false;


	}
}
