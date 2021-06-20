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
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(LoginRequest loginRequest)
        {
            if (loginRequest == null)
                return BadRequest();

            Usuario usuario = new Usuario();

            try
            {
                using (SqlConnection sqlConnection = new
            SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT USU_CODIGO, USU_IDENTIFICACION, USU_NOMBRE, USU_PASSWORD, 
	                                                        USU_EMAIL, USU_ESTADO, USU_FEC_NAC, USU_TELEFONO
                                                        FROM   Usuario
                                                        WHERE USU_IDENTIFICACION = @USU_IDENTIFICACION
                                                        AND USU_PASSWORD = @USU_PASSWORD", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@USU_IDENTIFICACION", loginRequest.Username);
                    sqlCommand.Parameters.AddWithValue("@USU_PASSWORD", loginRequest.Password);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.Read())
                    {
                        usuario.USU_CODIGO = sqlDataReader.GetInt32(0);
                        usuario.USU_IDENTIFICACION = sqlDataReader.GetString(1);
                        usuario.USU_NOMBRE = sqlDataReader.GetString(2);
                        usuario.USU_PASSWORD = sqlDataReader.GetString(3);
                        usuario.USU_EMAIL = sqlDataReader.GetString(4);
                        usuario.USU_ESTADO = sqlDataReader.GetString(5);
                        usuario.USU_FEC_NAC = sqlDataReader.GetDateTime(6);
                        usuario.USU_TELEFONO = sqlDataReader.GetString(7);

                        var token = TokenGenerator.GenerateTokenJwt(loginRequest.Username);
                        usuario.Token = token;
                    }

                    sqlConnection.Close();

                    if(!string.IsNullOrEmpty(usuario.Token))
                         return Ok(usuario);
                    else
                        return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpPost]
        [Route("register")]
        public IHttpActionResult Register(Usuario usuario)
        {
            if (usuario == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
            SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO Usuario (USU_IDENTIFICACION, USU_NOMBRE, USU_PASSWORD,
	                                                        USU_EMAIL, USU_ESTADO, USU_FEC_NAC, USU_TELEFONO) 
                                                            VALUES (@USU_IDENTIFICACION, @USU_NOMBRE, @USU_PASSWORD, 
	                                                        @USU_EMAIL, @USU_ESTADO, @USU_FEC_NAC, @USU_TELEFONO) ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@USU_IDENTIFICACION", usuario.USU_IDENTIFICACION);
                    sqlCommand.Parameters.AddWithValue("@USU_NOMBRE", usuario.USU_NOMBRE);
                    sqlCommand.Parameters.AddWithValue("@USU_PASSWORD", usuario.USU_PASSWORD);
                    sqlCommand.Parameters.AddWithValue("@USU_EMAIL", usuario.USU_EMAIL);
                    sqlCommand.Parameters.AddWithValue("@USU_ESTADO", usuario.USU_ESTADO);
                    sqlCommand.Parameters.AddWithValue("@USU_FEC_NAC", usuario.USU_FEC_NAC);
                    sqlCommand.Parameters.AddWithValue("@USU_TELEFONO", usuario.USU_TELEFONO);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();

                    if (filasAfectadas > 0)
                        return Ok(usuario);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok();
        }

    }
}
