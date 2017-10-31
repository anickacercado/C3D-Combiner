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
                this.CADENA = valor.ToString();
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
                    c3D = resResta(expIzq, expDer);
                }
                else
                {
                    c3D = resResta(expDer);
                }
            }
            else if (nodo.tipo.Equals("*"))
            {
                c3D = resMultiplicacion(expIzq, expDer);
            }
            else if (nodo.tipo.Equals("/"))
            {
                c3D = resDivision(expIzq, expDer);
            }
            else if (nodo.tipo.Equals("^") || nodo.tipo.ToUpper().Equals("POW"))
            {
                c3D = resPotencia(expIzq, expDer);
            }
            else if (nodo.tipo.Equals("++"))
            {
                c3D = resAumento(expIzq);
            }
            else if (nodo.tipo.Equals("--"))
            {
                c3D = resDecremento(expIzq);
            }
            else if (nodo.tipo.Equals(">"))
            {
                c3D = resRelacional(expIzq, expDer, ">");
            }
            else if (nodo.tipo.Equals("<"))
            {
                c3D = resRelacional(expIzq, expDer, "<");
            }
            else if (nodo.tipo.Equals(">="))
            {
                c3D = resRelacional(expIzq, expDer, ">=");
            }
            else if (nodo.tipo.Equals("<="))
            {
                c3D = resRelacional(expIzq, expDer, "<=");
            }
            else if (nodo.tipo.Equals("=="))
            {
                c3D = resRelacional(expIzq, expDer, "==");
            }
            else if (nodo.tipo.Equals("!="))
            {
                c3D = resRelacional(expIzq, expDer, "!=");
            }
            else if (nodo.tipo.ToUpper().Equals("OR") || nodo.tipo.Equals("||"))
            {
                c3D = resOr(expIzq, expDer);
            }
            else if (nodo.tipo.ToUpper().Equals("XOR") || nodo.tipo.Equals("??"))
            {
                c3D = resXor(expIzq, expDer);
            }
            else if (nodo.tipo.ToUpper().Equals("AND") || nodo.tipo.Equals("&&"))
            {
                c3D = resAnd(expIzq, expDer);
            }
            else if (nodo.tipo.ToUpper().Equals("NOT") || nodo.tipo.Equals("!"))
            {
                c3D = resNot(expDer);
            }
            else if (nodo.tipo.Equals("ENTERO"))
            {
                c3D.temporal = memoria.getTemp();
                c3D.codigo = c3D.temporal + "=" + nodo.ENTERO.ToString() + "; //Entero" + "\r\n";
                c3D.tipo = nodo.tipo;
                temp = new expresion(nodo);
            }
            else if (nodo.tipo.Equals("DECIMAL"))
            {
                c3D.temporal = memoria.getTemp();
                c3D.codigo = c3D.temporal + "=" + nodo.DECIMAL.ToString() + "; //Decimal" + "\r\n";
                c3D.tipo = nodo.tipo;
                temp = new expresion(nodo);
            }
            else if (nodo.tipo.Equals("CADENA"))
            {
                c3D.temporal = memoria.getTemp();

                /*CODIGO CADENA*/
                String codigo = "";
                codigo +=  c3D.temporal + " = H; //Inicio de cadena" + "\r\n";
                codigo += "H = H + 1;" + "\r\n";

                for (int i=0; i<nodo.CADENA.Length; i++) {
                    codigo += "Heap[H] = " + (int)nodo.CADENA[i] + ";" + "\r\n";
                    codigo += "H = H + 1;" + "\r\n";
                }

                codigo += "Heap[H] = " + (int)memoria.finCadena + "; //Fin de cadena" + "\r\n";
                codigo += "H = H + 1;" + "\r\n";
                /*CODIGO CADENA*/

                c3D.tipo = nodo.tipo;
                c3D.codigo = codigo;
                temp = new expresion(nodo);

            }
            else if (nodo.tipo.Equals("BOOLEANO"))
            {
                c3D.temporal = memoria.getTemp();
                c3D.codigo = c3D.temporal + "=" + nodo.ENTERO.ToString() + "; //Booleano" + "\r\n";
                c3D.tipo = nodo.tipo;
                temp = new expresion(nodo);
            }
            else if (nodo.tipo.Equals("CARACTER"))
            {
                c3D.temporal = memoria.getTemp();
                c3D.codigo = c3D.temporal + "=" + nodo.ENTERO.ToString() + "; //Caracter" + "\r\n";
                c3D.tipo = nodo.tipo;
                temp = new expresion(nodo);
            }
            return c3D;
        }

        public cadena3D resCondicion()
        {
            return resCondicion(this);
        }

        public cadena3D resRelacional(expresion expIzq, expresion expDer, String tipo)
        {
            String codigo = "";
            cadena3D temp = new cadena3D();
            cadena3D c3DIzq = resCondicion(expIzq);
            cadena3D c3DDer = resCondicion(expDer);
            if (c3DIzq.tipo.Equals("ENTERO"))
            {
                switch (c3DDer.tipo)
                {
                    case "ENTERO":
                        {
                            String etq1 = memoria.getEtq();
                            String etq2 = memoria.getEtq();

                            codigo += "//Inicio relacional entero " + tipo + " entero" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += "if " + c3DIzq.temporal + "" + tipo + "" + c3DDer.temporal + " goto " + etq1 + ";" + "\r\n";
                            codigo += "goto " + etq2 + ";" + "\r\n";
                            codigo += "//Fin relacional entero " + tipo + " entero" + "\r\n\n";

                            temp.tipo = "BOOLEANO";
                            temp.etqVerdadera = etq1;
                            temp.etqFalsa = etq2;
                            temp.codigo += codigo;
                        }
                        break;
                    case "DECIMAL":
                        {
                            String etq1 = memoria.getEtq();
                            String etq2 = memoria.getEtq();

                            codigo += "//Inicio relacional entero " + tipo + " decimal" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += "if " + c3DIzq.temporal + "" + tipo + "" + c3DDer.temporal + " goto " + etq1 + ";" + "\r\n";
                            codigo += "goto " + etq2 + ";" + "\r\n";
                            codigo += "//Fin relacional entero " + tipo + " decimal" + "\r\n\n";

                            temp.tipo = "BOOLEANO";
                            temp.etqVerdadera = etq1;
                            temp.etqFalsa = etq2;
                            temp.codigo += codigo;
                        }
                        break;
                    case "CADENA":
                        {
                            String tempEntero = memoria.getTemp();
                            String tempCad1 = memoria.getTemp();
                            String tempSumaCad1 = memoria.getTemp();
                            String tempHeap1 = memoria.getTemp();

                            String etq1 = memoria.getEtq();
                            String etq2 = memoria.getEtq();
                            String etq3 = memoria.getEtq();
                            String etq4 = memoria.getEtq();

                            codigo += "//Inicio comparacion de entero " + tipo + " cadena" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo + "\r\n";

                            codigo += tempEntero + "=" + c3DIzq.temporal + "; //Entero" + "\r\n";
                            codigo += tempCad1 + "=" + c3DDer.temporal + "; //Posicion en Heap caracter cadena 1" + "\r\n";
                            codigo += tempSumaCad1 + "=0" + "; //Suma cadena 1" + "\r\n";
                            codigo += "\r\n";
                            codigo += etq1 + ":" + "\r\n";
                            codigo += "\t" + tempHeap1 + "=" + "Heap[" + tempCad1 + "]" + "; //Valor de ascii del Heap cadena 1" + "\r\n";
                            codigo += "\t" + "if " + tempHeap1 + "" + tipo + "0 goto " + etq2 + ";" + "\r\n";
                            codigo += "\t" + tempSumaCad1 + "=" + tempSumaCad1 + "+" + tempHeap1 + ";" + "\r\n";
                            codigo += "\t" + tempCad1 + "=" + tempCad1 + "+1;" + "\r\n";
                            codigo += "\t" + "goto " + etq1 + ";" + "\r\n\n";

                            codigo += etq2 + ":" + "\r\n";
                            codigo += "\t" + "if " + tempEntero + "" + tipo + "" + tempSumaCad1 + " goto " + etq3 + ";" + "\r\n";
                            codigo += "\t" + "goto " + etq4 + ";" + "\r\n";
                            codigo += "//Fin comparacion de entero " + tipo + " cadena" + "\r\n\n";

                            temp.tipo = "BOOLEANO";
                            temp.etqVerdadera = etq3;
                            temp.etqFalsa = etq4;
                            temp.codigo += codigo;
                        }
                        break;
                    case "BOOLEANO":
                        {
                            String etq1 = memoria.getEtq();
                            String etq2 = memoria.getEtq();

                            codigo += "//Inicio relacional entero " + tipo + " booleano" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += "if " + c3DIzq.temporal + "" + tipo + "" + c3DDer.temporal + " goto " + etq1 + ";" + "\r\n";
                            codigo += "goto " + etq2 + ";" + "\r\n";
                            codigo += "//Fin relacional entero " + tipo + " booleano" + "\r\n\n";

                            temp.tipo = "BOOLEANO";
                            temp.etqVerdadera = etq1;
                            temp.etqFalsa = etq2;
                            temp.codigo += codigo;
                        }
                        break;
                    case "CARACTER":
                        {
                            String etq1 = memoria.getEtq();
                            String etq2 = memoria.getEtq();

                            codigo += "//Inicio relacional entero " + tipo + " caracter" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += "if " + c3DIzq.temporal + "" + tipo + "" + c3DDer.temporal + " goto " + etq1 + ";" + "\r\n";
                            codigo += "goto " + etq2 + ";" + "\r\n";
                            codigo += "//Fin relacional entero " + tipo + " caracter" + "\r\n\n";

                            temp.tipo = "BOOLEANO";
                            temp.etqVerdadera = etq1;
                            temp.etqFalsa = etq2;
                            temp.codigo += codigo;
                        }
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", c3DIzq.tipo + " " + tipo + " " + c3DDer.tipo, fila, columna);
                        break;
                }
            }
            else if (c3DIzq.tipo.Equals("DECIMAL"))
            {
                switch (c3DDer.tipo)
                {
                    case "ENTERO":
                        {
                            String etq1 = memoria.getEtq();
                            String etq2 = memoria.getEtq();

                            codigo += "//Inicio relacional decimal " + tipo + " entero" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += "if " + c3DIzq.temporal + "" + tipo + "" + c3DDer.temporal + " goto " + etq1 + ";" + "\r\n";
                            codigo += "goto " + etq2 + ";" + "\r\n";
                            codigo += "//Fin relacional decimal " + tipo + " entero" + "\r\n\n";

                            temp.tipo = "BOOLEANO";
                            temp.etqVerdadera = etq1;
                            temp.etqFalsa = etq2;
                            temp.codigo += codigo;
                        }
                        break;
                    case "DECIMAL":
                        {
                            String etq1 = memoria.getEtq();
                            String etq2 = memoria.getEtq();

                            codigo += "//Inicio relacional decimal " + tipo + " decimal" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += "if " + c3DIzq.temporal + "" + tipo + "" + c3DDer.temporal + " goto " + etq1 + ";" + "\r\n";
                            codigo += "goto " + etq2 + ";" + "\r\n";
                            codigo += "//Fin relacional decimal " + tipo + " decimal" + "\r\n\n";

                            temp.tipo = "BOOLEANO";
                            temp.etqVerdadera = etq1;
                            temp.etqFalsa = etq2;
                            temp.codigo += codigo;
                        }
                        break;
                    case "CADENA":
                        {
                            String tempEntero = memoria.getTemp();
                            String tempCad1 = memoria.getTemp();
                            String tempSumaCad1 = memoria.getTemp();
                            String tempHeap1 = memoria.getTemp();

                            String etq1 = memoria.getEtq();
                            String etq2 = memoria.getEtq();
                            String etq3 = memoria.getEtq();
                            String etq4 = memoria.getEtq();

                            codigo += "//Inicio comparacion de decimal " + tipo + " cadena" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo + "\r\n";

                            codigo += tempEntero + "=" + c3DIzq.temporal + "; //Decimal" + "\r\n";
                            codigo += tempCad1 + "=" + c3DDer.temporal + "; //Posicion en Heap caracter cadena 1" + "\r\n";
                            codigo += tempSumaCad1 + "=0" + "; //Suma cadena 1" + "\r\n";
                            codigo += "\r\n";
                            codigo += etq1 + ":" + "\r\n";
                            codigo += "\t" + tempHeap1 + "=" + "Heap[" + tempCad1 + "]" + "; //Valor de ascii del Heap cadena 1" + "\r\n";
                            codigo += "\t" + "if " + tempHeap1 + "" + tipo + "0 goto " + etq2 + ";" + "\r\n";
                            codigo += "\t" + tempSumaCad1 + "=" + tempSumaCad1 + "+" + tempHeap1 + ";" + "\r\n";
                            codigo += "\t" + tempCad1 + "=" + tempCad1 + "+1;" + "\r\n";
                            codigo += "\t" + "goto " + etq1 + ";" + "\r\n\n";

                            codigo += etq2 + ":" + "\r\n";
                            codigo += "\t" + "if " + tempEntero + "" + tipo + "" + tempSumaCad1 + " goto " + etq3 + ";" + "\r\n";
                            codigo += "\t" + "goto " + etq4 + ";" + "\r\n";
                            codigo += "//Fin comparacion de decimal " + tipo + " cadena" + "\r\n\n";

                            temp.tipo = "BOOLEANO";
                            temp.etqVerdadera = etq3;
                            temp.etqFalsa = etq4;
                            temp.codigo += codigo;
                        }
                        break;
                    case "BOOLEANO":
                        {
                            String etq1 = memoria.getEtq();
                            String etq2 = memoria.getEtq();

                            codigo += "//Inicio relacional decimal " + tipo + " booleano" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += "if " + c3DIzq.temporal + "" + tipo + "" + c3DDer.temporal + " goto " + etq1 + ";" + "\r\n";
                            codigo += "goto " + etq2 + ";" + "\r\n";
                            codigo += "//Fin relacional decimal " + tipo + " booleano" + "\r\n\n";

                            temp.tipo = "BOOLEANO";
                            temp.etqVerdadera = etq1;
                            temp.etqFalsa = etq2;
                            temp.codigo += codigo;
                        }
                        break;
                    case "CARACTER":
                        {
                            String etq1 = memoria.getEtq();
                            String etq2 = memoria.getEtq();

                            codigo += "//Inicio relacional decimal " + tipo + " caracter" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += "if " + c3DIzq.temporal + "" + tipo + "" + c3DDer.temporal + " goto " + etq1 + ";" + "\r\n";
                            codigo += "goto " + etq2 + ";" + "\r\n";
                            codigo += "//Fin relacional decimal " + tipo + " caracter" + "\r\n\n";

                            temp.tipo = "BOOLEANO";
                            temp.etqVerdadera = etq1;
                            temp.etqFalsa = etq2;
                            temp.codigo += codigo;
                        }
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", c3DIzq.tipo + " " + tipo + " " + c3DDer.tipo, fila, columna);
                        break;
                }
            }
            else if (expIzq.tipo.Equals("CADENA"))
            {
                switch (expDer.tipo)
                {
                    case "CADENA":
                        {
                            String tempCad1 = memoria.getTemp();
                            String tempCad2 = memoria.getTemp();
                            String tempSumaCad1 = memoria.getTemp();
                            String tempHeap1 = memoria.getTemp();
                            String tempSumaCad2 = memoria.getTemp();
                            String tempHeap2 = memoria.getTemp();

                            String etq1 = memoria.getEtq();
                            String etq2 = memoria.getEtq();
                            String etq3 = memoria.getEtq();
                            String etq4 = memoria.getEtq();
                            String etq5 = memoria.getEtq();
                            String etq6 = memoria.getEtq();

                            codigo += "//Inicio relacional de cadena " + tipo + " cadena" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo + "\r\n";

                            codigo += tempCad1 + "=" + c3DIzq.temporal + "; //Posicion en Heap caracter cadena 1" + "\r\n";
                            codigo += tempCad2 + "=" + c3DDer.temporal + "; //Posicion en Heap caracter cadena 2" + "\r\n";
                            codigo += tempSumaCad1 + "=0" + "; //Suma cadena 1" + "\r\n";
                            codigo += "\r\n";
                            codigo += etq1 + ":" + "\r\n";
                            codigo += "\t" + tempHeap1 + "=" + "Heap[" + tempCad1 + "]" + "; //Valor de ascii del Heap cadena 1" + "\r\n";
                            codigo += "\t" + "if " + tempHeap1 + "" + tipo + "0 goto " + etq2 + ";" + "\r\n";
                            codigo += "\t" + tempSumaCad1 + "=" + tempSumaCad1 + "+" + tempHeap1 + ";" + "\r\n";
                            codigo += "\t" + tempCad1 + "=" + tempCad1 + "+1;" + "\r\n";
                            codigo += "\t" + "goto " + etq1 + ";" + "\r\n\n";

                            codigo += etq2 + ":" + "\r\n";
                            codigo += "\t" + tempSumaCad2 + "=0" + "; //Suma cadena 2" + "\r\n";
                            codigo += etq3 + ":" + "\r\n";
                            codigo += "\t" + tempHeap2 + "=" + "Heap[" + tempCad2 + "]" + "; //Valor de ascii del Heap cadena 2" + "\r\n";
                            codigo += "\t" + "if " + tempHeap2 + "" + tipo + "0 goto " + etq4 + ";" + "\r\n";
                            codigo += "\t" + tempSumaCad2 + "=" + tempSumaCad2 + "+" + tempHeap2 + ";" + "\r\n";
                            codigo += "\t" + tempCad2 + "=" + tempCad2 + "+1;" + "\r\n";
                            codigo += "\t" + "goto " + etq3 + ";" + "\r\n\n";

                            codigo += etq4 + ":" + "\r\n";
                            codigo += "\t" + "if " + tempSumaCad1 + "" + tipo + "" + tempSumaCad2 + " goto " + etq5 + ";" + "\r\n";
                            codigo += "\t" + "goto " + etq6 + ";" + "\r\n";
                            codigo += "//Fin relacional de cadena " + tipo + " cadena" + "\r\n\n";

                            temp.tipo = "BOOLEANO";
                            temp.etqVerdadera = etq5;
                            temp.etqFalsa = etq6;
                            temp.codigo += codigo;
                        }
                        break;
                    case "CARACTER":
                        {
                            String tempCad1 = memoria.getTemp();
                            String tempEntero = memoria.getTemp();
                            String tempSumaCad1 = memoria.getTemp();
                            String tempHeap1 = memoria.getTemp();

                            String etq1 = memoria.getEtq();
                            String etq2 = memoria.getEtq();
                            String etq3 = memoria.getEtq();
                            String etq4 = memoria.getEtq();

                            codigo += "//Inicio relacional de cadena " + tipo + " caracter" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo + "\r\n";

                            codigo += tempCad1 + "=" + c3DIzq.temporal + "; //Posicion en Heap caracter cadena 1" + "\r\n";
                            codigo += tempEntero + "=" + c3DDer.temporal + "; //Caracter" + "\r\n";
                            codigo += tempSumaCad1 + "=0" + "; //Suma cadena 1" + "\r\n";
                            codigo += "\r\n";
                            codigo += etq1 + ":" + "\r\n";
                            codigo += "\t" + tempHeap1 + "=" + "Heap[" + tempCad1 + "]" + "; //Valor de ascii del Heap cadena 1" + "\r\n";
                            codigo += "\t" + "if " + tempHeap1 + "" + tipo + "0 goto " + etq2 + ";" + "\r\n";
                            codigo += "\t" + tempSumaCad1 + "=" + tempSumaCad1 + "+" + tempHeap1 + ";" + "\r\n";
                            codigo += "\t" + tempCad1 + "=" + tempCad1 + "+1;" + "\r\n";
                            codigo += "\t" + "goto " + etq1 + ";" + "\r\n\n";

                            codigo += etq2 + ":" + "\r\n";
                            codigo += "\t" + "if " + tempSumaCad1 + "" + tipo + "" + tempEntero + " goto " + etq3 + ";" + "\r\n";
                            codigo += "\t" + "goto " + etq4 + ";" + "\r\n";
                            codigo += "//Fin relacional de cadena " + tipo + " caracter" + "\r\n\n";

                            temp.tipo = "BOOLEANO";
                            temp.etqVerdadera = etq3;
                            temp.etqFalsa = etq4;
                            temp.codigo += codigo;
                        }
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", c3DIzq.tipo + " " + tipo + " " + c3DDer.tipo, fila, columna);
                        break;
                }
            }
            else if (c3DIzq.tipo.Equals("BOOLEANO"))
            {
                switch (c3DDer.tipo)
                {
                    case "ENTERO":
                        {
                            String etq1 = memoria.getEtq();
                            String etq2 = memoria.getEtq();

                            codigo += "//Inicio relacional booleano " + tipo + " entero" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += "if " + c3DIzq.temporal + "" + tipo + "" + c3DDer.temporal + " goto " + etq1 + ";" + "\r\n";
                            codigo += "goto " + etq2 + ";" + "\r\n";
                            codigo += "//Fin relacional booleano " + tipo + " entero" + "\r\n\n";

                            temp.tipo = "BOOLEANO";
                            temp.etqVerdadera = etq1;
                            temp.etqFalsa = etq2;
                            temp.codigo += codigo;
                        }
                        break;
                    case "DECIMAL":
                        {
                            String etq1 = memoria.getEtq();
                            String etq2 = memoria.getEtq();

                            codigo += "//Inicio relacional booleano " + tipo + " decimal" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += "if " + c3DIzq.temporal + "" + tipo + "" + c3DDer.temporal + " goto " + etq1 + ";" + "\r\n";
                            codigo += "goto " + etq2 + ";" + "\r\n";
                            codigo += "//Fin relacional booleano " + tipo + " decimal" + "\r\n\n";

                            temp.tipo = "BOOLEANO";
                            temp.etqVerdadera = etq1;
                            temp.etqFalsa = etq2;
                            temp.codigo += codigo;
                        }
                        break;
                    case "BOOLEANO":
                        {
                            String etq1 = memoria.getEtq();
                            String etq2 = memoria.getEtq();

                            codigo += "//Inicio relacional booleano " + tipo + " booleano" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += "if " + c3DIzq.temporal + "" + tipo + "" + c3DDer.temporal + " goto " + etq1 + ";" + "\r\n";
                            codigo += "goto " + etq2 + ";" + "\r\n";
                            codigo += "//Fin relacional booleano " + tipo + " booleano" + "\r\n\n";

                            temp.tipo = "BOOLEANO";
                            temp.etqVerdadera = etq1;
                            temp.etqFalsa = etq2;
                            temp.codigo += codigo;
                        }
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", c3DIzq.tipo + " " + tipo + " " + c3DDer.tipo, fila, columna);
                        break;
                }
            }
            else if (expIzq.tipo.Equals("CARACTER"))
            {
                switch (expDer.tipo)
                {
                    case "ENTERO":
                        {
                            String etq1 = memoria.getEtq();
                            String etq2 = memoria.getEtq();

                            codigo += "//Inicio relacional caracter " + tipo + " entero" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += "if " + c3DIzq.temporal + "" + tipo + "" + c3DDer.temporal + " goto " + etq1 + ";" + "\r\n";
                            codigo += "goto " + etq2 + ";" + "\r\n";
                            codigo += "//Fin relacional caracter " + tipo + " entero" + "\r\n\n";

                            temp.tipo = "BOOLEANO";
                            temp.etqVerdadera = etq1;
                            temp.etqFalsa = etq2;
                            temp.codigo += codigo;
                        }
                        break;
                    case "DECIMAL":
                        {
                            String etq1 = memoria.getEtq();
                            String etq2 = memoria.getEtq();

                            codigo += "//Inicio relacional caracter " + tipo + " decimal" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += "if " + c3DIzq.temporal + "" + tipo + "" + c3DDer.temporal + " goto " + etq1 + ";" + "\r\n";
                            codigo += "goto " + etq2 + ";" + "\r\n";
                            codigo += "//Fin relacional  caracter " + tipo + " decimal" + "\r\n\n";

                            temp.tipo = "BOOLEANO";
                            temp.etqVerdadera = etq1;
                            temp.etqFalsa = etq2;
                            temp.codigo += codigo;
                        }
                        break;
                    case "CADENA":
                        {
                            String tempEntero = memoria.getTemp();
                            String tempCad1 = memoria.getTemp();
                            String tempSumaCad1 = memoria.getTemp();
                            String tempHeap1 = memoria.getTemp();

                            String etq1 = memoria.getEtq();
                            String etq2 = memoria.getEtq();
                            String etq3 = memoria.getEtq();
                            String etq4 = memoria.getEtq();

                            codigo += "//Inicio relacional de caracter " + tipo + " cadena" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo + "\r\n";

                            codigo += tempEntero + "=" + c3DIzq.temporal + "; //Caracter" + "\r\n";
                            codigo += tempCad1 + "=" + c3DDer.temporal + "; //Posicion en Heap caracter cadena 1" + "\r\n";
                            codigo += tempSumaCad1 + "=0" + "; //Suma cadena 1" + "\r\n";
                            codigo += "\r\n";
                            codigo += etq1 + ":" + "\r\n";
                            codigo += "\t" + tempHeap1 + "=" + "Heap[" + tempCad1 + "]" + "; //Valor de ascii del Heap cadena 1" + "\r\n";
                            codigo += "\t" + "if " + tempHeap1 + "" + tipo + "0 goto " + etq2 + ";" + "\r\n";
                            codigo += "\t" + tempSumaCad1 + "=" + tempSumaCad1 + "+" + tempHeap1 + ";" + "\r\n";
                            codigo += "\t" + tempCad1 + "=" + tempCad1 + "+1;" + "\r\n";
                            codigo += "\t" + "goto " + etq1 + ";" + "\r\n\n";

                            codigo += etq2 + ":" + "\r\n";
                            codigo += "\t" + "if " + tempEntero + "" + tipo + "" + tempSumaCad1 + " goto " + etq3 + ";" + "\r\n";
                            codigo += "\t" + "goto " + etq4 + ";" + "\r\n";
                            codigo += "//Fin relacional de caracter " + tipo + " cadena" + "\r\n\n";

                            temp.tipo = "BOOLEANO";
                            temp.etqVerdadera = etq3;
                            temp.etqFalsa = etq4;
                            temp.codigo += codigo;
                        }
                        break;
                    case "CARACTER":
                        {
                            String etq1 = memoria.getEtq();
                            String etq2 = memoria.getEtq();

                            codigo += "//Inicio relacional caracter " + tipo + " caracter" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += "if " + c3DIzq.temporal + "" + tipo + "" + c3DDer.temporal + " goto " + etq1 + ";" + "\r\n";
                            codigo += "goto " + etq2 + ";" + "\r\n";
                            codigo += "//Fin relacional caracter " + tipo + " caracter" + "\r\n\n";

                            temp.tipo = "BOOLEANO";
                            temp.etqVerdadera = etq1;
                            temp.etqFalsa = etq2;
                            temp.codigo += codigo;
                        }
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", c3DIzq.tipo + " " + tipo + " " + c3DDer.tipo, fila, columna);
                        break;
                }
            }
            return temp;
        }

        public cadena3D resOr(expresion expIzq, expresion expDer)
        {
            String codigo = "";
            cadena3D temp = new cadena3D();
            cadena3D c3DIzq = resCondicion(expIzq);
            cadena3D c3DDer = resCondicion(expDer);

            if (c3DIzq.tipo.Equals("BOOLEANO"))
            {
                switch (c3DDer.tipo)
                {
                    case "BOOLEANO":

                        codigo += "//Inicio logica de OR " +  "\r\n";
                        if (c3DIzq.etqVerdadera == "" && c3DIzq.etqFalsa == "")
                        {
                            String etqVerdadera = memoria.getEtq();
                            String etqFalsa = memoria.getEtq();

                            codigo += c3DIzq.codigo;
                            codigo += "if " + c3DIzq.temporal + "==1 goto " + etqVerdadera + ";" + "\r\n";
                            codigo += "goto " + etqFalsa + ";" + "\r\n";

                            c3DIzq.etqVerdadera = etqVerdadera;
                            c3DIzq.etqFalsa = etqFalsa;
                        }
                        else {
                            codigo += c3DIzq.codigo;
                        }

                        codigo += c3DIzq.etqFalsa + ":" + "\r\n";

                        if (c3DDer.etqVerdadera == "" && c3DDer.etqFalsa == "")
                        {
                            String etqVerdadera = memoria.getEtq();
                            String etqFalsa = memoria.getEtq();

                            codigo += c3DDer.codigo;
                            codigo += "if " + c3DDer.temporal + "==1 goto " + etqVerdadera + ";" + "\r\n";
                            codigo += "goto " + etqFalsa + ";" + "\r\n";

                            c3DDer.etqVerdadera = etqVerdadera;
                            c3DDer.etqFalsa = etqFalsa;
                        }
                        else
                        {
                            codigo += c3DDer.codigo;
                        }
                        codigo += "//Fin logica de OR " + "\r\n\n";

                        temp.etqVerdadera = c3DIzq.etqVerdadera + ":\n" + c3DDer.etqVerdadera;
                        temp.etqFalsa = c3DDer.etqFalsa;
                        temp.codigo = codigo;
                        temp.tipo = "BOOLEANO";
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " || " + expDer.tipo, fila, columna);
                        break;
                }
            }
            return temp;
        }

        public cadena3D resAnd(expresion expIzq, expresion expDer)
        {
            String codigo = "";
            cadena3D temp = new cadena3D();
            cadena3D c3DIzq = resCondicion(expIzq);
            cadena3D c3DDer = resCondicion(expDer);

            if (c3DIzq.tipo.Equals("BOOLEANO"))
            {
                switch (c3DDer.tipo)
                {
                    case "BOOLEANO":

                        codigo += "//Inicio logica de AND " + "\r\n";
                        if (c3DIzq.etqVerdadera == "" && c3DIzq.etqFalsa == "")
                        {
                            String etqVerdadera = memoria.getEtq();
                            String etqFalsa = memoria.getEtq();

                            codigo += c3DIzq.codigo;
                            codigo += "if " + c3DIzq.temporal + "==1 goto " + etqVerdadera + ";" + "\r\n";
                            codigo += "goto " + etqFalsa + ";" + "\r\n";

                            c3DIzq.etqVerdadera = etqVerdadera;
                            c3DIzq.etqFalsa = etqFalsa;
                        }
                        else
                        {
                            codigo += c3DIzq.codigo;
                        }

                        codigo += c3DIzq.etqFalsa + ":" + "\r\n";

                        if (c3DDer.etqVerdadera == "" && c3DDer.etqFalsa == "")
                        {
                            String etqVerdadera = memoria.getEtq();
                            String etqFalsa = memoria.getEtq();

                            codigo += c3DDer.codigo;
                            codigo += "if " + c3DDer.temporal + "==1 goto " + etqVerdadera + ";" + "\r\n";
                            codigo += "goto " + etqFalsa + ";" + "\r\n";

                            c3DDer.etqVerdadera = etqVerdadera;
                            c3DDer.etqFalsa = etqFalsa;
                        }
                        else
                        {
                            codigo += c3DDer.codigo;
                        }
                        codigo += "//Fin logica de AND " + "\r\n\n";

                        temp.etqVerdadera = c3DDer.etqVerdadera;
                        temp.etqFalsa = c3DIzq.etqFalsa + ":\n" + c3DDer.etqFalsa;
                        temp.codigo = codigo;
                        temp.tipo = "BOOLEANO";
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " && " + expDer.tipo, fila, columna);
                        break;
                }
            }
            return temp;
        }

        public cadena3D resXor(expresion expIzq, expresion expDer)
        {
            String codigo = "";
            cadena3D temp = new cadena3D();
            cadena3D c3DIzq = resCondicion(expIzq);
            cadena3D c3DDer = resCondicion(expDer);

            if (c3DIzq.tipo.Equals("BOOLEANO"))
            {
                switch (c3DDer.tipo)
                {
                    case "BOOLEANO":

                        String etq1v = memoria.getEtq();
                        String etq1f = memoria.getEtq();
                        temp.etqVerdadera = memoria.getEtq();
                        temp.etqFalsa = memoria.getEtq();
                        String etq2f = memoria.getEtq();
                        String temp1 = memoria.getTemp();
                        String temp2 = memoria.getTemp();



                        codigo += "//Inicio logica de XOR " + "\r\n";
                        if (c3DIzq.etqVerdadera == "" && c3DIzq.etqFalsa == "")
                        {
                            String etqVerdadera = memoria.getEtq();
                            String etqFalsa = memoria.getEtq();

                            codigo += c3DIzq.codigo;
                            codigo += "if " + c3DIzq.temporal + "==1 goto " + etqVerdadera + ";" + "\r\n";
                            codigo += "goto " + etqFalsa + ";" + "\r\n";

                            c3DIzq.etqVerdadera = etqVerdadera;
                            c3DIzq.etqFalsa = etqFalsa;
                        }
                        else
                        {
                            codigo += c3DIzq.codigo;
                        }

                        codigo += c3DIzq.etqVerdadera + ":\r\n";
                        codigo += "\t" + temp1 + "=1;\r\n";
                        codigo += "\t" + "goto " + etq1v + ";\n";
                        codigo += c3DIzq.etqFalsa + ":\r\n";
                        codigo += "\t" + temp1 + "=0;\r\n";
                        codigo += etq1v + ":\r\n";


                        if (c3DDer.etqVerdadera == "" && c3DDer.etqFalsa == "")
                        {
                            String etqVerdadera = memoria.getEtq();
                            String etqFalsa = memoria.getEtq();

                            codigo += "\t" + c3DDer.codigo;
                            codigo += "\t" + "if " + c3DDer.temporal + "==1 goto " + etqVerdadera + ";" + "\r\n";
                            codigo += "\t" + "goto " + etqFalsa + ";" + "\r\n";

                            c3DDer.etqVerdadera = etqVerdadera;
                            c3DDer.etqFalsa = etqFalsa;
                        }
                        else
                        {
                            codigo += c3DDer.codigo;
                        }

                        codigo += c3DDer.etqVerdadera + ":\r\n";
                        codigo += "\t" + temp2 + "=1;\r\n";
                        codigo += "\t" + "goto " + etq1f + ";\r\n";
                        codigo += c3DDer.etqFalsa + ":\r\n";
                        codigo += "\t" + temp2 + "=0;\r\n";
                        codigo += etq1f + ":\r\n";
                        codigo += "\t" + "if " + temp1 + "!=" + temp2 + " goto " + temp.etqVerdadera + ";\r\n";
                        codigo += "\t" + "goto " + temp.etqFalsa + ";\r\n";
                        codigo += "//Fin logica de XOR " + "\r\n\n";

                        temp.etqVerdadera = c3DDer.etqVerdadera;
                        temp.etqFalsa = c3DIzq.etqFalsa + ":\n" + c3DDer.etqFalsa;
                        temp.codigo = codigo;
                        temp.tipo = "BOOLEANO";
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " ?? " + expDer.tipo, fila, columna);
                        break;
                }
            }
            return temp;
        }

        public cadena3D resNot(expresion expDer)
        {
            String codigo = "";
            cadena3D temp = new cadena3D();
            cadena3D c3DDer = resCondicion(expDer);

                switch (c3DDer.tipo)
                {
                    case "BOOLEANO":

                        codigo += "//Inicio logica de NOT " + "\r\n";
                        if (c3DDer.etqVerdadera == "" && c3DDer.etqFalsa == "")
                        {
                            String etqVerdadera = memoria.getEtq();
                            String etqFalsa = memoria.getEtq();

                            codigo += c3DDer.codigo;
                            codigo += "if " + c3DDer.temporal + "== 1 goto " + etqVerdadera + ";" + "\r\n";
                            codigo += "goto " + etqFalsa + ";" + "\r\n";

                            c3DDer.etqVerdadera = etqVerdadera;
                            c3DDer.etqFalsa = etqFalsa;
                        }
                        else
                        {
                            codigo += c3DDer.codigo;
                        }
                        codigo += "//Fin logica de NOT " + "\r\n\n";

                        temp.etqVerdadera = c3DDer.etqFalsa;
                        temp.etqFalsa = c3DDer.etqVerdadera;
                        temp.codigo = codigo;
                        temp.tipo = "BOOLEANO";
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", expIzq.tipo + " ! " + expDer.tipo, fila, columna);
                        break;
                }
            return temp;
        }

        public cadena3D resSuma(expresion expIzq, expresion expDer)
        {
            String codigo = "";
            cadena3D temp = new cadena3D();
            cadena3D c3DIzq = resCondicion(expIzq);
            cadena3D c3DDer = resCondicion(expDer);


            if (c3DIzq.tipo.Equals("ENTERO"))
            {
                switch (c3DDer.tipo)
                {
                    case "ENTERO":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "ENTERO";              
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "+" + c3DDer.temporal + "; //Suma entero entero" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    case "DECIMAL":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "DECIMAL";                       
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "+" + c3DDer.temporal + "; //Suma  entero decimal" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    case "CADENA":
                        {
                            cadena3D codigo_intToStr = intToSTR(c3DIzq.temporal);
                            temp.temporal = memoria.getTemp();
                            temp.tipo = "CADENA";

                            String tempCad1 = memoria.getTemp();
                            String tempCad2 = memoria.getTemp();
                            String tempHeap1 = memoria.getTemp();
                            String tempHeap2 = memoria.getTemp();
                            String etq1 = memoria.getEtq();
                            String etq2 = memoria.getEtq();
                            String etq3 = memoria.getEtq();


                            codigo += "//Inicio concatenacion de entero cadena" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo + codigo_intToStr.codigo + "\r\n\n";

                            codigo += temp.temporal + "=H; //Inicio de la nueva cadena" + "\r\n";
                            codigo += tempCad1 + "=" + codigo_intToStr.temporal + "; //Posicion Heap cadena 1" + "\r\n";
                            codigo += tempCad2 + "=" + c3DDer.temporal + "; //Posicion Heap cadena 2" + "\r\n\n";

                            codigo += etq1 + ": //Cadena 1" + "\r\n";
                            codigo += "\t" + tempHeap1 + "=" + "Heap[" + tempCad1 + "]; //Asignar ascii" + "\r\n";
                            codigo += "\t" + "if " + tempHeap1 + "==" + (int)memoria.finCadena + " goto " + etq2 + ";" + "\r\n";
                            codigo += "\t" + "Heap[H]=" + tempHeap1 + "; //Asignar en Heap ascii" + "\r\n";
                            codigo += "\t" + "H=H+1;" + "\r\n";
                            codigo += "\t" + tempCad1 + "=" + tempCad1 + "+1;" + "\r\n";
                            codigo += "\t" + "goto " + etq1 + ";" + "\r\n\n";


                            codigo += etq2 + ": //Cadena 2" + "\r\n";
                            codigo += "\t" + tempHeap2 + "=" + "Heap[" + tempCad2 + "]; //Asignar ascii" + "\r\n";
                            codigo += "\t" + "if " + tempHeap2 + "==" + (int)memoria.finCadena + " goto " + etq3 + ";" + "\r\n";
                            codigo += "\t" + "Heap[H]=" + tempHeap2 + "; //Asignar en Heap ascii" + "\r\n";
                            codigo += "\t" + "H=H+1;" + "\r\n";
                            codigo += "\t" + tempCad2 + "=" + tempCad2 + "+1;" + "\r\n";
                            codigo += "\t" + "goto " + etq2 + ";" + "\r\n\n";

                            codigo += etq3 + ":" + " \r\n";
                            codigo += "\t" + "Heap[H]=" + (int)memoria.finCadena + ";\r\n";
                            codigo += "\t" + "H=H+1;" + "\r\n";
                            codigo += "//Fin concatenacion de entero cadena" + "\r\n\n";

                            temp.codigo = codigo;
                        }
                        break;
                    case "BOOLEANO":
                        if (c3DDer.etqVerdadera == "" && c3DDer.etqFalsa == "")
                        {
                            temp.temporal = memoria.getTemp();
                            temp.tipo = "ENTERO";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += temp.temporal + "=" + c3DIzq.temporal + "+" + c3DDer.temporal + "; //Suma entero booleano" + "\r\n\n";
                            temp.codigo += codigo;
                        }
                        else {
                       
                            String tempo = memoria.getTemp();
                            String etq = memoria.getEtq();
                            temp.temporal = memoria.getTemp();

                            codigo += "//Inicio Suma entero booleano" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += c3DDer.etqVerdadera + ": \r\n";
                            codigo += "\t"+ tempo + "=1; \r\n";
                            codigo += "\t" + "goto " + etq + "; \r\n";
                            codigo +=  c3DDer.etqFalsa + ": \r\n";
                            codigo += "\t" + tempo + " = 0; \r\n";
                            codigo +=  etq + ": \r\n";       
                            codigo += "\t" + temp.temporal + " = " + c3DIzq.temporal + " + " + tempo + "; \r\n";
                            codigo += "//Fin Suma entero booleano" + "\r\n\n";

                            temp.codigo = codigo;
                            temp.tipo = "ENTERO";
                        }
                        break;
                    case "CARACTER":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "ENTERO";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "+" + c3DDer.temporal + "; //Suma entero caracter" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO", c3DIzq.tipo + " + " + c3DDer.tipo, fila, columna);
                        break;
                }
            }
            else if (c3DIzq.tipo.Equals("DECIMAL"))
            {
                switch (c3DDer.tipo)
                {
                    case "ENTERO":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "DECIMAL";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "+" + c3DDer.temporal + "; //Suma entero decimal" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    case "DECIMAL":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "DECIMAL";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "+" + c3DDer.temporal + "; //Suma decimal decimal" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    case "CADENA":
                        {
                        /*PENDIENTE*/
                        }
                        break;
                    case "BOOLEANO":
                        if (c3DDer.etqVerdadera == "" && c3DDer.etqFalsa == "")
                        {
                            temp.temporal = memoria.getTemp();
                            temp.tipo = "DECIMAL";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += temp.temporal + "=" + c3DIzq.temporal + "+" + c3DDer.temporal + "; //Suma decimal booleano" + "\r\n\n";
                            temp.codigo += codigo;
                        }
                        else
                        {

                            String tempo = memoria.getTemp();
                            String etq = memoria.getEtq();
                            temp.temporal = memoria.getTemp();

                            codigo += "//Inicio Suma decimal booleano" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += c3DDer.etqVerdadera + ": \r\n";
                            codigo += "\t" + tempo + "=1; \r\n";
                            codigo += "\t" + "goto " + etq + "; \r\n";
                            codigo += c3DDer.etqFalsa + ": \r\n";
                            codigo += "\t" + tempo + " = 0; \r\n";
                            codigo += etq + ": \r\n";
                            codigo += "\t" + temp.temporal + " = " + c3DIzq.temporal + " + " + tempo + "; \r\n";
                            codigo += "//Fin Suma decimal booleano" + "\r\n\n";

                            temp.codigo = codigo;
                            temp.tipo = "DECIMAL";
                        }
                        break;
                    case "CARACTER":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "DECIMAL";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "+" + c3DDer.temporal + "; //Suma decimal caracter" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO", c3DIzq.tipo + " + " + c3DDer.tipo, fila, columna);
                        break;
                }
            }

            else if (c3DIzq.tipo.Equals("CADENA"))
            {
                switch (c3DDer.tipo)
                {
                    case "ENTERO":
                        {
                            temp.temporal = memoria.getTemp();
                            temp.tipo = "CADENA";

                            String tempCad1 = memoria.getTemp();
                            String tempCad2 = memoria.getTemp();
                            String tempHeap1 = memoria.getTemp();
                            String tempHeap2 = memoria.getTemp();
                            String etq1 = memoria.getEtq();
                            String etq2 = memoria.getEtq();
                            String etq3 = memoria.getEtq();


                            codigo += "//Inicio concatenacion de cadena entero" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo + "\r\n\n";

                            codigo += temp.temporal + "=H; //Inicio de la nueva cadena" + "\r\n";
                            codigo += tempCad1 + "=" + c3DIzq.temporal + "; //Posicion Heap cadena 1" + "\r\n";
                            codigo += tempCad2 + "=" + c3DDer.temporal + "; //Posicion Heap cadena 2" + "\r\n\n";

                            codigo += etq1 + ": //Cadena 1" + "\r\n";
                            codigo += "\t" + tempHeap1 + "=" + "Heap[" + tempCad1 + "]; //Asignar ascii" + "\r\n";
                            codigo += "\t" + "if " + tempHeap1 + "==" + (int)memoria.finCadena + " goto " + etq2 + ";" + "\r\n";
                            codigo += "\t" + "Heap[H]=" + tempHeap1 + "; //Asignar en Heap ascii" + "\r\n";
                            codigo += "\t" + "H=H+1;" + "\r\n";
                            codigo += "\t" + tempCad1 + "=" + tempCad1 + "+1;" + "\r\n";
                            codigo += "\t" + "goto " + etq1 + ";" + "\r\n\n";


                            codigo += etq2 + ": //Cadena 2" + "\r\n";
                            codigo += "\t" + tempHeap2 + "=" + "Heap[" + tempCad2 + "]; //Asignar ascii" + "\r\n";
                            codigo += "\t" + "if " + tempHeap2 + "==" + (int)memoria.finCadena + " goto " + etq3 + ";" + "\r\n";
                            codigo += "\t" + "Heap[H]=" + tempHeap2 + "; //Asignar en Heap ascii" + "\r\n";
                            codigo += "\t" + "H=H+1;" + "\r\n";
                            codigo += "\t" + tempCad2 + "=" + tempCad2 + "+1;" + "\r\n";
                            codigo += "\t" + "goto " + etq2 + ";" + "\r\n\n";

                            codigo += etq3 + ":" + " \r\n";
                            codigo += "\t" + "Heap[H]=" + (int)memoria.finCadena + ";\r\n";
                            codigo += "\t" + "H=H+1;" + "\r\n";
                            codigo += "//Fin concatenacion de cadena entero" + "\r\n\n";

                            temp.codigo = codigo;
                        }
                        break;
                    case "DECIMAL":
                        /*PENDIENTE*/
                        break;
                    case "CADENA":
                        {
                            temp.temporal = memoria.getTemp();
                            temp.tipo = "CADENA";

                            String tempCad1 = memoria.getTemp();
                            String tempCad2 = memoria.getTemp();
                            String tempHeap1 = memoria.getTemp();
                            String tempHeap2 = memoria.getTemp();
                            String etq1 = memoria.getEtq();
                            String etq2 = memoria.getEtq();
                            String etq3 = memoria.getEtq();


                            codigo += "//Inicio concatenacion de cadena" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo + "\r\n\n";

                            codigo += temp.temporal + "=H; //Inicio de la nueva cadena" + "\r\n";
                            codigo += tempCad1 + "=" + c3DIzq.temporal + "; //Posicion Heap cadena 1" + "\r\n";
                            codigo += tempCad2 + "=" + c3DDer.temporal + "; //Posicion Heap cadena 2" + "\r\n\n";

                            codigo += etq1 + ": //Cadena 1" + "\r\n";
                            codigo += "\t" + tempHeap1 + "=" + "Heap[" +tempCad1+ "]; //Asignar ascii" + "\r\n";
                            codigo += "\t" + "if " + tempHeap1 + "==" + (int)memoria.finCadena + " goto " + etq2 + ";" + "\r\n";
                            codigo += "\t" + "Heap[H]=" + tempHeap1 + "; //Asignar en Heap ascii" + "\r\n";
                            codigo += "\t" + "H=H+1;" + "\r\n";
                            codigo += "\t" + tempCad1 + "=" + tempCad1 + "+1;" + "\r\n";
                            codigo += "\t" + "goto " + etq1 + ";" + "\r\n\n";


                            codigo += etq2 + ": //Cadena 2" + "\r\n";
                            codigo += "\t" + tempHeap2 + "=" + "Heap[" + tempCad2 + "]; //Asignar ascii" + "\r\n";
                            codigo += "\t" + "if " + tempHeap2 + "==" + (int)memoria.finCadena + " goto " + etq3 + ";" + "\r\n";
                            codigo += "\t" + "Heap[H]=" + tempHeap2 + "; //Asignar en Heap ascii" + "\r\n";
                            codigo += "\t" + "H=H+1;" + "\r\n";
                            codigo += "\t" + tempCad2 + "=" + tempCad2 + "+1;" + "\r\n";
                            codigo += "\t" + "goto " + etq2 + ";" + "\r\n\n";

                            codigo += etq3 + ":" + " \r\n";
                            codigo += "\t" + "Heap[H]=" + (int)memoria.finCadena + ";\r\n";
                            codigo += "\t" + "H=H+1;" + "\r\n";
                            codigo += "//Fin concatenacion de cadena" + "\r\n\n";

                            temp.codigo = codigo;
                        }
                        break;
                    case "CARACTER":
                        {
                            temp.temporal = memoria.getTemp();
                            temp.tipo = "CADENA";

                            String tempCad1 = memoria.getTemp();
                            String tempCad2 = memoria.getTemp();
                            String tempHeap1 = memoria.getTemp();
                            String tempHeap2 = memoria.getTemp();
                            String etq1 = memoria.getEtq();
                            String etq2 = memoria.getEtq();

                            codigo += "//Inicio concatenacion de cadena caracter" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo + "\r\n\n";

                            codigo += temp.temporal + "=H; //Inicio de la nueva cadena" + "\r\n";
                            codigo += tempCad1 + "=" + c3DIzq.temporal + "; //Posicion Heap cadena 1" + "\r\n";
                            codigo += tempCad2 + "=" + c3DDer.temporal + "; //Entero" + "\r\n\n";

                            codigo += etq1 + ": //Cadena 1" + "\r\n";
                            codigo += "\t" + tempHeap1 + "=" + "Heap[" + tempCad1 + "]; //Asignar ascii" + "\r\n";
                            codigo += "\t" + "if " + tempHeap1 + "==" + (int)memoria.finCadena + " goto " + etq2 + ";" + "\r\n";
                            codigo += "\t" + "Heap[H]=" + tempHeap1 + "; //Asignar en Heap ascii" + "\r\n";
                            codigo += "\t" + "H=H+1;" + "\r\n";
                            codigo += "\t" + tempCad1 + "=" + tempCad1 + "+1;" + "\r\n";
                            codigo += "\t" + "goto " + etq1 + ";" + "\r\n\n";


                            codigo += etq2 + ": //Concatenar entero" + "\r\n";
                            codigo += "\t" + "Heap[H]=" + tempCad2 + "; //Asignar en Heap ascii" + "\r\n";
                            codigo += "\t" + "H=H+1;" + "\r\n";
                            codigo += "\t" + "Heap[H]=" + (int)memoria.finCadena + ";\r\n";
                            codigo += "\t" + "H=H+1;" + "\r\n";
                            codigo += "//Fin concatenacion de cadena caracter" + "\r\n\n";

                            temp.codigo = codigo;
                        }
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO", c3DIzq.tipo + " + " + c3DDer.tipo, fila, columna);
                        break;
                }
            }

            else if (c3DIzq.tipo.Equals("BOOLEANO"))
            {
                switch (c3DDer.tipo)
                {
                    case "ENTERO":

                        if (c3DIzq.etqVerdadera == "" && c3DIzq.etqFalsa == "")
                        {
                            temp.temporal = memoria.getTemp();
                            temp.tipo = "ENTERO";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += temp.temporal + "=" + c3DIzq.temporal + "+" + c3DDer.temporal + "; //Suma booleano entero" + "\r\n\n";
                            temp.codigo += codigo;
                        }
                        else
                        {

                            String tempo = memoria.getTemp();
                            String etq = memoria.getEtq();
                            temp.temporal = memoria.getTemp();

                           
                            codigo += "//Inicio Suma booleano entero" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += c3DIzq.etqVerdadera + ": \r\n";
                            codigo += "\t" + tempo + "=1; \r\n";
                            codigo += "\t" + "goto " + etq + "; \r\n";
                            codigo += c3DIzq.etqFalsa + ": \r\n";
                            codigo += "\t" + tempo + " = 0; \r\n";
                            codigo += etq + ": \r\n";
                            codigo += "\t" + temp.temporal + " = " + tempo + " + " + c3DDer.temporal + "; \r\n";
                            codigo += "//Fin Suma booleano entero" + "\r\n\n";

                            temp.codigo =  codigo;
                            temp.tipo = "ENTERO";
                        }

                        break;
                    case "DECIMAL":
                        if (c3DIzq.etqVerdadera == "" && c3DIzq.etqFalsa == "")
                        {
                            temp.temporal = memoria.getTemp();
                            temp.tipo = "DECIMAL";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += temp.temporal + "=" + c3DIzq.temporal + "+" + c3DDer.temporal + "; //Suma booleano decimal" + "\r\n\n";
                            temp.codigo += codigo;
                        }
                        else
                        {

                            String tempo = memoria.getTemp();
                            String etq = memoria.getEtq();
                            temp.temporal = memoria.getTemp();


                            codigo += "//Inicio Suma booleano decimal" + "\r\n";
                            codigo +=  c3DIzq.codigo + c3DDer.codigo;
                            codigo += c3DIzq.etqVerdadera + ": \r\n";
                            codigo += "\t" + tempo + "=1; \r\n";
                            codigo += "\t" + "goto " + etq + "; \r\n";
                            codigo += c3DIzq.etqFalsa + ": \r\n";
                            codigo += "\t" + tempo + " = 0; \r\n";
                            codigo += etq + ": \r\n";
                            codigo += "\t" + temp.temporal + " = " + tempo + " + " + c3DDer.temporal + "; \r\n";
                            codigo += "//Fin Suma booleano decimal" + "\r\n\n";

                            temp.codigo = codigo;
                            temp.tipo = "DECIMAL";
                        }

                        break;
                    case "BOOLEANO":
                        if (c3DIzq.etqVerdadera == "" && c3DIzq.etqFalsa == "")
                        {
                            temp.temporal = memoria.getTemp();
                            temp.tipo = "ENTERO";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += temp.temporal + "=" + c3DIzq.temporal + "+" + c3DDer.temporal + "; //Suma booleano entero" + "\r\n\n";
                            temp.codigo += codigo;
                        }
                        else
                        {

                            String tempo = memoria.getTemp();
                            String etq = memoria.getEtq();
                            temp.temporal = memoria.getTemp();


                            codigo += "//Inicio Suma booleano booleano" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += c3DIzq.etqVerdadera + ": \r\n";
                            codigo += "\t" + tempo + "=1; \r\n";
                            codigo += "\t" + "goto " + etq + "; \r\n";
                            codigo += c3DIzq.etqFalsa + ": \r\n";
                            codigo += "\t" + tempo + " = 0; \r\n";
                            codigo += etq + ": \r\n";
                            codigo += "\t" + temp.temporal + " = " + tempo + " + " + c3DDer.temporal + "; \r\n";
                            codigo += "//Fin Suma booleano booleano" + "\r\n\n";

                            temp.codigo = codigo;
                            temp.tipo = "ENTERO";
                        }

                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO", c3DIzq.tipo + " + " + c3DDer.tipo, fila, columna);
                        break;
                }
            }


            else if (c3DIzq.tipo.Equals("CARACTER"))
            {
                switch (c3DDer.tipo)
                {
                    case "ENTERO":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "ENTERO";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "+" + c3DDer.temporal + "; //Suma caracter entero" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    case "DECIMAL":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "DECIMAL";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "+" + c3DDer.temporal + "; //Suma caracter decimal" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    case "CADENA":
                        {
                            temp.temporal = memoria.getTemp();
                            temp.tipo = "CADENA";

                            String tempCad2 = memoria.getTemp();
                            String tempCad1 = memoria.getTemp();                    
                            String tempHeap1 = memoria.getTemp();
                            String etq1 = memoria.getEtq();
                            String etq2 = memoria.getEtq();

                            codigo += "//Inicio concatenacion de caracter cadena" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo + "\r\n\n";

                            codigo += temp.temporal + "=H; //Inicio de la nueva cadena" + "\r\n";
                            codigo += tempCad2 + "=" + c3DIzq.temporal + "; //Entero" + "\r\n";
                            codigo += tempCad1 + "=" + c3DDer.temporal + "; //Posicion Heap cadena 1" + "\r\n\n";

                            codigo += "Heap[H]=" + tempCad2 + "; //Asignar en Heap ascii" + "\r\n";
                            codigo += "H=H+1;" + "\r\n\n";

                            codigo += etq1 + ": //Cadena 1" + "\r\n";
                            codigo += "\t" + tempHeap1 + "=" + "Heap[" + tempCad1 + "]; //Asignar ascii" + "\r\n";
                            codigo += "\t" + "if " + tempHeap1 + "==" + (int)memoria.finCadena + " goto " + etq2 + ";" + "\r\n";
                            codigo += "\t" + "Heap[H]=" + tempHeap1 + "; //Asignar en Heap ascii" + "\r\n";
                            codigo += "\t" + "H=H+1;" + "\r\n";
                            codigo += "\t" + tempCad1 + "=" + tempCad1 + "+1;" + "\r\n";
                            codigo += "\t" + "goto " + etq1 + ";" + "\r\n\n";

                            codigo += etq2 + ": //Concatenar entero" + "\r\n";
                            codigo += "\t" + "Heap[H]=" + (int)memoria.finCadena + ";\r\n";
                            codigo += "\t" + "H=H+1;" + "\r\n";
                            codigo += "//Fin concatenacion de caracter cadena" + "\r\n\n";
                            temp.codigo = codigo;
                        }
                        break;
                    case "CARACTER":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "ENTERO";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "+" + c3DDer.temporal + "; //Suma caracter caracter" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO", c3DIzq.tipo + " + " + c3DDer.tipo, fila, columna);
                        break;
                }
            }
            return temp;
        }

        public cadena3D resResta(expresion expDer)
        {
            String codigo = "";
            cadena3D temp = new cadena3D();
            cadena3D c3DDer = resCondicion(expDer);
            
            if (c3DDer.tipo.Equals("ENTERO"))
            {
                temp.temporal = memoria.getTemp();
                String tempMult = memoria.getTemp();
                temp.tipo = "ENTERO";
                codigo += c3DDer.codigo + "\r\n";
                codigo += tempMult + "=-1;" + "\r\n";
                codigo += temp.temporal + "=" + c3DDer.temporal + "*" +  tempMult + "; //Cambio de signo entero" + "\r\n\n";
                temp.codigo += codigo;
            }
            else if (c3DDer.tipo.Equals("DECIMAL"))
            {
                temp.temporal = memoria.getTemp();
                String tempMult = memoria.getTemp();
                temp.tipo = "DECIMAL";
                codigo += c3DDer.codigo + "\r\n";
                codigo += tempMult + "=-1;" + "\r\n";
                codigo += temp.temporal + "=" + c3DDer.temporal + "*" + tempMult + "; //Cambio de signo decimal" + "\r\n\n";
                temp.codigo += codigo;
            }
            else if (c3DDer.tipo.Equals("BOOLEANO"))
            {
                temp.temporal = memoria.getTemp();
                String tempMult = memoria.getTemp();
                temp.tipo = "ENTERO";
                codigo += c3DDer.codigo + "\r\n";
                codigo += tempMult + "=-1;" + "\r\n";
                codigo += temp.temporal + "=" + c3DDer.temporal + "*" + tempMult + "; //Cambio de signo booleano" + "\r\n\n";
                temp.codigo += codigo;
            }
            else if (c3DDer.tipo.Equals("CARACTER"))
            { 
                temp.temporal = memoria.getTemp();
                String tempMult = memoria.getTemp();
                temp.tipo = "ENTERO";
                codigo += c3DDer.codigo + "\r\n";
                codigo += tempMult + "=-1;" + "\r\n";
                codigo += temp.temporal + "=" + c3DDer.temporal + "*" + tempMult + "; //Cambio de signo caracter" + "\r\n\n";
                temp.codigo += codigo;
            }
            else
            {
                memoria.addError("ERROR SEMANTICO", " - " + c3DDer.tipo, fila, columna);
            }
            return temp;
        }

        public cadena3D resResta(expresion expIzq, expresion expDer)
        {
            String codigo = "";
            cadena3D temp = new cadena3D();
            cadena3D c3DIzq = resCondicion(expIzq);
            cadena3D c3DDer = resCondicion(expDer);

            if (c3DIzq.tipo.Equals("ENTERO"))
            {
                switch (c3DDer.tipo)
                {
                    case "ENTERO":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "ENTERO";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "-" + c3DDer.temporal + "; //Resta entero entero" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    case "DECIMAL":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "DECIMAL";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "-" + c3DDer.temporal + "; //Resta entero decimal" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    case "BOOLEANO":
                        if (c3DDer.etqVerdadera == "" && c3DDer.etqFalsa == "")
                        {
                            temp.temporal = memoria.getTemp();
                            temp.tipo = "ENTERO";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += temp.temporal + "=" + c3DIzq.temporal + "-" + c3DDer.temporal + "; //Resta entero booleano" + "\r\n\n";
                            temp.codigo += codigo;
                        }
                        else
                        {

                            String tempo = memoria.getTemp();
                            String etq = memoria.getEtq();
                            temp.temporal = memoria.getTemp();

                            codigo += "//Inicio Resta entero booleano" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += c3DDer.etqVerdadera + ": \r\n";
                            codigo += "\t" + tempo + "=1; \r\n";
                            codigo += "\t" + "goto " + etq + "; \r\n";
                            codigo += c3DDer.etqFalsa + ": \r\n";
                            codigo += "\t" + tempo + " = 0; \r\n";
                            codigo += etq + ": \r\n";
                            codigo += "\t" + temp.temporal + " = " + c3DIzq.temporal + " - " + tempo + "; \r\n";
                            codigo += "//Fin Resta entero booleano" + "\r\n\n";

                            temp.codigo = codigo;
                            temp.tipo = "ENTERO";
                        }
                        break;
                    case "CARACTER":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "ENTERO";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "-" + c3DDer.temporal + "; //Resta entero caracter" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", c3DIzq.tipo + " - " + c3DDer.tipo, fila, columna);
                        break;
                }
            }
            else if (c3DIzq.tipo.Equals("DECIMAL"))
            {
                switch (c3DDer.tipo)
                {
                    case "ENTERO":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "DECIMAL";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "-" + c3DDer.temporal + "; //Resta decimal entero" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    case "DECIMAL":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "DECIMAL";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "-" + c3DDer.temporal + "; //Resta decimal decimal" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    case "BOOLEANO":
                        if (c3DDer.etqVerdadera == "" && c3DDer.etqFalsa == "")
                        {
                            temp.temporal = memoria.getTemp();
                            temp.tipo = "DECIMAL";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += temp.temporal + "=" + c3DIzq.temporal + "-" + c3DDer.temporal + "; //Resta decimal booleano" + "\r\n\n";
                            temp.codigo += codigo;
                        }
                        else
                        {

                            String tempo = memoria.getTemp();
                            String etq = memoria.getEtq();
                            temp.temporal = memoria.getTemp();

                            codigo += "//Inicio Resta decimal booleano" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += c3DDer.etqVerdadera + ": \r\n";
                            codigo += "\t" + tempo + "=1; \r\n";
                            codigo += "\t" + "goto " + etq + "; \r\n";
                            codigo += c3DDer.etqFalsa + ": \r\n";
                            codigo += "\t" + tempo + " = 0; \r\n";
                            codigo += etq + ": \r\n";
                            codigo += "\t" + temp.temporal + " = " + c3DIzq.temporal + " - " + tempo + "; \r\n";
                            codigo += "//Fin Resta decimal booleano" + "\r\n\n";

                            temp.codigo = codigo;
                            temp.tipo = "DECIMAL";
                        }
                        break;
                    case "CARACTER":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "DECIMAL";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "-" + c3DDer.temporal + "; //Resta decimal caracter" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", c3DIzq.tipo + " - " + c3DDer.tipo, fila, columna);
                        break;
                }
            }
            else if (c3DIzq.tipo.Equals("BOOLEANO"))
            {
                switch (c3DDer.tipo)
                {
                    case "ENTERO":

                        if (c3DIzq.etqVerdadera == "" && c3DIzq.etqFalsa == "")
                        {
                            temp.temporal = memoria.getTemp();
                            temp.tipo = "ENTERO";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += temp.temporal + "=" + c3DIzq.temporal + "-" + c3DDer.temporal + "; //Resta booleano entero" + "\r\n\n";
                            temp.codigo += codigo;
                        }
                        else
                        {

                            String tempo = memoria.getTemp();
                            String etq = memoria.getEtq();
                            temp.temporal = memoria.getTemp();


                            codigo += "//Inicio Resta booleano entero" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += c3DIzq.etqVerdadera + ": \r\n";
                            codigo += "\t" + tempo + "=1; \r\n";
                            codigo += "\t" + "goto " + etq + "; \r\n";
                            codigo += c3DIzq.etqFalsa + ": \r\n";
                            codigo += "\t" + tempo + " = 0; \r\n";
                            codigo += etq + ": \r\n";
                            codigo += "\t" + temp.temporal + " = " + tempo + " - " + c3DDer.temporal + "; \r\n";
                            codigo += "//Fin Resta booleano entero" + "\r\n\n";

                            temp.codigo = codigo;
                            temp.tipo = "ENTERO";
                        }
                        break;
                    case "DECIMAL":
                        if (c3DIzq.etqVerdadera == "" && c3DIzq.etqFalsa == "")
                        {
                            temp.temporal = memoria.getTemp();
                            temp.tipo = "DECIMAL";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += temp.temporal + "=" + c3DIzq.temporal + "-" + c3DDer.temporal + "; //Resta booleano decimal" + "\r\n\n";
                            temp.codigo += codigo;
                        }
                        else
                        {

                            String tempo = memoria.getTemp();
                            String etq = memoria.getEtq();
                            temp.temporal = memoria.getTemp();


                            codigo += "//Inicio Resta booleano decimal" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += c3DIzq.etqVerdadera + ": \r\n";
                            codigo += "\t" + tempo + "=1; \r\n";
                            codigo += "\t" + "goto " + etq + "; \r\n";
                            codigo += c3DIzq.etqFalsa + ": \r\n";
                            codigo += "\t" + tempo + " = 0; \r\n";
                            codigo += etq + ": \r\n";
                            codigo += "\t" + temp.temporal + " = " + tempo + " - " + c3DDer.temporal + "; \r\n";
                            codigo += "//Fin Resta booleano decimal" + "\r\n\n";

                            temp.codigo = codigo;
                            temp.tipo = "DECIMAL";
                        }
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", c3DIzq.tipo + " - " + c3DDer.tipo, fila, columna);
                        break;
                }
            }
            else if (c3DIzq.tipo.Equals("CARACTER"))
            {
                switch (c3DDer.tipo)
                {
                    case "ENTERO":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "ENTERO";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "-" + c3DDer.temporal + "; //Resta caracter entero" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    case "DECIMAL":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "DECIMAL";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "-" + c3DDer.temporal + "; //Resta caracter decimal" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO", c3DIzq.tipo + " - " + c3DDer.tipo, fila, columna);
                        break;
                }
            }
            return temp;
        }

        public cadena3D resMultiplicacion(expresion expIzq, expresion expDer)
        {
            String codigo = "";
            cadena3D temp = new cadena3D();
            cadena3D c3DIzq = resCondicion(expIzq);
            cadena3D c3DDer = resCondicion(expDer);

            if (c3DIzq.tipo.Equals("ENTERO"))
            {
                switch (c3DDer.tipo)
                {
                    case "ENTERO":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "ENTERO";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "*" + c3DDer.temporal + "; //Multiplicacion entero entero" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    case "DECIMAL":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "DECIMAL";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "*" + c3DDer.temporal + "; //Multiplicacion entero decimal" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    case "BOOLEANO":
                        if (c3DDer.etqVerdadera == "" && c3DDer.etqFalsa == "")
                        {
                            temp.temporal = memoria.getTemp();
                            temp.tipo = "ENTERO";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += temp.temporal + "=" + c3DIzq.temporal + "*" + c3DDer.temporal + "; //Multiplicacion entero booleano" + "\r\n\n";
                            temp.codigo += codigo;
                        }
                        else
                        {

                            String tempo = memoria.getTemp();
                            String etq = memoria.getEtq();
                            temp.temporal = memoria.getTemp();

                            codigo += "//Inicio Multiplicacion entero booleano" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += c3DDer.etqVerdadera + ": \r\n";
                            codigo += "\t" + tempo + "=1; \r\n";
                            codigo += "\t" + "goto " + etq + "; \r\n";
                            codigo += c3DDer.etqFalsa + ": \r\n";
                            codigo += "\t" + tempo + " = 0; \r\n";
                            codigo += etq + ": \r\n";
                            codigo += "\t" + temp.temporal + " = " + c3DIzq.temporal + " * " + tempo + "; \r\n";
                            codigo += "//Fin Multiplicacion entero booleano" + "\r\n\n";

                            temp.codigo = codigo;
                            temp.tipo = "ENTERO";
                        }
                        break;
                    case "CARACTER":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "ENTERO";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "*" + c3DDer.temporal + "; //Multiplicacion entero caracter" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", c3DIzq.tipo + " * " + c3DDer.tipo, fila, columna);
                        break;
                }
            }
            else if (c3DIzq.tipo.Equals("DECIMAL"))
            {
                switch (c3DDer.tipo)
                {
                    case "ENTERO":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "DECIMAL";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "*" + c3DDer.temporal + "; //Multiplicacion decimal entero" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    case "DECIMAL":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "DECIMAL";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "*" + c3DDer.temporal + "; //Multiplicacion decimal decimal" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    case "BOOLEANO":
                        if (c3DDer.etqVerdadera == "" && c3DDer.etqFalsa == "")
                        {
                            temp.temporal = memoria.getTemp();
                            temp.tipo = "DECIMAL";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += temp.temporal + "=" + c3DIzq.temporal + "*" + c3DDer.temporal + "; //Multiplicacion decimal booleano" + "\r\n\n";
                            temp.codigo += codigo;
                        }
                        else
                        {

                            String tempo = memoria.getTemp();
                            String etq = memoria.getEtq();
                            temp.temporal = memoria.getTemp();

                            codigo += "//Inicio Multiplicacion decimal booleano" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += c3DDer.etqVerdadera + ": \r\n";
                            codigo += "\t" + tempo + "=1; \r\n";
                            codigo += "\t" + "goto " + etq + "; \r\n";
                            codigo += c3DDer.etqFalsa + ": \r\n";
                            codigo += "\t" + tempo + " = 0; \r\n";
                            codigo += etq + ": \r\n";
                            codigo += "\t" + temp.temporal + " = " + c3DIzq.temporal + " * " + tempo + "; \r\n";
                            codigo += "//Fin Multiplicacion booleano" + "\r\n\n";

                            temp.codigo = codigo;
                            temp.tipo = "DECIMAL";
                        }
                        break;
                    case "CARACTER":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "DECIMAL";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "*" + c3DDer.temporal + "; //Multiplicacion decimal caracter" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", c3DIzq.tipo + " * " + c3DDer.tipo, fila, columna);
                        break;
                }
            }
            else if (c3DIzq.tipo.Equals("BOOLEANO"))
            {
                switch (c3DDer.tipo)
                {
                    case "ENTERO":

                        if (c3DIzq.etqVerdadera == "" && c3DIzq.etqFalsa == "")
                        {
                            temp.temporal = memoria.getTemp();
                            temp.tipo = "ENTERO";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += temp.temporal + "=" + c3DIzq.temporal + "*" + c3DDer.temporal + "; //Multiplicacion booleano entero" + "\r\n\n";
                            temp.codigo += codigo;
                        }
                        else
                        {

                            String tempo = memoria.getTemp();
                            String etq = memoria.getEtq();
                            temp.temporal = memoria.getTemp();


                            codigo += "//Inicio Multiplicacion booleano entero" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += c3DIzq.etqVerdadera + ": \r\n";
                            codigo += "\t" + tempo + "=1; \r\n";
                            codigo += "\t" + "goto " + etq + "; \r\n";
                            codigo += c3DIzq.etqFalsa + ": \r\n";
                            codigo += "\t" + tempo + " = 0; \r\n";
                            codigo += etq + ": \r\n";
                            codigo += "\t" + temp.temporal + " = " + tempo + " * " + c3DDer.temporal + "; \r\n";
                            codigo += "//Fin Multiplicacion booleano entero" + "\r\n\n";

                            temp.codigo = codigo;
                            temp.tipo = "ENTERO";
                        }

                        break;
                    case "DECIMAL":
                        if (c3DIzq.etqVerdadera == "" && c3DIzq.etqFalsa == "")
                        {
                            temp.temporal = memoria.getTemp();
                            temp.tipo = "DECIMAL";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += temp.temporal + "=" + c3DIzq.temporal + "*" + c3DDer.temporal + "; //Multiplicacion booleano decimal" + "\r\n\n";
                            temp.codigo += codigo;
                        }
                        else
                        {

                            String tempo = memoria.getTemp();
                            String etq = memoria.getEtq();
                            temp.temporal = memoria.getTemp();


                            codigo += "//Inicio Multiplicacion booleano decimal" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += c3DIzq.etqVerdadera + ": \r\n";
                            codigo += "\t" + tempo + "=1; \r\n";
                            codigo += "\t" + "goto " + etq + "; \r\n";
                            codigo += c3DIzq.etqFalsa + ": \r\n";
                            codigo += "\t" + tempo + " = 0; \r\n";
                            codigo += etq + ": \r\n";
                            codigo += "\t" + temp.temporal + " = " + tempo + " * " + c3DDer.temporal + "; \r\n";
                            codigo += "//Fin Multiplicacion booleano decimal" + "\r\n\n";

                            temp.codigo = codigo;
                            temp.tipo = "DECIMAL";
                        }
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", c3DIzq.tipo + " * " + c3DDer.tipo, fila, columna);
                        break;
                }
            }
            else if (c3DIzq.tipo.Equals("CARACTER"))
            {
                switch (c3DDer.tipo)
                {
                    case "ENTERO":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "ENTERO";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "*" + c3DDer.temporal + "; //Multiplicacion caracter entero" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    case "DECIMAL":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "DECIMAL";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "*" + c3DDer.temporal + "; //Multiplicacion caracter decimal" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO", c3DIzq.tipo + " * " + c3DDer.tipo, fila, columna);
                        break;
                }
            }
            return temp;
        }

        public cadena3D resDivision(expresion expIzq, expresion expDer)
        {
            String codigo = "";
            cadena3D temp = new cadena3D();
            cadena3D c3DIzq = resCondicion(expIzq);
            cadena3D c3DDer = resCondicion(expDer);
            if (c3DIzq.tipo.Equals("ENTERO"))
            {
                switch (c3DDer.tipo)
                {
                    case "ENTERO":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "DECIMAL";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "/" + c3DDer.temporal + "; //Division entero entero" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    case "DECIMAL":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "DECIMAL";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "/" + c3DDer.temporal + "; //Division entero decimal" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    case "BOOLEANO":
                        if (c3DDer.etqVerdadera == "" && c3DDer.etqFalsa == "")
                        {
                            temp.temporal = memoria.getTemp();
                            temp.tipo = "DECIMAL";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += temp.temporal + "=" + c3DIzq.temporal + "/" + c3DDer.temporal + "; //Division entero booleano" + "\r\n\n";
                            temp.codigo += codigo;
                        }
                        else
                        {

                            String tempo = memoria.getTemp();
                            String etq = memoria.getEtq();
                            temp.temporal = memoria.getTemp();

                            codigo += "//Inicio Division entero booleano" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += c3DDer.etqVerdadera + ": \r\n";
                            codigo += "\t" + tempo + "=1; \r\n";
                            codigo += "\t" + "goto " + etq + "; \r\n";
                            codigo += c3DDer.etqFalsa + ": \r\n";
                            codigo += "\t" + tempo + " = 0; \r\n";
                            codigo += etq + ": \r\n";
                            codigo += "\t" + temp.temporal + " = " + c3DIzq.temporal + " / " + tempo + "; \r\n";
                            codigo += "//Fin Division entero booleano" + "\r\n\n";

                            temp.codigo = codigo;
                            temp.tipo = "DECIMAL";
                        }
                        break;
                    case "CARACTER":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "DECIMAL";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "/" + c3DDer.temporal + "; //Division entero caracter" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", c3DIzq.tipo + " / " + c3DDer.tipo, fila, columna);
                        break;
                }
            }
            else if (c3DIzq.tipo.Equals("DECIMAL"))
            {
                switch (c3DDer.tipo)
                {
                    case "ENTERO":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "DECIMAL";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "/" + c3DDer.temporal + "; //Division decimal entero" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    case "DECIMAL":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "DECIMAL";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "/" + c3DDer.temporal + "; //Division decimal decimal" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    case "BOOLEANO":
                        if (c3DDer.etqVerdadera == "" && c3DDer.etqFalsa == "")
                        {
                            temp.temporal = memoria.getTemp();
                            temp.tipo = "DECIMAL";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += temp.temporal + "=" + c3DIzq.temporal + "/" + c3DDer.temporal + "; //Division decimal booleano" + "\r\n\n";
                            temp.codigo += codigo;
                        }
                        else
                        {

                            String tempo = memoria.getTemp();
                            String etq = memoria.getEtq();
                            temp.temporal = memoria.getTemp();

                            codigo += "//Inicio Division decimal booleano" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += c3DDer.etqVerdadera + ": \r\n";
                            codigo += "\t" + tempo + "=1; \r\n";
                            codigo += "\t" + "goto " + etq + "; \r\n";
                            codigo += c3DDer.etqFalsa + ": \r\n";
                            codigo += "\t" + tempo + " = 0; \r\n";
                            codigo += etq + ": \r\n";
                            codigo += "\t" + temp.temporal + " = " + c3DIzq.temporal + " / " + tempo + "; \r\n";
                            codigo += "//Fin Division decimal booleano" + "\r\n\n";

                            temp.codigo = codigo;
                            temp.tipo = "DECIMAL";
                        }
                        break;
                    case "CARACTER":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "DECIMAL";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "/" + c3DDer.temporal + "; //Division decimal caracter" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", c3DIzq.tipo + " / " + c3DDer.tipo, fila, columna);
                        break;
                }
            }
            else if (c3DIzq.tipo.Equals("BOOLEANO"))
            {
                switch (c3DDer.tipo)
                {
                    case "ENTERO":

                        if (c3DIzq.etqVerdadera == "" && c3DIzq.etqFalsa == "")
                        {
                            temp.temporal = memoria.getTemp();
                            temp.tipo = "DECIMAL";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += temp.temporal + "=" + c3DIzq.temporal + "/" + c3DDer.temporal + "; //Division booleano entero" + "\r\n\n";
                            temp.codigo += codigo;
                        }
                        else
                        {

                            String tempo = memoria.getTemp();
                            String etq = memoria.getEtq();
                            temp.temporal = memoria.getTemp();


                            codigo += "//Inicio Division booleano entero" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += c3DIzq.etqVerdadera + ": \r\n";
                            codigo += "\t" + tempo + "=1; \r\n";
                            codigo += "\t" + "goto " + etq + "; \r\n";
                            codigo += c3DIzq.etqFalsa + ": \r\n";
                            codigo += "\t" + tempo + " = 0; \r\n";
                            codigo += etq + ": \r\n";
                            codigo += "\t" + temp.temporal + " = " + tempo + " / " + c3DDer.temporal + "; \r\n";
                            codigo += "//Fin Division booleano entero" + "\r\n\n";

                            temp.codigo = codigo;
                            temp.tipo = "DECIMAL";
                        }

                        break;
                    case "DECIMAL":
                        if (c3DIzq.etqVerdadera == "" && c3DIzq.etqFalsa == "")
                        {
                            temp.temporal = memoria.getTemp();
                            temp.tipo = "DECIMAL";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += temp.temporal + "=" + c3DIzq.temporal + "/" + c3DDer.temporal + "; //Division booleano decimal" + "\r\n\n";
                            temp.codigo += codigo;
                        }
                        else
                        {

                            String tempo = memoria.getTemp();
                            String etq = memoria.getEtq();
                            temp.temporal = memoria.getTemp();


                            codigo += "//Inicio Division booleano decimal" + "\r\n";
                            codigo += c3DIzq.codigo + c3DDer.codigo;
                            codigo += c3DIzq.etqVerdadera + ": \r\n";
                            codigo += "\t" + tempo + "=1; \r\n";
                            codigo += "\t" + "goto " + etq + "; \r\n";
                            codigo += c3DIzq.etqFalsa + ": \r\n";
                            codigo += "\t" + tempo + " = 0; \r\n";
                            codigo += etq + ": \r\n";
                            codigo += "\t" + temp.temporal + " = " + tempo + " / " + c3DDer.temporal + "; \r\n";
                            codigo += "//Fin Division booleano decimal" + "\r\n\n";

                            temp.codigo = codigo;
                            temp.tipo = "DECIMAL";
                        }
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", c3DIzq.tipo + " / " + c3DDer.tipo, fila, columna);
                        break;
                }
            }
            else if (c3DIzq.tipo.Equals("CARACTER"))
            {
                switch (c3DDer.tipo)
                {
                    case "ENTERO":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "DECIMAL";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "/" + c3DDer.temporal + "; //Division caracter entero" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    case "DECIMAL":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "DECIMAL";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "/" + c3DDer.temporal + "; //Division caracter decimal" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO", c3DIzq.tipo + " - " + c3DDer.tipo, fila, columna);
                        break;
                }
            }
            return temp;
        }

        public cadena3D resPotencia(expresion expIzq, expresion expDer)
        {
            String codigo = "";
            cadena3D temp = new cadena3D();
            cadena3D c3DIzq = resCondicion(expIzq);
            cadena3D c3DDer = resCondicion(expDer);
            if (c3DIzq.tipo.Equals("ENTERO"))
            {
                switch (c3DDer.tipo)
                {
                    case "ENTERO":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "ENTERO";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "^" + c3DDer.temporal + "; //Potencia entero entero" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    case "DECIMAL":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "DECIMAL";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "^" + c3DDer.temporal + "; //Potencia entero decimal" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    case "BOOLEANO":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "ENTERO";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "^" + c3DDer.temporal + "; //Potencia entero booleano" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    case "CARACTER":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "ENTERO";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "^" + c3DDer.temporal + "; //Potencia entero caracter" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", c3DIzq.tipo + " ^ " + c3DDer.tipo, fila, columna);
                        break;
                }
            }
            else if (c3DIzq.tipo.Equals("DECIMAL"))
            {
                switch (c3DDer.tipo)
                {
                    case "ENTERO":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "DECIMAL";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "^" + c3DDer.temporal + "; //Potencia decimal entero" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    case "DECIMAL":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "DECIMAL";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "^" + c3DDer.temporal + "; //Potencia decimal decimal" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    case "BOOLEANO":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "DECIMAL";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "^" + c3DDer.temporal + "; //Potencia decimal booleano" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    case "CARACTER":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "DECIMAL";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "^" + c3DDer.temporal + "; //Potencia decimal caracter" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", c3DIzq.tipo + " ^ " + c3DDer.tipo, fila, columna);
                        break;
                }
            }
            else if (c3DIzq.tipo.Equals("BOOLEANO"))
            {
                switch (c3DDer.tipo)
                {
                    case "ENTERO":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "ENTERO";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "^" + c3DDer.temporal + "; //Potencia booleano entero" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    case "DECIMAL":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "DECIMAL";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "^" + c3DDer.temporal + "; //Potencia booleano decimal" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO ", c3DIzq.tipo + " ^ " + c3DDer.tipo, fila, columna);
                        break;
                }
            }
            else if (c3DIzq.tipo.Equals("CARACTER"))
            {
                switch (c3DDer.tipo)
                {
                    case "ENTERO":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "ENTERO";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "^" + c3DDer.temporal + "; //Potencia caracter entero" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    case "DECIMAL":
                        temp.temporal = memoria.getTemp();
                        temp.tipo = "DECIMAL";
                        codigo += c3DIzq.codigo + c3DDer.codigo;
                        codigo += temp.temporal + "=" + c3DIzq.temporal + "^" + c3DDer.temporal + "; //Potencia caracter decimal" + "\r\n\n";
                        temp.codigo += codigo;
                        break;
                    default:
                        memoria.addError("ERROR SEMANTICO", c3DIzq.tipo + " - " + c3DDer.tipo, fila, columna);
                        break;
                }
            }
            return temp;
        }

        public cadena3D resAumento(expresion expIzq)
        {
            String codigo = "";
            cadena3D temp = new cadena3D();
            cadena3D c3DIzq = resCondicion(expIzq);
            if (c3DIzq.tipo.Equals("ENTERO"))
            {
                temp.temporal = memoria.getTemp();
                String tempMult = memoria.getTemp();
                temp.tipo = "ENTERO";
                codigo += c3DIzq.codigo + "\r\n";
                codigo += tempMult + "=1;" + "\r\n";
                codigo += temp.temporal + "=" + c3DIzq.temporal + "-" + tempMult + "; //Decremento entero" + "\r\n\n";
                temp.codigo += codigo;
            }
            else if (c3DIzq.tipo.Equals("DECIMAL"))
            {
                temp.temporal = memoria.getTemp();
                String tempMult = memoria.getTemp();
                temp.tipo = "DECIMAL";
                codigo += c3DIzq.codigo + "\r\n";
                codigo += tempMult + "=1;" + "\r\n";
                codigo += temp.temporal + "=" + c3DIzq.temporal + "-" + tempMult + "; //Decremento decimal" + "\r\n\n";
                temp.codigo += codigo;
            }
            else if (c3DIzq.tipo.Equals("CARACTER"))
            {
                temp.temporal = memoria.getTemp();
                String tempMult = memoria.getTemp();
                temp.tipo = "ENTERO";
                codigo += c3DIzq.codigo + "\r\n";
                codigo += tempMult + "=1;" + "\r\n";
                codigo += temp.temporal + "=" + c3DIzq.temporal + "-" + tempMult + "; //Decremento caracter" + "\r\n\n";
                temp.codigo += codigo;
            }
            else
            {
                memoria.addError("ERROR SEMANTICO ", c3DIzq.tipo + " ++ ", fila, columna);
            }
            return temp;
        }

        public cadena3D resDecremento(expresion expIzq)
        {
            String codigo = "";
            cadena3D temp = new cadena3D();
            cadena3D c3DIzq = resCondicion(expIzq);
            if (c3DIzq.tipo.Equals("ENTERO"))
            {
                temp.temporal = memoria.getTemp();
                String tempMult = memoria.getTemp();
                temp.tipo = "ENTERO";
                codigo += c3DIzq.codigo + "\r\n";
                codigo += tempMult + "=1;" + "\r\n";
                codigo += temp.temporal + "=" + c3DIzq.temporal + "+" + tempMult + "; //Aumento entero" + "\r\n\n";
                temp.codigo += codigo;
            }
            else if (c3DIzq.tipo.Equals("DECIMAL"))
            {
                temp.temporal = memoria.getTemp();
                String tempMult = memoria.getTemp();
                temp.tipo = "DECIMAL";
                codigo += c3DIzq.codigo + "\r\n";
                codigo += tempMult + "=1;" + "\r\n";
                codigo += temp.temporal + "=" + c3DIzq.temporal + "+" + tempMult + "; //Aumento decimal" + "\r\n\n";
                temp.codigo += codigo;
            }
            else if (c3DIzq.tipo.Equals("CARACTER"))
            {
                temp.temporal = memoria.getTemp();
                String tempMult = memoria.getTemp();
                temp.tipo = "ENTERO";
                codigo += c3DIzq.codigo + "\r\n";
                codigo += tempMult + "=1;" + "\r\n";
                codigo += temp.temporal + "=" + c3DIzq.temporal + "+" + tempMult + "; //Aumento caracter" + "\r\n\n";
                temp.codigo += codigo;
            }
            else
            {
                memoria.addError("ERROR SEMANTICO ", c3DIzq.tipo + " -- ", fila, columna);
            }
            return temp;
        }

        private cadena3D intToSTR(String entero)
        {
            String codigo = "";
            cadena3D temp = new cadena3D();
            String temp0 = memoria.getTemp();
            String temp1 = memoria.getTemp();
            String temp2 = memoria.getTemp();
            String temp3 = memoria.getTemp();
            String temp4 = memoria.getTemp();
            String temp5 = memoria.getTemp();
            String temp6 = memoria.getTemp();
            String temp7 = memoria.getTemp();
            String etq1 = memoria.getEtq();
            String etq2 = memoria.getEtq();
            String etq3 = memoria.getEtq();
            String etq4 = memoria.getEtq();
            String etq5 = memoria.getEtq();
            String etq6 = memoria.getEtq();
            String etq7 = memoria.getEtq();
            String etq8 = memoria.getEtq();
            String etq9 = memoria.getEtq();
            String etq10 = memoria.getEtq();
            String etq11 = memoria.getEtq();
            String etq12 = memoria.getEtq();
            /***************************************/
            codigo += "\n//Inicio intToStr " + "\r\n";
            codigo += "//Inicio intToStr " + "\r\n";
            codigo += temp0 + "=" + entero + ";//Entero a cadena" +"\r\n";
            codigo += temp1 + " = " + "1;" + "\r\n";
            codigo += "if " + temp0 + " >= 0 goto " + etq1 + ";" + "\r\n";
            codigo += temp1 + " = -1;" + "\r\n";
            codigo += temp0 + " = " + temp0 + " * -1;" + "\r\n";
            codigo += etq1 + ":" + "\r\n";
            codigo += "\t" + temp2 + " = 1; //Tamanio del numero" + "\r\n"; 
            codigo += etq3 + ":" + "\r\n";
            codigo += "\t" + temp3 + " = 1;" + "\r\n"; 
            codigo += etq4 + ":" + "\r\n";
            codigo += "\t" + temp4 + " = " + temp2 + " * " + temp3 + ";" + "\r\n";
            codigo += "\t" + "if " + temp3 + " > 10 goto " + etq5 + ";" + "\r\n";
            codigo += "\t" + "if " + temp0 + " < " + temp4 + " goto " + etq2 + ";" + "\r\n";
            codigo += "\t" + temp3 + " = " + temp3 + " + 1;" + "\r\n";
            codigo += "\t" + "goto " + etq4 + ";" + "\r\n";
            codigo += etq5 + ":" + "\r\n";
            codigo += "\t" + temp2 + " = " + temp2 + " * 10;" + "\r\n";
            codigo += "\t" + "goto " + etq3 + ";" + "\r\n\n";
            /***************************************/
            codigo += etq2 + ":" + "\r\n";
            codigo += "\t" + temp5 + " = H; //Posicion del Heap de intToStr" + "\r\n";
            codigo += "\t" + "if " + temp1 + " == 1 goto " + etq6 + ";" + "\r\n";
            codigo += "\t" + "Heap[H] = 45;" + "\r\n";
            codigo += "\t" + "H = H + 1;" + "\r\n";
            codigo += etq6 + ":" + "\r\n";
            codigo += "\t" + temp3 + " = " + temp3 + " - 1;" + "\r\n";
            codigo += "\t" + temp6 + " = 0;" + "\r\n";
            codigo += "\t" + temp7 + "= 48;" + "\r\n";
            codigo += etq7 + ":" + "\r\n";
            codigo += "\t" + "if " + temp6 + " == " + temp3 + " goto " + etq8 + ";" + "\r\n";
            codigo += "\t" + temp6 + " = " + temp6 + " + 1;" + "\r\n";
            codigo += "\t" + temp7 + " = " + temp7 + " + 1;" + "\r\n";
            codigo += "\t" + "goto " + etq7 + ";" + "\r\n\n";
            /***************************************/
            codigo += etq8 + ":" + "\r\n";
            codigo += "\t" + "Heap[H] = " + temp7 + ";" + "\r\n";
            codigo += "\t" + "H = H + 1;" + "\r\n\n";
            /***************************************/
            codigo += "\t" + "if " + temp2 + " == 1 goto " + etq9 + ";" + "\r\n";
            codigo += "\t" + temp4 + " = " + temp2 + " * " + temp3 + ";" + "\r\n";
            codigo += "\t" + temp0 + " = " + temp0 + " - " + temp4 + ";" + "\r\n";
            codigo += "\t" + temp2 + " = " + temp2 + " / 10;" + "\r\n\n";
            /***************************************/
            codigo += etq10 + ":" + "\r\n";
            codigo += "\t" + temp3 + " = 1;" + "\r\n"; 
            codigo += etq11 + ":" + "\r\n";
            codigo += "\t" + temp4 + " = " + temp2 + " * " + temp3 + ";" + "\r\n";
            codigo += "\t" + "if " + temp3 + " > 10 goto " + etq12 + ";" + "\r\n";
            codigo += "\t" + "if " + temp0 + " < " + temp4 + " goto " + etq6 + ";" + "\r\n";
            codigo += "\t" + temp3 + " = " + temp3 + " + 1;" + "\r\n";
            codigo += "\t" + "goto " + etq11 + ";" + "\r\n";
            codigo += etq12 + ":" + "\r\n";
            codigo += "\t" + temp2 + " = " + temp2 + " * 10;" + "\r\n";
            codigo += "\t" + "goto " + etq10 + ";" + "\r\n\n";
            /***************************************/
            codigo += etq9 + ":" + "\r\n";
            codigo += "\t" + "Heap[H] = 0;" + "\r\n";
            codigo += "//Fin intToStr " + "\r\n";
            codigo += "//Fin intToStr " + "\r\n\n";
            /***************************************/
            temp.codigo = codigo;
            temp.tipo = "CADENA";
            temp.temporal = temp5;
            return temp;
        }
    }
}
