using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace _Compi2_Proyecto2_201212859.Formularios
{
    class repositorio
    {
        public static string usuario = "";
        public static string contrasenia = "";
        public static string nombre = "";
        public static string autor = "";
        public static string url = "";
        public static string fecha_creacion = "";
        public static string fecha_modificacion = "";
        public static string descripcion = "";
        public static string codigo = "";
        public static string ruta = "";
        public static string stringConexion = "Server=" + "localhost" + ";Port=" + "3306" + ";Database=" + "repositorio" + ";Uid=" + "root" + ";Pwd=" + "admin" + ";";


        public void crearRepositorio() {
            url = "http:/localhost/repositorio/" + usuario + "/" + nombre;
            string Query = "INSERT INTO `repositorio`.`clase` (`nombre`, `autor`, `url`, `fecha_creacion`, `fecha_modificacion`, `descripcion`, `codigo`, `ruta`) VALUES ('" + repositorio.nombre + "', '" + repositorio.autor + "', '" + repositorio.url + "', '" + repositorio.fecha_creacion + "', '" + repositorio.fecha_modificacion + "', '" + repositorio.descripcion + "', '" + repositorio.codigo + "', '" + repositorio.ruta + "');";
            try
            {
                MySqlConnection conexion = new MySqlConnection(stringConexion);
                conexion.Open();
                MySqlCommand comando = conexion.CreateCommand();
                comando.CommandText = Query;
                comando.ExecuteNonQuery();
                conexion.Close();
                MessageBox.Show("Repositorio Actualizado");
            }
            catch {
                MessageBox.Show("Error Repositorio");
            }
        }


        public void iniciarSesion()
        {
            url = "http:/localhost/repositorio/" + usuario + "/" + nombre;
            string Query = "select * from usuario where usuario= '" + repositorio.usuario + "' and contrasenia= '" + repositorio.contrasenia + "';";
            try
            {
                MySqlConnection conexion = new MySqlConnection(stringConexion);
                conexion.Open();
                MySqlCommand comando = conexion.CreateCommand();
                comando.CommandText = Query;
                MySqlDataReader leer = comando.ExecuteReader();
                if (leer.Read()) //Si el usuario es correcto nos abrira la otra ventana.
                {
                    MessageBox.Show("Bienvenido");
                }
                else {
                    repositorio.usuario = "";
                    repositorio.contrasenia = "";
                    MessageBox.Show("Usuario o contraseña erroneos");
                }
                conexion.Close();
            }
            catch
            {
                MessageBox.Show("Error Inicio de Sesión");
            }
        }


        public static void cerrarSesion() {
            repositorio.usuario = "";
            repositorio.contrasenia = "";
            repositorio.nombre = "";
            repositorio.autor = "";
            repositorio.url = "";
            repositorio.fecha_creacion = "";
            repositorio.fecha_modificacion = "";
            repositorio.descripcion = "";
            repositorio.codigo = "";
            repositorio.ruta = "";
            MessageBox.Show("Ha cerrado sus sesión exitosamente, si desea utilizar el modulo de código compartido inicie sesión nuevamente.");
        }
    }
}
