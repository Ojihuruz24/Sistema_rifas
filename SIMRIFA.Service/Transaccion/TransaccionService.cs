using SIMRIFA.DataAccess.Db_Context;
using SIMRIFA.DataAccess.Repository;
using SIMRIFA.DataAccess.UnitOfWork;
using SIMRIFA.Model.core;
using SIMRIFA.Model.ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
				var result = await _repository.Add(transaccion);
				return result;
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
				var newlist = new List<TransaccionDto>();

				var result = await _repository.GetOneOrAll(Funcion);

				if (result != null)
				{
					newlist.AddRange(result);
				}

				return newlist;
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
				var newlist = new List<string>();

				var result = await _repository.GetOneOrAll();

				if (result != null)
				{
					newlist.AddRange(result.Select(x => x.Referencia).ToList());
				}

				return newlist;
			}
			catch (Exception ex)
			{
				throw;
			}
		}

	}
}
