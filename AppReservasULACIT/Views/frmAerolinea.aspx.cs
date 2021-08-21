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
    public partial class frmAerolinea : System.Web.UI.Page
    {
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
                aerolineas = await aerolineaManager.ObtenerAerolineas(Session["Token"].ToString());
                gvAerolineas.DataSource = aerolineas.ToList();
                gvAerolineas.DataBind();
            }
            catch (Exception exc)
            {
                lblStatus.Text = "Hubo un error al cargar la lista de servicios. Detalle: " + exc.Message;
                lblStatus.Visible = true;
            }
            try
            {
                aeropuertos = await aeropuertoManager.ObtenerAeropuertos(Session["Token"].ToString());
                ddlSede.DataSource = aeropuertos.ToList();
                ddlSede.DataBind();
                ddlSede.DataTextField = "ARP_PAIS";
                ddlSede.DataValueField = "ARP_PAIS";
                ddlSede.DataBind();
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
                resultado = await aerolineaManager.Eliminar(lblCodigoEliminar.Text, Session["Token"].ToString());
                if (!string.IsNullOrEmpty(resultado))
                {
                    ltrModalMensaje.Text = "Aerolinea eliminada";
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
                        try
                        {
                            if (txtTelefonoMant.Text.Length == 8 && txtCorreoMant.Text.Contains("@") && txtSitioWeb.Text.Contains(".com"))
                            {
                                Aerolinea aerolinea = new Aerolinea()
                                {
                                    AER_NOMBRE = txtNombreMant.Text,
                                    AER_TELEFONO = txtTelefonoMant.Text,
                                    AER_CORREO = txtCorreoMant.Text,
                                    AER_SITIO_WEB = txtSitioWeb.Text,
                                    AER_SEDE = ddlSede.SelectedValue
                                };

                                Aerolinea respuestaAerolinea = await aerolineaManager.Ingresar(aerolinea, Session["Token"].ToString());

                                if (!string.IsNullOrEmpty(respuestaAerolinea.AER_NOMBRE))
                                {
                                    lblResultado.Text = "Aerolinea ingresado con exito";
                                    lblResultado.Visible = true;
                                    lblResultado.ForeColor = Color.Green;
                                    InicializarControles();
                                }
                            }
                            else
                            {
                                lblResultado.Text = "La informacion ingresada No es Valida";
                                lblResultado.Visible = true;
                                lblResultado.ForeColor = Color.Red;
                                InicializarControles();
                            }
                        }
                        catch
                        {
                            lblResultado.Text = "La informacion ingresada No es Valida";
                            lblResultado.Visible = true;
                            lblResultado.ForeColor = Color.Red;
                            InicializarControles();
                        }
                    }

                    else//MODIFICAR
                    {
                        try
                        {
                            if (txtTelefonoMant.Text.Length < 50 && txtTelefonoMant.Text.Length > 7 && txtCorreoMant.Text.Contains("@") && txtSitioWeb.Text.Contains(".com"))
                            {
                                Aerolinea aerolinea = new Aerolinea()
                                {
                                    AER_CODIGO = Convert.ToInt32(txtCodigoMant.Text),
                                    AER_NOMBRE = txtNombreMant.Text,
                                    AER_TELEFONO = txtTelefonoMant.Text,
                                    AER_CORREO = txtCorreoMant.Text,
                                    AER_SITIO_WEB = txtSitioWeb.Text,
                                    AER_SEDE = ddlSede.SelectedValue
                                };

                                Aerolinea respuestaAerolinea = await aerolineaManager.Actualizar(aerolinea, Session["Token"].ToString());

                                if (!string.IsNullOrEmpty(respuestaAerolinea.AER_NOMBRE))
                                {
                                    lblResultado.Text = "Aerolinea modificada con exito";
                                    lblResultado.Visible = true;
                                    lblResultado.ForeColor = Color.Green;
                                    InicializarControles();
                                }
                            }
                            else
                            {
                                lblResultado.Text = "La informacion ingresada No es Valida";
                                lblResultado.Visible = true;
                                lblResultado.ForeColor = Color.Red;
                                InicializarControles();
                            }
                        }
                        catch (Exception exc)
                        {
                            lblResultado.Text = "La informacion ingresada No es Valida";
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
            ltrTituloMantenimiento.Text = "Nueva aerolinea";
            lblResultado.Text = string.Empty;
            txtCodigoMant.Text = string.Empty;
            txtNombreMant.Text = string.Empty;
            txtTelefonoMant.Text = string.Empty;
            txtCorreoMant.Text = string.Empty;
            txtSitioWeb.Text = string.Empty;

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

        protected void gvAerolineas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow fila = gvAerolineas.Rows[index];

            switch (e.CommandName)
            {
                case "Modificar":
                    ltrTituloMantenimiento.Text = "Modificar aerolinea";
                    txtCodigoMant.Text = fila.Cells[0].Text;
                    txtNombreMant.Text = fila.Cells[1].Text;
                    txtTelefonoMant.Text = fila.Cells[2].Text;
                    txtCorreoMant.Text = fila.Cells[3].Text;
                    txtSitioWeb.Text = fila.Cells[4].Text;
                    ddlSede.SelectedValue = fila.Cells[5].Text;

                    ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
                    break;
                case "Eliminar":
                    lblResultado.Text = "";
                    lblResultado.Visible = false;
                    lblCodigoEliminar.Text = fila.Cells[0].Text;
                    lblCodigoEliminar.Visible = false;
                    ltrModalMensaje.Text = "Confirme que desea eliminar la aerolinea " + fila.Cells[0].Text + "-" + fila.Cells[1].Text;
                    ScriptManager.RegisterStartupScript(this,
               this.GetType(), "LaunchServerSide", "$(function() {openModal(); } );", true);

                    break;
                default:
                    break;
            }
        }
    }
}