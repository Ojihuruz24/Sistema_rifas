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
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		private readonly SIMRIFAdbContext _dbcontext;
		private readonly IUnitOfWork _unitOfWork;
		internal DbSet<TEntity> _dbSet;

		public Repository(IUnitOfWork unitOfWork, SIMRIFAdbContext dbcontext)
		{
			_unitOfWork = unitOfWork;
			_dbcontext = dbcontext;
			_dbSet = _dbcontext.Set<TEntity>();
		}

		public async Task<TEntity> Add(TEntity entity)
		{
			try
			{
				//var result = await _dbSet.AddAsync(entity);
				var result = await _dbSet.AddAsync(entity);
				result.State = EntityState.Added;
				return result.Entity;
			}
			catch (Exception ex)
			{
                await Console.Out.WriteLineAsync(ex.Message);
				throw;
			}
		}

		public async Task<TEntity> Update(TEntity entity)
		{
			var result = await Task.Run(() => _dbcontext.Update(entity));
			result.State = EntityState.Modified;
			return result.Entity;
		}

		public async Task<TEntity> Delete(TEntity entity)
		{
			var result = await Task.Run(() => _dbSet.Remove(entity));

			result.State = EntityState.Deleted;
			return result.Entity;
		}

		public async Task<IEnumerable<TEntity>> GetOneOrAll(Expression<Func<TEntity, bool>> Funcion = default)
		{
			IQueryable<TEntity> Resultado = default;
			try
			{
				var Qry = _dbSet.AsQueryable();

				if (Funcion != default)
				{
					Qry = Qry.Where(Funcion);
					Resultado = Qry;
				}

				Resultado = Qry;
			}
			catch (Exception ex)
			{

			}
			return Resultado;
		}
	}
}
