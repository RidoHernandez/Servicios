using Servicios.Data;
using Servicios.POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Servicios.DAO
{
    public class DAOVehiculo
    {
        public async Task<List<Vehiculo>> ObtenerTodos(int idCliente)
        {
            List<Vehiculo> listaVehiculos = new List<Vehiculo>();

            using (SqlConnection con = new ConexionBD().ObtenerConexion())
            {
                string query = @"SELECT *
                         FROM Vehiculos
                         WHERE Clave_cliente = @IdCliente";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@IdCliente", System.Data.SqlDbType.Int)
                                  .Value = idCliente;

                    await con.OpenAsync();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Vehiculo vehiculo = new Vehiculo
                            {   
                                Id_vehiculo= Convert.ToInt32(reader["Id_vehiculo"]),
                                Num_serie = reader["Numero_serie"]?.ToString(),
                                Clave_cliente = reader["Clave_cliente"] != DBNull.Value? Convert.ToInt32(reader["Clave_cliente"]): 0,
                                Placas = reader["Placas"]?.ToString(),
                                Marca = reader["Marca"]?.ToString(),
                                Modelo = reader["Modelo"]?.ToString(),
                                Anio = reader["Anio"] != DBNull.Value? Convert.ToInt32(reader["Anio"]): 0,
                                Color = reader["Color"]?.ToString(),
                                Kilometraje_actual = reader["Kilometraje_actual"] != DBNull.Value? Convert.ToInt32(reader["Kilometraje_actual"]): 0,
                                Tipo = reader["Tipo"]?.ToString()
                            };

                            listaVehiculos.Add(vehiculo);
                        }
                    }
                }
            }

            return listaVehiculos;
        }

        public async Task<Vehiculo> ObtenerPorNumSerie(string numSerie)
        {
            Vehiculo vehiculo = null;

            using (SqlConnection connection = new ConexionBD().ObtenerConexion())
            {
                await connection.OpenAsync();

                string query = @"SELECT Numero_serie, Clave_cliente, Placas, Marca, Modelo,
                                Anio, Color, Kilometraje_actual, Tipo
                         FROM Vehiculos
                         WHERE Numero_serie = @NumSerie";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@NumSerie", System.Data.SqlDbType.VarChar).Value = numSerie;

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            vehiculo = new Vehiculo
                            {
                                Num_serie = reader["Numero_serie"]?.ToString(),
                                Clave_cliente = reader.GetInt32(reader.GetOrdinal("Clave_cliente")),
                                Placas = reader["Placas"]?.ToString(),
                                Marca = reader["Marca"]?.ToString(),
                                Modelo = reader["Modelo"]?.ToString(),
                                Anio = reader.GetInt32(reader.GetOrdinal("Anio")),
                                Color = reader["Color"]?.ToString(),
                                Kilometraje_actual = reader.GetInt32(reader.GetOrdinal("Kilometraje_actual")),
                                Tipo = reader["Tipo"]?.ToString()
                            };
                        }
                    }
                }
            }

            return vehiculo;
        }

    }
}