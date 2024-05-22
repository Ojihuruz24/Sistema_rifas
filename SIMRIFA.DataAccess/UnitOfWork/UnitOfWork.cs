using Microsoft.EntityFrameworkCore;
using SIMRIFA.DataAccess.Db_Context;
using SIMRIFA.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.DataAccess.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		//private readonly SIMRIFAdbContext _context;
		//private Dictionary<Type, object> _repositories;

		public SIMRIFAdbContext _context { get; }

		public UnitOfWork(SIMRIFAdbContext context)
		{
			_context = context;
			//_repositories = new Dictionary<Type, object>();
		}

		public async Task<int> Commit()
		{
			var result = await _context.SaveChangesAsync();

			return result;
		}

		public async Task Rollback()
		{
			await _context.Database.BeginTransactionAsync();
		}

		//public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
		//{
		//	if (!_repositories.ContainsKey(typeof(TEntity)))
		//	{
		//		_repositories[typeof(TEntity)] = new Repository<TEntity>(_context);
		//	}
		//	return (IRepository<TEntity>)_repositories[typeof(TEntity)];
		//}
	}
}
