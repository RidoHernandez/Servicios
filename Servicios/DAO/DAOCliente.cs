using Servicios.Data;
using Servicios.POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Servicios.DAO
{
    public class DAOCliente
    {
        public async Task<List<Cliente>> ObtenerTodos()
        {
            List<Cliente> listaClientes = new List<Cliente>();

            ConexionBD conexion = new ConexionBD();

            using (SqlConnection con = conexion.ObtenerConexion())
            {
                string query = "SELECT * FROM Clientes";

                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Cliente cliente = new Cliente
                    {
                        Clave_cliente = Convert.ToInt32(reader["Clave_cliente"]),
                        RFC = reader["RFC"].ToString(),
                        Nombre = reader["Nombre"].ToString(),
                        Apellido_paterno = reader["Apellido_paterno"].ToString(),
                        Apellido_materno = reader["Apellido_materno"].ToString(),
                        Calle = reader["Calle"].ToString(),
                        Numero = reader["Numero"].ToString(),
                        Numero2 = reader["Numero2"].ToString(),
                        Numero3 = reader["Numero3"].ToString(),
                        Colonia = reader["Colonia"].ToString(),
                        Codigo_postal = reader["Codigo_postal"].ToString(),
                        Ciudad = reader["Ciudad"].ToString(),
                        Correo = reader["Correo"].ToString(),
                        Fecha_registro = Convert.ToDateTime(reader["Fecha_registro"])
                    };

                    listaClientes.Add(cliente);
                }
            }

            return listaClientes;
        }

        public async Task<Cliente> ObtenerPorId(int clienteId)
        {
            Cliente cliente = null;

            using (SqlConnection connection = new ConexionBD().ObtenerConexion())
            {
                await connection.OpenAsync();

                string query = @"SELECT Clave_cliente, RFC, Nombre, Apellido_paterno, Correo,Codigo_Postal, Fecha_Registro, Ciudad,
                                Apellido_materno, Calle, Numero, Numero2, 
                                Numero3, Colonia
                         FROM Clientes
                         WHERE Clave_cliente = @clienteId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@clienteId", clienteId);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            cliente = new Cliente
                            {
                                Clave_cliente = reader.GetInt32(reader.GetOrdinal("Clave_cliente")),
                                RFC = reader["RFC"]?.ToString(),
                                Nombre = reader["Nombre"]?.ToString(),
                                Apellido_paterno = reader["Apellido_paterno"]?.ToString(),
                                Apellido_materno = reader["Apellido_materno"]?.ToString(),
                                Correo = reader["Correo"]?.ToString(),
                                Codigo_postal = reader["Codigo_postal"]?.ToString(),
                                Calle = reader["Calle"]?.ToString(),
                                Numero = reader["Numero"]?.ToString(),
                                Numero2 = reader["Numero2"]?.ToString(),
                                Numero3 = reader["Numero3"]?.ToString(),
                                Colonia = reader["Colonia"]?.ToString(),
                                Ciudad = reader["Ciudad"]?.ToString(),
                            };
                        }
                    }
                }
            }

            return cliente;
        }
    }
}