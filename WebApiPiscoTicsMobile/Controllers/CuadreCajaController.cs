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
    [RoutePrefix("api/cuadre")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CuadreCajaController : ApiController
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
        [Route("cuadre")]
        public IHttpActionResult ByNumberId(CuadreCajaRequest cuadre)
        {
            CuadreCajaRequest cuadrerequest = new CuadreCajaRequest(connection, connectionweb);
            return Ok(cuadrerequest.RegistroCuadreCaja(cuadre));
        }

        [HttpPost]
        [Route("lstPagoUser")]
        public IHttpActionResult LstPagos(CuadreCajaRequest cuadre)
        {
            CuadreCajaRequest cuadrerequest = new CuadreCajaRequest(connection, connectionweb);
            return Ok(cuadrerequest.RegistroLstPagos(cuadre));
        }

    }
}
