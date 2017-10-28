using _Compi2_Proyecto2_201212859.codigo3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.ejecucion_alto_nivel
{
    class pasadas
    {
        List<simbolo> tablaSimbolo;

        public pasadas(List<simbolo> tablaSimbolo) {
            this.tablaSimbolo = tablaSimbolo;
        }

        public pasadas() {
            this.tablaSimbolo = new List<simbolo>();
        }

        public void ejecutar()
        {
            for (int i =0; i<tablaSimbolo.Count(); i++)
            {
                if (tablaSimbolo[i].rol.Equals("ESTRUCTURA_CLASE"))
                {
                    estructura_clase estructura_clase = (estructura_clase)tablaSimbolo[i].valor;
                    estructura_clase.generar3D();
                }
                else if (tablaSimbolo[i].rol.Equals("CLASE"))
                {
                    clase clase = (clase)tablaSimbolo[i].valor;
                    clase.generar3D();
                }
            
                else if (tablaSimbolo[i].rol.Equals("DECLARACION"))
                {
                    declaracion declaracion = (declaracion)tablaSimbolo[i].valor;
                    declaracion.generar3D();
                }
                else if (tablaSimbolo[i].rol.Equals("METODO"))
                {
                    metodo metodo = (metodo)tablaSimbolo[i].valor;
                    metodo.generar3D();
                }
                else if (tablaSimbolo[i].rol.Equals("CONSTRUCTOR"))
                {
                    metodo metodo = (metodo)tablaSimbolo[i].valor;
                    metodo.generar3D();
                }
                else if (tablaSimbolo[i].rol.Equals("MIENTRAS"))
                {
                    mientras metodo = (mientras)tablaSimbolo[i].valor;
                    metodo.generar3D();
                }
                else if (tablaSimbolo[i].rol.Equals("ASIGNACION"))
                {
                    asignacion asignacion = (asignacion)tablaSimbolo[i].valor;
                    asignacion.generar3D();
                }
            }
        }
    }
}
