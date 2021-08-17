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

    public partial class frmEstadistica : System.Web.UI.Page
    {
        IEnumerable<Estadistica> estadisticas = new ObservableCollection<Estadistica>();
        EstadisticaManager estadisticaManager = new EstadisticaManager();

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
                estadisticas = await estadisticaManager.ObtenerEstadisticas(Session["Token"].ToString());
                gvEstadisticas.DataSource = estadisticas.ToList();
                gvEstadisticas.DataBind();
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
                resultado = await estadisticaManager.Eliminar(lblCodigoEliminar.Text, Session["Token"].ToString());
                if (!string.IsNullOrEmpty(resultado))
                {
                    ltrModalMensaje.Text = "Estadistica eliminada";
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
                        Estadistica estadistica = new Estadistica()
                        {
                            USU_CODIGO = Convert.ToInt32(txtUsuCodigoMant.Text),
                            EST_FEC_HORA = Convert.ToDateTime(txtFechaMant.Text),
                            EST_NAVEGADOR = txtNavegadorMant.Text,
                            EST_PLATAFORMA_DISPOSITIVO = txtPlataforma.Text,
                            EST_FABRICANTE_DISPOSTIVO = txtFabricante.Text,
                            EST_VISTA = txtVista.Text,
                            EST_ACCION = txtAccion.Text
                        };

                        Estadistica respuestaEstadistica = await estadisticaManager.Ingresar(estadistica, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(respuestaEstadistica.EST_NAVEGADOR))
                        {
                            lblResultado.Text = "Estadistica ingresada con exito";
                            lblResultado.Visible = true;
                            lblResultado.ForeColor = Color.Green;
                            InicializarControles();
                        }
                    }
                    else//MODIFICAR
                    {
                        Estadistica estadistica = new Estadistica()
                        {
                            EST_CODIGO = Convert.ToInt32(txtCodigoMant.Text),
                            USU_CODIGO = Convert.ToInt32(txtUsuCodigoMant.Text),
                            EST_FEC_HORA = Convert.ToDateTime(txtFechaMant.Text),
                            EST_NAVEGADOR = txtNavegadorMant.Text,
                            EST_PLATAFORMA_DISPOSITIVO = txtPlataforma.Text,
                            EST_FABRICANTE_DISPOSTIVO = txtFabricante.Text,
                            EST_VISTA = txtVista.Text,
                            EST_ACCION = txtAccion.Text
                        };

                        Estadistica respuestaEstadistica = await estadisticaManager.Actualizar(estadistica, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(respuestaEstadistica.EST_NAVEGADOR))
                        {
                            lblResultado.Text = "Estadistica modificada con exito";
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
            ltrTituloMantenimiento.Text = "Nueva estadistica";
            lblResultado.Text = string.Empty;
            txtCodigoMant.Text = string.Empty;
            txtUsuCodigoMant.Text = string.Empty;
            txtFechaMant.Text = string.Empty;
            txtPlataforma.Text = string.Empty;
            txtFabricante.Text = string.Empty;
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

        protected void gvEstadisticas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow fila = gvEstadisticas.Rows[index];

            switch (e.CommandName)
            {
                case "Modificar":
                    ltrTituloMantenimiento.Text = "Modificar estadistica";
                    txtCodigoMant.Text = fila.Cells[0].Text;
                    txtUsuCodigoMant.Text = fila.Cells[1].Text;
                    txtFechaMant.Text = fila.Cells[2].Text;
                    txtNavegadorMant.Text = fila.Cells[3].Text;
                    txtPlataforma.Text = fila.Cells[4].Text;
                    txtFabricante.Text = fila.Cells[5].Text;
                    txtVista.Text = fila.Cells[6].Text;
                    txtAccion.Text = fila.Cells[7].Text;
                    ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
                    break;
                case "Eliminar":
                    lblCodigoEliminar.Text = fila.Cells[0].Text;
                    lblCodigoEliminar.Visible = false;
                    ltrModalMensaje.Text = "Confirme que desea eliminar la estadistica " + fila.Cells[0].Text + "-" + fila.Cells[1].Text;
                    ScriptManager.RegisterStartupScript(this,
               this.GetType(), "LaunchServerSide", "$(function() {openModal(); } );", true);

                    break;
                default:
                    break;
            }
        }
    }
}