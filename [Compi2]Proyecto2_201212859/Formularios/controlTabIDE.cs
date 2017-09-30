using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _Compi2_Proyecto2_201212859.Formularios
{
    class controlTabIDE: TabControl
    {
        int contador = 1;

        //evento para cerrar las pestañas


        public void agregarNewTab()
        {
            String nombre = "New " + contador.ToString();
            tabIDE tab = new tabIDE(nombre, "", "");
            this.TabPages.Add(tab);
            contador++;
        }

        public void abrirTab()
        {
            OpenFileDialog abrir = new OpenFileDialog();
            //abrir.Filter = Constante.DialogFilter;
            abrir.Title = "Abrir";
            abrir.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            abrir.ShowDialog();

            if (abrir.FileName != "")
            {
                tabIDE aux = new tabIDE(abrir.SafeFileName, File.ReadAllText(abrir.FileName), abrir.FileName);
                this.TabPages.Add(aux);
            }
        }

        public void guardarTab()
        {
            tabIDE TTaux = (tabIDE)this.SelectedTab;
            if (TTaux != null)
            {
                TTaux.guardarArchivo();
                this.Refresh();
                this.Update();
            }
        }

        public void guardarComoTab()
        {
            tabIDE TTaux = (tabIDE)this.SelectedTab;
            if (TTaux != null)
            {
                TTaux.guardarComoArchivo();
                this.Refresh();
                this.Update();
            }
        }

        public void borrarTab()
        {
            tabIDE TTaux = (tabIDE)this.SelectedTab;
            if (TTaux != null)
            {
                this.TabPages.Remove(TTaux);
            }
        }
    }
}

