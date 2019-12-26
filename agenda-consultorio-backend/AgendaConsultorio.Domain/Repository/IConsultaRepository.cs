using AgendaConsultorio.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaConsultorio.Domain.Repository
{
    public interface IConsultaRepository
    {
        public IEnumerable<Consulta> ObterConsultas();
        public Consulta BuscarPeloPaciente(string paciente);
        public void DeletarConsulta(Consulta consultaParametro);
    }
}
