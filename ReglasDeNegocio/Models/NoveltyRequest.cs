using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReglasDeNegocio.Models
{
    public class NoveltyRequest
    {
        public string Contrato { get; set; }
        public int Novedad { get; set; }
        public int DIAPOST { get; set; }
        public string USUARIO { get; set; }
        public string IDCOBRADOR { get; set; }
        public string MODULO { get; set; }
        public int TRANSAC { get; set; }
        public DateTime FECHAPROGRAMADA { get; set; }
        public string POSX { get; set; }
        public string POSY { get; set; }

        public string Observaciones { get; set; }

        private string CadenaConexion = String.Empty;
        private string CadenaConexionWeb = String.Empty;
        public NoveltyRequest() { }

        public NoveltyRequest(String _cadenaconex, String _cadenaConxWeb)
        {
            this.CadenaConexion = _cadenaconex;
            this.CadenaConexionWeb = _cadenaConxWeb;
        }

        public int RegistroNovedad(NoveltyRequest novedad)
        {
            CatalogoPagos catalogo = new CatalogoPagos(CadenaConexion, CadenaConexionWeb);
            return catalogo.GrabarNovedad(novedad);
            
            ///PagoDTO contract = new PagoDTO();
            //return catalogo.GrabarPago(pago);
            //1;
        }


       
    }
}
