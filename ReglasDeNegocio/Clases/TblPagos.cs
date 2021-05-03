using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelSincronizador
{
    public class TblPagos
    {
        public int IDPAGO { get; set; }
        public string IDGENERADOR { get; set; }
        public string IDGENERADORT { get; set; }
        public DateTime FECHA { get; set; }
        public string IDCONTRATO { get; set; }
        public string IDPERSONA { get; set; }
        public double VALOR { get; set; }
        public double DESCUENTO { get; set; }
        public Int16 ANULADO { get; set; }
        public string MAQUINA { get; set; }
        public string TRANSAC { get; set; }
        public string USUARIO { get; set; }
        public string OBSERVACIONES { get; set; }
        public Int16 ESTADO { get; set; }
        public Int16 PUNTOS { get; set; }
        public double CUOTAMENSUAL { get; set; }
        public string IDENTIFICADORBASE { get; set; }
        public string NORECIBO { get; set; }
        public DateTime DESDE { get; set; }
        public DateTime HASTA { get; set; }
        public string IDCOBRADOR { get; set; }
        public string DETALLE { get; set; }
        public Int16 TM { get; set; }
        public string RIFA { get; set; }
        public string TITULAR { get; set; }
        public string ESTADOCONTRATO { get; set; }
        public string TIPOPAGO { get; set; }
        public double TOTAL { get; set; }
        public string FORMAPAGO { get; set; }
        public string MODULO {get;set;}
        public double SALDO { get; set; }
        public Int16 CODIGOBANCO { get; set; }
        public string NROREF { get; set; }
        public DateTime FECHAPAGOF { get; set; }
        public Int16 IDCAJAIND { get; set; }
        public Int16 IDCAJAEMP { get; set; }
        public string IDGENERADORC { get; set; }
        public string TPAGO { get; set; }
        public float ABONOCONTRATO { get; set; }
        public string CLASEDOC { get; set; }
        public Int16 IDCAJA { get; set; }
        public string POSICIONX { get; set; }
        public string POSICIONY { get; set; }

    }

}
