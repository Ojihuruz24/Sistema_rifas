using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SIMRIFA.DataAccess.Repository;
using SIMRIFA.DataAccess.Repository.MunicipioRepo;
using SIMRIFA.DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.DataAccess.ConfiguracionRepositorio
{
	public static class RepositoriosExtensions
	{
		public static IServiceCollection AddRepositorios(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddScoped<IMunicipioRepositorio, MunicipioRepositorio>();
            return services;
		}
	}
}
