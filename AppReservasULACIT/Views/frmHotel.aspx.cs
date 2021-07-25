﻿using AppReservasULACIT.Controllers;
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
    public partial class frmHotel : System.Web.UI.Page
    {
        IEnumerable<Hotel> hoteles = new ObservableCollection<Hotel>();
        HotelManager hotelManager = new HotelManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
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
                hoteles = await hotelManager.ObtenerHoteles(Session["Token"].ToString());
                gvHoteles.DataSource = hoteles.ToList();
                gvHoteles.DataBind();
            }
            catch (Exception exc)
            {
                lblStatus.Text = "Hubo un error al cargar la lista de servicios. Detalle: "+exc.Message;
                lblStatus.Visible = true;
            }
        }

        protected async void btnAceptarModal_Click(object sender, EventArgs e)
        {
            try
            {
                string resultado = string.Empty;
                resultado = await hotelManager.Eliminar(lblCodigoEliminar.Text, Session["Token"].ToString());
                if (!string.IsNullOrEmpty(resultado))
                {
                    ltrModalMensaje.Text = "Hotel eliminado";
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
                        Hotel hotel = new Hotel()
                        {
                            HOT_NOMBRE = txtNombreMant.Text,
                            HOT_EMAIL = txtEmailMant.Text,
                            HOT_DIRECCION = txtDireccionMant.Text,
                            HOT_TELEFONO = txtTelefono.Text,
                            HOT_CATEGORIA = ddlCategoria.SelectedValue
                        };

                        Hotel respuestaHotel = await hotelManager.Ingresar(hotel, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(respuestaHotel.HOT_NOMBRE))
                        {
                            lblResultado.Text = "Hotel ingresado con exito";
                            lblResultado.Visible = true;
                            lblResultado.ForeColor = Color.Green;
                            InicializarControles();
                        }
                    }
                    else//MODIFICAR
                    {
                        Hotel hotel = new Hotel()
                        {
                            HOT_CODIGO = Convert.ToInt32(txtCodigoMant.Text),
                            HOT_NOMBRE = txtNombreMant.Text,
                            HOT_EMAIL = txtEmailMant.Text,
                            HOT_DIRECCION = txtDireccionMant.Text,
                            HOT_TELEFONO = txtTelefono.Text,
                            HOT_CATEGORIA = ddlCategoria.SelectedValue
                        };

                        Hotel respuestaHotel = await hotelManager.Actualizar(hotel, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(respuestaHotel.HOT_NOMBRE))
                        {
                            lblResultado.Text = "Hotel modificado con exito";
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
            ltrTituloMantenimiento.Text = "Nuevo hotel";
            lblResultado.Text = string.Empty;
            txtCodigoMant.Text = string.Empty;
            txtNombreMant.Text = string.Empty;
            txtEmailMant.Text = string.Empty;
            txtDireccionMant.Text = string.Empty;
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

        protected void gvHoteles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow fila = gvHoteles.Rows[index];

            switch (e.CommandName)
            {
                case "Modificar":
                    ltrTituloMantenimiento.Text = "Modificar hotel";
                    txtCodigoMant.Text = fila.Cells[0].Text;
                    txtNombreMant.Text = fila.Cells[1].Text;
                    txtEmailMant.Text = fila.Cells[2].Text;
                    txtDireccionMant.Text = fila.Cells[3].Text;
                    txtTelefono.Text = fila.Cells[4].Text;
                    ddlCategoria.SelectedValue = fila.Cells[5].Text;
                    ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
                    break;
                case "Eliminar":
                    lblCodigoEliminar.Text = fila.Cells[0].Text;
                    lblCodigoEliminar.Visible = false;
                    ltrModalMensaje.Text = "Confirme que desea eliminar el hotel " + fila.Cells[0].Text + "-" + fila.Cells[1].Text;
                    ScriptManager.RegisterStartupScript(this,
               this.GetType(), "LaunchServerSide", "$(function() {openModal(); } );", true);

                    break;
                default:
                    break;
            }
        }
    }
}