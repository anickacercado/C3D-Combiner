using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.C3D
{
    [Language("C3D", "1.0", "C3D Grammar")]
    class gramaticaC3D:Grammar
    {

        private readonly TerminalSet mSkipTokensInPreview = new TerminalSet(); //used in token preview for conflict resolution
        public gramaticaC3D() : base(caseSensitive: false)
        {

            CommentTerminal SingleLineComment = new CommentTerminal("SingleLineComment", "//", "\r", "\n", "\u2085", "\u2028", "\u2029");
            CommentTerminal DelimitedComment = new CommentTerminal("DelimitedComment", "/*", "*/");
            NonGrammarTerminals.Add(SingleLineComment);
            NonGrammarTerminals.Add(DelimitedComment);


            RegexBasedTerminal er_temp = new RegexBasedTerminal("er_temp", "[t][0-9]+");
            RegexBasedTerminal er_etiq = new RegexBasedTerminal("er_etiq", "[l][0-9]+");
            RegexBasedTerminal er_id = new RegexBasedTerminal("er_id", "[a-zA-Z][a-zA-Z0-9_-]*");
            var er_entero = new NumberLiteral("er_entero", NumberOptions.IntOnly);
            var er_decimal = new NumberLiteral("er_decimal");
            RegexBasedTerminal er_print = new RegexBasedTerminal("print_num", "\"%d\"|\"%c\"|\"%f\"");

            var t_void = ToTerm("void");
            var t_goto = ToTerm("goto");
            var t_stack = ToTerm("stack");
            var t_heap = ToTerm("heap");
            var t_h = ToTerm("h");
            var t_p = ToTerm("p");
            var t_if = ToTerm("if");
            var t_main = ToTerm("main");
            var t_print = ToTerm("print");
            var t_ifFalse = ToTerm("ifFalse");

            MarkReservedWords("goto");
            MarkReservedWords("stack");
            MarkReservedWords("heap");
            MarkReservedWords("h");
            MarkReservedWords("p");
            MarkReservedWords("if");
            MarkReservedWords("main");
            MarkReservedWords("print");
            MarkReservedWords("ifFalse");

            //Operadores Aritmeticos
            var t_mas = ToTerm("+");
            var t_menos = ToTerm("-");
            var t_por = ToTerm("*");
            var t_div = ToTerm("/");
            var t_potencia = ToTerm("^");

            //Operadores Relacionales
            var t_igualacion = ToTerm("==");
            var t_diferente = ToTerm("!=");
            var t_menor = ToTerm("<");
            var t_mayor = ToTerm(">");
            var t_menor_igual = ToTerm("<=");
            var t_mayor_igual = ToTerm(">=");

            //Puntuacion
            var t_par_abre = ToTerm("(");
            var t_par_cierra = ToTerm(")");
            var t_llave_abre = ToTerm("{");
            var t_llave_cierra = ToTerm("}");
            var t_cor_abre = ToTerm("[");
            var t_cor_cierra = ToTerm("]");
            var t_dos_puntos = ToTerm(":");
            var t_punto_coma = ToTerm(";");
            var t_igual = ToTerm("=");
            var t_coma = ToTerm(",");


            var INICIO = new NonTerminal("INICIO");
            var R = new NonTerminal("R");
            var A = new NonTerminal("A");
            var METODO = new NonTerminal("METODO");
            var LISTA_METODO = new NonTerminal("LISTA_METODO");
            var SENTENCIAS = new NonTerminal("SENTENCIAS");
            var LISTA_SENTENCIAS = new NonTerminal("LISTA_SENTENCIAS");
            var ASIGNACION = new NonTerminal("ASIGNACION");
            var OPCION = new NonTerminal("OPCION");
            var LLAMADA_METODO = new NonTerminal("LLAMADA_METODO");
            var CONDICION = new NonTerminal("CONDICION");
            var SALTO = new NonTerminal("SALTO");
            var ETIQUETA = new NonTerminal("ETIQUETA");
            var PRINT = new NonTerminal("PRINT");

            INICIO.Rule = LISTA_METODO;

            METODO.Rule = t_void + er_id + t_par_abre + t_par_cierra + t_llave_abre + LISTA_SENTENCIAS + t_llave_cierra
                | t_main + t_par_abre + t_par_cierra + t_llave_abre + LISTA_SENTENCIAS + t_llave_cierra
                ;

            ASIGNACION.Rule = t_stack + t_cor_abre + OPCION + t_cor_cierra + t_igual + A + t_punto_coma
                         | t_heap + t_cor_abre + OPCION + t_cor_cierra + t_igual + A + t_punto_coma
                         | er_temp + t_igual + A + t_punto_coma
                         | er_temp + t_igual + t_stack + t_cor_abre + OPCION + t_cor_cierra + t_punto_coma
                         | er_temp + t_igual + t_heap + t_cor_abre + OPCION + t_cor_cierra + t_punto_coma
                         | t_p + t_igual + A + t_punto_coma
                         | t_h + t_igual + A + t_punto_coma
                         ;

            A.Rule = A + t_mas + A
               | A + t_menos + A
               | A + t_por + A
               | A + t_div + A
               | A + t_potencia + A
               | er_decimal
               | er_entero
               | er_temp
               | t_h
               | t_p
               ;

            R.Rule = R + t_igualacion + R
               | R + t_diferente + R
               | R + t_menor + R
               | R + t_mayor + R
               | R + t_menor_igual + R
               | R + t_mayor_igual + R
               | er_decimal
               | er_entero
               | er_temp
               ;

            OPCION.Rule = t_h
                | t_p
                | er_temp
                ;

            LLAMADA_METODO.Rule = er_id + t_par_abre + t_par_abre + t_punto_coma
                ;

            CONDICION.Rule = t_if + R + t_goto + er_etiq + t_punto_coma
                | t_ifFalse + R + t_goto + er_etiq + t_punto_coma
                ;


            SALTO.Rule = t_goto + er_etiq + t_punto_coma
                ;

            ETIQUETA.Rule = er_etiq + t_dos_puntos;

            PRINT.Rule = t_print + t_par_abre + er_print + t_coma + er_temp + t_par_cierra + t_punto_coma;

            SENTENCIAS.Rule = ETIQUETA
                | LLAMADA_METODO
                | SALTO
                | CONDICION
                | ASIGNACION
                | PRINT
                ;

            LISTA_SENTENCIAS.Rule = MakePlusRule(LISTA_SENTENCIAS, SENTENCIAS)
             ;

            LISTA_METODO.Rule = MakePlusRule(LISTA_METODO, METODO)
             ;

            RegisterOperators(1, Associativity.Left, "==", "!=", "<", ">", "<=", ">=");
            RegisterOperators(2, Associativity.Left, "+", "-");
            RegisterOperators(3, Associativity.Left, "*", "/");
            RegisterOperators(4, Associativity.Left, "^");

            MarkPunctuation(":",";","=","(",")","{","}","[","]",",");
            Root = INICIO;
        }
    }
}
