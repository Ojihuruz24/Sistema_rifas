using System.Linq.Expressions;

namespace SIMRIFA.Logic.TipoIdentificacion
{
	public interface ITipoIdentificacionService
	{
		Task<Model.core.TipoIdentificacion> ActualizarAsync(Model.core.TipoIdentificacion tipoIdentificacion);
		Task<Model.core.TipoIdentificacion> AgregarAsync(Model.core.TipoIdentificacion tipoIdentificacion);
		Task<Model.core.TipoIdentificacion> EliminarAsync(Model.core.TipoIdentificacion tipoIdentificacion);
		Task<IEnumerable<Model.core.TipoIdentificacion>> ObtenerAsync(Expression<Func<Model.core.TipoIdentificacion, bool>> Function = default);
	}
}