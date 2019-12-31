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
            if (consultaParametro.DataInicial < consultaParametro.DataFinal)
            {
                var listaConsulta = consultaRepository.ObterConsultas();
                bool datasDistintas;
                bool horarioVago = true;
                foreach (var cs in listaConsulta)
                {
                    datasDistintas = (consultaParametro.DataInicial.Date > cs.DataFinal.Date) || (consultaParametro.DataFinal.Date < cs.DataInicial.Date);
                    if (!datasDistintas)
                    {
                        if (consultaParametro.DataInicial.Date == cs.DataFinal.Date && consultaParametro.DataFinal.Date == cs.DataInicial.Date)
                        {
                            horarioVago = ((consultaParametro.DataInicial.TimeOfDay >= cs.DataFinal.TimeOfDay) || (consultaParametro.DataFinal.TimeOfDay <= cs.DataInicial.TimeOfDay));
                        }
                        else
                        {
                            if (consultaParametro.DataInicial.Date == cs.DataFinal.Date)
                            {
                                horarioVago = consultaParametro.DataInicial.TimeOfDay >= cs.DataFinal.TimeOfDay;
                            }
                            else
                            {
                                if (consultaParametro.DataFinal.Date == cs.DataInicial.Date)
                                {
                                    horarioVago = consultaParametro.DataFinal.TimeOfDay <= cs.DataInicial.TimeOfDay;
                                }
                                else
                                {
                                    horarioVago = false;
                                }
                            }
                        }
                    }
                }
                if (horarioVago)
                {
                    var consulta = new Consulta(consultaParametro.Paciente, consultaParametro.DataNascimento, consultaParametro.DataInicial, consultaParametro.DataFinal, consultaParametro.Observacoes);
                    consultaRepository.InserirConsulta(consulta);
                    return "Inserido com sucesso!";
                } else {
                    return "Horário ocupado!";
                }
            }
            else
            {
                return "A Data Inicial não pode ser menor do que a Data Final";
            }
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
