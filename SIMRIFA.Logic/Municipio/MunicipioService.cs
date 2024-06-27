using SIMRIFA.DataAccess.Repository;
using SIMRIFA.DataAccess.Repository.MunicipioRepo;
using SIMRIFA.Model.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Logic.Municipio
{
    public class MunicipioService : IMunicipioService
    {
        private readonly IRepository<SIMRIFA.Model.core.Municipio> _repository;
        private readonly IMunicipioRepositorio _municipioRepositorio;

        public MunicipioService(IRepository<SIMRIFA.Model.core.Municipio> repository, IMunicipioRepositorio municipioRepositorio)
        {
            _repository = repository;
            _municipioRepositorio = municipioRepositorio;
        }

        public async Task<IEnumerable<Model.core.Municipio>> ObtenerAsync(Expression<Func<Model.core.Municipio, bool>> Function = default)
        {
            var Qry = await _repository.GetOneOrAll(Function);
            return Qry;
        }

           public async Task<IEnumerable<Model.core.Municipio>> ObtenerCrtierioAsync(Expression<Func<Model.core.Municipio, bool>> Function = default)
        {
            var Qry = await _municipioRepositorio.ObtenerAsync(Function);
            return Qry;
        }

        public async Task<Model.core.Municipio> ActualizarAsync(Model.core.Municipio municipio)
        {
            Model.core.Municipio data = await _repository.Update(municipio);
            return data;
        }


        public async Task<Model.core.Municipio> EliminarAsync(Model.core.Municipio municipio)
        {
            var result = await _repository.Delete(municipio);
            return result;
        }

        public async Task<Model.core.Municipio> AgregarAsync(Model.core.Municipio municipio)
        {
            var result = await _repository.Add(municipio);
            return result;
        }
    }
}
