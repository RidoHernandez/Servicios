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
    public class DAOOrdernCompleta
    {
        public async Task InsertarOrdenCompleta(OrdenServicio orden,List<Orden_OrdenServicio> detalles)
        {
            if (orden == null)
                throw new ArgumentNullException(nameof(orden));

            if (detalles == null || !detalles.Any())
                throw new ArgumentException("La orden debe tener al menos un servicio.");

            ConexionBD conexion = new ConexionBD();

            using (SqlConnection con = conexion.ObtenerConexion())
            {
                await con.OpenAsync();
                SqlTransaction transaction = con.BeginTransaction();

                try
                {
                    string queryOrden = @"INSERT INTO Ordenes_Servicio
                    (Fecha_ingreso,
                     Fecha_estimada_entrega,
                     Fecha_real_entrega,
                     Estado,
                     Costo_total,
                     Id_vehiculo)
                     VALUES
                    (@Fecha_ingreso,
                     @Fecha_estimada_entrega,
                     @Fecha_real_entrega,
                     @Estado,
                     @Costo_total,
                     @Id_vehiculo);

                     SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmdOrden = new SqlCommand(queryOrden, con, transaction))
                    {
                        cmdOrden.Parameters.AddWithValue("@Fecha_ingreso", orden.Fecha_ingreso);
                        cmdOrden.Parameters.AddWithValue("@Fecha_estimada_entrega", orden.Fecha_estimada_entrega);

                        if (orden.Fecha_real_entrega.HasValue)
                            cmdOrden.Parameters.AddWithValue("@Fecha_real_entrega", orden.Fecha_real_entrega.Value);
                        else
                            cmdOrden.Parameters.AddWithValue("@Fecha_real_entrega", DBNull.Value);

                        cmdOrden.Parameters.AddWithValue("@Estado", orden.Estado);
                        cmdOrden.Parameters.AddWithValue("@Costo_total", orden.Costo_total);
                        cmdOrden.Parameters.AddWithValue("@Id_vehiculo", orden.Id_vehiculo);

                        var result = await cmdOrden.ExecuteScalarAsync();
                        orden.Folio_orden = Convert.ToInt32(result);
                    }

                    string queryDetalle = @"INSERT INTO Orden_Servicio_Servicio
                    (Folio_orden, Clave_servicio, Cantidad, Precio_aplicado)
                     VALUES
                    (@Folio_orden, @Clave_servicio, @Cantidad, @Precio_aplicado)";

                    foreach (var detalle in detalles)
                    {
                        using (SqlCommand cmdDetalle = new SqlCommand(queryDetalle, con, transaction))
                        {
                            cmdDetalle.Parameters.AddWithValue("@Folio_orden", orden.Folio_orden);
                            cmdDetalle.Parameters.AddWithValue("@Clave_servicio", detalle.Clave_servicio);
                            cmdDetalle.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                            cmdDetalle.Parameters.AddWithValue("@Precio_aplicado", detalle.Precio_aplicado);

                            await cmdDetalle.ExecuteNonQueryAsync();
                        }
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}