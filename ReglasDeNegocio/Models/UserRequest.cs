using ModelSincronizador;
using ReglasDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WebApiPiscoTicsMobile.Models
{
    public class UserRequest : TblUsuarios
    {
        private string CadenaConexion = string.Empty;
        private string CadenaconexionWeb = string.Empty;

        public UserRequest() { }
        public UserRequest(string Conexion, string ConexionWeb) {
            this.CadenaConexion = Conexion;
            this.CadenaconexionWeb = ConexionWeb;
        }

        public TblUsuarios ValidateLogin(LoginRequest login)
        {
            CatalogoUsuarios catalogo = new CatalogoUsuarios(CadenaconexionWeb);
            TblUsuarios usario = catalogo.ConsultarUsuarioLogin(login.Username, login.Password);
            return usario;

        }

        public Boolean ValidateSerial(String serial, String usuario)
        {
            CatalogoUsuarios catalogo = new CatalogoUsuarios(CadenaconexionWeb);
            return catalogo.ConsultarLicencia(serial , usuario);
        }


        public TblUsuarios ValidateLoginSP(LoginRequest login)
        {
            CatalogoUsuarios catalogo = new CatalogoUsuarios(CadenaconexionWeb);
            TblUsuarios usuario = catalogo.ConsultarUsuarioLoginSP(login.Username, login.Password);
            return usuario;
        }

        public bool AgregarLicencia(String Licencia)
        {
            CatalogoUsuarios catalogo = new CatalogoUsuarios(CadenaconexionWeb);
            bool resultado = catalogo.AgregarLicencia(Licencia);
            return resultado;
        }


        public bool RemoverLicencia(String Licencia)
        {
            CatalogoUsuarios catalogo = new CatalogoUsuarios(CadenaconexionWeb);
            bool resultado = catalogo.RemoverLicencia(Licencia);
            return resultado;
        }


        public List<UserRequest> ListaUsuarios()
        {
            CatalogoUsuarios catalogo = new CatalogoUsuarios(CadenaconexionWeb);
            //CatalogoUsuarios catalogo = new CatalogoUsuarios(@"Server = 190.248.139.202; User = sysdba; Password = masterkey; Charser = NONE; Database=190.248.139.202:D:\pentasystem\PiscoCompanyPruebas\WebServiceAdasysA.fdb");
            List<UserRequest> lstUser = new List<UserRequest>();


            lstUser = catalogo.ConsultarUsuario().Select(c => new UserRequest
            {
                ID = c.ID,
                USERNAME = c.USERNAME,
                //CLAVE = c.CLAVE,
                //ESTADO = c.ESTADO,
                //FECHAINICIAL = c.FECHAINICIAL,
                //FECHAFINAL = c.FECHAFINAL,
                ////HORAINICIAL = c.HORAINICIAL,
                //HORAFINAL = c.HORAFINAL,
                //IDCOBRADOR = c.IDCOBRADOR,
                NOMBRES = c.NOMBRES,
                APELLIDOS = c.APELLIDOS,
                //MAQUINA = c.MAQUINA,
                //NIT = c.NIT,
                //PREFIJO = c.PREFIJO,
                //IDCAJAIND = c.IDCAJAIND,
                //IDCAJAEMP = c.IDCAJAEMP,
                //IDCAJAPAR = c.IDCAJAPAR,
                //IDCAJA = c.IDCAJA,
                //IDCAJAANT = c.IDCAJAANT,
            }).ToList();
            
            return lstUser;
        }


        public List<UserRequest> ListaUsuariosLocal()
        {
            CatalogoUsuarios catalogo = new CatalogoUsuarios(CadenaconexionWeb);
            //CatalogoUsuarios catalogo = new CatalogoUsuarios(@"Server = 190.248.139.202; User = sysdba; Password = masterkey; Charser = NONE; Database=190.248.139.202:D:\pentasystem\PiscoCompanyPruebas\WebServiceAdasysA.fdb");
            List<UserRequest> lstUser = new List<UserRequest>();


            lstUser = catalogo.ConsultarUsuario().Select(c => new UserRequest
            {
                ID = c.ID,
                USERNAME = c.USERNAME,
                CLAVE = c.CLAVE,
                ESTADO = c.ESTADO,
                NOMBRES = c.NOMBRES,
                APELLIDOS = c.APELLIDOS,
                IDCOBRADOR = c.IDCOBRADOR
            }).ToList();

            return lstUser;
        }

        public List<String> ListaLicencias()
        {
            CatalogoUsuarios catalogo = new CatalogoUsuarios(CadenaconexionWeb,CadenaConexion);
            List<string> lstUser = new List<string>();
            lstUser = catalogo.ConsultarLicencias();
            return lstUser;
            
        }


    }
}