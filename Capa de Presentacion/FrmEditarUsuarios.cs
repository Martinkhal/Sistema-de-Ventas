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
    public partial class FrmEditarUsuarios : DevComponents.DotNetBar.Metro.MetroForm
    {
        clsUsuarios U = new clsUsuarios();
        public FrmEditarUsuarios()
        {
            InitializeComponent();
        }

        private void FrmEditarUsuarios_Load(object sender, EventArgs e)
        {

        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                U.IdEmpleado = Convert.ToInt32(txtIdUsuario.Text);
                U.User = txtUsuario.Text;
                U.Password = txtContraseña.Text;

                DevComponents.DotNetBar.MessageBoxEx.Show(U.MantenimientoUsuarios(), "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                Limpiar();
                this.Close();//cerramos la ventana una vez que se realizo el guardado del empleado.
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message);
            }
            FrmListadoUsuarios LU = new FrmListadoUsuarios();
            LU.timer1.Start(); //CDA05
        }

        private void Limpiar()
        {
            txtIdUsuario.Clear();
            txtUsuario.Clear();
            txtContraseña.Clear();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (DevComponents.DotNetBar.MessageBoxEx.Show("¿Está Seguro que Desea Eliminar este Usuario?", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                try
                {
                    U.IdEmpleado = Convert.ToInt32(txtIdUsuario.Text);
                    U.User = txtUsuario.Text;
                    U.Password = txtContraseña.Text;

                    DevComponents.DotNetBar.MessageBoxEx.Show(U.EliminarUsuario(), "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    Limpiar();
                    this.Close();//cerramos la ventana una vez que se realizo el eliminado del empleado.
                }
                catch (Exception ex)
                {
                    DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message);
                }
            }
            else
            {
                this.Close();
            }
        }
    }
}
