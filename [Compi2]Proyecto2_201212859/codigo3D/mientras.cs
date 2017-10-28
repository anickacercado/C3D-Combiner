using _Compi2_Proyecto2_201212859.ejecucion_alto_nivel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class mientras
    {
        public expresion expresion;
        public ambito ambito;
        public simbolo padre = null;

        public mientras(expresion expresion, ambito ambito) {
            this.expresion = expresion;
            this.ambito = ambito;
        }

        public void generar3D()
        {

            String codigo = "";
            cadena3D expresion3D = expresion.resCondicion(); 
            if (expresion.tipo.Equals("BOOLEANO")) {



            }

            memoria.cadena3D += expresion3D.codigo;
            pasadas pasadas = new pasadas(ambito.tablaSimbolo);
            pasadas.ejecutar();
        }
    }
}
