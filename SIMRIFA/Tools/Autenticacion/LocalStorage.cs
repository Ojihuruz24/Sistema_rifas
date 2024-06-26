using Microsoft.JSInterop;
using SIMRIFA.Enum;
using System.Text.Json;

namespace SIMRIFA.Tools.Autenticacion
{
	public class LocalStorage : ILocalStorage
	{
		private readonly IJSRuntime _jSRuntime;
		private readonly string _SessionStorage = "sessionStorage.";
		private readonly string _localStorage = "localStorage.";
		private readonly ProteccionDato _proteccion;

		public LocalStorage(IJSRuntime jSRuntime, ProteccionDato proteccion)
		{
			_jSRuntime = jSRuntime;
			_proteccion = proteccion;
		}

		public async Task ClearAll()
		{
			await _jSRuntime.InvokeVoidAsync($"{_SessionStorage}clear").ConfigureAwait(false);
		}

		public async Task<T> GetValue<T>(ValuesKeys valuesKeys)
		{
			//string data = await _jSRuntime.InvokeAsync<string>($"{_tipoAlmacenamiento}getItem", valuesKeys.ToString()).ConfigureAwait(false);
			string data = await _jSRuntime.InvokeAsync<string>($"{_SessionStorage}getItem",valuesKeys.ToString()).ConfigureAwait(false);
			string desproteger = _proteccion.Desproteger(data);

			return IsDataNull.Check<T>(desproteger);
		}

		public async Task RemoveItem(ValuesKeys key)
		{
			await _jSRuntime.InvokeVoidAsync($"{_SessionStorage}removeItem", key.ToString()).ConfigureAwait(false);
		}

		public async Task SetValue<T>(ValuesKeys key, T value)
		{
			//await _jSRuntime.InvokeVoidAsync($"{_tipoAlmacenamiento}setItem", key.ToString(), JsonSerializer.Serialize(value)).ConfigureAwait(false);
			await _jSRuntime.InvokeVoidAsync($"{_SessionStorage}setItem", key.ToString(), _proteccion.Proteger(JsonSerializer.Serialize(value))).ConfigureAwait(false);
		}
	}
}
