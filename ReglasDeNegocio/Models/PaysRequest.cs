using ModelSincronizador;
using ModelSincronizador.DTO;
using ReglasDeNegocio.Clases;
using ReglasDeNegocio.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReglasDeNegocio.Models
{
    public class PaysRequest
    {
        public string IDCONTRATO { get; set; }
        public string IDPERSONA  { get; set; }
        public float VALOR { get; set; }
        public float DESCUENTO { get; set; }
        public float CANTIDADCUOTAS { get; set; }
        public string MAQUINA { get; set; }
        public string TRANSAC { get; set; }
        
        public string USUARIO { get; set; }
        public string OBSERVACIONES { get; set; }
        public float CUOTAMENSUAL { get; set; }
        public string ESTADO { get; set; }
        public string FORMAPAGO { get; set; }
        public string FECHAPAGOR { get; set; }
        public string POSX { get; set; }
        public string POSY { get; set; }
        public string NROREF { get; set; }
        public string IDENTIFICADORBASE { get; set; }
        public string TITULAR { get; set; }
        
        public string PLAN { get; set; }


        private string CadenaConexion = String.Empty;
        private string CadenaConexionWeb = String.Empty;
        public PaysRequest() { }

        public PaysRequest(String _cadenaconex, String _cadenaConxWeb)
        {
            this.CadenaConexion = _cadenaconex;
            this.CadenaConexionWeb = _cadenaConxWeb;
        }

        public List<string> ConsultarPagos(string NroContrato)
        {
            CatalogoPagos catalogo = new CatalogoPagos(CadenaConexion, CadenaConexionWeb);
            return catalogo.ConsultaPagos(NroContrato);
        }

        public List<string> ConsultarNovedades(string NroContrato)
        {
            CatalogoPagos catalogo = new CatalogoPagos(CadenaConexion, CadenaConexionWeb);
            return catalogo.ConsultaNovedad(NroContrato);
        }
        public List<string> ConsultarAdicionales(string NroContrato)
        {
            CatalogoPagos catalogo = new CatalogoPagos(CadenaConexion, CadenaConexionWeb);
            return catalogo.ConsultarAdicionales(NroContrato);
        }
        public int oki(string NroContrato)
        {
            CatalogoPagos catalogo = new CatalogoPagos(CadenaConexion, CadenaConexionWeb);
            PagoDTO contract = new PagoDTO();
            //contract = catalogo.GrabarPago();
            return 1;
        }

        public PagoResultDTO Registropago(PaysRequest pago)
        {
            CatalogoPagos catalogo = new CatalogoPagos(CadenaConexion, CadenaConexionWeb);
            PagoDTO contract = new PagoDTO();
            return catalogo.GrabarPago(pago);
             //1;
        }


        public ConsulPagoDTO BuscarPago(string NroPago)
        {
            CatalogoPagos catalogo = new CatalogoPagos(CadenaConexion, CadenaConexionWeb);
            ConsulPagoDTO cpay = new ConsulPagoDTO();
            return catalogo.ConsultarPagoImpresion(NroPago);
        }


        public NotificarReciboDTO NotificarPago(String NoRecibo)
        {
            CatalogoPagos catalogo = new CatalogoPagos(CadenaConexion, CadenaConexionWeb);
            NotificarReciboDTO contract = new NotificarReciboDTO();
            return catalogo.NotificaRecibo(NoRecibo);
        }

        public List<TblTipoPago> ListaTipoPago()
        {
            CatalogoPagos catalogo = new CatalogoPagos(CadenaConexion, CadenaConexionWeb);
            List<TblTipoPago> lst = new List<TblTipoPago>();
            return catalogo.TiposdePagos();
        }


        public TblFuneraria DatosFuneraria()
        {
            CatalogoPagos catalogo = new CatalogoPagos(CadenaConexion, CadenaConexionWeb);
            TblFuneraria funeraria = new TblFuneraria();
            return catalogo.Funeraria();
        }

    }
}
