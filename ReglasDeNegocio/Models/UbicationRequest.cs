using ModelSincronizador;
using ReglasDeNegocio.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReglasDeNegocio.Models
{
    public class UbicationRequest
    {
        private string CadenaConexion = String.Empty;
        private string CadenaConexionWeb = String.Empty;

        public String Usuario { get; set; }
        public DateTime Fecha { get;set; }

        public UbicationRequest(String _cadenaconex, String _cadenaConxWeb)
        {
            this.CadenaConexion = _cadenaconex;
            this.CadenaConexionWeb = _cadenaConxWeb;
        }



        public List<PosicisionDTO> ValidatePosicion(UbicationRequest data)
        {
            CatalogoUsuarios catalogo = new CatalogoUsuarios(CadenaConexionWeb, CadenaConexion);
            List<PosicisionDTO> usario = catalogo.ListaUbicaciones(data);
            return usario;

        }


        public List<TblRutas> RutaLocal(string Cobrador)
        {
            CatalogoUsuarios catalogo = new CatalogoUsuarios(CadenaConexionWeb, CadenaConexion);
            List<TblRutas> ruta = catalogo.LstRutaSP(Cobrador);
            
            return ruta;
        }
    }
}
