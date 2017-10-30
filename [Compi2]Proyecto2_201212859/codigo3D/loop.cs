using _Compi2_Proyecto2_201212859.ejecucion_alto_nivel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class loop
    {
        public ambito ambito;
        public simbolo padre = null;

        public loop(ambito ambito) {
            this.ambito = ambito;
        }

        public String generar3D()
        {
            String codigo = "";
            String etqRetorno = memoria.getEtq();

            codigo += "//Inicio del LOOP" + "\r\n";
            codigo += etqRetorno + ":" + "\r\n";

            /*Se concatena las sentencias dentro del mientras*/
            pasadas pasadas = new pasadas(ambito.tablaSimbolo);
            codigo += pasadas.ejecutar();
            /*Se concatena las sentencias dentro del mientras*/

            codigo += "goto " + etqRetorno + ";\n";
            codigo += "//Fin del LOOP" + "\r\n\n";
            return codigo;
        }
    }
}
