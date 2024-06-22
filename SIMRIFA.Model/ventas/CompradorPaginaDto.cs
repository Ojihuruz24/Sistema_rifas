using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Model.ventas
{
    [Table("COMPRA_PAG_CLIENTE", Schema = "ventas")]
    public class CompradorPaginaDto
    {

        [Key]
        [Column("ID_COMPRA_PAG_CLIENTE")]
        [Required]
        public int idCompraPagCliente { get; set; }

        [Column("TIPO_CUENTA")]
        [Required]
        public string TipoCuenta { get; set; }


        [Column("DESCRIPCION_SOCIO")]
        [Required]
        public string DescripcionSocio { get; set; }


        [Column("EVIDENCIA_SOCIO")]
        [Required]
        public byte EvidenciaSocio { get; set; }
    
        // ACA VA LA CLASE ORGANIZADOR
        public virtual DatosOrganizador DatosOrganizador { get; set; }

     //ACA VA LA CLASE DE INFO DE LA PAGINA

		public virtual DatosInfoPag DatosInfoPag { get; set; }	


		[Column("TIPO_PASARELA")]
        [Required]
        public string PasarelaPago { get; set; }


        [Column("LLAVE_PUBLICA")]
        [Required]
        public string LlavePublica { get; set; }


        [Column("LLAVE_PRIVADA")]
        [Required]
        public string LlavePrivada { get; set; }


        [Column("LLAVE_EVENTO")]
        [Required]
        public string LlaveEvento { get; set; }


        [Column("LLAVE_INTEGRIDAD")]
        [Required]
        public string LlaveIntegridad { get; set; }

	}

    [Table("COMPRA_PAG_INFO_CLIENTE", Schema = "ventas")] // 1
    public class DatosOrganizador
    {
        [Column("ID")]
        [Key]
		public int IdCompraPagInfoCliente { get; set; }

		[Column("LOGO")]
		[Required]
		public byte Logo { get; set; }


		[Column("ORGANIZADOR")]
		[Required]
		public string Organizador { get; set; }


		[Column("NOMBRE_RESPONSABLE")]
		[Required]
		public string NombreResponsable { get; set; }


		[Column("PAIS")]
		[Required]
		public string Pais { get; set; }

		[Column("DEPARTAMENTO")]
		[Required]
		public string Departamento { get; set; }


		[Column("CORREO")]
		[Required]
		public string Correo { get; set; }


		[Column("CELULAR")]
		[Required]
		public string Celular { get; set; }


		[Column("TIPO_CEDULA")]
		[Required]
		public string TipoCedula { get; set; }


		[Column("IDENTIFICACION")]
		[Required]
		public string Identificacion { get; set; }


		[Column("FACEBOOK")]
		[Required]
		public string Facebook { get; set; }


		[Column("INSTAGRAM")]
		[Required]
		public string Intagram { get; set; }


		[Column("TELEGRAM")]
		[Required]
		public string Telegram { get; set; }


		[Column("TIKTOK")]
		[Required]
		public string Tiktok { get; set; }

	}

	[Table("COMPRA_PAG_DATOS_INFO_PAG", Schema = "ventas")] // 2
	public class DatosInfoPag
	{
		[Key]
		[Column("ID")]
		public int IdDatosInfoPag { get; set; }

		[Column("BANER_UNO")]
		[Required]
		public byte BannerUno { get; set; }


		[Column("BANER_DOS")]
		[Required]
		public byte BannerDos { get; set; }


		[Column("BANNER_TRES")]
		[Required]
		public byte BannerTres { get; set; }


		[Column("NUMERO_MAXIMO")]
		[Required]
		public int CantidadNumerosMaximo { get; set; }


		[Column("PRECIO_TICKET")]
		[Required]
		public string PrecioTicket { get; set; }


		[Column("MONEDA")]
		[Required]
		public string Moneda { get; set; }


		[Column("FECHA_SORTEO")]
		[Required]
		public DateTime FechaSorteo { get; set; }


		[Column("HORA_SORTEO")]
		[Required]
		public TimeSpan HoraEvento { get; set; }

	}


	[Table("COMPRA_PAG_INFO_PAGO_WOMPI", Schema = "ventas")] //3
	public class CompraPaginaInfoPagoWompi
	{
		[Key]
		[Column("ID_COMPRA_PAG_INFO_PAGO_WOMPI")]
		public int Id { get;  set; }


		[Column("DESCRIPCION")]
		public string? Descripcion { get; set; }


		[Column("LLAVE_PRIMARIA")]
		[Required]
		public string LlavePrimaria { get; set; }


		[Column("LLAVE_PRIVADA")]
		[Required]
		public string LlavePrivada { get; set; }


		[Column("LLAVE_EVENTO")]
		[Required]
		public string LlaveEvento { get; set; }


		[Column("LLAVE_INTEGRIDAD")]
		[Required]
		public string llaveIntegridad { get; set; }
		

	}
}
