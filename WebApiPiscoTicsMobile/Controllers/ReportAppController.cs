using ReglasDeNegocio.DTO;
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
    [RoutePrefix("api/posicion")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ReportAppController : ApiController
    {
        string connection =
      System.Configuration.ConfigurationManager.
      ConnectionStrings["CadenaConex"].ConnectionString;

        string connectionweb =
        System.Configuration.ConfigurationManager.
        ConnectionStrings["CadenaConexWeb"].ConnectionString;


        [HttpPost]
        [Route("lstUser")]
        public IHttpActionResult lstUser(UbicationRequest data)
        {
            UbicationRequest usrquest = new UbicationRequest(connection, connectionweb);
            return Ok(usrquest.ValidatePosicion(data));
        }

        [HttpPost]
        [Route("lstRutas")]
        public IHttpActionResult lstRutas(CuadreCajaRequest data)
        {
            UbicationRequest usrquest = new UbicationRequest(connection, connectionweb);
            return Ok(usrquest.RutaLocal(data.Dato));
        }

        [HttpGet]
        [Route("lstUsuario")]
        public IHttpActionResult oki()
        {
            UserRequest usrquest = new UserRequest(connection, connectionweb);
            return Ok(usrquest.ListaUsuarios());
        }

        //PosicisionDTO pst = new PosicisionDTO();
    }
}
