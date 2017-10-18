using _Compi2_Proyecto2_201212859;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class expresion
    {
        public int ENTERO;
        public double DECIMAL;
        public String CADENA;
        public bool BOOL;
        public char CHAR;
        public expresion expIzq;
        public expresion expDer;
        public String tipo;
        public int fila;
        public int columna;

        public int getENTERO()
        {
            return ENTERO;
        }

        public void setENTERO(int ENTERO)
        {
            this.ENTERO = ENTERO;
        }

        public double getDECIMAL()
        {
            return DECIMAL;
        }

        public void setDECIMAL(double DECIMAL)
        {
            this.DECIMAL = DECIMAL;
        }

        public String getCADENA()
        {
            return CADENA;
        }

        public void setCADENA(String CADENA)
        {
            this.CADENA = CADENA;
        }


        public bool getBOOL()
        {
            return BOOL;
        }

        public void setBOOL(bool BOOL)
        {
            this.BOOL = BOOL;
        }

        public expresion getExpIzq()
        {
            return expIzq;
        }

        public void setExpIzq(expresion expIzq)
        {
            this.expIzq = expIzq;
        }

        public expresion getExpDer()
        {
            return expDer;
        }

        public void setExpDer(expresion expDer)
        {
            this.expDer = expDer;
        }

        public String getTipo()
        {
            return tipo;
        }

        public void setTipo(String tipo)
        {
            this.tipo = tipo;
        }

        public int getFila()
        {
            return fila;
        }

        public void setFila(int fila)
        {
            this.fila = fila;
        }

        public int getColumna()
        {
            return columna;
        }

        public void setColumna(int columna)
        {
            this.columna = columna;
        }

        public expresion(expresion nodo)
        {
            this.ENTERO = nodo.ENTERO;
            this.DECIMAL = nodo.DECIMAL;
            this.CADENA = nodo.CADENA;
            this.BOOL = nodo.BOOL;
            this.expIzq = nodo.expIzq;
            this.expDer = nodo.expDer;
            this.tipo = nodo.tipo;
            this.fila = nodo.fila;
            this.columna = nodo.columna;
        }

        public expresion(expresion expIzq, expresion expDer, String tipo, String nombre, int fila, int columna, Object valor)
        {
            this.expIzq = expIzq;
            this.expDer = expDer;
            this.tipo = tipo;
            this.fila = fila;
            this.columna = columna;
            if (tipo.Equals("ENTERO"))
            {
                this.ENTERO = Int32.Parse(valor.ToString());
                this.CADENA = valor.ToString();
            }
            else if (tipo.Equals("DOUBLE"))
            {
                this.DECIMAL = Double.Parse(valor.ToString());
                this.CADENA = valor.ToString();
            }
            else if (tipo.Equals("CADENA"))
            {
                this.CADENA = (String)valor;
            }
            else if (tipo.Equals("BOOL"))
            {
                this.CADENA = valor.ToString();
                if (this.CADENA.Equals("true"))
                {
                    this.BOOL = true;
                    this.ENTERO = 1;
                    this.DECIMAL = 1;
                }
                else
                {
                    this.BOOL = false;
                    this.ENTERO = 0;
                    this.DECIMAL = 0;
                }
            }
            else if (tipo.Equals("CHAR"))
            {
                    //Agregar Acciones
            }
        }

    private expresion resCondicion(expresion nodo)
{
    expresion temp = new expresion(null, null, "ERROR", "ERROR", nodo.fila, nodo.columna, null);
    expresion expIzq = nodo.expIzq;
    expresion expDer = nodo.expDer;

    if (nodo.expIzq != null)
    {
        expIzq = nodo.expIzq.resCondicion();
    }
    if (nodo.expDer != null)
    {
        expDer = nodo.expDer.resCondicion();
    }
    if (nodo.tipo.Equals("+"))
    {
        temp = resSuma(expIzq, expDer);
    }
    else if (nodo.tipo.Equals("-"))
    {
        if (nodo.expIzq != null)
        {
            temp = resResta(expIzq, expDer);
        }
        else
        {
            temp = resResta(expDer);
        }
    }
    else if (nodo.tipo.Equals("*"))
    {
        temp = resMultiplicacion(expIzq, expDer);
    }
    else if (nodo.tipo.Equals("/"))
    {
        temp = resDivision(expIzq, expDer);
    }
    else if (nodo.tipo.Equals("^"))
    {
        temp = resPotencia(expIzq, expDer);
    }
    else if (nodo.tipo.Equals("++"))
    {
        temp = resAumento(expIzq);
    }
    else if (nodo.tipo.Equals("--"))
    {
        temp = resDecremento(expIzq);
    }
    else if (nodo.tipo.Equals(">"))
    {
        temp = resMayor(expIzq, expDer);
    }
    else if (nodo.tipo.Equals("<"))
    {
        temp = resMenor(expIzq, expDer);
    }
    else if (nodo.tipo.Equals(">="))
    {
        temp = resMayorIgual(expIzq, expDer);
    }
    else if (nodo.tipo.Equals("<="))
    {
        temp = resMenorIgual(expIzq, expDer);
    }
    else if (nodo.tipo.Equals("=="))
    {
        temp = resIgual(expIzq, expDer);
    }
    else if (nodo.tipo.Equals("!="))
    {
        temp = resDiferente(expIzq, expDer);
    }
    else if (nodo.tipo.Equals("OR"))
    {
        temp = resOr(expIzq, expDer);
    }
    else if (nodo.tipo.Equals("AND"))
    {
        temp = resAnd(expIzq, expDer);
    }
    else if (nodo.tipo.Equals("!"))
    {
        temp = resNot(expDer);
    }
    else if (nodo.tipo.Equals("ENTERO"))
    {
        temp = new expresion(nodo);
    }
    else if (nodo.tipo.Equals("DOUBLE"))
    {
        temp = new expresion(nodo);
    }
    else if (nodo.tipo.Equals("CADENA"))
    {
        temp = new expresion(nodo);
    }
    else if (nodo.tipo.Equals("BOOL"))
    {
        temp = new expresion(nodo);
    }
    else if (nodo.tipo.Equals("CHAR"))
    {   
        //Agregar Acciones
    }
    return temp;
}

public expresion resCondicion()
{
    return resCondicion(this);
}

        public expresion resMayor(expresion expIzq, expresion expDer)
        {
            expresion temp = new expresion(null, null, "ERROR", "ERROR", fila, columna, null);
            if (expIzq.tipo.Equals("ENTERO"))
            {
                switch (expDer.tipo)
                {
                    case "ENTERO":
                        temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.ENTERO > expDer.ENTERO);
                        break;
                    case "DOUBLE":
                        temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.ENTERO > expDer.DECIMAL);
                        break;
                    case "BOOL":
                        temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.ENTERO > expDer.ENTERO);
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " > " + expDer.tipo, fila, columna);
                        break;
                }
            }
            else if (expIzq.tipo.Equals("DOUBLE"))
            {
                switch (expDer.tipo)
                {
                    case "ENTERO":
                        temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.DECIMAL > expDer.ENTERO);
                        break;
                    case "DOUBLE":
                        temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.DECIMAL > expDer.DECIMAL);
                        break;
                    case "BOOL":
                        temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.DECIMAL > expDer.ENTERO);
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " > " + expDer.tipo, fila, columna);
                        break;
                }
            }
            else if (expIzq.tipo.Equals("BOOL"))
            {
                switch (expDer.tipo)
                {
                    case "ENTERO":
                        temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.ENTERO > expDer.ENTERO);
                        break;
                    case "DOUBLE":
                        temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.ENTERO > expDer.DECIMAL);
                        break;
                    case "BOOL":
                        temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.ENTERO > expDer.ENTERO);
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " > " + expDer.tipo, fila, columna);
                        break;
                }
            }
    return temp;
}

