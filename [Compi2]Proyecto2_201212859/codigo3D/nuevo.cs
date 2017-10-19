using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class nuevo
    {
        public String nombre;
        public List<expresion> parametros;
        simbolo padre;
        public int fila;
        public int columna;

        public nuevo(String nombre, List<expresion> parametros, simbolo padre, int fila, int columna)
        {
            this.nombre = nombre;
            this.parametros = parametros;
            this.padre = padre;
            this.fila = fila;
            this.columna = columna;
        }
    }
}
