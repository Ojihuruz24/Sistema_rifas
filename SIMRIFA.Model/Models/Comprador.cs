using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Model.Models
{
	[Table("COMPRADOR", Schema = "dbo")]
	public class Comprador
	{
		[Key]
		[Column("ID_USUARIO")]
		[Required]
		public int IdUsuario { get; set; }

		[Column("NOMBRE")]
		[Required]
		public string Nombre { get; set; }

		[Column("APELLIDO")]
		public string Apellido { get; set; }
		
		[Column("IDENTIFICACION")]
		public string Identificacion { get; set; }

		[Column("CORREO")]
		[Required]
		public string Correo { get; set; }

		[Column("TELEFONO")]
		[Required]
		public string Telefono { get; set; }

		[Column("SERIE")]
		[Required]
		public string Serie { get; set; }

		[Column("CANTIDAD")]
		[Required]
		public string Cantidad { get; set; }


		[Column("NUMEROS")]
		[Required]
		public string Numeros { get; set; }

		[Column("VALOR")]
		[Required]
		public string Valor { get; set; }

		[Column("ESTADO")]
		[Required]
		public string Estado { get; set; }

		[Column("FECHA_CREACION")]
		[Required]
		public DateTime FechaCreacion { get; set; }
	}
}
