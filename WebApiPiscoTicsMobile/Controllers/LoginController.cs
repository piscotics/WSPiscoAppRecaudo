using ModelSincronizador;
using ReglasDeNegocio;
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
    [RoutePrefix("api/login")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : ApiController
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
            UserRequest usrquest = new UserRequest(connection, connectionweb);
            return Ok(true);
        }


        [HttpGet]
        [Route("userlocale")]
        public IHttpActionResult lstusers()
        {
            UserRequest usrquest = new UserRequest(connection, connectionweb);
            return Ok(usrquest.ListaUsuariosLocal());
        }



        [HttpGet]
        [Route("licenceslocale")]
        public IHttpActionResult lstlicence()
        {
            UserRequest usrquest = new UserRequest(connection, connectionweb);
            return Ok(usrquest.ListaLicencias());
        }



        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(LoginRequest login)
        {
            //Validamos que el login no venga en blanco
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);


            UserRequest usrquest = new UserRequest(connection, connectionweb);
            //TblUsuarios usr = usrquest.ValidateLogin(login);
            TblUsuarios usr = usrquest.ValidateLoginSP(login);

           
            if (usr != null)
            {
                if (login.Password == usr.CLAVE)
                    if(usrquest.ValidateSerial(login.Serial, login.Username))
                        return Ok(usr);
                    else
                        return Ok("Licencia No Registrada");
                else
                    return Unauthorized();

            }
            else
                return Unauthorized();


        }

        [HttpPost]
        [Route("addlicence")]
        public IHttpActionResult AddLicence(string Licence)
        {
            //Validamos que el login no venga en blanco
            if (Licence == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);


            UserRequest usrquest = new UserRequest(connection, connectionweb);
            //TblUsuarios usr = usrquest.ValidateLogin(login);
            bool res = usrquest.AgregarLicencia(Licence);

            return Ok(res);

        }


        [HttpPost]
        [Route("removelicence")]
        public IHttpActionResult DeleteLicence(string Licence)
        {
            //Validamos que el login no venga en blanco
            //Validamos que el login no venga en blanco
            if (Licence == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);


            UserRequest usrquest = new UserRequest(connection, connectionweb);
            //TblUsuarios usr = usrquest.ValidateLogin(login);
            bool res = usrquest.RemoverLicencia(Licence);

            return Ok(res);


        }
    }
}
