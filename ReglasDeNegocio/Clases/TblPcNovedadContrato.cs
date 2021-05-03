using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelSincronizador
{
    public class TblPcNovedadContrato
    {
        int IDNOVEDADCONTRATO { get; set; }
        int IDNOVEDAD { get; set; }
        string IDCONTRATO { get; set; }
        DateTime FECHANOVEDAD { get; set; }
        Int16 POSTFECHADODIA { get; set; }
        Int16 APLICADA { get; set; }
        DateTime FECHAN { get; set; }
        string USUARIO { get; set; }
        string IDCOBRADOR { get; set; }
        string MODULO { get; set; }
        int TRANSAC { get; set; }
    }
}
