using _Compi2_Proyecto2_201212859.Formularios;
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

namespace _Compi2_Proyecto2_201212859
{
    public partial class principal : Form
    {
        MenuItem myMenuItem = new MenuItem("Show Me");
        public principal()
        {
            InitializeComponent();
            tablaErrores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tablaSimbolos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ListDirectory();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ControlEditor.agregarNewTab();
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ControlEditor.guardarTab();
            ListDirectory();
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ControlEditor.guardarComoTab();
            ListDirectory();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ControlEditor.abrirTab();
        }

        private void ListDirectory()
        {
            treeView1.Nodes.Clear();
            var rootDirectoryInfo = new DirectoryInfo(memoria.pathProyecto);
            treeView1.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
        }

        private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeNode(directoryInfo.Name);
            foreach (var directory in directoryInfo.GetDirectories())
                directoryNode.Nodes.Add(CreateDirectoryNode(directory));
            foreach (var file in directoryInfo.GetFiles())
                directoryNode.Nodes.Add(new TreeNode(file.Name));
            return directoryNode;
        }



        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                string clickedNode = e.Node.Text;
                contextMenuStrip1.Show(treeView1, e.Location);
            }
        }

        private void crearDirectorio(string Carpeta) {
            string Path = memoria.pathProyecto + Carpeta;
            try
            {
                if (Directory.Exists(Path))
                {
                    Console.WriteLine("That path exists already.");
                    return;
                }
                else {
                    DirectoryInfo di = Directory.CreateDirectory(Path);
                    Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(Path));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        private void crearToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void crearCarpetaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MensajeCrearDirectorio();
        }

        public void MensajeCrearDirectorio()
        {
            CrearCarpeta testDialog = new CrearCarpeta();
            if (testDialog.ShowDialog(this) == DialogResult.OK)
            {
                crearDirectorio(testDialog.textBox1.Text);
                ListDirectory();
            }
            testDialog.Dispose();
        }

        private void crearCarpetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MensajeCrearDirectorio();
        }

        private void cerrarArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ControlEditor.borrarTab();
        }

        public void limpiarTablaError() {
            tablaErrores.Rows.Clear();
            tablaErrores.Refresh();
        }

        public static void insertarError(string linea, string columna, string tipo, string descripcion, string ruta) {
            tablaErrores.Rows.Insert(0,linea,columna,tipo,descripcion,ruta);
        }

        private void compilarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            limpiarTablaError();
            ControlEditor.analizar();
        }
    }
}
