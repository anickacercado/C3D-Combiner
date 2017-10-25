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
            var t_funcion = ToTerm("funcion");
            var t_metodo = ToTerm("metodo");
            var t_out_string = ToTerm("out_string");



            //Expresiones Regulares
            RegexBasedTerminal er_visibilidad = new RegexBasedTerminal("er_visibilidad", "publico|protegido|privado");
            RegexBasedTerminal er_tipo = new RegexBasedTerminal("er_tipo", "entero|cadena|decimal|booleano|caracter");
            RegexBasedTerminal er_casteo = new RegexBasedTerminal("er_casteo", "ParseInt|ParseDouble|intToStr|doubleToStr|doubleToInt");


            var er_entero = new NumberLiteral("er_entero", NumberOptions.IntOnly);
            var er_decimal = new NumberLiteral("er_decimal");
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
            MarkReservedWords("imprimir");
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
            MarkReservedWords("funcion");
            MarkReservedWords("metodo");
            MarkReservedWords("out_string");

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
            var LLAMADA_METODO = new NonTerminal("LLAMADA_METODO");
            var DIMENSION = new NonTerminal("DIMENSION");
            var LISTA_DIMENSION = new NonTerminal("LISTA_DIMENSION");
            var ARREGLO = new NonTerminal("ARREGLO");
            var LISTA_ARREGLO = new NonTerminal("LISTA_ARREGLO");
            var OPCION_IMPORTAR = new NonTerminal("OPCION_IMPORTAR");
            var SALIR = new NonTerminal("SALIR");
            var CONTINUAR = new NonTerminal("CONTINUAR");
            var ELEGIR = new NonTerminal("ELEGIR");
            var CASO = new NonTerminal("CASO");
            var LISTA_CASO = new NonTerminal("LISTA_CASO");
            var VALOR = new NonTerminal("VALOR");
            var LOOP = new NonTerminal("LOOP");
            var PARA = new NonTerminal("PARA");
            var LISTA_CLASE = new NonTerminal("LISTA_CLASE");
            var CLASE = new NonTerminal("CLASE");
            var VISIBILIDAD = new NonTerminal("VISIBILIDAD");
            var LISTA_PARAMETRO_E = new NonTerminal("LISTA_PARAMETRO_E");
            var LISTA_DIMENSION_METODO_E = new NonTerminal("LISTA_DIMENSION_METODO_E");
            var LISTA_DIMENSION_METODO = new NonTerminal("LISTA_DIMENSION_METODO");
            var DIMENSION_METODO = new NonTerminal("DIMENSION_METODO");
            var HIJO = new NonTerminal("HIJO");
            var LISTA_E_E = new NonTerminal("LISTA_E_E");
            var LLAMADA_OBJETO = new NonTerminal("LLAMADA_OBJETO");
            var DEFECTO = new NonTerminal("DEFECTO");
            var SOBRESCRIBIR = new NonTerminal("SOBRESCRIBIR");



            /*Producciones*/
            /*Clases e importacion*/

            INICIO.Rule = IMPORTAR + LISTA_CLASE;

            CLASE.Rule = t_clase + er_id + t_cor_abre + t_cor_cierra + t_dos_pts + Eos + Indent + LISTA_SENTENCIAS_GLOBALES + Dedent
                | t_clase + er_id + t_cor_abre + er_id + t_cor_cierra + t_dos_pts + Eos + Indent + LISTA_SENTENCIAS_GLOBALES + Dedent
                ;


            IMPORTAR.Rule = t_importar + LISTA_IMPORTAR + Eos
                | Empty
                ;

            OPCION_IMPORTAR.Rule = er_id + ".olc"
                            | er_id + ".tree"
                            | er_cadena
                            ;

     
            /*Sentencias Globales*/

            SENTENCIAS_GLOBALES.Rule =
                 METODO 
                | VISIBILIDAD + DECLARAR + Eos
                ;

          DECLARAR.Rule = TIPO + LISTA_ID + "=>" + E
                        | TIPO + LISTA_ID
                        | TIPO + er_id + LISTA_DIMENSION;

            METODO.Rule =
                        t_sobrescribir + Eos + VISIBILIDAD + t_metodo + er_id + t_cor_abre + LISTA_PARAMETRO_E + t_cor_cierra + t_dos_pts + Eos + Indent + LISTA_SENTENCIAS_LOCALES + Dedent
                      | VISIBILIDAD + t_metodo + er_id + t_cor_abre + LISTA_PARAMETRO_E + t_cor_cierra + t_dos_pts + Eos + Indent + LISTA_SENTENCIAS_LOCALES + Dedent
                      
                      | t_sobrescribir + Eos + VISIBILIDAD + t_funcion + TIPO + LISTA_DIMENSION_METODO_E + er_id + t_cor_abre + LISTA_PARAMETRO_E + t_cor_cierra + t_dos_pts + Eos + Indent + LISTA_SENTENCIAS_LOCALES + Dedent
                      | VISIBILIDAD + t_funcion + TIPO + LISTA_DIMENSION_METODO_E + er_id + t_cor_abre + LISTA_PARAMETRO_E + t_cor_cierra + t_dos_pts + Eos + Indent + LISTA_SENTENCIAS_LOCALES + Dedent
                      
                      | t_constructor + t_cor_abre + LISTA_PARAMETRO_E + t_cor_cierra + t_dos_pts + Eos + Indent + LISTA_SENTENCIAS_LOCALES + Dedent
                      ;

            SOBRESCRIBIR.Rule = t_sobrescribir + Eos;

            LISTA_PARAMETRO_E.Rule = LISTA_PARAMETRO
                | Empty;

            LISTA_DIMENSION_METODO_E.Rule = LISTA_DIMENSION_METODO
                                    | Empty
                                    ;

            LISTA_DIMENSION_METODO.Rule = MakePlusRule(LISTA_DIMENSION_METODO, DIMENSION_METODO);

            DIMENSION_METODO.Rule = t_cor_abre + t_cor_cierra;


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
                                    | LLAMADA_METODO + Eos /*metodos*/
                                    | AUMENTO_DECREMENTO + Eos;
                                    ;

            ASIGNAR.Rule = OBJETO + er_id + LISTA_DIMENSION + "=>" + E
                        | er_id + LISTA_DIMENSION + "=>" + E
                        | OBJETO + er_id + "=>" + E
                        | er_id + "=>" + E
                        ;

            LLAMADA_METODO.Rule = OBJETO + er_id + t_cor_abre + LISTA_E_E + t_cor_cierra
                            | er_id +  t_cor_abre + LISTA_E_E + t_cor_cierra
                            | OBJETO + er_id + LISTA_DIMENSION
                            | er_id + LISTA_DIMENSION
                            | t_out_string + t_cor_abre + E + t_cor_cierra                        /*Imprimir cadena*/
                            | t_super + t_cor_abre + LISTA_E_E + t_cor_cierra                     /*Llamada a constructor*/                  
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

            ELEGIR.Rule = t_elegir + t_caso + E + t_dos_pts + Eos + Indent + LISTA_CASO + DEFECTO + Dedent
                ;

            CASO.Rule = VALOR + t_dos_pts + Eos + Indent + LISTA_SENTENCIAS_LOCALES + Dedent
                ;

            DEFECTO.Rule = t_defecto + t_dos_pts + Eos + Indent + LISTA_SENTENCIAS_LOCALES + Dedent
                | Empty;

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

            DIMENSION.Rule = t_cor_abre + E + t_cor_cierra;


            /*Parametros y tipos*/
            PARAMETRO.Rule = TIPO +  LISTA_DIMENSION_METODO_E + er_id 
                ;

            TIPO.Rule = er_tipo
                  | er_id
                  ;

            VISIBILIDAD.Rule = er_visibilidad
                               | Empty;

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

            LISTA_CLASE.Rule = MakePlusRule(LISTA_CLASE, CLASE)
                ;

            LISTA_E_E.Rule = LISTA_E
                | Empty
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
                | er_decimal
                | er_entero             
                | er_caracter
                | er_cadena
                | er_booleano
                | LLAMADA_OBJETO
                | t_nuevo + er_id + t_cor_abre + LISTA_E_E + t_cor_cierra
                | er_casteo + t_cor_abre + E + t_cor_cierra    /*casteo*/
                ;


            LLAMADA_OBJETO.Rule = OBJETO + er_id
                | OBJETO + er_id + t_cor_abre + LISTA_E_E + t_cor_cierra
                | OBJETO + er_id + LISTA_DIMENSION
                | er_id
                | er_id + t_cor_abre + LISTA_E_E + t_cor_cierra
                | er_id + LISTA_DIMENSION
                ;

            OBJETO.Rule = MakePlusRule(OBJETO, HIJO)
                ;

            HIJO.Rule = er_id + t_punto
                | er_id + t_cor_abre + LISTA_E_E + t_cor_cierra + t_punto
                | er_id + LISTA_DIMENSION + t_punto
                | t_self + t_punto
                | t_super + t_punto
                ;

            VALOR.Rule = er_decimal
                | er_entero
                | er_caracter
                | er_cadena
                | er_booleano
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
            MarkPunctuation(";", ",", ".", ":", "(", ")", "[", "]", "{", "}", "=>");
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
