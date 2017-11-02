using _Compi2_Proyecto2_201212859.ejecucion_alto_nivel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class si
    {
        public expresion expresion;
        public ambito ambito;
        public List<sino_si> lista_sino_si;
        public sino sino;
        public simbolo padre = null;

        public si(expresion expresion, ambito ambito, List<sino_si> lista_sino_si, sino sino) {
            this.expresion = expresion;
            this.ambito = ambito;
            this.lista_sino_si = lista_sino_si;
            this.sino = sino;
        }

        public String generar3D()
        {
            String codigo = "";
            cadena3D expresion3D = expresion.resCondicion();

            codigo += "/*Inicio de SI*/" + "\r\n";
            if (expresion3D.tipo == "BOOLEANO")
            {
                codigo += expresion3D.codigo;
                codigo += expresion3D.etqVerdadera + ":" + "\r\n";
                pasadas pasadas = new pasadas(ambito.tablaSimbolo);
                codigo += memoria.identar(pasadas.ejecutar());

                String etiqSalida = memoria.getEtq();
                codigo += "goto " + etiqSalida + ";\r\n";
                codigo += expresion3D.etqFalsa + ":" + "\r\n\n";

                if (lista_sino_si != null)
                {
                    foreach (sino_si sino_si in lista_sino_si)
                    {
                        cadena3D expresion3D_sino_si = sino_si.expresion.resCondicion();

                        if (expresion3D.tipo == "BOOLEANO")
                        {
                            codigo += "//Inicio de sino_si" + "\r\n";
                            codigo += expresion3D_sino_si.codigo;
                            codigo += expresion3D_sino_si.etqVerdadera + ":" + "\r\n";
                            pasadas = new pasadas(sino_si.ambito.tablaSimbolo);
                            codigo += memoria.identar(pasadas.ejecutar());
                            codigo += "goto " + etiqSalida + ";\r\n";
                            codigo += expresion3D_sino_si.etqFalsa + ":" + "\r\n";
                            codigo += "//Fin de sino_si" + "\r\n\n";
                        }
                        else
                        {
                            memoria.addError("ERROR SEMANTICO ", "La condicion SI_NO_SI debe ser de tipo booleano", sino_si.expresion.fila, sino_si.expresion.columna);
                        }
                    }
                }

                if (sino != null)
                {
                    codigo += "//Inicio de sino" + "\r\n";
                    pasadas = new pasadas(sino.ambito.tablaSimbolo);
                    codigo += memoria.identar(pasadas.ejecutar());
                    codigo += "//Inicio de sino" + "\r\n";
                }
                codigo += etiqSalida + ":" + "\r\n";
                codigo += "/*Fin de SI*/" + "\r\n\n";
            }
            else {
                memoria.addError("ERROR SEMANTICO ", "La condicion SI debe ser de tipo booleano", expresion.fila, expresion.columna);
            }
            return codigo;
        }
    }
}
