using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class elegir
    {
        public expresion expresion;
        public List<caso> lista_caso;
        public caso defecto;
        public ambito ambito;
        public simbolo padre =  null;

        public elegir(expresion expresion, List<caso> lista_caso, caso defecto, ambito ambito) {
            this.expresion = expresion;
            this.lista_caso = lista_caso;
            this.defecto = defecto;
            this.ambito = ambito;
        }

    }
}
