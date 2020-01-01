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
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
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

        [HttpDelete("{dataInicialParametro}")]
        public string DeletaConsulta(DateTime dataInicialParametro)
                {
            return consultaService.DeletaConsultas(dataInicialParametro);
        }

        [HttpPut]
        public string AtualizaConsulta([FromBody]ConsultaViewModel[] dataInicialParametro)
        {
            return consultaService.AtualizaConsultas(dataInicialParametro);
        }

    }
}
