using SIMRIFA.DataAccess.Repository.MunicipioRepo;
using SIMRIFA.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Logic.TipoCliente
{
	public class TipoClienteService : ITipoClienteService
	{
		private readonly IRepository<SIMRIFA.Model.core.TipoCliente> _repository;

		public TipoClienteService(IRepository<SIMRIFA.Model.core.TipoCliente> repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<Model.core.TipoCliente>> ObtenerAsync(Expression<Func<Model.core.TipoCliente, bool>> Function = default)
		{
			var Qry = await _repository.GetOneOrAll(Function);
			return Qry;
		}

		public async Task<Model.core.TipoCliente> ActualizarAsync(Model.core.TipoCliente tipoCliente)
		{
			Model.core.TipoCliente data = await _repository.Update(tipoCliente);
			return data;
		}


		public async Task<Model.core.TipoCliente> EliminarAsync(Model.core.TipoCliente tipoCliente)
		{
			var result = await _repository.Delete(tipoCliente);
			return result;
		}

		public async Task<Model.core.TipoCliente> AgregarAsync(Model.core.TipoCliente tipoCliente)
		{
			var result = await _repository.Add(tipoCliente);
			return result;
		}
	}
}
