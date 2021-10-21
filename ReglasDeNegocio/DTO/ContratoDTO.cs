using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelSincronizador.DTO
{
    public class ContratoDTO
    {

        public string IdContrato { get; set; }
        public DateTime FechaAfiliacion { get; set; }
        public DateTime? FechaCobertura { get; set; }
        public string EstadoContrato { get; set; }
        public string Cedula { get; set; }
        public string Titular { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Plan { get; set; }
        public double Cuota { get; set; }
        public DateTime? FechaUltimoPago { get; set; }
        public string NoRecibo { get; set; }
        public double Valor { get; set; }

        public string Departamento { get; set; }
        public string Municipio { get; set; }
        
        public string  Barrio { get; set; }
        public string DepartamentoCobro { get; set; }
        public string MunicipioCobro { get; set; }
        public string BarrioCobro { get; set; }
        public string DireccionCobro { get; set; }
        
        public string Email { get; set; }
        public string Nota1 { get; set; }

    }
}
