using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SIMRIFA.Service.Cliente;
using SIMRIFA.Service.Compra;
using SIMRIFA.Service.Correo;
using SIMRIFA.Service.NumeroAleatorio;
using SIMRIFA.Service.Series;
using SIMRIFA.Service.Tools;
using SIMRIFA.Service.Transaccion;
using SIMRIFA.Service.TransaccionesWompi;
using SIMRIFA.Service.Wompi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Service.ConfiguracionServicio
{
	public static class ServiceExtensions
	{

		public static IServiceCollection AddLogica(this IServiceCollection services)
		{
			services.TryAddScoped<IUtils, Utils>();
			services.TryAddScoped<ICompradorService, CompradorService>();
			services.TryAddScoped<ISerieService, SerieService>();
			services.TryAddScoped<ICorreoServicio, CorreoServicio>();
			services.TryAddScoped<ICorreo, Tools.Correo>();
			services.TryAddScoped<IWompiService, WompiService>();
			services.TryAddScoped<IClienteService, ClienteService>();
			services.TryAddScoped<ITransaccionService, TransaccionService>();
			services.TryAddScoped<INumeroAleatorioService, NumeroAleatorioService>();
			services.TryAddScoped<ITransaccionesService, TransaccionesService>();
			return services;
		}
	}
}
