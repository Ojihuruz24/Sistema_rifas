using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SIMRIFA.Logic.ClienteLogic;
using SIMRIFA.Logic.CorreoLogic;
using SIMRIFA.Logic.NumeroAleatorioLogic;
using SIMRIFA.Logic.SerieLogic;
using SIMRIFA.Logic.TipoIdentificacion;
using SIMRIFA.Logic.Tools;
using SIMRIFA.Logic.Transaccion;

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
			services.TryAddScoped<ITransaccionLogicService, TransaccionLogicService>();
			services.TryAddScoped<IUtilisLogic, UtilisLogic>();
			services.TryAddScoped<ITipoIdentificacionService, TipoIdentificacionService>();
			return services;
		}
	}
}