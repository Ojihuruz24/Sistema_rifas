using System.Linq.Expressions;

namespace SIMRIFA.Service.Cliente
{
	public interface IClienteService
	{
		Task<SIMRIFA.Model.core.Cliente> AgregarAsync(Model.core.Cliente cliente);
		Task<Model.core.Cliente> ActualizarAsync(Model.core.Cliente cliente);
		Task<IEnumerable<Model.core.Cliente>> ObtenerAsync(Expression<Func<Model.core.Cliente, bool>> Function);
		Task<Model.core.Cliente> EliminarAsync(Model.core.Cliente cliente);
	}
}