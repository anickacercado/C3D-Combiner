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
        public ambito ambito;
        public int tamanio;
        public int posicion;
        public Object valor;
        public int fila;
        public int columna;


        public simbolo(String nombre, String rol, String tipo, ambito ambito, int tamanio, int posicion, Object valor, int fila, int columna) {
              this.nombre = nombre;
              this.rol = rol;
              this.tipo = tipo;
              this.ambito = ambito;
              this.tamanio = tamanio;
              this.posicion = posicion;
              this.valor = valor;
              this.fila = fila;
              this.columna = columna;
        }
    }
}
