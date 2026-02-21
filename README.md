# Sistema de Gestión de Servicios Automotrices

Aplicación web desarrollada en **ASP.NET WebForms (.NET Framework)** para la gestión de:

* Clientes
* Vehículos
* Servicios automotrices
* Cálculo automático de subtotal, IVA y total
* Registro de orden de servicio

El sistema permite seleccionar un cliente, asociar uno de sus vehículos y agregar múltiples servicios a una orden, calculando automáticamente los totales.

---

# Características principales

* Alta y selección de clientes
* Alta y selección de vehículos
* Asociación cliente–vehículo
* Selección dinámica de múltiples servicios
* Cálculo automático de:

* Subtotal
* IVA (16%)
* Total

* Eliminación de servicios desde GridView
* Arquitectura en capas (DAO + Entidades)
* Programación asíncrona con `async/await`
* Persistencia temporal con ViewState

---

### Patrón utilizado

* Separación de responsabilidades
* DAO para acceso a datos
* Entidades como modelos
* WebForms para la interfaz

---

# Tecnologías Utilizadas

* ASP.NET WebForms (.NET Framework)
* C#
* ADO.NET (SqlConnection, SqlCommand, SqlDataReader)
* SQL Server
* HTML5
* CSS3
* Bootstrap (para estilos de botones)
* IIS (Internet Information Services)
* Windows Server (Hosting)

---

# Base de Datos

El sistema se conecta a:

* Microsoft SQL Server

Tablas principales:

* Clientes
* Vehiculos
* Servicios
* (Opcional) OrdenTrabajo
* (Opcional) DetalleOrdenTrabajo

---

# Funcionamiento General

1. Seleccionar cliente desde DropDownList
2. Se cargan sus datos automáticamente
3. Seleccionar vehículo
4. Seleccionar servicio
5. El servicio se agrega al GridView
6. Se recalculan:

   * Subtotal
   * IVA (16%)
   * Total
7. Se confirma el registro

---

# Despliegue en Producción

El sistema está diseñado para alojarse en:

* Windows Server
* IIS (Internet Information Services)

### Pasos generales de despliegue:

1. Publicar proyecto desde Visual Studio
2. Copiar archivos al servidor
3. Crear sitio en IIS
4. Configurar Application Pool (.NET Framework correcto)
5. Configurar cadena de conexión a SQL Server
6. Asignar permisos a la carpeta del sitio

---

# Requisitos del Sistema

Servidor:

* Windows Server 2016 o superior
* IIS habilitado
* .NET Framework 4.7.2 o superior
* SQL Server instalado

Cliente:

* Navegador moderno (Chrome, Edge, Firefox)

---

# Seguridad

* Uso de parámetros en consultas SQL (prevención de SQL Injection)
* No se utiliza `AddWithValue`
* Conexiones cerradas correctamente con `using`
* Manejo básico de excepciones

---

# Autor
Ricardo Abraham Perez Hernandez | 2026
Sistema de Gestión de Servicios Automotrices

# Licencia

Uso académico y demostrativo.

