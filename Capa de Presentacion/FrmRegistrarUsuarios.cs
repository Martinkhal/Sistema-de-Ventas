﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaLogicaNegocio;
using DevComponents.DotNetBar;

namespace Capa_de_Presentacion
{
    public partial class FrmRegistrarUsuarios : DevComponents.DotNetBar.Metro.MetroForm
    {
        clsUsuarios U = new clsUsuarios();

        public FrmRegistrarUsuarios()
        {
            InitializeComponent();
        }

        private void FrmRegistrarUsuarios_Load(object sender, EventArgs e)
        {
            
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (txtUser.Text.Trim() != "")
            {
                if (txtPassword.Text.Trim() != "")
                {
                    if (Program.IdEmpleado != 0)
                    {
                        U.IdEmpleado = Program.IdEmpleado;
                        U.User = txtUser.Text;
                        U.Password = txtPassword.Text;
                        DevComponents.DotNetBar.MessageBoxEx.Show(U.RegistrarUsuarios(), "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        Limpiar();
                        this.Close();
                    }
                    else {
                        DevComponents.DotNetBar.MessageBoxEx.Show("Lo Sentimos, Pero Usted debe Eligir un \n Empleado para Crear una Cuenta de Usuario.\n \n G R A C I A S.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese su Contraseña.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Focus();
                }
            }
            else {
                DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese su Usuario.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUser.Focus();
            }
        }

        private void Limpiar() {
            txtPassword.Clear();
            txtUser.Clear();
            Program.IdEmpleado = 0;
            txtUser.Focus();
            lblCargo.Text = "";
            lblDni.Text = "";
            lblEmpleado.Text = "";
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            //FrmListadoClientes C = new FrmListadoClientes();
            //C.Show();
            //se localiza el formulario buscandolo entre los forms abiertos.
            this.Close(); //con este Close evitamos que hayan dos ventanas de Registrar Usuario.
            Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is FrmListadoEmpleados);
            if (frm != null)
            {
                //si la instancia existe la pongo en primer plano.
                frm.BringToFront();
                return;
            }
            //sino existe la instancia se crea una nueva.
            frm = new FrmListadoEmpleados();
            frm.Show();
        }

        private void btnListaUsuarios_Click(object sender, EventArgs e)
        {
            FrmListadoUsuarios C = new FrmListadoUsuarios();
            //if (dataGridView1.SelectedRows.Count > 0)
            //    Program.Evento = 1;
            //else
            //    Program.Evento = 0;
            //dataGridView1.ClearSelection();

            Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is FrmListadoUsuarios);
            if (frm != null)
            {
                //si la instancia existe la pongo en primer plano
                frm.BringToFront();
                return;
            }
            //sino existe la instancia se crea una nueva
            frm = new FrmListadoUsuarios();
            frm.Show();

            //C.Show();
            //}
        }
    }
}
