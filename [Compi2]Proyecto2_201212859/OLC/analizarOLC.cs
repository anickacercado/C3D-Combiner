using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace _Compi2_Proyecto2_201212859.OLC
{
    class analizarOLC
    {
        public void analizar(string entrada, string ruta) {
            ParseTreeOLC(entrada,ruta);
        }

        private ParseTree ParseTreeOLC(string entrada, string ruta) {
            gramaticaOLC Gramatica=new gramaticaOLC();
            LanguageData language = new LanguageData(Gramatica);
            Parser parser = new Parser(language);
            ParseTree arbol = parser.Parse(entrada);
         
            if (arbol.Root != null && arbol.ParserMessages.Count == 0)
            {
                MessageBox.Show("Entrada correcta");
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
