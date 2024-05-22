using SIMRIFA.Model.ventas;

namespace SIMRIFA.Service.Transaccion
{
	public interface ITransaccionService
	{
		Task<TransaccionDto> AgregarTransaccion(TransaccionDto transaccion);
	}
}