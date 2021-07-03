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
    [RoutePrefix("api/avion")]
    public class AvionController : ApiController
    {
        public IHttpActionResult GetId(int id)
        {
            Avion avion = new Avion();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT AVI_CODIGO, AER_CODIGO, ASI_CODIGO, AVI_MODELO, 
                                                            AVI_TIPO_RUTA, AVI_CAPACIDAD, AVI_EQUIPAJE
                                                            FROM   Avion
                                                             WHERE AVI_CODIGO = @AVI_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@AVI_CODIGO", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        avion.AVI_CODIGO = sqlDataReader.GetInt32(0);
                        avion.AER_CODIGO = sqlDataReader.GetInt32(1);
                        avion.ASI_CODIGO = sqlDataReader.GetInt32(2);
                        avion.AVI_MODELO = sqlDataReader.GetString(3);
                        avion.AVI_TIPO_RUTA = sqlDataReader.GetString(4);
                        avion.AVI_CAPACIDAD = sqlDataReader.GetInt32(5);
                        avion.AVI_EQUIPAJE = sqlDataReader.GetString(6);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(avion);
        }

        public IHttpActionResult GetAll()
        {
            List<Avion> aviones = new List<Avion>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT AVI_CODIGO, AER_CODIGO, ASI_CODIGO, AVI_MODELO, 
                                                            AVI_TIPO_RUTA, AVI_CAPACIDAD, AVI_EQUIPAJE
                                                             FROM Avion", sqlConnection);

                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Avion avion = new Avion();
                        avion.AVI_CODIGO = sqlDataReader.GetInt32(0);
                        avion.AER_CODIGO = sqlDataReader.GetInt32(1);
                        avion.ASI_CODIGO = sqlDataReader.GetInt32(2);
                        avion.AVI_MODELO = sqlDataReader.GetString(3);
                        avion.AVI_TIPO_RUTA = sqlDataReader.GetString(4);
                        avion.AVI_CAPACIDAD = sqlDataReader.GetInt32(5);
                        avion.AVI_EQUIPAJE = sqlDataReader.GetString(6);
                        aviones.Add(avion);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(aviones);
        }

        [HttpPost]
        public IHttpActionResult Ingresar(Avion avion)
        {
            if (avion == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new

                        SqlCommand(@"INSERT INTO AVION (AER_CODIGO, ASI_CODIGO, AVI_MODELO, 
                                   AVI_TIPO_RUTA, AVI_CAPACIDAD, AVI_EQUIPAJE) 
                                   VALUES (@AER_CODIGO, @ASI_CODIGO, @AVI_MODELO, @AVI_TIPO_RUTA, @AVI_CAPACIDAD, @AVI_EQUIPAJE) ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@AER_CODIGO", avion.AER_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@ASI_CODIGO", avion.ASI_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@AVI_MODELO", avion.AVI_MODELO);
                    sqlCommand.Parameters.AddWithValue("@AVI_TIPO_RUTA", avion.AVI_TIPO_RUTA);
                    sqlCommand.Parameters.AddWithValue("@AVI_CAPACIDAD", avion.AVI_CAPACIDAD);
                    sqlCommand.Parameters.AddWithValue("@AVI_EQUIPAJE", avion.AVI_EQUIPAJE);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(avion);
        }

        [HttpPut]
        public IHttpActionResult Actualizar(Avion avion)
        {
            if (avion == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new
                        SqlCommand(@"UPDATE AVION SET AER_CODIGO = @AER_CODIGO, ASI_CODIGO = @ASI_CODIGO, AVI_MODELO = @AVI_MODELO, 
                                   AVI_TIPO_RUTA = @AVI_TIPO_RUTA, AVI_CAPACIDAD = @AVI_CAPACIDAD, AVI_EQUIPAJE = @AVI_EQUIPAJE
                                    WHERE AVI_CODIGO = @AVI_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@AVI_CODIGO", avion.AVI_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@AER_CODIGO", avion.AER_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@ASI_CODIGO", avion.ASI_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@AVI_MODELO", avion.AVI_MODELO);
                    sqlCommand.Parameters.AddWithValue("@AVI_TIPO_RUTA", avion.AVI_TIPO_RUTA);
                    sqlCommand.Parameters.AddWithValue("@AVI_CAPACIDAD", avion.AVI_CAPACIDAD);
                    sqlCommand.Parameters.AddWithValue("@AVI_EQUIPAJE", avion.AVI_EQUIPAJE);


                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(avion);
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
                        SqlCommand(@"DELETE AVION WHERE AVI_CODIGO = @AVI_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@AVI_CODIGO", id);

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