public expresion resMenor(expresion expIzq, expresion expDer)
{
    expresion temp = new expresion(null, null, "ERROR", "ERROR", fila, columna, null);
    if (expIzq.tipo.Equals("ENTERO"))
    {
        switch (expDer.tipo)
        {
            case "ENTERO":
                temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.ENTERO < expDer.ENTERO);
                break;
            case "DOUBLE":
                temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.ENTERO < expDer.DECIMAL);
                break;
            case "BOOL":
                temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.ENTERO < expDer.ENTERO);
                break;
            default:
                memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " < " + expDer.tipo, fila, columna);
                break;
        }
    }
    else if (expIzq.tipo.Equals("DOUBLE"))
    {
        switch (expDer.tipo)
        {
            case "ENTERO":
                temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.DECIMAL < expDer.ENTERO);
                break;
            case "DOUBLE":
                temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.DECIMAL < expDer.DECIMAL);
                break;
            case "BOOL":
                temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.DECIMAL < expDer.ENTERO);
                break;
            default:
                memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " < " + expDer.tipo, fila, columna);
                break;
        }
    }
    else if (expIzq.tipo.Equals("BOOL"))
    {
        switch (expDer.tipo)
        {
            case "ENTERO":
                temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.ENTERO < expDer.ENTERO);
                break;
            case "DOUBLE":
                temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.ENTERO < expDer.DECIMAL);
                break;
            case "BOOL":
                temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.ENTERO < expDer.ENTERO);
                break;
            default:
                memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " < " + expDer.tipo, fila, columna);
                break;
        }
    }
    return temp;
}

