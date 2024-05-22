using SIMRIFA.DataAccess.Db_Context;
using SIMRIFA.DataAccess.Repository;
using SIMRIFA.DataAccess.UnitOfWork;
using SIMRIFA.Model.core;
using SIMRIFA.Model.ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Service.Transaccion
{
	public class TransaccionService : ITransaccionService
	{
		private readonly SIMRIFAdbContext _context;
		private readonly IUnitOfWork _IUnitOfWork;
		private readonly IRepository<TransaccionDto> _repository;
		public TransaccionService(SIMRIFAdbContext context, IUnitOfWork iUnitOfWork, IRepository<TransaccionDto> repository)
		{
			_context = context;
			_IUnitOfWork = iUnitOfWork;
			_repository = repository;
		}

		public async Task<TransaccionDto> AgregarTransaccion(TransaccionDto transaccion)
		{
			try
			{
				//var result = _IUnitOfWork._context.Transaccion.Update(transaccion);
				var result = await _repository.Add(transaccion);
				//await _IUnitOfWork.Commit();
				return result;
			}
			catch (Exception ex)
			{
				//_IUnitOfWork.Rollback();
				throw;
			}
			finally
			{
				//_IUnitOfWork._context.Dispose();
			}
		}

	}
}
