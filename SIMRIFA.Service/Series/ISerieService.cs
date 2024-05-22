using SIMRIFA.Model.core;

namespace SIMRIFA.Service.Series
{
	public interface ISerieService
	{
		Task<Serie> AddSerie(Serie serie);
		Task<Serie> ActualizarSerie(Serie serie);
		Task<IEnumerable<Serie>> ObtenerSerieActiva();
	}
}