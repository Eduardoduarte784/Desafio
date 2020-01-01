using AgendaConsultorio.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaConsultorio.Domain.Repository
{
    public interface IConsultaRepository
    {
        public IEnumerable<Consulta> ObterConsultas();
        public Consulta BuscarPelaDataInicial(DateTime dataInicialParametro);
        public void InserirConsulta(Consulta consultaParametro);
        public void DeletarConsulta(Consulta consultaParametro);
        public void AtualizarConsulta(Consulta consultaParametro);
    }
}
