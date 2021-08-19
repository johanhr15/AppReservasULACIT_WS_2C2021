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
    public partial class frmAvion : System.Web.UI.Page
    {

        IEnumerable<Avion> aviones = new ObservableCollection<Avion>();
        AvionManager avionManager = new AvionManager();
        IEnumerable<Aerolinea> aerolineas = new ObservableCollection<Aerolinea>();
        AerolineaManager aerolineaManager = new AerolineaManager();
        IEnumerable<Asiento> asientos = new ObservableCollection<Asiento>();
        AsientoManager asientoManager = new AsientoManager();


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
                aviones = await avionManager.ObtenerAvion(Session["Token"].ToString());
                gvAviones.DataSource = aviones.ToList();
                gvAviones.DataBind();
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
                asientos = await asientoManager.ObtenerAsiento(Session["Token"].ToString());
                ddlCodigoAsiento.DataSource = asientos.ToList();
                ddlCodigoAsiento.DataBind();
                ddlCodigoAsiento.DataTextField = "ASI_CODIGO";
                ddlCodigoAsiento.DataValueField = "ASI_CODIGO";
                ddlCodigoAsiento.DataBind();
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
                resultado = await avionManager.Eliminar(lblCodigoEliminar.Text, Session["Token"].ToString());
                if (!string.IsNullOrEmpty(resultado))
                {
                    ltrModalMensaje.Text = "Avión eliminado";
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
                        Avion avion = new Avion()
                        {
                            AER_CODIGO = Convert.ToInt32(ddlCodigoAerolinea.SelectedValue),
                            ASI_CODIGO = Convert.ToInt32(ddlCodigoAsiento.SelectedValue),
                            AVI_MODELO = txtModelo.Text,
                            AVI_TIPO_RUTA = ddlTipoRuta.SelectedValue,
                            AVI_CAPACIDAD = Convert.ToInt32(txtCapacidad.Text),
                            AVI_EQUIPAJE = txtEquipaje.Text,

                        };

                        Avion respuestaAvion = await avionManager.Ingresar(avion, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(respuestaAvion.AVI_MODELO))
                        {
                            lblResultado.Text = "Avion ingresado con exito";
                            lblResultado.Visible = true;
                            lblResultado.ForeColor = Color.Green;
                            InicializarControles();
                        }
                    }
                    else//MODIFICAR
                    {
                        Avion avion = new Avion()
                        {
                            AVI_CODIGO = Convert.ToInt32(txtCodigoMant.Text),
                            AER_CODIGO = Convert.ToInt32(ddlCodigoAerolinea.SelectedValue),
                            ASI_CODIGO = Convert.ToInt32(ddlCodigoAsiento.SelectedValue),
                            AVI_MODELO = txtModelo.Text,
                            AVI_TIPO_RUTA = ddlTipoRuta.SelectedValue,
                            AVI_CAPACIDAD = Convert.ToInt32(txtCapacidad.Text),
                            AVI_EQUIPAJE = txtEquipaje.Text
                        };

                        Avion respuestaAvion = await avionManager.Actualizar(avion, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(respuestaAvion.AVI_MODELO))
                        {
                            lblResultado.Text = "Avion modificado con exito";
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
            ltrTituloMantenimiento.Text = "Nuevo Avion";
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

        protected void gvAviones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow fila = gvAviones.Rows[index];

            switch (e.CommandName)
            {
                case "Modificar":
                    lblResultado.Text = "";
                    lblResultado.Visible = false;
                    ltrTituloMantenimiento.Text = "Modificar Avion";
                    txtCodigoMant.Text = fila.Cells[0].Text;
                    ddlCodigoAerolinea.SelectedValue = fila.Cells[1].Text;
                    ddlCodigoAsiento.SelectedValue = fila.Cells[2].Text;
                    txtModelo.Text = fila.Cells[3].Text;
                    ddlTipoRuta.SelectedValue = fila.Cells[4].Text;
                    txtCapacidad.Text = fila.Cells[5].Text;
                    txtEquipaje.Text = fila.Cells[6].Text;

                    ScriptManager.RegisterStartupScript(this,
               this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
                    break;

                case "Eliminar":
                    lblResultado.Text = "";
                    lblResultado.Visible = false;
                    lblCodigoEliminar.Text = fila.Cells[0].Text;
                    lblCodigoEliminar.Visible = false;
                    ltrModalMensaje.Text = "Confirme que desea eliminar el avión " + fila.Cells[0].Text + "-" + fila.Cells[3].Text;
                    ScriptManager.RegisterStartupScript(this,
               this.GetType(), "LaunchServerSide", "$(function() {openModal(); } );", true);

                    break;
                default:
                    break;
            }
        }
    }
}