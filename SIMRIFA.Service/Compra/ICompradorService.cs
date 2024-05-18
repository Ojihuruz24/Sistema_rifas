using SIMRIFA.Model.Models;

namespace SIMRIFA.Service.Compra
{
	public interface ICompradorService
	{
		Task<List<string>> AddComprador(int cantidad, string precio);
	}
}