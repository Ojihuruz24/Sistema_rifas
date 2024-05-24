using Microsoft.EntityFrameworkCore;
using SIMRIFA.Model.Config;
using SIMRIFA.Model.ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.DataAccess.Db_Context.Builders
{
	internal class ConfigBuilder
	{
		public static void Builder(ModelBuilder modelBuilder)
		{

			modelBuilder.Entity<CorreoConfig>(E =>
			{
				E.ToTable("CORREO", "config");
				E.HasKey(c => c.IdCorreo);
			});
		}
	}
}
