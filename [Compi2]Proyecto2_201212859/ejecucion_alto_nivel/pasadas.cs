using _Compi2_Proyecto2_201212859.codigo3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.ejecucion_alto_nivel
{
    class pasadas
    {
        List<simbolo> tablaSimbolo;

        public pasadas(List<simbolo> tablaSimbolo) {
            this.tablaSimbolo = tablaSimbolo;
        }

        public pasadas() {
            this.tablaSimbolo = new List<simbolo>();
        }

        public String ejecutar()
        {
            String codigo = "";
            for (int i =0; i<tablaSimbolo.Count(); i++)
            {
                if (tablaSimbolo[i].rol.Equals("CLASE"))
                {
                    clase clase = (clase)tablaSimbolo[i].valor;
                    codigo += clase.generar3D();
                }

                else if (tablaSimbolo[i].rol.Equals("DECLARACION"))
                {
                    declaracion declaracion = (declaracion)tablaSimbolo[i].valor;
                    codigo += declaracion.generar3D();
                }
                else if (tablaSimbolo[i].rol.Equals("METODO"))
                {
                    metodo metodo = (metodo)tablaSimbolo[i].valor;
                    codigo += metodo.generar3D();
                }
                else if (tablaSimbolo[i].rol.Equals("CONSTRUCTOR"))
                {
                    metodo metodo = (metodo)tablaSimbolo[i].valor;
                    codigo += metodo.generar3D();
                }
                else if (tablaSimbolo[i].rol.Equals("MIENTRAS"))
                {
                    mientras metodo = (mientras)tablaSimbolo[i].valor;
                    codigo += metodo.generar3D();
                }
                else if (tablaSimbolo[i].rol.Equals("ASIGNACION"))
                {
                    asignacion asignacion = (asignacion)tablaSimbolo[i].valor;
                    codigo += asignacion.generar3D();
                }
                else if (tablaSimbolo[i].rol.Equals("HACER_MIENTRAS"))
                {
                    hacer_mientras hacer_mientras = (hacer_mientras)tablaSimbolo[i].valor;
                    codigo += hacer_mientras.generar3D();
                }
                else if (tablaSimbolo[i].rol.Equals("REPETIR_HASTA"))
                {
                    repetir_hasta repetir_hasta = (repetir_hasta)tablaSimbolo[i].valor;
                    codigo += repetir_hasta.generar3D();
                }
                else if (tablaSimbolo[i].rol.Equals("LOOP"))
                {
                    loop loop = (loop)tablaSimbolo[i].valor;
                    codigo += loop.generar3D();
                }
                else if (tablaSimbolo[i].rol.Equals("IMPRIMIR"))
                {
                    imprimir imprimir = (imprimir)tablaSimbolo[i].valor;
                    codigo += imprimir.generar3D();
                }
                else if (tablaSimbolo[i].rol.Equals("IMPRIMIR"))
                {
                    imprimir imprimir = (imprimir)tablaSimbolo[i].valor;
                    codigo += imprimir.generar3D();
                }
                else if (tablaSimbolo[i].rol.Equals("PARA"))
                {
                    para para = (para)tablaSimbolo[i].valor;
                    codigo += para.generar3D();
                }
                else if (tablaSimbolo[i].rol.Equals("ELEGIR"))
                {
                    elegir elegir = (elegir)tablaSimbolo[i].valor;
                    codigo += elegir.generar3D();
                }
                else if (tablaSimbolo[i].rol.Equals("SI"))
                {
                    si elegir = (si)tablaSimbolo[i].valor;
                    codigo += elegir.generar3D();
                }
                else if (tablaSimbolo[i].rol.Equals("CONTINUAR"))
                {
                    codigo += "~CONTINUAR~" + "//Continuar \r\n\n";
                }
                else if (tablaSimbolo[i].rol.Equals("SALIR"))
                {
                    codigo += "~SALIR~" + "//Salir \r\n\n";
                }
                else if (tablaSimbolo[i].rol.Equals("RETORNAR"))
                {
                    expresion expresion = (expresion)tablaSimbolo[i].valor;
                    cadena3D expresion3D = expresion.resCondicion();

                    codigo += "/*Inicio de retorno*/" + "\r\n";
                    codigo += expresion3D.codigo + "\r\n";
                    String temp = memoria.getTemp();
                    codigo += temp + "=P+1" + ";" + "\r\n";
                    codigo += "Stack[" + temp + "]=" + expresion3D.temporal + ";" + "\r\n";
                    codigo += "~RETORNAR~" + "//Retornar \r\n";
                    codigo += "/*Fin de retorno*/" + "\r\n\n";           
                }
            }
            return codigo;
        }
    }
}
