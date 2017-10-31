using System;
using _Compi2_Proyecto2_201212859.ejecucion_alto_nivel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class clase
    { 
        public String nombre;
        public String herencia;
        public ambito ambito;
        public int fila;
        public int columna;
        public simbolo padre = null;

        public clase(String nombre, String herencia, ambito ambito, int fila, int columna) {
            this.nombre = nombre;
            this.ambito = ambito;
            this.herencia = herencia;
            this.fila = fila;
            this.columna = columna;
        }

        public void generar3D()
        {
            pasadas pasadas = new pasadas(ambito.tablaSimbolo);
            pasadas.ejecutar();
        }
    }
}
