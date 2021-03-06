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
    public partial class frmHabitacion : System.Web.UI.Page
    {
        IEnumerable<Habitacion> habitaciones = new ObservableCollection<Habitacion>();
        HabitacionManager habitacionManager = new HabitacionManager();

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
                habitaciones = await habitacionManager.ObtenerHabitaciones(Session["Token"].ToString());
                gvHabitaciones.DataSource = habitaciones.ToList();
                gvHabitaciones.DataBind();
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
                resultado = await habitacionManager.Eliminar(lblCodigoEliminar.Text, Session["Token"].ToString());
                if (!string.IsNullOrEmpty(resultado))
                {
                    ltrModalMensaje.Text = "Habitacion eliminada";
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
                        Habitacion habitacion = new Habitacion()
                        {
                            HOT_CODIGO = Convert.ToInt32(txtHotelCodigoMant.Text),
                            HAB_NUMERO = Convert.ToInt32(txtNumeroMant.Text),
                            HAB_CAPACIDAD = Convert.ToInt32(txtCapacidadMant.Text),
                            HAB_TIPO = txtTipo.Text,
                            HAB_DESCRIPCION = txtDescripcion.Text,
                            HAB_ESTADO = txtEstado.Text,
                            HAB_PRECIO = Convert.ToDecimal(txtPrecio.Text)

                        };

                        Habitacion respuestaHabitacion = await habitacionManager.Ingresar(habitacion, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(respuestaHabitacion.HAB_ESTADO))
                        {
                            lblResultado.Text = "Hotel ingresado con exito";
                            lblResultado.Visible = true;
                            lblResultado.ForeColor = Color.Green;
                            InicializarControles();
                        }
                    }
                    else//MODIFICAR
                    {
                        Habitacion habitacion = new Habitacion()
                        {
                            HAB_CODIGO = Convert.ToInt32(txtCodigoMant.Text),
                            HOT_CODIGO = Convert.ToInt32(txtHotelCodigoMant.Text),
                            HAB_NUMERO = Convert.ToInt32(txtNumeroMant.Text),
                            HAB_CAPACIDAD = Convert.ToInt32(txtCapacidadMant.Text),
                            HAB_TIPO = txtTipo.Text,
                            HAB_DESCRIPCION = txtDescripcion.Text,
                            HAB_ESTADO = txtEstado.Text,
                            HAB_PRECIO = Convert.ToDecimal(txtPrecio.Text)
                        };

                        Habitacion respuestaHabitacion = await habitacionManager.Actualizar(habitacion, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(respuestaHabitacion.HAB_ESTADO))
                        {
                            lblResultado.Text = "Habitacion modificada con exito";
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
            ltrTituloMantenimiento.Text = "Nueva habitacion";
            lblResultado.Text = string.Empty;
            txtCodigoMant.Text = string.Empty;
            txtHotelCodigoMant.Text = string.Empty;
            txtNumeroMant.Text = string.Empty;
            txtCapacidadMant.Text = string.Empty;
            txtTipo.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtEstado.Text = string.Empty;
            txtPrecio.Text = string.Empty;

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
        protected void gvHabitaciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow fila = gvHabitaciones.Rows[index];

            switch (e.CommandName)
            {
                case "Modificar":
                    ltrTituloMantenimiento.Text = "Modificar habitacion";
                    txtCodigoMant.Text = fila.Cells[0].Text;
                    txtHotelCodigoMant.Text = fila.Cells[1].Text;
                    txtNumeroMant.Text = fila.Cells[2].Text;
                    txtCapacidadMant.Text = fila.Cells[3].Text;
                    txtTipo.Text = fila.Cells[4].Text;
                    txtDescripcion.Text = fila.Cells[5].Text;
                    txtEstado.Text = fila.Cells[6].Text;
                    txtPrecio.Text = fila.Cells[7].Text;
                    ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
                    break;
                case "Eliminar":
                    lblCodigoEliminar.Text = fila.Cells[0].Text;
                    lblCodigoEliminar.Visible = false;
                    ltrModalMensaje.Text = "Confirme que desea eliminar la habitacion " + fila.Cells[0].Text + "-" + fila.Cells[1].Text;
                    ScriptManager.RegisterStartupScript(this,
               this.GetType(), "LaunchServerSide", "$(function() {openModal(); } );", true);

                    break;
                default:
                    break;
            }
        }
    }
}