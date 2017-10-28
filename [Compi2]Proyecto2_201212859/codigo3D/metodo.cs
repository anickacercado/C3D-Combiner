using System;
using System.Collections.Generic;
using _Compi2_Proyecto2_201212859.ejecucion_alto_nivel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class metodo
    {
        public string visibilidad;
        public string tipo;
        public string nombre;
        public List<simbolo> parametros;
        public int dimensiones;
        public ambito ambito;
        public int fila;
        public int columna;
        public simbolo padre = null;

        public metodo(String visibilidad, String tipo, int dimensiones, String nombre, List<simbolo> parametros, ambito ambito, int fila, int columna)
        {
            this.visibilidad = visibilidad;
            this.tipo = tipo;
            this.dimensiones = dimensiones;
            this.nombre = nombre;
            this.parametros = parametros;
            this.ambito = ambito;
            this.fila = fila;
            this.columna = columna;
        }

        public void generar3D() {
            pasadas pasadas = new pasadas(ambito.tablaSimbolo);
            pasadas.ejecutar();
        }
    }
}
