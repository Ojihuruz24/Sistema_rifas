using SIMRIFA.Model.Models;

namespace SIMRIFA.Service.Series
{
	public interface ISerieService
	{
		void AddSerie(Serie serie);
		void ActualizarSerie(Serie serie);
		IEnumerable<Serie> ObtenerSerieActiva();
	}
}