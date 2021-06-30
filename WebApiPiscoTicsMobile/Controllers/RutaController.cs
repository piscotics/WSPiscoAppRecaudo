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
    [RoutePrefix("api/ruta")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RutaController : ApiController
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

            return Ok("gola ");
        }

        [HttpPost]
        [Route("lstRutaUser")]
        public IHttpActionResult LstPagos(RutaRequest ruta)
        {
            RutaRequest rutarequest = new RutaRequest(connection, connectionweb);
            return Ok(rutarequest.RegistroLstRutas(ruta));
        }
    }
}
