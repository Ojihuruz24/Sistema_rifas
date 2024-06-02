using SIMRIFA.Model.ventas;
using System.Linq.Expressions;

namespace SIMRIFA.Logic.Transaccion
{
	public interface ITransaccionLogicService
	{
		Task<TransaccionDto> AgregarTransaccion(TransaccionDto transaccion);
		Task<IEnumerable<string>> ObtenerReferencias();
		Task<IEnumerable<TransaccionDto>> ObtenerTransacciones(Expression<Func<TransaccionDto, bool>> Funcion = null);
	}
}