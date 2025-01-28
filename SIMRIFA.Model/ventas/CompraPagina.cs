using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Model.ventas
{
    [Table("COMPRA_PAGINA", Schema = "vta")] //3
    public class CompraPagina : Auditoria
    {
        [Key]
        [Column("ID_COMPRA_PAGINA")]
        [Required]
        public int IdCompraPagina { get; set; }

        [Column("TIPO_CUENTA")]
        [Required]
        public string TipoCuenta { get; set; }


        [Column("DESCRIPCION")]
        [Required]
        public string DescripcionSocio { get; set; }


        [Column("EVIDENCIA")]
        [Required]
        public string EvidenciaSocio { get; set; }


		// ACA VA LA CLASE ORGANIZADOR

		[Column("ID_COMPRA_PAG_INFO_CLIENTE")]
		[Required]
		public int IdCompraPagInfoCliente { get; set; }
		public virtual CompraPagInfoCliente  CompraPagInfoCliente { get; set; }

		//ACA VA LA CLASE DE INFO DE LA PAGINA

		[Column("ID_COMPRA_PAG_INFO_PAGINA")]
		[Required]
		public int IdCompraPagInfoPagina { get; set; }
		public virtual CompraPagInfoPagina  CompraPagInfoPagina { get; set; }	

		[Column("TIPO_PASARELA")]
        [Required]
        public int TipoPasarela { get; set; }

		[Column("CONFIGURACION_PASARELA")]
		[Required]
		public string ConfigPasarela { get; set; }

		[Column("URL_PAG")]
		[Required]
		public string UrlPagina { get; set; }
	}

	

	
}
