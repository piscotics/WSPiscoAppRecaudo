using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelSincronizador
{
    public class TblAuditoria
    {
        int IDAUDITORIA  {get; set;}
        DateTime FECHATRANS  {get;set;}
        //string FECHATRANS { get; set; }
        string TIPOTRANS { get; set; }
        string DATOS { get; set; }
        double LONGITUD { get; set; }
        double SOCKET { get; set; }
        string CODMETODO { get; set; }
        string NOMMETODO { get; set; }
        string NOMBREPC { get; set; }
    }
}
