using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class pasadas
    {
        List<simbolo> tablaSimbolo;

        public pasadas(List<simbolo> tablaSimbolo) {
            this.tablaSimbolo = tablaSimbolo;
        }

        public void ejecutar()
        {
            for (int i =0; i<tablaSimbolo.Count(); i++) {
                if (tablaSimbolo[i].nombre.Equals("ASIGNACION")) {
                    asignacion asignacion = (asignacion)tablaSimbolo[i].valor;
                    asignacion.generar3D();
                }

            }
        }


    }
}
