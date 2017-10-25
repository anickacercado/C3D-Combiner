using _Compi2_Proyecto2_201212859.codigo3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859
{
    class memoria
    {
        /*GUI*/
        public static string pathProyecto= "C:\\Proyecto2\\";
        public static String ruta = "";         //Ruta del archivo que se esta analizando

        /*Codigo 3D*/
        public static int temporal;
        public static String cadena3D;

        public static String getTemp() {
            memoria.temporal++;
            String temp = "t" + memoria.temporal.ToString();
            return temp;
        }

        public static List<estructura_clase> lista_estructura_clase = new List<estructura_clase>();

        public static void addError(String tipo, String descripcion, int linea, int columna)
        {
            principal.insertarError(linea.ToString(), columna.ToString(), tipo, descripcion, memoria.ruta);
        }
    }
}
