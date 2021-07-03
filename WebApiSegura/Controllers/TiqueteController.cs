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
    [RoutePrefix("api/Tiquete")]
    public class TiqueteController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Tiquete tiquete = new Tiquete();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT TIQ_CODIGO, AER_CODIGO, ESC_CODIGO,
                                                            PAS_CODIGO, VUE_CODIGO, RVU_CODIGO, TIQ_PRECIO, TIQ_ALIMENTACION, TIQ_DEVOLUCION, TIQ_VISA_REQUERIDA
                                                            FROM TIQUETE
                                                            WHERE TIQ_CODIGO = @TIQ_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@TIQ_CODIGO", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        tiquete.TIQ_CODIGO = sqlDataReader.GetInt32(0);
                        tiquete.AER_CODIGO = sqlDataReader.GetInt32(1);
                        tiquete.ESC_CODIGO = sqlDataReader.GetInt32(2);
                        tiquete.PAS_CODIGO = sqlDataReader.GetInt32(3);
                        tiquete.VUE_CODIGO = sqlDataReader.GetInt32(4);
                        tiquete.RVU_CODIGO = sqlDataReader.GetInt32(5);
                        tiquete.TIQ_PRECIO = sqlDataReader.GetDecimal(6);
                        tiquete.TIQ_ALIMENTACION = sqlDataReader.GetString(7);
                        tiquete.TIQ_DEVOLUCION = sqlDataReader.GetString(8);
                        tiquete.TIQ_VISA_REQUERIDA = sqlDataReader.GetString(9);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
            return Ok(tiquete);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Tiquete> tiquetes = new List<Tiquete>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT TIQ_CODIGO, AER_CODIGO, ESC_CODIGO,
                                                            PAS_CODIGO, VUE_CODIGO, RVU_CODIGO, TIQ_PRECIO, TIQ_ALIMENTACION, TIQ_DEVOLUCION, TIQ_VISA_REQUERIDA
                                                            FROM TIQUETE", sqlConnection);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Tiquete tiquete = new Tiquete();
                        tiquete.TIQ_CODIGO = sqlDataReader.GetInt32(0);
                        tiquete.AER_CODIGO = sqlDataReader.GetInt32(1);
                        tiquete.ESC_CODIGO = sqlDataReader.GetInt32(2);
                        tiquete.PAS_CODIGO = sqlDataReader.GetInt32(3);
                        tiquete.VUE_CODIGO = sqlDataReader.GetInt32(4);
                        tiquete.RVU_CODIGO = sqlDataReader.GetInt32(5);
                        tiquete.TIQ_PRECIO = sqlDataReader.GetDecimal(6);
                        tiquete.TIQ_ALIMENTACION = sqlDataReader.GetString(7);
                        tiquete.TIQ_DEVOLUCION = sqlDataReader.GetString(8);
                        tiquete.TIQ_VISA_REQUERIDA = sqlDataReader.GetString(9);
                        tiquetes.Add(tiquete);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(tiquetes);
        }

        [HttpPost]
        public IHttpActionResult Ingresar(Tiquete tiquete)
        {
            if (tiquete == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO TIQUETE(AER_CODIGO, ESC_CODIGO,
                                                            PAS_CODIGO, VUE_CODIGO, RVU_CODIGO, TIQ_PRECIO,TIQ_ALIMENTACION, TIQ_DEVOLUCION, TIQ_VISA_REQUERIDA)
                                                            VALUES (@AER_CODIGO, @ESC_CODIGO,
                                                            @PAS_CODIGO, @VUE_CODIGO, @RVU_CODIGO, @TIQ_PRECIO, @TIQ_ALIMENTACION, @TIQ_DEVOLUCION, @TIQ_VISA_REQUERIDA)",
                                                            sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@AER_CODIGO", tiquete.AER_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@ESC_CODIGO", tiquete.ESC_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@PAS_CODIGO", tiquete.PAS_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@VUE_CODIGO", tiquete.VUE_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@RVU_CODIGO", tiquete.RVU_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@TIQ_PRECIO", tiquete.TIQ_PRECIO);
                    sqlCommand.Parameters.AddWithValue("@TIQ_ALIMENTACION", tiquete.TIQ_ALIMENTACION);
                    sqlCommand.Parameters.AddWithValue("@TIQ_DEVOLUCION", tiquete.TIQ_DEVOLUCION);
                    sqlCommand.Parameters.AddWithValue("@TIQ_VISA_REQUERIDA", tiquete.TIQ_VISA_REQUERIDA);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(tiquete);
        }

        [HttpPut]
        public IHttpActionResult Actualizar(Tiquete tiquete)
        {
            if (tiquete == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new
                        SqlCommand(@"UPDATE tiquete SET AER_CODIGO = @AER_CODIGO, 
                                                           ESC_CODIGO = @ESC_CODIGO, PAS_CODIGO = @PAS_CODIGO,
                                                           VUE_CODIGO = @VUE_CODIGO,
                                                           RVU_CODIGO = @RVU_CODIGO,
                                                           TIQ_PRECIO = @TIQ_PRECIO,
                                                           TIQ_ALIMENTACION = @TIQ_ALIMENTACION,
                                                           TIQ_DEVOLUCION = @TIQ_DEVOLUCION,
                                                           TIQ_VISA_REQUERIDA = @TIQ_VISA_REQUERIDA,
                                                           WHERE TIQ_CODIGO = @TIQ_CODIGO",
                                                            sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@TIQ_CODIGO", tiquete.TIQ_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@AER_CODIGO", tiquete.AER_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@ESC_CODIGO", tiquete.ESC_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@PAS_CODIGO", tiquete.PAS_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@VUE_CODIGO", tiquete.VUE_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@RVU_CODIGO", tiquete.RVU_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@TIQ_PRECIO", tiquete.TIQ_PRECIO);
                    sqlCommand.Parameters.AddWithValue("@TIQ_ALIMENTACION", tiquete.TIQ_ALIMENTACION);
                    sqlCommand.Parameters.AddWithValue("@TIQ_DEVOLUCION", tiquete.TIQ_DEVOLUCION);
                    sqlCommand.Parameters.AddWithValue("@TIQ_VISA_REQUERIDA", tiquete.TIQ_VISA_REQUERIDA);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(tiquete);
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
                        SqlCommand(@"DELETE TIQUETE WHERE TIQ_CODIGO = @TIQ_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@TIQ_CODIGO", id);

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
