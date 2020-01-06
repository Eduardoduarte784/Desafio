using AgendaConsultorio.Application.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AgendaConsultorio.Testes
{
    public class Comparativo : IComparer, IComparer<ConsultaViewModel>
    {
        public int Compare(ConsultaViewModel x, ConsultaViewModel y)
        {
            if ((x.Paciente == y.Paciente) && (x.DataNascimento == y.DataNascimento) && (x.DataInicial == y.DataInicial) && (x.DataFinal == y.DataFinal) && (x.Observacoes == y.Observacoes)) return 0; return 1;
        }
        public int Compare(object x, object y)
        {
            return Compare(x as ConsultaViewModel, y as ConsultaViewModel);
        }
    }
}
