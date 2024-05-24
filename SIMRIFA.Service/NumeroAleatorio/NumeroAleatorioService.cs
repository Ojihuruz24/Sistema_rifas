using Microsoft.EntityFrameworkCore;
using SIMRIFA.DataAccess.Repository;
using SIMRIFA.Model.core;
using SIMRIFA.Service.Series;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace SIMRIFA.Service.NumeroAleatorio
{
	public class NumeroAleatorioService : INumeroAleatorioService
	{
		private readonly IRepository<SIMRIFA.Model.core.NumeroAleatorio> _repository;

		public NumeroAleatorioService(IRepository<SIMRIFA.Model.core.NumeroAleatorio> repository)
		{
			_repository = repository;
		}


		public async Task<SIMRIFA.Model.core.NumeroAleatorio> AgregarAsync(SIMRIFA.Model.core.NumeroAleatorio numeroAleatorio)
		{
			var result = await _repository.Add(numeroAleatorio);
			return result;
		}

		public async Task<Model.core.NumeroAleatorio> ActualizarAsync(Model.core.NumeroAleatorio numeroAleatorio)
		{

			Model.core.NumeroAleatorio data = await _repository.Update(numeroAleatorio);
			return data;
		}

		public async Task<IEnumerable<Model.core.NumeroAleatorio>> ObtenerAsync(Expression<Func<Model.core.NumeroAleatorio, bool>> Function = default)
		{
			var Qry = await _repository.GetOneOrAll(Function);
			return Qry;
		}

		public async Task<Model.core.NumeroAleatorio> EliminarAsync(Model.core.NumeroAleatorio numeroAleatorio)
		{
			var result = await _repository.Delete(numeroAleatorio);
			return result;

		}

		public async Task<List<string>> ObtenerNumerosAsync(Expression<Func<Model.core.NumeroAleatorio, bool>> Function = default)
		{
			var Qry = await _repository.GetOneOrAll(Function);

			IEnumerable<string> numeros = new List<string>();

			if (Qry != null)
			{
				numeros = Qry.Select(x => x.Numero);
			}

			return numeros.ToList();
		}



		public async Task<List<string>> ActualizarEstadoNumero(Expression<Func<Model.core.NumeroAleatorio, bool>> Function = default)
		{
			var Qry = await _repository.GetOneOrAll(Function);

			IEnumerable<string> numeros = new List<string>();

			if (Qry != null)
			{
				numeros = Qry.Select(x => x.Numero);
			}

			return numeros.ToList();
		}

		public async Task<List<string>> GenerarNumeroAletorio(int valor, int valorMaximo)
		{
			var litnumTemp = new List<string>();

			litnumTemp.AddRange(await NumerosExistentes());

			var nuevoNumeros = new List<string>();

			for (int i = 0; i < valor; i++)
			{
				string numero;
				do
				{
					numero = await GenerarNumeroAleatorio(valorMaximo);

				} while (litnumTemp.Contains(numero));

				litnumTemp.Add(numero);
				nuevoNumeros.Add(numero);
			}

			return nuevoNumeros;
		}


		private async Task<List<string>> NumerosExistentes()
		{
			var newlit = new List<string>();

			newlit.AddRange(await ObtenerNumerosAsync());

			return newlit;
		}

		private async Task<string> GenerarNumeroAleatorio(int valorMaximo)
		{
			Random rnd = new Random();

			var temp = await Task.Run(() => rnd.Next(0, valorMaximo));

			string format = string.Empty;

			for (int i = 1; i < valorMaximo.ToString().Length; i++)
			{
				format += "0";
			}

			var numeroFormateado = temp.ToString(format);

			return numeroFormateado;
		}
	}
}
