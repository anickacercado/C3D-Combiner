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
        public char CARACTER;
        public llamadaObjeto llamadaObjeto;
        public nuevo nuevo;
        public expresion expIzq;
        public expresion expDer;
        public String tipo;
        public int fila;
        public int columna;
        public simbolo padre;
        public cadena3D c3DIzq;
        public cadena3D c3DDer;

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
            this.CARACTER = nodo.CARACTER;
            this.llamadaObjeto = nodo.llamadaObjeto;
            this.nuevo = nodo.nuevo;
            this.expIzq = nodo.expIzq;
            this.expDer = nodo.expDer;
            this.tipo = nodo.tipo;
            this.fila = nodo.fila;
            this.columna = nodo.columna;
            this.padre = nodo.padre;
            this.c3DIzq = nodo.c3DIzq;
            this.c3DDer = nodo.c3DDer;
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
                this.DECIMAL = Double.Parse(valor.ToString());
                this.CADENA = valor.ToString();
            }
            else if (tipo.Equals("DECIMAL"))
            {
                this.DECIMAL = Double.Parse(valor.ToString());
                this.CADENA = valor.ToString();
            }
            else if (tipo.Equals("CADENA"))
            {
                this.CADENA = (String)valor;
                this.CADENA = this.CADENA.Substring(1, this.CADENA.Length - 2);
            }
            else if (tipo.Equals("BOOLEANO"))
            {
                this.CADENA = valor.ToString();
                if (this.CADENA.ToUpper().Equals("TRUE"))
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
            else if (tipo.Equals("CARACTER"))
            {
                this.CADENA = valor.ToString();
                this.CADENA = this.CADENA.Substring(1, this.CADENA.Length - 2);
                this.CARACTER = this.CADENA[0];
                this.ENTERO = this.CARACTER;
            }
            else if (tipo.Equals("LLAMADA_OBJETO"))
            {
                this.llamadaObjeto = (llamadaObjeto)valor;

            }
            else if (tipo.Equals("NUEVO"))
            {
                this.nuevo = (nuevo)valor;
            }
        }

        private cadena3D resCondicion(expresion nodo)
        {
            
            expresion temp = new expresion(null, null, "ERROR", "ERROR", nodo.fila, nodo.columna, null);
            expresion expIzq = nodo.expIzq;
            expresion expDer = nodo.expDer;
            cadena3D c3D = new cadena3D();

            if (nodo.tipo.Equals("+"))
            {
                c3D = resSuma(expIzq, expDer);
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
            else if (nodo.tipo.Equals("^") || nodo.tipo.ToUpper().Equals("POW"))
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
            else if (nodo.tipo.ToUpper().Equals("OR") || nodo.tipo.Equals("||"))
            {
                temp = resOr(expIzq, expDer);
            }
            else if (nodo.tipo.ToUpper().Equals("XOR") || nodo.tipo.Equals("??"))
            {
                temp = resXor(expIzq, expDer);
            }
            else if (nodo.tipo.ToUpper().Equals("AND") || nodo.tipo.Equals("&&"))
            {
                temp = resAnd(expIzq, expDer);
            }
            else if (nodo.tipo.ToUpper().Equals("NOT") || nodo.tipo.Equals("!"))
            {
                temp = resNot(expDer);
            }
            else if (nodo.tipo.Equals("ENTERO"))
            {
                c3D.temporal = memoria.getTemp();
                c3D.codigo = memoria.temporal + "=" + nodo.CADENA + "; //ENTERO";
                c3D.tipo = nodo.tipo;
                temp = new expresion(nodo);
            }
            else if (nodo.tipo.Equals("DECIMAL"))
            {
                c3D.temporal = memoria.getTemp();
                c3D.codigo = memoria.temporal + "=" + nodo.CADENA + "; //DECIMAL";
                c3D.tipo = nodo.tipo;
                temp = new expresion(nodo);
            }
            else if (nodo.tipo.Equals("CADENA"))
            {
                c3D.temporal = memoria.getTemp();

                /*CODIGO CADENA*/
                Char finCadena = '\0';
                String codigo = "";
                codigo += "\t\n" + c3D.temporal + "= H; //INICIO DE CADENA";
                codigo += "\t\n" + "H = H + 1;";

                for (int i=0; i<nodo.CADENA.Length - 1; i++) {
                    codigo += "\t\n" + "Heap[H] = " + (int)nodo.CADENA[i] + ";";
                    codigo += "\t\n" + "H = H + 1;";
                }

                codigo += "\t\n" + "Heap[H] = " + (int)finCadena + "; //FIN DE CADENA";
                codigo += "\t\n" + "H = H + 1;";
                /*CODIGO CADENA*/

                temp = new expresion(nodo);

            }
            else if (nodo.tipo.Equals("BOOLEANO"))
            {
                c3D.temporal = memoria.getTemp();
                c3D.codigo = memoria.temporal + "=" + nodo.ENTERO.ToString() + "; //BOOLEANO";
                c3D.tipo = nodo.tipo;
                temp = new expresion(nodo);
            }
            else if (nodo.tipo.Equals("CARACTER"))
            {
                c3D.temporal = memoria.getTemp();
                c3D.codigo = memoria.temporal + "=" + nodo.ENTERO.ToString() + "; //CARACTER";
                c3D.tipo = nodo.tipo;
                temp = new expresion(nodo);
            }
            return c3D;
        }

        public cadena3D resCondicion()
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
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.ENTERO > expDer.ENTERO);
                        break;
                    case "DECIMAL":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.ENTERO > expDer.DECIMAL);
                        break;
                    case "BOOLEANO":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.ENTERO > expDer.ENTERO);
                        break;
                    case "CARACTER":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.ENTERO > expDer.CARACTER);
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " > " + expDer.tipo, fila, columna);
                        break;
                }
            }
            else if (expIzq.tipo.Equals("DECIMAL"))
            {
                switch (expDer.tipo)
                {
                    case "ENTERO":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.DECIMAL > expDer.ENTERO);
                        break;
                    case "DECIMAL":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.DECIMAL > expDer.DECIMAL);
                        break;
                    case "BOOLEANO":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.DECIMAL > expDer.ENTERO);
                        break;
                    case "CARACTER":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.DECIMAL > expDer.CARACTER);
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " > " + expDer.tipo, fila, columna);
                        break;
                }
            }
            else if (expIzq.tipo.Equals("BOOLEANO"))
            {
                switch (expDer.tipo)
                {
                    case "ENTERO":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.ENTERO > expDer.ENTERO);
                        break;
                    case "DECIMAL":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.ENTERO > expDer.DECIMAL);
                        break;
                    case "BOOLEANO":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.ENTERO > expDer.ENTERO);
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " > " + expDer.tipo, fila, columna);
                        break;
                }
            }
            else if (expIzq.tipo.Equals("CARACTER"))
            {
                switch (expDer.tipo)
                {
                    case "ENTERO":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.CARACTER > expDer.ENTERO);
                        break;
                    case "DECIMAL":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.CARACTER > expDer.DECIMAL);
                        break;
                    case "CARACTER":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.CARACTER > expDer.CARACTER);
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
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.ENTERO < expDer.ENTERO);
                        break;
                    case "DECIMAL":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.ENTERO < expDer.DECIMAL);
                        break;
                    case "BOOLEANO":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.ENTERO < expDer.ENTERO);
                        break;
                    case "CARACTER":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.ENTERO < expDer.CARACTER);
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " < " + expDer.tipo, fila, columna);
                        break;
                }
            }
            else if (expIzq.tipo.Equals("DECIMAL"))
            {
                switch (expDer.tipo)
                {
                    case "ENTERO":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.DECIMAL < expDer.ENTERO);
                        break;
                    case "DECIMAL":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.DECIMAL < expDer.DECIMAL);
                        break;
                    case "BOOLEANO":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.DECIMAL < expDer.ENTERO);
                        break;
                    case "CARACTER":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.DECIMAL < expDer.CARACTER);
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " < " + expDer.tipo, fila, columna);
                        break;
                }
            }
            else if (expIzq.tipo.Equals("BOOLEANO"))
            {
                switch (expDer.tipo)
                {
                    case "ENTERO":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.ENTERO < expDer.ENTERO);
                        break;
                    case "DECIMAL":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.ENTERO < expDer.DECIMAL);
                        break;
                    case "BOOLEANO":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.ENTERO < expDer.ENTERO);
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " < " + expDer.tipo, fila, columna);
                        break;
                }
            }
            else if (expIzq.tipo.Equals("CARACTER"))
            {
                switch (expDer.tipo)
                {
                    case "ENTERO":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.CARACTER < expDer.ENTERO);
                        break;
                    case "DECIMAL":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.CARACTER < expDer.DECIMAL);
                        break;
                    case "CARACTER":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.CARACTER < expDer.CARACTER);
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " > " + expDer.tipo, fila, columna);
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
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.ENTERO == expDer.ENTERO);
                        break;
                    case "DECIMAL":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.ENTERO == expDer.DECIMAL);
                        break;
                    case "BOOLEANO":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.ENTERO == expDer.ENTERO);
                        break;
                    case "CARACTER":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.ENTERO == expDer.CARACTER);
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " == " + expDer.tipo, fila, columna);
                        break;
                }
            }
            else if (expIzq.tipo.Equals("DECIMAL"))
            {
                switch (expDer.tipo)
                {
                    case "ENTERO":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.DECIMAL == expDer.ENTERO);
                        break;
                    case "DECIMAL":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.DECIMAL == expDer.DECIMAL);
                        break;
                    case "BOOLEANO":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.DECIMAL == expDer.ENTERO);
                        break;
                    case "CARACTER":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.DECIMAL == expDer.CARACTER);
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " == " + expDer.tipo, fila, columna);
                        break;
                }
            }
            else if (expIzq.tipo.Equals("CADENA"))
            {
                switch (expDer.tipo)
                {
                    case "CADENA":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.CADENA.Equals(expDer.CADENA));
                        break;
                    case "CARACTER":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.CADENA.Equals(expDer.CARACTER.ToString()));
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " == " + expDer.tipo, fila, columna);
                        break;
                }
            }
            else if (expIzq.tipo.Equals("BOOLEANO"))
            {
                switch (expDer.tipo)
                {
                    case "ENTERO":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.ENTERO == expDer.ENTERO);
                        break;
                    case "DECIMAL":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.ENTERO == expDer.DECIMAL);
                        break;
                    case "BOOLEANO":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.BOOL == expDer.BOOL);
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " == " + expDer.tipo, fila, columna);
                        break;
                }
            }
            else if (expIzq.tipo.Equals("CARACTER"))
            {
                switch (expDer.tipo)
                {
                    case "ENTERO":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.CARACTER == expDer.ENTERO);
                        break;
                    case "DECIMAL":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.CARACTER == expDer.DECIMAL);
                        break;
                    case "CADENA":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.CARACTER == expDer.ENTERO);
                        break;
                    case "CARACTER":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.CARACTER.ToString().Equals(expDer.CADENA));
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " == " + expDer.tipo, fila, columna);
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
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.ENTERO != expDer.ENTERO);
                        break;
                    case "DECIMAL":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.ENTERO != expDer.DECIMAL);
                        break;
                    case "BOOLEANO":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.ENTERO != expDer.ENTERO);
                        break;
                    case "CARACTER":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.ENTERO != expDer.CARACTER);
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " != " + expDer.tipo, fila, columna);
                        break;
                }
            }
            else if (expIzq.tipo.Equals("DECIMAL"))
            {
                switch (expDer.tipo)
                {
                    case "ENTERO":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.DECIMAL != expDer.ENTERO);
                        break;
                    case "DECIMAL":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.DECIMAL != expDer.DECIMAL);
                        break;
                    case "BOOLEANO":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.DECIMAL != expDer.ENTERO);
                        break;
                    case "CARACTER":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.DECIMAL != expDer.CARACTER);
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
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, !expIzq.CADENA.Equals(expDer.CADENA));
                        break;
                    case "CARACTER":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, !expIzq.CADENA.Equals(expDer.CARACTER.ToString()));
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " != " + expDer.tipo, fila, columna);
                        break;
                }
            }
            else if (expIzq.tipo.Equals("BOOLEANO"))
            {
                switch (expDer.tipo)
                {
                    case "ENTERO":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.ENTERO != expDer.ENTERO);
                        break;
                    case "DECIMAL":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.ENTERO != expDer.DECIMAL);
                        break;
                    case "BOOLEANO":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.BOOL != expDer.BOOL);
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " != " + expDer.tipo, fila, columna);
                        break;
                }
            }
            else if (expIzq.tipo.Equals("CARACTER"))
            {
                switch (expDer.tipo)
                {
                    case "ENTERO":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.CARACTER != expDer.ENTERO);
                        break;
                    case "DECIMAL":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.CARACTER != expDer.DECIMAL);
                        break;
                    case "CADENA":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.CARACTER != expDer.ENTERO);
                        break;
                    case "CARACTER":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, !expIzq.CARACTER.ToString().Equals(expDer.CADENA));
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
            if (mayor.tipo.Equals("BOOLEANO"))
            {
                switch (igual.tipo)
                {
                    case "BOOLEANO":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, mayor.BOOL || igual.BOOL);
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
            if (menor.tipo.Equals("BOOLEANO"))
            {
                switch (igual.tipo)
                {
                    case "BOOLEANO":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, menor.BOOL || igual.BOOL);
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
            if (expIzq.tipo.Equals("BOOLEANO"))
            {
                switch (expDer.tipo)
                {
                    case "BOOLEANO":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.BOOL || expDer.BOOL);
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
            if (expIzq.tipo.Equals("BOOLEANO"))
            {
                switch (expDer.tipo)
                {
                    case "BOOLEANO":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, expIzq.BOOL && expDer.BOOL);
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " && " + expDer.tipo, fila, columna);
                        break;
                }
            }
            return temp;
        }

        public expresion resXor(expresion expIzq, expresion expDer)
        {
            expresion temp = new expresion(null, null, "ERROR", "ERROR", fila, columna, null);
            if (expIzq.tipo.Equals("BOOLEANO"))
            {
                switch (expDer.tipo)
                {
                    case "BOOLEANO":
                        temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, (!expIzq.BOOL && expDer.BOOL) || (expIzq.BOOL && !expDer.BOOL));
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
            if (expDer.tipo.Equals("BOOLEANO"))
            {
                temp = new expresion(null, null, "BOOLEANO", "BOOLEANO", fila, columna, !expDer.BOOL);
            }
            else
            {
                memoria.addError("ERROR SEMANTICO ", "!" + expDer.tipo, fila, columna);
            }
            return temp;
        }

        public cadena3D resSuma(expresion expIzq, expresion expDer)
        {
            //expresion temp = new expresion(null, null, "ERROR", "ERROR", fila, columna, null);
            cadena3D temp = new cadena3D();
            cadena3D c3DIzq = resCondicion(expIzq);
            cadena3D c3DDer = resCondicion(expDer);


            if (expIzq.tipo.Equals("ENTERO"))
            {
                switch (expDer.tipo)
                {
                    case "ENTERO":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "ENTERO";

                        String codigo = "";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += "\t\n\n" + temp.temporal + "=" + c3DIzq.temporal + "+" + c3DDer.temporal + "; //SUMA RETORNA ENTERO";
                        temp.codigo += codigo;

                        //temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.ENTERO + expDer.ENTERO);
                        break;
                    case "DECIMAL":
                        //temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, expIzq.ENTERO + expDer.DECIMAL);
                        break;
                    case "CADENA":
                        //temp = new expresion(null, null, "CADENA", "CADENA", fila, columna, expIzq.ENTERO.ToString() + expDer.CADENA);
                        break;
                    case "BOOLEANO":
                        //temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.ENTERO + expDer.ENTERO);
                        break;
                    case "CARACTER":
                        //temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.ENTERO + expDer.CARACTER);
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO", expIzq.tipo + " + " + expDer.tipo, fila, columna);
                        break;
                }
            }
            //else if (expIzq.tipo.Equals("DECIMAL"))
            //{
            //    switch (expDer.tipo)
            //    {
            //        case "ENTERO":
            //            temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, expIzq.DECIMAL + expDer.ENTERO);
            //            break;
            //        case "DECIMAL":
            //            temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, expIzq.DECIMAL + expDer.DECIMAL);
            //            break;
            //        case "CADENA":
            //            temp = new expresion(null, null, "CADENA", "CADENA", fila, columna, expIzq.DECIMAL.ToString() + expDer.CADENA);
            //            break;
            //        case "BOOLEANO":
            //            temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, expIzq.DECIMAL + expDer.ENTERO);
            //            break;
            //        case "CARACTER":
            //            temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, expIzq.DECIMAL + expDer.CARACTER);
            //            break;
            //        default:
            //            memoria.addError("ERROR SEMANTICO", expIzq.tipo + " + " + expDer.tipo, fila, columna);
            //            break;
            //    }
            //}
            //else if (expIzq.tipo.Equals("CADENA"))
            //{
            //    switch (expDer.tipo)
            //    {
            //        case "ENTERO":
            //            temp = new expresion(null, null, "CADENA", "CADENA", fila, columna, expIzq.CADENA + expDer.ENTERO.ToString());
            //            break;
            //        case "DECIMAL":
            //            temp = new expresion(null, null, "CADENA", "CADENA", fila, columna, expIzq.CADENA + expDer.DECIMAL.ToString());
            //            break;
            //        case "CADENA":
            //            temp = new expresion(null, null, "CADENA", "CADENA", fila, columna, expIzq.CADENA + expDer.CADENA);
            //            break;
            //        case "CARACTER":
            //            temp = new expresion(null, null, "CADENA", "CADENA", fila, columna, expIzq.CADENA + expDer.CADENA);
            //            break;
            //        default:
            //            memoria.addError("ERROR SEMANTICO", expIzq.tipo + " + " + expDer.tipo, fila, columna);
            //            break;
            //    }
            //}
            //else if (expIzq.tipo.Equals("BOOLEANO"))
            //{
            //    switch (expDer.tipo)
            //    {
            //        case "ENTERO":
            //            temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.ENTERO + expDer.ENTERO);
            //            break;
            //        case "DECIMAL":
            //            temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, expIzq.ENTERO + expDer.DECIMAL);
            //            break;
            //        case "BOOLEANO":
            //            temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.BOOL || expDer.BOOL);
            //            break;
            //        case "CADENA":
            //            temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.CADENA + expDer.CADENA);
            //            break;
            //        default:
            //            memoria.addError("ERROR SEMANTICO", expIzq.tipo + " + " + expDer.tipo, fila, columna);
            //            break;
            //    }
            //}
            //else if (expIzq.tipo.Equals("CARACTER"))
            //{
            //    switch (expDer.tipo)
            //    {
            //        case "ENTERO":
            //            temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.CARACTER + expDer.ENTERO);
            //            break;
            //        case "DECIMAL":
            //            temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, expIzq.CARACTER + expDer.DECIMAL);
            //            break;
            //        case "CADENA":
            //            temp = new expresion(null, null, "CADENA", "CADENA", fila, columna, expIzq.CADENA + expDer.CADENA);
            //            break;
            //        case "CARACTER":
            //            temp = new expresion(null, null, "CADENA", "CADENA", fila, columna, expIzq.CADENA + expDer.CADENA);
            //            break;
            //        default:
            //            memoria.addError("ERROR SEMANTICO", expIzq.tipo + " + " + expDer.tipo, fila, columna);
            //            break;
            //    }
            //}
            return temp;
        }

        public expresion resResta(expresion expDer)
        {
            expresion temp = new expresion(null, null, "ERROR", "ERROR", fila, columna, null);
            if (expDer.tipo.Equals("ENTERO"))
            {
                temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, -expDer.ENTERO);
            }
            else if (expDer.tipo.Equals("DECIMAL"))
            {
                temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, -expDer.DECIMAL);
            }
            else if (expDer.tipo.Equals("BOOLEANO"))
            {
                temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, -expDer.ENTERO);
            }
            else if (expDer.tipo.Equals("CARACTER"))
            {
                temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, -expDer.CARACTER);
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
                    case "DECIMAL":
                        temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.ENTERO - expDer.DECIMAL);
                        break;
                    case "BOOLEANO":
                        temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.ENTERO - expDer.ENTERO);
                        break;
                    case "CARACTER":
                        temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.ENTERO - expDer.CARACTER);
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " - " + expDer.tipo, fila, columna);
                        break;
                }
            }
            else if (expIzq.tipo.Equals("DECIMAL"))
            {
                switch (expDer.tipo)
                {
                    case "ENTERO":
                        temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, expIzq.DECIMAL - expDer.ENTERO);
                        break;
                    case "DECIMAL":
                        temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, expIzq.DECIMAL - expDer.DECIMAL);
                        break;
                    case "BOOLEANO":
                        temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, expIzq.DECIMAL - expDer.ENTERO);
                        break;
                    case "CARACTER":
                        temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, expIzq.DECIMAL - expDer.CARACTER);
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " - " + expDer.tipo, fila, columna);
                        break;
                }
            }
            else if (expIzq.tipo.Equals("BOOLEANO"))
            {
                switch (expDer.tipo)
                {
                    case "ENTERO":
                        temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.ENTERO - expDer.ENTERO);
                        break;
                    case "DECIMAL":
                        temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, expIzq.ENTERO - expDer.DECIMAL);
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " - " + expDer.tipo, fila, columna);
                        break;
                }
            }
            else if (expIzq.tipo.Equals("CARACTER"))
            {
                switch (expDer.tipo)
                {
                    case "ENTERO":
                        temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.CARACTER - expDer.ENTERO);
                        break;
                    case "DECIMAL":
                        temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, expIzq.CARACTER - expDer.DECIMAL);
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO", expIzq.tipo + " - " + expDer.tipo, fila, columna);
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
                    case "DECIMAL":
                        temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, expIzq.ENTERO * expDer.DECIMAL);
                        break;
                    case "BOOLEANO":
                        temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.ENTERO * expDer.ENTERO);
                        break;
                    case "CARACTER":
                        temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.ENTERO * expDer.CARACTER);
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " * " + expDer.tipo, fila, columna);
                        break;
                }
            }
            else if (expIzq.tipo.Equals("DECIMAL"))
            {
                switch (expDer.tipo)
                {
                    case "ENTERO":
                        temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, expIzq.DECIMAL * expDer.ENTERO);
                        break;
                    case "DECIMAL":
                        temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, expIzq.DECIMAL * expDer.DECIMAL);
                        break;
                    case "BOOLEANO":
                        temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, expIzq.DECIMAL * expDer.ENTERO);
                        break;
                    case "CARACTER":
                        temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, expIzq.DECIMAL * expDer.CARACTER);
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " * " + expDer.tipo, fila, columna);
                        break;
                }
            }
            else if (expIzq.tipo.Equals("BOOLEANO"))
            {
                switch (expDer.tipo)
                {
                    case "ENTERO":
                        temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.ENTERO * expDer.ENTERO);
                        break;
                    case "DECIMAL":
                        temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, expIzq.ENTERO * expDer.DECIMAL);
                        break;
                    case "BOOLEANO":
                        temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.BOOL && expDer.BOOL);
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " * " + expDer.tipo, fila, columna);
                        break;
                }
            }
            else if (expIzq.tipo.Equals("CARACTER"))
            {
                switch (expDer.tipo)
                {
                    case "ENTERO":
                        temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.CARACTER * expDer.ENTERO);
                        break;
                    case "DECIMAL":
                        temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, expIzq.CARACTER * expDer.DECIMAL);
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO", expIzq.tipo + " - " + expDer.tipo, fila, columna);
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
                    case "DECIMAL":
                        temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, expIzq.ENTERO / expDer.DECIMAL);
                        break;
                    case "BOOLEANO":
                        temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, expIzq.ENTERO / expDer.ENTERO);
                        break;
                    case "CARACTER":
                        temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, expIzq.ENTERO / expDer.CARACTER);
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " / " + expDer.tipo, fila, columna);
                        break;
                }
            }
            else if (expIzq.tipo.Equals("DECIMAL"))
            {
                switch (expDer.tipo)
                {
                    case "ENTERO":
                        temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, expIzq.DECIMAL / expDer.ENTERO);
                        break;
                    case "DECIMAL":
                        temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, expIzq.DECIMAL / expDer.DECIMAL);
                        break;
                    case "BOOLEANO":
                        temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, expIzq.DECIMAL / expDer.ENTERO);
                        break;
                    case "CARACTER":
                        temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, expIzq.ENTERO / expDer.CARACTER);
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " / " + expDer.tipo, fila, columna);
                        break;
                }
            }
            else if (expIzq.tipo.Equals("BOOLEANO"))
            {
                switch (expDer.tipo)
                {
                    case "ENTERO":
                        temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, expIzq.ENTERO / expDer.ENTERO);
                        break;
                    case "DECIMAL":
                        temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, expIzq.ENTERO / expDer.DECIMAL);
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " / " + expDer.tipo, fila, columna);
                        break;
                }
            }
            else if (expIzq.tipo.Equals("CARACTER"))
            {
                switch (expDer.tipo)
                {
                    case "ENTERO":
                        temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.CARACTER / expDer.ENTERO);
                        break;
                    case "DECIMAL":
                        temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, expIzq.CARACTER / expDer.DECIMAL);
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO", expIzq.tipo + " - " + expDer.tipo, fila, columna);
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
                    case "DECIMAL":
                        temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, Math.Pow(expIzq.ENTERO, expDer.DECIMAL));
                        break;
                    case "BOOLEANO":
                        {
                            double pb = Math.Pow(expIzq.ENTERO, expDer.ENTERO);
                            int vb = (int)pb;
                            temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, vb);
                        }
                        break;
                    case "CARACTER":
                        {
                            double pb = Math.Pow(expIzq.ENTERO, expDer.ENTERO);
                            int vb = (int)pb;
                            temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, vb);
                        }
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " ^ " + expDer.tipo, fila, columna);
                        break;
                }
            }
            else if (expIzq.tipo.Equals("DECIMAL"))
            {
                switch (expDer.tipo)
                {
                    case "ENTERO":
                        temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, Math.Pow(expIzq.DECIMAL, expDer.ENTERO));
                        break;
                    case "DECIMAL":
                        temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, Math.Pow(expIzq.DECIMAL, expDer.DECIMAL));
                        break;
                    case "BOOLEANO":
                        temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, Math.Pow(expIzq.DECIMAL, expDer.ENTERO));
                        break;
                    case "CARACTER":
                        temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, Math.Pow(expIzq.DECIMAL, expDer.ENTERO));
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " ^ " + expDer.tipo, fila, columna);
                        break;
                }
            }
            else if (expIzq.tipo.Equals("BOOLEANO"))
            {
                switch (expDer.tipo)
                {
                    case "ENTERO":
                        double p = Math.Pow(expIzq.ENTERO, expDer.ENTERO);
                        int v = (int)p;
                        temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, v);
                        break;
                    case "DECIMAL":
                        temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, Math.Pow(expIzq.ENTERO, expDer.DECIMAL));
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " ^ " + expDer.tipo, fila, columna);
                        break;
                }
            }
            else if (expIzq.tipo.Equals("CARACTER"))
            {
                switch (expDer.tipo)
                {
                    case "ENTERO":
                        double pb = Math.Pow(expIzq.ENTERO, expDer.ENTERO);
                        int vb = (int)pb;
                        temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, vb);
                        break;
                    case "DECIMAL":
                        temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, Math.Pow(expIzq.ENTERO, expDer.DECIMAL));
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO", expIzq.tipo + " - " + expDer.tipo, fila, columna);
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
            else if (expIzq.tipo.Equals("DECIMAL"))
            {
                temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, expIzq.DECIMAL + 1);
            }
            else if (expIzq.tipo.Equals("CARACTER"))
            {
                temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.ENTERO + 1);
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
            else if (expIzq.tipo.Equals("DECIMAL"))
            {
                temp = new expresion(null, null, "DECIMAL", "DECIMAL", fila, columna, expIzq.DECIMAL - 1);
            }
            else if (expIzq.tipo.Equals("CARACTER"))
            {
                temp = new expresion(null, null, "ENTERO", "ENTERO", fila, columna, expIzq.ENTERO - 1);
            }
            else
            {
                memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " -- ", fila, columna);
            }
            return temp;
        }
    }
}
