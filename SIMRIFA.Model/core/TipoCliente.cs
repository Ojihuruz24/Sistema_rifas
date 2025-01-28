using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Model.core
{

	[Table("TIPO_CLIENTE", Schema = "core")]
	public class TipoCliente : Auditoria
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID_TIPO_CLIENTE { get; set; }

		[Required]
		public string NOMBRE { get; set; }

		[Required]
		public string DESCRIPCION { get; set; }
		
	}
}
