using AppReservasULACIT.Controllers;
using AppReservasULACIT.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppReservasULACIT
{
    public partial class frmLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void btnIngresar_Click(object sender, EventArgs e)
        {
            if(Page.IsValid)
            {
                try
                {
                    LoginRequest loginRequest = new LoginRequest() 
                    { Username = txtUsername.Text, Password = txtPassword.Text };

                    UsuarioManager usuarioManager = new UsuarioManager();

                    Usuario usuario = new Usuario();

                    usuario = await usuarioManager.Validar(loginRequest);

                    if(usuario != null)
                    {
                        JwtSecurityToken jwtSecurityToken;
                        var jwtHandler = new JwtSecurityTokenHandler();
                        jwtSecurityToken = jwtHandler.ReadJwtToken(usuario.Token);

                        Session["CodigoUsuario"] = usuario.USU_CODIGO;
                        Session["Identificacion"] = usuario.USU_IDENTIFICACION;
                        Session["Nombre"] = usuario.USU_NOMBRE;
                        Session["Token"] = usuario.Token;

                        FormsAuthentication.RedirectFromLoginPage(usuario.USU_IDENTIFICACION, false);

                    }
                    else
                    {
                        lblError.Text = "Credenciales invalidas";
                        lblError.Visible = true;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}