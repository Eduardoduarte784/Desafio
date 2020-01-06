using AgendaConsultorio.Application.Service;
using AgendaConsultorio.Application.Service.Interface;
using AgendaConsultorio.Application.ViewModel;
using AgendaConsultorio.Domain.Entity;
using AgendaConsultorio.Domain.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AgendaConsultorio.Testes
{
    public class ConsultaTeste
    {
        private Mock<IConsultaRepository> _mockRepository;
        private IConsultaService _service;

        [Test]
        public void InsereConsultasTeste()
        {
            _mockRepository = new Mock<IConsultaRepository>();
            _service = new ConsultaService(_mockRepository.Object);
            var dataDeNascimento = new DateTime(2000, 01, 30, 07, 00, 00);
            var dataDeInicio = new DateTime(2020, 01, 30, 07, 00, 00);
            var dataDeTermino = new DateTime(2020, 01, 30, 08, 00, 00);
            var consultaViewModel = new ConsultaViewModel("teste", dataDeNascimento, dataDeInicio, dataDeTermino, "campo de observações");
            Assert.AreEqual("Inserido com sucesso!", _service.InsereConsultas(consultaViewModel));
        }

        [Test]
        public void InsereConsultasDataInicialMaiorTeste()
        {
            _mockRepository = new Mock<IConsultaRepository>();
            _service = new ConsultaService(_mockRepository.Object);
            var dataDeNascimento = new DateTime(2000, 01, 30, 07, 00, 00);
            var dataDeInicio = new DateTime(2015, 01, 30, 07, 00, 00);
            var dataDeTermino = new DateTime(2015, 01, 30, 06, 00, 00);
            var consultaViewModel = new ConsultaViewModel("teste", dataDeNascimento, dataDeInicio, dataDeTermino, "campo de observações");
            Assert.AreEqual("A Data Inicial não pode ser menor do que a Data Final", _service.InsereConsultas(consultaViewModel));
        }

        [Test]
        public void InsereConsultasHorarioOcupadoTeste()
        {
            var dataDeNascimento = new DateTime(2000, 01, 30, 07, 00, 00);
            var dataDeInicio = new DateTime(2020, 01, 30, 07, 00, 00);
            var dataDeTermino = new DateTime(2020, 01, 30, 09, 00, 00);
            var consulta = new Consulta("teste", dataDeNascimento, dataDeInicio, dataDeTermino, "campo de observações");
            var listaDeConsultas = new List<Consulta>();
            listaDeConsultas.Add(consulta);
            _mockRepository = new Mock<IConsultaRepository>();
            _mockRepository.Setup(m => m.ObterConsultas()).Returns(listaDeConsultas);
            _service = new ConsultaService(_mockRepository.Object);
            var consultaViewModel = new ConsultaViewModel("teste", dataDeNascimento, dataDeInicio, dataDeTermino, "campo de observações");
            Assert.AreEqual("Horário ocupado!", _service.InsereConsultas(consultaViewModel));
        }

        [Test]
        public void DeletaConsultasTeste()
        {
            var dataDeNascimento = new DateTime(2000, 01, 30, 07, 00, 00);
            var dataDeInicio = new DateTime(2020, 01, 30, 07, 00, 00);
            var dataDeTermino = new DateTime(2020, 01, 30, 09, 00, 00);
            var consulta = new Consulta("teste", dataDeNascimento, dataDeInicio, dataDeTermino, "campo de observações");
            _mockRepository = new Mock<IConsultaRepository>();
            _mockRepository.Setup(m => m.BuscarPelaDataInicial(dataDeInicio)).Returns(consulta);
            _service = new ConsultaService(_mockRepository.Object);
            Assert.AreEqual("Deletado com sucesso!", _service.DeletaConsultas(dataDeInicio));
        }

        [Test]
        public void DeletaConsultasNaoExistenteTeste()
        {
            Consulta nulo = null;
            var dataParametro = new DateTime(2020, 01, 20, 19, 30, 00);
            _mockRepository = new Mock<IConsultaRepository>();
            _mockRepository.Setup(m => m.BuscarPelaDataInicial(dataParametro)).Returns(nulo);
            _service = new ConsultaService(_mockRepository.Object);
            Assert.AreEqual("Consulta não existe na base de dados", _service.DeletaConsultas(dataParametro));
        }

        [Test]
        public void AtualizaConsultasTeste()
        {
            var dataDeNascimento = new DateTime(1999, 01, 30, 07, 00, 00);
            var dataDeInicio = new DateTime(2020, 01, 30, 07, 00, 00);
            var dataDeTermino = new DateTime(2020, 01, 30, 08, 00, 00);
            var consulta = new Consulta("André", dataDeNascimento, dataDeInicio, dataDeTermino, "Sem observações");
            var dataDeNascimento2 = new DateTime(2000, 02, 28, 07, 00, 00);
            var dataDeInicio2 = new DateTime(2020, 02, 28, 07, 00, 00);
            var dataDeTermino2 = new DateTime(2020, 02, 28, 08, 00, 00);
            var dataDeNascimento3 = new DateTime(2001, 03, 30, 07, 00, 00);
            var dataDeInicio3 = new DateTime(2020, 03, 30, 07, 00, 00);
            var dataDeTermino3 = new DateTime(2020, 03, 30, 08, 00, 00);
            var consulta2 = new Consulta("Carlos", dataDeNascimento2, dataDeInicio2, dataDeTermino2, "Sem observações");
            var consulta3 = new Consulta("Daniel", dataDeNascimento3, dataDeInicio3, dataDeTermino3, "Sem observações");
            var listaDeConsultas = new List<Consulta>();
            listaDeConsultas.AddRange(new[] { consulta, consulta2, consulta3 });
            var consultaASerAtualizada = new ConsultaViewModel("André", dataDeNascimento, dataDeInicio, dataDeTermino, "Sem observações");
            var dataDeNascimentoAtt = new DateTime(1998, 01, 30, 07, 00, 00);
            var dataDeInicioAtt = new DateTime(2020, 01, 31, 10, 00, 00);
            var dataDeTerminoAtt = new DateTime(2020, 01, 31, 11, 00, 00);
            var consultaAtualizada = new ConsultaViewModel("André de Souza", dataDeNascimentoAtt, dataDeInicioAtt, dataDeTerminoAtt, "campo de observações");
            ConsultaViewModel[] arrayParametro = { consultaASerAtualizada, consultaAtualizada };
            _mockRepository = new Mock<IConsultaRepository>();
            _mockRepository.Setup(m => m.BuscarPelaDataInicial(consulta.DataInicial)).Returns(consulta);
            _mockRepository.Setup(m => m.ObterConsultas()).Returns(listaDeConsultas);
            _service = new ConsultaService(_mockRepository.Object);
            Assert.AreEqual("Atualizado com sucesso!", _service.AtualizaConsultas(arrayParametro));
        }

        [Test]
        public void AtualizaConsultasHorarioIndisponivelTeste()
        {
            var dataDeNascimento = new DateTime(1999, 01, 30, 07, 00, 00);
            var dataDeInicio = new DateTime(2020, 01, 30, 07, 00, 00);
            var dataDeTermino = new DateTime(2020, 01, 30, 09, 00, 00);
            var consulta = new Consulta("André", dataDeNascimento, dataDeInicio, dataDeTermino, "Sem observações");
            var dataDeNascimento2 = new DateTime(2000, 02, 28, 07, 00, 00);
            var dataDeInicio2 = new DateTime(2020, 02, 28, 07, 00, 00);
            var dataDeTermino2 = new DateTime(2020, 02, 28, 08, 00, 00);
            var dataDeNascimento3 = new DateTime(2001, 03, 30, 07, 00, 00);
            var dataDeInicio3 = new DateTime(2020, 03, 30, 07, 00, 00);
            var dataDeTermino3 = new DateTime(2020, 03, 30, 08, 00, 00);
            var consulta2 = new Consulta("Carlos", dataDeNascimento2, dataDeInicio2, dataDeTermino2, "Sem observações");
            var consulta3 = new Consulta("Daniel", dataDeNascimento3, dataDeInicio3, dataDeTermino3, "Sem observações");
            var listaDeConsultas = new List<Consulta>();
            listaDeConsultas.AddRange(new[] { consulta, consulta2, consulta3 });
            var consultaASerAtualizada = new ConsultaViewModel("André", dataDeNascimento, dataDeInicio, dataDeTermino, "Sem observações");
            var dataDeNascimentoAtt = new DateTime(1998, 01, 30, 07, 00, 00);
            var dataDeInicioAtt = new DateTime(2020, 02, 28, 07, 30, 00);
            var dataDeTerminoAtt = new DateTime(2020, 02, 28, 08, 30, 00);
            var consultaAtualizada = new ConsultaViewModel("André de Souza", dataDeNascimentoAtt, dataDeInicioAtt, dataDeTerminoAtt, "campo de observações");
            ConsultaViewModel[] arrayParametro = { consultaASerAtualizada, consultaAtualizada };
            _mockRepository = new Mock<IConsultaRepository>();
            _mockRepository.Setup(m => m.BuscarPelaDataInicial(consulta.DataInicial)).Returns(consulta);
            _mockRepository.Setup(m => m.ObterConsultas()).Returns(listaDeConsultas);
            _service = new ConsultaService(_mockRepository.Object);
            Assert.AreEqual("Horário Indisponível", _service.AtualizaConsultas(arrayParametro));
        }

        [Test]
        public void AtualizaConsultasDataInicialMaiorTeste()
        {
            var dataDeNascimento = new DateTime(2000, 01, 30, 07, 00, 00);
            var dataDeInicio = new DateTime(2020, 01, 30, 07, 00, 00);
            var dataDeTermino = new DateTime(2020, 01, 30, 09, 00, 00);
            var consulta = new Consulta("André", dataDeNascimento, dataDeInicio, dataDeTermino, "Sem observações");
            var listaDeConsultas = new List<Consulta>();
            listaDeConsultas.Add(consulta);
            var consultaASerAtualizada = new ConsultaViewModel("André", dataDeNascimento, dataDeInicio, dataDeTermino, "Sem observações");
            var dataDeNascimentoAtt = new DateTime(1998, 01, 30, 07, 00, 00);
            var dataDeInicioAtt = new DateTime(2020, 01, 30, 08, 30, 00);
            var dataDeTerminoAtt = new DateTime(2020, 01, 30, 05, 30, 00);
            var consultaAtualizada = new ConsultaViewModel("André de Souza", dataDeNascimentoAtt, dataDeInicioAtt, dataDeTerminoAtt, "campo de observações");
            ConsultaViewModel[] arrayParametro = { consultaASerAtualizada, consultaAtualizada };
            _mockRepository = new Mock<IConsultaRepository>();
            _mockRepository.Setup(m => m.BuscarPelaDataInicial(consulta.DataInicial)).Returns(consulta);
            _service = new ConsultaService(_mockRepository.Object);
            Assert.AreEqual("Data final não pode ser menor do que a inicial", _service.AtualizaConsultas(arrayParametro));
        }

        [Test]
        public void AtualizaConsultasNaoExistenteTeste()
        {
            var dataParametro = new DateTime(2020, 01, 30, 07, 00, 00);
            Consulta nulo = null;
            var dataDeNascimento = new DateTime(2000, 01, 30, 07, 00, 00);
            var dataDeInicio = new DateTime(2020, 01, 30, 07, 00, 00);
            var dataDeTermino = new DateTime(2020, 01, 30, 09, 00, 00);
            var consultaASerAtualizada = new ConsultaViewModel("André", dataDeNascimento, dataDeInicio, dataDeTermino, "Sem observações");
            var dataDeNascimentoAtt = new DateTime(1998, 01, 30, 07, 00, 00);
            var dataDeInicioAtt = new DateTime(2020, 01, 30, 08, 30, 00);
            var dataDeTerminoAtt = new DateTime(2020, 01, 30, 05, 30, 00);
            var consultaAtualizada = new ConsultaViewModel("André de Souza", dataDeNascimentoAtt, dataDeInicioAtt, dataDeTerminoAtt, "campo de observações");
            ConsultaViewModel[] arrayParametro = { consultaASerAtualizada, consultaAtualizada };
            _mockRepository = new Mock<IConsultaRepository>();
            _mockRepository.Setup(m => m.BuscarPelaDataInicial(dataParametro)).Returns(nulo);
            _service = new ConsultaService(_mockRepository.Object);
            Assert.AreEqual("Registro não existe na base de dados", _service.AtualizaConsultas(arrayParametro));
        }

        [Test]
        public void GetConsultasTeste()
        {
            var dataDeNascimento1 = new DateTime(1999, 01, 30, 07, 00, 00);
            var dataDeInicio1 = new DateTime(2020, 01, 30, 07, 00, 00);
            var dataDeTermino1 = new DateTime(2020, 01, 30, 08, 00, 00);
            var dataDeNascimento2 = new DateTime(2000, 02, 28, 07, 00, 00);
            var dataDeInicio2 = new DateTime(2020, 02, 28, 07, 00, 00);
            var dataDeTermino2 = new DateTime(2020, 02, 28, 08, 00, 00);
            var dataDeNascimento3 = new DateTime(2001, 03, 30, 07, 00, 00);
            var dataDeInicio3 = new DateTime(2020, 03, 30, 07, 00, 00);
            var dataDeTermino3 = new DateTime(2020, 03, 30, 08, 00, 00);
            var consulta1 = new Consulta("André", dataDeNascimento1, dataDeInicio1, dataDeTermino1, "Sem observações");
            var consulta2 = new Consulta("Carlos", dataDeNascimento2, dataDeInicio2, dataDeTermino2, "Sem observações");
            var consulta3 = new Consulta("Daniel", dataDeNascimento3, dataDeInicio3, dataDeTermino3, "Sem observações");
            var consultaViewModel1 = new ConsultaViewModel("André", dataDeNascimento1, dataDeInicio1, dataDeTermino1, "Sem observações");
            var consultaViewModel2 = new ConsultaViewModel("Carlos", dataDeNascimento2, dataDeInicio2, dataDeTermino2, "Sem observações");
            var consultaViewModel3 = new ConsultaViewModel("Daniel", dataDeNascimento3, dataDeInicio3, dataDeTermino3, "Sem observações");
            var listaDeConsultas = new List<Consulta>{ consulta1, consulta2, consulta3 };
            var listaConsultaViewModel = new List<ConsultaViewModel>{ consultaViewModel1, consultaViewModel2, consultaViewModel3 };
            _mockRepository = new Mock<IConsultaRepository>();
            _mockRepository.Setup(m => m.ObterConsultas()).Returns(listaDeConsultas);
            _service = new ConsultaService(_mockRepository.Object);
            CollectionAssert.AreEqual(listaConsultaViewModel, _service.getConsultas(), new Comparativo());
        }
    }
}
