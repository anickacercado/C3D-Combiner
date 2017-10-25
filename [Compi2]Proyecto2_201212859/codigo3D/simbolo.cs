using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class simbolo
    {

        public String nombre;
        public String rol;
        public String tipo;
        public String visibilidad;
        public ambito ambito;
        public int tamanio;
        public int posicion;
        public simbolo hermano = null;
        public simbolo padre = null;
        public Object valor;
        public int fila;
        public int columna;


        public simbolo(String visibilidad, String tipo, String nombre, String rol, int fila, int columna, ambito ambito, Object valor)
        {
            this.posicion = -1;
            this.visibilidad = visibilidad;
            this.tipo = tipo;
            this.nombre = nombre;
            this.rol = rol;
            this.fila = fila;
            this.columna = columna;
            this.ambito = ambito;
            this.valor = valor;
            this.tamanio = this.ambito.tamanio;
        }

    }
}
