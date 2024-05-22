using SIMRIFA.DataAccess.Db_Context;
using SIMRIFA.DataAccess.Repository;

namespace SIMRIFA.DataAccess.UnitOfWork
{
	public interface IUnitOfWork
	{
		SIMRIFAdbContext _context { get; }
		Task<int> Commit();
		Task Rollback();
	}
}