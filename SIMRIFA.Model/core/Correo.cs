using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Model.core
{
	[Table("CORREO", Schema = "core")]
	public class Correo
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("ID_CORREO")]
		public int IdCorreo { get; set; }

		[Required]
		[Column("ID_CLIENTE")]
		public int? IdCliente { get; set; }

		[Required]
		[MaxLength(255)]
		[Column("EMAIL_REMITENTE")]
		public string EmailRemitente { get; set; }

		[Required]
		[MaxLength(255)]
		[Column("NOMBRE_REMITENTE")]
		public string NombreRemitente { get; set; }

		[Required]
		[MaxLength(255)]
		[Column("ASUNTO")]
		public string Asunto { get; set; }

		[Required]
		[Column("CUERPO")]
		public string Cuerpo { get; set; }

		[Required]
		[MaxLength(100)]
		[Column("ESTADO")]
		public string Estado { get; set; }

		[Required]
		[Column("REINTENTOS")]
		public int Reintentos { get; set; }

		[Column("FECHA_ENVIO")]
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime FechaEnvio { get; set; }

		[ForeignKey("IdCliente")]
		public virtual Cliente Cliente { get; set; }
	}
}
