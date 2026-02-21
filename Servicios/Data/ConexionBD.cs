using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace Servicios.Data
{
    public class ConexionBD
    {
        private string connectionString;

        public ConexionBD()
        {
            connectionString = ConfigurationManager
                .ConnectionStrings["ConexionBD"]
                .ConnectionString;
        }

        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(connectionString);
        }
    }
}