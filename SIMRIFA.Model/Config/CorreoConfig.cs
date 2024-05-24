using SIMRIFA.Model.core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Model.Config
{
	[Table("CORREO", Schema = "config")]
	public class CorreoConfig
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("ID_CORREO")]
		public int IdCorreo { get; set; }

		[Required]
		[Column("EMAIL_REMITENTE")]
		public string EmaiRemitente { get; set; }

		[Required]
		[Column("NOMBRE_REMITENTE")]
		public string NombreRemitente { get; set; }

		[Required]
		[Column("PASS")]
		public string Password { get; set; }

		[Required]
		[Column("ASUNTO")]
		public string Asunto { get; set; }

		[Required]
		[Column("CUERPO")]
		public string Cuerpo { get; set; }

		[Required]
		[Column("ESTADO")]
		public bool Estado { get; set; }


	}
}
