
using System.Text.Json;

namespace SIMRIFA.Tools.Autenticacion
{
	public class IsDataNull
	{
		public static T Check<T>(string data)
		{
			return data == null ? default : JsonSerializer.Deserialize<T>(data);
		}

		public static T CreateInstanceIsNull<T>(T data) where T : new()
		{
			return data == null ? new T() : data;
		}

	}
}
