using SIMRIFA.Model.ventas;
using SIMRIFA.Service.Transaccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Logic.Transaccion
{
	public class TransaccionLogicService : ITransaccionLogicService
	{
		private readonly ITransaccionService _transaccionService;

		public TransaccionLogicService(ITransaccionService transaccionService)
		{
			_transaccionService = transaccionService;
		}

		public async Task<TransaccionDto> AgregarTransaccion(TransaccionDto transaccion)
		{
			try
			{
				return await _transaccionService.AgregarTransaccion(transaccion);
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public async Task<IEnumerable<TransaccionDto>> ObtenerTransacciones(Expression<Func<TransaccionDto, bool>> Funcion = default)
		{
			try
			{
				return await _transaccionService.ObtenerTransacciones(Funcion);
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public async Task<IEnumerable<string>> ObtenerReferencias()
		{
			try
			{
				return await _transaccionService.ObtenerReferencias();
			}
			catch (Exception ex)
			{
				throw;
			}
		}

	}
}
