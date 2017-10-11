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

        //gramaticaOLC Gramatica;
        gramaticaTREE Gramatica;
        LanguageData language;
        Parser parser;


        public tabIDE(String nombre, String texto, String ruta)
        {
            this.Text = nombre;
            this.ruta = ruta;
            initComponent(texto);
        }


        public void analizar()
        {
            if (guardarArchivo())
            {
                string entrada = TBContenido.Text;
                //analizarOLC aOLC = new analizarOLC();
                //aOLC.analizar(entrada, ruta);  

                analizarTREE aTREE = new analizarTREE();
                aTREE.analizar(entrada, ruta);
            }
        }

        private void initComponent(String texto)
        {
            this.modificado = false;

            //Gramatica = new gramaticaOLC();
            Gramatica = new gramaticaTREE();
            language = new LanguageData(Gramatica);
            parser = new Parser(language);

            TBContenido = new IronyFCTB();
            TBContenido.Grammar = Gramatica;

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

                //guardar.Filter = Constante.DialogFilter;
                guardar.Title = "Guardar Archivo";
                guardar.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                if (guardar.ShowDialog() == DialogResult.OK)
                {
                    System.IO.FileStream fs = (System.IO.FileStream)guardar.OpenFile();
                    fs.Close();
                    System.IO.File.WriteAllText(guardar.FileName, this.TBContenido.Text);
                    ruta = guardar.FileName;
                    modificado = false;
                    this.Text = System.IO.Path.GetFileName(ruta);
                    estado = true;
                }
            }
            else
            {
                System.IO.File.WriteAllText(this.ruta, this.TBContenido.Text);
                modificado = false;
                this.Text = System.IO.Path.GetFileName(ruta);
                estado = true;
            }
            return estado;
        }

        public void guardarComoArchivo()
        {
            SaveFileDialog guardar = new SaveFileDialog();

            //guardar.Filter = Constante.DialogFilter;
            guardar.Title = "Guardar Archivo";
            guardar.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);


            if (guardar.ShowDialog() == DialogResult.OK)
            {
                System.IO.FileStream fs = (System.IO.FileStream)guardar.OpenFile();
                fs.Close();
                System.IO.File.WriteAllText(guardar.FileName, this.TBContenido.Text);
                ruta = guardar.FileName;

                modificado = false;
                this.Text = System.IO.Path.GetFileName(ruta);
            }
        }
    }

}

