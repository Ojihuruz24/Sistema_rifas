using SIMRIFA.Model.Config;
using SIMRIFA.Model.Models.Wompi;
using System.Text.Json;

namespace SIMRIFA.Service.Tools
{
	public interface IUtils
	{

		Task<CorreoConfig> GetConfiguracionCorreoAsync();
	}
}