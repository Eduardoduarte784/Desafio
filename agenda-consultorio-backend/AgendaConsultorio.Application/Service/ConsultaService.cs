using AgendaConsultorio.Application.Service.Interface;
using AgendaConsultorio.Application.ViewModel;
using AgendaConsultorio.Domain.Entity;
using AgendaConsultorio.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaConsultorio.Application.Service
{
    public class ConsultaService : IConsultaService
    {
        private IConsultaRepository consultaRepository;
        public ConsultaService(IConsultaRepository parametroConsultaRepository)
        {
            this.consultaRepository = parametroConsultaRepository;
        }
        public IEnumerable<ConsultaViewModel> getConsultas()
        {
            var listaConsulta = consultaRepository.ObterConsultas();
            var listaConsultaViewModel = new List<ConsultaViewModel>();
            foreach (var lc in listaConsulta)
            {
                var consultaViewModel = new ConsultaViewModel(lc.Paciente, lc.DataNascimento, lc.DataInicial, lc.DataFinal, lc.Observacoes);
                listaConsultaViewModel.Add(consultaViewModel);
            }
            return listaConsultaViewModel;
        }

        public string InsereConsultas(ConsultaViewModel consultaParametro)
        {
            //     if (consultaRepository.BuscarPeloPaciente(consultaParametro.Paciente) == null)
            //     {
            var consulta = new Consulta(consultaParametro.Paciente, consultaParametro.DataNascimento, consultaParametro.DataInicial, consultaParametro.DataFinal, consultaParametro.Observacoes);
                consultaRepository.InserirConsulta(consulta);
                return "Inserido com sucesso!";
       //     }
      //      else
     //       {
       //         return "Consulta já existe na base de dados";
      //      }
        }

        public string DeletaConsultas(string pacienteParametro)
        {
            if (consultaRepository.BuscarPeloPaciente(pacienteParametro) != null)
            {
                consultaRepository.DeletarConsulta(consultaRepository.BuscarPeloPaciente(pacienteParametro));
                return "Deletado com sucesso!";
            }
            else
            {
                return "Consulta não existe na base de dados";
            }
        }
    }
}
