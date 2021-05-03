using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReglasDeNegocio.DTO
{
    public class PosicisionDTO
    {
        public String Id { get; set; }
        public String PosX { get; set; }
        public String Posy { get; set; }
        public String Tipo { get; set; }

        public String Nombre { get; set; }
        public String IdContrato { get; set; }
        public Double Valor { get; set; }
    }
}
