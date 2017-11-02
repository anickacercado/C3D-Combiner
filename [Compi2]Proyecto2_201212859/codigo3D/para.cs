using _Compi2_Proyecto2_201212859.ejecucion_alto_nivel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class para
    {
        public simbolo declara_asigna;
        public expresion condicion;
        public simbolo decre_aum;
        public ambito ambito;
        public simbolo padre = null;

        public para(simbolo declara_asigna, expresion condicion, simbolo decre_aum, ambito ambito)
        {
            this.declara_asigna = declara_asigna;
            this.condicion = condicion;
            this.decre_aum = decre_aum;
            this.ambito = ambito;
        }

        public String generar3D()
        {
            String codigo = "";

            codigo += "/*Inicio del ciclo para*/" + "\r\n";
            if (declara_asigna.rol.Equals("DECLARACION")) {
                declaracion declaracion = (declaracion)declara_asigna.valor;
                codigo += declaracion.generar3D();
            }
            else {
                asignacion asignacion = (asignacion)declara_asigna.valor;
                codigo += asignacion.generar3D();
            }

            String etqRetorno = memoria.getEtq();
            cadena3D expresion3D = condicion.resCondicion();
            if (expresion3D.tipo.Equals("BOOLEANO"))
            {
                codigo += etqRetorno + ":" + "\r\n";
                codigo += expresion3D.codigo;
                codigo += expresion3D.etqVerdadera + ":" + "\r\n";

                /*Se concatena las sentencias dentro del for*/
                pasadas pasadas = new pasadas(ambito.tablaSimbolo);
                codigo += memoria.identar(pasadas.ejecutar());
                /*Se concatena las sentencias dentro del for*/

                codigo += ((aumento_decremento)decre_aum.valor).generar3D();

                codigo += "goto " + etqRetorno + ";\r\n";
                codigo += expresion3D.etqFalsa + ":" + "\r\n";
            }
            else
            {
                memoria.addError("ERROR SEMANTICO ", "La condicion debe ser de tipo booleano", condicion.fila, condicion.columna);
            }
            codigo += "/*Fin del ciclo para*/" + "\r\n\n";

            //Goto etiqueta de retorno y salida
            codigo = memoria.reemplazar(codigo, "goto " + expresion3D.etqFalsa + ";", "goto " + etqRetorno + ";");
            //Goto etiqueta de retorno y salida
            return codigo;
        }
    }
}
