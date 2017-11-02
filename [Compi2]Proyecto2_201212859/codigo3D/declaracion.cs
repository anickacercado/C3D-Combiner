using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class declaracion
    {
            public string nombre;
            public string tipo;
            public string visibilidad;
            public List<expresion> dimensiones;
            public expresion expresion;
            public ambito ambito;
            public int fila;
            public int columna;
            public simbolo padre = null;

        public declaracion(String visibilidad, String tipo, String nombre, List<expresion> dimensiones, ambito ambito, int fila, int columna, expresion expresion)
        {
            this.visibilidad = visibilidad;
            this.tipo = tipo;
            this.nombre = nombre;
            this.dimensiones = dimensiones;
            this.ambito = ambito;
            this.fila = fila;
            this.columna = columna;
            this.expresion = expresion;
        }

        public String generar3D()
        {
          String codigo = "";

            if (expresion != null)
            {
                /*Declaracion y asignacion*/
                cadena3D expresion3D = expresion.resCondicion();
                if (tipo.Equals("ENTERO"))
                {
                    if (expresion3D.tipo.Equals("ENTERO") || expresion3D.tipo.Equals("DECIMAL") || expresion3D.tipo.Equals("CARACTER") || expresion3D.tipo.Equals("BOOLEANO"))
                    {
                        codigo += "/*Inicio Declaracion*/" + "\r\n";
                        codigo += expresion3D.codigo;
                        String temp = memoria.getTemp();
                        codigo += temp + "=" + "P+" + padre.posicion.ToString() + ";" + "\r\n";
                        codigo += "Stack[" + temp + "]=" + expresion3D.temporal + ";" + "\r\n";
                        codigo += "/*Fin Declaracion*/" + "\r\n\n";
                    }
                    else
                    {
                        memoria.addError("ERROR SEMANTICO ", tipo + " -NO COINCIDEN TIPOS- "+ expresion3D.tipo, fila, columna);
                    }
                }
                else if (tipo.Equals("DECIMAL"))
                {
                    if (expresion3D.tipo.Equals("ENTERO") || expresion3D.tipo.Equals("DECIMAL") || expresion3D.tipo.Equals("CARACTER") || expresion3D.tipo.Equals("BOOLEANO"))
                    {
                        codigo += "/*Inicio Declaracion*/" + "\r\n";
                        codigo += expresion3D.codigo;
                        String temp = memoria.getTemp();
                        codigo += temp + "=" + "P+" + padre.posicion.ToString() + ";" + "\r\n";
                        codigo += "Stack[" + temp + "]=" + expresion3D.temporal + ";" + "\r\n";
                        codigo += "/*Fin Declaracion*/" + "\r\n\n";
                    }
                    else
                    {
                        memoria.addError("ERROR SEMANTICO ", tipo + " -NO COINCIDEN TIPOS- " + expresion3D.tipo, fila, columna);
                    }
                }
                else if (tipo.Equals("CARACTER"))
                {
                    if (expresion3D.tipo.Equals("ENTERO") || expresion3D.tipo.Equals("CARACTER"))
                    {
                        codigo += "/*Inicio Declaracion*/" + "\r\n";
                        codigo += expresion3D.codigo;
                        String temp = memoria.getTemp();
                        codigo += temp + "=" + "P+" + padre.posicion.ToString() + ";" + "\r\n";
                        codigo += "Stack[" + temp + "]=" + expresion3D.temporal + ";" + "\r\n";
                        codigo += "/*Fin Declaracion*/" + "\r\n\n";
                    }
                    else
                    {
                        memoria.addError("ERROR SEMANTICO ", tipo + " -NO COINCIDEN TIPOS- " + expresion3D.tipo, fila, columna);
                    }
                }
                else if (tipo.Equals("CADENA"))
                {
                    if (expresion3D.tipo.Equals("CADENA"))
                    {
                        codigo += "/*Inicio Declaracion*/" + "\r\n";
                        codigo += expresion3D.codigo;
                        String temp = memoria.getTemp();
                        codigo += temp + "=" + "P+" + padre.posicion.ToString() + ";" + "\r\n";
                        codigo += "Stack[" + temp + "]=" + expresion3D.temporal + ";" + "\r\n";
                        codigo += "/*Inicio Declaracion*/" + "\r\n\n";
                    }
                    else
                    {
                        memoria.addError("ERROR SEMANTICO ", tipo + " -NO COINCIDEN TIPOS- " + expresion3D.tipo, fila, columna);
                    }
                }
            }
            else {

                if (dimensiones.Count > 0)
                {
                    /*Declaracion arreglo*/
                    codigo += "/*Inicio Declaracion Arreglo*/" + "\r\n";
                    String temp_pos_heap = memoria.getTemp();
                    String temp_pos_stack = memoria.getTemp();
                    String temp_dimension= memoria.getTemp();

                    codigo += temp_pos_heap + "=H" + ";" + "\r\n";
                    codigo += temp_pos_stack + "=" + "P+" + padre.posicion.ToString() + ";" + "\r\n";
                    codigo += "Stack[" + temp_pos_stack + "]=" + temp_pos_heap + ";" + "\r\n";

                    String temp_num_dimensiones = memoria.getTemp();
                    codigo += temp_num_dimensiones + "=" + dimensiones.Count + ";" + "\r\n";
                    codigo += "Heap[H]=" + temp_num_dimensiones + ";" + " //Numero de dimensiones del arreglo \r\n";
                    codigo += "H = H + 1;" + "\r\n\n";

                    codigo += temp_dimension + "=1;" + "\r\n\n";

                    foreach (expresion expresion in dimensiones) {
                        cadena3D expresion3D = expresion.resCondicion();
                        codigo += "//Inicio valor dimension" + "\r\n";
                        codigo += expresion3D.codigo;
                        codigo += "Heap[H]=" + expresion3D.temporal + ";" + "\r\n";
                        codigo += "//Fin valor dimension" + "\r\n\n";
                        codigo += "H = H + 1;" + "\r\n";
                        codigo += temp_dimension + "=" + temp_dimension + " * " + expresion3D.temporal + "; //Numero dimensiones que se necesitan apartar en el Heap" + "\r\n\n";
                    }

                    codigo += "H = H + "+ temp_dimension + "; //Se apartan las posiciones en el Heap" + "\r\n";
                    codigo += "/*Fin Declaracion Arreglo*/" + "\r\n\n";

                }
                else
                {
                    /*Declaracion nulo*/
                    codigo += "/*Inicio Declaracion*/" + "\r\n";
                    String temp = memoria.getTemp();
                    codigo += temp + "=" + "P+" + padre.posicion.ToString() + ";" + "\r\n";
                    codigo += "Stack[" + temp + "]=" + memoria.inicia_variable + ";" + "\r\n";
                    codigo += "/*Fin Declaracion*/" + "\r\n\n";
                }
                }
            return codigo;
        }
    }
}
