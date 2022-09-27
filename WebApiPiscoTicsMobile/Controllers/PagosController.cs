using ReglasDeNegocio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApiPiscoTicsMobile.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/pago")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PagosController : ApiController
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
            PaysRequest pago = new PaysRequest(connection, connectionweb);
            return Ok(pago.oki("-1"));
        }


        [HttpPost]
        [Route("Create")]
        public IHttpActionResult RegistrarPago(PaysRequest pago) {
            PaysRequest pagore = new PaysRequest(connection, connectionweb);
            return Ok(pagore.Registropago(pago));
        }

        [HttpGet]
        [Route("TiposPagos")]
        public IHttpActionResult TiposdePagos()
        {
            PaysRequest pagore = new PaysRequest(connection, connectionweb);
            return Ok(pagore.ListaTipoPago());
        }


        [HttpPost]
        [Route("FacturasPagos")]
        public IHttpActionResult FacturasPagos(string idContrato)
        {
            PaysRequest pagore = new PaysRequest(connection, connectionweb);
            return Ok(pagore.ListaFacturasPagos(idContrato));
        }

        [HttpPost]
        [Route("insertNove")]
        public IHttpActionResult InserNove(NoveltyRequest novedad)
        {
            NoveltyRequest nove = new NoveltyRequest(connection, connectionweb);
            return Ok(nove.RegistroNovedad(novedad));
        }


        [HttpPost]
        [Route("searcPays")]
        public IHttpActionResult Searchbene(string NumberId)
        {
            PaysRequest pagore = new PaysRequest(connection, connectionweb);
            return Ok(pagore.ConsultarPagos(NumberId));
        }

        [HttpPost]
        [Route("searcNovedades")]
        public IHttpActionResult searcNovedades(string NumberId)
        {
            PaysRequest pagore = new PaysRequest(connection, connectionweb);
            return Ok(pagore.ConsultarNovedades(NumberId));
        }

        [HttpPost]
        [Route("searcAdicionales")]
        public IHttpActionResult searcAdicionales(string NumberId)
        {
            PaysRequest pagore = new PaysRequest(connection, connectionweb);
            return Ok(pagore.ConsultarAdicionales(NumberId));
        }

        [HttpGet]
        [Route("Funeraria")]
        public IHttpActionResult FunerariaDatos()
        {
            PaysRequest pagore = new PaysRequest(connection, connectionweb);
            return Ok(pagore.DatosFuneraria());
        }


        [HttpPost]
        [Route("searchpago")]
        public IHttpActionResult searchpagos(string NroPago)
        {
            PaysRequest pagore = new PaysRequest(connection, connectionweb);
            return Ok(pagore.BuscarPago(NroPago));
        }

        [HttpPost]
        [Route("notificarrecibo")]
        public IHttpActionResult notificarrecibo(string NoRecibo)
        {
            PaysRequest pagore = new PaysRequest(connection, connectionweb);
            return Ok(pagore.NotificarPago(NoRecibo));
        }

        [HttpPost]
        [Route("guardamovilestado")] 
        public IHttpActionResult guardamovilestado(string Usuario, string Estado, string Terminal)
        {
            PaysRequest pagore = new PaysRequest(connection, connectionweb);
            return Ok(pagore.GuardaMovilEstado(Usuario, Estado, Terminal));
        }

        [HttpPost] 
        [Route("guardahistoricoimpresion")]
        public IHttpActionResult guardahistoricoimpresion(string IdContrato, string NoRecibo, string Usuario, string Terminal )
        {
            PaysRequest pagore = new PaysRequest(connection, connectionweb);
            return Ok(pagore.GuardaHistoricoImpresion(IdContrato,NoRecibo, Usuario, Terminal));
        }

    }
}
