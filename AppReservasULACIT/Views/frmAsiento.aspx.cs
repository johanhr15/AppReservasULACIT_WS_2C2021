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
    public partial class frmAsiento : System.Web.UI.Page
    {

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
                asientos = await asientoManager.ObtenerAsiento(Session["Token"].ToString());
               gvAsientos.DataSource = asientos.ToList();
                gvAsientos.DataBind();
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
                resultado = await asientoManager.Eliminar(lblCodigoEliminar.Text, Session["Token"].ToString());
                if (!string.IsNullOrEmpty(resultado))
                {
                    ltrModalMensaje.Text = "Asiento eliminado";
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
                        Asiento asiento = new Asiento()
                        {
                            ASI_FILA = txtFila.Text,
                            ASI_LETRA = ddlAsiLetra.SelectedValue,
                            ASI_DESCRIPCION = ddlAsiDescripcion.SelectedValue,
                            ASI_CLASE = ddlAsiClase.SelectedValue

                        };

                        Asiento respuestaAsiento = await asientoManager.Ingresar(asiento, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(respuestaAsiento.ASI_FILA))
                        {
                            lblResultado.Text = "Asiento ingresado con exito";
                            lblResultado.Visible = true;
                            lblResultado.ForeColor = Color.Green;
                            InicializarControles();
                        }
                    }
                    else//MODIFICAR
                    {
                        Asiento asiento = new Asiento()
                        {
                            ASI_CODIGO = Convert.ToInt32(txtCodigoMant.Text),
                            ASI_FILA = txtFila.Text,
                            ASI_LETRA = ddlAsiLetra.SelectedValue,
                            ASI_DESCRIPCION = ddlAsiDescripcion.SelectedValue,
                            ASI_CLASE = ddlAsiClase.SelectedValue,
                        };

                        Asiento respuestaAsiento = await asientoManager.Actualizar(asiento, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(respuestaAsiento.ASI_FILA))
                        {
                            lblResultado.Text = "Asiento modificado con exito";
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
            ltrTituloMantenimiento.Text = "Nuevo Asiento";
            lblResultado.Text = string.Empty;
            txtCodigoMant.Text = string.Empty;
            txtFila.Text = string.Empty;
           

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

        protected void gvAsientos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow fila = gvAsientos.Rows[index];

            switch (e.CommandName)
            {
                case "Modificar":
                    lblResultado.Text = "";
                    lblResultado.Visible = false;
                    ltrTituloMantenimiento.Text = "Modificar Asiento";
                    txtCodigoMant.Text = fila.Cells[0].Text;
                    txtFila.Text = fila.Cells[1].Text;
                    ddlAsiLetra.SelectedValue = fila.Cells[2].Text;
                    ddlAsiDescripcion.SelectedValue = fila.Cells[3].Text;
                    ddlAsiClase.SelectedValue = fila.Cells[4].Text;

                    ScriptManager.RegisterStartupScript(this,
               this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
                    break;

                case "Eliminar":
                    lblResultado.Text = "";
                    lblResultado.Visible = false;
                    lblCodigoEliminar.Text = fila.Cells[0].Text;
                    lblCodigoEliminar.Visible = false;
                    ltrModalMensaje.Text = "Confirme que desea eliminar el Asiento " + fila.Cells[0].Text + "-" + fila.Cells[1].Text + fila.Cells[2].Text;
                    ScriptManager.RegisterStartupScript(this,
               this.GetType(), "LaunchServerSide", "$(function() {openModal(); } );", true);

                    break;
                default:
                    break;
            }
        }
    }
}