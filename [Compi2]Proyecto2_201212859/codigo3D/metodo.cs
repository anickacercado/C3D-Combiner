using System;
using System.Collections.Generic;
using _Compi2_Proyecto2_201212859.ejecucion_alto_nivel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class metodo
    {
        public String visibilidad;
        public String tipo;
        public String nombre;
        public List<simbolo> parametros;
        public int dimensiones;
        public ambito ambito;
        public int fila;
        public int columna;
        public simbolo padre = null;

        public metodo(String visibilidad, String tipo, int dimensiones, String nombre, List<simbolo> parametros, ambito ambito, int fila, int columna)
        {
            this.visibilidad = visibilidad;
            this.tipo = tipo;
            this.dimensiones = dimensiones;
            this.parametros = parametros;
            this.ambito = ambito;
            this.fila = fila;
            this.columna = columna;

            this.nombre = nombre_metodo(parametros, nombre);
        }

        private String nombre_metodo(List<simbolo> simbolo_parametro, String nombre)
        {
            foreach (simbolo simbolo in simbolo_parametro)
            {
                parametro parametro = (parametro)simbolo.valor;
                nombre = nombre + "_" + parametro.tipo;
            }
            return nombre;
        }

        public String generar3D() {
            String codigo = "";
           
            codigo += "void " + nombre + "(){\n";

            pasadas pasadas = new pasadas(ambito.tablaSimbolo);
            codigo += memoria.identar(pasadas.ejecutar());
            String etq = memoria.getEtq();
            codigo += memoria.identar(etq + ":\r\n");

            codigo += "}" + "\r\n\n";

            codigo = codigo.Replace("~RETORNAR~", "goto " + etq + ";");
            return codigo;
        }
    }
}
