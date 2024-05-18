using Microsoft.EntityFrameworkCore;
using SIMRIFA.DataAccess.Db_Context;
using SIMRIFA.DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.DataAccess.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		//private readonly SIMRIFAdbContext _dbcontext;
		private readonly IUnitOfWork _unitOfWork;

		public Repository(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public void Add(T entity) => _unitOfWork._context.Add(entity);

		public void Update(T entity)
		{
			_unitOfWork._context.Attach(entity).State = EntityState.Modified;
		}

		public void Delete(T entity)
		{
			_unitOfWork._context.Remove(entity);
		}

		public async Task<IEnumerable<T>> GetOneOrAll(Expression<Func<T, bool>> Funcion = default)
		{
			IQueryable<T> Resultado = _unitOfWork._context.Set<T>();
			try
			{
				if (Funcion != default)
				{
					Resultado = Resultado.Where(Funcion);

					return await Resultado.ToListAsync<T>();
				}
			}
			catch (Exception ex)
			{

			}
			return Resultado;
		}
	}
}
