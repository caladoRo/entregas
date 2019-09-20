using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
    public class FuncionariosController : ControllerBase
    {
        FuncionarioRepository FuncionarioRepository = new FuncionarioRepository();

        [Authorize]
        [HttpGet]
        public IActionResult Listar()
        {
            var identity = HttpContext.User.Identity  as ClaimsIdentity;
            IEnumerable<Claim> claim = identity.Claims;
            var PermissaoClaim = claim.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault().Value;
            if (PermissaoClaim == "ADMINISTRADOR")
            {
                return Ok(FuncionarioRepository.Listar());
            }
            else if(PermissaoClaim == "COMUM"){
                var IdClaim = claim.Where(x => x.Type == JwtRegisteredClaimNames.Jti).FirstOrDefault().Value;
                return Ok(FuncionarioRepository.BuscarPorIdUsuario(Convert.ToInt32(IdClaim)));
            }
            else
            {
                return Forbid();
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult Cadastrar(Funcionarios funcionario)
        {
            try
            {
                FuncionarioRepository.Cadastrar(funcionario);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Funcionarios funcionario)
        {
            try
            {
                funcionario.IdFuncionario = id;
                Funcionarios FuncionarioBuscado = FuncionarioRepository.BuscarPorId(funcionario.IdFuncionario);

                if (FuncionarioBuscado == null)
                    return NotFound();

                FuncionarioRepository.Atualizar(funcionario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Ih, deu erro." + ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                Funcionarios funcionario = new Funcionarios();
                funcionario.IdFuncionario = id;
                Funcionarios FuncionarioBuscado = FuncionarioRepository.BuscarPorId(funcionario.IdFuncionario);

                if (FuncionarioBuscado == null)
                    return NotFound();

                FuncionarioRepository.Deletar(funcionario.IdFuncionario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Ih, deu erro." + ex.Message });
            }
        }
    }
}