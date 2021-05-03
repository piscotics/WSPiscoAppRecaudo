using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelSincronizador
{
    public class TblActivacionIMEI
    {
        public int IDACTIVACIONIMEI { get; set; }
        public DateTime FECHAREGISTRO { get; set; }
        public string IMEI { get; set; }
        public string ESTADO { get; set; }
        public string USUARIO { get; set; }
    }
}
