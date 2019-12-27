using AgendaConsultorio.Application.Service.Interface;
using AgendaConsultorio.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

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

        [HttpPost]
        public string InsereConsulta([FromBody]ConsultaViewModel consultaParametro)
        {
            return consultaService.InsereConsultas(consultaParametro);
        }

        [HttpDelete("{pacienteParametro}")]
        public string DeletaConsulta(string pacienteParametro)
                {
            return consultaService.DeletaConsultas(pacienteParametro);
        }

    }
}
