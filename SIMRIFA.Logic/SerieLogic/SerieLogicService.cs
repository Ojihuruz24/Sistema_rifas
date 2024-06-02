using SIMRIFA.Service.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMRIFA.Model.core; 

namespace SIMRIFA.Logic.SerieLogic
{
	public class SerieLogicService : ISerieLogicService
	{
		private readonly ISerieService _serieService;

		public SerieLogicService(ISerieService serieService)
		{
			_serieService = serieService;
		}

		public async Task<Serie> AddSerie(Serie serie)
		{
			return await _serieService.AddSerie(serie);
		}

		public async Task<IEnumerable<Serie>> ObtenerSerieActiva()
		{
			return await _serieService.ObtenerSerieActiva();
		}

		public async Task<Serie> ActualizarSerie(Serie serie)
		{
			return await _serieService.ActualizarSerie(serie);

		}
	}
}
