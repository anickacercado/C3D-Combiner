using _Compi2_Proyecto2_201212859.codigo3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.ejecucion_alto_nivel
{
    class estructura_clase
    {
        public List<String> importar;
        public ambito ambito;
        public String ruta;


        public estructura_clase(List<String> importar, ambito ambito, String ruta) {
            this.importar = importar;
            this.ambito = ambito;
            this.ruta = ruta;
        }

        public String generar3D()
        {
            String codigo = "";
            pasadas pasadas = new pasadas(ambito.tablaSimbolo);
            codigo += pasadas.ejecutar();
            return codigo;
        }

        public void generar_tabla_simbolo(simbolo simbolo, string nombre_ambito) {
            switch (simbolo.rol)
            {
                case "CLASE":
                    if (simbolo.tamanio > 0)
                    {
                        principal.insertarTablaSimbolo(simbolo.nombre, "", simbolo.rol, simbolo.visibilidad, nombre_ambito, simbolo.tamanio, simbolo.posicion);
                        foreach (simbolo simbolo_hijo in simbolo.ambito.tablaSimbolo)
                        {
                            generar_tabla_simbolo(simbolo_hijo, simbolo.nombre);
                        }
                    }
                    break;

                case "METODO":
                    {
                        if (simbolo.tamanio > 0)
                        {
                            principal.insertarTablaSimbolo(simbolo.nombre, simbolo.tipo, simbolo.rol, simbolo.visibilidad, nombre_ambito, simbolo.tamanio, simbolo.posicion);
                            List<simbolo> lista_parametro = ((metodo)simbolo.valor).parametros;
                            foreach (simbolo simb in lista_parametro)
                            {
                                principal.insertarTablaSimbolo(simb.nombre, simb.tipo, simb.rol, simb.visibilidad, nombre_ambito + "_" + simbolo.nombre, simb.tamanio, simb.posicion);
                            }
                            foreach (simbolo simbolo_hijo in simbolo.ambito.tablaSimbolo)
                            {
                                generar_tabla_simbolo(simbolo_hijo, nombre_ambito + "_" + simbolo.nombre);
                            }
                        }
                    }
                    break;

                case "CONSTRUCTOR":
                    {
                        if (simbolo.tamanio > 0)
                        {
                            principal.insertarTablaSimbolo(simbolo.nombre, simbolo.tipo, simbolo.rol, simbolo.visibilidad, nombre_ambito, simbolo.tamanio, simbolo.posicion);
                            List<simbolo> lista_parametro = ((metodo)simbolo.valor).parametros;
                            foreach (simbolo simb in lista_parametro)
                            {
                                principal.insertarTablaSimbolo(simb.nombre, simb.tipo, simb.rol, simb.visibilidad, nombre_ambito + "_" + simbolo.nombre, simb.tamanio, simb.posicion);
                            }
                            foreach (simbolo simbolo_hijo in simbolo.ambito.tablaSimbolo)
                            {
                                generar_tabla_simbolo(simbolo_hijo, nombre_ambito + "_" + simbolo.nombre);
                            }
                        }
                    }
                    break;

                case "SI":
                    {
                        if (simbolo.tamanio > 0)
                        {
                            principal.insertarTablaSimbolo(simbolo.nombre, "", simbolo.rol, "", nombre_ambito, simbolo.tamanio, simbolo.posicion);
                            si si = (si)simbolo.valor;
                            foreach (simbolo simbolo_hijo in si.ambito.tablaSimbolo)
                            {
                                generar_tabla_simbolo(simbolo_hijo, nombre_ambito + "_" + simbolo.nombre);
                            }

                            try
                            {
                                if (si.lista_sino_si != null)
                                {
                                    foreach (sino_si sinosi in si.lista_sino_si)
                                    {
                                        principal.insertarTablaSimbolo("SINO_SI", "", "SINO_SI", "", nombre_ambito, sinosi.ambito.tamanio, -1);
                                        foreach (simbolo simb in sinosi.ambito.tablaSimbolo)
                                        {
                                            generar_tabla_simbolo(simb, nombre_ambito + "_" + "SINO_SI");
                                        }
                                    }
                                }
                            }
                            catch { }

                            try
                            {
                                if (si.sino != null)
                                {
                                    principal.insertarTablaSimbolo("SINO", "", "SINO", "", nombre_ambito, si.sino.ambito.tamanio, -1);
                                    foreach (simbolo simb in si.sino.ambito.tablaSimbolo)
                                    {
                                        generar_tabla_simbolo(simb, nombre_ambito + "_" + "SINO");
                                    }
                                }
                            }
                            catch { }
                        }
                    }
                    break;

                case "PARA":
                    {
                        if (simbolo.tamanio > 0)
                        {
                            principal.insertarTablaSimbolo(simbolo.nombre, "", simbolo.rol, "", nombre_ambito, simbolo.tamanio, simbolo.posicion);
                            para para = (para)simbolo.valor;
                            if (para.declara_asigna.rol.Equals("DECLARACION"))
                            {
                                generar_tabla_simbolo(para.declara_asigna, nombre_ambito + "_" + simbolo.rol);
                            }
                            foreach (simbolo simbolo_hijo in simbolo.ambito.tablaSimbolo)
                            {
                                generar_tabla_simbolo(simbolo_hijo, nombre_ambito + "_" + simbolo.nombre);
                            }
                        }
                    }
                    break;

                case "ELEGIR":
                    {
                        if (simbolo.tamanio > 0)
                        {
                            principal.insertarTablaSimbolo(simbolo.nombre, "", simbolo.rol, "", nombre_ambito, simbolo.tamanio, simbolo.posicion);
                            elegir elegir = (elegir)simbolo.valor;
                            foreach (caso caso in elegir.lista_caso)
                            {
                                principal.insertarTablaSimbolo("CASO", "", "CASO", "", nombre_ambito + "_" + "ELEGIR", caso.ambito.tamanio, -1);
                                foreach (simbolo simb in caso.ambito.tablaSimbolo)
                                {
                                    generar_tabla_simbolo(simb, nombre_ambito + "_ELEGIR_CASO");
                                }
                            }

                            if (elegir.defecto != null)
                            {
                                principal.insertarTablaSimbolo("DEFECTO", "", "DEFECTO", "", nombre_ambito + "_" + "ELEGIR", elegir.defecto.ambito.tamanio, -1);
                                foreach (simbolo simb in elegir.defecto.ambito.tablaSimbolo)
                                {
                                    generar_tabla_simbolo(simb, nombre_ambito + "_ELEGIR_DEFECTO");
                                }
                            }
                        }
                    }

                    break;
                case "DECLARACION":
                    {
                        principal.insertarTablaSimbolo(simbolo.nombre, simbolo.tipo, simbolo.rol, simbolo.visibilidad, nombre_ambito, simbolo.tamanio, simbolo.posicion);
                    }
                    break;
                default:
                    if (simbolo.tamanio > 0)
                    {
                        principal.insertarTablaSimbolo(simbolo.nombre, "", simbolo.rol, "", nombre_ambito, simbolo.tamanio, simbolo.posicion);
                        foreach (simbolo simb in simbolo.ambito.tablaSimbolo)
                        {
                            generar_tabla_simbolo(simb, nombre_ambito + "_" + simbolo.rol);
                        }
                    }
                    break;
            }
        }


        public void generar_tabla_simbolo()
        {
            foreach (simbolo simbolo in ambito.tablaSimbolo)
            {
                generar_tabla_simbolo(simbolo, "");
            }
        }
    }
}
