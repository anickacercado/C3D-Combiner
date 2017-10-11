using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _Compi2_Proyecto2_201212859.TREE
{

    [Language("TREE", "1.0", "TREE Grammar")]
    class gramaticaTREE:Grammar
    {

        private readonly TerminalSet mSkipTokensInPreview = new TerminalSet(); //used in token preview for conflict resolution
        public gramaticaTREE() : base(caseSensitive: false)
        {

            CommentTerminal DelimitedComment = new CommentTerminal("DelimitedComment", "{--", "--}");
            CommentTerminal SingleLineComment = new CommentTerminal("SingleLineComment", "##", "\r", "\n", "\u2085", "\u2028", "\u2029");
            NonGrammarTerminals.Add(DelimitedComment);
            NonGrammarTerminals.Add(SingleLineComment);

            //Operadores Aritmeticos
            var t_mas = ToTerm("+");
            var t_menos = ToTerm("-");
            var t_por = ToTerm("*");
            var t_div = ToTerm("/");
            var t_potencia = ToTerm("pow");
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
            var t_or = ToTerm("or");
            var t_xor = ToTerm("xor");
            var t_and = ToTerm("and");
            var t_not = ToTerm("not");

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
            var t_dos_pts = ToTerm(":");
      
            var t_clase = ToTerm("clase");
            var t_self = ToTerm("self");
            var t_sobrescribir = ToTerm("/**sobrescribir**/");
            var t_constructor = ToTerm("__constructor");
            var t_retornar = ToTerm("retornar");
            var t_imprimir = ToTerm("imprimir");
            var t_mientras = ToTerm("mientras");
            var t_hacer = ToTerm("hacer");
            var t_repetir = ToTerm("repetir");
            var t_hasta = ToTerm("hasta");
            var t_importar = ToTerm("importar");
            var t_si = ToTerm("si");
            var t_si_no_si = ToTerm("si_no_si");
            var t_si_no = ToTerm("si_no");
            var t_salir = ToTerm("salir");
            var t_continuar = ToTerm("continuar");
            var t_elegir = ToTerm("elegir");
            var t_caso = ToTerm("caso");
            var t_defecto = ToTerm("defecto");
            var t_loop = ToTerm("loop");
            var t_para = ToTerm("para");
            var t_nuevo = ToTerm("nuevo");
            var t_super = ToTerm("super");



            //Expresiones Regulares
            RegexBasedTerminal er_visibilidad = new RegexBasedTerminal("er_visibilidad", "publico|protegido|privado");
            RegexBasedTerminal er_tipo = new RegexBasedTerminal("er_tipo", "entero|cadena|decimal|booleano|caracter|void");
            RegexBasedTerminal er_casteo = new RegexBasedTerminal("er_casteo", "ParseInt|ParseDouble|intToStr|doubleToStr|doubleToInt");

            // RegexBasedTerminal er_entero = new RegexBasedTerminal("er_entero", "[0-9]+");
            // RegexBasedTerminal er_decimal = new RegexBasedTerminal("er_decimal", "([0-9]+).([0-9]+)");
            RegexBasedTerminal er_caracter = new RegexBasedTerminal("er_caracter", "'[a-zA-Z0-9]'");
            RegexBasedTerminal er_cadena = new RegexBasedTerminal("er_cadena", "\"[^\n\r]*\"");
            RegexBasedTerminal er_booleano = new RegexBasedTerminal("er_booleano", "true|false");
            RegexBasedTerminal er_id = new RegexBasedTerminal("er_id", "[a-zA-Z][a-zA-Z0-9_-]*");
            NumberLiteral numero = new NumberLiteral("numero");

            //Palabras Reservadas

            MarkReservedWords("clase");
            MarkReservedWords("self");
            MarkReservedWords("/**sobrescribir**/");
            MarkReservedWords("retornar");
            MarkReservedWords("entero");
            MarkReservedWords("cadena");
            MarkReservedWords("decimal");
            MarkReservedWords("booleano");
            MarkReservedWords("caracter");
            MarkReservedWords("void");
            MarkReservedWords("publico");
            MarkReservedWords("protegido");
            MarkReservedWords("privado");
            MarkReservedWords("true");
            MarkReservedWords("false");
            MarkReservedWords("or");
            MarkReservedWords("xor");
            MarkReservedWords("and");
            MarkReservedWords("not");
            MarkReservedWords("__constructor");
            MarkReservedWords("importar");
            MarkReservedWords("si");
            MarkReservedWords("si_no_si");
            MarkReservedWords("si_no");
            MarkReservedWords("salir");
            MarkReservedWords("continuar");
            MarkReservedWords("elegir");
            MarkReservedWords("caso");
            MarkReservedWords("defecto");
            MarkReservedWords("repetir");
            MarkReservedWords("hasta");
            MarkReservedWords("mientras");
            MarkReservedWords("hacer");
            MarkReservedWords("imprimir");
            MarkReservedWords("loop");
            MarkReservedWords("para");
            MarkReservedWords("ParseInt");
            MarkReservedWords("ParseDouble");
            MarkReservedWords("intToStr");
            MarkReservedWords("doubleToStr");
            MarkReservedWords("doubleToInt");
            MarkReservedWords("nuevo");
            MarkReservedWords("super");

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
            var REPETIR = new NonTerminal("REPETIR");           
            var AUMENTO_DECREMENTO = new NonTerminal("AUMENTO_DECREMENTO");
            var F = new NonTerminal("F");
            var LLAMADA_METODO = new NonTerminal("LLAMADA_METODO");
            var DIMENSION = new NonTerminal("DIMENSION");
            var LISTA_DIMENSION = new NonTerminal("LISTA_DIMENSION");
            var ARREGLO = new NonTerminal("ARREGLO");
            var LISTA_ARREGLO = new NonTerminal("LISTA_ARREGLO");
            var TIPO_RETORNO = new NonTerminal("TIPO_RETORNO");
            var OPCION_IMPORTAR = new NonTerminal("OPCION_IMPORTAR");
            var SALIR = new NonTerminal("SALIR");
            var CONTINUAR = new NonTerminal("CONTINUAR");
            var ELEGIR = new NonTerminal("ELEGIR");
            var CASO = new NonTerminal("CASO");
            var LISTA_CASO = new NonTerminal("LISTA_CASO");
            var VALOR = new NonTerminal("VALOR");
            var LOOP = new NonTerminal("LOOP");
            var PARA = new NonTerminal("PARA");






            /*Producciones*/
            /*Clases e importacion*/

            INICIO.Rule = IMPORTAR + Eos + t_clase + er_id + t_cor_abre + t_cor_cierra + t_dos_pts + Eos + Indent + LISTA_SENTENCIAS_GLOBALES + Dedent
                      |IMPORTAR + Eos + t_clase + er_id + t_cor_abre + er_id + t_cor_cierra + t_dos_pts + Eos + Indent + LISTA_SENTENCIAS_GLOBALES + Dedent         /*Herencia*/
                      | t_clase + er_id + t_cor_abre + t_cor_cierra + t_dos_pts + Eos + Indent + LISTA_SENTENCIAS_GLOBALES + Dedent
                      | t_clase + er_id + t_cor_abre + er_id + t_cor_cierra + t_dos_pts + Eos + Indent + LISTA_SENTENCIAS_GLOBALES + Dedent                         /*Herencia*/
                      ;
            ;

            IMPORTAR.Rule = t_importar + LISTA_IMPORTAR
             ;

            OPCION_IMPORTAR.Rule = er_id + ".olc"
                            | er_id + ".tree"
                            | er_cadena
                            ;

            /*Sentencias Globales*/

            SENTENCIAS_GLOBALES.Rule =
                 METODO 
                |DECLARAR + Eos
                |OBJETO + Eos
                ;


            DECLARAR.Rule =
                er_tipo + LISTA_ID                                                                                                     /*Declarar Variable*/
              | er_tipo + LISTA_ID  + "=>" + E                                                                                         /*Declarar Variable y asignar*/

              | er_tipo + er_id + LISTA_DIMENSION
              | er_tipo + er_id + LISTA_DIMENSION + "=>" + E                         /*Declarar Arreglos*/
              | er_visibilidad + er_tipo + er_id + LISTA_DIMENSION
              | er_visibilidad + er_tipo + er_id + LISTA_DIMENSION + "=>" + E        /*Declarar Arreglos*/

              | er_visibilidad + er_tipo + LISTA_ID                                                                                     /*Declarar Variable*/
              | er_visibilidad + er_tipo + LISTA_ID + "=>" + E                                                                          /*Declarar Variable y asignar*/
              ;

            METODO.Rule =
                        /*Funciones propias de la clase*/
                        er_visibilidad + TIPO_RETORNO + er_id + t_cor_abre + LISTA_PARAMETRO + t_cor_cierra + t_dos_pts + Eos + Indent + LISTA_SENTENCIAS_LOCALES + Dedent
                      | er_visibilidad + TIPO_RETORNO + er_id + t_cor_abre + t_cor_cierra + t_dos_pts + Eos + Indent + LISTA_SENTENCIAS_LOCALES + Dedent
                      | TIPO_RETORNO + er_id + t_cor_abre + LISTA_PARAMETRO + t_cor_cierra + t_dos_pts + Eos + Indent + LISTA_SENTENCIAS_LOCALES + Dedent
                      | TIPO_RETORNO + er_id + t_cor_abre + t_cor_cierra + t_dos_pts + Eos + Indent + LISTA_SENTENCIAS_LOCALES + Dedent

                      /*Funciones heredadas*/
                      | t_sobrescribir + Eos + er_visibilidad + TIPO_RETORNO + er_id + t_cor_abre + LISTA_PARAMETRO + t_cor_cierra + t_dos_pts + Eos + Indent + LISTA_SENTENCIAS_LOCALES + Dedent
                      | t_sobrescribir + Eos + er_visibilidad + TIPO_RETORNO + er_id + t_cor_abre + t_cor_cierra + t_dos_pts + Eos + Indent + LISTA_SENTENCIAS_LOCALES + Dedent
                      | t_sobrescribir + Eos + TIPO_RETORNO + er_id + t_cor_abre + LISTA_PARAMETRO + t_cor_cierra + t_dos_pts + Eos + Indent + LISTA_SENTENCIAS_LOCALES + Dedent
                      | t_sobrescribir + Eos + TIPO_RETORNO + er_id + t_cor_abre + t_cor_cierra + t_dos_pts + Eos + Indent + LISTA_SENTENCIAS_LOCALES + Dedent

                      /*Metodo constructor*/
                      | t_constructor + t_cor_abre + t_cor_cierra + t_dos_pts + Eos + Indent + LISTA_SENTENCIAS_LOCALES + Dedent
                      | t_constructor + t_cor_abre + LISTA_PARAMETRO + t_cor_cierra + t_dos_pts + Eos + Indent + LISTA_SENTENCIAS_LOCALES + Dedent
                      ;

            /*Sentencias Locales*/
            SENTENCIAS_LOCALES.Rule = DECLARAR + Eos
                                    | ASIGNAR + Eos
                                    | RETORNAR + Eos
                                    | SENTENCIA_SI
                                    | SALIR + Eos
                                    | CONTINUAR + Eos
                                    | ELEGIR
                                    | MIENTRAS
                                    | HACER_MIENTRAS
                                    | REPETIR
                                    | LOOP
                                    | PARA
                                    | OBJETO + Eos
                                    | F + Eos /*metodos*/
                                    ;

            ASIGNAR.Rule =    t_self + t_punto + er_id + "=>" + E                             /*Asignar atributos*/
                            | t_self + t_punto + er_id + LISTA_DIMENSION + "=>" + E            /*Asignar atributos arreglos*/
                            | er_id + LISTA_DIMENSION + "=>" + E                               /*Asignar arreglos*/
                            | er_id + "=>" + E                                                      /*Asignar variables*/
                        ;

            RETORNAR.Rule = t_retornar + E
                ;

            SI.Rule = t_si  + E + t_dos_pts + Eos+ Indent + LISTA_SENTENCIAS_LOCALES + Dedent
                ;

            SINO_SI.Rule = t_si_no_si + E + t_dos_pts + Eos + Indent + LISTA_SENTENCIAS_LOCALES + Dedent
                ;

            SINO.Rule = t_si_no + t_dos_pts + Eos + Indent + LISTA_SENTENCIAS_LOCALES + Dedent
                ;

            SENTENCIA_SI.Rule = SI
                | SI + SINO
                | SI + LISTA_SINO_SI
                | SI + LISTA_SINO_SI + SINO
                ;

            SALIR.Rule = t_salir
                ;

            CONTINUAR.Rule = t_continuar
                ;

            ELEGIR.Rule = t_elegir + t_caso + E + t_dos_pts + Eos + Indent + LISTA_CASO + Dedent
                ;

            CASO.Rule = VALOR + t_dos_pts + Eos + Indent + LISTA_SENTENCIAS_LOCALES + Dedent
                |t_defecto + t_dos_pts + Eos + Indent + LISTA_SENTENCIAS_LOCALES + Dedent
                ;

            MIENTRAS.Rule = t_mientras + E + t_dos_pts + Eos + Indent + LISTA_SENTENCIAS_LOCALES + Dedent
                ;

            HACER_MIENTRAS.Rule = t_hacer + t_dos_pts + Eos + Indent + LISTA_SENTENCIAS_LOCALES + Dedent + t_mientras + E + Eos
                ;

            REPETIR.Rule = t_repetir + t_dos_pts + Eos + Indent + LISTA_SENTENCIAS_LOCALES + Dedent + t_hasta + E + Eos
                ;

            LOOP.Rule = t_loop + t_dos_pts + Eos + Indent + LISTA_SENTENCIAS_LOCALES + Dedent
                ;

            AUMENTO_DECREMENTO.Rule = t_par_abre + er_id + t_aumento + t_par_cierra
                            | t_par_abre + er_id + t_decremento + t_par_cierra
                            ;

            PARA.Rule =  t_para + t_cor_abre + ASIGNAR + t_dos_pts + E + t_dos_pts + AUMENTO_DECREMENTO + t_cor_cierra + t_dos_pts + Eos + Indent + LISTA_SENTENCIAS_LOCALES + Dedent
                | t_para + t_cor_abre + DECLARAR + t_dos_pts + E + t_dos_pts + AUMENTO_DECREMENTO + t_cor_cierra + t_dos_pts + Eos + Indent + LISTA_SENTENCIAS_LOCALES + Dedent
                ;


            OBJETO.Rule =
                  er_id + er_id                                                                                     /*Declarar Objetos*/

                //| er_id + er_id + LISTA_DIMENSION                                                                   /*Declarar Arreglos de Objetos*/
                //| er_id + er_id + LISTA_DIMENSION + t_igual + t_llave_abre + LISTA_E + t_llave_cierra               /*Declarar e Asignar Arreglos de Objetos*/

                | er_id + "=>" + t_nuevo + er_id + t_cor_abre + t_cor_cierra                                       /*Instanciar Objetos*/
                | er_id + "=>" + t_nuevo + er_id + t_cor_abre + LISTA_E + t_cor_cierra                             /*Instanciar Objetos*/
                | er_id + er_id + "=>" + t_nuevo + er_id + t_cor_abre + t_cor_cierra                               /*Declarar e Instanciar Objetos*/
                | er_id + er_id + "=>" + t_nuevo + er_id + t_cor_abre + LISTA_E + t_cor_cierra                     /*Declarar e Instanciar Objetos*/
                ;

            DIMENSION.Rule = t_cor_abre + E + t_cor_cierra;


            /*Parametros y tipos*/
            PARAMETRO.Rule = er_tipo + er_id
                | er_id + er_id
                ;

            TIPO_RETORNO.Rule = er_tipo
                //| er_id
                  | er_tipo + LISTA_DIMENSION
                  | er_id + LISTA_DIMENSION
                  ;

            /*Listas*/
            LISTA_SENTENCIAS_GLOBALES.Rule = MakePlusRule(LISTA_SENTENCIAS_GLOBALES, SENTENCIAS_GLOBALES)
               ;

            LISTA_SENTENCIAS_LOCALES.Rule = MakePlusRule(LISTA_SENTENCIAS_LOCALES, SENTENCIAS_LOCALES)
               ;

            LISTA_ID.Rule = MakePlusRule(LISTA_ID, t_coma, er_id)
                ;

            LISTA_PARAMETRO.Rule = MakePlusRule(LISTA_PARAMETRO, t_coma, PARAMETRO)
                ;

            LISTA_IMPORTAR.Rule = MakePlusRule(LISTA_IMPORTAR, t_coma, OPCION_IMPORTAR)
                ;

            LISTA_SINO_SI.Rule = MakePlusRule(LISTA_SINO_SI, SINO_SI)
                ;

            LISTA_CASO.Rule = MakePlusRule(LISTA_CASO, CASO)
                ;

            LISTA_DIMENSION.Rule = MakePlusRule(LISTA_DIMENSION, DIMENSION)
                ;

            LISTA_E.Rule = MakePlusRule(LISTA_E, t_coma, E)
                ;

            /*Expresiones*/
            E.Rule = t_llave_abre + E + E + t_or + t_llave_cierra
                | t_llave_abre + E + E + t_xor + t_llave_cierra
                | t_llave_abre + E + E + t_and + t_llave_cierra
                | t_llave_abre + E + t_not + t_llave_cierra
                | t_cor_abre + E + E + t_igualacion + t_cor_cierra
                | t_cor_abre + E + E + t_diferente + t_cor_cierra
                | t_cor_abre + E + E + t_menor + t_cor_cierra
                | t_cor_abre + E + E + t_mayor + t_cor_cierra
                | t_cor_abre + E + E + t_menor_igual + t_cor_cierra
                | t_cor_abre + E + E + t_mayor_igual + t_cor_cierra
                | t_par_abre + E + E + t_mas + t_par_cierra
                | t_par_abre + E + E + t_menos + t_par_cierra
                | t_par_abre + E + E + t_por + t_par_cierra
                | t_par_abre + E + E + t_div + t_par_cierra
                | t_par_abre + E + E + t_potencia + t_par_cierra
                | t_par_abre + E + t_menos + t_par_cierra
                | er_casteo + t_cor_abre + E + t_cor_cierra                 /*casteo*/
                | numero
                | er_caracter
                | er_cadena
                | er_booleano
                | F
            ;

            /*Llamada metodo - id*/
            F.Rule =
                  er_id                                                 /*id*/
                | t_super                                               /*super*/
                | t_self                                                /*self*/
                | er_id + t_cor_abre + t_cor_cierra                     /*Metodo sin parametros*/
                | er_id + t_cor_abre + LISTA_E + t_cor_cierra           /*Metodo con parametros*/
                | er_id + LISTA_DIMENSION                               /*Arreglo*/
                | F + t_punto + F
                ;

            VALOR.Rule = numero
                | er_caracter
                | er_cadena
                | er_booleano
                | er_id
            ;

            RegisterBracePair("(", ")");
            RegisterBracePair("[", "]");
            RegisterBracePair("{", "}");
            RegisterOperators(1, Associativity.Left,  t_or, t_xor);
            RegisterOperators(2, Associativity.Left,  t_and);
            RegisterOperators(3, Associativity.Left, t_not);
            RegisterOperators(4, Associativity.Left, "==", "!=", "<", ">", "<=", ">=");
            RegisterOperators(6, Associativity.Left, "+", "-");
            RegisterOperators(7, Associativity.Left, "*", "/");
            RegisterOperators(8, Associativity.Left, "pow");
            RegisterOperators(9, numero, er_caracter, er_cadena, er_booleano);
            RegisterOperators(10, er_id);
            MarkPunctuation(";", ",", ".", ":");
            Root = INICIO;
        }

        public override void CreateTokenFilters(LanguageData language, TokenFilterList filters)
        {
            var outlineFilter = new CodeOutlineFilter(language.GrammarData,
              OutlineOptions.ProduceIndents | OutlineOptions.CheckBraces, ToTerm(@"\")); // "\" is continuation symbol
            filters.Add(outlineFilter);
        }
    }
}
