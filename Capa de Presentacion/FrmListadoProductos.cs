﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevComponents.DotNetBar;
using CapaLogicaNegocio;
using System.Diagnostics;

namespace Capa_de_Presentacion
{
    public partial class FrmListadoProductos : DevComponents.DotNetBar.Metro.MetroForm
    {
        int Listado = 0;
        private clsProducto P = new clsProducto();

        public FrmListadoProductos()
        {
            InitializeComponent();
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            //string user = FrmMenuPrincipal.variable; //obtenemos el nombre de usuario para saber si limitamos opciones al usuario activo.
            //string cargo = FrmMenuPrincipal.cargoEmpleado;//obtenemos el cargo del usuario logueado para saber si limitamos opciones al usuario.
            ////if (user != "admin, admin")
            //{
            //    btnNuevo.Enabled = false;
            //    btnEditar.Enabled = false;
            //}
            //string usuarioLogueado = Program.NombreEmpleadoLogueado.ToString(); //otra forma de limitar el acceso a usuarios que no son administradores
            
            //if (cargo != "2")//ACA LIMITAMOS EL ACCESO A USUARIOS QUE NO SEAN EL ADMINISTRADOR.
            //{
            //    btnNuevo.Enabled = false;
            //    btnEditar.Enabled = false;
            //}

            timer1.Start();
            timer1.Interval = 5000;
            CargarListado();
            dataGridView1.ClearSelection();
        }

        private void CargarListado()
        {
            DataTable dt = new DataTable();
            dt = P.Listar();
            //try
            //{
                dataGridView1.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add(dt.Rows[i][0]);
                    dataGridView1.Rows[i].Cells[0].Value = dt.Rows[i][0].ToString();
                    dataGridView1.Rows[i].Cells[1].Value = dt.Rows[i][1].ToString();
                    dataGridView1.Rows[i].Cells[2].Value = dt.Rows[i][2].ToString();
                    dataGridView1.Rows[i].Cells[3].Value = dt.Rows[i][3].ToString();
                    dataGridView1.Rows[i].Cells[4].Value = dt.Rows[i][4].ToString();
                    dataGridView1.Rows[i].Cells[5].Value = dt.Rows[i][5].ToString();
                    dataGridView1.Rows[i].Cells[6].Value = dt.Rows[i][6].ToString();
                    dataGridView1.Rows[i].Cells[7].Value = dt.Rows[i][7].ToString();
                    dataGridView1.Rows[i].Cells[8].Value = Convert.ToDateTime(dt.Rows[i][8].ToString()).ToShortDateString();
                }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (DevComponents.DotNetBar.MessageBoxEx.Show("¿Está Seguro que Desea Salir.?", "Sistema de Ventas.", MessageBoxButtons.YesNo,MessageBoxIcon.Error) == DialogResult.Yes)
            {
                Close();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //if (dataGridView1.SelectedRows.Count > 0){
            //    //DevComponents.DotNetBar.MessageBoxEx.Show("La Fila no debe Estar Seleccionada.");
            //    //FrmRegistroProductos P = new FrmRegistroProductos();
            //    //dataGridView1.ClearSelection();
            //    //P.Show();
            //}else{
            Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is FrmRegistroProductos);
            if (frm != null)
            {
                //si la instancia existe la pongo en primer plano
                frm.BringToFront();
                return;
            }
            else
            {
                FrmRegistroProductos P = new FrmRegistroProductos();
                //if (dataGridView1.SelectedRows.Count > 0)
                //    Program.Evento = 1;
                //else
                    Program.Evento = 0;
                dataGridView1.ClearSelection();
                P.Show();
            }
            CargarListado(); //CDA05

