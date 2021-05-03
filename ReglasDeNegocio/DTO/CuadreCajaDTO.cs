using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReglasDeNegocio.DTO
{
    public class CuadreCajaDTO
    {
        public DateTime FECHA { get; set; }
        public int CANTIDADPAGOS { get; set; }
        public double VALORPAGOS { get; set; }
        public int CANTIDADANULADOS { get; set; }
        public int CANTIDADNOVEDADES { get; set; }
    }
}
