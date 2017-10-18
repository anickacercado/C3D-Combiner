using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class clase
    { 
        public String nombre;
        public List<String> imports = new List<String>();
        public String herencia;
        public ambito ambito;


        public clase(String nombre, List<String> imports, String herencia, ambito ambito) {
            this.nombre = nombre;
            this.imports = imports;
            this.ambito = ambito;
        }
    }
}
