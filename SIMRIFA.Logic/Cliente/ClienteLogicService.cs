using SIMRIFA.Service.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Logic.Cliente
{
	public class ClienteLogicService
	{
		private readonly IClienteService _service;

		public ClienteLogicService(IClienteService service)
		{
			_service = service;
		}

		public async Task<SIMRIFA.Model.core.Cliente> AgregarAsync(SIMRIFA.Model.core.Cliente cliente)
		{
			var result = await _service.AgregarAsync(cliente);
			return result;
		}
		public async Task<Model.core.Cliente> ActualizarAsync(Model.core.Cliente cliente)
		{
			Model.core.Cliente data = await _service.ActualizarAsync(cliente);
			return data;
		}

		public async Task<IEnumerable<Model.core.Cliente>> ObtenerAsync(Expression<Func<Model.core.Cliente, bool>> Function)
		{
			var Qry = await _service.ObtenerAsync(Function);
			return Qry;
		}

		public async Task<Model.core.Cliente> EliminarAsync(Model.core.Cliente cliente)
		{
			var result = await _service.EliminarAsync(cliente);
			return result;
		}
	}
}
