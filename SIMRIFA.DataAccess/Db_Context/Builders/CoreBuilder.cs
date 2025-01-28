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
    public static class CoreBuilder
    {
        public static void Builder(ModelBuilder modelBuilder)
        {


			modelBuilder.Entity<TipoIdentificacion>(E =>
			{
				E.ToTable("TIPO_IDENTIFICACION", "core");
				E.HasKey(c => c.ID_TIPO_IDENTIFICACION);
			});


			modelBuilder.Entity<Cliente>(E =>
            {
                E.ToTable("CLIENTE", "core");
                E.HasKey(c => c.IdCliente);
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

            modelBuilder.Entity<Municipio>(E =>
            {
                E.ToTable("MUNICIPIO", "core");
                E.HasKey(c => c.ID_MUNICIPIO);

                E.HasOne(m => m.Departamento)
               .WithMany()
               .HasForeignKey(t => t.COD_DEPARTAMENTO);
            });

            modelBuilder.Entity<Departamento>(E =>
            {
                E.ToTable("DEPARTAMENTO", "core");
                E.HasKey(c => c.COD_DEPARTAMENTO);
            });


             modelBuilder.Entity<TipoCliente>(E =>
            {
                E.ToTable("TIPO_CLIENTE", "core");
                E.HasKey(c => c.ID_TIPO_CLIENTE);
            });

        }
    }
}
