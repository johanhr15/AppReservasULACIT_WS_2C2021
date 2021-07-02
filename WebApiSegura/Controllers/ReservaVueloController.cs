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
    [RoutePrefix("api/ReservaVuelo")]
    public class ReservaVueloController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Reserva_Vuelo reserva_Vuelo = new Reserva_Vuelo();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT RVU_CODIGO, USU_CODIGO, AGE_CODIGO,
                                                            RVU_MONEDA, RVU_PRECIO_TOTAL, RVU_FECHA
                                                            FROM RESERVA_VUELO
                                                            WHERE RVU_CODIGO = @RVU_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@RVU_CODIGO", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        reserva_Vuelo.RVU_CODIGO = sqlDataReader.GetInt32(0);
                        reserva_Vuelo.USU_CODIGO = sqlDataReader.GetInt32(1);
                        reserva_Vuelo.AGE_CODIGO = sqlDataReader.GetInt32(2);
                        reserva_Vuelo.RVU_MONEDA = sqlDataReader.GetString(3);
                        reserva_Vuelo.RVU_PRECIO_TOTAL = sqlDataReader.GetDecimal(4);
                        reserva_Vuelo.RVU_FECHA = sqlDataReader.GetDateTime(5);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
            return Ok(reserva_Vuelo);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Reserva_Vuelo> reserva_Vuelos = new List<Reserva_Vuelo>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT RVU_CODIGO, USU_CODIGO, AGE_CODIGO,
                                                            RVU_MONEDA, RVU_PRECIO_TOTAL, RVU_FECHA
                                                            FROM RESERVA_VUELO", sqlConnection);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Reserva_Vuelo reserva_Vuelo = new Reserva_Vuelo();
                        reserva_Vuelo.RVU_CODIGO = sqlDataReader.GetInt32(0);
                        reserva_Vuelo.USU_CODIGO = sqlDataReader.GetInt32(1);
                        reserva_Vuelo.AGE_CODIGO = sqlDataReader.GetInt32(2);
                        reserva_Vuelo.RVU_MONEDA = sqlDataReader.GetString(3);
                        reserva_Vuelo.RVU_PRECIO_TOTAL = sqlDataReader.GetDecimal(4);
                        reserva_Vuelo.RVU_FECHA = sqlDataReader.GetDateTime(5);
                        reserva_Vuelos.Add(reserva_Vuelo);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(reserva_Vuelos);
        }

        [HttpPost]
        public IHttpActionResult Ingresar(Reserva_Vuelo reserva_Vuelo)
        {
            if (reserva_Vuelo == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO RESERVA_VUELO(USU_CODIGO, AGE_CODIGO,
                                                            RVU_MONEDA, RVU_PRECIO_TOTAL, RVU_FECHA)
                                                            VALUES (@USU_CODIGO, @AGE_CODIGO,
                                                            @RVU_MONEDA, @RVU_PRECIO_TOTAL, @RVU_FECHA)",
                                                            sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@USU_CODIGO", reserva_Vuelo.USU_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@AGE_CODIGO", reserva_Vuelo.AGE_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@RVU_MONEDA", reserva_Vuelo.RVU_MONEDA);
                    sqlCommand.Parameters.AddWithValue("@RVU_PRECIO_TOTAL", reserva_Vuelo.RVU_PRECIO_TOTAL);
                    sqlCommand.Parameters.AddWithValue("@RVU_FECHA", reserva_Vuelo.RVU_FECHA);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(reserva_Vuelo);
        }

        [HttpPut]
        public IHttpActionResult Actualizar(Reserva_Vuelo reserva_Vuelo)
        {
            if (reserva_Vuelo == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new
                        SqlCommand(@"UPDATE RESERVA_VUELO SET USU_CODIGO = @USU_CODIGO, 
                                                           AGE_CODIGO = @AGE_CODIGO, RVU_MONEDA = @RVU_MONEDA,
                                                           RVU_PRECIO_TOTAL = @RVU_PRECIO_TOTAL,
                                                           RVU_FECHA = @RVU_FECHA
                                                           WHERE RVU_CODIGO = @RVU_CODIGO",
                                                            sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@RVU_CODIGO", reserva_Vuelo.RVU_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@USU_CODIGO", reserva_Vuelo.USU_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@AGE_CODIGO", reserva_Vuelo.AGE_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@RVU_MONEDA", reserva_Vuelo.RVU_MONEDA);
                    sqlCommand.Parameters.AddWithValue("@RVU_PRECIO_TOTAL", reserva_Vuelo.RVU_PRECIO_TOTAL);
                    sqlCommand.Parameters.AddWithValue("@RVU_FECHA", reserva_Vuelo.RVU_FECHA);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(reserva_Vuelo);
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
                        SqlCommand(@"DELETE RESERVA_VUELO WHERE RVU_CODIGO = @RVU_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@RVU_CODIGO", id);

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