public expresion resIgual(expresion expIzq, expresion expDer)
{
    expresion temp = new expresion(null, null, "ERROR", "ERROR", fila, columna, null);
    if (expIzq.tipo.Equals("ENTERO"))
    {
        switch (expDer.tipo)
        {
            case "ENTERO":
                temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.ENTERO == expDer.ENTERO);
                break;
            case "DOUBLE":
                temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.ENTERO == expDer.DECIMAL);
                break;
            case "BOOL":
                temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.ENTERO == expDer.ENTERO);
                break;
            default:
                memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " = " + expDer.tipo, fila, columna);
                break;
        }
    }
    else if (expIzq.tipo.Equals("DOUBLE"))
    {
        switch (expDer.tipo)
        {
            case "ENTERO":
                temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.DECIMAL == expDer.ENTERO);
                break;
            case "DOUBLE":
                temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.DECIMAL == expDer.DECIMAL);
                break;
            case "BOOL":
                temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.DECIMAL == expDer.ENTERO);
                break;
            default:
                memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " = " + expDer.tipo, fila, columna);
                break;
        }
    }
    else if (expIzq.tipo.Equals("CADENA"))
    {
        switch (expDer.tipo)
        {
            case "CADENA":
                temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.CADENA.Equals(expDer.CADENA));
                break;
            default:
                memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " = " + expDer.tipo, fila, columna);
                break;
        }
    }
    else if (expIzq.tipo.Equals("BOOL"))
    {
        switch (expDer.tipo)
        {
            case "ENTERO":
                temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.ENTERO == expDer.ENTERO);
                break;
            case "DOUBLE":
                temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.ENTERO == expDer.DECIMAL);
                break;
            case "BOOL":
                temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.BOOL == expDer.BOOL);
                break;
            default:
                memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " = " + expDer.tipo, fila, columna);
                break;
        }
    }
    return temp;
}

public expresion resDiferente(expresion expIzq, expresion expDer)
{
    expresion temp = new expresion(null, null, "ERROR", "ERROR", fila, columna, null);
    if (expIzq.tipo.Equals("ENTERO"))
    {
        switch (expDer.tipo)
        {
            case "ENTERO":
                temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.ENTERO != expDer.ENTERO);
                break;
            case "DOUBLE":
                temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.ENTERO != expDer.DECIMAL);
                break;
            case "BOOL":
                temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.ENTERO != expDer.ENTERO);
                break;
            default:
                memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " != " + expDer.tipo, fila, columna);
                break;
        }
    }
    else if (expIzq.tipo.Equals("DOUBLE"))
    {
        switch (expDer.tipo)
        {
            case "ENTERO":
                temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.DECIMAL != expDer.ENTERO);
                break;
            case "DOUBLE":
                temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.DECIMAL != expDer.DECIMAL);
                break;
            case "BOOL":
                temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.DECIMAL != expDer.ENTERO);
                break;
            default:
                memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " != " + expDer.tipo, fila, columna);
                break;
        }
    }
    else if (expIzq.tipo.Equals("CADENA"))
    {
        switch (expDer.tipo)
        {
            case "CADENA":
                temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, !expIzq.CADENA.Equals(expDer.CADENA));
                break;
            default:
                memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " != " + expDer.tipo, fila, columna);
                break;
        }
    }
    else if (expIzq.tipo.Equals("BOOL"))
    {
        switch (expDer.tipo)
        {
            case "ENTERO":
                temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.ENTERO != expDer.ENTERO);
                break;
            case "DOUBLE":
                temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.ENTERO != expDer.DECIMAL);
                break;
            case "BOOL":
                temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.BOOL != expDer.BOOL);
                break;
            default:
                memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " != " + expDer.tipo, fila, columna);
                break;
        }
    }
    return temp;
}

