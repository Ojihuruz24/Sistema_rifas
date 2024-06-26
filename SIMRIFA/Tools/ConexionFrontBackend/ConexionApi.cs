using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;
using NuGet.Protocol;
using SIMRIFA.Tools.Autenticacion;
using Newtonsoft.Json.Linq;

namespace SIMRIFA.Tools.ConexionFrontBackend
{
	public class ConexionApi<TEntity, TObject> : IConexionApi<TEntity, TObject> where TEntity : class where TObject : class
	{

		private readonly ILocalStorage _localStorage;
		private readonly HttpClient _httpClient;
		private readonly IConfiguration _configuration;

		public ConexionApi(HttpClient httpClient, IConfiguration configuration, ILocalStorage localStorage)
		{
			_httpClient = httpClient;
			_configuration = configuration;
			_localStorage = localStorage;
		}

		public async Task<TEntity> Delete(string url, TObject model)
		{
			try
			{
				TEntity Resultado;

				//string token = await _localStorage.GetItemAsync<string>("jwtToken");

				string token = await _localStorage.GetValue<string>(Enum.ValuesKeys.Session);

				var httpContent = new HttpRequestMessage(
				 HttpMethod.Delete, $"{GetUrl()}{url}");
				_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				//_httpClient.Timeout = TimeSpan.FromMinutes(1);

				string strJSon = JsonSerializer.Serialize(model);
				httpContent.Content = new StringContent(strJSon, Encoding.UTF8, "application/json");
				var response = await _httpClient.SendAsync(httpContent);
				if (response.IsSuccessStatusCode)
				{
					using var responseStream = await response.Content.ReadAsStreamAsync();
					var options = new JsonSerializerOptions();
					options.Converters.Add(new JsonStringEnumConverter());
					Resultado = await JsonSerializer.DeserializeAsync<TEntity>(responseStream, options);
					return await Task.FromResult(Resultado);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return null;
		}

		public async Task<IEnumerable<TEntity>> GetAll(string url)
		{
			try
			{
				IEnumerable<TEntity> Resultado;

				string token = await _localStorage.GetValue<string>(Enum.ValuesKeys.Session);

				var httpContent = new HttpRequestMessage(
				 HttpMethod.Get, $"{GetUrl()}{url}");
				_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				//_httpClient.Timeout = TimeSpan.FromMinutes(1);
				HttpResponseMessage response = await _httpClient.SendAsync(httpContent);

				var contenido = response.Content.ReadAsStringAsync();

				var contenido2 = response.Content.ToJson();


				if (response.IsSuccessStatusCode)
				{
					using var responseStream = await response.Content.ReadAsStreamAsync();

					var options = new JsonSerializerOptions();
					options.Converters.Add(new JsonStringEnumConverter());

					Resultado = await JsonSerializer.DeserializeAsync<IEnumerable<TEntity>>(responseStream, options);
					return await Task.FromResult(Resultado);
				}
				return null;
			}
			catch (Exception ex)
			{
				//_logger.LogError(ex, "Error al seleccionar");
				//throw ex;
				return null;
			}
		}

		public async Task<TEntity> GetByItem(string url)
		{
			try
			{
				TEntity Resultado;
				string token = await _localStorage.GetValue<string>(Enum.ValuesKeys.Session);
				var httpContent = new HttpRequestMessage(
				 HttpMethod.Get, $"{GetUrl()}{url}");
				_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				//_httpClient.Timeout = TimeSpan.FromMinutes(1);
				var response = await _httpClient.SendAsync(httpContent);


				if (response.IsSuccessStatusCode)
				{
					using var responseStream = await response.Content.ReadAsStreamAsync();
					var options = new JsonSerializerOptions();
					options.Converters.Add(new JsonStringEnumConverter());
					if (responseStream != null)
					{
						Resultado = await JsonSerializer.DeserializeAsync<TEntity>(responseStream, options);
						return await Task.FromResult(Resultado);
					}
					return null;
				}
			}
			catch (Exception ex)
			{
				//_logger.LogError(ex, "Error al seleccionar");
				//throw ex;
				return null;
			}
			return null;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="url"></param>
		/// <param name="model"></param>
		/// <returns></returns>
		public async Task<TEntity> Post(string url, TObject model)
		{
			try
			{
				TEntity Resultado;
				string token = await _localStorage.GetValue<string>(Enum.ValuesKeys.Session);
				var httpContent = new HttpRequestMessage(
				 HttpMethod.Post, $"{GetUrl()}{url}");
				_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

				string strJSon = JsonSerializer.Serialize(model);
				httpContent.Content = new StringContent(strJSon, Encoding.UTF8, "application/json");
				var response = await _httpClient.SendAsync(httpContent);
				var responseStream = await response.Content.ReadAsStreamAsync();
				var options = new JsonSerializerOptions();
				options.Converters.Add(new JsonStringEnumConverter());
				switch (response.StatusCode)
				{

					case System.Net.HttpStatusCode.OK:
						Resultado = await JsonSerializer.DeserializeAsync<TEntity>(responseStream, options);
						return await Task.FromResult(Resultado);
						break;
					case System.Net.HttpStatusCode.Created:
						throw new Exception("Se creo Correctamente!");
						break;
					case System.Net.HttpStatusCode.NoContent:
						throw new Exception("Se realizo mal la peticion!");
						break;
					case System.Net.HttpStatusCode.BadRequest:
						var error = await JsonSerializer.DeserializeAsync<ResponseApi>(responseStream, options);
						throw new Exception(error.Error);
						break;
					case System.Net.HttpStatusCode.Unauthorized:
						throw new Exception("No esta Autorizado");
						break;
					case System.Net.HttpStatusCode.Forbidden:
						throw new Exception("NO tiene permiso!");
						break;
					case System.Net.HttpStatusCode.NotFound:
						throw new Exception("No se Encontro ningun contenido!");
						break;
					case System.Net.HttpStatusCode.InternalServerError:
						throw new Exception("Se ha presentado un error en el servidor!");
						break;
					default:
						throw new Exception("Se ha presentado un error en el servidor!");
						break;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Metodo Generico para poder descargar cualquier archivo ejemplo .PDF, .EXCEl, PNG entre otros
		/// </summary>
		/// <param name="url"></param> Ruta de la api controlador
		/// <param name="model"></param> Objeto modelo que envia al controlador -> Parametros 
		/// <param name="nombre"></param> Nombre del archivo con el cual quiere descargar el archivo
		/// <param name="rutaParcial"></param> Ruta de donde desea guardar el archivo -> nombre carpeta
		/// <returns>Devuelve la ruta de donde se encuentra generado el archivo </returns>
		#region PostFile 
		public async Task<string> PostFile(string url, TObject model, string nombre, string rutaParcial)
		{
			try
			{
				string token = await _localStorage.GetValue<string>(Enum.ValuesKeys.Session);
				var httpContent = new HttpRequestMessage(
				 HttpMethod.Post, $"{GetUrl()}{url}");
				_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				//_httpClient.Timeout = TimeSpan.FromMinutes(1);
				string strJSon = JsonSerializer.Serialize(model);
				httpContent.Content = new StringContent(strJSon, Encoding.UTF8, "application/json");
				var response = await _httpClient.SendAsync(httpContent);
				if (response.IsSuccessStatusCode)
				{
					//Sacar la extensión del archivo que se quiere descargar
					string Ext = Path.GetExtension(response.Content.Headers.ContentDisposition.FileNameStar.ToString());
					//Generar la ruta generica wwwroot
					string directory = Directory.GetCurrentDirectory() + _configuration.GetValue<string>("RutaArchivoHC");
					//Validar si existe la carpeta "rutaParcial" en wwwroot de no existir se crea
					if (!Directory.Exists(directory + rutaParcial))
					{
						Directory.CreateDirectory(directory + rutaParcial);
					}
					//Ruta relatica de donde va el archivo a generarse
					string relativa = $"{rutaParcial}\\{nombre}-{DateTime.Now.ToString("yyyyMMddHHmm")}." + Ext.Trim();
					//Unión de las dos rutas -> Generica(wwwroot) y relativa (seleccion)
					string Ruta = directory + relativa;
					//Obtener la respuesta de la api
					var responseStream = await response.Content.ReadAsByteArrayAsync();
					//CrearAtencionFinal el archivo
					File.WriteAllBytes(Ruta, responseStream);
					//Retornar la ruta donde deseo generar el archivo.
					return relativa;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return null;
		}
		#endregion
		public async Task<IEnumerable<TEntity>> PostAll(string url, TObject model)
		{
			try
			{
				IEnumerable<TEntity> Resultado;
				string token = await _localStorage.GetValue<string>(Enum.ValuesKeys.Session);
				var httpContent = new HttpRequestMessage(
				 HttpMethod.Post, $"{GetUrl()}{url}");
				_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				//_httpClient.Timeout = TimeSpan.FromMinutes(1);
				string strJSon = JsonSerializer.Serialize(model);
				httpContent.Content = new StringContent(strJSon, Encoding.UTF8, "application/json");
				var response = await _httpClient.SendAsync(httpContent);
				var responseStream = await response.Content.ReadAsStreamAsync();
				var options = new JsonSerializerOptions();
				options.Converters.Add(new JsonStringEnumConverter());

				switch (response.StatusCode)
				{

					case System.Net.HttpStatusCode.OK:

						Resultado = await JsonSerializer.DeserializeAsync<IEnumerable<TEntity>>(responseStream, options);
						return await Task.FromResult(Resultado);
						break;
					case System.Net.HttpStatusCode.Created:
						throw new Exception("Se creo Correctamente!");
						break;
					case System.Net.HttpStatusCode.NoContent:
						throw new Exception("Se creo Correctamente!");
						break;
					case System.Net.HttpStatusCode.BadRequest:
						throw new Exception("Se ha presentado un error!");
						break;
					case System.Net.HttpStatusCode.Unauthorized:
						throw new Exception("No esta Autorizado");
						break;
					case System.Net.HttpStatusCode.Forbidden:
						throw new Exception("No tiene permiso!");
						break;
					case System.Net.HttpStatusCode.NotFound:
						throw new Exception("No se Encontro ningun contenido!");
						break;
					case System.Net.HttpStatusCode.InternalServerError:
						throw new Exception("Se ha presentado un error en el servidor!");
						break;
					default:
						throw new Exception("Se ha presentado un error en el servidor!");
						break;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<TEntity> Put(string url, TObject model)
		{
			try
			{
				TEntity Resultado;
				string token = await _localStorage.GetValue<string>(Enum.ValuesKeys.Session);
				var httpContent = new HttpRequestMessage(
				 HttpMethod.Put, $"{GetUrl()}{url}");
				_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				//_httpClient.Timeout = TimeSpan.FromMinutes(1);
				string strJSon = JsonSerializer.Serialize(model);
				httpContent.Content = new StringContent(strJSon, Encoding.UTF8, "application/json");
				var response = await _httpClient.SendAsync(httpContent);
				using var responseStream = await response.Content.ReadAsStreamAsync();
				var options = new JsonSerializerOptions();
				options.Converters.Add(new JsonStringEnumConverter());

				switch (response.StatusCode)
				{
					case System.Net.HttpStatusCode.OK:

						Resultado = await JsonSerializer.DeserializeAsync<TEntity>(responseStream, options);
						return await Task.FromResult(Resultado);
						break;
					case System.Net.HttpStatusCode.Created:
						throw new Exception("Se creo Correctamente!");
						break;
					case System.Net.HttpStatusCode.NoContent:
						throw new Exception("Se creo Correctamente!");
						break;
					case System.Net.HttpStatusCode.BadRequest:
						var error = await JsonSerializer.DeserializeAsync<ResponseApi>(responseStream, options);
						throw new Exception(error.Error);
						break;
					case System.Net.HttpStatusCode.Unauthorized:
						throw new Exception("No esta Autorizado");
						break;
					case System.Net.HttpStatusCode.Forbidden:
						throw new Exception("NO tiene permiso!");
						break;
					case System.Net.HttpStatusCode.NotFound:
						throw new Exception("No se Encontro ningun contenido!");
						break;
					case System.Net.HttpStatusCode.InternalServerError:
						throw new Exception("Se ha presentado un error en el servidor!");
						break;
					default:
						throw new Exception("Se ha presentado un error en el servidor!");
						break;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private string GetUrl()
		{
			var url = _configuration.GetValue<string>("Integracion:ApiOsdagupO");

			return url;
		}

		public async Task<HttpResponseMessage> GetResponse(string url)
		{
			try
			{

				string token = await _localStorage.GetValue<string>(Enum.ValuesKeys.Session);
				var httpContent = new HttpRequestMessage(
				 HttpMethod.Get, $"{GetUrl()}{url}");
				_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				//_httpClient.Timeout = TimeSpan.FromMinutes(1);
				var response = await _httpClient.SendAsync(httpContent);


				if (response.IsSuccessStatusCode)
				{

					return await Task.FromResult(response);

				}
			}
			catch (Exception ex)
			{
				//_logger.LogError(ex, "Error al seleccionar");
				//throw ex;
				return null;
			}
			return null;
		}
	}


	public class ResponseApi
	{
		public string? Error { get; set; }
		public System.Net.HttpStatusCode? Code { get; set; }
		public string? DataError { get; set; }
	}
}
