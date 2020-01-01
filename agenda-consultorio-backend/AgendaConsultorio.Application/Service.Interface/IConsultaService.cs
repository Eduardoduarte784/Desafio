using AgendaConsultorio.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaConsultorio.Application.Service.Interface
{
    public interface IConsultaService
    {
        public IEnumerable<ConsultaViewModel> getConsultas();
        public string InsereConsultas(ConsultaViewModel consultaParametro);
        public string DeletaConsultas(DateTime dataInicialParametro);
        public string AtualizaConsultas(ConsultaViewModel[] consultaParametro);
    }
}
