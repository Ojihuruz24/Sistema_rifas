using Microsoft.EntityFrameworkCore;
using SIMRIFA.DataAccess.Db_Context;
using SIMRIFA.DataAccess.UnitOfWork;
using SIMRIFA.Model.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.DataAccess.Repository.MunicipioRepo
{
    public class MunicipioRepositorio : IMunicipioRepositorio
    {
        private readonly SIMRIFAdbContext _dbcontext;

        public MunicipioRepositorio(SIMRIFAdbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IEnumerable<Model.core.Municipio>> ObtenerAsync(Expression<Func<Model.core.Municipio, bool>> Function = default)
        {
            IEnumerable<Municipio>? Resultado = default;
            try
            {
                var Qry = _dbcontext.Municipio.Include(x => x.Departamento).AsQueryable();

                if (Function != default)
                {
                    Qry = Qry.Where(Function);
                }
                Resultado = await Qry.Take(50).ToListAsync();
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Error al {nameof(ObtenerAsync)}");
            }

            return Resultado;
        }
    }
}
