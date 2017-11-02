using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            proyecto.tiposBasicos = new List<string> { "this", "entero", "decimal", "caracter", "booleano" };
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
            if (listBox1.SelectedItem != null)
                if (proyecto.clases.Count > 0)
                {
                    foreach (ModuloDiagramasUML.Clase clasei in proyecto.clases)
                        foreach (ModuloDiagramasUML.Relacion rel in clasei.relaciones)
                            if (rel.clase == listBox1.SelectedItem.ToString())
                            {
                                clasei.relaciones.Remove(rel);
                                break;
                            }
                    foreach (ModuloDiagramasUML.Clase clasei in proyecto.clases)
                        if (clasei.nombre == listBox1.SelectedItem.ToString())
                        {
                            proyecto.clases.Remove(clasei);
                            actualizarPaneles();
                            break;
                        }
                }
        }

        public void actualizarPaneles()
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            listBox1.Items.Clear();
            foreach(Clase clase in proyecto.clases){
                listBox1.Items.Add(clase.nombre);
            }
            generarUML_IMG();
            if (comboBox1.SelectedItem!=null && comboBox1.SelectedItem.ToString() == "OLC++")
            {

            }
            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "Tree")
            {
                generarTree();
            }
        }

        public void generarTree()
        {
            String cod = "";
            String niv = "\n";

            foreach (Clase clase in proyecto.clases)
            {
                cod += niv + "## Aqui empiezo la clase" + clase.nombre ;

                foreach (Atributo atr in clase.atributos)
                {
                    niv = "\n\t";
                    cod += niv + atr.acceso + " " + atr.tipo + " " + atr.nombre;
                }

                cod += "\n\n";

                foreach (Funcion fun in clase.funciones)
                {
                    niv = "\n\t";
                    if (fun.tipo == "void")
                    {
                        cod += niv + fun.acceso + " " + "metodo" + " " + fun.nombre + " []:";
                        cod += niv + "\t## Instrucciones";

                        //publicar metodo get
                    }
                    else
                    {
                        cod += niv + fun.acceso + " " + "funcion" + " " + fun.tipo + " " + fun.nombre + " []:";
                        cod += niv + "\t## Instrucciones";
                        cod += niv + "\tretornar ## variables";
                    }
                }

                cod += "\n\n";

                foreach (Atributo atr in clase.atributos)
                {
                    niv = "\n\t";
                    //cod += niv + atr.acceso + " " + atr.tipo + " " + atr.nombre;
                }
            }


            textBox1.Text = cod;
        }


        public void generarUML_IMG()
        {
            pictureBox1.Image = null;
            pictureBox1.Refresh();

            string path = @"UML_XD.png";
            string dot = generarDOT();
            Graficar(dot, path);

            Image image;
            using (Stream stream = File.OpenRead(path))
            {
                image = System.Drawing.Image.FromStream(stream);
            }
            pictureBox1.Image = image;
            File.Delete(path); 

            //pictureBox1.Image = Image.FromFile(path);
            pictureBox1.Refresh();
        }

        public String generarDOT()
        {
            string grafo = "";
            grafo = @"
digraph G{

    fontname = ""Bitstream Vera Sans""
    fontsize = 8

    node[
        fontname = ""Bitstream Vera Sans""
        fontsize = 8
        shape = ""record""
    ]";

            foreach(Clase clase in proyecto.clases)
            {
                grafo += clase.nombre + " [\nlabel = \"{ " + clase.nombre + @" | ";

                foreach(Atributo atr in clase.atributos)
                {
                    grafo += ((atr.acceso.Equals("Publico"))? "+" : (atr.acceso.Equals("Privado")) ? "-" :"#" );
                    grafo += atr.nombre + ": " + atr.tipo + "\\n ";
                }

                grafo += " | ";
                foreach (Funcion fun in clase.funciones)
                {
                    grafo += ((fun.acceso.Equals("Publico")) ? "+" : (fun.acceso.Equals("Privado")) ? "-" : "#");
                    grafo += fun.nombre + "(): " + fun.tipo + "\\n ";
                }
                grafo += "}\"\n]\n";

            }

            foreach (Clase clase in proyecto.clases)
            {
                foreach(Relacion rel in clase.relaciones)
                    if (rel.tipo == "Herencia")
                        grafo += clase.nombre + " -> " + rel.clase + " [dir=both arrowhead=onormal arrowtail=none]";
                    else if (rel.tipo == "Agregacion")
                        grafo += clase.nombre + " -> " + rel.clase + " [dir=both arrowhead=odiamond arrowtail=none]";
                    else if (rel.tipo == "Composicion")
                        grafo += clase.nombre + " -> " + rel.clase + " [dir=both arrowhead=diamond arrowtail=none]";
                    else if (rel.tipo == "Asociacion")
                        grafo += clase.nombre + " -> " + rel.clase + " [dir=both arrowhead=vee arrowtail=none  label=\"1   ->   0...* \"]";
                    else if (rel.tipo == "Dependencia")
                        grafo += clase.nombre + " -> " + rel.clase + " [dir=both style=dotted arrowhead=vee arrowtail=none]";
            }



            grafo += "}";
            return grafo;
        }


        private void Graficar(string grafoDot, string path)
        {
            WINGRAPHVIZLib.DOT dot = new WINGRAPHVIZLib.DOT();
            WINGRAPHVIZLib.BinaryImage img = dot.ToPNG(grafoDot);
            img.Save(path);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                ModuloDiagramasUML_AddClass addClass = new ModuloDiagramasUML_AddClass(proyecto, listBox1.SelectedItem.ToString());
                addClass.ShowDialog();
                actualizarPaneles();
            }
        }
    }
}
