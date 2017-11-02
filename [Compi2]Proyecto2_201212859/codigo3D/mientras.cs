using _Compi2_Proyecto2_201212859.ejecucion_alto_nivel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class mientras
    {
        public expresion expresion;
        public ambito ambito;
        public simbolo padre = null;

        public mientras(expresion expresion, ambito ambito) {
            this.expresion = expresion;
            this.ambito = ambito;
        }

        public String  generar3D()
        {
            String codigo = "";
            String etqRetorno = memoria.getEtq();
            cadena3D expresion3D = expresion.resCondicion();

            if (expresion3D.tipo.Equals("BOOLEANO"))
            {
                codigo += "/*Inicio del ciclo Mientras*/" + "\r\n";
                codigo += etqRetorno + ":" + "\r\n";
                codigo += expresion3D.codigo;

                //Para el caso de While(True)
                if (expresion3D.etqVerdadera == "" && expresion3D.etqFalsa == "") {
                    expresion3D.etqVerdadera = memoria.getEtq();
                    expresion3D.etqFalsa = memoria.getEtq();

                    codigo += "\t" + "if " + expresion3D.temporal + "==1 goto " + expresion3D.etqVerdadera + ";\r\n";
                    codigo += "\t" + "goto " + expresion3D.etqFalsa + ";\r\n";
                }
                codigo += expresion3D.etqVerdadera + ":" + "\r\n";

                /*Se concatena las sentencias dentro del mientras*/
                pasadas pasadas = new pasadas(ambito.tablaSimbolo);
                codigo += memoria.identar(pasadas.ejecutar());
                /*Se concatena las sentencias dentro del mientras*/

                codigo += "goto " + etqRetorno + ";\r\n";
                codigo += expresion3D.etqFalsa + ":" + "\r\n";
                codigo += "/*Fin del ciclo Mientras*/" + "\r\n\n";
            }
            else {
                memoria.addError("ERROR SEMANTICO ", "La condicion debe ser de tipo booleano", expresion.fila, expresion.columna);
            }

            //Goto etiqueta de retorno y salida
            codigo = memoria.reemplazar(codigo, "goto " + expresion3D.etqFalsa + ";", "goto " + etqRetorno + ";");
            //Goto etiqueta de retorno y salida
            return codigo;
        }
    }
}
