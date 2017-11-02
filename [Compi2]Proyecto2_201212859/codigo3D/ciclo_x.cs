using _Compi2_Proyecto2_201212859.ejecucion_alto_nivel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class ciclo_x
    {

        public expresion expresion1;
        public expresion expresion2;
        public ambito ambito;
        public int fila;
        public int columna;
        public simbolo padre = null;

        public ciclo_x(expresion expresion1, expresion expresion2, int fila, int columna, ambito ambito)
        {
            this.expresion1 = expresion1;
            this.expresion2 = expresion2;
            this.fila = fila;
            this.columna = columna;
            this.ambito = ambito;
        }

        public String generar3D()
        {
            String codigo = "";
            String etq1= memoria.getEtq();
            String etqVerdadera = memoria.getEtq();
            String etqFalsa = memoria.getEtq();
            String etqSalida = memoria.getEtq();

            expresion expresion_or = new expresion(expresion1, expresion2, "||", "||", fila, columna, null);
            expresion expresion_and = new expresion(expresion1, expresion2, "&&", "&&", fila, columna, null);

            cadena3D expresion3D_OR = expresion_or.resCondicion();   

            if (expresion3D_OR.tipo.Equals("BOOLEANO"))
            {
                codigo += "/*Inicio del ciclo X*/" + "\r\n";
                codigo += etq1 + ":" + "\r\n";
                codigo += expresion3D_OR.codigo;
                codigo += expresion3D_OR.etqVerdadera + ":" + "\r\n";
                codigo += memoria.identar("goto " + etqVerdadera + ";\r\n");
                codigo += expresion3D_OR.etqFalsa+ ":" + "\r\n";
                codigo += memoria.identar("goto " + etqSalida + ";\r\n");

                codigo += etqVerdadera + ":" + "\r\n";
                /*Se concatena las sentencias dentro del mientras*/
                pasadas pasadas = new pasadas(ambito.tablaSimbolo);
                codigo += memoria.identar(pasadas.ejecutar());
                /*Se concatena las sentencias dentro del mientras*/
                cadena3D expresion3D_AND = expresion_and.resCondicion();
                codigo += expresion3D_AND.codigo;
                codigo += expresion3D_AND.etqVerdadera + ":" + "\r\n";
                codigo += memoria.identar("goto " + etqVerdadera + ";\r\n");
                codigo += expresion3D_AND.etqFalsa + ":" + "\r\n";
                codigo += memoria.identar("goto " + etqSalida + ";\r\n");

                codigo += etqSalida + ":" + "\r\n";
                codigo += "/*Fin del ciclo X*/" + "\r\n\n";
            }
            else
            {
                memoria.addError("ERROR SEMANTICO ", "La condicion debe ser de tipo booleano", fila, columna);
            }
            //Goto etiqueta de retorno y salida
            codigo = memoria.reemplazar(codigo, "goto " + etqSalida + ";", "goto " + etq1 + ";");
            //Goto etiqueta de retorno y salida
            return codigo;
        }

    }
}
