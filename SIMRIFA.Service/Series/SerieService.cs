using SIMRIFA.DataAccess.Db_Context;
using SIMRIFA.DataAccess.Repository;
using SIMRIFA.DataAccess.UnitOfWork;
using SIMRIFA.Model.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Service.Series
{
	public class SerieService : ISerieService
	{
		private readonly IRepository<SIMRIFA.Model.core.Serie> _repository;

		public SerieService(IRepository<SIMRIFA.Model.core.Serie> repository)
		{
			_repository = repository;
		}

		public async Task<Serie> AddSerie(Serie serie)
		{
			var result = await _repository.Add(serie);
			return result;
		}

		public async Task<IEnumerable<Serie>> ObtenerSerieActiva()
		{
			var data = await _repository.GetOneOrAll(x => x.Estado == true);
			return data;
		}

		public async Task<Serie> ActualizarSerie(Serie serie)
		{
			var data = await _repository.Update(serie);
			return data;

		}

	}
}
