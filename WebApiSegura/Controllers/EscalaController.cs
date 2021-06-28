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
    [RoutePrefix("api/escala")]
    public class EscalaController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Escala escala = new Escala();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT ESC_CODIGO, ESC_NUMERO_TERMINAL, ESC_ARP_CODIGO, 
                                                                   ESC_TIEMPO_ESPERA, ESC_TRASBORDO
                                                             FROM   Escala
                                                             WHERE ESC_CODIGO = @ESC_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@ESC_CODIGO", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        escala.ESC_CODIGO = sqlDataReader.GetInt32(0);
                        escala.ESC_NUMERO_TERMINAL = sqlDataReader.GetInt32(1);
                        escala.ESC_ARP_CODIGO = sqlDataReader.GetInt32(2);
                        escala.ESC_TIEMPO_ESPERA = sqlDataReader.GetDateTime(3);
                        escala.ESC_TRASBORDO = sqlDataReader.GetString(4);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(escala);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Escala> escalas = new List<Escala>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT ESC_CODIGO, ESC_NUMERO_TERMINAL, ESC_ARP_CODIGO, 
                                                                    ESC_TIEMPO_ESPERA, ESC_TRASBORDO
                                                             FROM   Escala", sqlConnection);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Escala escala = new Escala();
                        escala.ESC_CODIGO = sqlDataReader.GetInt32(0);
                        escala.ESC_NUMERO_TERMINAL = sqlDataReader.GetInt32(1);
                        escala.ESC_ARP_CODIGO = sqlDataReader.GetInt32(2);
                        escala.ESC_TIEMPO_ESPERA = sqlDataReader.GetDateTime(3);
                        escala.ESC_TRASBORDO = sqlDataReader.GetString(4);
                        escalas.Add(escala);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(escalas);
        }

        [HttpPost]
        public IHttpActionResult Ingresar(Escala escala)
        {
            if (escala == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new
                        SqlCommand(@"INSERT INTO ESCALA (ESC_NUMERO_TERMINAL, ESC_ARP_CODIGO, 
                                                                    ESC_TIEMPO_ESPERA, ESC_TRASBORDO) 
                                                             VALUES (@ESC_NUMERO_TERMINAL, @ESC_ARP_CODIGO, 
                                                                    @ESC_TIEMPO_ESPERA, @ESC_TRASBORDO) ",
                                                                    sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@ESC_NUMERO_TERMINAL", escala.ESC_NUMERO_TERMINAL);
                    sqlCommand.Parameters.AddWithValue("@ESC_ARP_CODIGO", escala.ESC_ARP_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@ESC_TIEMPO_ESPERA", escala.ESC_TIEMPO_ESPERA);
                    sqlCommand.Parameters.AddWithValue("@ESC_TRASBORDO", escala.ESC_TRASBORDO);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(escala);
        }

        [HttpPut]
        public IHttpActionResult Actualizar(Escala escala)
        {
            if (escala == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new
                        SqlCommand(@"UPDATE ESCALA SET ESC_NUMERO_TERMINAL = @ESC_NUMERO_TERMINAL, ESC_ARP_CODIGO = @ESC_ARP_CODIGO, 
                                                      ESC_TIEMPO_ESPERA = @ESC_TIEMPO_ESPERA, 
                                                      ESC_TRASBORDO = @ESC_TRASBORDO
                                     WHERE ESC_CODIGO = @ESC_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@ESC_CODIGO", escala.ESC_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@ESC_NUMERO_TERMINAL", escala.ESC_NUMERO_TERMINAL);
                    sqlCommand.Parameters.AddWithValue("@ESC_ARP_CODIGO", escala.ESC_ARP_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@ESC_TIEMPO_ESPERA", escala.ESC_TIEMPO_ESPERA);
                    sqlCommand.Parameters.AddWithValue("@ESC_TRASBORDO", escala.ESC_TRASBORDO);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(escala);
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
                        SqlCommand(@"DELETE ESCALA WHERE ESC_CODIGO = @ESC_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@ESC_CODIGO", id);

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
