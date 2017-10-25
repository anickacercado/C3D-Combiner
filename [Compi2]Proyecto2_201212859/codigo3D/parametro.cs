using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class parametro
    {
        public string tipo;
        public string nombre;
        public int dimensiones;
        public int fila;
        public int columna;
        public simbolo padre=null;

        public parametro(string tipo, string nombre, int dimensiones, int fila, int columna) {
            this.tipo = tipo;
            this.nombre = nombre;
            this.dimensiones = dimensiones;
            this.fila = fila;
            this.columna = columna;
        }

    }
}
