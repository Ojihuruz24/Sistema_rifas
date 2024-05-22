using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SIMRIFA.Model.core
{

	[Table("SERIE", Schema = "core")]
	public class Serie
	{

		[Key]
		[Column("ID_SERIE")]
		[Required]
		public int IdSerie { get; set; }					  	
		

		[Column("NUMERO_SERIE")]
		[Required]
		public int NumeroSerie { get; set; }				  	
		

		[Column("ESTADO")]
		[Required]
		public bool Estado { get; set; }					  	
		

		[Column("NUMERO_MAXIMO")]
		[Required]
		public int NumeroMaximo { get; set; }				  	
		

		[Column("CONTADOR")]
		public int Contador { get; set; }					  	
		

		[Column("MARGEN")]
		public int? Margen { get; set; }


		[Column("FECHA_CREACION")]
		public DateTime FechaCreacion { get; set; }
	}
}
