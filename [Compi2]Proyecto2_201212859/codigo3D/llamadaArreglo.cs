using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class llamadaArreglo
    {
        public String nombre_arreglo;
        public List<expresion> dimensiones;
        public simbolo padre;
        public int fila;
        public int columna;

        public llamadaArreglo(String nombre_arreglo, List<expresion> dimensiones, simbolo padre, int fila, int columna)
        {
            this.nombre_arreglo = nombre_arreglo;
            this.dimensiones = dimensiones;
            this.padre = padre;
            this.fila = fila;
            this.columna = columna;
        }
    }
}
