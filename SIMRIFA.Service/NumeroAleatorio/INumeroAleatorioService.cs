using System.Linq.Expressions;

namespace SIMRIFA.Service.NumeroAleatorio
{
	public interface INumeroAleatorioService
	{
		Task<Model.core.NumeroAleatorio> ActualizarAsync(Model.core.NumeroAleatorio numeroAleatorio);
		Task<Model.core.NumeroAleatorio> AgregarAsync(Model.core.NumeroAleatorio numeroAleatorio);
		Task<Model.core.NumeroAleatorio> EliminarAsync(Model.core.NumeroAleatorio numeroAleatorio);
		Task<IEnumerable<Model.core.NumeroAleatorio>> ObtenerAsync(Expression<Func<Model.core.NumeroAleatorio, bool>> Function = default);

		Task<List<string>> ObtenerNumerosAsync(Expression<Func<Model.core.NumeroAleatorio, bool>> Function = default);

		Task<List<string>> GenerarNumeroAletorio(int valor);
	}
}