using Microsoft.EntityFrameworkCore;
using Senai.Ekips.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Ekips.WebApi.Repositories
{
    public class FuncionarioRepository
    {
        public List<Funcionarios> Listar()
        {
            using (EkipsContext ctx = new EkipsContext())
            {
                return ctx.Funcionarios.Include(x => x.IdCargoNavigation).Include(x=> x.IdDepartamentoNavigation).ToList();
            }
        }

        public void Cadastrar(Funcionarios funcionario)
        {
            using(EkipsContext ctx = new EkipsContext())
            {
                ctx.Funcionarios.Add(funcionario);
                ctx.SaveChanges();
            }
        }

        public Funcionarios BuscarPorId(int id)
        {
            using (EkipsContext ctx = new EkipsContext())
            {
                return ctx.Funcionarios.Include(x => x.IdCargoNavigation).Include(x => x.IdDepartamentoNavigation).FirstOrDefault(x => x.IdFuncionario == id);
            }
        }

        public Funcionarios BuscarPorIdUsuario(int id)
        {
            using (EkipsContext ctx = new EkipsContext())
            {
                return ctx.Funcionarios.Include(x => x.IdCargoNavigation).Include(x => x.IdDepartamentoNavigation).Include(x => x.IdUsuarioNavigation).FirstOrDefault(x => x.IdUsuarioNavigation.IdUsuario == id);
            }
        }

        public void Atualizar(Funcionarios funcionario)
        {
            using (EkipsContext ctx = new EkipsContext())
            {
                Funcionarios FuncionarioBuscado = ctx.Funcionarios.FirstOrDefault(x => x.IdFuncionario == funcionario.IdFuncionario);
                FuncionarioBuscado.Nome = funcionario.Nome;
                FuncionarioBuscado.Cpf = funcionario.Cpf;
                FuncionarioBuscado.DataNascimento = funcionario.DataNascimento;
                FuncionarioBuscado.Salario = funcionario.Salario;
                FuncionarioBuscado.IdDepartamento = funcionario.IdDepartamento;
                FuncionarioBuscado.IdCargo = funcionario.IdCargo;
                FuncionarioBuscado.IdUsuario = funcionario.IdUsuario;
                ctx.Funcionarios.Update(FuncionarioBuscado);
                ctx.SaveChanges();
            }
        }

        public void Deletar(int id)
        {
            using (EkipsContext ctx = new EkipsContext())
            {
                Funcionarios FuncionarioBuscado = ctx.Funcionarios.Find(id);
                ctx.Funcionarios.Remove(FuncionarioBuscado);
                ctx.SaveChanges();
            }
        }
    }
}
