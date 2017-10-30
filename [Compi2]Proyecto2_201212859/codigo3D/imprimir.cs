using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class imprimir
    {
        public expresion expresion;
        public simbolo padre = null;

        public imprimir(expresion expresion) {
            this.expresion = expresion;
        }

        public String generar3D()
        {
            String codigo = "";
            cadena3D expresion3D = expresion.resCondicion();
            if (expresion3D.temporal != "") {
                if (expresion3D.tipo == "ENTERO") {
                    codigo += "//Inicia Impresion de entero" + "\r\n";
                    codigo += expresion3D.codigo + "\r\n";
                    codigo += "print(\"%d\"," + expresion3D.temporal + ");" + "\r\n";
                    codigo += "//Finaliza Impresion de entero" + "\r\n\n";
                }
                else if (expresion3D.tipo == "DECIMAL") {
                    codigo += "//Inicia Impresion de decimal" + "\r\n";
                    codigo += expresion3D.codigo + "\r\n";
                    codigo += "print(\"%f\"," + expresion3D.temporal + ");" + "\r\n";
                    codigo += "//Finaliza Impresion de decimal" + "\r\n\n";

                }
                else if (expresion3D.tipo == "BOOLEANO") {
                    codigo += "//Inicia Impresion de booleano" + "\r\n";
                    codigo += expresion3D.codigo + "\r\n";
                    codigo += "print(\"%d\"," + expresion3D.temporal + ");" + "\r\n";
                    codigo += "//Finaliza Impresion de booleano" + "\r\n\n";
                }
                else if (expresion3D.tipo == "CARACTER") {
                    codigo += "//Inicia Impresion de caracter" + "\r\n";
                    codigo += expresion3D.codigo + "\r\n";
                    codigo += "print(\"%c\"," + expresion3D.temporal + ");" + "\r\n";
                    codigo += "//Finaliza Impresion de caracter" + "\r\n\n";
                }
                else if (expresion3D.tipo == "CADENA") {
                    String temp1 = memoria.getTemp();
                    String temp2 = memoria.getTemp();
                    String etq1 = memoria.getEtq();
                    String etq2 = memoria.getEtq();

                    codigo += "//Inicia Impresion de cadena" + "\r\n";
                    codigo += expresion3D.codigo + "\r\n";
                    codigo += temp1 + "=" + expresion3D.temporal + "; //Posicion Heap cadena" + "\r\n";
                    codigo += etq1 + ":" + "\r\n";
                    codigo += temp2 + "=" + "Heap[" + temp1 + "]; //Asignar ascii" + "\r\n";
                    codigo += "if " + temp2 + "==0 goto " + etq2 + ";" + "\r\n";
                    codigo += "print(\"%c\"," + temp2 + ");" + "\r\n";
                    codigo += temp1 + "=" + temp1 + "+1" + ";" + "\r\n";
                    codigo += "goto " + etq1 + ";" + "\r\n";
                    codigo += etq2 + ":" + "\r\n";
                    codigo += "//Finaliza Impresion de cadena" + "\r\n\n";
                }
            }
            else {
                //ERROR SEMANTICO
            }
            return codigo;
        }
    }
}
