using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Model.core
{

	[Table("TIPO_IDENTIFICACION", Schema = "core")]
	public class TipoIdentificacion : Auditoria
	{
		[Key]
		[Column("ID_TIPO_IDENTIFICACION")]
		[Required]
		public int ID_TipoIdentificacion { get; set; }

		[Key]
		[Column("DESCRIPCION")]
		[Required]
		public string Descripcion { get; set; }

		[Key]
		[Column("COD_OPERATIVO")]
		[Required]
		public string CodOperativo { get; set; }

	}
}
