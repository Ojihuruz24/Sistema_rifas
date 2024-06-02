using SIMRIFA.Model.core;
using System.Linq.Expressions;

namespace SIMRIFA.Logic.ClienteLogic
{
	public interface IClienteLogicService
	{
		Task<Cliente> ActualizarAsync(Cliente cliente);
		Task<Cliente> AgregarAsync(Cliente cliente);
		Task<Cliente> EliminarAsync(Cliente cliente);
		Task<IEnumerable<Cliente>> ObtenerAsync(Expression<Func<Cliente, bool>> Function);
	}
}