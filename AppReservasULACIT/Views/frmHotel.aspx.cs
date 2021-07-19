using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppReservasULACIT.Controllers;
using AppReservasULACIT.Models;

namespace AppReservasULACIT.Views
{
    public partial class frmHotel : System.Web.UI.Page
    {
        IEnumerable<Hotel> hoteles = new ObservableCollection<Hotel>();
        HotelManager hotelManager = new HotelManager();

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
                hoteles = await hotelManager.ObtenerHoteles(Session["Token"].ToString());
                gvHoteles.DataSource = hoteles.ToList();
                gvHoteles.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}