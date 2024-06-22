using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIMRIFA.Model.core;
using SIMRIFA.Model.Models.Wompi;
using SIMRIFA.Model.ventas;
using SIMRIFA.DataAccess.Db_Context.Builders;
using SIMRIFA.Model.Config;

namespace SIMRIFA.DataAccess.Db_Context
{
	public class SIMRIFAdbContext : DbContext
	{
		string _connectionString;

		public SIMRIFAdbContext(string connectionString) => _connectionString = connectionString;

		public SIMRIFAdbContext(DbContextOptions<SIMRIFAdbContext> options) : base(options) { }

		public SIMRIFAdbContext()
		{
			IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
			_connectionString = config.GetConnectionString("DefaultConnection");
		}

		#region DbSet

		public virtual DbSet<Cliente> Cliente { get; set; }
		public virtual DbSet<TransaccionDto> Transaccion { get; set; }
		public virtual DbSet<Serie> Serie { get; set; }
		public virtual DbSet<NumeroAleatorio> NumeroAleatorio { get; set; }
		public virtual DbSet<Correo> Correo { get; set; }
		public virtual DbSet<InfoTransaccionDto>  InfoTransaccionDtos { get; set; }
		public virtual DbSet<CorreoConfig>  CorreConfig { get; set; }

		public virtual DbSet<TipoIdentificacion>  TipoIdentificacion { get; set; }


		//public virtual DbSet<Boleta> Boleta { get; set; }
		//public virtual DbSet<Comprador> Comprador { get; set; }
		//public virtual DbSet<Serie> Serie { get; set; }
		//public virtual DbSet<CompradorBoleta> CompradorBoleta { get; set; }

		#endregion

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			// Assign _connectionString to the optionsBuilder:
			if (_connectionString != null)
				optionsBuilder.UseSqlServer(_connectionString);

			// Recommended: uncomment the following line to enable the SQL trace window:
			if (InsideLINQPad) optionsBuilder.EnableSensitiveDataLogging(true);
		}

		internal bool InsideLINQPad => AppDomain.CurrentDomain.FriendlyName.StartsWith("LINQPad");

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			CoreBuilder.Builder(modelBuilder);
			VentasBuilder.Builder(modelBuilder);
			ConfigBuilder.Builder(modelBuilder);
		}

	}
}
