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
            gramaticaTREE Gramatica = new gramaticaTREE();
            LanguageData language = new LanguageData(Gramatica);
            Parser parser = new Parser(language);
            ParseTree arbol = parser.Parse(entrada);

            if (arbol.Root != null && arbol.ParserMessages.Count == 0)
            {
                MessageBox.Show("Entrada correcta");
                arbolTREE.recorrerArbol(arbol.Root);
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
