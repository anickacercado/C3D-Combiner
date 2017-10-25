﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class llamadaArregloMetodo
    {
        public String nombre_arreglo;
        public List<expresion> dimensiones; //Dimensiones - Parametros
        public simbolo padre = null;
        public int fila;
        public int columna;

        public llamadaArregloMetodo(String nombre_arreglo, List<expresion> dimensiones, simbolo padre, int fila, int columna)
        {
            this.nombre_arreglo = nombre_arreglo;
            this.dimensiones = dimensiones;
            this.padre = padre;
            this.fila = fila;
            this.columna = columna;
        }
    }
}
