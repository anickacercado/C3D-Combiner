using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class llamadaMetodo
    {
        public String nombre_metodo;
        public List<expresion> parametros;
        public simbolo padre = null;
        public int fila;
        public int columna;

        public llamadaMetodo(String nombre_metodo, List<expresion> parametros, simbolo padre, int fila, int columna) {
            this.nombre_metodo = nombre_metodo;
            this.parametros = parametros;
            this.padre = padre;
            this.fila = fila;
            this.columna = columna;
        }
    }
}
