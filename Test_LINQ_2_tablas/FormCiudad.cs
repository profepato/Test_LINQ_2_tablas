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
    public partial class FormCiudad : Form {

        private TestLinqDataContext db;
        private int idEliminar;

        public FormCiudad() {
            InitializeComponent();

            db = new TestLinqDataContext();

            tblCiudad.DataSource = db.ciudad;

            // Para que se seleccione una fila completa
            tblCiudad.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    
            // Para que no se seleccione más de una fila
            tblCiudad.MultiSelect = false;
        }

        private void txtBuscarCiudad_TextChanged(object sender, EventArgs e) {
            String filtro = txtBuscarCiudad.Text;

            tblCiudad.DataSource = db.ciudad.Where(ciu => ciu.nombre.Contains(filtro) || ciu.id.ToString() == filtro);
        }

        private void btnRegistrar_Click(object sender, EventArgs e) {
            ciudad c = new ciudad();
            c.nombre = txtNombreCiudad.Text;

            db.ciudad.InsertOnSubmit(c);
            db.SubmitChanges();

            txtNombreCiudad.Text = "";

            // Turbio
            tblCiudad.DataSource = db.ciudad.Where(ci=>ci.nombre.Contains(""));

            MessageBox.Show("Ciudad registrada!");
        }

        private void btnEliminar_Click(object sender, EventArgs e) {
            String idCiudad = tblCiudad.CurrentRow.Cells[0].Value.ToString();
            String nombre = tblCiudad.CurrentRow.Cells[1].Value.ToString();
            
            if (MessageBox.Show("¿Eliminar a \""+nombre+"\"?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                ciudad c = db.ciudad.Where(
                    ciu => ciu.id == int.Parse(idCiudad)
                ).FirstOrDefault();

                db.ciudad.DeleteOnSubmit(c);
                db.SubmitChanges();

                // Turbio
                tblCiudad.DataSource = db.ciudad.Where(ci => ci.nombre.Contains(""));

                MessageBox.Show("Ciudad eliminada!");
            }

        }

        private void tblCiudad_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            String idCiudad = tblCiudad.CurrentRow.Cells[0].Value.ToString();
            String nombre = tblCiudad.CurrentRow.Cells[1].Value.ToString();

            txtNombreCiudad.Text = nombre;

            idEliminar = int.Parse(idCiudad);
        }

        private void btnActualizar_Click(object sender, EventArgs e) {
            ciudad c = db.ciudad.Where(ciu => ciu.id == idEliminar).FirstOrDefault();
            c.nombre = txtNombreCiudad.Text;

            db.SubmitChanges();
            txtNombreCiudad.Text = "";
            // Turbio
            tblCiudad.DataSource = db.ciudad.Where(ci => ci.nombre.Contains(""));

            MessageBox.Show("Ciudad actualizada!");
        }
    }
}
