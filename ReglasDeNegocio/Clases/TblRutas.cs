using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelSincronizador
{
    public class TblRutas
    {
            public int IDRUTA {get; set;}
            public string USUARIO {get; set;}
            public string IDCOBRADOR {get; set;}
            public string IDCONTRATO{get; set;}
            public string CEDULA{get; set;}
            public string TITULAR{get; set;}
            public string DIRECCION{get; set;}
            public string TELEFONO{get; set;}
            public string CIUDAD{get; set;}
            public Int16 DIACOBRO1{get; set;}
            public Int16 DIACOBRO2{get; set;}
            public string ESTADO{get; set;}
            public Int16 NOVEDAD{get; set;}
            public Int16 POSTFECHADODIA{get; set;}
            public Int16 INDICE{get; set;}
            public float CUOTA{get; set;}
            public Int16 PENDIENTE{get; set;}
            public string ESTADOCONTRATO{get; set;}
            public DateTime FECHAR{get; set;}
            public string BASEDATOS{get; set;}
            public string MODULO{get; set;}
            
            public String EMPRESA { get; set; }
            public string NIT { get; set; }
            
            public string DIRECCIONCOBRO { get; set; }
            public string BOXCONTRATANTE { get; set; }
            
            public float VALORCARTERA { get; set; }
            public float VALORSEGURO { get; set; }
            public string CELULAR { get; set; }
            public DateTime PAGOHASTA { get; set; }
            public string DEPTOC { get; set; }
            public string MPIOC { get; set; }
            public string BARRIOC { get; set; }
            public string MOTIVO { get; set; }

            public string FECHAPROGRAMADA { get; set; }
            public string CODBARRIO { get; set; }
            public string COBERTURA { get; set; }

            public string ULTIMOSPAGOS { get; set; }
            public string BENEFICIARIOS { get; set; }
            public DateTime FECHAAFILIACION { get; set; }
            public string NOMBREPLAN { get; set; }
        
    }


}
