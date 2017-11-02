﻿using _Compi2_Proyecto2_201212859.ejecucion_alto_nivel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class hacer_mientras
    {
        public expresion expresion;
        public ambito ambito;
        public simbolo padre = null;

        public hacer_mientras(expresion expresion, ambito ambito)
        {
            this.expresion = expresion;
            this.ambito = ambito;
        }

        public String generar3D()
        {
            String codigo = "";
            cadena3D expresion3D = expresion.resCondicion();
            pasadas pasadas = new pasadas(ambito.tablaSimbolo);

            if (expresion3D.tipo.Equals("BOOLEANO"))
            {
                codigo += "/*Inicio del ciclo HACER-MIENTRAS*/" + "\r\n";

                //Para el caso de HACER-MIENTRAS(True)
                if (expresion3D.etqVerdadera == "" && expresion3D.etqFalsa == "")
                {
                    expresion3D.etqVerdadera = memoria.getEtq();
                    expresion3D.etqFalsa = memoria.getEtq();

                    codigo += expresion3D.etqVerdadera + ":" + "\r\n";

                    /*Se concatena las sentencias dentro del hacer_mientras*/
                    codigo += memoria.identar(pasadas.ejecutar());                    
                    /*Se concatena las sentencias dentro del hacer_mientras*/

                    codigo += expresion3D.codigo;

                    codigo += "\t" + "if " + expresion3D.temporal + "==1 goto " + expresion3D.etqVerdadera + ";\n";
                    codigo += "\t" + "goto " + expresion3D.etqFalsa + ";\n";
                }

                else {
                    codigo += expresion3D.etqVerdadera + ":" + "\r\n";

                    /*Se concatena las sentencias dentro del hacer_mientras*/
                    codigo += memoria.identar(pasadas.ejecutar());
                    /*Se concatena las sentencias dentro del hacer_mientras*/

                    codigo += expresion3D.codigo;
                }

                codigo += expresion3D.etqFalsa + ":" + "\r\n";
                codigo += "/*Fin del ciclo HACER-MIENTRAS*/" + "\r\n\n";
            }
            else
            {
                memoria.addError("ERROR SEMANTICO ", "La condicion debe ser de tipo booleano", expresion.fila, expresion.columna);
            }

            //Goto etiqueta de retorno y salida
            codigo = memoria.reemplazar(codigo, "goto " + expresion3D.etqFalsa + ";", "goto " + expresion3D.etqVerdadera + ";");
            //Goto etiqueta de retorno y salida
            return codigo;
        }
    }
}
