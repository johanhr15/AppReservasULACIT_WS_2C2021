using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppReservasULACIT.Controllers;
using AppReservasULACIT.Models;

namespace AppReservasULACIT
{
    public partial class frmRegistro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnFechaNac_Click(object sender, EventArgs e)
        {
            cldFechaNacimiento.Visible = true;
        }
        protected void cldFechaNacimiento_SelectionChanged(object sender, EventArgs e)
        {
            txtFechaNacimiento.Text = cldFechaNacimiento.SelectedDate.ToString("dd/MM/yyyy");
            cldFechaNacimiento.Visible = false;
        }
        protected async void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    UsuarioManager usuarioManager = new UsuarioManager();



                    Usuario usuario = new Usuario()
                    {
                        USU_IDENTIFICACION = txtIdentificacion.Text,
                        USU_NOMBRE = txtNombre.Text,
                        USU_EMAIL = txtEmail.Text,
                        USU_FEC_NAC = Convert.ToDateTime(txtFechaNacimiento.Text),
                        USU_TELEFONO = txtTelefono.Text,
                        USU_PASSWORD = txtPassword.Text,
                        USU_ESTADO = "A"
                    };

                    Usuario usuarioRegistrado = await usuarioManager.Registrar(usuario);



                    if (!string.IsNullOrEmpty(usuario.USU_IDENTIFICACION))
                        Response.Redirect("frmLogin.aspx");
                    else
                    {
                        lblStatus.Text = "Hubo un error al registrar el usuario.";
                        lblStatus.Visible = true;
                    }
                }
                catch (Exception ex)
                {

                    lblStatus.Text = "Hubo un error al registrar el usuario.";
                    lblStatus.Visible = true;
                }
            }
        }
    }
}