public expresion resMayorIgual(expresion expIzq, expresion expDer)
{
    expresion temp = new expresion(null, null, "ERROR", "ERROR", fila, columna, null);
    expresion mayor = resMayor(expIzq, expDer);
    expresion igual = resIgual(expIzq, expDer);
    if (mayor.tipo.Equals("BOOL"))
    {
        switch (igual.tipo)
        {
            case "BOOL":
                temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, mayor.BOOL || igual.BOOL);
                break;
            default:
                memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " >= " + expDer.tipo, fila, columna);
                break;
        }
    }
    return temp;
}

public expresion resMenorIgual(expresion expIzq, expresion expDer)
{
    expresion temp = new expresion(null, null, "ERROR", "ERROR", fila, columna, null);
    expresion menor = resMenor(expIzq, expDer);
    expresion igual = resIgual(expIzq, expDer);
    if (menor.tipo.Equals("BOOL"))
    {
        switch (igual.tipo)
        {
            case "BOOL":
                temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, menor.BOOL || igual.BOOL);
                break;
            default:
                memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " <= " + expDer.tipo, fila, columna);
                break;
        }
    }
    return temp;
}

public expresion resOr(expresion expIzq, expresion expDer)
{
    expresion temp = new expresion(null, null, "ERROR", "ERROR", fila, columna, null);
    if (expIzq.tipo.Equals("BOOL"))
    {
        switch (expDer.tipo)
        {
            case "BOOL":
                temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.BOOL || expDer.BOOL);
                break;
            default:
                memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " || " + expDer.tipo, fila, columna);
                break;
        }
    }
    return temp;
}

public expresion resAnd(expresion expIzq, expresion expDer)
{
    expresion temp = new expresion(null, null, "ERROR", "ERROR", fila, columna, null);
    if (expIzq.tipo.Equals("BOOL"))
    {
        switch (expDer.tipo)
        {
            case "BOOL":
                temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, expIzq.BOOL && expDer.BOOL);
                break;
            default:
                memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " && " + expDer.tipo, fila, columna);
                break;
        }
    }
    return temp;
}

public expresion resNot(expresion expDer)
{
    expresion temp = new expresion(null, null, "ERROR", "ERROR", fila, columna, null);
    if (expDer.tipo.Equals("BOOL"))
    {
        temp = new expresion(null, null, "BOOL", "BOOL", fila, columna, !expDer.BOOL);
    }
    else
    {
        memoria.addError("ERROR SEMANTICO ", "!" + expDer.tipo, fila, columna);
    }
    return temp;
}

