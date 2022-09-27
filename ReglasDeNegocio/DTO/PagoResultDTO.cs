using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReglasDeNegocio.DTO
{
    public class PagoResultDTO
    {
        public string NroRecibo { get; set; }
        public string Desde { get; set; }
        public string Hasta { get; set; }
        public string Concepto { get; set; }
        public string DetallePago { get; set; }
        public string Respuesta { get; set; }
        public string Anulado { get; set; }
        public string PVisita { get; set; }
        public string Valorenletras { get; set; }
        public DateTime? Vdesde { get; set; }
        public DateTime? Vhasta { get; set; }
        public double? VlrCto { get; set; }
        public double? VlrSaldo { get; set; } 
        public double? VlrDctoPago { get; set; }
        public double? VlrIva { get; set; }

        public string IdContrato { get; set; }

        public string IdPersona { get; set; }

        public string NitEmpresa { get; set; }
        public string Empresa { get; set; }
        public string TelefonoEmpresa { get; set; }
        public string DireccionEmpresa { get; set; }
        public string CiudadEmpresa { get; set; }

    }
}
