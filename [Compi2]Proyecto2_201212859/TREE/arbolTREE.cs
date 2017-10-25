using _Compi2_Proyecto2_201212859.codigo3D;
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
                        List<String> lista_importar = (List<String>)recorrerArbol(raiz.ChildNodes[0]);
                        List<simbolo> lista_clase = (List<simbolo>)recorrerArbol(raiz.ChildNodes[1]);
                        ambito ambito = new ambito("ESTRUCTURA_CLASE", lista_clase);

                        foreach (simbolo simbolo in lista_clase)
                        {
                            /*Dar posición a los simbolos*/
                            posicion_simbolo(simbolo, 0);
                        }

                        estructura_clase estructura_clase = new estructura_clase(lista_importar, ambito, "");
                        return estructura_clase;
                    }
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
                        /*Sintetizando_lista_importar*/
                        List<String> importar = new List<String>();
                        /*Si no esta vacio*/
                        if (raiz.ChildNodes.Count > 0) {
                            ParseTreeNode nodo = raiz.ChildNodes[1];
                            List<String> importar_aux = (List<String>)recorrerArbol(nodo);
                            importar = importar_aux;
                        }
                        return importar;
                    }
                case "LISTA_IMPORTAR":
                    {
                        //Sintetizando_lista_importar
                        List<String> importar = new List<String>();
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            String a = (String)recorrerArbol(nodo);
                            importar.Add(a);
                        }
                        return importar;
                    }
                case "DECLARAR":
                        {
                            List<simbolo> tablaSimbolo = new List<simbolo>();
                            String tipo = (String)recorrerArbol(raiz.ChildNodes[0]);
                            if (raiz.ChildNodes.Count == 2)
                            {
                                foreach (ParseTreeNode variable in raiz.ChildNodes[1].ChildNodes)
                                {
                                    List<expresion> lista_expresion = new List<expresion>();
                                    ambito ambito = new ambito("DECLARACION");
                                    declaracion declaracion = new declaracion("PROTEGIDO", tipo, variable.Token.ValueString, lista_expresion, ambito, variable.Token.Location.Line + 1, variable.Token.Location.Column, null);
                                    simbolo simbolo = new simbolo(declaracion.visibilidad, declaracion.tipo, declaracion.nombre, "DECLARACION", declaracion.fila, declaracion.columna, declaracion.ambito, declaracion);
                                    simbolo.tamanio = 1;
                                    declaracion.padre = simbolo;
                                    padre_expresion(lista_expresion, simbolo);
                                    tablaSimbolo.Add(simbolo);
                                }
                            }
                            else
                            {
                                if (raiz.ChildNodes[2].Term.Name.Equals("E"))
                                {
                                    expresion valor = (expresion)recorrerArbol(raiz.ChildNodes[2]);
                                    foreach (ParseTreeNode variable in raiz.ChildNodes[1].ChildNodes)
                                    {
                                        List<expresion> lista_expresion = new List<expresion>();
                                        ambito ambito = new ambito("DECLARACION");
                                        declaracion declaracion = new declaracion("PROTEGIDO", tipo, variable.Token.ValueString, lista_expresion, ambito, variable.Token.Location.Line + 1, variable.Token.Location.Column, valor);
                                        simbolo simbolo = new simbolo(declaracion.visibilidad, declaracion.tipo, declaracion.nombre, "DECLARACION", declaracion.fila, declaracion.columna, declaracion.ambito, declaracion);
                                        simbolo.tamanio = 1;
                                        declaracion.padre = simbolo;
                                        padre_expresion(lista_expresion, simbolo);
                                        tablaSimbolo.Add(simbolo);
                                    }
                                }
                                else
                                {          
                                    List<expresion> lista_expresion =  (List<expresion>)recorrerArbol(raiz.ChildNodes[2]);
                                    ambito ambito = new ambito("DECLARACION");
                                    declaracion declaracion = new declaracion("PROTEGIDO", tipo, raiz.ChildNodes[1].Token.ValueString, lista_expresion, ambito, raiz.ChildNodes[1].Token.Location.Line + 1, raiz.ChildNodes[1].Token.Location.Column, null);
                                    simbolo simbolo = new simbolo(declaracion.visibilidad, declaracion.tipo, declaracion.nombre, "DECLARACION", declaracion.fila, declaracion.columna, declaracion.ambito, declaracion);
                                    simbolo.tamanio = 1;
                                    declaracion.padre = simbolo;
                                    padre_expresion(lista_expresion, simbolo);
                                    tablaSimbolo.Add(simbolo);
                                }
                            }
                            return tablaSimbolo;
                        }
                case "LISTA_ID":
                    {
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            Object o = recorrerArbol(nodo);
                        }
                    }
                    break;
                case "METODO":
                    {
                        simbolo simbolo = null;
                        if (raiz.ChildNodes[1].Term.Name.Equals("metodo") || raiz.ChildNodes[2].Term.Name.Equals("metodo"))
                        {
                            if (raiz.ChildNodes.Count == 5)
                            {
                                String visibilidad = (String)recorrerArbol(raiz.ChildNodes[0]);
                                String nombre = raiz.ChildNodes[2].Token.ValueString;
                                List<simbolo> lista_parametro = (List<simbolo>)recorrerArbol(raiz.ChildNodes[3]);
                                List<simbolo> lista_sentencias = (List<simbolo>)recorrerArbol(raiz.ChildNodes[4]);
                                metodo metodo = new metodo(visibilidad, "VOID", 0, nombre, lista_parametro, new ambito(nombre, lista_sentencias), raiz.ChildNodes[1].Token.Location.Line + 1, raiz.ChildNodes[1].Token.Location.Column + 1);
                                simbolo = new simbolo(metodo.visibilidad, metodo.tipo, metodo.nombre, "METODO", metodo.fila, metodo.columna, metodo.ambito, metodo);

                                metodo.padre = simbolo;
                                if (lista_parametro.Count > 0)
                                {
                                    simbolo hermano = null;
                                    foreach (simbolo simbolo_parametro in lista_parametro)
                                    {
                                        simbolo_parametro.padre = simbolo;
                                        ((parametro)simbolo_parametro.valor).padre = simbolo;
                                        hermano = simbolo_parametro;
                                        simbolo.tamanio++;
                                        simbolo.ambito.tamanio++;

                                    }
                                    metodo.ambito.tablaSimbolo[0].hermano = hermano;
                                }
                                padre_simbolo(metodo.ambito.tablaSimbolo, simbolo);
                            }
                            else
                            {
                                /*SOBRESCRIBIR*/
                                String visibilidad = (String)recorrerArbol(raiz.ChildNodes[1]);
                                String nombre = raiz.ChildNodes[3].Token.ValueString;
                                List<simbolo> lista_parametro = (List<simbolo>)recorrerArbol(raiz.ChildNodes[4]);
                                List<simbolo> lista_sentencias = (List<simbolo>)recorrerArbol(raiz.ChildNodes[5]);
                                metodo metodo = new metodo(visibilidad, "VOID", 0, nombre, lista_parametro, new ambito(nombre, lista_sentencias), raiz.ChildNodes[2].Token.Location.Line + 1, raiz.ChildNodes[2].Token.Location.Column + 1);
                                simbolo = new simbolo(metodo.visibilidad, metodo.tipo, metodo.nombre, "METODO", metodo.fila, metodo.columna, metodo.ambito, metodo);
                                metodo.padre = simbolo;

                                if (lista_parametro.Count > 0)
                                {
                                    simbolo hermano = null;
                                    foreach (simbolo simbolo_parametro in lista_parametro)
                                    {
                                        simbolo_parametro.padre = simbolo;
                                        ((parametro)simbolo_parametro.valor).padre = simbolo;
                                        hermano = simbolo_parametro;
                                        simbolo.tamanio++;
                                        simbolo.ambito.tamanio++;
                                    }
                                    metodo.ambito.tablaSimbolo[0].hermano = hermano;
                                }
                                padre_simbolo(metodo.ambito.tablaSimbolo, simbolo);
                            }
                        }
                        else if (raiz.ChildNodes[1].Term.Name.Equals("funcion") || raiz.ChildNodes[2].Term.Name.Equals("funcion"))
                        {
                            if (raiz.ChildNodes.Count == 7)
                            {
                                String visibilidad = (String)recorrerArbol(raiz.ChildNodes[0]);
                                String tipo = (String)recorrerArbol(raiz.ChildNodes[2]);
                                int dimensiones = (int)recorrerArbol(raiz.ChildNodes[3]);
                                String nombre = raiz.ChildNodes[4].Token.ValueString;
                                List<simbolo> lista_parametro = (List<simbolo>)recorrerArbol(raiz.ChildNodes[5]);
                                List<simbolo> lista_sentencias = (List<simbolo>)recorrerArbol(raiz.ChildNodes[6]);
                                metodo metodo = new metodo(visibilidad, tipo, dimensiones, nombre, lista_parametro, new ambito(nombre, lista_sentencias), raiz.ChildNodes[1].Token.Location.Line + 1, raiz.ChildNodes[1].Token.Location.Column + 1);
                                simbolo = new simbolo(metodo.visibilidad, metodo.tipo, metodo.nombre, "METODO", metodo.fila, metodo.columna, metodo.ambito, metodo);
                                metodo.padre = simbolo;

                                if (lista_parametro.Count > 0)
                                {
                                    simbolo hermano = null;
                                    foreach (simbolo simbolo_parametro in lista_parametro)
                                    {
                                        simbolo_parametro.padre = simbolo;
                                        ((parametro)simbolo_parametro.valor).padre = simbolo;
                                        hermano = simbolo_parametro;
                                        simbolo.tamanio++;
                                        simbolo.ambito.tamanio++;
                                    }
                                    metodo.ambito.tablaSimbolo[0].hermano = hermano;
                                }
                                padre_simbolo(metodo.ambito.tablaSimbolo, simbolo);
                            }
                            else
                            {
                                /*SOBRESCRIBIR*/
                                String visibilidad = (String)recorrerArbol(raiz.ChildNodes[1]);
                                String tipo = (String)recorrerArbol(raiz.ChildNodes[3]);
                                int dimensiones = (int)recorrerArbol(raiz.ChildNodes[4]);
                                String nombre = raiz.ChildNodes[5].Token.ValueString;
                                List<simbolo> lista_parametro = (List<simbolo>)recorrerArbol(raiz.ChildNodes[6]);
                                List<simbolo> lista_sentencias = (List<simbolo>)recorrerArbol(raiz.ChildNodes[7]);
                                metodo metodo = new metodo(visibilidad, tipo, dimensiones, nombre, lista_parametro, new ambito(nombre, lista_sentencias), raiz.ChildNodes[2].Token.Location.Line + 1, raiz.ChildNodes[2].Token.Location.Column + 1);
                                simbolo = new simbolo(metodo.visibilidad, metodo.tipo, metodo.nombre, "METODO", metodo.fila, metodo.columna, metodo.ambito, metodo);
                                metodo.padre = simbolo;

                                if (lista_parametro.Count > 0)
                                {
                                    simbolo hermano = null;
                                    foreach (simbolo simbolo_parametro in lista_parametro)
                                    {
                                        simbolo_parametro.padre = simbolo;
                                        ((parametro)simbolo_parametro.valor).padre = simbolo;
                                        hermano = simbolo_parametro;
                                        simbolo.tamanio++;
                                        simbolo.ambito.tamanio++;
                                    }
                                    metodo.ambito.tablaSimbolo[0].hermano = hermano;
                                }
                                padre_simbolo(metodo.ambito.tablaSimbolo, simbolo);
                            }
                        }
                        else
                        {
                            /*Constructor*/
                            List<simbolo> lista_parametro = (List<simbolo>)recorrerArbol(raiz.ChildNodes[1]);
                            List<simbolo> lista_sentencias = (List<simbolo>)recorrerArbol(raiz.ChildNodes[2]);
                            metodo metodo = new metodo("VOID", "VOID", 0, "CONSTRUCTOR", lista_parametro, new ambito("CONSTRUCTOR", lista_sentencias), raiz.ChildNodes[0].Token.Location.Line + 1, raiz.ChildNodes[0].Token.Location.Column + 1);
                            simbolo = new simbolo(metodo.visibilidad, metodo.tipo, metodo.nombre, "CONSTRUCTOR", metodo.fila, metodo.columna, metodo.ambito, metodo);
                            metodo.padre = simbolo;

                            if (lista_parametro.Count > 0)
                            {
                                simbolo Hermano = null;
                                foreach (simbolo simbolo_parametro in lista_parametro)
                                {
                                    simbolo_parametro.padre = simbolo;
                                    ((parametro)simbolo_parametro.valor).padre = simbolo;
                                    Hermano = simbolo_parametro;
                                    simbolo.tamanio++;
                                    simbolo.ambito.tamanio++;
                                }
                                metodo.ambito.tablaSimbolo[0].hermano = Hermano;
                            }
                            padre_simbolo(metodo.ambito.tablaSimbolo, simbolo);
                        }
                        List<simbolo> retorna_tabla_simbolo = new List<simbolo>();
                        retorna_tabla_simbolo.Add(simbolo);
                        return retorna_tabla_simbolo;
                    }
                case "PARAMETRO":
                    {
                        String tipo = (String)recorrerArbol(raiz.ChildNodes[0]);
                        int dimensiones = (int)recorrerArbol(raiz.ChildNodes[1]);
                        parametro parametro = new parametro(tipo, raiz.ChildNodes[2].Token.ValueString, dimensiones, raiz.ChildNodes[2].Token.Location.Line + 1, raiz.ChildNodes[2].Token.Location.Column + 1);
                        ambito ambito = new ambito("PARAMETRO");
                        simbolo simbolo = new simbolo("PROTEGIDO",parametro.tipo,parametro.nombre,"PARAMETRO",parametro.fila,parametro.columna,ambito,parametro);
                        simbolo.tamanio = 1;
                        parametro.padre = simbolo;
                        return simbolo;
                    }
                case "LISTA_PARAMETRO":
                    {
                        List<simbolo> lista_simbolo = new List<simbolo>();
                        simbolo hermano = null;
                        foreach (ParseTreeNode nodo in raiz.ChildNodes) {
                            simbolo simbolo = (simbolo)recorrerArbol(nodo);
                            simbolo.hermano = hermano;
                            hermano = simbolo;
                            lista_simbolo.Add(simbolo);
                        }
                        return lista_simbolo;
                    }
                case "ASIGNAR":
                    {
                        simbolo simbolo = null;
                        asignacion asignacion = null;
                        if (raiz.ChildNodes[0].Term.Name.Equals("er_id")) {
                            switch (raiz.ChildNodes[1].Term.Name) {
                                case "LISTA_DIMENSION":
                                    {
                                        List<expresion> lista_expresion = (List<expresion>)recorrerArbol(raiz.ChildNodes[1]);
                                        llamadaArreglo llamada_arreglo = new llamadaArreglo(raiz.ChildNodes[0].Token.ValueString, lista_expresion, null, raiz.ChildNodes[0].Token.Location.Line + 1, raiz.ChildNodes[0].Token.Location.Column + 1);
                                        llamadaObjeto llamada_objeto = new llamadaObjeto(null, "ARREGLO", null, null, llamada_arreglo, null, null, llamada_arreglo.fila, llamada_arreglo.columna);
                                        expresion expresion = (expresion)recorrerArbol(raiz.ChildNodes[2]);
                                        ambito ambito = new ambito("ASIGNACION");
                                        asignacion = new asignacion(llamada_objeto, expresion, ambito, raiz.ChildNodes[0].Token.Location.Line + 1, raiz.ChildNodes[0].Token.Location.Column + 1);
                                        simbolo = new simbolo("ASIGNACION", "ASIGNACION", "ASIGNACION", "ASIGNACION", asignacion.fila, asignacion.columna, asignacion.ambito, asignacion);

                                        padre_expresion(lista_expresion, simbolo);
                                        asignacion.padre = simbolo;
                                        expresion.padre = simbolo;
                                        llamada_objeto.padre = simbolo;
                                        llamada_arreglo.padre = simbolo;

                                    }
                                    break;
                                default:
                                    {
                                        llamadaObjeto llamada_objeto = (llamadaObjeto)recorrerArbol(raiz.ChildNodes[0]);
                                        expresion expresion = (expresion)recorrerArbol(raiz.ChildNodes[1]);
                                        ambito ambito = new ambito("ASIGNACION");
                                        asignacion = new asignacion(llamada_objeto, expresion, ambito, raiz.ChildNodes[0].Token.Location.Line + 1, raiz.ChildNodes[0].Token.Location.Column + 1);
                                        simbolo = new simbolo("ASIGNACION", "ASIGNACION", "ASIGNACION", "ASIGNACION", asignacion.fila, asignacion.columna, asignacion.ambito, asignacion);

                                        asignacion.padre = simbolo;
                                        expresion.padre = simbolo;
                                        llamada_objeto.padre = simbolo;
                                    }
                                    break;
                            }
                        }
                        else if (raiz.ChildNodes[0].Term.Name.Equals("OBJETO"))
                        {
                            switch (raiz.ChildNodes[2].Term.Name)
                            {
                                case "LISTA_DIMENSION":
                                    {
                                        llamadaObjeto objeto_padre = (llamadaObjeto)recorrerArbol(raiz.ChildNodes[0]);
                                        List<expresion> lista_expresion = (List<expresion>)recorrerArbol(raiz.ChildNodes[2]);
                                        llamadaArreglo llamada_arreglo = new llamadaArreglo(raiz.ChildNodes[1].Token.ValueString, lista_expresion, null, raiz.ChildNodes[1].Token.Location.Line + 1, raiz.ChildNodes[1].Token.Location.Column + 1);
                                        llamadaObjeto llamada_objeto = new llamadaObjeto(null, "ARREGLO", null, null, llamada_arreglo, null, null, llamada_arreglo.fila, llamada_arreglo.columna);
                                        objeto_padre.set_hijo(llamada_objeto);

                                        expresion expresion = (expresion)recorrerArbol(raiz.ChildNodes[3]);
                                        ambito ambito = new ambito("ASIGNACION");
                                        asignacion = new asignacion(objeto_padre, expresion, ambito, raiz.ChildNodes[1].Token.Location.Line + 1, raiz.ChildNodes[1].Token.Location.Column + 1);
                                        simbolo = new simbolo("ASIGNACION", "ASIGNACION", "ASIGNACION", "ASIGNACION", asignacion.fila, asignacion.columna, asignacion.ambito, asignacion);

                                        padre_expresion(lista_expresion, simbolo);
                                        asignacion.padre = simbolo;
                                        expresion.padre = simbolo;
                                        objeto_padre.padre = simbolo;
                                        llamada_arreglo.padre = simbolo;
                                    }
                                    break;
                                default:
                                    {
                                        llamadaObjeto objeto_padre = (llamadaObjeto)recorrerArbol(raiz.ChildNodes[0]);
                                        llamadaObjeto llamada_objeto = new llamadaObjeto(null, "VARIABLE", raiz.ChildNodes[1].Token.ValueString, null, null, null, null, raiz.ChildNodes[1].Token.Location.Line + 1, raiz.ChildNodes[1].Token.Location.Column + 1);
                                        objeto_padre.set_hijo(llamada_objeto);

                                        expresion expresion = (expresion)recorrerArbol(raiz.ChildNodes[2]);
                                        ambito ambito = new ambito("ASIGNACION");
                                        asignacion = new asignacion(objeto_padre, expresion, ambito, raiz.ChildNodes[1].Token.Location.Line + 1, raiz.ChildNodes[1].Token.Location.Column + 1);
                                        simbolo = new simbolo("ASIGNACION", "ASIGNACION", "ASIGNACION", "ASIGNACION", asignacion.fila, asignacion.columna, asignacion.ambito, asignacion);

                                        asignacion.padre = simbolo;
                                        expresion.padre = simbolo;
                                        objeto_padre.padre = simbolo;
                                    }
                                    break;
                            }
                        }
                        List<simbolo> retorna_tabla_simbolo = new List<simbolo>();
                        retorna_tabla_simbolo.Add(simbolo);
                        return retorna_tabla_simbolo;
                    }
                case "SENTENCIAS_GLOBALES":
                    {
                        if (raiz.ChildNodes.Count > 1) {
                            List<simbolo> lista_simbolo = new List<simbolo>();
                            lista_simbolo = (List<simbolo>)recorrerArbol(raiz.ChildNodes[1]);
                            foreach (simbolo simbolo in lista_simbolo)
                            {
                                declaracion declaracion = (declaracion)simbolo.valor;
                                declaracion.visibilidad = (String)recorrerArbol(raiz.ChildNodes[0]);
                                simbolo.visibilidad = declaracion.visibilidad;
                            }
                            return lista_simbolo;
                        }
                        else {
                            return recorrerArbol(raiz.ChildNodes[0]);
                        }
                    }
                case "LISTA_SENTENCIAS_GLOBALES":
                    {
                        {
                            List<simbolo> tablaSimbolo = new List<simbolo>();
                            foreach (ParseTreeNode nodo in raiz.ChildNodes)
                            {
                                simbolo hermano = null;
                                foreach (simbolo simbolo in (List<simbolo>)recorrerArbol(nodo))
                                {
                                    simbolo.hermano = hermano;
                                    hermano = simbolo;
                                    tablaSimbolo.Add(simbolo);
                                }
                            }
                            return tablaSimbolo;
                        }
                    }
                case "SENTENCIAS_LOCALES":
                    {
                        return recorrerArbol(raiz.ChildNodes[0]);
                    }
                case "LISTA_SENTENCIAS_LOCALES":
                    {
                        {
                            List<simbolo> tablaSimbolo = new List<simbolo>();
                            foreach (ParseTreeNode nodo in raiz.ChildNodes)
                            {
                                simbolo hermano = null;
                                foreach (simbolo simbolo in (List<simbolo>)recorrerArbol(nodo))
                                {
                                    simbolo.hermano = hermano;
                                    hermano = simbolo;
                                    tablaSimbolo.Add(simbolo);
                                }
                            }
                            return tablaSimbolo;
                        }
                    }
                case "RETORNAR":
                    {
                        ambito ambito = new codigo3D.ambito("RETORNAR");
                        simbolo simbolo = new simbolo("RETORNAR", "RETORNAR", "RETORNAR", "RETORNAR", raiz.ChildNodes[0].Token.Location.Line + 1, raiz.ChildNodes[0].Token.Location.Column + 1, ambito, null);

                        List<simbolo> retorna_tabla_simbolo = new List<simbolo>();
                        retorna_tabla_simbolo.Add(simbolo);
                        return retorna_tabla_simbolo;
                    }
                case "TIPO":
                    {
                        return raiz.ChildNodes[0].Token.Text.ToUpper();
                    }
                case "SI":
                    {
                        expresion expresion = (expresion)recorrerArbol(raiz.ChildNodes[1]);
                        List<simbolo> tablaSimbolo = (List<simbolo>)recorrerArbol(raiz.ChildNodes[2]);
                        ambito ambito = new codigo3D.ambito("SI", tablaSimbolo);
                        si si = new si(expresion, ambito, null,null);
                        simbolo simbolo = new simbolo("SI", "SI", "SI", "SI", raiz.ChildNodes[0].Token.Location.Line + 1, raiz.ChildNodes[0].Token.Location.Column + 1, si.ambito, si);

                        expresion.padre = simbolo;
                        padre_simbolo(tablaSimbolo, simbolo);

                        foreach (simbolo simb in tablaSimbolo)
                        {
                            simb.padre = simbolo;
                        }

                        return simbolo;
                    }
               case "SINO_SI":
                    {
                        expresion expresion = (expresion)recorrerArbol(raiz.ChildNodes[1]);
                        List<simbolo> tablaSimbolo = (List<simbolo>)recorrerArbol(raiz.ChildNodes[2]);
                        ambito ambito = new codigo3D.ambito("SINO_SI", tablaSimbolo);

                        sino_si sino_si = new sino_si(expresion, ambito);
                        return sino_si;
                    }
                case "LISTA_SINO_SI":
                    {
                        //Sintetizando_lista_importar
                        List<sino_si> lista_sino_si = new List<sino_si>();
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            sino_si a = (sino_si)recorrerArbol(nodo);
                            lista_sino_si.Add(a);
                        }
                        return lista_sino_si;
                    }
                case "SINO":
                    {
                        List<simbolo> tablaSimbolo = (List<simbolo>)recorrerArbol(raiz.ChildNodes[1]);
                        ambito ambito = new codigo3D.ambito("SINO", tablaSimbolo);
                        sino sino = new sino(ambito);
                        return sino;
                    }
                case "SENTENCIA_SI":
                    {
                        List<simbolo> retorna_tabla_simbolo = new List<simbolo>();
                        simbolo simbolo_si = (simbolo)recorrerArbol(raiz.ChildNodes[0]);
                        si si = (si)simbolo_si.valor;

                        if (raiz.ChildNodes.Count == 1)
                        {
                            retorna_tabla_simbolo.Add(simbolo_si);
                        }
                        else if (raiz.ChildNodes.Count == 2)
                        {
                            if (raiz.ChildNodes[0].Term.Name.Equals("SINO"))
                            {
                                sino sino = (sino)recorrerArbol(raiz.ChildNodes[1]);
                                //padre_simbolo(sino.ambito.tablaSimbolo, simbolo_si);

                                foreach (simbolo simbolo in sino.ambito.tablaSimbolo)
                                {
                                    simbolo.padre = simbolo_si;
                                    simbolo_si.ambito.tamanio += simbolo.tamanio;
                                }


                                si.sino = sino;
                                si.sino.padre = simbolo_si;
                            }
                            else
                            {
                                List<sino_si> sino_si = (List<sino_si>)recorrerArbol(raiz.ChildNodes[1]);
                                foreach (sino_si si_s in sino_si)
                                {
                                    si_s.expresion.padre = simbolo_si;
                                    si_s.padre = simbolo_si;
                                    foreach (simbolo simbolo in si_s.ambito.tablaSimbolo)
                                    {
                                        simbolo.padre = simbolo_si;
                                        simbolo_si.ambito.tamanio += simbolo.tamanio;
                                    }
                                }
                                si.lista_sino_si = sino_si;
                            }
                            retorna_tabla_simbolo.Add(simbolo_si);
                        }
                        else {
                            //sino
                            sino sino = (sino)recorrerArbol(raiz.ChildNodes[2]);
                            //padre_simbolo(sino.ambito.tablaSimbolo, simbolo_si);
                            foreach (simbolo simbolo in sino.ambito.tablaSimbolo)
                            {
                                simbolo.padre = simbolo_si;
                                simbolo_si.ambito.tamanio += simbolo.tamanio;
                            }
                            si.sino = sino;
                            si.sino.padre = simbolo_si;

                            //sino_si
                            List<sino_si> sino_si = (List<sino_si>)recorrerArbol(raiz.ChildNodes[1]);
                            foreach (sino_si si_s in sino_si)
                            {
                                si_s.expresion.padre = simbolo_si;
                                si_s.padre = simbolo_si;
                                foreach (simbolo simbolo in si_s.ambito.tablaSimbolo)
                                {
                                    simbolo.padre = simbolo_si;
                                    simbolo_si.ambito.tamanio += simbolo.tamanio;
                                }
                            }
                            si.lista_sino_si = sino_si;
                            retorna_tabla_simbolo.Add(simbolo_si);
                        }
                        simbolo_si.tamanio = simbolo_si.ambito.tamanio;
                        return retorna_tabla_simbolo;
                    }
                case "MIENTRAS":
                    {
                        expresion expresion = (expresion)recorrerArbol(raiz.ChildNodes[1]);
                        List<simbolo> tablaSimbolo = (List<simbolo>)recorrerArbol(raiz.ChildNodes[2]);
                        ambito ambito = new codigo3D.ambito("MIENTRAS", tablaSimbolo);

                        mientras mientras = new mientras(expresion, ambito);
                        simbolo simbolo = new simbolo("MIENTRAS", "MIENTRAS", "MIENTRAS", "MIENTRAS", raiz.ChildNodes[0].Token.Location.Line + 1, raiz.ChildNodes[0].Token.Location.Column + 1, mientras.ambito, mientras);
                        mientras.padre = simbolo;
                        mientras.expresion.padre = simbolo;

                        padre_simbolo(tablaSimbolo, simbolo);

                        List<simbolo> retorna_tabla_simbolo = new List<simbolo>();
                        retorna_tabla_simbolo.Add(simbolo);
                        return retorna_tabla_simbolo;
                    }
                case "HACER_MIENTRAS":
                    {
                        expresion expresion = (expresion)recorrerArbol(raiz.ChildNodes[3]);
                        List<simbolo> tablaSimbolo = (List<simbolo>)recorrerArbol(raiz.ChildNodes[1]);
                        ambito ambito = new codigo3D.ambito("HACER_MIENTRAS", tablaSimbolo);

                        hacer_mientras hacer_mientras = new hacer_mientras(expresion, ambito);
                        simbolo simbolo = new simbolo("HACER_MIENTRAS", "HACER_MIENTRAS", "HACER_MIENTRAS", "HACER_MIENTRAS", raiz.ChildNodes[0].Token.Location.Line + 1, raiz.ChildNodes[0].Token.Location.Column + 1, hacer_mientras.ambito, hacer_mientras);
                        hacer_mientras.padre = simbolo;
                        hacer_mientras.expresion.padre = simbolo;

                        padre_simbolo(tablaSimbolo, simbolo);

                        List<simbolo> retorna_tabla_simbolo = new List<simbolo>();
                        retorna_tabla_simbolo.Add(simbolo);
                        return retorna_tabla_simbolo;
                    }
                case "REPETIR":
                    {
                        expresion expresion = (expresion)recorrerArbol(raiz.ChildNodes[3]);
                        List<simbolo> tablaSimbolo = (List<simbolo>)recorrerArbol(raiz.ChildNodes[1]);
                        ambito ambito = new codigo3D.ambito("REPETIR_HASTA", tablaSimbolo);

                        repetir_hasta repetir_hasta = new repetir_hasta(expresion, ambito);
                        simbolo simbolo = new simbolo("REPETIR_HASTA", "REPETIR_HASTA", "REPETIR_HASTA", "REPETIR_HASTA", raiz.ChildNodes[0].Token.Location.Line + 1, raiz.ChildNodes[0].Token.Location.Column + 1, repetir_hasta.ambito, repetir_hasta);
                        repetir_hasta.padre = simbolo;
                        repetir_hasta.expresion.padre = simbolo;

                        padre_simbolo(tablaSimbolo, simbolo);

                        List<simbolo> retorna_tabla_simbolo = new List<simbolo>();
                        retorna_tabla_simbolo.Add(simbolo);
                        return retorna_tabla_simbolo;
                    }
                case "AUMENTO_DECREMENTO":
                    {
                        ambito ambito = new ambito("AUMENTO_DECREMENTO");
                        llamadaObjeto llamadaObjeto = (llamadaObjeto)recorrerArbol(raiz.ChildNodes[0]);
                        aumento_decremento aumento_decremento = new aumento_decremento(llamadaObjeto, raiz.ChildNodes[0].Token.ValueString, ambito, llamadaObjeto.fila, llamadaObjeto.columna);
                        simbolo simbolo = new simbolo("AUMENTO_DECREMENTO", "AUMENTO_DECREMENTO", "AUMENTO_DECREMENTO", "AUMENTO_DECREMENTO", raiz.ChildNodes[0].Token.Location.Line + 1, raiz.ChildNodes[0].Token.Location.Column + 1, aumento_decremento.ambito, aumento_decremento);
                        aumento_decremento.padre = simbolo;

                        List<simbolo> retorna_tabla_simbolo = new List<simbolo>();
                        retorna_tabla_simbolo.Add(simbolo);
                        return retorna_tabla_simbolo;
                    }
                case "LLAMADA_METODO":
                    {
                        simbolo simbolo = null;
                        if (raiz.ChildNodes[0].Term.Name.Equals("er_id"))
                        {
                            List<expresion> lista_expresion = (List<expresion>)recorrerArbol(raiz.ChildNodes[1]);
                            switch (raiz.ChildNodes[1].Term.Name)
                            {
                                case "LISTA_DIMENSION":
                                    {
                                        if (lista_expresion.Count == 1)
                                        {
                                            llamadaMetodo llamada_metodo = new llamadaMetodo(raiz.ChildNodes[0].Token.ValueString, lista_expresion, null, raiz.ChildNodes[0].Token.Location.Line + 1, raiz.ChildNodes[0].Token.Location.Column + 1);
                                            llamadaObjeto llamada_objeto = new llamadaObjeto(null, "METODO", null, llamada_metodo, null, null, null, llamada_metodo.fila, llamada_metodo.columna);
                                            ambito ambito = new ambito("LLAMADA_METODO");
                                            simbolo = new simbolo("LLAMADA_METODO", "LLAMADA_METODO", "LLAMADA_METODO", "LLAMADA_METODO", llamada_objeto.fila, llamada_objeto.columna, ambito, llamada_objeto);

                                            padre_expresion(lista_expresion, simbolo);
                                            llamada_metodo.padre = simbolo;
                                            llamada_objeto.padre = simbolo;
                                        }
                                        else
                                        {
                                            int fila = raiz.ChildNodes[0].Token.Location.Line + 1;
                                            int columna = raiz.ChildNodes[0].Token.Location.Column + 1;
                                            principal.insertarError(fila.ToString(), columna.ToString(), "SINTACTICO", "LLamada a Arreglo invalida", "");
                                        }
                                    }
                                    break;
                                default:
                                    {
                                        llamadaMetodo llamada_metodo = new llamadaMetodo(raiz.ChildNodes[0].Token.ValueString, lista_expresion, null, raiz.ChildNodes[0].Token.Location.Line + 1, raiz.ChildNodes[0].Token.Location.Column + 1);
                                        llamadaObjeto llamada_objeto = new llamadaObjeto(null, "METODO", null, llamada_metodo, null, null, null, llamada_metodo.fila, llamada_metodo.columna);
                                        ambito ambito = new ambito("LLAMADA_METODO");
                                        simbolo = new simbolo("LLAMADA_METODO", "LLAMADA_METODO", "LLAMADA_METODO", "LLAMADA_METODO", llamada_objeto.fila, llamada_objeto.columna, ambito, llamada_objeto);

                                        padre_expresion(lista_expresion, simbolo);
                                        llamada_metodo.padre = simbolo;
                                        llamada_objeto.padre = simbolo;
                                    }
                                    break;
                            }
                        }

                        else if (raiz.ChildNodes[0].Term.Name.Equals("OBJETO"))
                        {
                            List<expresion> lista_expresion = (List<expresion>)recorrerArbol(raiz.ChildNodes[2]);
                            switch (raiz.ChildNodes[2].Term.Name)
                            {
                                case "LISTA_DIMENSION":
                                    {
                                        if (lista_expresion.Count == 1)
                                        {
                                            llamadaObjeto objeto_padre = (llamadaObjeto)recorrerArbol(raiz.ChildNodes[0]);
                                            llamadaMetodo llamada_metodo = new llamadaMetodo(raiz.ChildNodes[1].Token.ValueString, lista_expresion, null, raiz.ChildNodes[1].Token.Location.Line + 1, raiz.ChildNodes[1].Token.Location.Column + 1);
                                            llamadaObjeto llamada_objeto = new llamadaObjeto(null, "METODO", null, llamada_metodo, null, null, null, llamada_metodo.fila, llamada_metodo.columna);
                                            objeto_padre.set_hijo(llamada_objeto);

                                            ambito ambito = new ambito("LLAMADA_METODO");
                                            simbolo = new simbolo("LLAMADA_METODO", "LLAMADA_METODO", "LLAMADA_METODO", "LLAMADA_METODO", objeto_padre.fila, objeto_padre.columna, ambito, objeto_padre);

                                            padre_expresion(lista_expresion, simbolo);
                                            llamada_metodo.padre = simbolo;
                                            llamada_objeto.padre = simbolo;
                                        }
                                        else
                                        {
                                            int fila = raiz.ChildNodes[1].Token.Location.Line + 1;
                                            int columna = raiz.ChildNodes[1].Token.Location.Column + 1;
                                            principal.insertarError(fila.ToString(), columna.ToString(), "SINTACTICO", "LLamada a Arreglo invalida", "");
                                        }
                                    }
                                    break;
                                default:
                                    {
                                        llamadaObjeto objeto_padre = (llamadaObjeto)recorrerArbol(raiz.ChildNodes[0]);
                                        llamadaMetodo llamada_metodo = new llamadaMetodo(raiz.ChildNodes[1].Token.ValueString, lista_expresion, null, raiz.ChildNodes[1].Token.Location.Line + 1, raiz.ChildNodes[1].Token.Location.Column + 1);
                                        llamadaObjeto llamada_objeto = new llamadaObjeto(null, "METODO", null, llamada_metodo, null, null, null, llamada_metodo.fila, llamada_metodo.columna);
                                        objeto_padre.set_hijo(llamada_objeto);

                                        ambito ambito = new ambito("LLAMADA_METODO");
                                        simbolo = new simbolo("LLAMADA_METODO", "LLAMADA_METODO", "LLAMADA_METODO", "LLAMADA_METODO", objeto_padre.fila, objeto_padre.columna, ambito, objeto_padre);

                                        padre_expresion(lista_expresion, simbolo);
                                        llamada_metodo.padre = simbolo;
                                        llamada_objeto.padre = simbolo;
                                    }
                                    break;
                            }
                        }
                        else if (raiz.ChildNodes[0].Term.Name.Equals("out_string"))
                        {
                            expresion expresion = (expresion)recorrerArbol(raiz.ChildNodes[1]);
                            imprimir imprimir = new imprimir(expresion);
                            ambito ambito = new ambito("IMPRIMIR");
                            simbolo = new simbolo("IMPRIMIR", "IMPRIMIR", "IMPRIMIR", "IMPRIMIR", raiz.ChildNodes[0].Token.Location.Line + 1, raiz.ChildNodes[0].Token.Location.Column + 1, ambito, imprimir);

                            expresion.padre = simbolo;
                            imprimir.padre = simbolo;
                        }
                        else if (raiz.ChildNodes[0].Term.Name.Equals("super"))
                        {
                            List<expresion> lista_expresion = (List<expresion>)recorrerArbol(raiz.ChildNodes[1]);
                            super super = new super(lista_expresion);
                            ambito ambito = new ambito("SUPER");
                            simbolo = new simbolo("SUPER", "SUPER", "SUPER", "SUPER", raiz.ChildNodes[0].Token.Location.Line + 1, raiz.ChildNodes[0].Token.Location.Column + 1, ambito, super);

                            padre_expresion(lista_expresion, simbolo);
                            super.padre = simbolo;
                        }
                        List<simbolo> retorna_tabla_simbolo = new List<simbolo>();
                        if (simbolo != null)
                        {
                            retorna_tabla_simbolo.Add(simbolo);
                        }
                        return retorna_tabla_simbolo;
                    }
                case "DIMENSION":
                    {
                        expresion expresion = null;
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            expresion = (expresion)recorrerArbol(nodo);
                        }
                        return expresion;
                    }
                case "LISTA_DIMENSION":
                    {
                        List<expresion> lista_expresion = new List<expresion>();
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            lista_expresion.Add((expresion)recorrerArbol(nodo));
                        }
                        return lista_expresion;
                    }
                case "ARREGLO":
                    {
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            Object o = recorrerArbol(nodo);
                        }
                    }
                    break;
                case "LISTA_ARREGLO":
                    {
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            Object o = recorrerArbol(nodo);
                        }
                    }
                    break;
                case "SALIR":
                    {
                        ambito ambito = new codigo3D.ambito("SALIR");
                        simbolo simbolo = new simbolo("SALIR", "SALIR", "SALIR", "SALIR", raiz.ChildNodes[0].Token.Location.Line + 1, raiz.ChildNodes[0].Token.Location.Column + 1, ambito, null);
                        List<simbolo> retorna_tabla_simbolo = new List<simbolo>();
                        retorna_tabla_simbolo.Add(simbolo);
                        return retorna_tabla_simbolo;
                    }
                case "CONTINUAR":
                    {
                        ambito ambito = new codigo3D.ambito("CONTINUAR");
                        simbolo simbolo = new simbolo("CONTINUAR", "CONTINUAR", "CONTINUAR", "CONTINUAR", raiz.ChildNodes[0].Token.Location.Line + 1, raiz.ChildNodes[0].Token.Location.Column + 1,ambito, null);
                        List<simbolo> retorna_tabla_simbolo = new List<simbolo>();
                        retorna_tabla_simbolo.Add(simbolo);
                        return retorna_tabla_simbolo;
                    }
                case "ELEGIR":
                    {
                        expresion expresion = (expresion)recorrerArbol(raiz.ChildNodes[1]);
                        List<caso> lista_caso = (List<caso>)recorrerArbol(raiz.ChildNodes[3]);
                        caso defecto = (caso)recorrerArbol(raiz.ChildNodes[4]);

                        ambito ambito = new ambito("ELEGIR");
                        elegir elegir = new elegir(expresion,lista_caso,defecto,ambito);
                        simbolo simbolo = new simbolo("ELEGIR", "ELEGIR", "ELEGIR", "ELEGIR", raiz.ChildNodes[0].Token.Location.Line + 1, raiz.ChildNodes[0].Token.Location.Column + 1, elegir.ambito, elegir);

                        elegir.padre= simbolo;
                        elegir.ambito.tamanio = 0;
                        foreach (caso caso in lista_caso)
                        {
                            caso.expresion.padre = simbolo;
                            caso.padre = simbolo;
                            foreach (simbolo simb in caso.ambito.tablaSimbolo)
                            {
                                simb.padre = simbolo;
                                elegir.ambito.tamanio += simb.tamanio;
                            }
                        }
                        if (defecto !=  null) {
                            defecto.padre = simbolo;
                            //padre_simbolo(defecto.ambito.tablaSimbolo, simbolo);
                            foreach (simbolo simb in defecto.ambito.tablaSimbolo)
                            {
                                simb.padre = simbolo;
                                elegir.ambito.tamanio += simb.tamanio;
                            }
                        }
                        simbolo.tamanio = elegir.ambito.tamanio;
                        List<simbolo> retorna_tabla_simbolo = new List<simbolo>();
                        retorna_tabla_simbolo.Add(simbolo);
                        return retorna_tabla_simbolo;

                    }
                    case "CASO":
                    {

                        expresion expresion = (expresion)recorrerArbol(raiz.ChildNodes[0]);
                        List<simbolo> tablaSimbolo = (List<simbolo>)recorrerArbol(raiz.ChildNodes[1]);
                        ambito ambito = new ambito("CASO", tablaSimbolo);
                        caso caso = new caso(expresion, ambito);
                        return caso;
                    }
                case "LISTA_CASO":
                    {
                        //Sintetizando_lista_importar
                        List<caso> lista_caso = new List<caso>();
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            caso a = (caso)recorrerArbol(nodo);
                            lista_caso.Add(a);
                        }
                        return lista_caso;
                    }
                case "VALOR":
                    {
                        return recorrerArbol(raiz.ChildNodes[0]);
                    }

                case "DEFECTO":
                    {
                        if (raiz.ChildNodes.Count > 0)
                        {
                            expresion  expresion= (expresion)recorrerArbol(raiz.ChildNodes[0]);
                            List<simbolo> tablaSimbolo = (List<simbolo>)recorrerArbol(raiz.ChildNodes[1]);
                            ambito ambito = new ambito("CASO", tablaSimbolo);
                            caso caso = new caso(ambito);
                            return caso;
                        }
                            return null;
                    }
                case "LOOP":
                    {
                        List<simbolo> tablaSimbolo = (List<simbolo>)recorrerArbol(raiz.ChildNodes[1]);
                        ambito ambito = new codigo3D.ambito("LOOP", tablaSimbolo);

                        loop loop = new loop(ambito);
                        simbolo simbolo = new simbolo("LOOP", "LOOP", "LOOP", "LOOP", raiz.ChildNodes[0].Token.Location.Line + 1, raiz.ChildNodes[0].Token.Location.Column + 1, loop.ambito, loop);
                        loop.padre = simbolo;

                        padre_simbolo(tablaSimbolo, simbolo);

                        List<simbolo> retorna_tabla_simbolo = new List<simbolo>();
                        retorna_tabla_simbolo.Add(simbolo);
                        return retorna_tabla_simbolo;
                    }
                case "PARA":
                    {
                        //declara_asigna
                        List<simbolo> lista_declara_asigna = (List<simbolo>)recorrerArbol(raiz.ChildNodes[1]);
                        simbolo declara_asigna = lista_declara_asigna[0];

                        //valor
                        expresion valor = (expresion)recorrerArbol(raiz.ChildNodes[2]);

                        //aumenta_decrementa 
                        List<simbolo> lista_decre_aum = (List<simbolo>)recorrerArbol(raiz.ChildNodes[3]);
                        simbolo decre_aum = lista_decre_aum[0];

                        //tablaSimbolo
                        List<simbolo> tablaSimbolo = (List<simbolo>)recorrerArbol(raiz.ChildNodes[4]);
                        ambito ambito = new codigo3D.ambito("PARA", tablaSimbolo);

                        //para
                        para para = new para(declara_asigna, valor, decre_aum, ambito);
                        simbolo simbolo = new simbolo("PARA", "PARA", "PARA", "PARA", raiz.ChildNodes[0].Token.Location.Line + 1, raiz.ChildNodes[0].Token.Location.Column + 1, para.ambito, para);

                        para.declara_asigna.padre = simbolo;
                        valor.padre = para.declara_asigna;
                        decre_aum.padre = para.declara_asigna;

                        para.padre = simbolo;
                        padre_simbolo(tablaSimbolo, para.declara_asigna);


                        if (para.declara_asigna.rol.Equals("DECLARACION"))
                        {
                            para.declara_asigna.tamanio = 1;
                            para.ambito.tamanio++;
                            simbolo.tamanio++;
                            simbolo.ambito.tamanio++;
                        }

                        List<simbolo> retorna_tabla_simbolo = new List<simbolo>();
                        retorna_tabla_simbolo.Add(simbolo);
                        return retorna_tabla_simbolo;
                    }
                case "LISTA_CLASE":
                    {
                        List<simbolo> tablaSimbolo = new List<simbolo>();
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            simbolo hermano = null;
                            foreach (simbolo simbolo in (List<simbolo>)recorrerArbol(nodo))
                            {
                                simbolo.hermano = hermano;
                                hermano = simbolo;
                                tablaSimbolo.Add(simbolo);
                            }
                        }
                        return tablaSimbolo;
                    }
                case "CLASE":
                    {
                        simbolo simbolo = null;
                        if (raiz.ChildNodes.Count == 3)
                        {
                            List<simbolo> lista_simbolo = (List<simbolo>)recorrerArbol(raiz.ChildNodes[2]);
                            ambito ambito = new ambito(raiz.ChildNodes[1].Token.ValueString, lista_simbolo);
                            clase clase = new clase(raiz.ChildNodes[1].Token.ValueString, "", ambito, raiz.ChildNodes[0].Token.Location.Line + 1, raiz.ChildNodes[1].Token.Location.Column + 1);
                            simbolo = new simbolo("PUBLICO", "VOID", clase.nombre, "CLASE", clase.fila, clase.columna, clase.ambito, clase);
                        }
                        else
                        {
                            List<simbolo> lista_simbolo = (List<simbolo>)recorrerArbol(raiz.ChildNodes[3]);
                            ambito ambito = new ambito(raiz.ChildNodes[1].Token.ValueString, lista_simbolo);
                            clase clase = new clase(raiz.ChildNodes[1].Token.ValueString, raiz.ChildNodes[2].Token.ValueString, ambito, raiz.ChildNodes[0].Token.Location.Line + 1, raiz.ChildNodes[1].Token.Location.Column + 1);
                            simbolo = new simbolo("PUBLICO", "VOID", clase.nombre, "CLASE", clase.fila, clase.columna, clase.ambito, clase);
                        }
                        List<simbolo> retorna_tabla_simbolo = new List<simbolo>();
                        retorna_tabla_simbolo.Add(simbolo);
                        return retorna_tabla_simbolo;
                    }
                case "VISIBILIDAD":
                    {
                        String visibilidad = "PUBLICO";
                        if (raiz.ChildNodes.Count > 0) {
                            visibilidad = raiz.ChildNodes[0].Token.Text.ToUpper();
                        }
                        return visibilidad;
                        
                    }
                case "LISTA_PARAMETRO_E":
                    {
                        List<simbolo> lista_simbolo = new List<simbolo>();
                        if (raiz.ChildNodes.Count > 0)
                        {
                            lista_simbolo = (List<simbolo>)recorrerArbol(raiz.ChildNodes[0]);
                        }
                        return lista_simbolo;
                    }
                case "LISTA_DIMENSION_METODO_E":
                    {
                        if (raiz.ChildNodes.Count > 0)
                        {
                            return raiz.ChildNodes[0].ChildNodes.Count;
                        }
                        return 0;
                    }

                case "LISTA_DIMENSION_METODO":
                    {
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            Object o = recorrerArbol(nodo);
                        }
                    }
                    break;
                case "DIMENSION_METODO":
                    {
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            Object o = recorrerArbol(nodo);
                        }
                    }
                    break;
                case "E":
                    {
                        expresion nodo = null;
                        if (raiz.ChildNodes.Count == 1)
                        {
                            nodo = (expresion)recorrerArbol(raiz.ChildNodes[0]);

                        }
                        else if (raiz.ChildNodes.Count == 2)
                        {
                            if (raiz.ChildNodes[0].Term.Name.Equals("er_casteo"))
                            {
                                expresion der = (expresion)recorrerArbol(raiz.ChildNodes[1]);
                                nodo = new expresion(der, null, raiz.ChildNodes[0].Term.Name, raiz.ChildNodes[0].Term.Name, raiz.ChildNodes[0].Token.Location.Line + 1, raiz.ChildNodes[0].Token.Location.Column + 1, null);

                            }
                            else
                            {
                                expresion izq = (expresion)recorrerArbol(raiz.ChildNodes[0]);
                                nodo = new expresion(null, izq, raiz.ChildNodes[1].Term.Name, raiz.ChildNodes[1].Term.Name, raiz.ChildNodes[1].Token.Location.Line + 1, raiz.ChildNodes[1].Token.Location.Column + 1, null);
                            }
                        }
                        else if (raiz.ChildNodes.Count == 3)
                        {
                            if (raiz.ChildNodes[0].Term.Name.Equals("nuevo"))
                            {
                                List<expresion> lista_expresion = (List<expresion>)recorrerArbol(raiz.ChildNodes[2]);
                                nuevo nuevo = new nuevo(raiz.ChildNodes[1].Token.ValueString, lista_expresion, null, raiz.ChildNodes[0].Token.Location.Line + 1, raiz.ChildNodes[0].Token.Location.Column + 1);
                                nodo = new expresion(null, null, "NUEVO", "NUEVO", nuevo.fila, nuevo.fila, nuevo);
                            }
                            else {
                                expresion izq = (expresion)recorrerArbol(raiz.ChildNodes[0]);
                                expresion der = (expresion)recorrerArbol(raiz.ChildNodes[1]);
                                nodo = new expresion(izq, der, raiz.ChildNodes[2].Term.Name, raiz.ChildNodes[2].Term.Name, raiz.ChildNodes[2].Token.Location.Line + 1, raiz.ChildNodes[2].Token.Location.Column + 1, null);
                            }   
                        }
                        return nodo;
                    }
                case "er_entero":
                    {
                        return new expresion(null, null, "ENTERO", "ENTERO", raiz.Token.Location.Line + 1, raiz.Token.Location.Column + 1, raiz.Token.ValueString);
                    }

                case "er_decimal":
                    {
                        return new expresion(null, null, "DECIMAL", "DECIMAL", raiz.Token.Location.Line + 1, raiz.Token.Location.Column + 1, raiz.Token.ValueString);
                    }

                case "er_caracter":
                    {
                        return new expresion(null, null, "CARACTER", "CARACTER", raiz.Token.Location.Line + 1, raiz.Token.Location.Column + 1, raiz.Token.ValueString);
                    }

                case "er_cadena":
                    {
                        return new expresion(null, null, "CADENA", "CADENA", raiz.Token.Location.Line + 1, raiz.Token.Location.Column + 1, raiz.Token.ValueString);
                    }

                case "er_booleano":
                    {
                        return new expresion(null, null, "BOOLEANO", "BOOLEANO", raiz.Token.Location.Line + 1, raiz.Token.Location.Column + 1, raiz.Token.ValueString);
                    }
                case "er_id":
                    {
                        return new llamadaObjeto(null, "VARIABLE", raiz.Token.ValueString, null, null, null, null, raiz.Token.Location.Line + 1, raiz.Token.Location.Column + 1);
                    }
                case "self":
                    {
                        return new expresion(null, null, "SELF", "SELF", raiz.Token.Location.Line + 1, raiz.Token.Location.Column + 1, "");
                    }
                case "super":
                    {
                        return new llamadaObjeto(null, "SUPER", "SUPER", null, null, null, null, raiz.Token.Location.Line + 1, raiz.Token.Location.Column + 1);
                    }
                case "OBJETO":
                    {
                        llamadaObjeto llamadaObjeto = null;
                        ParseTreeNode nodo = null;
                        for (int i = 0; i < raiz.ChildNodes.Count; i++) {
                            nodo = raiz.ChildNodes[i];
                            if (i == 0) {
                                llamadaObjeto = (llamadaObjeto)recorrerArbol(nodo);
                            }
                            else
                            {
                                llamadaObjeto llamadaObjeto_aux = (llamadaObjeto)recorrerArbol(nodo);
                                llamadaObjeto.set_hijo(llamadaObjeto_aux);
                            }
                        }
                        return llamadaObjeto;
                    }
                case "HIJO":
                    {
                        if (raiz.ChildNodes.Count == 1)
                        {
                            switch (raiz.ChildNodes[0].Term.Name)
                            {
                                case "super":
                                    return new llamadaObjeto(null, "SUPER", "SUPER", null, null, null, null, raiz.ChildNodes[0].Token.Location.Line + 1, raiz.ChildNodes[0].Token.Location.Column + 1);

                                case "self":
                                    return new llamadaObjeto(null, "SELF", "SELF", null, null, null, null, raiz.ChildNodes[0].Token.Location.Line + 1, raiz.ChildNodes[0].Token.Location.Column + 1);

                                default:
                                    return new llamadaObjeto(null, "VARIABLE", raiz.ChildNodes[0].Token.ValueString, null, null, null, null, raiz.ChildNodes[0].Token.Location.Line + 1, raiz.ChildNodes[0].Token.Location.Column + 1);
                            }
                        }
                        else
                        {
                            switch (raiz.ChildNodes[1].Term.Name)
                            {
                                case "LISTA_DIMENSION":
                                    {
                                        if (raiz.ChildNodes[1].ChildNodes.Count == 1)
                                        {
                                            List<expresion> lista_expresion = (List<expresion>)recorrerArbol(raiz.ChildNodes[1]);
                                            llamadaArregloMetodo llamada_arreglo_metodo = new llamadaArregloMetodo(raiz.ChildNodes[0].Token.ValueString, lista_expresion, null, raiz.ChildNodes[0].Token.Location.Line + 1, raiz.ChildNodes[0].Token.Location.Column + 1);
                                            return new llamadaObjeto(null, "ARREGLO_METODO", null, null, null, llamada_arreglo_metodo, null, llamada_arreglo_metodo.fila, llamada_arreglo_metodo.columna); ;
                                        }
                                        else
                                        {
                                            List<expresion> lista_expresion = (List<expresion>)recorrerArbol(raiz.ChildNodes[1]);
                                            llamadaArreglo llamada_arreglo = new llamadaArreglo(raiz.ChildNodes[0].Token.ValueString, lista_expresion, null, raiz.ChildNodes[0].Token.Location.Line + 1, raiz.ChildNodes[0].Token.Location.Column + 1);
                                            return new llamadaObjeto(null, "ARREGLO", null, null, llamada_arreglo, null, null, llamada_arreglo.fila, llamada_arreglo.columna);
                                        }
                                    }

                                case "LISTA_E_E":
                                    {
                                        List<expresion> lista_expresion = (List<expresion>)recorrerArbol(raiz.ChildNodes[1]);
                                        llamadaMetodo llamada_metodo = new llamadaMetodo(raiz.ChildNodes[0].Token.ValueString, lista_expresion, null, raiz.ChildNodes[0].Token.Location.Line + 1, raiz.ChildNodes[0].Token.Location.Column + 1);
                                        return new llamadaObjeto(null, "METODO", null, llamada_metodo, null, null, null, llamada_metodo.fila, llamada_metodo.columna);
                                    }
                            }
                        }
                    }
                    break;
                case "LISTA_E_E":
                    {
                        /*Sintetizando_lista_expresion*/
                        List<expresion> lista_expresion = new List<expresion>();
                        /*Si no esta vacio*/
                        if (raiz.ChildNodes.Count > 0)
                        {
                            ParseTreeNode nodo = raiz.ChildNodes[0];
                            List<expresion> lista_expresion_aux = (List<expresion>)recorrerArbol(nodo);
                            lista_expresion = lista_expresion_aux;
                        }
                        return lista_expresion;
                    }
                case "LISTA_E":
                    {
                        List<expresion> lista_expresion = new List<expresion>();
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            lista_expresion.Add((expresion)recorrerArbol(nodo));
                        }
                        return lista_expresion;
                    }
                case "LLAMADA_OBJETO":
                    {

                        llamadaObjeto retorna_objeto = null;
                        if (raiz.ChildNodes.Count == 1)
                        {
                            retorna_objeto = (llamadaObjeto)recorrerArbol(raiz.ChildNodes[0]);
                        }
                        else if (raiz.ChildNodes.Count == 2)
                        {
                            switch (raiz.ChildNodes[1].Term.Name)
                            {
                                /*objeto.id*/
                                case "er_id":
                                    {
                                        llamadaObjeto llamada_objeto_objeto = (llamadaObjeto)recorrerArbol(raiz.ChildNodes[0]);
                                        llamadaObjeto llamada_objeto_id  = new llamadaObjeto(null, "VARIABLE", raiz.Token.ValueString, null, null, null, null, raiz.Token.Location.Line + 1, raiz.Token.Location.Column + 1);
                                        llamada_objeto_objeto.set_hijo(llamada_objeto_id);
                                        retorna_objeto = llamada_objeto_objeto;
                                    }
                                    break;

                                /*arreglo[E][E]*/
                                case "LISTA_DIMENSION":
                                    {
                                        if (raiz.ChildNodes[1].ChildNodes.Count == 1)
                                        {
                                            List<expresion> lista_expresion = (List<expresion>)recorrerArbol(raiz.ChildNodes[1]);
                                            llamadaArregloMetodo llamada_arreglo_metodo = new llamadaArregloMetodo(raiz.ChildNodes[0].Token.ValueString, lista_expresion, null, raiz.ChildNodes[0].Token.Location.Line + 1, raiz.ChildNodes[0].Token.Location.Column + 1);
                                            llamadaObjeto llamada_objeto_arreglo_metodo = new llamadaObjeto(null, "ARREGLO_METODO", null, null, null, llamada_arreglo_metodo, null, llamada_arreglo_metodo.fila, llamada_arreglo_metodo.columna);
                                            retorna_objeto = llamada_objeto_arreglo_metodo;

                                        }
                                        else
                                        {
                                            List<expresion> lista_expresion = (List<expresion>)recorrerArbol(raiz.ChildNodes[1]);
                                            llamadaArreglo llamada_arreglo = new llamadaArreglo(raiz.ChildNodes[0].Token.ValueString, lista_expresion, null, raiz.ChildNodes[0].Token.Location.Line + 1, raiz.ChildNodes[0].Token.Location.Column + 1);
                                            llamadaObjeto llamada_objeto_arreglo = new llamadaObjeto(null, "ARREGLO", null, null, llamada_arreglo, null, null, llamada_arreglo.fila, llamada_arreglo.columna);
                                            retorna_objeto = llamada_objeto_arreglo;
                                        }
                                    }
                                    break;
                                /*metodo[]*/
                                case "LISTA_E_E":
                                    {
                                        List<expresion> lista_expresion = (List<expresion>)recorrerArbol(raiz.ChildNodes[1]);
                                        llamadaMetodo llamada_metodo = new llamadaMetodo(raiz.ChildNodes[0].Token.ValueString, lista_expresion, null, raiz.ChildNodes[0].Token.Location.Line + 1, raiz.ChildNodes[0].Token.Location.Column + 1);
                                        llamadaObjeto llamada_objeto_metodo = new llamadaObjeto(null, "METODO", null, llamada_metodo, null, null, null, llamada_metodo.fila, llamada_metodo.columna);
                                        retorna_objeto = llamada_objeto_metodo;
                                    }
                                    break;
                            }
                        }
                        else if (raiz.ChildNodes.Count == 3)
                        {
                            switch (raiz.ChildNodes[2].Term.Name)
                            {
                                case "LISTA_DIMENSION":
                                { 
                                    if (raiz.ChildNodes[2].ChildNodes.Count == 1)
                                    {
                                        llamadaObjeto llamada_objeto_objeto = (llamadaObjeto)recorrerArbol(raiz.ChildNodes[0]);
                                        List<expresion> lista_expresion = (List<expresion>)recorrerArbol(raiz.ChildNodes[2]);
                                        llamadaArregloMetodo llamada_arreglo_metodo = new llamadaArregloMetodo(raiz.ChildNodes[1].Token.ValueString, lista_expresion, null, raiz.ChildNodes[1].Token.Location.Line + 1, raiz.ChildNodes[1].Token.Location.Column + 1);
                                        llamadaObjeto llamada_objeto_arreglo_metodo = new llamadaObjeto(null, "ARREGLO_METODO", null, null, null, llamada_arreglo_metodo, null, llamada_arreglo_metodo.fila, llamada_arreglo_metodo.columna);
                                        llamada_objeto_objeto.set_hijo(llamada_objeto_arreglo_metodo);
                                        retorna_objeto = llamada_objeto_objeto;
                                    }
                                    else
                                    {
                                        llamadaObjeto llamada_objeto_objeto = (llamadaObjeto)recorrerArbol(raiz.ChildNodes[0]);
                                        List<expresion> lista_expresion = (List<expresion>)recorrerArbol(raiz.ChildNodes[2]);
                                        llamadaArreglo llamada_arreglo = new llamadaArreglo(raiz.ChildNodes[1].Token.ValueString, lista_expresion, null, raiz.ChildNodes[1].Token.Location.Line + 1, raiz.ChildNodes[1].Token.Location.Column + 1);
                                        llamadaObjeto llamada_objeto_arreglo = new llamadaObjeto(null, "ARREGLO", null, null, llamada_arreglo, null, null, llamada_arreglo.fila, llamada_arreglo.columna);
                                        llamada_objeto_objeto.set_hijo(llamada_objeto_arreglo);
                                        retorna_objeto = llamada_objeto_objeto;
                                    }
                                }
                                break;
                                case "LISTA_E_E":
                                    {
                                        llamadaObjeto llamada_objeto_objeto = (llamadaObjeto)recorrerArbol(raiz.ChildNodes[0]);
                                        List<expresion> lista_expresion = (List<expresion>)recorrerArbol(raiz.ChildNodes[2]);
                                        llamadaMetodo llamada_metodo = new llamadaMetodo(raiz.ChildNodes[1].Token.ValueString, lista_expresion, null, raiz.ChildNodes[1].Token.Location.Line + 1, raiz.ChildNodes[1].Token.Location.Column + 1);
                                        llamadaObjeto llamada_objeto_metodo = new llamadaObjeto(null, "METODO", null, llamada_metodo, null, null, null, llamada_metodo.fila, llamada_metodo.columna);
                                        llamada_objeto_objeto.set_hijo(llamada_objeto_metodo);
                                        retorna_objeto = llamada_objeto_objeto;
                                    }
                                break;
                            }
                        }
                        return new expresion(null, null, "LLAMADA_OBJETO", "LLAMADA_OBJETO", retorna_objeto.fila, retorna_objeto.columna, retorna_objeto);
                    }
            }
            return null;
        }


        private static void padre_simbolo(List<simbolo> tablaSimbolo, simbolo simbolo) {
            foreach (simbolo simbolo_sentencia in tablaSimbolo)
            {
                simbolo_sentencia.padre = simbolo;
            }
        }

        private static void padre_expresion(List<expresion> lista_expresion, simbolo simbolo)
        {
            foreach (expresion expresion in lista_expresion)
            {
                expresion.padre = simbolo;
            }
        }


        public static int posicion_simbolo(simbolo simbolo, int posicion)
        {
            switch (simbolo.rol)
            {
                case "CLASE":
                    foreach (simbolo simb in simbolo.ambito.tablaSimbolo)
                    {
                        if (simb.rol.Equals("DECLARACION"))
                        {
                            simb.posicion = posicion;
                            posicion= posicion + 1;
                        }
                        else
                        {
                            if (simbolo.tamanio > 0)
                            {
                                posicion_simbolo(simb, 0);
                            }
                        }
                    }
                    simbolo.tamanio = posicion;
                    simbolo.ambito.tamanio = posicion;
                    break;

                case "DECLARACION":
                    simbolo.posicion = posicion;
                    posicion = posicion + 1;
                    break;

                case "METODO":
                    foreach (simbolo simb in ((metodo)simbolo.valor).parametros)
                    {
                        simb.posicion = posicion;
                        posicion = posicion + 1;
                    }

                    foreach (simbolo simb in simbolo.ambito.tablaSimbolo)
                    {
                        posicion = posicion_simbolo(simb, posicion);
                    }
                    break;

                case "CONSTRUCTOR":
                    foreach (simbolo simb in ((metodo)simbolo.valor).parametros)
                    {
                        simb.posicion = posicion++;
                        posicion = posicion + 1; ;
                    }

                    foreach (simbolo simb in simbolo.ambito.tablaSimbolo)
                    {
                        posicion = posicion_simbolo(simb, posicion);
                    }
                    break;

                case "SI":
                    si si = (si)simbolo.valor;
                    try
                    {
                        foreach (simbolo simb in si.ambito.tablaSimbolo)
                        {
                            posicion = posicion_simbolo(simb, posicion);
                        }
                    }
                    catch { }

                    try
                    {
                        if (si.lista_sino_si != null)
                        {
                            foreach (sino_si sinosi in si.lista_sino_si)
                            {
                                foreach (simbolo simb in sinosi.ambito.tablaSimbolo)
                                {
                                    posicion = posicion_simbolo(simb, posicion);
                                }
                            }
                        }
                    }
                    catch { }

                    try
                    {
                        if (si.sino != null)
                        {
                            foreach (simbolo simb in si.sino.ambito.tablaSimbolo)
                            {
                                posicion = posicion_simbolo(simb, posicion);
                            }
                        }
                    }
                    catch { }

                    break;

                case "PARA":
                    para para = (para)simbolo.valor;
                    if (para.declara_asigna.rol.Equals("DECLARACION"))
                    {
                        para.declara_asigna.posicion = posicion;
                        posicion = posicion + 1;
                    }

                    foreach (simbolo simb in simbolo.ambito.tablaSimbolo)
                    {
                        posicion = posicion_simbolo(simb, posicion);
                    }
                    break;

                case "ELEGIR":
                    elegir elegir = (elegir)simbolo.valor;
                    foreach (caso caso in elegir.lista_caso)
                    {
                        foreach (simbolo simb in caso.ambito.tablaSimbolo)
                        {
                            posicion = posicion_simbolo(simb, posicion);
                        }
                    }

                    if (elegir.defecto != null)
                    {
                        foreach (simbolo simb in elegir.defecto.ambito.tablaSimbolo)
                        {
                            posicion = posicion_simbolo(simb, posicion);
                        }
                    }
                    break;

        
                default:
                    if (simbolo.tamanio > 0)
                    {
                        foreach (simbolo simb in simbolo.ambito.tablaSimbolo)
                        {
                            posicion = posicion_simbolo(simb, posicion);
                        }
                    }
                    break;
            }
            return posicion;
        }

    }
}
