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
    public class DepartamentosController : ControllerBase
    {
        DepartamentoRepository DepartamentoRepository = new DepartamentoRepository();

        [Authorize]
        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(DepartamentoRepository.Listar());
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            return Ok(DepartamentoRepository.BuscarPorId(id));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Cadastrar(Departamentos departamento)
        {
            try
            {
                DepartamentoRepository.Cadastrar(departamento);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}