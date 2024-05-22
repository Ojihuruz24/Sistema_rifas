using Microsoft.EntityFrameworkCore;
using SIMRIFA.DataAccess.Repository;
using SIMRIFA.DataAccess.UnitOfWork;
using System.Linq.Expressions;

namespace SIMRIFA.Service.Cliente
{
	public class ClienteService : IClienteService
	{

		private readonly IRepository<SIMRIFA.Model.core.Cliente> _repository;

		public ClienteService(IRepository<SIMRIFA.Model.core.Cliente> repository)
		{
			_repository = repository;
		}


		public async Task<SIMRIFA.Model.core.Cliente> AgregarAsync(SIMRIFA.Model.core.Cliente cliente)
		{
			var result = await _repository.Add(cliente);
			return result;
		}

		public async Task<Model.core.Cliente> ActualizarAsync(Model.core.Cliente cliente)
		{

			Model.core.Cliente data = await _repository.Update(cliente);
			return data;
		}

		public async Task<IEnumerable<Model.core.Cliente>> ObtenerAsync(Expression<Func<Model.core.Cliente, bool>> Function)
		{
			var Qry = await _repository.GetOneOrAll(Function);
			return Qry;
		}

		public async Task<Model.core.Cliente> EliminarAsync(Model.core.Cliente cliente)
		{
			var result = await _repository.Delete(cliente);
			return result;



			//IEnumerable<SIMRIFA.Model.core.Cliente> Resultado = default;
			//try
			//{
			//	var Qry = await _repository.GetOneOrAll(Function);

			//	return Qry;

			//	//if (Function != default)
			//	//{
			//	//	Qry = Qry.Where(Function);
			//	//}
			//	//Resultado = await Qry.ToListAsync();
			//}
			//catch (Exception ex)
			//{

			//}
			//return Resultado;
		}
	}
}
