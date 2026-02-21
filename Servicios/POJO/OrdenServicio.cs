using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios.POJO
{
    public class OrdenServicio
    {
        public int Folio_orden { get; set; }

        
        public string Num_serie { get; set; }

        public DateTime Fecha_ingreso { get; set; }
        public DateTime Fecha_estimada_entrega { get; set; }

        
        public DateTime? Fecha_real_entrega { get; set; }

        
        public string Estado { get; set; }

        public decimal Costo_total
        {
            get; set;
        }
    }
}