            // }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) {

                Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is FrmRegistroProductos);
                if (frm != null)
                {
                    //si la instancia existe la pongo en primer plano
                    frm.BringToFront();
                    return;
                }
                else
                {
                    FrmRegistroProductos P = new FrmRegistroProductos();
                    P.txtIdP.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    P.IdC.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    P.txtProducto.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    P.txtMarca.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    P.txtPCompra.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    P.txtPVenta.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                    P.txtStock.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                    P.txtStockMinimo.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString(); //CDA05
                    P.dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[8].Value.ToString());
                    P.Show();
                }

                if (dataGridView1.SelectedRows.Count > 0)
                    Program.Evento = 1;
                else
                    Program.Evento = 0;
                dataGridView1.ClearSelection();
            }
            else {
                DevComponents.DotNetBar.MessageBoxEx.Show("Debe Seleccionar la Fila a Editar.","Sistema de Ventas.",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            dataGridView1.ClearSelection();
            timer1.Start();
            CargarListado(); //CDA05
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (Listado)
            {
                case 0: CargarListado(); break;        
            }
        }

        private void BusquedaProductos() {
            DataTable dt = new DataTable();
            try
            {
                P.Marca = txtBuscarProducto.Text;
                dt = P.BusquedaProductos(P.Marca);
                dataGridView1.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add(dt.Rows[i][0]);
                    dataGridView1.Rows[i].Cells[0].Value = dt.Rows[i][0].ToString();
                    dataGridView1.Rows[i].Cells[1].Value = dt.Rows[i][1].ToString();
                    dataGridView1.Rows[i].Cells[2].Value = dt.Rows[i][2].ToString();
                    dataGridView1.Rows[i].Cells[3].Value = dt.Rows[i][3].ToString();
                    dataGridView1.Rows[i].Cells[4].Value = dt.Rows[i][4].ToString();
                    dataGridView1.Rows[i].Cells[5].Value = dt.Rows[i][5].ToString();
                    dataGridView1.Rows[i].Cells[6].Value = dt.Rows[i][6].ToString();
                    dataGridView1.Rows[i].Cells[7].Value = dt.Rows[i][7].ToString(); //CDA05    
                    dataGridView1.Rows[i].Cells[8].Value = Convert.ToDateTime(dt.Rows[i][8].ToString()).ToShortDateString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dataGridView1.ClearSelection();
        }
        private void txtBuscarProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                BusquedaProductos();
                timer1.Stop();
            }
            else {
                CargarListado();
                timer1.Start();
            }
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13){
             if(dataGridView1.SelectedRows.Count>0)
            dataGridView1.ClearSelection();
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
                Program.IdProducto = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                Program.Descripcion = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                Program.Marca = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                Program.PrecioVenta = Convert.ToDecimal(dataGridView1.CurrentRow.Cells[5].Value.ToString());
                Program.Stock = Convert.ToInt32(dataGridView1.CurrentRow.Cells[6].Value.ToString());
                Program.StockMinimo = Convert.ToInt32(dataGridView1.CurrentRow.Cells[7].Value.ToString()); //CDA05
            this.Close();
        }

        private void txtBuscarProducto_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscarProducto.TextLength > 0)
            {

                DataTable dt = new DataTable();
                P.Marca = txtBuscarProducto.Text;
                dt = P.BusquedaProductos(P.Marca);
                try
                {
                    dataGridView1.Rows.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dataGridView1.Rows.Add(dt.Rows[i][0]);
                        dataGridView1.Rows[i].Cells[0].Value = dt.Rows[i][0].ToString();
                        dataGridView1.Rows[i].Cells[1].Value = dt.Rows[i][1].ToString();
                        dataGridView1.Rows[i].Cells[2].Value = dt.Rows[i][2].ToString();
                        dataGridView1.Rows[i].Cells[3].Value = dt.Rows[i][3].ToString();
                        dataGridView1.Rows[i].Cells[4].Value = dt.Rows[i][4].ToString();
                        dataGridView1.Rows[i].Cells[5].Value = dt.Rows[i][5].ToString();
                        dataGridView1.Rows[i].Cells[6].Value = dt.Rows[i][6].ToString();
                        dataGridView1.Rows[i].Cells[7].Value = dt.Rows[i][7].ToString();//CDA05
                        dataGridView1.Rows[i].Cells[8].Value = Convert.ToDateTime(dt.Rows[i][8].ToString()).ToShortDateString();
                    }
                    dataGridView1.ClearSelection();
                    timer1.Stop();
                }
                catch (Exception ex)
                {
                    DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message);
                }
            }
            else
            {
                CargarListado();
            }
        }
    }
}
