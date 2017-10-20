using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class declaracion
    {
        public string nombre;
        public string tipo;
        public string visibilidad;
        public List<expresion> dimensiones;
        public expresion expresion;
        public ambito ambito;
        public int fila;
        public int columna;
        public simbolo padre= null;

        public declaracion(String visibilidad, String tipo, String nombre, List<expresion> dimensiones, ambito ambito, int fila, int columna, expresion expresion)
        {
            this.visibilidad = visibilidad;
            this.tipo = tipo;
            this.nombre = nombre;
            this.dimensiones = dimensiones;
            this.ambito = ambito;
            this.fila = fila;
            this.columna = columna;
            this.expresion = expresion;
        }

    }
}
