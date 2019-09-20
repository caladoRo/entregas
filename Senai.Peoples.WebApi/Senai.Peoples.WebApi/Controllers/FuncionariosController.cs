using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Peoples.WebApi.Domains;
using Senai.Peoples.WebApi.Repositories;

namespace Senai.Peoples.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        FuncionarioRepository FuncionarioRepository = new FuncionarioRepository();

        //Busca
        [HttpGet]
        public List<FuncionarioDomain> Listar()
        {
            return FuncionarioRepository.Listar();
        }

        [HttpGet("buscar/{nome}")]
        public IActionResult BuscarPorNome(string nome)
        {
            FuncionarioDomain Funcionario = FuncionarioRepository.BuscarPorNome(nome);
            if (Funcionario == null)
            {
                return NotFound();
            }
            return Ok(Funcionario);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            FuncionarioDomain Funcionario = FuncionarioRepository.BuscarPorId(id);
            if (Funcionario == null)
            {
                return NotFound();
            }
            return Ok(Funcionario);
        }


        //Cadastro
        [HttpPost]
        public IActionResult Cadastrar(FuncionarioDomain funcionario)
        {
            FuncionarioRepository.Cadastrar(funcionario);
            return Ok();
        }

        //Atualização
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, FuncionarioDomain funcionario)
        {
            funcionario.IdFuncionario = id;
            FuncionarioRepository.Alterar(funcionario);
            return Ok();
        }

        //Deletar
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            FuncionarioRepository.Deletar(id);
            return Ok();
        }
    }
}