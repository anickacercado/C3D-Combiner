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
                                    declaracion declaracion = new declaracion("PUBLICO", tipo, variable.Token.ValueString, lista_expresion, ambito, variable.Token.Location.Line + 1, variable.Token.Location.Column, null);
                                    simbolo simbolo = new simbolo(declaracion.visibilidad, declaracion.tipo, declaracion.nombre, "DECLARACION", declaracion.fila, declaracion.columna, declaracion.ambito, declaracion);
                                    simbolo.tamanio = 1;
                                    declaracion.padre = simbolo;
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
                                        declaracion declaracion = new declaracion("PUBLICO", tipo, variable.Token.ValueString, lista_expresion, ambito, variable.Token.Location.Line + 1, variable.Token.Location.Column, valor);
                                        simbolo simbolo = new simbolo(declaracion.visibilidad, declaracion.tipo, declaracion.nombre, "DECLARACION", declaracion.fila, declaracion.columna, declaracion.ambito, declaracion);
                                        simbolo.tamanio = 1;
                                        declaracion.padre = simbolo;
                                        tablaSimbolo.Add(simbolo);
                                    }
                                }
                                else
                                {          
                                    List<expresion> lista_expresion =  (List<expresion>)recorrerArbol(raiz.ChildNodes[2]);
                                    ambito ambito = new ambito("DECLARACION");
                                    declaracion declaracion = new declaracion("PUBLICO", tipo, raiz.ChildNodes[1].Token.ValueString, lista_expresion, ambito, raiz.ChildNodes[1].Token.Location.Line + 1, raiz.ChildNodes[1].Token.Location.Column, null);
                                    simbolo simbolo = new simbolo(declaracion.visibilidad, declaracion.tipo, declaracion.nombre, "DECLARACION", declaracion.fila, declaracion.columna, declaracion.ambito, declaracion);
                                    simbolo.tamanio = 1;
                                    declaracion.padre = simbolo;
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
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            Object o = recorrerArbol(nodo);
                        }
                    }
                    break;
                case "PARAMETRO":
                    {
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            Object o = recorrerArbol(nodo);
                        }
                    }
                    break;
                case "LISTA_PARAMETRO":
                    {
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            Object o = recorrerArbol(nodo);
                        }
                    }
                    break;
                case "ASIGNAR":
                    {
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            Object o = recorrerArbol(nodo);
                        }
                    }
                    break;
                case "SENTENCIAS_GLOBALES":
                    {
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            Object o = recorrerArbol(nodo);
                        }
                    }
                    break;
                case "LISTA_SENTENCIAS_GLOBALES":
                    {
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            Object o = recorrerArbol(nodo);
                        }
                    }
                    break;
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
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            Object o = recorrerArbol(nodo);
                        }
                    }
                    break;
                case "TIPO":
                    {
                        return raiz.ChildNodes[0].Token.ToString();
                    }
                case "SI":
                    {
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            Object o = recorrerArbol(nodo);
                        }
                    }
                    break;
                case "SINO_SI":
                    {
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            Object o = recorrerArbol(nodo);
                        }
                    }
                    break;
                case "LISTA_SINO_SI":
                    {
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            Object o = recorrerArbol(nodo);
                        }
                    }
                    break;
                case "SINO":
                    {
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            Object o = recorrerArbol(nodo);
                        }
                    }
                    break;
                case "SENTENCIA_SI":
                    {
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            Object o = recorrerArbol(nodo);
                        }
                    }
                    break;
                case "MIENTRAS":
                    {
                        expresion expresion = (expresion)recorrerArbol(raiz.ChildNodes[1]);
                        List<simbolo> tablaSimbolo = (List<simbolo>)recorrerArbol(raiz.ChildNodes[2]);
                        ambito ambito = new codigo3D.ambito("MIENTRAS", tablaSimbolo);

                        mientras mientras = new mientras(expresion, ambito);
                        simbolo simbolo = new simbolo("MIENTRAS", "MIENTRAS", "MIENTRAS", "MIENTRAS", raiz.ChildNodes[0].Token.Location.Line + 1, raiz.ChildNodes[0].Token.Location.Column + 1, mientras.ambito, mientras);
                        simbolo.posicion = -1;
                        mientras.padre = simbolo;

                        set_simbolo(tablaSimbolo, simbolo);

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
                        simbolo.posicion = -1;
                        hacer_mientras.padre = simbolo;

                        set_simbolo(tablaSimbolo, simbolo);

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
                        simbolo.posicion = -1;
                        repetir_hasta.padre = simbolo;

                        set_simbolo(tablaSimbolo, simbolo);

                        List<simbolo> retorna_tabla_simbolo = new List<simbolo>();
                        retorna_tabla_simbolo.Add(simbolo);
                        return retorna_tabla_simbolo;
                    }
                case "AUMENTO_DECREMENTO":
                    {
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            Object o = recorrerArbol(nodo);
                        }
                    }
                    break;
                case "LLAMADA_METODO":
                    {
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            Object o = recorrerArbol(nodo);
                        }
                    }
                    break;
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
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            Object o = recorrerArbol(nodo);
                        }
                    }
                    break;
                case "CONTINUAR":
                    {
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            Object o = recorrerArbol(nodo);
                        }
                    }
                    break;
                case "ELEGIR":
                    {
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            Object o = recorrerArbol(nodo);
                        }
                    }
                    break;
                case "CASO":
                    {
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            Object o = recorrerArbol(nodo);
                        }
                    }
                    break;
                case "LISTA_CASO":
                    {
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            Object o = recorrerArbol(nodo);
                        }
                    }
                    break;
                case "VALOR":
                    {
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            Object o = recorrerArbol(nodo);
                        }
                    }
                    break;
                case "LOOP":
                    {
                        List<simbolo> tablaSimbolo = (List<simbolo>)recorrerArbol(raiz.ChildNodes[1]);
                        ambito ambito = new codigo3D.ambito("LOOP", tablaSimbolo);

                        loop loop = new loop(ambito);
                        simbolo simbolo = new simbolo("LOOP", "LOOP", "LOOP", "LOOP", raiz.ChildNodes[0].Token.Location.Line + 1, raiz.ChildNodes[0].Token.Location.Column + 1, loop.ambito, loop);
                        simbolo.posicion = -1;
                        loop.padre = simbolo;

                        set_simbolo(tablaSimbolo, simbolo);

                        List<simbolo> retorna_tabla_simbolo = new List<simbolo>();
                        retorna_tabla_simbolo.Add(simbolo);
                        return retorna_tabla_simbolo;
                    }
                case "PARA":
                    {
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            Object o = recorrerArbol(nodo);
                        }
                    }
                    break;
                case "LISTA_CLASE":
                    {
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            Object o = recorrerArbol(nodo);
                        }
                    }
                    break;
                case "CLASE":
                    {
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            Object o = recorrerArbol(nodo);
                        }
                    }
                    break;
                case "VISIBILIDAD":
                    {
                        String visibilidad = "PUBLICO";
                        if (raiz.ChildNodes.Count > 0) {
                            visibilidad = raiz.ChildNodes[0].Token.ToString();
                        }
                        return visibilidad;
                        
                    }
                case "LISTA_PARAMETRO_E":
                    {
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            Object o = recorrerArbol(nodo);
                        }
                    }
                    break;
                case "LISTA_DIMENSION_METODO_E":
                    {
                        foreach (ParseTreeNode nodo in raiz.ChildNodes)
                        {
                            Object o = recorrerArbol(nodo);
                        }
                    }
                    break;
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
                            expresion izq = (expresion)recorrerArbol(raiz.ChildNodes[0]);
                            nodo = new expresion(null, izq, raiz.ChildNodes[1].Term.Name, raiz.ChildNodes[1].Term.Name, raiz.ChildNodes[1].Token.Location.Line + 1, raiz.ChildNodes[1].Token.Location.Column + 1, null);
                 
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


        private static void set_simbolo(List<simbolo> tablaSimbolo, simbolo simbolo) {
            foreach (simbolo simbolo_sentencia in tablaSimbolo)
            {
                simbolo_sentencia.Padre = simbolo;
            }
        }
    }
}
