using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios.POJO
{
    [Serializable]
    public class Servicio
    {
        public int Clave_servicio { get; set; }
        public string Nombre_servicio { get; set; }
        public string Descripcion { get; set; }

        public decimal Costo_base { get; set; }
        public decimal Tiempo_estimado_horas { get; set; }
    }
}