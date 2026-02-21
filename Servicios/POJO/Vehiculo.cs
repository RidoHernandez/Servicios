using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios.POJO
{
    public class Vehiculo
    {
        public string Num_serie { get; set; }
        public int Clave_cliente { get; set; }

        public string Placas { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }

        public int Anio { get; set; }
        public string Color { get; set; }

        public int Kilometraje_actual { get; set; }
        public string Tipo { get; set; }

        public int Antiguedad
        {
            get
            {
                return DateTime.Now.Year - Anio;
            }
        }
    }
}