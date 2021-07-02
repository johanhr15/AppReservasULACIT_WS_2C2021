using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Http;
using WebApiSegura.Models;

namespace WebApiSegura.Controllers
{
    [Authorize]
    [RoutePrefix("api/agencia")]
    public class AgenciaController : ApiController
    {
        public IHttpActionResult GetId(int id)
        {
            Agencia agencia = new Agencia();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT AGE_CODIGO, AGE_NOMBRE, AGE_CORREO, AGE_TELEFONO, AGE_SITIO_WEB, AGE_HORARIO
                                                             FROM  Agencia
                                                             WHERE AGE_CODIGO = @AGE_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@AGE_CODIGO", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        agencia.AGE_CODIGO = sqlDataReader.GetInt32(0);
                        agencia.AGE_NOMBRE = sqlDataReader.GetString(1);
                        agencia.AGE_CORREO = sqlDataReader.GetString(2);
                        agencia.AGE_TELEFONO = sqlDataReader.GetString(3);
                        agencia.AGE_SITIO_WEB = sqlDataReader.GetString(4);
                        agencia.AGE_HORARIO = sqlDataReader.GetString(5);

                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(agencia);
        }

        public IHttpActionResult GetAll()
        {
            List<Agencia> agencias = new List<Agencia>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT AGE_CODIGO, AGE_NOMBRE, AGE_CORREO, AGE_TELEFONO, AGE_SITIO_WEB, AGE_HORARIO
                                                             FROM  Agencia", sqlConnection);

                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Agencia agencia = new Agencia();
                        agencia.AGE_CODIGO = sqlDataReader.GetInt32(0);
                        agencia.AGE_NOMBRE = sqlDataReader.GetString(1);
                        agencia.AGE_CORREO = sqlDataReader.GetString(2);
                        agencia.AGE_TELEFONO = sqlDataReader.GetString(3);
                        agencia.AGE_SITIO_WEB = sqlDataReader.GetString(4);
                        agencia.AGE_HORARIO = sqlDataReader.GetString(5);
                        agencias.Add(agencia);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(agencias);
        }

        [HttpPost]
        public IHttpActionResult Ingresar(Agencia agencia)
        {
            if (agencia == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new
                        SqlCommand(@"INSERT INTO AGENCIA (AGE_NOMBRE, AGE_CORREO, AGE_TELEFONO, AGE_SITIO_WEB, AGE_HORARIO) 
                                                             VALUES (@AGE_NOMBRE, @AGE_CORREO, @AGE_TELEFONO, @AGE_SITIO_WEB, @AGE_HORARIO) ",
                                                             sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@AGE_NOMBRE", agencia.AGE_NOMBRE);
                    sqlCommand.Parameters.AddWithValue("@AGE_CORREO", agencia.AGE_CORREO);
                    sqlCommand.Parameters.AddWithValue("@AGE_TELEFONO", agencia.AGE_TELEFONO);
                    sqlCommand.Parameters.AddWithValue("@AGE_SITIO_WEB", agencia.AGE_SITIO_WEB);
                    sqlCommand.Parameters.AddWithValue("@AGE_HORARIO", agencia.AGE_HORARIO);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(agencia);
        }

        [HttpPut]
        public IHttpActionResult Actualizar(Agencia agencia)
        {
            if (agencia == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new
                        SqlCommand(@"UPDATE AGENCIA SET AGE_NOMBRE = @AGE_NOMBRE , AGE_CORREO = @AGE_CORREO, 
                                                                    AGE_TELEFONO = @AGE_TELEFONO, AGE_SITIO_WEB = @AGE_SITIO_WEB, AGE_HORARIO = @AGE_HORARIO
                                                                    WHERE AGE_CODIGO = @AGE_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@AGE_CODIGO", agencia.AGE_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@AGE_NOMBRE", agencia.AGE_NOMBRE);
                    sqlCommand.Parameters.AddWithValue("@AGE_CORREO", agencia.AGE_CORREO);
                    sqlCommand.Parameters.AddWithValue("@AGE_TELEFONO", agencia.AGE_TELEFONO);
                    sqlCommand.Parameters.AddWithValue("@AGE_SITIO_WEB", agencia.AGE_SITIO_WEB);
                    sqlCommand.Parameters.AddWithValue("@AGE_HORARIO", agencia.AGE_HORARIO);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(agencia);
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
                        SqlCommand(@"DELETE AGENCIA WHERE AGE_CODIGO = @AGE_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@AGE_CODIGO", id);

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