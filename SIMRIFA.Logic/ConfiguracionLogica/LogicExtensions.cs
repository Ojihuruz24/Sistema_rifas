using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SIMRIFA.Logic.Correo;
using SIMRIFA.Logic.NumeroAleatorio;
using SIMRIFA.Service.Cliente;
using SIMRIFA.Service.Compra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Logic.ConfiguracionLogica
{
	public static class LogicExtensions
	{
		public static IServiceCollection AddLogica(this IServiceCollection services)
		{
			services.TryAddScoped<IClienteService, ClienteService>();
			services.TryAddScoped<ICorreoLogicService, CorreoLogicService>();
			services.TryAddScoped<INumeroAleatorioLogicService, NumeroAleatorioLogicService>();
			return services;
		}
	}
}