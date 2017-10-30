using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.ejecucion_alto_nivel
{
    class generar3D
    {
        String codigo = "";
        public generar3D() {
            foreach (estructura_clase estructura_clase in memoria.lista_estructura_clase) {
                pasadas pasadas = new pasadas(estructura_clase.ambito.tablaSimbolo);
                codigo += pasadas.ejecutar();
            }
            memoria.cadena3D = codigo;
        }
        
    }
}
