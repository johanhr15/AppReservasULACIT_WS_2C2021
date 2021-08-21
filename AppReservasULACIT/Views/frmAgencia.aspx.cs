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
    public partial class frmAgencia : System.Web.UI.Page
    {
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
                agencias = await agenciaManager.ObtenerAgencias(Session["Token"].ToString());
                gvAgencias.DataSource = agencias.ToList();
                gvAgencias.DataBind();
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
                resultado = await agenciaManager.Eliminar(lblCodigoEliminar.Text, Session["Token"].ToString());
                if (!string.IsNullOrEmpty(resultado))
                {
                    ltrModalMensaje.Text = "Agencia eliminada";
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
                        try
                        {
                            if (txtTelefonoMant.Text.Length < 50 && txtTelefonoMant.Text.Length > 7 && txtCorreo.Text.Contains("@") && txtSitioWeb.Text.Contains(".com"))
                            {
                                Agencia agencia = new Agencia()
                        {
                            AGE_NOMBRE = txtNombreMant.Text,
                            AGE_CORREO = txtCorreo.Text,
                            AGE_TELEFONO = txtTelefonoMant.Text,
                            AGE_SITIO_WEB = txtSitioWeb.Text,
                            AGE_HORARIO = txtHorario.Text
                        };

                        Agencia respuestaAgencia = await agenciaManager.Ingresar(agencia, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(respuestaAgencia.AGE_NOMBRE))
                        {
                            lblResultado.Text = "Agencia ingresada con exito";
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
                            if (txtTelefonoMant.Text.Length < 50 && txtTelefonoMant.Text.Length > 7 && txtCorreo.Text.Contains("@") && txtSitioWeb.Text.Contains(".com"))
                            {
                                Agencia agencia = new Agencia()
                        {
                            AGE_CODIGO = Convert.ToInt32(txtCodigoMant.Text),
                            AGE_NOMBRE = txtNombreMant.Text,
                            AGE_CORREO = txtCorreo.Text,
                            AGE_TELEFONO = txtTelefonoMant.Text,
                            AGE_SITIO_WEB = txtSitioWeb.Text,
                            AGE_HORARIO = txtHorario.Text
                        };

                        Agencia respuestaAgencia = await agenciaManager.Actualizar(agencia, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(respuestaAgencia.AGE_NOMBRE))
                        {
                            lblResultado.Text = "Agencia modificada con exito";
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
            ltrTituloMantenimiento.Text = "Nueva agencia";
            lblResultado.Text = string.Empty;
            txtCodigoMant.Text = string.Empty;
            txtNombreMant.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtTelefonoMant.Text = string.Empty;
            txtSitioWeb.Text = string.Empty;
            txtHorario.Text = string.Empty;

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

        protected void gvAgencias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow fila = gvAgencias.Rows[index];

            switch (e.CommandName)
            {
                case "Modificar":
                    lblResultado.Text = "";
                    lblResultado.Visible = false;
                    ltrTituloMantenimiento.Text = "Modificar agencia";
                    txtCodigoMant.Text = fila.Cells[0].Text;
                    txtNombreMant.Text = fila.Cells[1].Text;
                    txtCorreo.Text = fila.Cells[2].Text;
                    txtTelefonoMant.Text = fila.Cells[3].Text;
                    txtSitioWeb.Text = fila.Cells[4].Text;
                    txtHorario.Text = fila.Cells[5].Text;
                    ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
                    break;
                case "Eliminar":
                    lblResultado.Text = "";
                    lblResultado.Visible = false;
                    lblCodigoEliminar.Text = fila.Cells[0].Text;
                    lblCodigoEliminar.Visible = false;
                    ltrModalMensaje.Text = "Confirme que desea eliminar la agencia " + fila.Cells[0].Text + "-" + fila.Cells[1].Text;
                    ScriptManager.RegisterStartupScript(this,
               this.GetType(), "LaunchServerSide", "$(function() {openModal(); } );", true);

                    break;
                default:
                    break;
            }
        }
    }
}