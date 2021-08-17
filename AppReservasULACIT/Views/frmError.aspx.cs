using AppReservasULACIT.Controllers;
using AppReservasULACIT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppReservasULACIT.Views
{
    public partial class frmError : System.Web.UI.Page
    {
        IEnumerable<Error> errores = new ObservableCollection<Error>();
        ErrorManager errorManager = new ErrorManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodigoUsuario"] == null)
                {
                    Response.Redirect("~/frmLogin.aspx");
                }
                else
                    InicializarControles();
            }
        }
        private async void InicializarControles()
        {
            try
            {
                errores = await errorManager.ObtenerErrores(Session["Token"].ToString());
                gvErrores.DataSource = errores.ToList();
                gvErrores.DataBind();
            }
            catch (Exception exc)
            {
                lblStatus.Text = "Hubo un error al cargar la lista de servicios. Detalle: " + exc.Message;
                lblStatus.Visible = true;
            }
        }

        protected async void btnAceptarModal_Click(object sender, EventArgs e)
        {
            try
            {
                string resultado = string.Empty;
                resultado = await errorManager.Eliminar(lblCodigoEliminar.Text, Session["Token"].ToString());
                if (!string.IsNullOrEmpty(resultado))
                {
                    ltrModalMensaje.Text = "Error eliminado";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { openModal(); });", true);
                    InicializarControles();
                }
            }
            catch (Exception exc)
            {
                lblStatus.Text = "Hubo un error al cargar la lista de servicios. Detalle: " + exc.Message;
                lblStatus.Visible = true;
            }
        }

        protected void btnCancelarModal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { CloseModal(); });", true);
        }

        protected async void btnAceptarMant_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    if (string.IsNullOrEmpty(txtCodigoMant.Text))//INSERTAR
                    {
                        Error error = new Error()
                        {
                            USU_CODIGO = Convert.ToInt32(txtUsuCodigoMant.Text),
                            ERR_FEC_HORA = Convert.ToDateTime(txtFechaHoraMant.Text),
                            ERR_FUENTE = txtFuenteMant.Text,
                            ERR_NUMERO = txtNumero.Text,
                            ERR_DESCRIPCION = txtDescripcion.Text,
                            ERR_VISTA = txtVista.Text,
                            ERR_ACCION = txtAccion.Text,

                        };

                        Error respuestaError = await errorManager.Ingresar(error, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(respuestaError.ERR_DESCRIPCION))
                        {
                            lblResultado.Text = "Error ingresado con exito";
                            lblResultado.Visible = true;
                            lblResultado.ForeColor = Color.Green;
                            InicializarControles();
                        }
                    }
                    else//MODIFICAR
                    {
                        Error error = new Error()
                        {
                            ERR_CODIGO = Convert.ToInt32(txtCodigoMant.Text),
                            USU_CODIGO = Convert.ToInt32(txtUsuCodigoMant.Text),
                            ERR_FEC_HORA = Convert.ToDateTime(txtFechaHoraMant.Text),
                            ERR_FUENTE = txtFuenteMant.Text,
                            ERR_NUMERO = txtNumero.Text,
                            ERR_DESCRIPCION = txtDescripcion.Text,
                            ERR_VISTA = txtVista.Text,
                            ERR_ACCION = txtAccion.Text,

                        };

                        Error respuestaError = await errorManager.Actualizar(error, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(respuestaError.ERR_DESCRIPCION))
                        {
                            lblResultado.Text = "Error modificado con exito";
                            lblResultado.Visible = true;
                            lblResultado.ForeColor = Color.Green;
                            InicializarControles();
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                lblStatus.Text = "Hubo un error en la operacion. Detalle: " + exc.Message;
                lblStatus.Visible = true;
            }
        }

        protected void btnCancelarMant_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
               "$(function() {CloseMantenimiento(); } );", true);
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            ltrTituloMantenimiento.Text = "Nuevo error";
            lblResultado.Text = string.Empty;
            txtCodigoMant.Text = string.Empty;
            txtUsuCodigoMant.Text = string.Empty;
            txtFechaHoraMant.Text = string.Empty;
            txtFuenteMant.Text = string.Empty;
            txtNumero.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtVista.Text = string.Empty;
            txtAccion.Text = string.Empty;

            LimpiarControles();

            ScriptManager.RegisterStartupScript(this,
                 this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
        }

        private void LimpiarControles()
        {
            foreach (var item in Page.Controls)
            {
                if (item is TextBox)
                    ((TextBox)item).Text = String.Empty;

            }
        }

        protected void gvErrores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow fila = gvErrores.Rows[index];

            switch (e.CommandName)
            {
                case "Modificar":
                    ltrTituloMantenimiento.Text = "Modificar error";
                    txtCodigoMant.Text = fila.Cells[0].Text;
                    txtUsuCodigoMant.Text = fila.Cells[1].Text;
                    txtFechaHoraMant.Text = fila.Cells[2].Text;
                    txtFuenteMant.Text = fila.Cells[3].Text;
                    txtNumero.Text = fila.Cells[4].Text;
                    txtDescripcion.Text = fila.Cells[5].Text;
                    txtVista.Text = fila.Cells[6].Text;
                    txtAccion.Text = fila.Cells[7].Text;
                    ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
                    break;
                case "Eliminar":
                    lblCodigoEliminar.Text = fila.Cells[0].Text;
                    lblCodigoEliminar.Visible = false;
                    ltrModalMensaje.Text = "Confirme que desea eliminar el error " + fila.Cells[0].Text + "-" + fila.Cells[1].Text;
                    ScriptManager.RegisterStartupScript(this,
               this.GetType(), "LaunchServerSide", "$(function() {openModal(); } );", true);

                    break;
                default:
                    break;
            }
        }
    }
}