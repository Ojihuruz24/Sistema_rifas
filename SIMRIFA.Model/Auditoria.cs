using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Model
{
	public class Auditoria
	{
		[Column("FECHA_CREACION")]
		[Required]
		public DateTime FechaCreacion { get; set; }


		[Column("FECHA_MODIFICACION")]
		public DateTime? FechaModificacion { get; set;}


		[Column("ESTADO")]
		[Required]
		public bool Estado { get; set; }
	}
}
