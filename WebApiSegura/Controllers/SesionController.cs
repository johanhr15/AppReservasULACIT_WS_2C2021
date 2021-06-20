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
    [RoutePrefix("api/sesion")]
    public class SesionController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Sesion sesion = new Sesion();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT SES_CODIGO, USU_CODIGO, 
                                                            SES_FEC_HORA_INICIO, SES_FEC_HORA_FIN, 
                                                            SES_ESTADO FROM Sesion
                                                            WHERE SES_CODIGO = @SES_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@SES_CODIGO", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        sesion.SES_CODIGO = sqlDataReader.GetInt32(0);
                        sesion.USU_CODIGO = sqlDataReader.GetInt32(1);
                        sesion.SES_FEC_HORA_INICIO = sqlDataReader.GetDateTime(2);
                        sesion.SES_FEC_HORA_FIN = sqlDataReader.GetDateTime(3);
                        sesion.SES_ESTADO = sqlDataReader.GetString(4);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
            return Ok(sesion);
        }
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Sesion> sesiones = new List<Sesion>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT SES_CODIGO, USU_CODIGO, 
                                                            SES_FEC_HORA_INICIO, SES_FEC_HORA_FIN, 
                                                            SES_ESTADO FROM Sesion", sqlConnection);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Sesion sesion = new Sesion();
                        sesion.SES_CODIGO = sqlDataReader.GetInt32(0);
                        sesion.USU_CODIGO = sqlDataReader.GetInt32(1);
                        sesion.SES_FEC_HORA_INICIO = sqlDataReader.GetDateTime(2);
                        sesion.SES_FEC_HORA_FIN = sqlDataReader.GetDateTime(3);
                        sesion.SES_ESTADO = sqlDataReader.GetString(4);
                        sesiones.Add(sesion);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(sesiones);
        }

        [HttpPost]
        public IHttpActionResult Ingresar(Sesion sesion)
        {
            if (sesion == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO SESION (USU_CODIGO, 
                                                            SES_FEC_HORA_INICIO, SES_FEC_HORA_FIN, 
                                                            SES_ESTADO)
                                                            VALUES (@USU_CODIGO, @SES_FEC_HORA_INICIO, 
                                                            @SES_FEC_HORA_FIN, @SES_ESTADO)",
                                                            sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@USU_CODIGO", sesion.USU_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@SES_FEC_HORA_INICIO", sesion.SES_FEC_HORA_INICIO);
                    sqlCommand.Parameters.AddWithValue("@SES_FEC_HORA_FIN", sesion.SES_FEC_HORA_FIN);
                    sqlCommand.Parameters.AddWithValue("@SES_ESTADO", sesion.SES_ESTADO);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(sesion);
        }

        [HttpPut]
        public IHttpActionResult Actualizar(Sesion sesion)
        {
            if (sesion == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new
                        SqlCommand(@"UPDATE SESION SET USU_CODIGO = @USU_CODIGO, SES_FEC_HORA_INICIO = @SES_FEC_HORA_INICIO, 
                                                            SES_FEC_HORA_FIN = @SES_FEC_HORA_FIN,
                                                            SES_ESTADO = @SES_ESTADO
                                    WHERE SES_CODIGO = @SES_CODIGO",
                                                            sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@SES_CODIGO", sesion.SES_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@USU_CODIGO", sesion.USU_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@SES_FEC_HORA_INICIO", sesion.SES_FEC_HORA_INICIO);
                    sqlCommand.Parameters.AddWithValue("@SES_FEC_HORA_FIN", sesion.SES_FEC_HORA_FIN);
                    sqlCommand.Parameters.AddWithValue("@SES_ESTADO", sesion.SES_ESTADO);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(sesion);
        }

        [HttpDelete]
        public IHttpActionResult Eliminar(int id)
        {
            if (id < 1)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new
                        SqlCommand(@"DELETE SESION WHERE SES_CODIGO = @SES_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@SES_CODIGO", id);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(id);
        }
    }
}
