using SIMRIFA.Model.ventas;
using System.Linq.Expressions;

namespace SIMRIFA.Service.Transaccion
{
	public interface ITransaccionService
	{
		Task<TransaccionDto> AgregarTransaccion(TransaccionDto transaccion);

		Task<IEnumerable<TransaccionDto>> ObtenerTransacciones(Expression<Func<TransaccionDto, bool>> Funcion = default);

		Task<IEnumerable<string>> ObtenerReferencias();
	}
}