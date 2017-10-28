using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _Compi2_Proyecto2_201212859.Formularios
{
    public partial class ModuloDiagramasUML : Form
    {
        
        public struct Atributo
        {
            public string acceso;
            public string nombre;
            public string tipo;
            public string toString()
            {
                return nombre;
            }
        }
        public struct Funcion
        {
            public string acceso;
            public string nombre;
            public string tipo;
            public string toString()
            {
                return nombre;
            }
        }
        public struct Relacion
        {
            public string tipo;
            public string clase;
        }
        public struct Clase
        {
            public string nombre;
            public List<Atributo> atributos;
            public List<Funcion> funciones;
            public List<Relacion> relaciones;
            public string toString()
            {
                return nombre;
            }
        }
        public struct Proyecto
        {
            public List<string> accesos ;
            public List<string> tiposBasicos ;
            public List<Clase> clases;
            public string[] getClases()
            {
                List<string> tipos = new List<string>();
                for (int i = 0; i < clases.Count; i++) tipos.Add(clases[i].nombre);
                return tipos.ToArray();
            }
            public string[] getAllTipos()
            {
                List<string> tipos = new List<string>();
                tipos.AddRange(getClases());
                tipos.AddRange(tiposBasicos);
                return tipos.ToArray();
            }
        }
        
        public Proyecto proyecto;

        public ModuloDiagramasUML()
        {
            InitializeComponent();
            proyecto = new Proyecto();
            proyecto.accesos = new List<string> { "Publico", "Privado", "Protegido" };
            proyecto.tiposBasicos = new List<string> { "this", "entero", "decimal", "caracter" , "booleano" };
            proyecto.clases = new List<Clase>();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ModuloDiagramasUML_AddClass addClass = new ModuloDiagramasUML_AddClass(proyecto);
            addClass.ShowDialog();
            actualizarPaneles();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Console.Out.WriteLine("Nada");

        }

        public void actualizarPaneles()
        {
            listBox1.Items.Clear();
            foreach(Clase clase in proyecto.clases){
                listBox1.Items.Add(clase.nombre);
            }
        }
    }
}
