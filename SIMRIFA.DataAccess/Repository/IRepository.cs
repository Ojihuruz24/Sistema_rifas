using System.Collections.Generic;
using System.Linq.Expressions;

namespace SIMRIFA.DataAccess.Repository
{
	public interface IRepository<T> where T : class
    {
		void Add(T entity);
		void Delete(T entity);
		Task<IEnumerable<T>> GetOneOrAll(Expression<Func<T, bool>> Funcion = default);
		void Update(T entity);
	}
}