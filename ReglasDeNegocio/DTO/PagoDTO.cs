using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReglasDeNegocio.DTO
{
    public class PagoDTO
    {
        public DateTime FECHA { get; set; }
        public String IDCONTRATO { get; set; }
        public String IDPERSONA { get; set; }
        public float VALOR { get; set; }
        public float DESCUENTO { get; set; }
        public Int16 ANULADO { get; set; }
        public String MAQUINA { get; set; }
        public String TRANSAC {get;set;}
        public String USUARIO { get; set; }
        public string OBSERVACIONES { get; set; }
        public float CUOTAMENSUAL { get; set; }
        public DateTime DESDE { get; set; }
        public DateTime HASTA { get; set; }
        public String IDENTIFICADORBASE { get; set; }
        public Int16 PUNTOS { get; set; }
        public String TITULAR { get; set; }
        public String ESTADO { get; set; }
        public String TIPOPAGO { get; set; }
        public String FORMAPAGO { get; set; }
        public DateTime FECHAPAGOR { get; set; }
        public String TPAGO { get; set; }
        public float SALDOC { get; set; }
        public String POSX { get; set; }
        public String POSY { get; set; }
        public String NROREF { get; set; }
        
    }
}
