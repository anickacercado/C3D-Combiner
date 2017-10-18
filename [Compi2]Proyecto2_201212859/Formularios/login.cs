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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            repositorio.usuario = txtUsuario.Text;
            repositorio.contrasenia = txtContrasenia.Text;
            repositorio r = new Formularios.repositorio();
            r.iniciarSesion();
        }
    }
}
