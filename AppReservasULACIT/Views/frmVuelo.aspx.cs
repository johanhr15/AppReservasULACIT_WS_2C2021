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
    public partial class frmVuelo : System.Web.UI.Page
    {
        IEnumerable<Vuelo> vuelos = new ObservableCollection<Vuelo>();
        VueloManager vueloManager = new VueloManager();
        IEnumerable<Aerolinea> aerolineas = new ObservableCollection<Aerolinea>();
        AerolineaManager aerolineaManager = new AerolineaManager();
        IEnumerable<Aeropuerto> aeropuertos = new ObservableCollection<Aeropuerto>();
        AeropuertoManager aeropuertoManager = new AeropuertoManager();


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
                vuelos = await vueloManager.ObtenerVuelos(Session["Token"].ToString());
                gvVuelos.DataSource = vuelos.ToList();
                gvVuelos.DataBind();
            }
            catch (Exception exc)
            {
                lblStatus.Text = "Hubo un error al cargar la lista de servicios. Detalle: " + exc.Message;
                lblStatus.Visible = true;
            }
            try
            {
                aerolineas = await aerolineaManager.ObtenerAerolineas(Session["Token"].ToString());
                ddlCodigoAerolinea.DataSource = aerolineas.ToList();
                ddlCodigoAerolinea.DataBind();
                ddlCodigoAerolinea.DataTextField = "AER_NOMBRE";
                ddlCodigoAerolinea.DataValueField = "AER_CODIGO";
                ddlCodigoAerolinea.DataBind();
            }
            catch (Exception exc)
            {

                lblStatus.Text = "Hubo un error al cargar la lista de servicios. Detalle: " + exc.Message;
                lblStatus.Visible = true;
            }
            try
            {
                aeropuertos = await aeropuertoManager.ObtenerAeropuertos(Session["Token"].ToString());
                ddlCodigoAeropuerto.DataSource = aeropuertos.ToList();
                ddlCodigoAeropuerto.DataBind();
                ddlCodigoAeropuerto.DataTextField = "ARP_PAIS";
                ddlCodigoAeropuerto.DataValueField = "ARP_CODIGO";
                ddlCodigoAeropuerto.DataBind();
                ddlCodigoAeropuerto2.DataSource = aeropuertos.ToList();
                ddlCodigoAeropuerto2.DataBind();
                ddlCodigoAeropuerto2.DataTextField = "ARP_PAIS";
                ddlCodigoAeropuerto2.DataValueField = "ARP_CODIGO";
                ddlCodigoAeropuerto2.DataBind();
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
                resultado = await vueloManager.Eliminar(lblCodigoEliminar.Text, Session["Token"].ToString());
                if (!string.IsNullOrEmpty(resultado))
                {
                    ltrModalMensaje.Text = "Vuelo eliminado";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { openModal(); });", true);
                    InicializarControles();
                }
            }
            catch (Exception exc)
            {
                lblStatus.Text = "Hubo un error al cargar la lista de servicios. Detalle: " + exc.Message;
                lblStatus.Visible = true;
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
                        try
                        { 
                            if (Convert.ToDateTime(txtVueHoraPartida.Text) >= DateTime.Now && Convert.ToDateTime(txtVueHoraPartida.Text) < Convert.ToDateTime(txtVueHoraLlegada.Text))
                            {
                                Vuelo vuelo = new Vuelo()
                                {
                                    AER_CODIGO = Convert.ToInt32(ddlCodigoAerolinea.SelectedValue),
                                    VUE_ORI_CODIGO = Convert.ToInt32(ddlCodigoAeropuerto.SelectedValue),
                                    VUE_DES_CODIGO = Convert.ToInt32(ddlCodigoAeropuerto2.SelectedValue),
                                    VUE_TERMINAL = ddlTerminal.SelectedValue,
                                    VUE_PUERTA = ddlPuerta.SelectedValue,
                                    VUE_HORA_PARTIDA = Convert.ToDateTime(txtVueHoraPartida.Text),
                                    VUE_HORA_LLEGADA = Convert.ToDateTime(txtVueHoraLlegada.Text)
                                };

                                Vuelo respuestaVuelo = await vueloManager.Ingresar(vuelo, Session["Token"].ToString());

                                if (!string.IsNullOrEmpty(respuestaVuelo.VUE_TERMINAL))
                                {
                                    lblResultado.Text = "Vuelo ingresado con exito";
                                    lblResultado.Visible = true;
                                    lblResultado.ForeColor = Color.Green;
                                    InicializarControles();
                                }
                            }
                            else
                            {
                                lblResultado.Text = "La Fecha Ingresada No es Valida";
                                lblResultado.Visible = true;
                                lblResultado.ForeColor = Color.Red;
                                InicializarControles();
                            }
                        }
                        catch
                        {
                            lblResultado.Text = "La Fecha Ingresada No es Valida";
                            lblResultado.Visible = true;
                            lblResultado.ForeColor = Color.Red;
                            InicializarControles();
                        }
                    }
                    else//MODIFICAR
                    {
                        try
                        {
                            if (Convert.ToDateTime(txtVueHoraPartida.Text) >= DateTime.Now && Convert.ToDateTime(txtVueHoraPartida.Text) < Convert.ToDateTime(txtVueHoraLlegada.Text) && !string.IsNullOrEmpty(txtVueHoraPartida.Text) && !string.IsNullOrEmpty(txtVueHoraLlegada.Text))
                            {
                                Vuelo vuelo = new Vuelo()
                                {
                                    VUE_CODIGO = Convert.ToInt32(txtCodigoMant.Text),
                                    AER_CODIGO = Convert.ToInt32(ddlCodigoAerolinea.SelectedValue),
                                    VUE_ORI_CODIGO = Convert.ToInt32(ddlCodigoAeropuerto.SelectedValue),
                                    VUE_DES_CODIGO = Convert.ToInt32(ddlCodigoAeropuerto2.SelectedValue),
                                    VUE_TERMINAL = ddlTerminal.SelectedValue,
                                    VUE_PUERTA = ddlPuerta.SelectedValue,
                                    VUE_HORA_PARTIDA = Convert.ToDateTime(txtVueHoraPartida.Text),
                                    VUE_HORA_LLEGADA = Convert.ToDateTime(txtVueHoraLlegada.Text)
                                };

                                Vuelo respuestaVuelo = await vueloManager.Actualizar(vuelo, Session["Token"].ToString());

                                if (!string.IsNullOrEmpty(respuestaVuelo.VUE_TERMINAL))
                                {
                                    lblResultado.Text = "Vuelo modificado con exito";
                                    lblResultado.Visible = true;
                                    lblResultado.ForeColor = Color.Green;
                                    InicializarControles();
                                }
                            }
                            else
                            {
                                lblResultado.Text = "La Fecha Ingresada No es Valida";
                                lblResultado.Visible = true;
                                lblResultado.ForeColor = Color.Red;
                                InicializarControles();

                            }
                        }

                        catch (Exception exc)
                        {
                            lblResultado.Text = "La Fecha Ingresada No es Valida";
                            lblResultado.Visible = true;
                            lblResultado.ForeColor = Color.Red;
                            InicializarControles();
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                lblStatus.Text = "Hubo un error en la operacion. Detalles: " + exc.Message;
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
            ltrTituloMantenimiento.Text = "Nuevo Vuelo";
            lblResultado.Text = string.Empty;
            txtCodigoMant.Text = string.Empty;

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

        protected void gvVuelos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow fila = gvVuelos.Rows[index];

            switch (e.CommandName)
            {
                case "Modificar":
                    ltrTituloMantenimiento.Text = "Modificar vuelo";
                    txtCodigoMant.Text = fila.Cells[0].Text;
                    ddlCodigoAerolinea.SelectedValue = fila.Cells[1].Text;
                    ddlCodigoAeropuerto.SelectedValue = fila.Cells[2].Text;
                    ddlCodigoAeropuerto2.SelectedValue = fila.Cells[3].Text;
                    ddlTerminal.SelectedValue = fila.Cells[4].Text;
                    ddlPuerta.SelectedValue = fila.Cells[5].Text;
                    txtVueHoraPartida.Text = fila.Cells[6].Text;
                    txtVueHoraLlegada.Text = fila.Cells[7].Text;

                    ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
                    break;
                case "Eliminar":
                    lblResultado.Text = "";
                    lblResultado.Visible = false;
                    lblCodigoEliminar.Text = fila.Cells[0].Text;
                    lblCodigoEliminar.Visible = false;
                    ltrModalMensaje.Text = "Confirme que desea eliminar el vuelo " + fila.Cells[0].Text + "-" + fila.Cells[1].Text;
                    ScriptManager.RegisterStartupScript(this,
               this.GetType(), "LaunchServerSide", "$(function() {openModal(); } );", true);

                    break;
                default:
                    break;
            }
        }
    }
}