using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaConsultorio.Application.ViewModel
{
    public class ConsultaViewModel
    {
        public string Paciente { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        
        public string Observacoes { get; set; }

        public ConsultaViewModel(string paciente, DateTime dataNascimento, DateTime dataInicial, DateTime dataFinal, string observacoes)
        {
            this.Paciente = paciente;
            this.DataNascimento = dataNascimento;
            this.DataInicial = dataInicial;
            this.DataFinal = dataFinal;
            this.Observacoes = observacoes;
        }

        public ConsultaViewModel(){}
    }
}
