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
    [RoutePrefix("api/estadistica")]
    public class EstadisticaController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Estadistica estadistica = new Estadistica();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT EST_CODIGO, USU_CODIGO, EST_FEC_HORA, 
                                                            EST_NAVEGADOR, EST_PLATAFORMA_DISPOSITIVO, EST_FABRICANTE_DISPOSITIVO, 
                                                            EST_VISTA, EST_ACCION 
                                                            FROM Estadistica
                                                            WHERE EST_CODIGO = @EST_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@EST_CODIGO", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        estadistica.EST_CODIGO = sqlDataReader.GetInt32(0);
                        estadistica.USU_CODIGO = sqlDataReader.GetInt32(1);
                        estadistica.EST_FEC_HORA = sqlDataReader.GetDateTime(2);
                        estadistica.EST_NAVEGADOR = sqlDataReader.GetString(3);
                        estadistica.EST_PLATAFORMA_DISPOSITIVO = sqlDataReader.GetString(4);
                        estadistica.EST_FABRICANTE_DISPOSITIVO = sqlDataReader.GetString(5);
                        estadistica.EST_VISTA = sqlDataReader.GetString(6);
                        estadistica.EST_ACCION = sqlDataReader.GetString(7);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
            return Ok(estadistica);
        }
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Estadistica> estadisticas = new List<Estadistica>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT EST_CODIGO, USU_CODIGO, EST_FEC_HORA, 
                                                            EST_NAVEGADOR, EST_PLATAFORMA_DISPOSITIVO, EST_FABRICANTE_DISPOSITIVO, 
                                                            EST_VISTA, EST_ACCION 
                                                            FROM Estadistica", sqlConnection);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Estadistica estadistica = new Estadistica();
                        estadistica.EST_CODIGO = sqlDataReader.GetInt32(0);
                        estadistica.USU_CODIGO = sqlDataReader.GetInt32(1);
                        estadistica.EST_FEC_HORA = sqlDataReader.GetDateTime(2);
                        estadistica.EST_NAVEGADOR = sqlDataReader.GetString(3);
                        estadistica.EST_PLATAFORMA_DISPOSITIVO = sqlDataReader.GetString(4);
                        estadistica.EST_FABRICANTE_DISPOSITIVO = sqlDataReader.GetString(5);
                        estadistica.EST_VISTA = sqlDataReader.GetString(6);
                        estadistica.EST_ACCION = sqlDataReader.GetString(7);
                        estadisticas.Add(estadistica);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(estadisticas);
        }

        [HttpPost]
        public IHttpActionResult Ingresar(Estadistica estadistica)
        {
            if (estadistica == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO ESTADISTICA(USU_CODIGO, 
                                                            EST_FEC_HORA, EST_NAVEGADOR, EST_PLATAFORMA_DISPOSITIVO,
                                                            EST_FABRICANTE_DISPOSITIVO,EST_VISTA,EST_ACCION)
                                                            VALUES (@USU_CODIGO, @EST_FEC_HORA, 
                                                            @EST_NAVEGADOR, @EST_PLATAFORMA_DISPOSITIVO, @EST_FABRICANTE_DISPOSITIVO,
                                                            @EST_VISTA, @EST_ACCION)",
                                                            sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@USU_CODIGO", estadistica.USU_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@EST_FEC_HORA", estadistica.EST_FEC_HORA);
                    sqlCommand.Parameters.AddWithValue("@EST_NAVEGADOR", estadistica.EST_NAVEGADOR);
                    sqlCommand.Parameters.AddWithValue("@EST_PLATAFORMA_DISPOSITIVO", estadistica.EST_PLATAFORMA_DISPOSITIVO);
                    sqlCommand.Parameters.AddWithValue("@EST_FABRICANTE_DISPOSITIVO", estadistica.EST_FABRICANTE_DISPOSITIVO);
                    sqlCommand.Parameters.AddWithValue("@EST_VISTA", estadistica.EST_VISTA);
                    sqlCommand.Parameters.AddWithValue("@EST_ACCION", estadistica.EST_ACCION);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(estadistica);
        }

        [HttpPut]
        public IHttpActionResult Actualizar(Estadistica estadistica)
        {
            if (estadistica == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new
                        SqlCommand(@"UPDATE ESTADISTICA SET USU_CODIGO = @USU_CODIGO, EST_FEC_HORA = @EST_FEC_HORA, 
                                                            EST_NAVEGADOR = @EST_NAVEGADOR, EST_PLATAFORMA_DISPOSITIVO = @EST_PLATAFORMA_DISPOSITIVO,
                                                            EST_FABRICANTE_DISPOSITIVO = @EST_FABRICANTE_DISPOSITIVO, EST_VISTA = @EST_VISTA,
                                                            EST_ACCION = @EST_ACCION 
                                    WHERE EST_CODIGO = @EST_CODIGO",
                                                            sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@EST_CODIGO", estadistica.EST_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@USU_CODIGO", estadistica.USU_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@EST_FEC_HORA", estadistica.EST_FEC_HORA);
                    sqlCommand.Parameters.AddWithValue("@EST_NAVEGADOR", estadistica.EST_NAVEGADOR);
                    sqlCommand.Parameters.AddWithValue("@EST_PLATAFORMA_DISPOSITIVO", estadistica.EST_PLATAFORMA_DISPOSITIVO);
                    sqlCommand.Parameters.AddWithValue("@EST_FABRICANTE_DISPOSITIVO", estadistica.EST_FABRICANTE_DISPOSITIVO);
                    sqlCommand.Parameters.AddWithValue("@EST_VISTA", estadistica.EST_VISTA);
                    sqlCommand.Parameters.AddWithValue("@EST_ACCION", estadistica.EST_ACCION);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(estadistica);
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
                        SqlCommand(@"DELETE ESTADISTICA WHERE EST_CODIGO = @EST_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@EST_CODIGO", id);

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
