using SIMRIFA.Model.core;

namespace SIMRIFA.Logic.SerieLogic
{
	public interface ISerieLogicService
	{
		Task<Serie> ActualizarSerie(Serie serie);
		Task<Serie> AddSerie(Serie serie);
		Task<IEnumerable<Serie>> ObtenerSerieActiva();
	}
}