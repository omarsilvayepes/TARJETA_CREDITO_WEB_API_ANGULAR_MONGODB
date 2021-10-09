using BackendTarjetaCredito.Model;
using BackendTarjetaCredito.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860


namespace BackendTarjetaCredito.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetaController : ControllerBase
    {
       
        //inyectando el servicio

        private readonly TarjetaService _tarjetaService;

        public TarjetaController(TarjetaService tarjetaService)
        {
            _tarjetaService = tarjetaService;
        }




        // GET: api/<TarjetaController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<TarjetaCredito> listarTarjetas = await _tarjetaService.getAllTarjetas();
                return Ok(listarTarjetas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        // POST api/<TarjetaController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TarjetaCredito tarjeta)
        {
            try
            {
                await _tarjetaService.AddTarjeta(tarjeta);
                return Ok(tarjeta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        // PUT api/<TarjetaController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] TarjetaCredito tarjeta)
        {
            try
            {
                if (id != tarjeta.Id || await _tarjetaService.getTarjetaById(id) ==null)
                {
                    return NotFound(new { message = "id No es valido" });
                }
                await _tarjetaService.updateTarjeta(tarjeta);
                return Ok(new { message = "La Tarjeta fue actualizada con exito" });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<TarjetaController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var tarjeta = await _tarjetaService.getTarjetaById(id);
                if (tarjeta == null)
                {
                    return NotFound(new { message = "id No es valido" });
                }
                await _tarjetaService.deleteTarjetaById(id);
                return Ok(new { message = "La Tarjeta fue eliminada con exito" });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

