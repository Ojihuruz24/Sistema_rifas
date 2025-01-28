using System.Linq.Expressions;

namespace SIMRIFA.Logic.TipoCliente
{
	public interface ITipoClienteService
	{
		Task<Model.core.TipoCliente> ActualizarAsync(Model.core.TipoCliente tipoCliente);
		Task<Model.core.TipoCliente> AgregarAsync(Model.core.TipoCliente tipoCliente);
		Task<Model.core.TipoCliente> EliminarAsync(Model.core.TipoCliente tipoCliente);
		Task<IEnumerable<Model.core.TipoCliente>> ObtenerAsync(Expression<Func<Model.core.TipoCliente, bool>> Function = null);
	}
}