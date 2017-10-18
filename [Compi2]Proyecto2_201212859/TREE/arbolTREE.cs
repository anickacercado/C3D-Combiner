using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.TREE
{
    static class arbolTREE
    {
        public static Object recorrerArbol(ParseTreeNode raiz)
        {
            switch (raiz.Term.Name)
            {
                case "INICIO":
                    {
                        Object importar = recorrerArbol(raiz.ChildNodes[0]);
                        Object clases = recorrerArbol(raiz.ChildNodes[1]);
                    }
                break;
                case "OPCION_IMPORTAR":
                    {
                        String cadena = "";
                        if (raiz.ChildNodes.Count == 1)
                        {
                            cadena = raiz.ChildNodes[0].Token.ValueString;
                        }
                        else
                        {
                            cadena = raiz.ChildNodes[0].Token.ValueString + raiz.ChildNodes[1].Token.ValueString;
                        }
                        return cadena;
                    }
                case "IMPORTAR":
                    {
                        List<String> importar = new List<String>();
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            foreach (String a in (List<String>)recorrerArbol(nodo))
                            {
                                importar.Add(a);
                            }

                        }
                        return importar;

                    }
                case "LISTA_IMPORTAR":
                    {
                        List<String> importar = new List<String>();
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            String a = (String)recorrerArbol(nodo);
                            importar.Add(a);
                        }
                        return importar;
                    }
            }
            return null;
        }
    }
}
