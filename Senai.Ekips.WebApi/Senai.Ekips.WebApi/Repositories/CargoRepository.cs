using Senai.Ekips.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Ekips.WebApi.Repositories
{
    public class CargoRepository
    {
        public List<Cargos> Listar()
        {
            using(EkipsContext ctx = new EkipsContext())
            {
                return ctx.Cargos.ToList();
            }
        }

        public Cargos BuscarPorId(int id)
        {
            using (EkipsContext ctx = new EkipsContext())
            {
                return ctx.Cargos.FirstOrDefault(x => x.IdCargo == id);
            }
        }

        public void Cadastrar(Cargos cargo)
        {
            using (EkipsContext ctx = new EkipsContext())
            {
                ctx.Cargos.Add(cargo);
                ctx.SaveChanges();
            }
        }

        public void Atualizar(Cargos cargo)
        {
            using (EkipsContext ctx = new EkipsContext())
            {
                Cargos CargoBuscado = ctx.Cargos.FirstOrDefault(x => x.IdCargo == cargo.IdCargo);
                CargoBuscado.Nome = cargo.Nome;
                CargoBuscado.Ativo = cargo.Ativo;
                ctx.Cargos.Update(CargoBuscado);
                ctx.SaveChanges();
            }
        }
    }
}
