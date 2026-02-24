using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios.POJO
{
    public class Orden_OrdenServicio
    {
        public int Folio_orden { get; set; }
        public int Clave_servicio { get; set; }

        public int Cantidad { get; set; }

        public decimal Precio_aplicado { get; set; }
    }
}