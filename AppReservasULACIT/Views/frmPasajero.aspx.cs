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
    public partial class frmPasajero : System.Web.UI.Page
    {
        IEnumerable<Pasajero> pasajeros = new ObservableCollection<Pasajero>();
        PasajeroManager pasajeroManager = new PasajeroManager();

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
                pasajeros = await pasajeroManager.ObtenerPasajeros(Session["Token"].ToString());
                gvPasajeros.DataSource = pasajeros.ToList();
                gvPasajeros.DataBind();
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
                resultado = await pasajeroManager.Eliminar(lblCodigoEliminar.Text, Session["Token"].ToString());
                if (!string.IsNullOrEmpty(resultado))
                {
                    ltrModalMensaje.Text = "Pasajero eliminado";
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
                        Pasajero pasajero = new Pasajero()
                        {
                            PAS_PASAPORTE = txtPasaporteMant.Text,
                            PAS_NOMBRE = txtNombreMant.Text,
                            PAS_FEC_NACIMIENTO = Convert.ToDateTime(txtFecha.Text),
                            PAS_NACIONALIDAD = txtNacionalidad.Text,
                            PAS_CORREO = txtCorreo.Text,
                            PAS_TELEFONO = txtTelefono.Text
                        };

                        Pasajero respuestaPasajero = await pasajeroManager.Ingresar(pasajero, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(respuestaPasajero.PAS_PASAPORTE))
                        {
                            lblResultado.Text = "Pasajero ingresado con exito";
                            lblResultado.Visible = true;
                            lblResultado.ForeColor = Color.Green;
                            InicializarControles();
                        }
                    }
                    else//MODIFICAR
                    {
                        Pasajero pasajero = new Pasajero()
                        {
                            PAS_CODIGO = Convert.ToInt32(txtCodigoMant.Text),
                            PAS_PASAPORTE = txtPasaporteMant.Text,
                            PAS_NOMBRE = txtNombreMant.Text,
                            PAS_FEC_NACIMIENTO = Convert.ToDateTime(txtFecha.Text),
                            PAS_NACIONALIDAD = txtNacionalidad.Text,
                            PAS_CORREO = txtCorreo.Text,
                            PAS_TELEFONO= txtTelefono.Text

                        };

                        Pasajero respuestaPasajero = await pasajeroManager.Actualizar(pasajero, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(respuestaPasajero.PAS_PASAPORTE))
                        {
                            lblResultado.Text = "Pasaporte modificado con exito";
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
            ltrTituloMantenimiento.Text = "Nuevo pasaporte";
            lblResultado.Text = string.Empty;
            txtCodigoMant.Text = string.Empty;
            txtPasaporteMant.Text = string.Empty;
            txtNombreMant.Text = string.Empty;
            txtNacionalidad.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtTelefono.Text = string.Empty;

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

        protected void gvPasajeros_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow fila = gvPasajeros.Rows[index];

            switch (e.CommandName)
            {
                case "Modificar":
                    ltrTituloMantenimiento.Text = "Modificar pasajero";
                    txtCodigoMant.Text = fila.Cells[0].Text;
                    txtPasaporteMant.Text = fila.Cells[1].Text;
                    txtNombreMant.Text = fila.Cells[2].Text;
                    txtFecha.Text = fila.Cells[3].Text;
                    txtNacionalidad.Text = fila.Cells[4].Text;
                    txtCorreo.Text = fila.Cells[5].Text;
                    txtTelefono.Text = fila.Cells[6].Text;
                    ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
                    break;
                case "Eliminar":
                    lblCodigoEliminar.Text = fila.Cells[0].Text;
                    lblCodigoEliminar.Visible = false;
                    ltrModalMensaje.Text = "Confirme que desea eliminar el pasajero " + fila.Cells[0].Text + "-" + fila.Cells[1].Text;
                    ScriptManager.RegisterStartupScript(this,
               this.GetType(), "LaunchServerSide", "$(function() {openModal(); } );", true);

                    break;
                default:
                    break;
            }
        }
    }
}