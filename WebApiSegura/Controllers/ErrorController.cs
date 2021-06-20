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
    [RoutePrefix("api/error")]
    public class ErrorController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Error error = new Error();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT ERR_CODIGO, USU_CODIGO, ERR_FEC_HORA,
                                                            ERR_FUENTE, ERR_NUMERO, ERR_DESCRIPCION, ERR_VISTA,
                                                            ERR_ACCION FROM Error
                                                            WHERE ERR_CODIGO = @ERR_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@ERR_CODIGO", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        error.ERR_CODIGO = sqlDataReader.GetInt32(0);
                        error.USU_CODIGO = sqlDataReader.GetInt32(1);
                        error.ERR_FEC_HORA = sqlDataReader.GetDateTime(2);
                        error.ERR_FUENTE = sqlDataReader.GetString(3);
                        error.ERR_NUMERO = sqlDataReader.GetString(4);
                        error.ERR_DESCRIPCION = sqlDataReader.GetString(5);
                        error.ERR_VISTA = sqlDataReader.GetString(6);
                        error.ERR_ACCION = sqlDataReader.GetString(7);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
            return Ok(error);
        }
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Error> errores = new List<Error>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT ERR_CODIGO, USU_CODIGO, ERR_FEC_HORA,
                                                            ERR_FUENTE, ERR_NUMERO, ERR_DESCRIPCION, ERR_VISTA,
                                                            ERR_ACCION FROM Error", sqlConnection);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Error error = new Error();
                        error.ERR_CODIGO = sqlDataReader.GetInt32(0);
                        error.USU_CODIGO = sqlDataReader.GetInt32(1);
                        error.ERR_FEC_HORA = sqlDataReader.GetDateTime(2);
                        error.ERR_FUENTE = sqlDataReader.GetString(3);
                        error.ERR_NUMERO = sqlDataReader.GetString(4);
                        error.ERR_DESCRIPCION = sqlDataReader.GetString(5);
                        error.ERR_VISTA = sqlDataReader.GetString(6);
                        error.ERR_ACCION = sqlDataReader.GetString(7);
                        errores.Add(error);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(errores);
        }

        [HttpPost]
        public IHttpActionResult Ingresar(Error error)
        {
            if (error == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO ERROR(USU_CODIGO, ERR_FEC_HORA, 
                                                            ERR_FUENTE, ERR_NUMERO, ERR_DESCRIPCION,
                                                            ERR_VISTA,ERR_ACCION)
                                                            VALUES (@USU_CODIGO, @ERR_FEC_HORA, 
                                                            @ERR_FUENTE, @ERR_NUMERO, @ERR_DESCRIPCION,
                                                            @ERR_VISTA, @ERR_ACCION)",
                                                            sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@USU_CODIGO", error.USU_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@ERR_FEC_HORA", error.ERR_FEC_HORA);
                    sqlCommand.Parameters.AddWithValue("@ERR_FUENTE", error.ERR_FUENTE);
                    sqlCommand.Parameters.AddWithValue("@ERR_NUMERO", error.ERR_NUMERO);
                    sqlCommand.Parameters.AddWithValue("@ERR_DESCRIPCION", error.ERR_DESCRIPCION);
                    sqlCommand.Parameters.AddWithValue("@ERR_VISTA", error.ERR_VISTA);
                    sqlCommand.Parameters.AddWithValue("@ERR_ACCION", error.ERR_ACCION);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(error);
        }

        [HttpPut]
        public IHttpActionResult Actualizar(Error error)
        {
            if (error == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new
                        SqlCommand(@"UPDATE ERROR SET USU_CODIGO = @USU_CODIGO, ERR_FEC_HORA = @ERR_FEC_HORA, 
                                                            ERR_FUENTE = @ERR_FUENTE, ERR_NUMERO = @ERR_NUMERO,
                                                            ERR_DESCRIPCION = @ERR_DESCRIPCION, ERR_VISTA = @ERR_VISTA,
                                                            ERR_ACCION = @ERR_ACCION 
                                    WHERE ERR_CODIGO = @ERR_CODIGO",
                                                            sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@ERR_CODIGO", error.ERR_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@USU_CODIGO", error.USU_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@ERR_FEC_HORA", error.ERR_FEC_HORA);
                    sqlCommand.Parameters.AddWithValue("@ERR_FUENTE", error.ERR_FUENTE);
                    sqlCommand.Parameters.AddWithValue("@ERR_NUMERO", error.ERR_NUMERO);
                    sqlCommand.Parameters.AddWithValue("@ERR_DESCRIPCION", error.ERR_DESCRIPCION);
                    sqlCommand.Parameters.AddWithValue("@ERR_VISTA", error.ERR_VISTA);
                    sqlCommand.Parameters.AddWithValue("@ERR_ACCION", error.ERR_ACCION);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(error);
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
                        SqlCommand(@"DELETE ERROR WHERE ERR_CODIGO = @ERR_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@ERR_CODIGO", id);

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
