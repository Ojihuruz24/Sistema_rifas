using System.Collections.Generic;
using System.Linq.Expressions;

namespace SIMRIFA.DataAccess.Repository
{
	public interface IRepository<TEntity> where TEntity : class
    {
		Task<TEntity> Add(TEntity entity);
		Task<TEntity> Delete(TEntity entity);
		Task<IEnumerable<TEntity>> GetOneOrAll(Expression<Func<TEntity, bool>> Funcion = default);
		Task<TEntity> Update(TEntity entity);
	}
}