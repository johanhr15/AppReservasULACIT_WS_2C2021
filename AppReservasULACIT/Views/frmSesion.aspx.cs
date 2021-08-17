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
    public partial class frmSesion : System.Web.UI.Page
    {
        IEnumerable<Sesion> sesiones = new ObservableCollection<Sesion>();
        SesionManager sesionManager = new SesionManager();

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
                sesiones = await sesionManager.ObtenerSesiones(Session["Token"].ToString());
                gvSesiones.DataSource = sesiones.ToList();
                gvSesiones.DataBind();
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
                resultado = await sesionManager.Eliminar(lblCodigoEliminar.Text, Session["Token"].ToString());
                if (!string.IsNullOrEmpty(resultado))
                {
                    ltrModalMensaje.Text = "Sesion eliminada";
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
            lblResultado.Text = "";
            lblResultado.Visible = false;

            try
            {
                if (Page.IsValid)
                {
                    if (string.IsNullOrEmpty(txtCodigoMant.Text))//INSERTAR
                    {
                        Sesion sesion = new Sesion()
                        {
                            USU_CODIGO = Convert.ToInt32(txtUsuCodigoMant.Text),
                            SES_FEC_HORA_INICIO = Convert.ToDateTime(txtFechaInicioMant.Text),
                            SES_FEC_HORA_FIN = Convert.ToDateTime(txtFechaFinMant.Text),
                            SES_ESTADO = Convert.ToChar(txtEstado.Text)
                        };

                        Sesion respuestaSesion = await sesionManager.Ingresar(sesion, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(respuestaSesion.SES_ESTADO.ToString()))
                        {
                            lblResultado.Text = "Hotel ingresado con exito";
                            lblResultado.Visible = true;
                            lblResultado.ForeColor = Color.Green;
                            InicializarControles();
                        }
                    }
                    else//MODIFICAR
                    {
                        Sesion sesion = new Sesion()
                        {
                            SES_CODIGO = Convert.ToInt32(txtCodigoMant.Text),
                            USU_CODIGO = Convert.ToInt32(txtUsuCodigoMant.Text),
                            SES_FEC_HORA_INICIO = Convert.ToDateTime(txtFechaInicioMant.Text),
                            SES_FEC_HORA_FIN = Convert.ToDateTime(txtFechaFinMant.Text),
                            SES_ESTADO = Convert.ToChar(txtEstado.Text)
                        };

                        Sesion respuestaSesion = await sesionManager.Actualizar(sesion, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(respuestaSesion.SES_ESTADO.ToString()))
                        {
                            lblResultado.Text = "Sesion modificada con exito";
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
            ltrTituloMantenimiento.Text = "Nueva sesion";
            lblResultado.Text = string.Empty;
            txtCodigoMant.Text = string.Empty;
            txtUsuCodigoMant.Text = string.Empty;
            txtFechaInicioMant.Text = string.Empty;
            txtFechaFinMant.Text = string.Empty;
            txtEstado.Text = string.Empty;

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

        protected void gvSesiones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow fila = gvSesiones.Rows[index];

            switch (e.CommandName)
            {
                case "Modificar":
                    ltrTituloMantenimiento.Text = "Modificar hotel";
                    txtCodigoMant.Text = fila.Cells[0].Text;
                    txtUsuCodigoMant.Text = fila.Cells[1].Text;
                    txtFechaInicioMant.Text = fila.Cells[2].Text;
                    txtFechaFinMant.Text = fila.Cells[3].Text;
                    txtEstado.Text = fila.Cells[4].Text;
                    ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
                    break;
                case "Eliminar":
                    lblCodigoEliminar.Text = fila.Cells[0].Text;
                    lblCodigoEliminar.Visible = false;
                    ltrModalMensaje.Text = "Confirme que desea eliminar la sesion " + fila.Cells[0].Text + "-" + fila.Cells[1].Text;
                    ScriptManager.RegisterStartupScript(this,
               this.GetType(), "LaunchServerSide", "$(function() {openModal(); } );", true);

                    break;
                default:
                    break;
            }
        }
    }
}