using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class si
    {
        public expresion expresion;
        public ambito ambito;
        public List<sino_si> lista_sino_si;
        public sino sino;
        public simbolo padre = null;

        public si(expresion expresion, ambito ambito, List<sino_si> lista_sino_si, sino sino) {
            this.expresion = expresion;
            this.ambito = ambito;
            this.lista_sino_si = lista_sino_si;
            this.sino = sino;
        }
    }
}
