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
    public partial class frmTiquete : System.Web.UI.Page
    {


        IEnumerable<Tiquete> tiquetes = new ObservableCollection<Tiquete>();
        TiqueteManager tiqueteManager = new TiqueteManager();
        IEnumerable<Aerolinea> aerolineas = new ObservableCollection<Aerolinea>();
        AerolineaManager aerolineaManager = new AerolineaManager();
        IEnumerable<Escala> escalas = new ObservableCollection<Escala>();
        EscalaManager escalaManager = new EscalaManager();
        IEnumerable<Pasajero> pasajeros = new ObservableCollection<Pasajero>();
        PasajeroManager pasajeroManager = new PasajeroManager();
        IEnumerable<Vuelo> vuelos = new ObservableCollection<Vuelo>();
        VueloManager vueloManager = new VueloManager();
        IEnumerable<ReservaVuelo> reservaVuelos = new ObservableCollection<ReservaVuelo>();
        ReservaVueloManager reservaVueloManager = new ReservaVueloManager();
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
                tiquetes = await tiqueteManager.ObtenerTiquetes(Session["Token"].ToString());
                gvTiquetes.DataSource = tiquetes.ToList();
                gvTiquetes.DataBind();
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
                escalas = await escalaManager.ObtenerEscalas(Session["Token"].ToString());
                ddlCodigoEscala.DataSource = escalas.ToList();
                ddlCodigoEscala.DataBind();
                ddlCodigoEscala.DataTextField = "ESC_TRASBORDO";
                ddlCodigoEscala.DataValueField = "ESC_CODIGO";
                ddlCodigoEscala.DataBind();
            }
            catch (Exception exc)
            {
                lblStatus.Text = "Hubo un error al cargar la lista de servicios. Detalle: " + exc.Message;
                lblStatus.Visible = true;
            }

            try
            {
                pasajeros = await pasajeroManager.ObtenerPasajeros(Session["Token"].ToString());
                ddlCodigoPasajero.DataSource = pasajeros.ToList();
                ddlCodigoPasajero.DataBind();
                ddlCodigoPasajero.DataTextField = "PAS_NOMBRE";
                ddlCodigoPasajero.DataValueField = "PAS_CODIGO";
                ddlCodigoPasajero.DataBind();
            }
            catch (Exception exc)
            {
                lblStatus.Text = "Hubo un error al cargar la lista de servicios. Detalle: " + exc.Message;
                lblStatus.Visible = true;
            }

            try
            {
                vuelos = await vueloManager.ObtenerVuelos(Session["Token"].ToString());
                ddlCodigoVuelo.DataSource = vuelos.ToList();
                ddlCodigoVuelo.DataBind();
                ddlCodigoVuelo.DataTextField = "VUE_TERMINAL";
                ddlCodigoVuelo.DataValueField = "VUE_CODIGO";
                ddlCodigoVuelo.DataBind();
            }
            catch (Exception exc)
            {
                lblStatus.Text = "Hubo un error al cargar la lista de servicios. Detalle: " + exc.Message;
                lblStatus.Visible = true;
            }

            try
            {
                reservaVuelos = await reservaVueloManager.ObtenerReservaVuelos(Session["Token"].ToString());
                ddlCodigoReservaVuelo.DataSource = reservaVuelos.ToList();
                ddlCodigoReservaVuelo.DataBind();
                ddlCodigoReservaVuelo.DataTextField = "RVU_CODIGO";
                ddlCodigoReservaVuelo.DataValueField = "RVU_CODIGO";
                ddlCodigoReservaVuelo.DataBind();
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
                resultado = await tiqueteManager.Eliminar(lblCodigoEliminar.Text, Session["Token"].ToString());
                if (!string.IsNullOrEmpty(resultado))
                {
                    ltrModalMensaje.Text = "Tiquete eliminado";
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
                        Tiquete tiquete = new Tiquete()
                        {
                            AER_CODIGO = Convert.ToInt32(ddlCodigoAerolinea.SelectedValue),
                            ESC_CODIGO = Convert.ToInt32(ddlCodigoEscala.SelectedValue),
                            PAS_CODIGO = Convert.ToInt32(ddlCodigoPasajero.SelectedValue),
                            VUE_CODIGO = Convert.ToInt32(ddlCodigoVuelo.SelectedValue),
                            RVU_CODIGO = Convert.ToInt32(ddlCodigoReservaVuelo.SelectedValue),
                            TIQ_PRECIO = Convert.ToDecimal(txtPrecioTiquete.Text),
                            TIQ_ALIMENTACION = ddlAlimentacion.SelectedValue,
                            TIQ_DEVOLUCION = ddlDevolucion.SelectedValue,
                            TIQ_VISA_REQUERIDA = ddlVisaRequerida.SelectedValue

                        };

                        Tiquete respuestaTiquete = await tiqueteManager.Ingresar(tiquete, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(respuestaTiquete.TIQ_ALIMENTACION))
                        {
                            lblResultado.Text = "Tiquete ingresado con exito";
                            lblResultado.Visible = true;
                            lblResultado.ForeColor = Color.Green;
                            InicializarControles();
                        }
                    }
                    else//MODIFICAR
                    {
                        Tiquete tiquete = new Tiquete()
                        {
                            TIQ_CODIGO = Convert.ToInt32(txtCodigoMant.Text),
                            AER_CODIGO = Convert.ToInt32(ddlCodigoAerolinea.SelectedValue),
                            ESC_CODIGO = Convert.ToInt32(ddlCodigoEscala.SelectedValue),
                            PAS_CODIGO = Convert.ToInt32(ddlCodigoPasajero.SelectedValue),
                            VUE_CODIGO = Convert.ToInt32(ddlCodigoVuelo.SelectedValue),
                            RVU_CODIGO = Convert.ToInt32(ddlCodigoReservaVuelo.SelectedValue),
                            TIQ_PRECIO = Convert.ToDecimal(txtPrecioTiquete.Text),
                            TIQ_ALIMENTACION = ddlAlimentacion.SelectedValue,
                            TIQ_DEVOLUCION = ddlDevolucion.SelectedValue,
                            TIQ_VISA_REQUERIDA = ddlVisaRequerida.SelectedValue
                        };

                        Tiquete respuestaTiquete = await tiqueteManager.Actualizar(tiquete, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(respuestaTiquete.TIQ_ALIMENTACION))
                        {
                            lblResultado.Text = "Tiquete Modificado con exito";
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
            ltrTituloMantenimiento.Text = "Nuevo Tiquete";
            lblResultado.Text = string.Empty;
            txtCodigoMant.Text = string.Empty;
            txtPrecioTiquete.Text = string.Empty;

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

        protected void gvTiquetes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow fila = gvTiquetes.Rows[index];

            switch (e.CommandName)
            {
                case "Modificar":
                    lblResultado.Text = "";
                    lblResultado.Visible = false;
                    ltrTituloMantenimiento.Text = "Modificar Tiquete";
                    txtCodigoMant.Text = fila.Cells[0].Text;
                    ddlCodigoAerolinea.SelectedValue = fila.Cells[1].Text;
                    ddlCodigoEscala.SelectedValue = fila.Cells[2].Text;
                    ddlCodigoPasajero.SelectedValue = fila.Cells[3].Text;
                    ddlCodigoVuelo.SelectedValue = fila.Cells[4].Text;
                    ddlCodigoReservaVuelo.SelectedValue = fila.Cells[5].Text;
                    txtPrecioTiquete.Text = fila.Cells[6].Text;
                    ddlAlimentacion.SelectedValue = fila.Cells[7].Text;
                    ddlAlimentacion.SelectedValue = fila.Cells[8].Text;
                    ddlVisaRequerida.SelectedValue = fila.Cells[9].Text;
                    ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
                    break;
                case "Eliminar":
                    lblResultado.Text = "";
                    lblResultado.Visible = false;
                    lblCodigoEliminar.Text = fila.Cells[0].Text;
                    lblCodigoEliminar.Visible = false;
                    ltrModalMensaje.Text = "Confirme que desea eliminar el Tiquete " + fila.Cells[0].Text + "-" + fila.Cells[1].Text;
                    ScriptManager.RegisterStartupScript(this,
               this.GetType(), "LaunchServerSide", "$(function() {openModal(); } );", true);

                    break;
                default:
                    break;
            }
        }
    }
}