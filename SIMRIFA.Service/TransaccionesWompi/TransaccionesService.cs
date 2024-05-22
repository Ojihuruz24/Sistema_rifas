using SIMRIFA.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Service.TransaccionesWompi
{
	public class TransaccionesService : ITransaccionesService
	{
		private readonly IRepository<SIMRIFA.Model.Models.Wompi.InfoTransaccionDto> _repository;

		public TransaccionesService(IRepository<SIMRIFA.Model.Models.Wompi.InfoTransaccionDto> repository)
		{
			_repository = repository;
		}


		public async Task<SIMRIFA.Model.Models.Wompi.InfoTransaccionDto> AgregarAsync(SIMRIFA.Model.Models.Wompi.InfoTransaccionDto numeroAleatorio)
		{
			var result = await _repository.Add(numeroAleatorio);
			return result;
		}

		public async Task<SIMRIFA.Model.Models.Wompi.InfoTransaccionDto> ActualizarAsync(SIMRIFA.Model.Models.Wompi.InfoTransaccionDto numeroAleatorio)
		{

			SIMRIFA.Model.Models.Wompi.InfoTransaccionDto data = await _repository.Update(numeroAleatorio);
			return data;
		}

		public async Task<IEnumerable<SIMRIFA.Model.Models.Wompi.InfoTransaccionDto>> ObtenerAsync(Expression<Func<SIMRIFA.Model.Models.Wompi.InfoTransaccionDto, bool>> Function)
		{
			var Qry = await _repository.GetOneOrAll(Function);
			return Qry;
		}

		public async Task<SIMRIFA.Model.Models.Wompi.InfoTransaccionDto> EliminarAsync(SIMRIFA.Model.Models.Wompi.InfoTransaccionDto numeroAleatorio)
		{
			var result = await _repository.Delete(numeroAleatorio);
			return result;

		}
	}
}
