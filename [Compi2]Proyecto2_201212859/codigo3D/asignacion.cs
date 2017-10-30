using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class asignacion
    {
        public llamadaObjeto objeto;
        public expresion expresion;
        public ambito ambito;
        public int fila;
        public int columna;
        public simbolo padre = null;


        public asignacion(llamadaObjeto objeto, expresion expresion, ambito ambito, int fila, int columna)
        {
            this.objeto = objeto;
            this.expresion = expresion;
            this.ambito = ambito;
            this.fila = fila;
            this.columna = columna;
        }

        public String generar3D() {
            String codigo = "";
            cadena3D expresion3D = expresion.resCondicion();
            codigo += expresion3D.codigo;
            return codigo;
        }
    }
}
