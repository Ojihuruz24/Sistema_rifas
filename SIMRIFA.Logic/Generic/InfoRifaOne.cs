using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Logic.Generic
{
	public class InfoRifaOne
	{
		public InfoRifaOne()
		{

		}

		public CLIENTE ObtenerCliente(string urlPagina)
		{
			CLIENTE cliente = new CLIENTE();

			if (urlPagina == "001")
			{
				cliente.Nombre = "Oscar Dayan Agudelo Patiño";
				cliente.TipoIdentificacion = "CC";
				cliente.Identificacion = "1036957989";
				cliente.Password = "pepitoPerez";
				cliente.Direccion = "Porvenir";
				cliente.Telefono = "3113633371";
				cliente.Correo = "Oscar Dayan Agudelo Patiño";
				cliente.FechaNacimiento = DateTime.Now;
			}

			if (urlPagina == "002")
			{
				cliente.Nombre = "segundo cliente";
				cliente.TipoIdentificacion = "CC";
				cliente.Identificacion = "1242846483";
				cliente.Password = "5353";
				cliente.Direccion = "bogota";
				cliente.Telefono = "2525252";
				cliente.FechaNacimiento = DateTime.Now;
			}

			return cliente;
		}

		public PagoPagina ObtenerPagina(int IdCliente)
		{
			PagoPagina pagoPagina = new PagoPagina();

			if (IdCliente ==  1)
			{

			}

			if (IdCliente == 2)
			{

			}

			return pagoPagina;
		}
	}


	#region core.cliente
	public class CLIENTE
	{
		public int IdCliente { get; set; } // PK
		public string Nombre { get; set; }
		public string TipoIdentificacion { get; set; }
		public string Identificacion { get; set; }
		public string Password { get; set; }
		public string Direccion { get; set; }
		public string Telefono { get; set; }
		public string Correo { get; set; }
		public DateTime FechaNacimiento { get; set; }
		public int? IdPagina { get; set; }
		public string UrlInstagram { get; set; }
		public string TelefonoTelegram { get; set; }
		public string UrlFacebook { get; set; }
		public string Estado { get; set; }
		public string TipoUsuario { get; set; }
	}
	#endregion

	#region core.PAGO_PAGINA

	public class PagoPagina
	{
		public int IdPagoPagina { get; set; }
		public string NombrePagina { get; set; }
		public string UrlPagina { get; set; }
		public string Descripcion { get; set; }
		public string TipoPagina { get; set; }
		public bool Habilitada { get; set; }
		public string TipoPago { get; set; }
		public int? IdTransaccion { get; set; }
		public DateTime FechaCreacion { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public DateTime? FechaFin { get; set; }
		public int CantidadEnvioCorreo { get; set; }
		public string ServicioEmail { get; set; }
		public int? IdPasarelaPago { get; set; }
	}


	#endregion



	#region

	#endregion

	#region

	#endregion

	#region

	#endregion

	#region

	#endregion

	#region

	#endregion

	#region

	#endregion


















	public class wompi {

        public int id_wompi { get; set; }
        public string clave_prublica { get; set; }
        public string clave_privada { get; set; }
        public string url_evento { get; set; }
        public string evento { get; set; }
        public string Integridad_key { get; set; }
    }


}
