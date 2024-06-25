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
		[Required]
		public DateTime FECHA_CREACION { get; set; }

		public DateTime? FECHA_MODIFICACION { get; set;}

		[Required]
		public bool ESTADO { get; set; }
	}
}
