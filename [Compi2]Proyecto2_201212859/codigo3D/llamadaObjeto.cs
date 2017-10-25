using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class llamadaObjeto
    {
        public llamadaObjeto hijo;
        /*VARIABLE, METODO, ARREGLO, ARREGLO_METODO*/
        public String tipo; 
        public String nombre_variable;
        public llamadaMetodo llamadaMetodo;
        public llamadaArreglo llamadaArreglo;
        public llamadaArregloMetodo llamadaArregloMetodo;
        public simbolo padre = null;
        public int fila;
        public int columna;
   
        public llamadaObjeto(llamadaObjeto hijo, String tipo, String nombre_variable, llamadaMetodo llamadaMetodo, llamadaArreglo llamadaArreglo, llamadaArregloMetodo llamadaArregloMetodo, simbolo padre, int fila, int columna) {
            this.hijo = hijo;
            this.tipo = tipo;
            this.nombre_variable = nombre_variable;
            this.llamadaMetodo = llamadaMetodo;
            this.llamadaArreglo = llamadaArreglo;
            this.llamadaArregloMetodo = llamadaArregloMetodo;
            this.padre = padre;
            this.fila = fila;
            this.columna = columna;      
        }

        public void set_hijo(llamadaObjeto hijo)
        {
            if (this.hijo == null)
            {
                this.hijo = hijo;
            }
            else
            {
                this.hijo.set_hijo(hijo);
            }
        }
    }
}
