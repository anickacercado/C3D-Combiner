using _Compi2_Proyecto2_201212859.codigo3D;
using _Compi2_Proyecto2_201212859.Formularios;
using _Compi2_Proyecto2_201212859.ejecucion_alto_nivel;
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
using FastColoredTextBoxNS;
using _Compi2_Proyecto2_201212859.C3D;
using Irony.Parsing;

namespace _Compi2_Proyecto2_201212859
{
    public partial class principal : Form
    {
        MenuItem myMenuItem = new MenuItem("Show Me");
        IronyFCTB txt3D;
        IronyFCTB txt3D_optimizado;
        IronyFCTB txt3D_debug;

        public static principal componentes;
        public principal()
        {
            InitializeComponent();
            texto_3D();
            tablaErrores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tablaSimbolos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ListDirectory();
            principal.componentes = this;
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

        private void crearDirectorio(string Carpeta)
        {
            string Path = memoria.pathProyecto + Carpeta;
            try
            {
                if (Directory.Exists(Path))
                {
                    Console.WriteLine("That path exists already.");
                    return;
                }
                else
                {
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
            crearCarpeta testDialog = new crearCarpeta();
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

        public void limpiarGrid()
        {
            tablaErrores.Rows.Clear();
            tablaErrores.Refresh();
            tablaSimbolos.Rows.Clear();
            tablaSimbolos.Refresh();
        }

        public static void insertarError(string linea, string columna, string tipo, string descripcion, string ruta)
        {
            componentes.tablaErrores.Rows.Add(linea, columna, tipo, descripcion, ruta);
        }

        public static void insertarTablaSimbolo(String nombre, String tipo, String rol, String visibilidad, String ambito, int tamanio, int posicion)
        {
            int contador = componentes.tablaSimbolos.Rows.Count;
            componentes.tablaSimbolos.Rows.Add(nombre, tipo, rol, visibilidad, ambito, tamanio, posicion);
            if (rol.Equals("DECLARACION") || rol.Equals("PARAMETRO"))
            {
                componentes.tablaSimbolos.Rows[contador].DefaultCellStyle.BackColor = Color.DarkBlue;
                componentes.tablaSimbolos.Rows[contador].DefaultCellStyle.ForeColor = Color.White;
            }
            else if (rol.Equals("CLASE"))
            {
                componentes.tablaSimbolos.Rows[contador].DefaultCellStyle.BackColor = Color.DarkRed;
                componentes.tablaSimbolos.Rows[contador].DefaultCellStyle.ForeColor = Color.White;
            }
            else if (rol.Equals("METODO") || rol.Equals("CONSTRUCTOR"))
            {
                componentes.tablaSimbolos.Rows[contador].DefaultCellStyle.BackColor = Color.DarkGreen;
                componentes.tablaSimbolos.Rows[contador].DefaultCellStyle.ForeColor = Color.White;
            }
        }


        private void compilarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Limpia Reportes
            limpiarGrid();

            //Genera codigo 3D
            ControlEditor.analizar();
            generar3D generar3D = new generar3D();
            txt3D.Text = memoria.cadena3D;

            //Limpiar Estaticas 3D
            memoria.temporal = 0;
            memoria.etiqueta = 0;
            memoria.cadena3D = "";
            memoria.lista_estructura_clase = new List<estructura_clase>();
        }


        private void compartirClaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabIDE tabAux = (tabIDE)ControlEditor.SelectedTab;
            if (repositorio.usuario != "")
            {
                if (tabAux != null)
                {
                    if (tabAux.tipo == 2)
                    {
                        DateTime dt = DateTime.Now;
                        string fecha = dt.ToString("yyyy-MM-dd");
                        repositorio.nombre = tabAux.Text;
                        repositorio.autor = repositorio.usuario;
                        repositorio.fecha_creacion = fecha;
                        repositorio.fecha_modificacion = fecha;
                        repositorio.codigo = tabAux.TBContenido.Text;
                        repositorio.ruta = tabAux.ruta.Replace("\\", "\\\\");
                        compartirClase cla = new compartirClase();
                        cla.Show();
                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionar TAB de Gramatica TREE");
                    }
                }
                else { MessageBox.Show("Debe seleccionar archivo"); }
            }
            else { MessageBox.Show("Debe iniciar sesión primero"); }
        }

        private void ControlEditor_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void ControlEditor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void iniciarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            login l = new login();
            l.Show();
        }

        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            repositorio.cerrarSesion();
        }

        private void gramaticasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reporteGramatica rg = new reporteGramatica();
            rg.Show();
        }

        private void texto_3D()
        {
            gramaticaC3D gramatica = new gramaticaC3D();
            LanguageData language = new LanguageData(gramatica);
            Parser parser = new Parser(language);

            txt3D = new IronyFCTB();
            txt3D.Grammar = gramatica;
            txt3D.Multiline = true;
            txt3D.WordWrap = false;
            txt3D.Dock = DockStyle.Fill;
            this.tab4.Controls.Add(txt3D);

            txt3D_optimizado = new IronyFCTB();
            txt3D_optimizado.Grammar = gramatica;
            txt3D_optimizado.Multiline = true;
            txt3D_optimizado.WordWrap = false;
            txt3D_optimizado.Dock = DockStyle.Fill;
            this.tab5.Controls.Add(txt3D_optimizado);

            txt3D_debug = new IronyFCTB();
            txt3D_debug.Grammar = gramatica;
            txt3D_debug.Multiline = true;
            txt3D_debug.WordWrap = false;
            txt3D_debug.Dock = DockStyle.Fill;
            this.panel13.Controls.Add(txt3D_debug);
        }
    }
}
