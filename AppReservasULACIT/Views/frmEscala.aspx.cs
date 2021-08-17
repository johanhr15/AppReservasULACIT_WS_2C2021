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
    public partial class frmEscala : System.Web.UI.Page
    {
        IEnumerable<Escala> escalas = new ObservableCollection<Escala>();
        EscalaManager escalaManager = new EscalaManager();

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
                escalas = await escalaManager.ObtenerEscalas(Session["Token"].ToString());
                gvEscalas.DataSource = escalas.ToList();
                gvEscalas.DataBind();
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
                resultado = await escalaManager.Eliminar(lblCodigoEliminar.Text, Session["Token"].ToString());
                if (!string.IsNullOrEmpty(resultado))
                {
                    ltrModalMensaje.Text = "Escala eliminada";
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
                        Escala escala = new Escala()
                        {
                            ESC_NUMERO_TERMINAL = Convert.ToInt32(txtNumeroTerminalMant.Text),
                            ESC_ARP_CODIGO = Convert.ToInt32(txtCodigoAeroMant.Text),
                            ESC_TIEMPO_ESPERA = Convert.ToDateTime(txtTiempoEsperaMant.Text),
                            ESC_TRASBORDO = txtTrasbordo.Text
                            
                        };

                        Escala respuestaEscala = await escalaManager.Ingresar(escala, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(respuestaEscala.ESC_TRASBORDO))
                        {
                            lblResultado.Text = "Escala ingresada con exito";
                            lblResultado.Visible = true;
                            lblResultado.ForeColor = Color.Green;
                            InicializarControles();
                        }
                    }
                    else//MODIFICAR
                    {
                        Escala escala = new Escala()
                        {
                            ESC_CODIGO = Convert.ToInt32(txtCodigoMant.Text),
                            ESC_NUMERO_TERMINAL = Convert.ToInt32(txtNumeroTerminalMant.Text),
                            ESC_ARP_CODIGO = Convert.ToInt32(txtCodigoAeroMant.Text),
                            ESC_TIEMPO_ESPERA = Convert.ToDateTime(txtTiempoEsperaMant.Text),
                            ESC_TRASBORDO = txtTrasbordo.Text
                        };

                        Escala respuestaEscala = await escalaManager.Actualizar(escala, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(respuestaEscala.ESC_TRASBORDO))
                        {
                            lblResultado.Text = "Escala modificada con exito";
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
            ltrTituloMantenimiento.Text = "Nueva escala";
            lblResultado.Text = string.Empty;
            txtNumeroTerminalMant.Text = string.Empty;
            txtCodigoAeroMant.Text = string.Empty;
            txtTiempoEsperaMant.Text = string.Empty;
            txtTrasbordo.Text = string.Empty;
            

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

        protected void gvEscalas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow fila = gvEscalas.Rows[index];

            switch (e.CommandName)
            {
                case "Modificar":
                    ltrTituloMantenimiento.Text = "Modificar escala";
                    txtCodigoMant.Text = fila.Cells[0].Text;
                    txtNumeroTerminalMant.Text = fila.Cells[1].Text;
                    txtCodigoAeroMant.Text = fila.Cells[2].Text;
                    txtTiempoEsperaMant.Text = fila.Cells[3].Text;
                    txtTrasbordo.Text = fila.Cells[4].Text;
                    ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
                    break;
                case "Eliminar":
                    lblCodigoEliminar.Text = fila.Cells[0].Text;
                    lblCodigoEliminar.Visible = false;
                    ltrModalMensaje.Text = "Confirme que desea eliminar la escala " + fila.Cells[0].Text + "-" + fila.Cells[1].Text;
                    ScriptManager.RegisterStartupScript(this,
               this.GetType(), "LaunchServerSide", "$(function() {openModal(); } );", true);

                    break;
                default:
                    break;
            }
        }
    }
}