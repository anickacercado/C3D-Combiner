using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _Compi2_Proyecto2_201212859.Formularios
{
    class controlTabIDE : TabControl
    {
        int contador = 1;

        public void analizar() {
            tabIDE tabAux = (tabIDE)this.SelectedTab;
            if (tabAux != null)
            {
                tabAux.analizar();
            }
        }

        public void agregarNewTab()
        {
            String nombre = "New " + contador.ToString();
            tabIDE tab = new tabIDE(nombre, "", "",0);
            this.TabPages.Add(tab);
            contador++;
        }

        public void abrirTab()
        {
            OpenFileDialog abrir = new OpenFileDialog();
            int tipo;
            abrir.Filter = "Archivo OLC|*.olc|Archivo TREE|*.tree";
            abrir.Title = "Abrir";
            abrir.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            abrir.ShowDialog();

            if (abrir.FileName != "")
            {
                tipo = abrir.FilterIndex;
                tabIDE aux = new tabIDE(abrir.SafeFileName, File.ReadAllText(abrir.FileName), abrir.FileName, tipo);
                this.TabPages.Add(aux);
            }
        }

        public void guardarTab()
        {
            tabIDE tabAux = (tabIDE)this.SelectedTab;
            if (tabAux != null)
            {
                tabAux.guardarArchivo();
                this.Refresh();
                this.Update();
            }
        }

        public void guardarComoTab()
        {
            tabIDE tabAux = (tabIDE)this.SelectedTab;
            if (tabAux != null)
            {
                tabAux.guardarComoArchivo();
                this.Refresh();
                this.Update();
            }
        }

        public void borrarTab()
        {
            tabIDE tabAux = (tabIDE)this.SelectedTab;
            if (tabAux != null)
            {
                if (tabAux.esModificado())
                {
                    switch (MessageBox.Show("Desea guardar el archivo", "Guardar archivo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk))
                    {
                        case DialogResult.Yes:
                            guardarTab();
                            this.TabPages.Remove(tabAux);
                            break;
                        case DialogResult.No:
                            this.TabPages.Remove(tabAux);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    this.TabPages.Remove(tabAux);
                }
            }
        }
    }
}

