using SIMRIFA.Model.Models.Wompi;
using System.Linq.Expressions;

namespace SIMRIFA.Service.TransaccionesWompi
{
	public interface ITransaccionesService
	{
		Task<InfoTransaccionDto> ActualizarAsync(InfoTransaccionDto numeroAleatorio);
		Task<InfoTransaccionDto> AgregarAsync(InfoTransaccionDto numeroAleatorio);
		Task<InfoTransaccionDto> EliminarAsync(InfoTransaccionDto numeroAleatorio);
		Task<IEnumerable<InfoTransaccionDto>> ObtenerAsync(Expression<Func<InfoTransaccionDto, bool>> Function);
	}
}