public expresion resSuma(expresion expIzq, expresion expDer)
{
    expresion temp = new expresion(null, null, "ERROR", "ERROR", fila, columna, null);
    if (expIzq.tipo.Equals("ENTERO"))
    {
        switch (expDer.tipo)
        {
            case "ENTERO":
                temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.ENTERO + expDer.ENTERO);
                break;
            case "DOUBLE":
                temp = new expresion(null, null, "DOUBLE", "DOUBLE", fila, columna, expIzq.ENTERO + expDer.DECIMAL);
                break;
            case "CADENA":
                temp = new expresion(null, null, "CADENA", "CADENA", fila, columna, expIzq.ENTERO.ToString() + expDer.CADENA);
                break;
            case "BOOL":
                temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.ENTERO + expDer.ENTERO);
                break;
            default:
                memoria.addError("ERROR SEMANTICO", expIzq.tipo + " + " + expDer.tipo, fila, columna);
                break;
        }
    }
    else if (expIzq.tipo.Equals("DOUBLE"))
    {
        switch (expDer.tipo)
        {
            case "ENTERO":
                temp = new expresion(null, null, "DOUBLE", "DOUBLE", fila, columna, expIzq.DECIMAL + expDer.ENTERO);
                break;
            case "DOUBLE":
                temp = new expresion(null, null, "DOUBLE", "DOUBLE", fila, columna, expIzq.DECIMAL + expDer.DECIMAL);
                break;
            case "CADENA":
                temp = new expresion(null, null, "CADENA", "CADENA", fila, columna, expIzq.DECIMAL.ToString() + expDer.CADENA);
                break;
            case "BOOL":
                temp = new expresion(null, null, "DOUBLE", "DOUBLE", fila, columna, expIzq.DECIMAL + expDer.ENTERO);
                break;
            default:
                memoria.addError("ERROR SEMANTICO", expIzq.tipo + " + " + expDer.tipo, fila, columna);
                break;
        }
    }
    else if (expIzq.tipo.Equals("CADENA"))
    {
        switch (expDer.tipo)
        {
            case "ENTERO":
                temp = new expresion(null, null, "CADENA", "CADENA", fila, columna, expIzq.CADENA + expDer.ENTERO.ToString());
                break;
            case "DOUBLE":
                temp = new expresion(null, null, "CADENA", "CADENA", fila, columna, expIzq.CADENA + expDer.DECIMAL.ToString());
                break;
            case "CADENA":
                temp = new expresion(null, null, "CADENA", "CADENA", fila, columna, expIzq.CADENA + expDer.CADENA);
                break;
            default:
                memoria.addError("ERROR SEMANTICO", expIzq.tipo + " + " + expDer.tipo, fila, columna);
                break;
        }
    }
    else if (expIzq.tipo.Equals("BOOL"))
    {
        switch (expDer.tipo)
        {
            case "ENTERO":
                temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.ENTERO + expDer.ENTERO);
                break;
            case "DOUBLE":
                temp = new expresion(null, null, "DOUBLE", "DOUBLE", fila, columna, expIzq.ENTERO + expDer.DECIMAL);
                break;
            case "BOOL":
                temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.BOOL || expDer.BOOL);
                break;
            case "CADENA":
                temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.CADENA + expDer.CADENA);
                break;
            default:
                memoria.addError("ERROR SEMANTICO", expIzq.tipo + " + " + expDer.tipo, fila, columna);
                break;
        }
    }
    else if (expIzq.tipo.Equals("DATE"))
    {
        switch (expDer.tipo)
        {
            case "CADENA":
                temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.CADENA + expDer.CADENA);
                break;
            default:
                memoria.addError("ERROR SEMANTICO", expIzq.tipo + " + " + expDer.tipo, fila, columna);
                break;
        }
    }
    else if (expIzq.tipo.Equals("DATETIME"))
    {
        switch (expDer.tipo)
        {
            case "CADENA":
                temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.CADENA + expDer.CADENA);
                break;
            default:
                memoria.addError("ERROR SEMANTICO", expIzq.tipo + " + " + expDer.tipo, fila, columna);
                break;
        }
    }
    return temp;
}

public expresion resResta(expresion expDer)
{
    expresion temp = new expresion(null, null, "ERROR", "ERROR", fila, columna, null);
    if (expDer.tipo.Equals("ENTERO"))
    {
        temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, -expDer.ENTERO);
    }
    else if (expDer.tipo.Equals("DOUBLE"))
    {
        temp = new expresion(null, null, "DOUBLE", "DOUBLE", fila, columna, -expDer.DECIMAL);
    }
    else if (expDer.tipo.Equals("BOOL"))
    {
        temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, -expDer.ENTERO);
    }
    else
    {
        memoria.addError("ERROR SEMANTICO", expDer.tipo + " - " + expDer.tipo, fila, columna);
    }
    return temp;
}

