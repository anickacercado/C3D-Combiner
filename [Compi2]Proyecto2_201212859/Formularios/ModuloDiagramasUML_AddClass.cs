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
    public partial class ModuloDiagramasUML_AddClass : Form
    {
        ModuloDiagramasUML.Proyecto proyecto;

        public ModuloDiagramasUML_AddClass( ModuloDiagramasUML.Proyecto proyecto)
        {
            InitializeComponent();
            this.proyecto = proyecto;
            AddComboBoxColumns();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ModuloDiagramasUML_AddClass_Load(object sender, EventArgs e)
        {
        }

        private void AddComboBoxColumns()
        {
            this.Acceso.Items.AddRange(proyecto.accesos.ToArray());
            this.Tipo.Items.AddRange(proyecto.getAllTipos());

            this.AccesoM.Items.AddRange(proyecto.accesos.ToArray());
            this.TipoM.Items.AddRange(proyecto.getAllTipos());
            this.TipoM.Items.Add("void");

            this.Relacion.Items.AddRange("Herencia", "Agregacion", "Composicion" , "Asociacion", "Dependencia");

            this.Clase.Items.AddRange(proyecto.getClases());
            if (this.Clase.Items.Count <= 0) this.dataGridView3.Enabled = false;
            else this.dataGridView3.Enabled = true;

        }


        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ModuloDiagramasUML.Clase clase = new ModuloDiagramasUML.Clase();
            clase.nombre = textBox1.Text;
            clase.atributos = new List<ModuloDiagramasUML.Atributo>();
            clase.funciones = new List<ModuloDiagramasUML.Funcion>();
            clase.relaciones = new List<ModuloDiagramasUML.Relacion>();

            //Grid de Atributos
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value == null && row.Cells[1].Value == null && row.Cells[2].Value == null) continue;
                ModuloDiagramasUML.Atributo atr = new ModuloDiagramasUML.Atributo();
                atr.acceso = row.Cells[0].Value.ToString();
                atr.nombre = row.Cells[1].Value.ToString();
                atr.tipo = row.Cells[2].Value.ToString();
                clase.atributos.Add(atr);
            }

            //Grid de Funciones
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells[0].Value == null && row.Cells[1].Value == null && row.Cells[2].Value == null) continue;
                ModuloDiagramasUML.Funcion func = new ModuloDiagramasUML.Funcion();
                func.acceso = row.Cells[0].Value.ToString();
                func.nombre = row.Cells[1].Value.ToString();
                func.tipo = row.Cells[2].Value.ToString();
                clase.funciones.Add(func);
            }

            //Grid de Relaciones
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                
                if (row.Cells.Count<2 || row.Cells[0].Value == null || row.Cells[1].Value == null) continue;
                ModuloDiagramasUML.Relacion rel = new ModuloDiagramasUML.Relacion();
                rel.clase = row.Cells[1].Value.ToString();
                rel.tipo = row.Cells[0].Value.ToString();
                clase.relaciones.Add(rel);
            }

            this.proyecto.clases.Add(clase);
            this.Close();
        }
    }
}
