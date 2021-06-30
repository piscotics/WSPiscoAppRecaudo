using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReglasDeNegocio.Models
{
    public class RutaRequest
    {

        public string Dato { get; set; }
        public DateTime Fecha { get; set; }

        private string CadenaConexion = String.Empty;
        private string CadenaConexionWeb = String.Empty;

        public RutaRequest() { }

        public RutaRequest(String _cadenaconex, String _cadenaConxWeb)
        {
            this.CadenaConexion = _cadenaconex;
            this.CadenaConexionWeb = _cadenaConxWeb;
        }


       

        public List<string> RegistroLstRutas(RutaRequest ruta)
        {
            CatalogoRutas catalogo = new CatalogoRutas(CadenaConexion, CadenaConexionWeb);
            return catalogo.ConsultaLstRutas(ruta);
        }
    }
}
