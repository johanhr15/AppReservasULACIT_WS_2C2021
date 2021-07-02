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
    [RoutePrefix("api/vuelo")]
    public class VueloController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Vuelo vuelo = new Vuelo();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT VUE_CODIGO, AER_CODIGO, VUE_ORI_CODIGO, 
                                                            VUE_DES_CODIGO, VUE_TERMINAL, VUE_PUERTA, VUE_HORA_PARTIDA, VUE_HORA_LLEGADA FROM Vuelo
                                                            WHERE VUE_CODIGO = @VUE_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@VUE_CODIGO", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        vuelo.VUE_CODIGO = sqlDataReader.GetInt32(0);
                        vuelo.AER_CODIGO = sqlDataReader.GetInt32(1);
                        vuelo.VUE_ORI_CODIGO = sqlDataReader.GetInt32(2);
                        vuelo.VUE_DES_CODIGO = sqlDataReader.GetInt32(3);
                        vuelo.VUE_TERMINAL = sqlDataReader.GetString(4);
                        vuelo.VUE_PUERTA = sqlDataReader.GetString(5);
                        vuelo.VUE_HORA_PARTIDA = sqlDataReader.GetDateTime(6);
                        vuelo.VUE_HORA_LLEGADA = sqlDataReader.GetDateTime(7);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
            return Ok(vuelo);
        }
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Vuelo> vuelos = new List<Vuelo>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT VUE_CODIGO, AER_CODIGO, VUE_ORI_CODIGO, 
                                                            VUE_DES_CODIGO, VUE_TERMINAL, VUE_PUERTA, 
                                                            VUE_HORA_PARTIDA, VUE_HORA_LLEGADA FROM Vuelo", sqlConnection);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Vuelo vuelo = new Vuelo();
                        vuelo.VUE_CODIGO = sqlDataReader.GetInt32(0);
                        vuelo.AER_CODIGO = sqlDataReader.GetInt32(1);
                        vuelo.VUE_ORI_CODIGO = sqlDataReader.GetInt32(2);
                        vuelo.VUE_DES_CODIGO = sqlDataReader.GetInt32(3);
                        vuelo.VUE_TERMINAL = sqlDataReader.GetString(4);
                        vuelo.VUE_PUERTA = sqlDataReader.GetString(5);
                        vuelo.VUE_HORA_PARTIDA = sqlDataReader.GetDateTime(6);
                        vuelo.VUE_HORA_LLEGADA = sqlDataReader.GetDateTime(7);
                        vuelos.Add(vuelo);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(vuelos);
        }

        [HttpPost]
        public IHttpActionResult Ingresar(Vuelo vuelo)
        {
            if (vuelo == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO VUELO(AER_CODIGO, VUE_ORI_CODIGO, 
                                                            VUE_DES_CODIGO, VUE_TERMINAL, VUE_PUERTA,
                                                            VUE_HORA_PARTIDA, VUE_HORA_LLEGADA)
                                                            VALUES (@AER_CODIGO, @VUE_ORI_CODIGO, 
                                                            @VUE_DES_CODIGO, @VUE_TERMINAL, @VUE_PUERTA,
                                                            @VUE_HORA_PARTIDA, @VUE_HORA_LLEGADA)",
                                                            sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@AER_CODIGO", vuelo.AER_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@VUE_ORI_CODIGO", vuelo.VUE_ORI_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@VUE_DES_CODIGO", vuelo.VUE_DES_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@VUE_TERMINAL", vuelo.VUE_TERMINAL);
                    sqlCommand.Parameters.AddWithValue("@VUE_PUERTA", vuelo.VUE_PUERTA);
                    sqlCommand.Parameters.AddWithValue("@VUE_HORA_PARTIDA", vuelo.VUE_HORA_PARTIDA);
                    sqlCommand.Parameters.AddWithValue("@VUE_HORA_LLEGADA", vuelo.VUE_HORA_LLEGADA);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(vuelo);
        }

        [HttpPut]
        public IHttpActionResult Actualizar(Vuelo vuelo)
        {
            if (vuelo == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new
                        SqlCommand(@"UPDATE VUELO SET AER_CODIGO = @AER_CODIGO, VUE_ORI_CODIGO = @VUE_ORI_CODIGO,
                                                            VUE_DES_CODIGO = @VUE_DES_CODIGO, VUE_TERMINAL = @VUE_TERMINAL,
                                                            VUE_PUERTA = @VUE_PUERTA, VUE_HORA_PARTIDA = @VUE_HORA_PARTIDA,
                                                            VUE_HORA_LLEGADA = @VUE_HORA_LLEGADA
                                    WHERE VUE_CODIGO = @VUE_CODIGO",
                                                            sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@VUE_CODIGO", vuelo.VUE_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@AER_CODIGO", vuelo.AER_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@VUE_ORI_CODIGO", vuelo.VUE_ORI_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@VUE_DES_CODIGO", vuelo.VUE_DES_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@VUE_TERMINAL", vuelo.VUE_TERMINAL);
                    sqlCommand.Parameters.AddWithValue("@VUE_PUERTA", vuelo.VUE_PUERTA);
                    sqlCommand.Parameters.AddWithValue("@VUE_HORA_PARTIDA", vuelo.VUE_HORA_PARTIDA);
                    sqlCommand.Parameters.AddWithValue("@VUE_HORA_LLEGADA", vuelo.VUE_HORA_LLEGADA);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(vuelo);
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
                        SqlCommand(@"DELETE VUELO WHERE VUE_CODIGO = @VUE_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@VUE_CODIGO", id);

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

