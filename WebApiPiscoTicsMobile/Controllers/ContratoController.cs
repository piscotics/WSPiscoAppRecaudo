using ReglasDeNegocio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiPiscoTicsMobile.Models;

namespace WebApiPiscoTicsMobile.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/contrato")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ContratoController : ApiController
    {

        string connection =
        System.Configuration.ConfigurationManager.
        ConnectionStrings["CadenaConex"].ConnectionString;

        string connectionweb =
        System.Configuration.ConfigurationManager.
        ConnectionStrings["CadenaConexWeb"].ConnectionString;



        [HttpGet]
        [Route("oki")]
        public IHttpActionResult oki()
        {
            ContractRequest contractrequest = new ContractRequest(connection, connectionweb);
            return Ok(contractrequest.ConsultarContratoNumero(""));
        }


        [HttpPost]
        [Route("searchnumbercontract")]
        public IHttpActionResult ByNumberContract(string NumberContract)
        {
            ContractRequest contractrequest = new ContractRequest(connection, connectionweb);
            return Ok(contractrequest.ConsultarContratoNumero(NumberContract));
        }

        [HttpPost]
        [Route("searchnumberid")]
        public IHttpActionResult ByNumberId(string NumberId)
        {
            ContractRequest contractrequest = new ContractRequest(connection, connectionweb);
            return Ok(contractrequest.ConsultarContratoNumeroCedula(NumberId));
        }

        [HttpPost]
        [Route("searchBene")]
        public IHttpActionResult Searchbene(string NumberId)
        {
            ContractRequest contractrequest = new ContractRequest(connection, connectionweb);
            return Ok(contractrequest.VerBeneficiarios(NumberId));
        }



        [HttpGet]
        [Route("departamentos")]
        public IHttpActionResult departamentos()
        {
            ContractRequest contractrequest = new ContractRequest(connection, connectionweb);
            return Ok(contractrequest.ConsultarDepartamento());
        }


        [HttpPost]
        [Route("municipios")]
        public IHttpActionResult municipios(string Departamento)
        {
            ContractRequest contractrequest = new ContractRequest(connection, connectionweb);
            return Ok(contractrequest.ConsultarMunicipio(Departamento));
        }


        [HttpGet]
        [Route("tipoNovedad")]
        public IHttpActionResult tipoNovedad()
        {
            ContractRequest contractrequest = new ContractRequest(connection, connectionweb);
            return Ok(contractrequest.VerTipoNovedades());
        }


        [HttpPost]
        [Route("updatecontract")]
        public IHttpActionResult UpdateDataContract(UpdateContratoRequest contrato)
        {
            ContractRequest contractrequest = new ContractRequest(connection, connectionweb);
            return Ok(contractrequest.ActualizarDatosContrato(contrato));
        }
    }
}
