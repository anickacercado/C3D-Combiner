using _Compi2_Proyecto2_201212859.OLC;
using _Compi2_Proyecto2_201212859.TREE;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _Compi2_Proyecto2_201212859.Formularios
{
    public partial class reporteGramatica : Form
    {
        private static int contador;
        private static String grafo;
        public reporteGramatica()
        {
            InitializeComponent();
            panel2.AutoScroll = true;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (comboBox1.SelectedIndex == 0)
            {
                gramaticaOLC gramatica = new gramaticaOLC();
                LanguageData lenguaje = new LanguageData(gramatica);
                Parser parser = new Parser(lenguaje);
                ParseTree arbol = parser.Parse(richTextBox1.Text);
                ParseTreeNode raiz = arbol.Root;

                if (raiz == null)
                {
                    MessageBox.Show("Entrada no válida");
                }
                else
                {
                    string path = @"C:\Users\anick\Documents\Visual Studio 2015\Projects\[Compi2]Proyecto2_201212859\C3D-Combiner\[Compi2]Proyecto2_201212859\graficarAST\OLC.png";
                    GraficarAST(raiz, path);
                    pictureBox1.Image = Image.FromFile(path);
                    pictureBox1.Refresh();
                }
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                gramaticaTREE gramatica = new gramaticaTREE();
                LanguageData lenguaje = new LanguageData(gramatica);
                Parser parser = new Parser(lenguaje);
                ParseTree arbol = parser.Parse(richTextBox1.Text);
                ParseTreeNode raiz = arbol.Root;

                if (raiz == null)
                {
                    MessageBox.Show("Entrada no válida");
                }
                else
                {
                    string path = @"C:\Users\anick\Documents\Visual Studio 2015\Projects\[Compi2]Proyecto2_201212859\C3D-Combiner\[Compi2]Proyecto2_201212859\graficarAST\TREE.png";
                    GraficarAST(raiz, path);
                    pictureBox1.Image = Image.FromFile(path);
                    pictureBox1.Refresh();
                }
            }
        }

        public static string getDot(ParseTreeNode raiz)
        {
            grafo = "digraph G{";
            grafo += "nodo0[label=\"" + raiz.ToString() + "\"];\n";
            contador = 1;
            recorrerAST("nodo0", raiz);
            grafo += "}";
            return grafo;
        }

        private static void recorrerAST(string padre, ParseTreeNode hijos)
        {
            foreach (ParseTreeNode hijo in hijos.ChildNodes)
            {
                string nombreHijo = "nodo" + contador.ToString();
                grafo += nombreHijo + "[label=\"" + escapar(hijo.ToString()) + "\"];\n";
                grafo += padre + "->" + nombreHijo + ";\n";
                contador++;
                recorrerAST(nombreHijo, hijo);
            }
        }

        private static string escapar(string cadena)
        {
            cadena = cadena.Replace("\\", "\\\\");
            cadena = cadena.Replace("\"", "\\\"");
            return cadena;
        }

        public static void GraficarAST(ParseTreeNode raiz, string path)
        {
            if (raiz == null) { MessageBox.Show("No se ha generado Imagen"); }
            else { string grafoDot = getDot(raiz);
                Graficar(grafoDot, path);
            }
        }

        private static void Graficar(string grafoDot, string path)
        {
            WINGRAPHVIZLib.DOT dot = new WINGRAPHVIZLib.DOT();
            WINGRAPHVIZLib.BinaryImage img = dot.ToPNG(grafoDot);
            img.Save(path);
        }

        //CREAR ARCHIVO .DOT
        private static void ArchivoDot(string grafoDot)
        {
            FileStream fileStream = new FileStream(@"C:\Users\anick\Documents\Visual Studio 2015\Projects\[Compi2]Proyecto2_201212859\C3D-Combiner\[Compi2]Proyecto2_201212859\graficarAST\AST.dot", FileMode.Create, FileAccess.Write);
            StreamWriter streamWriter = new StreamWriter(fileStream);
            streamWriter.Write(grafoDot);
            streamWriter.Close();
            fileStream.Close();
        }

    }
}
