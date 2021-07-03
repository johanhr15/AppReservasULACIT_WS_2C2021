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
    [RoutePrefix("api/aerolinea")]
    public class AerolineaController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Aerolinea aerolinea = new Aerolinea();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT AER_CODIGO, AER_NOMBRE, 
                                                            AER_TELEFONO, AER_CORREO, AER_SITIO_WEB, AER_SEDE FROM Aerolinea
                                                            WHERE AER_CODIGO = @AER_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@AER_CODIGO", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        aerolinea.AER_CODIGO = sqlDataReader.GetInt32(0);
                        aerolinea.AER_NOMBRE = sqlDataReader.GetString(1);
                        aerolinea.AER_TELEFONO = sqlDataReader.GetString(2);
                        aerolinea.AER_CORREO = sqlDataReader.GetString(3);
                        aerolinea.AER_SITIO_WEB = sqlDataReader.GetString(4);
                        aerolinea.AER_SEDE = sqlDataReader.GetString(5);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
            return Ok(aerolinea);
        }
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Aerolinea> aerolineas = new List<Aerolinea>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT AER_CODIGO, AER_NOMBRE, 
                                                            AER_TELEFONO, AER_CORREO, AER_SITIO_WEB, 
                                                            AER_SEDE FROM Aerolinea", sqlConnection);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Aerolinea aerolinea = new Aerolinea();
                        aerolinea.AER_CODIGO = sqlDataReader.GetInt32(0);
                        aerolinea.AER_NOMBRE = sqlDataReader.GetString(1);
                        aerolinea.AER_TELEFONO = sqlDataReader.GetString(2);
                        aerolinea.AER_CORREO = sqlDataReader.GetString(3);
                        aerolinea.AER_SITIO_WEB = sqlDataReader.GetString(4);
                        aerolinea.AER_SEDE = sqlDataReader.GetString(5);
                        aerolineas.Add(aerolinea);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(aerolineas);
        }

        [HttpPost]
        public IHttpActionResult Ingresar(Aerolinea aerolinea)
        {
            if (aerolinea == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO AEROLINEA (AER_NOMBRE, 
                                                            AER_TELEFONO, AER_CORREO, AER_SITO_WEB,
                                                            AER_SEDE)
                                                            VALUES (@AER_NOMBRE, 
                                                            @AER_TELEFONO, @AER_CORREO, @AER_SITIO_WEB,
                                                            @AER_SEDE)",
                                                            sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@AER_NOMBRE", aerolinea.AER_NOMBRE);
                    sqlCommand.Parameters.AddWithValue("@AER_TELEFONO", aerolinea.AER_TELEFONO);
                    sqlCommand.Parameters.AddWithValue("@AER_CORREO", aerolinea.AER_CORREO);
                    sqlCommand.Parameters.AddWithValue("@AER_SITIO_WEB", aerolinea.AER_SITIO_WEB);
                    sqlCommand.Parameters.AddWithValue("@AER_SEDE", aerolinea.AER_SEDE);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(aerolinea);
        }

        [HttpPut]
        public IHttpActionResult Actualizar(Aerolinea aerolinea)
        {
            if (aerolinea == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new
                        SqlCommand(@"UPDATE AEROLINEA SET AVI_CODIGO = @AVI_CODIGO, AER_NOMBRE = @AER_NOMBRE, 
                                                            AER_TELEFONO = @AER_TELEFONO, AER_CORREO = @AER_CORREO,
                                                            AER_SITIO_WEB = @AER_SITO_WEB, AER_SEDE = @AER_SEDE
                                    WHERE AER_CODIGO = @AER_CODIGO",
                                                            sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@AER_CODIGO", aerolinea.AER_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@AER_NOMBRE", aerolinea.AER_NOMBRE);
                    sqlCommand.Parameters.AddWithValue("@AER_TELEFONO", aerolinea.AER_TELEFONO);
                    sqlCommand.Parameters.AddWithValue("@AER_CORREO", aerolinea.AER_CORREO);
                    sqlCommand.Parameters.AddWithValue("@AER_SITIO_WEB", aerolinea.AER_SITIO_WEB);
                    sqlCommand.Parameters.AddWithValue("@AER_SEDE", aerolinea.AER_SEDE);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(aerolinea);
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
                        SqlCommand(@"DELETE AEROLINEA WHERE AER_CODIGO = @AER_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@AER_CODIGO", id);

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