using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class cadena3D
    {
        public String valor;        //temporal, valor
        public String codigo;       //codigo3D
        public String tipo;         //entero... etc
        public String etqVerdadera; //Etiqueta verdadera
        public String etqFalsa;     //Etiqueta falsa

        public cadena3D(String valor, String codigo, String tipo, String etqVerdadera, String etqFalsa)
        {
            this.valor = valor;
            this.codigo = codigo;
            this.tipo = tipo;
            this.etqVerdadera = etqVerdadera;
            this.etqFalsa = etqFalsa;
        }
    }
}