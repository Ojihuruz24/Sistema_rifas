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

		public void Commit()
		{
			_context.SaveChanges();
		}

		public void Rollback()
		{
			_context.Dispose();
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
