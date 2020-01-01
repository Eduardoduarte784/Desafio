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
                bool horarioVago = true;
                foreach (var cs in listaConsulta)
                {
                    if ((consultaParametro.DataInicial < cs.DataFinal) && (consultaParametro.DataFinal > cs.DataInicial))
                    {
                        horarioVago = false;
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

        public string DeletaConsultas(DateTime dataInicialParametro)
        {
            if (consultaRepository.BuscarPelaDataInicial(dataInicialParametro) != null)
            {
                consultaRepository.DeletarConsulta(consultaRepository.BuscarPelaDataInicial(dataInicialParametro));
                return "Deletado com sucesso!";
            }
            else
            {
                return "Consulta não existe na base de dados";
            }
        }

        public string AtualizaConsultas(ConsultaViewModel[] consultaParametro)
        {
            if (consultaRepository.BuscarPelaDataInicial(consultaParametro[0].DataInicial) != null)
            {
                if (consultaParametro[1].DataInicial < consultaParametro[1].DataFinal)
                {
                    var listaConsulta = consultaRepository.ObterConsultas();
                    bool horarioVago = true;
                    foreach (var cs in listaConsulta)
                    {
                        if ((consultaParametro[1].DataInicial < cs.DataFinal) && (consultaParametro[1].DataFinal > cs.DataInicial))
                        {
                            horarioVago = false;
                        }
                    }
                        
                    if (horarioVago)
                    {
                        var consulta = consultaRepository.BuscarPelaDataInicial(consultaParametro[0].DataInicial);
                        consulta.Paciente = consultaParametro[1].Paciente;
                        consulta.DataNascimento = consultaParametro[1].DataNascimento;
                        consulta.DataInicial = consultaParametro[1].DataInicial;
                        consulta.DataFinal = consultaParametro[1].DataFinal;
                        consulta.Observacoes = consultaParametro[1].Observacoes;
                        consultaRepository.AtualizarConsulta(consulta);
                        return "Atualizado com sucesso!";
                    }
                    else
                    {
                        return "Horário Indisponível";
                    }
                }
                else
                {
                    return "Data final não pode ser menor do que a inicial";
                }
            }
            else
            { 
                return "Estoque não existe na base de dados";
            }
        }
    }
}
