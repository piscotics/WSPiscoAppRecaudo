using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelSincronizador
{
    public class TblUsuarios
    {
        public Int16 ID { get; set; }
        public string USERNAME { get; set; }
        public string CLAVE { get; set; }
        public string ESTADO { get; set; }
        public DateTime? FECHAINICIAL { get; set; }
        public DateTime? FECHAFINAL { get; set; }
        public DateTime? HORAINICIAL { get; set; }
        public DateTime? HORAFINAL { get; set; }
        public string IDCOBRADOR { get; set; }
        public string NOMBRES { get; set; }
        public string APELLIDOS { get; set; }
        public string MAQUINA { get; set; }
        public string NIT { get; set; }
        public string PREFIJO { get; set; }
        public Int16 IDCAJAIND { get; set; }
        public Int16 IDCAJAEMP { get; set; }
        public Int16 IDCAJAPAR { get; set; }
        public Int16 IDCAJA { get; set; }
        public Int16 IDCAJAANT { get; set; }

    }
}
