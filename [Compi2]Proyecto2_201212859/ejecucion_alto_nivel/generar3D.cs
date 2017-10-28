using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.ejecucion_alto_nivel
{
    class generar3D
    {
        pasadas pasadas = new pasadas();
        public generar3D() {
            foreach (estructura_clase estructura_clase in memoria.lista_estructura_clase) {
                pasadas = new pasadas(estructura_clase.ambito.tablaSimbolo);
                pasadas.ejecutar();
            }
        }
    }
}
