using Microsoft.EntityFrameworkCore;
using SIMRIFA.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.DataAccess.Db_Context
{
	public static class HisBuilder
	{
		public static void Builder(ModelBuilder modelBuilder)
		{


			modelBuilder.Entity<Comprador>(E =>
			{
				E.ToTable("COMPRADOR", "dbo");
				E.HasKey(c => c.IdUsuario);
				
			});

			modelBuilder.Entity<Serie>(E =>
			{
				E.ToTable("SERIE", "dbo");
				E.HasKey(c => c.IdSerie);

			});

			//modelBuilder.Entity<Boleta>(E =>
			//{
			//	E.ToTable("BOLETA", "dbo");
			//	E.HasKey(c => c.IdBoleta);

			//	E.HasOne(m => m.serie)
			//   .WithMany()
			//   .HasForeignKey(t => t.IdSerie)
			//   .OnDelete(DeleteBehavior.Restrict);
			//});




			//modelBuilder.Entity<CompradorBoleta>(E =>
			//{
			//	E.ToTable("COMPRADOR_BOLETA", "dbo");
			//	E.HasKey(c => c.IdBoleta);
			//	E.HasKey(c => c.IdUsuario);

			//	E.HasOne(m => m.Boleta)
			//   .WithMany()
			//   .HasForeignKey(t => t.IdBoleta)
			//   .OnDelete(DeleteBehavior.Restrict);

			//	E.HasOne(m => m.Comprador)
			//   .WithMany()
			//   .HasForeignKey(t => t.IdUsuario)
			//   .OnDelete(DeleteBehavior.Restrict);
			//});
		}
	}
}
