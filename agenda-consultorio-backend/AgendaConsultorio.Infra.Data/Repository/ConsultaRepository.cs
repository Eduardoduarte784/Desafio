using AgendaConsultorio.Domain.Entity;
using AgendaConsultorio.Domain.Repository;
using AgendaConsultorio.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgendaConsultorio.Infra.Data.Repository
{
    public class ConsultaRepository : IConsultaRepository
    {
        private AgendaConsultorioContext context;
        public ConsultaRepository(AgendaConsultorioContext parametroContext)
        {
            this.context = parametroContext;
        }
        public IEnumerable<Consulta> ObterConsultas()
        {
            var listaConsulta = context.Set<Consulta>().ToList();
            return listaConsulta;
        }

        public Consulta BuscarPeloPaciente(string paciente)
        {
            return this.context.Set<Consulta>().FirstOrDefault(x => x.Paciente == paciente);
        }

        public void DeletarConsulta(Consulta consultaParametro)
        {
            this.context.Set<Consulta>().Remove(consultaParametro);
            this.context.SaveChanges();
        }
    }
}
