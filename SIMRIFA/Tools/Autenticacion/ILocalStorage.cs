using SIMRIFA.Enum;

namespace SIMRIFA.Tools.Autenticacion
{
	public interface ILocalStorage
	{
		/// <summary>
		/// Obtiene un valor  que se encuentra almacenado en la memoria local
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="valuesKeys"></param>
		/// <returns></returns>
		Task<T> GetValue<T>(ValuesKeys valuesKeys);

		/// <summary>
		/// Guarda un valor  que se encuentra almacenado en la memoria local
		/// y lo serializa  al tipo de dato especifico
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="valuesKeys"></param>
		/// <returns></returns>
		Task SetValue<T>(ValuesKeys key, T value);


		/// <summary>
		/// Remueve de memoria un elemento especifico
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		Task RemoveItem(ValuesKeys key);

		/// <summary>
		/// Elimina todos los datos almacenando en memoria local
		/// </summary>
		/// <returns></returns>
		Task ClearAll();

	}
}
