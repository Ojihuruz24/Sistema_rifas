using SIMRIFA.Model.core;

namespace SIMRIFA.Service.Compra
{
	public interface ICompradorService
	{
		Task<List<string>> AddComprador(int cantidad, string precio);

		Task<Serie> SerieTest(Serie serie);
	}
}