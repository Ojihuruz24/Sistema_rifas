using SIMRIFA.Model.core;
using System.Linq.Expressions;

namespace SIMRIFA.DataAccess.Repository.MunicipioRepo
{
    public interface IMunicipioRepositorio
    {
        Task<IEnumerable<Municipio>> ObtenerAsync(Expression<Func<Municipio, bool>> Function = null);
    }
}