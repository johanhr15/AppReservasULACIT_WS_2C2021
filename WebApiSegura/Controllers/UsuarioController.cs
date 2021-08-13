using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiSegura.Models;

namespace WebApiSegura.Controllers
{
    [Authorize]
    [RoutePrefix("api/Usuario")]
    public class UsuarioController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Usuario> usuarios = new List<Usuario>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT USU_CODIGO, USU_IDENTIFICACION, USU_NOMBRE, USU_PASSWORD, 
	                                                        USU_EMAIL, USU_ESTADO, USU_FEC_NAC, USU_TELEFONO
                                                        FROM   Usuario", sqlConnection);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Usuario usuario = new Usuario();
                        usuario.USU_CODIGO = sqlDataReader.GetInt32(0);
                        usuario.USU_IDENTIFICACION = sqlDataReader.GetString(1);
                        usuario.USU_NOMBRE = sqlDataReader.GetString(2);
                        usuario.USU_PASSWORD = sqlDataReader.GetString(3);
                        usuario.USU_EMAIL = sqlDataReader.GetString(4);
                        usuario.USU_ESTADO = sqlDataReader.GetString(5);
                        usuario.USU_FEC_NAC = sqlDataReader.GetDateTime(6);
                        usuario.USU_TELEFONO = sqlDataReader.GetString(7);
                        usuarios.Add(usuario);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(usuarios);
        }
    }
}