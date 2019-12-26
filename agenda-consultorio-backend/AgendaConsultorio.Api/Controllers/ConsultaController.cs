using AgendaConsultorio.Application.Service.Interface;
using AgendaConsultorio.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaConsultorio.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultaController : ControllerBase
    {
        private IConsultaService consultaService;
        public ConsultaController(IConsultaService parametroConsultaService)
        {
            this.consultaService = parametroConsultaService;
        }

        [HttpGet]
        public IEnumerable<ConsultaViewModel> GetConsulta()
        {
            var lista = consultaService.getConsultas();
            return lista;
        }

        [HttpDelete("{pacienteParametro}")]
        public string DeletaEstoque(string pacienteParametro)
                {
            return consultaService.DeletaConsultas(pacienteParametro);
        }

    }
}
