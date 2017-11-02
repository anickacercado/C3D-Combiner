using _Compi2_Proyecto2_201212859.codigo3D;
using _Compi2_Proyecto2_201212859.ejecucion_alto_nivel;
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
        public static int temporal = 0;
        public static int etiqueta = 0;
        public static String cadena3D = "";
        public static Char fin_cadena = '\0';
        public static String inicia_variable = "-143638293";

        public static String getTemp() {    
            String temp = "t" + memoria.temporal.ToString();
            memoria.temporal++;
            return temp;
        }

        public static String getEtq()
        {      
            String temp = "l" + memoria.etiqueta.ToString();
            memoria.etiqueta++;
            return temp;
        }
        
        public static String reemplazar(String codigo, String salir, String continuar) {
            codigo = codigo.Replace("~SALIR~", salir);
            codigo = codigo.Replace("~CONTINUAR~", continuar);
            return codigo;
        }

        public static String identar(String codigo){
            String cadena_retorno = "";
            String[] arregloString = codigo.Split('\n');
            for (int x = 0; x < arregloString.Count(); x++){
                cadena_retorno += "\t" + arregloString[x] + "\n";
            }
            return cadena_retorno;
        }


        public static List<estructura_clase> lista_estructura_clase = new List<estructura_clase>();

        public static void addError(String tipo, String descripcion, int linea, int columna)
        {
            principal.insertarError(linea.ToString(), columna.ToString(), tipo, descripcion, memoria.ruta);
        }
    }
}
