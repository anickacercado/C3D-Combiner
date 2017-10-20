using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class ambito
    {
        public String nombre;
        public List<simbolo> tablaSimbolo;
        public ambito padre;
        public int tamanio;

        public ambito(String nombre, List<simbolo> tablaSimbolo)
        {
            this.nombre = nombre;
            this.tablaSimbolo = tablaSimbolo;
            this.padre = null;
            set_tamanio(tablaSimbolo);
            
        }

        public ambito(String nombre)
        {
           this.nombre = nombre;
           this.tablaSimbolo = new List<simbolo>();
        }


        public void set_tamanio(List<simbolo> tablaSimbolo)
        {
            foreach (simbolo simbolo in tablaSimbolo)
            {
                    this.tamanio += simbolo.tamanio;
            }
        }
    }
}
