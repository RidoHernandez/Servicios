using Servicios.DAO;
using Servicios.POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Servicios
{
    public partial class _Default : Page
    {
        private List<Servicio> servicios;
        private List<Cliente> clientes;
        private List<Vehiculo> vehiculos;
        private Vehiculo vehiculo
        {
            get
            {
                return ViewState["vehiculo"] as Vehiculo;
            }
            set
            {
                ViewState["vehiculo"] = value;
            }
        }
        private Cliente cliente
        {
            get
            {
                return ViewState["cliente"] as Cliente;
            }
            set
            {
                ViewState["cliente"] = value;
            }
        }
        private Servicio ServicioTemporal
        {
            get { return (Servicio)ViewState["ServicioTemporal"]; }
            set { ViewState["ServicioTemporal"] = value; }
        }

        private decimal total
        {
            get
            {
                if (ViewState["total"] == null)
                    ViewState["total"] = 0m;

                return (decimal)ViewState["total"];
            }
            set
            {
                ViewState["total"] = value;
            }
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await CargarClientesAsync();
                await CargarServicios();
            }
        }

        private async Task CargarClientesAsync()
        {
            try
            {
                DAOCliente daoCliente = new DAOCliente();

                clientes = await daoCliente.ObtenerTodos();

                ddlClientes.DataSource = clientes;
                ddlClientes.DataTextField = "Nombre";
                ddlClientes.DataValueField = "Clave_cliente";
                ddlClientes.DataBind();

                ddlClientes.Items.Insert(0, new ListItem("-- Selecciona un cliente --", ""));
            }
            catch (Exception ex)
            {
                lblError.Text = "❌ Error al cargar clientes: " + ex.Message;
            }
        }

        private async Task CargarVehiculosAsync(int clienteId)
        {
            try
            {
                DAOVehiculo daoVehiculo = new DAOVehiculo();
                vehiculos = await daoVehiculo.ObtenerTodos(clienteId);

                ddlVehiculos.DataSource = vehiculos;
                ddlVehiculos.DataTextField = "Modelo";
                ddlVehiculos.DataValueField = "Num_serie";
                ddlVehiculos.DataBind();

                ddlVehiculos.Items.Insert(0, new ListItem("-- Selecciona un Vehiculo --", ""));
            }
            catch (Exception ex)
            {
                lblError.Text = "❌ Error al cargar vehículos: " + ex.Message;
            }
        }

        protected async void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlClientes.SelectedValue))
            {
                int clienteId = int.Parse(ddlClientes.SelectedValue);

                try
                {
                    DAOCliente daoCliente = new DAOCliente();

                    cliente = await daoCliente.ObtenerPorId(clienteId);

                    if (cliente != null)
                    {
                        txtRFC.Text = cliente.RFC;
                        txtNombre.Text = cliente.Nombre;
                        txtApellidoPaterno.Text = cliente.Apellido_paterno;
                        txtApellidoMaterno.Text = cliente.Apellido_materno;
                        txtCalle.Text = cliente.Calle;
                        txtCiudad.Text = cliente.Ciudad;
                        txtCorreo.Text = cliente.Correo;
                        txtCP.Text = cliente.Codigo_postal;
                        txtTelefono1.Text = cliente.Numero;
                        txtTelefono2.Text = cliente.Numero2;
                        txtTelefono3.Text = cliente.Numero3;
                        txtColonia.Text = cliente.Colonia;
                        txtFechaRegistro.Text = cliente.Fecha_registro.ToString("yyyy-MM-dd");
                    }
                    await CargarVehiculosAsync(clienteId);
                }
                catch (Exception ex)
                {
                    lblError.Text = "❌ Error: " + ex.Message;
                }
            }
        }

        private async Task CargarServicios()
        {
            try
            {
                DAOServicio daoServicio = new DAOServicio();
                servicios = await daoServicio.ObtenerTodos();

                ddlServicios.DataSource = servicios;
                ddlServicios.DataTextField = "Nombre_Servicio";
                ddlServicios.DataValueField = "Clave_Servicio";
                ddlServicios.DataBind();

                ddlServicios.Items.Insert(0, new ListItem("-- Selecciona un Servicio --", ""));
            }
            catch (Exception ex)
            {
                lblError.Text = "❌ Error al cargar servicios: " + ex.Message;
            }

        }

        protected async void ddlVehiculos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlVehiculos.SelectedValue))
            {
                string numSerie = ddlVehiculos.SelectedValue;

                try
                {
                    DAOVehiculo daoVehiculo = new DAOVehiculo();

                    vehiculo = await daoVehiculo.ObtenerPorNumSerie(numSerie);

                    if (vehiculo != null)
                    {
                        txtKilometraje.Text = vehiculo.Kilometraje_actual.ToString();
                        txtMarca.Text = vehiculo.Marca;
                        txtColor.Text = vehiculo.Color;
                        txtModelo.Text = vehiculo.Modelo;
                        txtPlacas.Text = vehiculo.Placas;
                        txtAnio.Text = vehiculo.Anio.ToString();
                        txtNumSerie.Text = vehiculo.Num_serie;
                    }
                }
                catch (Exception ex)
                {
                    lblError.Text = "❌ Error: " + ex.Message;
                }
            }
        }

        protected async void ddlServicios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlServicios.SelectedValue))
            {
                int claveServicio = int.Parse(ddlServicios.SelectedValue);

                try
                {
                    DAOServicio daoServicio = new DAOServicio();

                    ServicioTemporal = await daoServicio.ObtenerPorClave(claveServicio);

                    lblError.Text = "Servicio listo para agregar ✔";

                }
                catch (Exception ex)
                {
                    lblError.Text = "❌ Error: " + ex.Message;
                }
            }
        }


        private List<Servicio> ServiciosSeleccionados
        {
            get
            {
                if (ViewState["ServiciosSeleccionados"] == null)
                    ViewState["ServiciosSeleccionados"] = new List<Servicio>();

                return (List<Servicio>)ViewState["ServiciosSeleccionados"];
            }
            set
            {
                ViewState["ServiciosSeleccionados"] = value;
            }
        }

        protected void btnAgregarServicio_Click(object sender, EventArgs e)
        {
            if (ServicioTemporal != null)
            {
                var lista = ServiciosSeleccionados;

                if (!lista.Any(s => s.Clave_servicio == ServicioTemporal.Clave_servicio))
                {
                    lista.Add(ServicioTemporal);
                    total += ServicioTemporal.Costo_base;
                }

                ServiciosSeleccionados = lista;

                gvServicios.DataSource = lista;
                gvServicios.DataBind();

                lblTotal.Text = total.ToString("C2");

                ServicioTemporal = null;

                if (gvServicios.Rows.Count > 0)
                {
                    gvServicios.UseAccessibleHeader = true;
                    gvServicios.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        protected void btnQuitar_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            int claveServicio = int.Parse(btn.CommandArgument);

            var lista = ServiciosSeleccionados;

            var servicioAEliminar = lista
                .FirstOrDefault(s => s.Clave_servicio == claveServicio);

            if (servicioAEliminar != null)
            {
                lista.Remove(servicioAEliminar);

                total -= servicioAEliminar.Costo_base;

                if (total < 0)
                    total = 0;

                ServiciosSeleccionados = lista;
            }

            gvServicios.DataSource = ServiciosSeleccionados;
            gvServicios.DataBind();

            lblSubtotal.Text = total.ToString("C2");
            lblIVA.Text = (total * 0.16m).ToString("C2");
            total += (total * 0.16m);
            lblTotal.Text = total.ToString("C2");
        }

        protected async void btnConfirmar_Click(object sender, EventArgs e)
        {

            if (cliente == null || vehiculo == null)
            {
                lblError.Text = "❌ Falta seleccionar cliente o vehículo.";
                return;
            }

            if (ServiciosSeleccionados == null || !ServiciosSeleccionados.Any())
            {
                lblError.Text = "❌ No hay servicios seleccionados.";
                return;
            }

            DateTime fechaEstimada;
            if (!DateTime.TryParse(txtFechaEstimada.Text, out fechaEstimada))
            {
                lblError.Text = "❌ Formato de fecha inválido.";
                return;
            }

            // Construir resumen
            System.Text.StringBuilder resumen = new System.Text.StringBuilder();

            resumen.Append("<h3>📋 Resumen de Orden</h3>");

            // Cliente
            resumen.Append("<b>Cliente:</b><br/>");
            resumen.Append($"Clave: {cliente.Clave_cliente} - ");
            resumen.Append($"{cliente.Nombre}<br/><br/>");

            // Vehículo
            resumen.Append("<b>Vehículo:</b><br/>");
            resumen.Append($"Id_Vehiculo: {vehiculo.Id_vehiculo} - ");
            resumen.Append($"{vehiculo.Marca} {vehiculo.Modelo}<br/><br/>");

            // Servicios
            resumen.Append("<b>Servicios:</b><br/>");

            foreach (var servicio in ServiciosSeleccionados)
            {
                resumen.Append($"• {servicio.Clave_servicio} - ");
                resumen.Append($"{servicio.Nombre_servicio} ");
                resumen.Append($"({servicio.Costo_base:C})<br/>");
            }

            resumen.Append("<br/>");


            resumen.Append($"<b>Total:</b> {total:C}");


            lblResumen.Text = resumen.ToString();

            //Transacciones de base de datos para guardar la orden, servicios y detalles
            try
            {
                OrdenServicio ordenServicio = new OrdenServicio
                {
                    Id_vehiculo = vehiculo.Id_vehiculo,
                    Fecha_ingreso = DateTime.Now,
                    Fecha_estimada_entrega = fechaEstimada,
                    Fecha_real_entrega = null,
                    Estado = "En Proceso",
                    Costo_total = total
                };

                List<Orden_OrdenServicio> detalles = new List<Orden_OrdenServicio>();
                foreach (var servicio in ServiciosSeleccionados)
                {
                    Orden_OrdenServicio detalle = new Orden_OrdenServicio
                    {   
                        Folio_orden = ordenServicio.Folio_orden,
                        
                        Clave_servicio = servicio.Clave_servicio,
                        Cantidad = 1,
                        Precio_aplicado = servicio.Costo_base
                    };
                    detalles.Add(detalle);
                }

                DAOOrdernCompleta daoOrdernCompleta = new DAOOrdernCompleta();
                await daoOrdernCompleta.InsertarOrdenCompleta(ordenServicio, detalles);

                //Mensake de que todo esta bien
                lblResumen.Text = "Los datos se insertaron correctamente. ✔<br/><br/>" + lblResumen.Text;

                //Limpieza de los registros temporales
                ServiciosSeleccionados = new List<Servicio>();
                total = 0m;
                ServicioTemporal = null;
                cliente = null;
                vehiculo = null;

                gvServicios.DataSource = null;
                gvServicios.DataBind();

                lblTotal.Text = "$0.00";
                lblSubtotal.Text = "$0.00";
                lblIVA.Text = "$0.00";
                lblResumen.Text = "";
                lblError.Text = "";

                //Limpieza de cajas de texto
                txtKilometraje.Text = "";
                txtMarca.Text = "";
                txtColor.Text = "";
                txtModelo.Text = "";
                txtPlacas.Text = "";
                txtAnio.Text = "";
                txtNumSerie.Text = "";

                txtRFC.Text = "";
                txtNombre.Text = "";
                txtApellidoPaterno.Text = "";
                txtApellidoMaterno.Text = "";
                txtCalle.Text = "";
                txtCiudad.Text = "";
                txtCorreo.Text = "";
                txtCP.Text = "";
                txtTelefono1.Text = "";
                txtTelefono2.Text = "";
                txtTelefono3.Text = "";
                txtColonia.Text = "";
                txtFechaRegistro.Text = "";

            }
            catch (Exception ex)
            {
                lblError.Text = "❌ Error: " + ex.Message;
                return;
            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            //Limpieza de los registros temporales
            ServiciosSeleccionados = new List<Servicio>();
            total = 0m;
            ServicioTemporal = null;
            cliente = null;
            vehiculo = null;

            gvServicios.DataSource = null;
            gvServicios.DataBind();

            lblTotal.Text = "$0.00";
            lblSubtotal.Text = "$0.00";
            lblIVA.Text = "$0.00";
            lblResumen.Text = "";
            lblError.Text = "";

            //Limpieza de cajas de texto
            txtKilometraje.Text = "";
            txtMarca.Text = "";
            txtColor.Text = "";
            txtModelo.Text = "";
            txtPlacas.Text = "";
            txtAnio.Text = "";
            txtNumSerie.Text = "";

            txtRFC.Text = "";
            txtNombre.Text = "";
            txtApellidoPaterno.Text = "";
            txtApellidoMaterno.Text = "";
            txtCalle.Text = "";
            txtCiudad.Text = "";
            txtCorreo.Text = "";
            txtCP.Text = "";
            txtTelefono1.Text = "";
            txtTelefono2.Text = "";
            txtTelefono3.Text = "";
            txtColonia.Text = "";
            txtFechaRegistro.Text = "";
        }
    }
}