public expresion resResta(expresion expIzq, expresion expDer)
{
    expresion temp = new expresion(null, null, "ERROR", "ERROR", fila, columna, null);
    if (expIzq.tipo.Equals("ENTERO"))
    {
        switch (expDer.tipo)
        {
            case "ENTERO":
                temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.ENTERO - expDer.ENTERO);
                break;
            case "DOUBLE":
                temp = new expresion(null, null, "DOUBLE", "DOUBLE", fila, columna, expIzq.ENTERO - expDer.DECIMAL);
                break;
            case "BOOL":
                temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.ENTERO - expDer.ENTERO);
                break;
            default:
                memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " - " + expDer.tipo, fila, columna);
                break;
        }
    }
    else if (expIzq.tipo.Equals("DOUBLE"))
    {
        switch (expDer.tipo)
        {
            case "ENTERO":
                temp = new expresion(null, null, "DOUBLE", "DOUBLE", fila, columna, expIzq.DECIMAL - expDer.ENTERO);
                break;
            case "DOUBLE":
                temp = new expresion(null, null, "DOUBLE", "DOUBLE", fila, columna, expIzq.DECIMAL - expDer.DECIMAL);
                break;
            case "BOOL":
                temp = new expresion(null, null, "DOUBLE", "DOUBLE", fila, columna, expIzq.DECIMAL - expDer.ENTERO);
                break;
            default:
                memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " - " + expDer.tipo, fila, columna);
                break;
        }
    }
    else if (expIzq.tipo.Equals("BOOL"))
    {
        switch (expDer.tipo)
        {
            case "ENTERO":
                temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.ENTERO - expDer.ENTERO);
                break;
            case "DOUBLE":
                temp = new expresion(null, null, "DOUBLE", "DOUBLE", fila, columna, expIzq.ENTERO - expDer.DECIMAL);
                break;
            default:
                memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " - " + expDer.tipo, fila, columna);
                break;
        }
    }
    return temp;
}

public expresion resMultiplicacion(expresion expIzq, expresion expDer)
{
    expresion temp = new expresion(null, null, "ERROR", "ERROR", fila, columna, null);
    if (expIzq.tipo.Equals("ENTERO"))
    {
        switch (expDer.tipo)
        {
            case "ENTERO":
                temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.ENTERO * expDer.ENTERO);
                break;
            case "DOUBLE":
                temp = new expresion(null, null, "DOUBLE", "DOUBLE", fila, columna, expIzq.ENTERO * expDer.DECIMAL);
                break;
            case "BOOL":
                temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.ENTERO * expDer.ENTERO);
                break;
            default:
                memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " * " + expDer.tipo, fila, columna);
                break;
        }
    }
    else if (expIzq.tipo.Equals("DOUBLE"))
    {
        switch (expDer.tipo)
        {
            case "ENTERO":
                temp = new expresion(null, null, "DOUBLE", "DOUBLE", fila, columna, expIzq.DECIMAL * expDer.ENTERO);
                break;
            case "DOUBLE":
                temp = new expresion(null, null, "DOUBLE", "DOUBLE", fila, columna, expIzq.DECIMAL * expDer.DECIMAL);
                break;
            case "BOOL":
                temp = new expresion(null, null, "DOUBLE", "DOUBLE", fila, columna, expIzq.DECIMAL * expDer.ENTERO);
                break;
            default:
                memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " * " + expDer.tipo, fila, columna);
                break;
        }
    }
    else if (expIzq.tipo.Equals("BOOL"))
    {
        switch (expDer.tipo)
        {
            case "ENTERO":
                temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.ENTERO * expDer.ENTERO);
                break;
            case "DOUBLE":
                temp = new expresion(null, null, "DOUBLE", "DOUBLE", fila, columna, expIzq.ENTERO * expDer.DECIMAL);
                break;
            case "BOOL":
                temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.BOOL && expDer.BOOL);
                break;
            default:
                memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " * " + expDer.tipo, fila, columna);
                break;
        }
    }
    return temp;
}

public expresion resDivision(expresion expIzq, expresion expDer)
{
    expresion temp = new expresion(null, null, "ERROR", "ERROR", fila, columna, null);
    if (expIzq.tipo.Equals("ENTERO"))
    {
        switch (expDer.tipo)
        {
            case "ENTERO":
                temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.ENTERO / expDer.ENTERO);
                break;
            case "DOUBLE":
                temp = new expresion(null, null, "DOUBLE", "DOUBLE", fila, columna, expIzq.ENTERO / expDer.DECIMAL);
                break;
            case "BOOL":
                temp = new expresion(null, null, "DOUBLE", "DOUBLE", fila, columna, expIzq.ENTERO / expDer.ENTERO);
                break;
            default:
                memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " / " + expDer.tipo, fila, columna);
                break;
        }
    }
    else if (expIzq.tipo.Equals("DOUBLE"))
    {
        switch (expDer.tipo)
        {
            case "ENTERO":
                temp = new expresion(null, null, "DOUBLE", "DOUBLE", fila, columna, expIzq.DECIMAL / expDer.ENTERO);
                break;
            case "DOUBLE":
                temp = new expresion(null, null, "DOUBLE", "DOUBLE", fila, columna, expIzq.DECIMAL / expDer.DECIMAL);
                break;
            case "BOOL":
                temp = new expresion(null, null, "DOUBLE", "DOUBLE", fila, columna, expIzq.DECIMAL / expDer.ENTERO);
                break;
            default:
                memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " / " + expDer.tipo, fila, columna);
                break;
        }
    }
    else if (expIzq.tipo.Equals("BOOL"))
    {
        switch (expDer.tipo)
        {
            case "ENTERO":
                temp = new expresion(null, null, "DOUBLE", "DOUBLE", fila, columna, expIzq.ENTERO / expDer.ENTERO);
                break;
            case "DOUBLE":
                temp = new expresion(null, null, "DOUBLE", "DOUBLE", fila, columna, expIzq.ENTERO / expDer.DECIMAL);
                break;
            default:
                memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " / " + expDer.tipo, fila, columna);
                break;
        }
    }
    return temp;
}

