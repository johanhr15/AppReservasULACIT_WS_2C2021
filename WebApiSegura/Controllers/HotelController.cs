using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Http;
using WebApiSegura.Models;

namespace WebApiSegura.Controllers
{
    [Authorize]
    [RoutePrefix("api/hotel")]
    public class HotelController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Hotel hotel = new Hotel();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT HOT_CODIGO, HOT_NOMBRE, HOT_EMAIL, 
                                                                    HOT_DIRECCION, HOT_TELEFONO, HOT_CATEGORIA
                                                             FROM   Hotel
                                                             WHERE HOT_CODIGO = @HOT_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@HOT_CODIGO", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        hotel.HOT_CODIGO = sqlDataReader.GetInt32(0);
                        hotel.HOT_NOMBRE = sqlDataReader.GetString(1);
                        hotel.HOT_EMAIL = sqlDataReader.GetString(2);
                        hotel.HOT_DIRECCION = sqlDataReader.GetString(3);
                        hotel.HOT_TELEFONO = sqlDataReader.GetString(4);
                        hotel.HOT_CATEGORIA = sqlDataReader.GetString(5);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(hotel);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Hotel> hoteles = new List<Hotel>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT HOT_CODIGO, HOT_NOMBRE, HOT_EMAIL, 
                                                                    HOT_DIRECCION, HOT_TELEFONO, HOT_CATEGORIA
                                                             FROM   Hotel", sqlConnection);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Hotel hotel = new Hotel();
                        hotel.HOT_CODIGO = sqlDataReader.GetInt32(0);
                        hotel.HOT_NOMBRE = sqlDataReader.GetString(1);
                        hotel.HOT_EMAIL = sqlDataReader.GetString(2);
                        hotel.HOT_DIRECCION = sqlDataReader.GetString(3);
                        hotel.HOT_TELEFONO = sqlDataReader.GetString(4);
                        hotel.HOT_CATEGORIA = sqlDataReader.GetString(5);
                        hoteles.Add(hotel);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(hoteles);
        }

        [HttpPost]
        public IHttpActionResult Ingresar(Hotel hotel)
        {
            if (hotel == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new
                        SqlCommand(@"INSERT INTO HOTEL (HOT_NOMBRE, HOT_EMAIL, 
                                                                    HOT_DIRECCION, HOT_TELEFONO, HOT_CATEGORIA) 
                                                             VALUES (@HOT_NOMBRE, @HOT_EMAIL, 
                                                                    @HOT_DIRECCION, @HOT_TELEFONO, @HOT_CATEGORIA) ",
                                                                    sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@HOT_NOMBRE", hotel.HOT_NOMBRE);
                    sqlCommand.Parameters.AddWithValue("@HOT_EMAIL", hotel.HOT_EMAIL);
                    sqlCommand.Parameters.AddWithValue("@HOT_DIRECCION", hotel.HOT_DIRECCION);
                    sqlCommand.Parameters.AddWithValue("@HOT_TELEFONO", hotel.HOT_TELEFONO);
                    sqlCommand.Parameters.AddWithValue("@HOT_CATEGORIA", hotel.HOT_CATEGORIA);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(hotel);
        }

        [HttpPut]
        public IHttpActionResult Actualizar(Hotel hotel)
        {
            if (hotel == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new
                        SqlCommand(@"UPDATE HOTEL SET HOT_NOMBRE = @HOT_NOMBRE, HOT_EMAIL = @HOT_EMAIL, 
                                                      HOT_DIRECCION = @HOT_DIRECCION, 
                                                      HOT_TELEFONO = @HOT_TELEFONO, HOT_CATEGORIA = @HOT_CATEGORIA
                                     WHERE HOT_CODIGO = @HOT_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@HOT_CODIGO", hotel.HOT_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@HOT_NOMBRE", hotel.HOT_NOMBRE);
                    sqlCommand.Parameters.AddWithValue("@HOT_EMAIL", hotel.HOT_EMAIL);
                    sqlCommand.Parameters.AddWithValue("@HOT_DIRECCION", hotel.HOT_DIRECCION);
                    sqlCommand.Parameters.AddWithValue("@HOT_TELEFONO", hotel.HOT_TELEFONO);
                    sqlCommand.Parameters.AddWithValue("@HOT_CATEGORIA", hotel.HOT_CATEGORIA);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(hotel);
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
                        SqlCommand(@"DELETE HOTEL WHERE HOT_CODIGO = @HOT_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@HOT_CODIGO", id);

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
