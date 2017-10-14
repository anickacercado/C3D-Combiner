using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Irony.Parsing;
using FastColoredTextBoxNS;
using System.Drawing;
using _Compi2_Proyecto2_201212859.OLC;
using _Compi2_Proyecto2_201212859.TREE;

namespace _Compi2_Proyecto2_201212859.Formularios
{
    class tabIDE : TabPage
    {

        IronyFCTB TBContenido;
        Label panel;
        bool modificado;
        string ruta;
        int tipo;

        gramaticaOLC gramatica_olc;
        gramaticaTREE gramatica_tree;
        LanguageData language;
        Parser parser;


        public tabIDE(String nombre, String texto, String ruta, int tipo)
        {
            this.Text = nombre;
            this.ruta = ruta;
            this.tipo = tipo;
            initComponent(texto);
        }


        public void analizar()
        {
            if (guardarArchivo())
            {
                string entrada = TBContenido.Text;
                if (this.tipo == 1)
                {
                    analizarOLC aOLC = new analizarOLC();
                    aOLC.analizar(entrada, ruta);
                }
                else if (this.tipo == 2)
                {
                    analizarTREE aTREE = new analizarTREE();
                    aTREE.analizar(entrada, ruta);
                }
            }
        }

        private void initComponent(String texto)
        {
            this.modificado = false;

            TBContenido = new IronyFCTB();
            if (this.tipo == 0 || this.tipo == 1)
            {
                gramatica_olc = new gramaticaOLC();
                language = new LanguageData(gramatica_olc);
                parser = new Parser(language);
                TBContenido.Grammar = gramatica_olc;

            }
            else if (this.tipo == 2)
            {
                gramatica_tree = new gramaticaTREE();
                language = new LanguageData(gramatica_tree);
                parser = new Parser(language);
                TBContenido.Grammar = gramatica_tree;
            }
 
            TBContenido.Multiline = true;
            TBContenido.Text = texto;
            TBContenido.WordWrap = false;
            TBContenido.Dock = DockStyle.Fill;

            panel = new Label();
            panel.Dock = DockStyle.Bottom;
            panel.Text = "Linea: 1, Columna: 1";
            panel.TextAlign = ContentAlignment.MiddleRight;

            TBContenido.TextChanged += TBContenido_TextChanged;
            TBContenido.SelectionChanged += TBContenido_SelectionChanged;

            this.Controls.Add(TBContenido);
            this.Controls.Add(panel);
        }

        private void TBContenido_SelectionChanged(object sender, EventArgs e)
        {
            panel.Text = "Linea: " + (TBContenido.Selection.Start.iLine + 1).ToString() + ", Columna: " + (TBContenido.Selection.Start.iChar + 1).ToString();
        }

        private void TBContenido_TextChanged(object sender, EventArgs e)
        {
            modificado = true;
        }

        public bool esModificado()
        {
            return modificado;
        }

        public Boolean guardarArchivo()
        {
            Boolean estado = false;
            if (String.IsNullOrWhiteSpace(ruta))
            {
                SaveFileDialog guardar = new SaveFileDialog();

                guardar.Filter ="Archivo OLC|*.olc|Archivo TREE|*.tree";
                guardar.Title = "Guardar Archivo";
                guardar.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                if (guardar.ShowDialog() == DialogResult.OK)
                {
                    System.IO.FileStream fs = (System.IO.FileStream)guardar.OpenFile();
                    fs.Close();
                    System.IO.File.WriteAllText(guardar.FileName, this.TBContenido.Text);

                    this.tipo = guardar.FilterIndex;
                    this.ruta = guardar.FileName;
                    this.modificado = false;
                    this.Text = System.IO.Path.GetFileName(ruta);
                    estado = true;
                }
            }
            else
            {
                System.IO.File.WriteAllText(this.ruta, this.TBContenido.Text);
                this.modificado = false;
                this.Text = System.IO.Path.GetFileName(ruta);
                estado = true;
            }
            return estado;
        }

        public void guardarComoArchivo()
        {
            SaveFileDialog guardar = new SaveFileDialog();

            guardar.Filter = "Archivo OLC|*.olc|Archivo TREE|*.tree";
            guardar.Title = "Guardar Archivo";
            guardar.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);


            if (guardar.ShowDialog() == DialogResult.OK)
            {
                System.IO.FileStream fs = (System.IO.FileStream)guardar.OpenFile();
                fs.Close();
                System.IO.File.WriteAllText(guardar.FileName, this.TBContenido.Text);
                this.tipo = guardar.FilterIndex;
                this.ruta = guardar.FileName;
                this.modificado = false;
                this.Text = System.IO.Path.GetFileName(ruta);
                initComponent(this.Text);
            }
        }
    }

}

