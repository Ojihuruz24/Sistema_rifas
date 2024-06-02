using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SIMRIFA.Logic.ClienteLogic;
using SIMRIFA.Logic.CorreoLogic;
using SIMRIFA.Logic.NumeroAleatorioLogic;
using SIMRIFA.Logic.SerieLogic;

namespace SIMRIFA.Logic.ConfiguracionLogica
{
	public static class LogicExtensions
	{
		public static IServiceCollection AddLogica(this IServiceCollection services)
		{
			services.TryAddScoped<ICorreoLogicService, CorreoLogicService>();
			services.TryAddScoped<INumeroAleatorioLogicService, NumeroAleatorioLogicService>();
			services.TryAddScoped<IClienteLogicService, ClienteLogicService>();
			services.TryAddScoped<ISerieLogicService, SerieLogicService>();
			return services;
		}
	}
}