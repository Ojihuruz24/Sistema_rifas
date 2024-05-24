using Microsoft.EntityFrameworkCore;
using SIMRIFA.Model.Config;
using SIMRIFA.Model.core;
using SIMRIFA.Model.Models.Wompi;
using SIMRIFA.Model.ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.DataAccess.Db_Context.Builders
{
    public static class VentasBuilder
    {
		public static void Builder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<TransaccionDto>(E =>
			{
				E.ToTable("TRANSACCION", "ventas");
				E.HasKey(c => c.IdTransaccion);

				E.HasOne(m => m.Cliente)
			   .WithMany()
			   .HasForeignKey(t => t.IdCliente)
			   .OnDelete(DeleteBehavior.Restrict);
			});
		}
	}
}
