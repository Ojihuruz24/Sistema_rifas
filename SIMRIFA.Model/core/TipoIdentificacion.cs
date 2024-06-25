using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIMRIFA.Model.core
{

	[Table("TIPO_IDENTIFICACION", Schema = "core")]
	public class TipoIdentificacion : Auditoria
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("ID_TIPO_IDENTIFICACION")]
		public int ID_TIPO_IDENTIFICACION { get; set; }

		[Required]
		[Column("DESCRIPCION")]
		public string DESCRIPCION { get; set; }

		[Required]
		[Column("COD_OPERATIVO")]
		public string COD_OPERATIVO { get; set; }

	}
}
