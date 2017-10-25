using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class super
    {
        public List<expresion> parametros;
        public simbolo padre = null;

        public super(List<expresion> parametros) {
            this.parametros = parametros;
        }
    }
}
