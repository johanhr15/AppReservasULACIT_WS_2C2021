using AppReservasULACIT.Controllers;
using AppReservasULACIT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppReservasULACIT.Views
{
    public partial class frmReservaVuelo : System.Web.UI.Page
    {

        IEnumerable<ReservaVuelo> reservaVuelos = new ObservableCollection<ReservaVuelo>();
        ReservaVueloManager reservaVueloManager = new ReservaVueloManager();
        IEnumerable<Usuario> usuarios = new ObservableCollection<Usuario>();
        UsuarioManager usuarioManager = new UsuarioManager();
        IEnumerable<Agencia> agencias = new ObservableCollection<Agencia>();
        AgenciaManager agenciaManager = new AgenciaManager();

        
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
                reservaVuelos = await reservaVueloManager.ObtenerReservaVuelos(Session["Token"].ToString());
                gvReservaVuelos.DataSource = reservaVuelos.ToList();
                gvReservaVuelos.DataBind();
            }
            catch (Exception exc)
            {
                lblStatus.Text = "Hubo un error al cargar la lista de servicios. Detalle: " + exc.Message;
                lblStatus.Visible = true;
            }

            try
            {
                usuarios = await usuarioManager.ObtenerUsuarios(Session["Token"].ToString());
                ddlCodigoUsuario.DataSource = usuarios.ToList();
                ddlCodigoUsuario.DataBind();
                ddlCodigoUsuario.DataTextField = "USU_NOMBRE";
                ddlCodigoUsuario.DataValueField = "USU_CODIGO";
                ddlCodigoUsuario.DataBind();
            }
            catch (Exception exc)
            {
                lblStatus.Text = "Hubo un error al cargar la lista de servicios. Detalle: " + exc.Message;
                lblStatus.Visible = true;
            }

            try
            {
                agencias = await agenciaManager.ObtenerAgencias(Session["Token"].ToString());
                ddlCodigoAgencia.DataSource = agencias.ToList();
                ddlCodigoAgencia.DataBind();
                ddlCodigoAgencia.DataTextField = "AGE_NOMBRE";
                ddlCodigoAgencia.DataValueField = "AGE_CODIGO";
                ddlCodigoAgencia.DataBind();
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
                resultado = await reservaVueloManager.Eliminar(lblCodigoEliminar.Text, Session["Token"].ToString());
                if (!string.IsNullOrEmpty(resultado))
                {
                    ltrModalMensaje.Text = "Reserva Vuelo eliminado";
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
                        ReservaVuelo reservaVuelo = new ReservaVuelo()
                        {
                            USU_CODIGO = Convert.ToInt32(ddlCodigoUsuario.SelectedValue),
                            AGE_CODIGO = Convert.ToInt32(ddlCodigoAgencia.SelectedValue),
                            RVU_MONEDA = ddlReservaVueloMoneda.SelectedValue,
                            RVU_PRECIO_TOTAL = Convert.ToDecimal(txtPrecioTotalReservaVuelo.Text),
                            RVU_FECHA = Convert.ToDateTime(txtFecha.Text),
                        };

                        ReservaVuelo respuestaReservaVuelo = await reservaVueloManager.Ingresar(reservaVuelo, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(respuestaReservaVuelo.RVU_MONEDA))
                        {
                            lblResultado.Text = "Reserva de Vuelo ingresada con exito";
                            lblResultado.Visible = true;
                            lblResultado.ForeColor = Color.Green;
                            InicializarControles();
                        }
                    }
                    else//MODIFICAR
                    {
                        ReservaVuelo reservaVuelo = new ReservaVuelo()
                        {
                            RVU_CODIGO = Convert.ToInt32(txtCodigoMant.Text),
                            USU_CODIGO = Convert.ToInt32(ddlCodigoUsuario.SelectedValue),
                            AGE_CODIGO = Convert.ToInt32(ddlCodigoAgencia.SelectedValue),
                            RVU_MONEDA = ddlReservaVueloMoneda.SelectedValue,
                            RVU_PRECIO_TOTAL = Convert.ToDecimal(txtPrecioTotalReservaVuelo.Text),
                            RVU_FECHA = Convert.ToDateTime(txtFecha.Text),
                        };

                        ReservaVuelo respuestaReservaVuelo = await reservaVueloManager.Actualizar(reservaVuelo, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(respuestaReservaVuelo.RVU_MONEDA))
                        {
                            lblResultado.Text = "Reserva de Vuelo ingresada con exito";
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
            ltrTituloMantenimiento.Text = "Nueva Reserva de Vuelo";
            lblResultado.Text = string.Empty;
            txtCodigoMant.Text = string.Empty;
            txtPrecioTotalReservaVuelo.Text = string.Empty;

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

        protected void gvReservaVuelos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow fila = gvReservaVuelos.Rows[index];

            switch (e.CommandName)
            {
                case "Modificar":
                    lblResultado.Text = "";
                    lblResultado.Visible = false;
                    ltrTituloMantenimiento.Text = "Modificar Reserva de Vuelos";
                    txtCodigoMant.Text = fila.Cells[0].Text;
                    ddlCodigoUsuario.SelectedValue = fila.Cells[1].Text;
                    ddlCodigoAgencia.SelectedValue = fila.Cells[2].Text;
                    ddlReservaVueloMoneda.SelectedValue = fila.Cells[3].Text;
                    txtPrecioTotalReservaVuelo.Text = fila.Cells[4].Text;
                    txtFecha.Text = fila.Cells[5].Text;

                    ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
                    break;
                case "Eliminar":
                    lblResultado.Text = "";
                    lblResultado.Visible = false;
                    lblCodigoEliminar.Text = fila.Cells[0].Text;
                    lblCodigoEliminar.Visible = false;
                    ltrModalMensaje.Text = "Confirme que desea eliminar la Reserva de Vuelo " + fila.Cells[0].Text + "-" + fila.Cells[1].Text;
                    ScriptManager.RegisterStartupScript(this,
               this.GetType(), "LaunchServerSide", "$(function() {openModal(); } );", true);

                    break;
                default:
                    break;
            }
        }
    }
}