using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class para
    {
        public simbolo declara_asigna;
        public expresion valor;
        public simbolo decre_aum;
        public ambito ambito;
        public simbolo padre = null;

        public para(simbolo declara_asigna, expresion valor, simbolo decre_aum, ambito ambito)
        {
            this.declara_asigna = declara_asigna;
            this.valor = valor;
            this.decre_aum = decre_aum;
            this.ambito = ambito;
        }

        public void cod3D() {
        }
    }
}
