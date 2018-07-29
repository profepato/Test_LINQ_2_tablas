using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_LINQ_2_tablas {
    public partial class FormPersona : Form {

        private TestLinqDataContext db;
        private int idPersonaActualizar;

        public FormPersona() {
            InitializeComponent();

            db = new TestLinqDataContext();

            // Cbo no editable
            cboCiudad.DropDownStyle = ComboBoxStyle.DropDownList;

            // Lleno el combobox
            cboCiudad.DataSource = db.ciudad;

            // Lo que se despliega es el nombre de la ciudad
            cboCiudad.DisplayMember = "Nombre";
            cboCiudad.ValueMember = "Id";

            tblPersonas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tblPersonas.MultiSelect = false;
            tblPersonas.DataSource = db.listadoPersonas;
        }

        private void btnRegistrar_Click(object sender, EventArgs e) {
            int idCiudad = int.Parse(cboCiudad.SelectedValue.ToString());

            persona p = new persona();

            p.nombre = txtNombrePersona.Text;
            p.ciudad = idCiudad;

            db.persona.InsertOnSubmit(p);
            db.SubmitChanges();

            // Turbio
            tblPersonas.DataSource = db.listadoPersonas.Where(t => t.Nombre.Contains(""));

            txtNombrePersona.Text = "";
            cboCiudad.SelectedIndex = 0;

            MessageBox.Show("Persona creada!");

        }

        private void btnEliminar_Click(object sender, EventArgs e) {
            String idPersona = tblPersonas.CurrentRow.Cells[0].Value.ToString();
            String nombre = tblPersonas.CurrentRow.Cells[1].Value.ToString();

            if (MessageBox.Show("¿Eliminar a \"" + nombre + "\"?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                persona p = db.persona.Where(
                    per => per.id == int.Parse(idPersona)
                ).FirstOrDefault();

                db.persona.DeleteOnSubmit(p);
                db.SubmitChanges();

                // Turbio
                tblPersonas.DataSource = db.listadoPersonas.Where(
                    per => per.Nombre.Contains("")
                );

                MessageBox.Show("Persona eliminada!");
            }
        }

        private void tblPersonas_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            idPersonaActualizar = int.Parse(tblPersonas.CurrentRow.Cells[0].Value.ToString());
            int idCiudad = int.Parse(tblPersonas.CurrentRow.Cells[2].Value.ToString());
            String nombre = tblPersonas.CurrentRow.Cells[1].Value.ToString();

            txtNombrePersona.Text = nombre;
            cboCiudad.SelectedValue = idCiudad;
        }

        private void txtBuscarPersona_TextChanged(object sender, EventArgs e) {
            String filtro = txtBuscarPersona.Text;

            tblPersonas.DataSource = db.listadoPersonas.Where(
                    per => per.Nombre.Contains(filtro) ||
                    per.Ciudad.Contains(filtro) ||
                    per.ID.ToString() == filtro
                );
        }

        private void btnActualizar_Click(object sender, EventArgs e) {
            persona p = db.persona.Where(
                per => per.id == idPersonaActualizar
                ).FirstOrDefault();

            p.nombre = txtNombrePersona.Text;
            p.ciudad = int.Parse(cboCiudad.SelectedValue.ToString());

            db.SubmitChanges();

            txtBuscarPersona.Text = "";
            cboCiudad.SelectedIndex = 0;

            // Turbio
            tblPersonas.DataSource = db.listadoPersonas.Where(
                per => per.Nombre.Contains("")
            );

            MessageBox.Show("Persona actualizada!");
        }
    }
}
