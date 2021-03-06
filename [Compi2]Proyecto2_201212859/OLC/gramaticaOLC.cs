﻿using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.OLC
{
    [Language("OLC++", "1.0", "OLC++ Grammar")]
    class gramaticaOLC:Grammar
    {

        private readonly TerminalSet mSkipTokensInPreview = new TerminalSet(); //used in token preview for conflict resolution
        public gramaticaOLC(): base(caseSensitive: false)
        {
            CommentTerminal DelimitedComment = new CommentTerminal("DelimitedComment", "/-", "-/");
            CommentTerminal SingleLineComment = new CommentTerminal("SingleLineComment", "//", "\r", "\n", "\u2085", "\u2028", "\u2029");
            NonGrammarTerminals.Add(DelimitedComment);
            NonGrammarTerminals.Add(SingleLineComment);

            //Palabras Reservadas
            MarkReservedWords("clase");
            MarkReservedWords("hereda_de");
            MarkReservedWords("este");
            MarkReservedWords("@sobrescribir");
            MarkReservedWords("principal");
            MarkReservedWords("retornar");
            MarkReservedWords("imprimir");
            MarkReservedWords("new");
            MarkReservedWords("si");
            MarkReservedWords("sino");
            MarkReservedWords("mientras");
            MarkReservedWords("hacer");
            MarkReservedWords("x");
            MarkReservedWords("repetir");
            MarkReservedWords("until");
            MarkReservedWords("para");
            MarkReservedWords("entero");
            MarkReservedWords("cadena");
            MarkReservedWords("decimal");
            MarkReservedWords("booleano");
            MarkReservedWords("caracter");
            MarkReservedWords("void");
            MarkReservedWords("publico");
            MarkReservedWords("protegido");
            MarkReservedWords("privado");
            MarkReservedWords("importar");
            MarkReservedWords("llamar");
            MarkReservedWords("true");
            MarkReservedWords("false");
            MarkReservedWords("void");

            var t_clase = ToTerm("clase");
            var t_hereda_de = ToTerm("hereda_de");
            var t_este = ToTerm("este");
            var t_sobrescribir = ToTerm("@sobrescribir");
            var t_principal = ToTerm("principal");
            var t_retornar = ToTerm("retornar");
            var t_imprimir = ToTerm("imprimir");
            var t_new = ToTerm("new");
            var t_si = ToTerm("si");
            var t_sino = ToTerm("sino");
            var t_mientras = ToTerm("mientras");
            var t_hacer = ToTerm("hacer");
            var t_x = ToTerm("x");
            var t_repetir = ToTerm("repetir");
            var t_until = ToTerm("until");
            var t_para = ToTerm("para");
            var t_void = ToTerm("void");


            //Operadores Aritmeticos
            var t_mas = ToTerm("+");
            var t_menos = ToTerm("-");
            var t_por = ToTerm("*");
            var t_div = ToTerm("/");
            var t_potencia = ToTerm("^");
            var t_aumento = ToTerm("++");
            var t_decremento = ToTerm("--");

            //Operadores Relacionales
            var t_igualacion = ToTerm("==");
            var t_diferente = ToTerm("!=");
            var t_menor = ToTerm("<");
            var t_mayor = ToTerm(">");
            var t_menor_igual = ToTerm("<=");
            var t_mayor_igual = ToTerm(">=");

            //Operadores Relacionales
            var t_or = ToTerm("||");
            var t_xor = ToTerm("??");
            var t_and = ToTerm("&&");
            var t_not = ToTerm("!");

            //Puntuacion
            var t_par_abre = ToTerm("(");
            var t_par_cierra = ToTerm(")");
            var t_llave_abre = ToTerm("{");
            var t_llave_cierra = ToTerm("}");
            var t_cor_abre = ToTerm("[");
            var t_cor_cierra = ToTerm("]");
            var t_punto = ToTerm(".");
            var t_punto_coma = ToTerm(";");
            var t_coma = ToTerm(",");
            var t_igual = ToTerm("=");



            //Expresiones Regulares
            RegexBasedTerminal er_visibilidad = new RegexBasedTerminal("er_visibilidad", "publico|protegido|privado");
            RegexBasedTerminal er_tipo = new RegexBasedTerminal("er_tipo", "entero|cadena|decimal|booleano|caracter");
            RegexBasedTerminal er_importar = new RegexBasedTerminal("er_importar", "importar|llamar");
            RegexBasedTerminal er_entero = new RegexBasedTerminal("er_entero", "[0-9]+");
            RegexBasedTerminal er_decimal = new RegexBasedTerminal("er_decimal", "([0-9]+).([0-9]+)");
            RegexBasedTerminal er_caracter = new RegexBasedTerminal("er_caracter", "'[a-zA-Z0-9]'");
            RegexBasedTerminal er_cadena = new RegexBasedTerminal("er_cadena", "\"[^\n\r]*\"");
            RegexBasedTerminal er_booleano = new RegexBasedTerminal("er_booleano", "true|false");
            RegexBasedTerminal er_id = new RegexBasedTerminal("er_id", "[a-zA-Z][a-zA-Z0-9_-]*");

            var INICIO = new NonTerminal("INICIO");
            var IMPORTAR = new NonTerminal("IMPORTAR");
            var LISTA_IMPORTAR = new NonTerminal("LISTA_IMPORTAR");
            var DECLARAR = new NonTerminal("DECLARAR");
            var LISTA_ID = new NonTerminal("LISTA_ID");
            var E = new NonTerminal("E");
            var LISTA_E = new NonTerminal("LISTA_E");
            var METODO = new NonTerminal("METODO");
            var PARAMETRO = new NonTerminal("PARAMETRO");
            var LISTA_PARAMETRO = new NonTerminal("LISTA_PARAMETRO");
            var ASIGNAR = new NonTerminal("ASIGNAR");
            var SENTENCIAS_GLOBALES = new NonTerminal("SENTENCIAS_GLOBALES");
            var LISTA_SENTENCIAS_GLOBALES = new NonTerminal("LISTA_SENTENCIAS_GLOBALES");
            var SENTENCIAS_LOCALES = new NonTerminal("SENTENCIAS_LOCALES");
            var LISTA_SENTENCIAS_LOCALES = new NonTerminal("LISTA_SENTENCIAS_LOCALES");
            var RETORNAR = new NonTerminal("RETORNAR");
            var OBJETO = new NonTerminal("OBJETO");
            var TIPO = new NonTerminal("TIPO");
            var SI = new NonTerminal("SI");
            var SINO_SI = new NonTerminal("SINO_SI");
            var LISTA_SINO_SI = new NonTerminal("LISTA_SINO_SI");
            var SINO = new NonTerminal("SINO");
            var SENTENCIA_SI = new NonTerminal("SENTENCIA_SI");
            var MIENTRAS = new NonTerminal("MIENTRAS");
            var HACER_MIENTRAS = new NonTerminal("HACER_MIENTRAS");
            var X = new NonTerminal("X");
            var REPETIR = new NonTerminal("REPETIR");
            var PARA = new NonTerminal("PARA");
            var IMPRIMIR = new NonTerminal("IMPRIMIR");
            var AUMENTO_DECREMENTO = new NonTerminal("AUMENTO_DECREMENTO");
            var F = new NonTerminal("F");
            var LLAMADA_METODO = new NonTerminal("LLAMADA_METODO");
            var DIMENSION = new NonTerminal("DIMENSION");
            var LISTA_DIMENSION = new NonTerminal("LISTA_DIMENSION");
            var ARREGLO = new NonTerminal("ARREGLO");
            var LISTA_ARREGLO = new NonTerminal("LISTA_ARREGLO");
            var TIPO_RETORNO = new NonTerminal("TIPO_RETORNO");

            //Nuevas Producciones
            var LISTA_CLASE = new NonTerminal("LISTA_CLASE");
            var CLASE = new NonTerminal("CLASE");
            var VISIBILIDAD = new NonTerminal("VISIBILIDAD");
            var LISTA_PARAMETRO_E = new NonTerminal("LISTA_PARAMETRO_E");
            var LISTA_DIMENSION_METODO_E = new NonTerminal("LISTA_DIMENSION_METODO_E");
            var LISTA_DIMENSION_METODO = new NonTerminal("LISTA_DIMENSION_METODO");
            var DIMENSION_METODO = new NonTerminal("DIMENSION_METODO");
            var HIJO = new NonTerminal("HIJO");
            var LISTA_E_E = new NonTerminal("LISTA_E_E");



            /*Producciones*/
            /*Clases e importacion*/

            INICIO.Rule = LISTA_IMPORTAR + t_clase + er_id + t_llave_abre + LISTA_SENTENCIAS_GLOBALES + t_llave_cierra
                        /*herencia*/
                        | LISTA_IMPORTAR + t_clase + er_id + t_hereda_de + er_id + t_llave_abre + LISTA_SENTENCIAS_GLOBALES + t_llave_cierra
                        ;

            IMPORTAR.Rule = er_importar + t_par_abre + er_cadena + t_par_cierra + t_punto_coma
                | Empty
                ;

            /*Sentencias Globales*/
            SENTENCIAS_GLOBALES.Rule = VISIBILIDAD + DECLARAR + t_punto_coma
                | METODO
                ;

            DECLARAR.Rule =
                  TIPO + LISTA_ID + t_igual + E
                | TIPO + LISTA_ID
                | TIPO + er_id + LISTA_DIMENSION
                | TIPO + er_id + LISTA_DIMENSION + t_igual + E
                ;

            METODO.Rule = t_sobrescribir + VISIBILIDAD + TIPO + LISTA_DIMENSION_METODO_E + er_id + t_par_abre + LISTA_PARAMETRO_E + t_par_cierra + t_llave_abre + LISTA_SENTENCIAS_LOCALES + t_llave_cierra
                | VISIBILIDAD + TIPO + LISTA_DIMENSION_METODO_E + er_id + t_par_abre + LISTA_PARAMETRO_E + t_par_cierra + t_llave_abre + LISTA_SENTENCIAS_LOCALES + t_llave_cierra
                //Metodos
                | t_sobrescribir + VISIBILIDAD + t_void + er_id + t_par_abre + LISTA_PARAMETRO_E + t_par_cierra + t_llave_abre + LISTA_SENTENCIAS_LOCALES + t_llave_cierra
                | VISIBILIDAD + t_void + er_id + t_par_abre + LISTA_PARAMETRO_E + t_par_cierra + t_llave_abre + LISTA_SENTENCIAS_LOCALES + t_llave_cierra
                //Metodo Principal
                | t_principal + t_par_abre + t_par_cierra + t_llave_abre + LISTA_SENTENCIAS_LOCALES + t_llave_cierra
                //Constructor
                | VISIBILIDAD + er_id + t_par_abre + LISTA_PARAMETRO_E + t_par_cierra + t_llave_abre + LISTA_SENTENCIAS_LOCALES + t_llave_cierra
                ;

            LISTA_PARAMETRO_E.Rule = LISTA_PARAMETRO
                | Empty;

            LISTA_DIMENSION_METODO_E.Rule = LISTA_DIMENSION_METODO
                                    | Empty
                                    ;

            LISTA_DIMENSION_METODO.Rule = MakePlusRule(LISTA_DIMENSION_METODO, DIMENSION_METODO);

            DIMENSION_METODO.Rule = t_cor_abre + t_cor_cierra;


            /*Sentencias Locales*/
            SENTENCIAS_LOCALES.Rule =
                  DECLARAR + t_punto_coma
                | ASIGNAR + t_punto_coma
                | OBJETO + t_punto_coma
                | RETORNAR + t_punto_coma
                | LLAMADA_METODO + t_punto_coma  
                | SENTENCIA_SI 
                | MIENTRAS  
                | HACER_MIENTRAS 
                | X
                | REPETIR
                | PARA
                | IMPRIMIR + t_punto_coma
                | AUMENTO_DECREMENTO + t_punto_coma
                ;
                

            ASIGNAR.Rule = OBJETO + er_id + LISTA_DIMENSION + t_igual + E
                           | er_id + LISTA_DIMENSION + t_igual + E
                           | OBJETO + er_id + t_igual + E
                           | er_id + t_igual + E
                           ;

            RETORNAR.Rule = t_retornar + E
                ;

            SI.Rule = t_si + t_par_abre + E + t_par_cierra + t_llave_abre + LISTA_SENTENCIAS_LOCALES + t_llave_cierra
                ;

            SINO_SI.Rule = t_sino + t_si + t_par_abre + E + t_par_cierra  + t_llave_abre + LISTA_SENTENCIAS_LOCALES + t_llave_cierra
                ;

            SINO.Rule = t_sino + t_llave_abre + LISTA_SENTENCIAS_LOCALES + t_llave_cierra
                ;

            SENTENCIA_SI.Rule = SI
                | SI + SINO
                | SI + LISTA_SINO_SI
                | SI + LISTA_SINO_SI + SINO
                ;

            MIENTRAS.Rule = t_mientras + t_par_abre + E + t_par_cierra + t_llave_abre + LISTA_SENTENCIAS_LOCALES + t_llave_cierra
                ;

            HACER_MIENTRAS.Rule = t_hacer + t_llave_abre + LISTA_SENTENCIAS_LOCALES + t_llave_cierra + t_mientras + t_par_abre + E + t_par_cierra + t_punto_coma
                ;

            X.Rule = t_x + t_par_abre + E + t_coma + E + t_par_cierra + t_llave_abre + LISTA_SENTENCIAS_LOCALES + t_llave_cierra
                ;

            REPETIR.Rule = t_repetir + t_llave_abre + LISTA_SENTENCIAS_LOCALES + t_llave_cierra + t_until + t_par_abre + E + t_par_cierra + t_punto_coma
                ;

            PARA.Rule = t_para + t_par_abre + DECLARAR + t_punto_coma + E + t_punto_coma + AUMENTO_DECREMENTO + t_par_cierra + t_llave_abre + LISTA_SENTENCIAS_LOCALES + t_llave_cierra
                | t_para + t_par_abre + ASIGNAR + t_punto_coma + E + t_punto_coma + AUMENTO_DECREMENTO + t_par_cierra + t_llave_abre + LISTA_SENTENCIAS_LOCALES + t_llave_cierra
                ;

            IMPRIMIR.Rule = t_imprimir + t_par_abre + E + t_par_cierra;

            AUMENTO_DECREMENTO.Rule = er_id + t_aumento
                                      |er_id + t_decremento
                                      ;

            DIMENSION.Rule = t_cor_abre + E + t_cor_cierra;


            LLAMADA_METODO.Rule = OBJETO + er_id + t_par_abre + LISTA_E_E + t_par_cierra
                | er_id + t_par_abre + LISTA_E_E + t_par_cierra
                ;

            /*Parametros y tipos*/
            PARAMETRO.Rule = TIPO + LISTA_DIMENSION_METODO_E + er_id
              ;

            TIPO.Rule = er_tipo
                  | er_id
                  ;

            VISIBILIDAD.Rule = er_visibilidad
                               | Empty;


            /*Listas*/
            LISTA_IMPORTAR.Rule = MakePlusRule(LISTA_IMPORTAR, IMPORTAR)
                ;

            LISTA_SENTENCIAS_GLOBALES.Rule = MakePlusRule(LISTA_SENTENCIAS_GLOBALES, SENTENCIAS_GLOBALES)
                ;

            LISTA_SENTENCIAS_LOCALES.Rule = MakePlusRule(LISTA_SENTENCIAS_LOCALES, SENTENCIAS_LOCALES)
                ;

            LISTA_SINO_SI.Rule = MakePlusRule(LISTA_SINO_SI, SINO_SI)
                ;

            LISTA_ID.Rule = MakePlusRule(LISTA_ID, t_coma, er_id)
                ;

            LISTA_PARAMETRO.Rule = MakePlusRule(LISTA_PARAMETRO, t_coma, PARAMETRO)
                ;

            LISTA_DIMENSION.Rule = MakePlusRule(LISTA_DIMENSION, DIMENSION)
                ;

            LISTA_E.Rule = MakePlusRule(LISTA_E, t_coma, E)
                ;

            LISTA_E_E.Rule = LISTA_E
             | Empty
             ;

            /*Expresiones*/
            E.Rule= E + t_mas + E
                | E + t_menos + E
                | E + t_por + E
                | E + t_div + E
                | E + t_potencia + E
                | E + t_igualacion + E
                | E + t_diferente + E
                | E + t_menor + E
                | E + t_mayor + E
                | E + t_menor_igual + E
                | E + t_mayor_igual + E
                | E + t_or + E
                | E + t_xor + E
                | E + t_and + E
                | t_not + E
                | t_menos + E
                | er_entero
                | er_decimal
                | er_caracter
                | er_cadena
                | er_booleano
                | OBJETO + er_id
                | OBJETO + er_id + t_par_abre + LISTA_E_E + t_par_cierra
                | OBJETO + er_id + LISTA_DIMENSION
                | er_id
                | er_id + t_par_abre + LISTA_E_E + t_par_cierra
                | er_id + LISTA_DIMENSION
                | t_new + er_id + t_cor_abre + LISTA_E_E + t_cor_cierra
                | t_par_abre + E + t_par_cierra
                | t_llave_abre + LISTA_E + t_llave_cierra;
            ;


            OBJETO.Rule = MakePlusRule(OBJETO, HIJO)
                ;

            HIJO.Rule = er_id + t_punto
                | er_id + t_par_abre + LISTA_E_E + t_par_cierra + t_punto
                | er_id + LISTA_DIMENSION + t_punto
                | t_este + t_punto
                ;


            RegisterBracePair("(", ")");
            RegisterBracePair("[", "]");
            RegisterBracePair("{", "}");
            RegisterOperators(1, Associativity.Left, "||", "??");
            RegisterOperators(2, Associativity.Left, "&&");
            RegisterOperators(3, Associativity.Right, "!");
            RegisterOperators(4, Associativity.Left, "==", "!=", "<", ">", "<=", ">=");
            RegisterOperators(6, Associativity.Left, "+", "-");
            RegisterOperators(7, Associativity.Left, "*", "/");
            RegisterOperators(8, Associativity.Left, "^");


            MarkPunctuation(";", ",", ".");
            Root = INICIO;
        }
    }
}


