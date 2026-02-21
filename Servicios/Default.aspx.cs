using Servicios.DAO;
using Servicios.POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Servicios
{
    public partial class _Default : Page
    {
        private Cliente cliente;
        private Vehiculo vehiculo;
        private List<Servicio> servicios;
        private List<Cliente> clientes;
        private List<Vehiculo> vehiculos;
        private Servicio servicio;
        private decimal total;
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
                ddlVehiculos.DataValueField = "Num_Serie";
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
                    DAOVehiculo daoVehiculo= new DAOVehiculo();

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
            if (!string.IsNullOrEmpty(ddlVehiculos.SelectedValue))
            {
                int claveServicio = int.Parse(ddlServicios.SelectedValue);

                try
                {
                    DAOServicio daoServicio = new DAOServicio();

                    servicio = await daoServicio.ObtenerPorClave(claveServicio);

                    if (servicio != null)
                    {
                        
                        var lista = ServiciosSeleccionados;
                        
                        if (!lista.Any(s => s.Clave_servicio == servicio.Clave_servicio))
                        {
                            lista.Add(servicio);
                        }

                        ServiciosSeleccionados = lista;
                        
                        gvServicios.DataSource = lista;
                        gvServicios.DataBind();
                        
                        decimal total = lista.Sum(s => s.Costo_base);
                        lblTotal.Text = total.ToString("C2");

                    }
                }
                catch (Exception ex)
                {
                    lblError.Text = "❌ Error: " + ex.Message;
                }
            }
        }

        //Mover esto aparte en otra carpeta para cargar datos de servicios seleccionados
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
    }
}