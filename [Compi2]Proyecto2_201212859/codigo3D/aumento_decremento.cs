using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class aumento_decremento
    {
        //Recibir Variable
        public llamadaObjeto llamadaObjeto; 
        public String tipo;
        public int fila;
        public int columna;
        public ambito ambito;
        public simbolo padre = null;

        public aumento_decremento(llamadaObjeto llamadaObjeto, String tipo, ambito ambito, int fila, int columna) {
            this.llamadaObjeto = llamadaObjeto;
            this.tipo = tipo;
            this.ambito = ambito;
            this.fila = fila;
            this.columna = columna;
        }

        public String generar3D()
        {
            String codigo = "";
            expresion expresion_llamada_objeto = new expresion(null, null, "LLAMADA_OBJETO", "LLAMADA_OBJETO", fila, fila, llamadaObjeto);
            expresion expresion_tipo = new expresion(expresion_llamada_objeto, null, tipo, tipo, fila, fila, null);
            codigo += expresion_tipo.resCondicion().codigo;
            return codigo;
        }
    }
}
