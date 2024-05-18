using Microsoft.EntityFrameworkCore;
using SIMRIFA.DataAccess.Db_Context;
using SIMRIFA.DataAccess.Repository;
using SIMRIFA.DataAccess.UnitOfWork;
using SIMRIFA.Model.Models;
using SIMRIFA.Service.Correo;
using SIMRIFA.Service.Series;
using SIMRIFA.Service.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Service.Compra
{
	public class CompradorService : ICompradorService
	{
		private readonly SIMRIFAdbContext _context;
		private readonly IUnitOfWork _IUnitOfWork;
		private readonly IUtils _utils;
		private readonly ISerieService _serieService;
		private readonly ICorreoServicio _correoServicio;
		private List<string> numeros = new List<string>();
		private int _numeroMaximo;

		public CompradorService(SIMRIFAdbContext dbContext, IUnitOfWork unitOfWork, ISerieService serieService, ICorreoServicio correoServicio)
		{
			_context = dbContext;
			_IUnitOfWork = unitOfWork;
			_serieService = serieService;
			_numeroMaximo = _serieService.ObtenerSerieActiva().FirstOrDefault().NumeroMaximo;
			_correoServicio = correoServicio;
		}

		public async Task<List<string>> AddComprador(int cantidad, string precio)
		{
			try
			{
				numeros = GenerarNumeroAletorio(cantidad);

				var actualSerie = ValidarSerie(numeros.Count);

				string number = AjustesNumeros();

				var comprador = new Model.Models.Comprador
				{
					Nombre = "dayan",
					Apellido = "agudelo",
					Identificacion = "1036957989",
					Correo = "dayan24agudelo@gmail.com",
					Telefono = "3113633371",
					//Serie = getserie?.FirstOrDefault()?.NumeroSerie.ToString(),
					Serie = actualSerie.NumeroSerie.ToString(),
					Cantidad = numeros.Count.ToString(),
					Numeros = number,
					Estado = "TEST",
					Valor = precio,
					FechaCreacion = DateTime.Now,
				};

				_IUnitOfWork._context.Add(comprador);
				_IUnitOfWork.Commit();

				if (true) // aca va ir la validaicon del PSE
				{

				}

				await _correoServicio.EnvioCorreoMailNet(comprador);

				return numeros;
			}
			catch (Exception ex)
			{
				_IUnitOfWork.Rollback();
				return numeros;
			}
			finally
			{
				_context.Dispose();
			}
		}

		private Serie ValidarSerie(int numeros)
		{
			var actualSerie = _serieService.ObtenerSerieActiva().FirstOrDefault();

			if (actualSerie != null)
			{
				if (actualSerie.NumeroMaximo == actualSerie.Contador)
				{
					actualSerie.Estado = false;
					_IUnitOfWork._context.Serie.Update(actualSerie);
					_IUnitOfWork.Commit();


					actualSerie.NumeroSerie = actualSerie.NumeroSerie + 1;
					actualSerie.Estado = true;
					actualSerie.NumeroMaximo = 10000;
					actualSerie.Contador = 0;
					actualSerie.FechaCreacion = DateTime.Now;
					_IUnitOfWork._context.Serie.Add(actualSerie);
					_IUnitOfWork.Commit();
				}

				actualSerie.Contador = actualSerie.Contador + numeros;

				_IUnitOfWork._context.Serie.Update(actualSerie);
			}

			return actualSerie;
		}

		private string AjustesNumeros()
		{
			string number = string.Empty;

			foreach (var item in numeros)
			{
				number += item.ToString() + "-";
			}

			if (number.EndsWith("-"))
			{
				number = number.Remove(number.Length - 1);
			}

			return number;
		}

		private List<string> GenerarNumeroAletorio(int valor)
		{
			var litnumTemp = new List<string>();

			litnumTemp.AddRange(NumerosExistentes());

			var nuevoNumeros = new List<string>();

			for (int i = 0; i < valor; i++)
			{
				string numero;
				do
				{
					numero = GenerarNumeroAleatorio();

				} while (litnumTemp.Contains(numero));

				litnumTemp.Add(numero);

				nuevoNumeros.Add(numero);
			}

			return nuevoNumeros;
		}

		private string GenerarNumeroAleatorio()
		{
			Random rnd = new Random();

			var temp = rnd.Next(0, _numeroMaximo);

			var numeroFormateado = temp.ToString("000");

			return numeroFormateado;
		}

		private List<string> NumerosExistentes()
		{
			var list = _IUnitOfWork._context.Comprador.Select(x => x.Numeros).ToList();
			var listnum = new List<string>();

			foreach (var item in list)
			{
				var conver = item.Split('-');

				foreach (var numero in conver)
				{
					listnum.Add(numero);
				}
			}

			return listnum;
		}

	}
}
