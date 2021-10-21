using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReglasDeNegocio.DTO
{
    public class ConsulPagoDTO
    {
        public string Contrato { get; set; }
        public double Cuota { get; set;}
        public string Cedula { get; set; }
        public string FechaPago { get; set; }

        public string Nombre { get; set; }
        public double Total { get; set; }
        public string PagoHasta { get; set; }
        public string PagoDesde { get; set; }
        public string NumeroDocumento { get; set; }
        public string Usuario { get; set; }
        public string Terminal { get; set; }
        public string Observaciones { get; set; }

        public string Concepto { get; set; }

        public string Anulado { get; set; }
        public string PVisita { get; set; }
        public string Valorenletras { get; set; }


        public string Departamento { get; set; }
        public string Municipio { get; set; }

        public DateTime? Vdesde { get; set; }
        public DateTime? Vhasta { get; set; }
        public double? VlrCto { get; set; }
        public double? VlrSaldo { get; set; }
        public double? VlrDctoPago { get; set; }
        public double? VlrIva { get; set; }

        public string FormaPago { get; set; }
        public string NROREF { get; set; }
        
    }
}
