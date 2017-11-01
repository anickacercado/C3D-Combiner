using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class caso
    {
        public expresion expresion;
        public ambito ambito;
        public int fila;
        public int columna;
        public simbolo padre = null;

        public caso(expresion expresion, ambito ambito) {
            this.expresion = expresion;
            this.ambito = ambito;
            this.fila = fila;
            this.columna = columna;
        }

        //Para el defecto
        public caso(ambito ambito)
        {
            this.ambito = ambito;
        }
    }
}
