using AgendaConsultorio.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaConsultorio.Application.Service.Interface
{
    public interface IConsultaService
    {
        public IEnumerable<ConsultaViewModel> getConsultas();
        public string DeletaConsultas(string pacienteParametro);
    }
}
