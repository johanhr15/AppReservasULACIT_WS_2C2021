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
    [RoutePrefix("api/asiento")]
    public class AsientoController : ApiController
    {
        public IHttpActionResult GetId(int id)
        {
            Asiento asiento = new Asiento();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT ASI_CODIGO, ASI_FILA, ASI_LETRA, 
                                                            ASI_DESCRIPCION, ASI_CLASE
                                                            FROM   Asiento
                                                             WHERE ASI_CODIGO = @ASI_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@ASI_CODIGO", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        asiento.ASI_CODIGO = sqlDataReader.GetInt32(0);
                        asiento.ASI_FILA = sqlDataReader.GetString(1);
                        asiento.ASI_LETRA = sqlDataReader.GetString(2);
                        asiento.ASI_DESCRIPCION = sqlDataReader.GetString(3);
                        asiento.ASI_CLASE = sqlDataReader.GetString(4);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(asiento);
        }

        public IHttpActionResult GetAll()
        {
            List<Asiento> asientos = new List<Asiento>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT ASI_CODIGO, ASI_FILA, ASI_LETRA, 
                                                            ASI_DESCRIPCION, ASI_CLASE
                                                             FROM   Asiento", sqlConnection);

                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Asiento asiento = new Asiento();
                        asiento.ASI_CODIGO = sqlDataReader.GetInt32(0);
                        asiento.ASI_FILA = sqlDataReader.GetString(1);
                        asiento.ASI_LETRA = sqlDataReader.GetString(2);
                        asiento.ASI_DESCRIPCION = sqlDataReader.GetString(3);
                        asiento.ASI_CLASE = sqlDataReader.GetString(4);
                        asientos.Add(asiento);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(asientos);
        }

        [HttpPost]
        public IHttpActionResult Ingresar(Asiento asiento)
        {
            if (asiento == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new

                        SqlCommand(@"INSERT INTO RESERVA (ASI_FILA, ASI_LETRA, 
                                                            ASI_DESCRIPCION, ASI_CLASE) 
                                                             VALUES (@ASI_FILA, @ASI_LETRA, @ASI_DESCRIPCION, @ASI_CLASE) ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@ASI_FILA", asiento.ASI_FILA);
                    sqlCommand.Parameters.AddWithValue("@ASI_LETRA", asiento.ASI_LETRA);
                    sqlCommand.Parameters.AddWithValue("@ASI_DESCRIPCION", asiento.ASI_DESCRIPCION);
                    sqlCommand.Parameters.AddWithValue("@ASI_CLASE", asiento.ASI_CLASE);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(asiento);
        }

        [HttpPut]
        public IHttpActionResult Actualizar(Asiento asiento)
        {
            if (asiento == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new
                        SqlCommand(@"UPDATE RESERVA SET ASI_FILA = @ASI_FILA, ASI_LETRA = @ASI_LETRA, 
                                   ASI_DESCRIPCION = @ASI_DESCRIPCION, ASI_CLASE = @ASI_CLASE
                                    WHERE ASI_CODIGO = @ASI_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@ASI_CODIGO", asiento.ASI_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@ASI_FILA", asiento.ASI_FILA);
                    sqlCommand.Parameters.AddWithValue("@ASI_LETRA", asiento.ASI_LETRA);
                    sqlCommand.Parameters.AddWithValue("@ASI_DESCRIPCION", asiento.ASI_DESCRIPCION);
                    sqlCommand.Parameters.AddWithValue("@ASI_CLASE", asiento.ASI_CLASE);


                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(asiento);
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
                        SqlCommand(@"DELETE RESERVA WHERE ASI_CODIGO = @ASI_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@ASI_CODIGO", id);

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