using CapaLogicaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
    public partial class FrmListadoUsuarios : DevComponents.DotNetBar.Metro.MetroForm
    {
        private clsUsuarios U = new clsUsuarios();
        int Listado = 0;

        public FrmListadoUsuarios()
        {
            InitializeComponent();
        }

        private void FrmListadoUsuarios_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.Interval = 5000;
            ListarUsuarios();
            dataGridView1.ClearSelection();
        }

        private void ListarUsuarios()
        {
            DataTable dt = new DataTable();
            dt = U.Listado();
            //try
            //{
                dataGridView1.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add(dt.Rows[i][0]);
                    dataGridView1.Rows[i].Cells[0].Value = dt.Rows[i][0].ToString();
                    dataGridView1.Rows[i].Cells[1].Value = dt.Rows[i][1].ToString();
                    dataGridView1.Rows[i].Cells[2].Value = dt.Rows[i][2].ToString();
                }
            //}
            //catch (Exception ex)
            //{
            //    DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message);
            //}
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Selected = true;
                timer1.Stop();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {

                    Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is FrmEditarUsuarios);
                    if (frm != null)
                    {
                        //si la instancia existe la pongo en primer plano
                        frm.BringToFront();
                        return;
                    }
                    else
                    {
                        FrmEditarUsuarios U = new FrmEditarUsuarios();
                        U.txtIdUsuario.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                        //Program.IdCargo = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value.ToString());
                        U.txtUsuario.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                        U.txtContraseña.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                        U.Show();
                    }
                }
                else
                {
                    DevComponents.DotNetBar.MessageBoxEx.Show("Seleccione un usuario para modificar.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
                dataGridView1.ClearSelection();
                timer1.Start();
            }
            ListarUsuarios(); //CDA05
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (Listado)
            {
                case 0: ListarUsuarios(); break;
            }
        }
    }
}
