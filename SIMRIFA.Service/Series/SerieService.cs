using SIMRIFA.DataAccess.Db_Context;
using SIMRIFA.DataAccess.Repository;
using SIMRIFA.DataAccess.UnitOfWork;
using SIMRIFA.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Service.Series
{
	public class SerieService : ISerieService
	{
		private readonly SIMRIFAdbContext _context;
		private readonly IUnitOfWork _IUnitOfWork;

		public SerieService(SIMRIFAdbContext dbContext, IUnitOfWork unitOfWork)
		{
			_context = dbContext;
			_IUnitOfWork = unitOfWork;
		}


		public void AddSerie(Serie serie)
		{
			try
			{
				_IUnitOfWork._context.Add(serie);
				_IUnitOfWork.Commit();
			}
			catch (Exception ex)
			{
				_IUnitOfWork.Rollback();
				throw;
			}
			finally
			{
				_context.Dispose();
			}
		}

		public IEnumerable<Serie> ObtenerSerieActiva()
		{
			//return await _IUnitOfWork._context.Find()  .GetOneOrAll(x => x.Estado == true);
			return  _IUnitOfWork._context.Set<Serie>().Where(x => x.Estado == true).ToList();
		}

		public void ActualizarSerie(Serie serie)
		{
			try
			{
				_IUnitOfWork._context.Update(serie);
				_IUnitOfWork.Commit();
			}
			catch (Exception ex)
			{
				_IUnitOfWork.Rollback();
			}
			finally
			{
				_context.Dispose();
			}
		}

	}
}
