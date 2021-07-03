using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Http;
using WebApiSegura.Models;

namespace WebApiSegura.Controllers
{
    [Authorize]
    [RoutePrefix("api/pasajero")]
    public class PasajeroController : ApiController
    {
        public IHttpActionResult GetId(int id)
        {
            Pasajero pasajero = new Pasajero();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT PAS_CODIGO, PAS_PASAPORTE, PAS_NOMBRE, PAS_FEC_NACIMIENTO, 
                                                             PAS_NACIONALIDAD, PAS_CORREO, PAS_TELEFONO
                                                             FROM  Pasajero
                                                             WHERE PAS_CODIGO = @PAS_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@PAS_CODIGO", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        pasajero.PAS_CODIGO = sqlDataReader.GetInt32(0);
                        pasajero.PAS_PASAPORTE = sqlDataReader.GetString(1);
                        pasajero.PAS_NOMBRE = sqlDataReader.GetString(2);
                        pasajero.PAS_FEC_NACIMIENTO = sqlDataReader.GetDateTime(3);
                        pasajero.PAS_NACIONALIDAD = sqlDataReader.GetString(4);
                        pasajero.PAS_CORREO = sqlDataReader.GetString(5);
                        pasajero.PAS_TELEFONO = sqlDataReader.GetString(6);

                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(pasajero);
        }

        public IHttpActionResult GetAll()
        {
            List<Pasajero> pasajeros = new List<Pasajero>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT PAS_CODIGO, PAS_PASAPORTE, PAS_NOMBRE, PAS_FEC_NACIMIENTO, 
                                                             PAS_NACIONALIDAD, PAS_CORREO, PAS_TELEFONO
                                                             FROM  Pasajero", sqlConnection);

                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Pasajero pasajero = new Pasajero();
                        pasajero.PAS_CODIGO = sqlDataReader.GetInt32(0);
                        pasajero.PAS_PASAPORTE = sqlDataReader.GetString(1);
                        pasajero.PAS_NOMBRE = sqlDataReader.GetString(2);
                        pasajero.PAS_FEC_NACIMIENTO = sqlDataReader.GetDateTime(3);
                        pasajero.PAS_NACIONALIDAD = sqlDataReader.GetString(4);
                        pasajero.PAS_CORREO = sqlDataReader.GetString(5);
                        pasajero.PAS_TELEFONO = sqlDataReader.GetString(6);
                        pasajeros.Add(pasajero);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(pasajeros);
        }

        [HttpPost]
        public IHttpActionResult Ingresar(Pasajero pasajero)
        {
            if (pasajero == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new
                        SqlCommand(@"INSERT INTO PASAJERO (PAS_PASAPORTE, PAS_NOMBRE, PAS_FEC_NACIMIENTO, 
                                                           PAS_NACIONALIDAD, PAS_CORREO, PAS_TELEFONO) 
                                                           VALUES (@PAS_PASAPORTE, @PAS_NOMBRE, @PAS_FEC_NACIMIENTO, 
                                                           @PAS_NACIONALIDAD, @PAS_CORREO, @PAS_TELEFONO) ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@PAS_PASAPORTE", pasajero.PAS_PASAPORTE);
                    sqlCommand.Parameters.AddWithValue("@PAS_NOMBRE", pasajero.PAS_NOMBRE);
                    sqlCommand.Parameters.AddWithValue("@PAS_FEC_NACIMIENTO", pasajero.PAS_FEC_NACIMIENTO);
                    sqlCommand.Parameters.AddWithValue("@PAS_NACIONALIDAD", pasajero.PAS_NACIONALIDAD);
                    sqlCommand.Parameters.AddWithValue("@PAS_CORREO", pasajero.PAS_CORREO);
                    sqlCommand.Parameters.AddWithValue("@PAS_TELEFONO", pasajero.PAS_TELEFONO);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(pasajero);
        }

        [HttpPut]
        public IHttpActionResult Actualizar(Pasajero pasajero)
        {
            if (pasajero == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new
                        SqlCommand(@"UPDATE PASAJERO SET PAS_PASAPORTE = @PAS_PASAPORTE, 
                                                         PAS_NOMBRE = @PAS_NOMBRE, PAS_FEC_NACIMIENTO = @PAS_FEC_NACIMIENTO, PAS_NACIONALIDAD = @PAS_NACIONALIDAD, 
                                                         PAS_CORREO = @PAS_CORREO, PAS_TELEFONO = @PAS_TELEFONO
                                     WHERE PAS_CODIGO = @PAS_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@PAS_CODIGO", pasajero.PAS_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@PAS_PASAPORTE", pasajero.PAS_PASAPORTE);
                    sqlCommand.Parameters.AddWithValue("@PAS_NOMBRE", pasajero.PAS_NOMBRE);
                    sqlCommand.Parameters.AddWithValue("@PAS_FEC_NACIMIENTO", pasajero.PAS_FEC_NACIMIENTO);
                    sqlCommand.Parameters.AddWithValue("@PAS_NACIONALIDAD", pasajero.PAS_NACIONALIDAD);
                    sqlCommand.Parameters.AddWithValue("@PAS_CORREO", pasajero.PAS_CORREO);
                    sqlCommand.Parameters.AddWithValue("@PAS_TELEFONO", pasajero.PAS_TELEFONO);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(pasajero);
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
                        SqlCommand(@"DELETE PASAJERO WHERE PAS_CODIGO = @PAS_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@PAS_CODIGO", id);

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
