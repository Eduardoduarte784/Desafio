using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaConsultorio.Domain.Entity
{
    public class Consulta
    {
        public Guid Id { get; set; }
        public string Paciente { get; set; }
        public string DataNascimento { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public string Observacoes { get; set; }

        public Consulta(string paciente, string dataNascimento, DateTime dataInicial, DateTime dataFinal, string observacoes)
        {
            this.Id = Guid.NewGuid();
            this.Paciente = paciente;
            this.DataNascimento = dataNascimento;
            this.DataInicial = dataInicial;
            this.DataFinal = dataFinal;
            this.Observacoes = observacoes; 
        }

    }
}
