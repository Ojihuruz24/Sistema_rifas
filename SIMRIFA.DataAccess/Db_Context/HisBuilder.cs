using Microsoft.EntityFrameworkCore;
using SIMRIFA.Model.core;
using SIMRIFA.Model.Models.Wompi;
using SIMRIFA.Model.ventas;
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

			modelBuilder.Entity<Cliente>(E =>
			{
				E.ToTable("CLIENTE", "core");
				E.HasKey(c => c.IdCliente);
			});

			modelBuilder.Entity<TransaccionDto>(E =>
			{
				E.ToTable("TRANSACCION", "ventas");
				E.HasKey(c => c.IdTransaccion);

				E.HasOne(m => m.Cliente)
			   .WithMany()
			   .HasForeignKey(t => t.IdCliente)
			   .OnDelete(DeleteBehavior.Restrict);
			});

			modelBuilder.Entity<Serie>(E =>
			{
				E.ToTable("SERIE", "core");
				E.HasKey(c => c.IdSerie);
			});

			modelBuilder.Entity<NumeroAleatorio>(E =>
			{
				E.ToTable("NUMERO_ALEATORIO", "core");
				E.HasKey(c => c.IdNumero);

				E.HasOne(m => m.Cliente)
			   .WithMany()
			   .HasForeignKey(t => t.IdCliente)
			   .OnDelete(DeleteBehavior.Restrict);

				E.HasOne(m => m.Serie)
			   .WithMany()
			   .HasForeignKey(t => t.IdSerie)
			   .OnDelete(DeleteBehavior.Restrict);
			});

			modelBuilder.Entity<Correo>(E =>
			{
				E.ToTable("CORREO", "core");
				E.HasKey(c => c.IdCorreo);

				E.HasOne(m => m.Cliente)
			   .WithMany()
			   .HasForeignKey(t => t.IdCliente)
			   .OnDelete(DeleteBehavior.Restrict);
				
			});


			
			modelBuilder.Entity<InfoTransaccionDto>(E =>
			{
				E.ToTable("InfoTransaccion", "wompi");
				E.HasKey(c => c.Id);

				E.HasOne(m => m.Cliente)
			   .WithMany()
			   .HasForeignKey(t => t.idCliente)
			   .OnDelete(DeleteBehavior.Restrict);
			});




			//modelBuilder.Entity<Comprador>(E =>
			//{
			//	E.ToTable("COMPRADOR", "dbo");
			//	E.HasKey(c => c.IdUsuario);

			//});

			//modelBuilder.Entity<Serie>(E =>
			//{
			//	E.ToTable("SERIE", "dbo");
			//	E.HasKey(c => c.IdSerie);

			//});

			////modelBuilder.Entity<Boleta>(E =>
			////{
			////	E.ToTable("BOLETA", "dbo");
			////	E.HasKey(c => c.IdBoleta);

			////	E.HasOne(m => m.serie)
			////   .WithMany()
			////   .HasForeignKey(t => t.IdSerie)
			////   .OnDelete(DeleteBehavior.Restrict);
			////});




			////modelBuilder.Entity<CompradorBoleta>(E =>
			////{
			////	E.ToTable("COMPRADOR_BOLETA", "dbo");
			////	E.HasKey(c => c.IdBoleta);
			////	E.HasKey(c => c.IdUsuario);

			////	E.HasOne(m => m.Boleta)
			////   .WithMany()
			////   .HasForeignKey(t => t.IdBoleta)
			////   .OnDelete(DeleteBehavior.Restrict);

			////	E.HasOne(m => m.Comprador)
			////   .WithMany()
			////   .HasForeignKey(t => t.IdUsuario)
			////   .OnDelete(DeleteBehavior.Restrict);
			////});
		}
	}
}
