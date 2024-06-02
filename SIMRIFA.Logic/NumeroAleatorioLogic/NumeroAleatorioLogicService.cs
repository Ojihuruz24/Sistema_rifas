using SIMRIFA.Service.NumeroAleatorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Logic.NumeroAleatorioLogic
{
	public class NumeroAleatorioLogicService : INumeroAleatorioLogicService
	{
		private readonly INumeroAleatorioService _numeroAleatorioService;

		public NumeroAleatorioLogicService(INumeroAleatorioService numeroAleatorioService)
		{
			_numeroAleatorioService = numeroAleatorioService;
		}

		public async Task<SIMRIFA.Model.core.NumeroAleatorio> AgregarAsync(SIMRIFA.Model.core.NumeroAleatorio numeroAleatorio)
		{
			return await _numeroAleatorioService.AgregarAsync(numeroAleatorio);
		}

		public async Task<Model.core.NumeroAleatorio> ActualizarAsync(Model.core.NumeroAleatorio numeroAleatorio)
		{
			return await _numeroAleatorioService.ActualizarAsync(numeroAleatorio);
		}

		public async Task<IEnumerable<Model.core.NumeroAleatorio>> ObtenerAsync(Expression<Func<Model.core.NumeroAleatorio, bool>> Function = default)
		{
			return await _numeroAleatorioService.ObtenerAsync(Function);
		}

		public async Task<Model.core.NumeroAleatorio> EliminarAsync(Model.core.NumeroAleatorio numeroAleatorio)
		{
			return await _numeroAleatorioService.EliminarAsync(numeroAleatorio);
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

		private async Task<List<string>> ObtenerNumerosAsync(Expression<Func<Model.core.NumeroAleatorio, bool>> Function = default)
		{
			var Qry = await _numeroAleatorioService.ObtenerAsync(Function);

			IEnumerable<string> numeros = new List<string>();

			if (Qry != null)
			{
				numeros = Qry.Select(x => x.Numero);
			}

			return numeros.ToList();
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
