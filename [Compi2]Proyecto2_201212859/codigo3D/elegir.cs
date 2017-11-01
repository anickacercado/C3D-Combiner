using _Compi2_Proyecto2_201212859.ejecucion_alto_nivel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class elegir
    {
        public expresion expresion;
        public List<caso> lista_caso;
        public caso defecto;
        public ambito ambito;
        public simbolo padre = null;

        public elegir(expresion expresion, List<caso> lista_caso, caso defecto, ambito ambito) {
            this.expresion = expresion;
            this.lista_caso = lista_caso;
            this.defecto = defecto;
            this.ambito = ambito;
        }
        public String generar3D()
        {
            String codigo = "";
            cadena3D expresion3D = expresion.resCondicion();
            String etiqSalida = memoria.getEtq();

            codigo += "/*Inicio de Elegir*/" + "\r\n";
            codigo += expresion3D.codigo;
            if (lista_caso != null)
            {
                foreach (caso caso in lista_caso)
                {
                    cadena3D expresion_caso = caso.expresion.resCondicion();
                    codigo += expresion_caso.codigo;
                    String etiqFalsa = memoria.getEtq();
                        codigo += "//Inicio de  caso" + "\r\n";
                        codigo += "ifFalse " + expresion3D.temporal + "==" + expresion_caso.temporal + " goto " + etiqFalsa + "; \r\n";
                        pasadas pasadas = new pasadas(caso.ambito.tablaSimbolo);
                        codigo += memoria.identar(pasadas.ejecutar());
                        codigo += etiqFalsa + ": \r\n";
                        codigo += "//Fin de caso" + "\r\n";
                }
            }

            if (defecto != null)
            {
                codigo += "//Inicio Defecto" + "\r\n";
                pasadas pasadas = new pasadas(defecto.ambito.tablaSimbolo);
                codigo += memoria.identar(pasadas.ejecutar());
                codigo += "//Fin Defecto" + "\r\n";
            }
            codigo += etiqSalida + ":" + "\r\n";
            codigo += "/*Fin de Elegir*/" + "\r\n\n";

            codigo = memoria.reemplazar(codigo, "goto " + etiqSalida + ";", "");
            return codigo;
        } 
    }
}
