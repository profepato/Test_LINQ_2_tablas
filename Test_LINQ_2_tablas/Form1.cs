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
    public partial class Form1 : Form {

        private TestLinqDataContext db;

        public Form1() {
            InitializeComponent();

            db = new TestLinqDataContext();


            /* tblPersonas.DataSource = db.persona.Join(
                 db.ciudad ,
                 per => per.ciudad,
                 ciu => ciu.id,
                 (per, ciu) => new {
                     id = per.id,
                     nombre = per.nombre,
                     ciudad = ciu.nombre
                 }
             ).Where(per => per.nombre.ToLower().Contains("m"));*/

            

            tblPersonas.DataSource = db.listadoPersonas;
            
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e) {
            if (MessageBox.Show("¿Desea salir?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                Application.Exit();
            }
        }

        private void ciudadToolStripMenuItem_Click(object sender, EventArgs e) {
            FormCiudad fc = new FormCiudad();
            fc.Show();
        }
    }
}