public expresion resPotencia(expresion expIzq, expresion expDer)
{
    expresion temp = new expresion(null, null, "ERROR", "ERROR", fila, columna, null);
    if (expIzq.tipo.Equals("ENTERO"))
    {
        switch (expDer.tipo)
        {
            case "ENTERO":
                double p = Math.Pow(expIzq.ENTERO, expDer.ENTERO);
                int v = (int)p;
                temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, v);
                break;
            case "DOUBLE":
                temp = new expresion(null, null, "DOUBLE", "DOUBLE", fila, columna, Math.Pow(expIzq.ENTERO, expDer.DECIMAL));
                break;
            case "BOOL":
                double pb = Math.Pow(expIzq.ENTERO, expDer.ENTERO);
                int vb = (int)pb;
                temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, vb);
                break;
            default:
                memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " ^ " + expDer.tipo, fila, columna);
                break;
        }
    }
    else if (expIzq.tipo.Equals("DOUBLE"))
    {
        switch (expDer.tipo)
        {
            case "ENTERO":
                temp = new expresion(null, null, "DOUBLE", "DOUBLE", fila, columna, Math.Pow(expIzq.DECIMAL, expDer.ENTERO));
                break;
            case "DOUBLE":
                temp = new expresion(null, null, "DOUBLE", "DOUBLE", fila, columna, Math.Pow(expIzq.DECIMAL, expDer.DECIMAL));
                break;
            case "BOOL":
                temp = new expresion(null, null, "DOUBLE", "DOUBLE", fila, columna, Math.Pow(expIzq.DECIMAL, expDer.ENTERO));
                break;
            default:
                memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " ^ " + expDer.tipo, fila, columna);
                break;
        }
    }
    else if (expIzq.tipo.Equals("BOOL"))
    {
        switch (expDer.tipo)
        {
            case "ENTERO":
                double p = Math.Pow(expIzq.ENTERO, expDer.ENTERO);
                int v = (int)p;
                temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, v);
                break;
            case "DOUBLE":
                temp = new expresion(null, null, "DOUBLE", "DOUBLE", fila, columna, Math.Pow(expIzq.ENTERO, expDer.DECIMAL));
                break;
            default:
                memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " ^ " + expDer.tipo, fila, columna);
                break;
        }
    }
    return temp;
}

public expresion resAumento(expresion expIzq)
{
    expresion temp = new expresion(null, null, "ERROR", "ERROR", fila, columna, null);
    if (expIzq.tipo.Equals("ENTERO"))
    {
        temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.ENTERO + 1);
    }
    else if (expIzq.tipo.Equals("DOUBLE"))
    {
        temp = new expresion(null, null, "DOUBLE", "DOUBLE", fila, columna, expIzq.DECIMAL + 1);
    }
    else
    {
        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " ++ ", fila, columna);
    }
    return temp;
}

public expresion resDecremento(expresion expIzq)
{
    expresion temp = new expresion(null, null, "ERROR", "ERROR", fila, columna, null);
    if (expIzq.tipo.Equals("ENTERO"))
    {
        temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.ENTERO - 1);
    }
    else if (expIzq.tipo.Equals("DOUBLE"))
    {
        temp = new expresion(null, null, "DOUBLE", "DOUBLE", fila, columna, expIzq.DECIMAL - 1);
    }
    else
    {
        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " -- ", fila, columna);
    }
    return temp;
}
    }
}
