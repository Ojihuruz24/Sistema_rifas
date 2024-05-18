﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIMRIFA.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		//public virtual DbSet<Boleta> Boleta { get; set; }
		public virtual DbSet<Comprador> Comprador { get; set; }
		public virtual DbSet<Serie> Serie { get; set; }
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
			HisBuilder.Builder(modelBuilder);
		}

	}
}
