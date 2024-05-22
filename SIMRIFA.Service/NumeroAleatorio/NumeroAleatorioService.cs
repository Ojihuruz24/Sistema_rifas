using Microsoft.EntityFrameworkCore;
using SIMRIFA.DataAccess.Repository;
using SIMRIFA.Model.core;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace SIMRIFA.Service.NumeroAleatorio
{
	public class NumeroAleatorioService : INumeroAleatorioService
	{
		private readonly IRepository<SIMRIFA.Model.core.NumeroAleatorio> _repository;

		public NumeroAleatorioService(IRepository<SIMRIFA.Model.core.NumeroAleatorio> repository)
		{
			_repository = repository;
		}


		public async Task<SIMRIFA.Model.core.NumeroAleatorio> AgregarAsync(SIMRIFA.Model.core.NumeroAleatorio numeroAleatorio)
		{
			var result = await _repository.Add(numeroAleatorio);
			return result;
		}

		public async Task<Model.core.NumeroAleatorio> ActualizarAsync(Model.core.NumeroAleatorio numeroAleatorio)
		{

			Model.core.NumeroAleatorio data = await _repository.Update(numeroAleatorio);
			return data;
		}

		public async Task<IEnumerable<Model.core.NumeroAleatorio>> ObtenerAsync(Expression<Func<Model.core.NumeroAleatorio, bool>> Function)
		{
			var Qry = await _repository.GetOneOrAll(Function);
			return Qry;
		}

		public async Task<Model.core.NumeroAleatorio> EliminarAsync(Model.core.NumeroAleatorio numeroAleatorio)
		{
			var result = await _repository.Delete(numeroAleatorio);
			return result;

		}
	}
}
