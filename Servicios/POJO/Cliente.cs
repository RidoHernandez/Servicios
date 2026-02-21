using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios.POJO
{
    public class Cliente
    {
        public int Clave_cliente { get; set; }
        public string RFC { get; set; }

        public string Nombre { get; set; }
        public string Apellido_paterno { get; set; }
        public string Apellido_materno { get; set; }

        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Numero2 { get; set; }
        public string Numero3 { get; set; }
        public string Colonia { get; set; }
        public string Codigo_postal { get; set; }
        public string Ciudad { get; set; }

        public string Correo { get; set; }
        public DateTime Fecha_registro { get; set; }


        public string NombreCompleto()
        {
            return $"{Nombre} {Apellido_paterno} {Apellido_materno}".Trim();
        }

    }
}