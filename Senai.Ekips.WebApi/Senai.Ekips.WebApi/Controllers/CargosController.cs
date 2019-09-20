using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Ekips.WebApi.Domains;
using Senai.Ekips.WebApi.Repositories;

namespace Senai.Ekips.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CargosController : ControllerBase
    {
        CargoRepository CargoRepository = new CargoRepository();

        [Authorize]
        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(CargoRepository.Listar());
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            return Ok(CargoRepository.BuscarPorId(id));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Cadastrar(Cargos cargo)
        {
            try
            {
                CargoRepository.Cadastrar(cargo);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Cargos cargo)
        {
            try
            {
                cargo.IdCargo = id;
                Cargos CargoBuscado = CargoRepository.BuscarPorId(cargo.IdCargo);

                if (CargoBuscado == null)
                    return NotFound();

                CargoRepository.Atualizar(cargo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Ih, deu erro." + ex.Message });
            }
        }
    }
}