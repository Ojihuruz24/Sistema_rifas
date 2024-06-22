using SIMRIFA.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Logic.TipoIdentificacion
{
	public class TipoIdentificacionService : ITipoIdentificacionService
	{
		private readonly IRepository<SIMRIFA.Model.core.TipoIdentificacion> _repository;

		public TipoIdentificacionService(IRepository<SIMRIFA.Model.core.TipoIdentificacion> repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<Model.core.TipoIdentificacion>> ObtenerAsync(Expression<Func<Model.core.TipoIdentificacion, bool>> Function = default)
		{
			var Qry = await _repository.GetOneOrAll(Function);
			return Qry;
		}

		public async Task<Model.core.TipoIdentificacion> ActualizarAsync(Model.core.TipoIdentificacion tipoIdentificacion)
		{
			Model.core.TipoIdentificacion data = await _repository.Update(tipoIdentificacion);
			return data;
		}


		public async Task<Model.core.TipoIdentificacion> EliminarAsync(Model.core.TipoIdentificacion tipoIdentificacion)
		{
			var result = await _repository.Delete(tipoIdentificacion);
			return result;
		}

		public async Task<Model.core.TipoIdentificacion> AgregarAsync(Model.core.TipoIdentificacion tipoIdentificacion)
		{
			var result = await _repository.Add(tipoIdentificacion);
			return result;
		}


	}
}
