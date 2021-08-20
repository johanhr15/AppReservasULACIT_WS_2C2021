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
    [RoutePrefix("api/aeropuerto")]
    public class AeropuertoController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Aeropuerto aeropuerto = new Aeropuerto();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT ARP_CODIGO, ARP_PAIS, ARP_CIUDAD,
                                                            ARP_ZONA_HORARIA, ARP_VISA, ARP_CONTROL_VACUNAS
                                                            FROM Aeropuerto
                                                            WHERE ARP_CODIGO = @ARP_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@ARP_CODIGO", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        aeropuerto.ARP_CODIGO = sqlDataReader.GetInt32(0);
                        aeropuerto.ARP_PAIS = sqlDataReader.GetString(1);
                        aeropuerto.ARP_CIUDAD = sqlDataReader.GetString(2);
                        aeropuerto.ARP_ZONA_HORARIA = sqlDataReader.GetString(3);
                        aeropuerto.ARP_VISA = sqlDataReader.GetString(4);
                        aeropuerto.ARP_CONTROL_VACUNAS = sqlDataReader.GetString(5);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
            return Ok(aeropuerto);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Aeropuerto> aeropuertos = new List<Aeropuerto>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT ARP_CODIGO, ARP_PAIS, ARP_CIUDAD,
                                                            ARP_ZONA_HORARIA, ARP_VISA, ARP_CONTROL_VACUNAS
                                                            FROM Aeropuerto", sqlConnection);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Aeropuerto aeropuerto = new Aeropuerto();
                        aeropuerto.ARP_CODIGO = sqlDataReader.GetInt32(0);
                        aeropuerto.ARP_PAIS = sqlDataReader.GetString(1);
                        aeropuerto.ARP_CIUDAD = sqlDataReader.GetString(2);
                        aeropuerto.ARP_ZONA_HORARIA = sqlDataReader.GetString(3);
                        aeropuerto.ARP_VISA = sqlDataReader.GetString(4);
                        aeropuerto.ARP_CONTROL_VACUNAS = sqlDataReader.GetString(5);
                        aeropuertos.Add(aeropuerto);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(aeropuertos);
        }

        [HttpPost]
        public IHttpActionResult Ingresar(Aeropuerto aeropuerto)
        {
            if (aeropuerto == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO AEROPUERTO(ARP_PAIS, 
                                                            ARP_CIUDAD, ARP_ZONA_HORARIA, ARP_VISA,
                                                            ARP_CONTROL_VACUNAS)
                                                            VALUES (@ARP_PAIS, 
                                                            @ARP_CIUDAD, @ARP_ZONA_HORARIA, @ARP_VISA,
                                                            @ARP_CONTROL_VACUNAS)",
                                                            sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@ARP_PAIS", aeropuerto.ARP_PAIS);
                    sqlCommand.Parameters.AddWithValue("@ARP_CIUDAD", aeropuerto.ARP_CIUDAD);
                    sqlCommand.Parameters.AddWithValue("@ARP_ZONA_HORARIA", aeropuerto.ARP_ZONA_HORARIA);
                    sqlCommand.Parameters.AddWithValue("@ARP_VISA", aeropuerto.ARP_VISA);
                    sqlCommand.Parameters.AddWithValue("@ARP_CONTROL_VACUNAS", aeropuerto.ARP_CONTROL_VACUNAS);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(aeropuerto);
        }

        [HttpPut]
        public IHttpActionResult Actualizar(Aeropuerto aeropuerto)
        {
            if (aeropuerto == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new
                        SqlCommand(@"UPDATE AEROPUERTO SET ARP_PAIS = @ARP_PAIS, 
                                                           ARP_CIUDAD = @ARP_CIUDAD, ARP_ZONA_HORARIA = @ARP_ZONA_HORARIA,
                                                           ARP_VISA = @ARP_VISA,
                                                           ARP_CONTROL_VACUNAS = @ARP_CONTROL_VACUNAS
                                                           WHERE ARP_CODIGO = @ARP_CODIGO",
                                                            sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@ARP_CODIGO", aeropuerto.ARP_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@ARP_PAIS", aeropuerto.ARP_PAIS);
                    sqlCommand.Parameters.AddWithValue("@ARP_CIUDAD", aeropuerto.ARP_CIUDAD);
                    sqlCommand.Parameters.AddWithValue("@ARP_ZONA_HORARIA", aeropuerto.ARP_ZONA_HORARIA);
                    sqlCommand.Parameters.AddWithValue("@ARP_VISA", aeropuerto.ARP_VISA);
                    sqlCommand.Parameters.AddWithValue("@ARP_CONTROL_VACUNAS", aeropuerto.ARP_CONTROL_VACUNAS);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(aeropuerto);
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
                        SqlCommand(@"DELETE AEROPUERTO WHERE ARP_CODIGO = @ARP_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@ARP_CODIGO", id);

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