///*Metodos heredaras*/
//| t_sobrescribir + er_visibilidad  + er_id + t_par_abre + LISTA_PARAMETRO + t_par_cierra + t_llave_abre + LISTA_SENTENCIAS_LOCALES + t_llave_cierra
//| t_sobrescribir + er_visibilidad  + er_id + t_par_abre + t_par_cierra + t_llave_abre + LISTA_SENTENCIAS_LOCALES + t_llave_cierra
//| t_sobrescribir + er_id + t_par_abre + LISTA_PARAMETRO + t_par_cierra + t_llave_abre + LISTA_SENTENCIAS_LOCALES + t_llave_cierra
//| t_sobrescribir + er_id + t_par_abre + t_par_cierra + t_llave_abre + LISTA_SENTENCIAS_LOCALES + t_llave_cierra

/*Metodos propias de la clase*/
//| er_visibilidad + er_id + t_par_abre + LISTA_PARAMETRO + t_par_cierra + t_llave_abre + LISTA_SENTENCIAS_LOCALES + t_llave_cierra
//| er_visibilidad + er_id + t_par_abre + t_par_cierra + t_llave_abre + LISTA_SENTENCIAS_LOCALES + t_llave_cierra
//| er_id + t_par_abre + LISTA_PARAMETRO + t_par_cierra + t_llave_abre + LISTA_SENTENCIAS_LOCALES + t_llave_cierra
//| er_id + t_par_abre + t_par_cierra + t_llave_abre + LISTA_SENTENCIAS_LOCALES + t_llave_cierra

//| E + t_aumento + E
//| E + t_decremento + E
