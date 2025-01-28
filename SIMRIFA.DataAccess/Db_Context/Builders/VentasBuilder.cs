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

			modelBuilder.Entity<CompraPagina>(E =>
			{
				E.ToTable("COMPRA_PAGINA", "vta");
				E.HasKey(c => c.IdCompraPagina);

				E.HasOne(m => m.CompraPagInfoCliente)
			   .WithMany()
			   .HasForeignKey(t => t.IdCompraPagInfoCliente)
			   .OnDelete(DeleteBehavior.Restrict);

				E.HasOne(m => m.CompraPagInfoPagina)
			   .WithMany()
			   .HasForeignKey(t => t.IdCompraPagInfoPagina)
			   .OnDelete(DeleteBehavior.Restrict);
			});

			modelBuilder.Entity<CompraPagInfoCliente>(E =>
			{
				E.ToTable("COMPRA_PAG_INFO_CLIENTE", "vta");
				E.HasKey(c => c.IdCompraPagInfoCliente);

				E.HasOne(m => m.TipoIdentificacion)
			   .WithMany()
			   .HasForeignKey(t => t.IdTipoIdentificacion)
			   .OnDelete(DeleteBehavior.Restrict);
			});

			modelBuilder.Entity<CompraPagInfoPagina>(E =>
			{
				E.ToTable("COMPRA_PAG_INFO_PAGINA", "vta");
				E.HasKey(c => c.IdDatosInfoPag);
			});


		}
	}
}
