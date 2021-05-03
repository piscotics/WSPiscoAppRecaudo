using ModelSincronizador.DTO;
using ReglasDeNegocio;
using ReglasDeNegocio.DTO;
using ReglasDeNegocio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiPiscoTicsMobile.Models
{
    public class ContractRequest : ContratoDTO
    {
        private string CadenaConexion = String.Empty;
        private string CadenaConexionWeb = String.Empty;

        public ContractRequest() { }


        public ContractRequest(String _cadenaconex, String _cadenaConxWeb) {
            this.CadenaConexion = _cadenaconex;
            this.CadenaConexionWeb = _cadenaConxWeb;
        }


        public ContratoDTO oki(string NroContrato)
        {
            CatalogoContrato catalogo = new CatalogoContrato(CadenaConexion);
            ContratoDTO contract = new ContratoDTO();
            contract = catalogo.ConsultarContrato("1922");
            return contract;
        }

        public ContratoDTO ConsultarContratoNumero(string NroContrato)
        {
            CatalogoContrato catalogo = new CatalogoContrato(CadenaConexion);
            ContratoDTO contract = new ContratoDTO();
            contract = catalogo.ConsultarContratoSP(NroContrato);
            return contract;
        }

        public List<ContratoDTO> ConsultarContratoNumeroCedula(string NroCedula)
        {
            CatalogoContrato catalogo = new CatalogoContrato(CadenaConexion);
            List<ContratoDTO> contract = new List<ContratoDTO>();
            contract = catalogo.ConsultarContratoxCedulaSP(NroCedula);
            return contract;
        }

        public List<String> ConsultarDepartamento()
        {
            CatalogoContrato catalogo = new CatalogoContrato(CadenaConexion);
            List<String> LstDepto = new List<string>();
            ///LstDepto = catalogo.Consu
            LstDepto = catalogo.ConsultaDepartamentosSP();
            return LstDepto;
        }


        public List<String> ConsultarMunicipio(string Dpto) {
            CatalogoContrato catalogo = new CatalogoContrato(CadenaConexion);
            List<String> LstMunicipio = new List<string>();
            LstMunicipio = catalogo.ConsultarMunicipiosSP(Dpto);
            return LstMunicipio;
        }

       

        public List<String> VerBeneficiarios(string NroCedula)
        {
            CatalogoContrato catalogo = new CatalogoContrato(CadenaConexion);
            List<String> contract = new List<String>();
            contract = catalogo.ConsultarBeneficiarios(NroCedula);
            return contract;
        }

        public string ActualizarDatosContrato(UpdateContratoRequest contracto)
        {
            CatalogoContrato catalogo = new CatalogoContrato(CadenaConexion);
            String contract = "";
            contract = catalogo.ActualizarContrato(contracto);
            return contract;
        }

        public List<TipoNovedadDTO> VerTipoNovedades()
        {
            CatalogoContrato catalogo = new CatalogoContrato(CadenaConexion, CadenaConexionWeb);
            List<TipoNovedadDTO> tnovedaddes = new List<TipoNovedadDTO>();
            tnovedaddes = catalogo.ConsultarTipoNovedades();
            return tnovedaddes;

        }


    }
}