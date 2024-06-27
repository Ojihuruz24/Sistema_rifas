using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMRIFA.Logic.Municipio;
using SIMRIFA.Logic.TipoIdentificacion;
using SIMRIFA.Model.core;
using System.Net;

namespace SIMRIFA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MunicipioController : ControllerBase
    {
        private readonly IMunicipioService _service;

        public MunicipioController(IMunicipioService service)
        {
            _service = service;
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            try
            {
                IEnumerable<Municipio> slit = await _service.ObtenerAsync(x => x.ESTADO == true);

                return Ok(slit);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{criterio}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Municipio>>> Get(string criterio)
        {
            return Ok((await _service.ObtenerCrtierioAsync(x => (x.NOMBRE_MUNICIPIO).Contains(criterio) || (x.Departamento.NOMBRE_DEPARTAMENTO).Contains(criterio))));
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Agregar([FromBody] Municipio municipio)
        {
            try
            {
                return Ok(await _service.AgregarAsync(municipio));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Actualizar([FromBody] Municipio municipio)
        {
            try
            {
                return Ok(await _service.ActualizarAsync(municipio));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> Eliminar([FromBody] Municipio  municipio)
        {
            try
            {
                return Ok(await _service.EliminarAsync(municipio));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

    }
}
