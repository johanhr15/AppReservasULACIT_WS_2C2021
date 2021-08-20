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
    public partial class frmAeropuerto : System.Web.UI.Page
    {
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
                aeropuertos = await aeropuertoManager.ObtenerAeropuertos(Session["Token"].ToString());
                gvAeropuertos.DataSource = aeropuertos.ToList();
                gvAeropuertos.DataBind();
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
                resultado = await aeropuertoManager.Eliminar(lblCodigoEliminar.Text, Session["Token"].ToString());
                if (!string.IsNullOrEmpty(resultado))
                {
                    ltrModalMensaje.Text = "Aeropuerto eliminado";
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
                        Aeropuerto aeropuerto = new Aeropuerto()
                        {
                            ARP_PAIS = txtPaisMant.Text,
                            ARP_CIUDAD = txtCiudadMant.Text,
                            ARP_ZONA_HORARIA = txtZonaHorariaMant.Text,
                            ARP_VISA = ddlVisaRequerida.SelectedValue,
                            ARP_CONTROL_VACUNAS = txtControlVacunas.Text,
                        };

                        Aeropuerto respuestaAeropuerto = await aeropuertoManager.Ingresar(aeropuerto, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(respuestaAeropuerto.ARP_PAIS))
                        {
                            lblResultado.Text = "Aeropuerto ingresado con exito";
                            lblResultado.Visible = true;
                            lblResultado.ForeColor = Color.Green;
                            InicializarControles();
                        }
                    }
                    else//MODIFICAR
                    {
                        Aeropuerto aeropuerto = new Aeropuerto()
                        {
                            ARP_CODIGO = Convert.ToInt32(txtCodigoMant.Text),
                            ARP_PAIS = txtPaisMant.Text,
                            ARP_CIUDAD = txtCiudadMant.Text,
                            ARP_ZONA_HORARIA = txtZonaHorariaMant.Text,
                            ARP_VISA = ddlVisaRequerida.SelectedValue,
                            ARP_CONTROL_VACUNAS = txtControlVacunas.Text,
                        };

                        Aeropuerto respuestaAeropuerto = await aeropuertoManager.Actualizar(aeropuerto, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(respuestaAeropuerto.ARP_PAIS))
                        {
                            lblResultado.Text = "Aeropuerto modificado con exito";
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
            ltrTituloMantenimiento.Text = "Nuevo aeropuerto";
            lblResultado.Text = string.Empty;
            txtCodigoMant.Text = string.Empty;
            txtPaisMant.Text = string.Empty;
            txtCiudadMant.Text = string.Empty;
            txtZonaHorariaMant.Text = string.Empty;
            ddlVisaRequerida.SelectedValue = string.Empty;
            txtControlVacunas.Text = string.Empty;

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

        protected void gvAeropuertos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow fila = gvAeropuertos.Rows[index];

            switch (e.CommandName)
            {
                case "Modificar":
                    ltrTituloMantenimiento.Text = "Modificar aeropuerto";
                    txtCodigoMant.Text = fila.Cells[0].Text;
                    txtPaisMant.Text = fila.Cells[1].Text;
                    txtCiudadMant.Text = fila.Cells[2].Text;
                    txtZonaHorariaMant.Text = fila.Cells[3].Text;
                    ddlVisaRequerida.SelectedValue = fila.Cells[4].Text;
                    txtControlVacunas.Text = fila.Cells[5].Text;
                    ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
                    break;
                case "Eliminar":
                    lblCodigoEliminar.Text = fila.Cells[0].Text;
                    lblCodigoEliminar.Visible = false;
                    ltrModalMensaje.Text = "Confirme que desea eliminar el aeropuerto " + fila.Cells[0].Text + "-" + fila.Cells[1].Text;
                    ScriptManager.RegisterStartupScript(this,
               this.GetType(), "LaunchServerSide", "$(function() {openModal(); } );", true);

                    break;
                default:
                    break;
            }
        }
    }
}