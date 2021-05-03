using ReglasDeNegocio.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReglasDeNegocio.Models
{
    public class CuadreCajaRequest
    {
        public string Dato { get; set; }
        public DateTime Fecha { get; set; }

        private string CadenaConexion = String.Empty;
        private string CadenaConexionWeb = String.Empty;

        public CuadreCajaRequest() { }

        public CuadreCajaRequest(String _cadenaconex, String _cadenaConxWeb)
        {
            this.CadenaConexion = _cadenaconex;
            this.CadenaConexionWeb = _cadenaConxWeb;
        }


        public CuadreCajaDTO RegistroCuadreCaja(CuadreCajaRequest cuadre)
        {
            CatalogoPagos catalogo = new CatalogoPagos(CadenaConexion, CadenaConexionWeb);
            return catalogo.GrabarCuadreCaja(cuadre);
        }

        public List<string> RegistroLstPagos(CuadreCajaRequest cuadre)
        {
            CatalogoPagos catalogo = new CatalogoPagos(CadenaConexion, CadenaConexionWeb);
            return catalogo.ConsultaLstPagos(cuadre);
        }

    }
}
