﻿using _Compi2_Proyecto2_201212859.codigo3D;
using _Compi2_Proyecto2_201212859.ejecucion_alto_nivel;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _Compi2_Proyecto2_201212859.TREE
{
    class analizarTREE
    {
        public void analizar(string entrada, string ruta)
        {
            ParseTree(entrada, ruta);
        }

        private ParseTree ParseTree(string entrada, string ruta)
        {
            memoria.ruta = ruta;
            gramaticaTREE Gramatica = new gramaticaTREE();
            LanguageData language = new LanguageData(Gramatica);
            Parser parser = new Parser(language);
            ParseTree arbol = parser.Parse(entrada);

            if (arbol.Root != null && arbol.ParserMessages.Count == 0)
            {
                MessageBox.Show("Entrada correcta");
                estructura_clase estructura_clase = (estructura_clase)arbolTREE.recorrerArbol(arbol.Root);
                estructura_clase.ruta = ruta;
                estructura_clase.generar_tabla_simbolo();
                memoria.lista_estructura_clase.Add(estructura_clase);
                return arbol;
            }
            else
            {
                foreach (Irony.LogMessage error in arbol.ParserMessages)
                {
                    if (error.Message.Contains("Syntax error,"))
                    {
                        principal.insertarError((error.Location.Line + 1).ToString(), (error.Location.Column + 1).ToString(), "Sintactico", error.Message, ruta);
                    }
                    else if (error.Message.Contains("Invalid character"))
                    {
                        principal.insertarError((error.Location.Line + 1).ToString(), (error.Location.Column + 1).ToString(), "Lexico", error.Message, ruta);
                    }
                    else
                    {
                        principal.insertarError((error.Location.Line + 1).ToString(), (error.Location.Column + 1).ToString(), "Sintactico", error.Message, ruta);
                    }

                }
            }
            return null;
        }
    }
}
