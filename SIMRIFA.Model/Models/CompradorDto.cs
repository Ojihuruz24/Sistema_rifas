using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Model.Models
{
	public class CompradorDto
	{
		
		public int IdUsuario { get; set; }

		
		public string Nombre { get; set; }
		
		public string Correo { get; set; }
		
		public string Cantidad { get; set; }
		
		public string Numeros { get; set; }
		
		public string Valor { get; set; }
		
		public string Referencia { get; set; }
		
		public DateTime FechaCreacion { get; set; }
	}
}
