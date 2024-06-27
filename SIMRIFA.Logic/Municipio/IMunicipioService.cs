using System.Linq.Expressions;

namespace SIMRIFA.Logic.Municipio
{
    public interface IMunicipioService
    {
        Task<Model.core.Municipio> ActualizarAsync(Model.core.Municipio municipio);
        Task<Model.core.Municipio> AgregarAsync(Model.core.Municipio municipio);
        Task<Model.core.Municipio> EliminarAsync(Model.core.Municipio municipio);
        Task<IEnumerable<Model.core.Municipio>> ObtenerAsync(Expression<Func<Model.core.Municipio, bool>> Function = null);
        Task<IEnumerable<Model.core.Municipio>> ObtenerCrtierioAsync(Expression<Func<Model.core.Municipio, bool>> Function = default);

	}
}