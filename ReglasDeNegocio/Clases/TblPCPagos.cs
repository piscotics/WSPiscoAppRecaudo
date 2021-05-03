using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelSincronizador
{
    public class TblPCPagos
    {
        int IDGENERADOR { get; set; }
        string IDGENERADORT { get; set; }
        DateTime FECHA { get; set; }
        string IDCONTRATO { get; set; }
        string IDPERSONA { get; set; }
        double VALOR { get; set; }
        double DESCUENTO { get; set; }
        Int16 ANULADO { get; set; }
        string MAQUINA { get; set; }
        string TRANSAC { get; set; }
        string USUARIO { get; set; }
        string OBSERVACIONES { get; set; }
        Int16 ESTADO { get; set; }
        Int16 PUNTOS { get; set; }
        double CUOTAMENSUAL { get; set; }
        string IDENTIFICADORBASE { get; set; }
        string NORECIBO { get; set; }
        DateTime DESDE { get; set; }
        DateTime HASTA { get; set; }
        string IDCOBRADOR { get; set; }
        string DETALLE { get; set; }
        Int16 TM { get; set; }
        string RIFA { get; set; }
        string TITULAR { get; set; }
        string ESTADOCONTRATO { get; set; }
        string TIPOPAGO { get; set; }
        double TOTAL { get; set; }
        string FORMAPAGO { get; set; }
        string MODULO { get; set; }
        double SALDO { get; set; }
        Int16 CODIGOBANCO { get; set; }
        string NROREF { get; set; }
        DateTime FECHAPAGOF { get; set; }
        string NAMPLIACION { get; set; }
        Int16 IDCAJAPAR { get; set; }
    }
}
