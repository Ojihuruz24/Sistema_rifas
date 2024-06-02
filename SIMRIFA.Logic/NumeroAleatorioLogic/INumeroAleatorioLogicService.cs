using System.Linq.Expressions;

namespace SIMRIFA.Logic.NumeroAleatorioLogic
{
	public interface INumeroAleatorioLogicService
	{
		Task<Model.core.NumeroAleatorio> ActualizarAsync(Model.core.NumeroAleatorio numeroAleatorio);
		Task<Model.core.NumeroAleatorio> AgregarAsync(Model.core.NumeroAleatorio numeroAleatorio);
		Task<Model.core.NumeroAleatorio> EliminarAsync(Model.core.NumeroAleatorio numeroAleatorio);
		Task<List<string>> GenerarNumeroAletorio(int valor, int valorMaximo);
		Task<IEnumerable<Model.core.NumeroAleatorio>> ObtenerAsync(Expression<Func<Model.core.NumeroAleatorio, bool>> Function = null);
	}
}