using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReglasDeNegocio.Models
{
    public class UpdateContratoRequest
    {
        public string contrato { get; set; }
        public string CEDULA { get; set; }
        public string email { get; set; }
        public string departamento { get; set; }
        public string Ciudad { get; set; }
        public string BARRIO { get;set; }
        public string DIRECCION { get; set; }
        public string telefono { get; set; }
        public string movil { get; set; }
        public string departamentocobro { get; set; }
        public string CiudadCobro { get; set; }
        public string BARRIOCOBRO { get; set; }
        public string DIRECCIONCobro { get; set; }
        public string USUARIO { get; set; }
        public string POSX { get; set; }
        public string POSY { get; set; }
    }
}
