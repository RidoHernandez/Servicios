using Servicios.Data;
using Servicios.POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.SqlClient;

namespace Servicios.DAO
{
    public class DAOServicio
    {
        public async Task<List<Servicio>> ObtenerTodos()
        {
            List<Servicio> listaServicios = new List<Servicio>();

            ConexionBD conexion = new ConexionBD();

            using (SqlConnection con = conexion.ObtenerConexion())
            {
                string query = "SELECT * FROM Servicios";

                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Servicio servicio = new Servicio
                    {
                        Clave_servicio = reader.GetInt32(reader.GetOrdinal("Clave_servicio")),
                        Nombre_servicio = reader.GetString(reader.GetOrdinal("Nombre_servicio")),
                        Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),

                        Costo_base = reader.GetDecimal(reader.GetOrdinal("Costo_base")),
                        Tiempo_estimado_horas = reader.GetDecimal(reader.GetOrdinal("Tiempo_estimado_horas"))
                    };

                    listaServicios.Add(servicio);
                }
            }

            return listaServicios;
        }

        public async Task<Servicio> ObtenerPorClave(int claveServicio)
        {
            Servicio servicio = null;

            using (SqlConnection connection = new ConexionBD().ObtenerConexion())
            {
                await connection.OpenAsync();

                string query = @"SELECT Clave_servicio, Nombre_servicio, Descripcion,
                                Costo_base, Tiempo_estimado_horas
                         FROM Servicios
                         WHERE Clave_servicio = @ClaveServicio";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@ClaveServicio",
                                           System.Data.SqlDbType.Int).Value = claveServicio;

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            servicio = new Servicio
                            {
                                Clave_servicio = reader.GetInt32(reader.GetOrdinal("Clave_servicio")),
                                Nombre_servicio = reader["Nombre_servicio"]?.ToString(),
                                Descripcion = reader["Descripcion"]?.ToString(),
                                Costo_base = reader.GetDecimal(reader.GetOrdinal("Costo_base")),
                                Tiempo_estimado_horas = reader.GetDecimal(reader.GetOrdinal("Tiempo_estimado_horas"))
                            };
                        }
                    }
                }
            }

            return servicio;
        }
    }
}