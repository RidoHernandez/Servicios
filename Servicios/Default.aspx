<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Servicios._Default" Async="true"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <link href="CSS/Style.css" rel="stylesheet" />
        <h2 id="title"><%: Title %>.</h2>

        <div class="container-main">
            <asp:Label ID="lblError" runat="server" CssClass="lbl-error"></asp:Label>
            <div class="title-page">Alta de Cliente y Vehículo</div>
            <div class="subtitle-page">Registro de clientes, vehículos y selección de servicios</div>

            <!-- ==========================
                 SECCIÓN CLIENTE
            =========================== -->
            <div class="card">
                <div class="card-title">📌 Datos del Cliente</div>
                
                <div class="form-group" style="flex: 1;">
                    <label>Seleccionar Cliente</label>
                    <asp:DropDownList ID="ddlClientes" runat="server" CssClass="input" OnSelectedIndexChanged="ddlClientes_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="-- Selecciona un cliente --" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </div>


                <div class="grid-form">

                    <div class="form-group">
                        <label>RFC</label>
                        <asp:TextBox ID="txtRFC" runat="server" CssClass="input" placeholder="Ej: ABCD010203XYZ"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Nombre</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="input" placeholder="Nombre del cliente"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Apellido Paterno</label>
                        <asp:TextBox ID="txtApellidoPaterno" runat="server" CssClass="input" placeholder="Apellido paterno"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Apellido Materno</label>
                        <asp:TextBox ID="txtApellidoMaterno" runat="server" CssClass="input" placeholder="Apellido materno"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Correo</label>
                        <asp:TextBox ID="txtCorreo" runat="server" CssClass="input" placeholder="correo@ejemplo.com"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Fecha de Registro</label>
                        <asp:TextBox ID="txtFechaRegistro" runat="server" CssClass="input" TextMode="Date"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Calle</label>
                        <asp:TextBox ID="txtCalle" runat="server" CssClass="input" placeholder="Ej: Av. Reforma"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Colonia</label>
                        <asp:TextBox ID="txtColonia" runat="server" CssClass="input" placeholder="Ej: Centro"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Código Postal</label>
                        <asp:TextBox ID="txtCP" runat="server" CssClass="input" placeholder="Ej: 64000"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Ciudad</label>
                        <asp:TextBox ID="txtCiudad" runat="server" CssClass="input" placeholder="Ej: Monterrey"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Teléfono 1</label>
                        <asp:TextBox ID="txtTelefono1" runat="server" CssClass="input" placeholder="Ej: 8123456789"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Teléfono 2</label>
                        <asp:TextBox ID="txtTelefono2" runat="server" CssClass="input" placeholder="Opcional"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Teléfono 3</label>
                        <asp:TextBox ID="txtTelefono3" runat="server" CssClass="input" placeholder="Opcional"></asp:TextBox>
                    </div>

                </div>
            </div>


            <!-- ==========================
                 SECCIÓN VEHÍCULO
            =========================== -->
            <div class="card">
                <div class="card-title">🚗 Datos del Vehículo</div>
                
                <div class="form-group" style="flex: 1;">
                    <label>Seleccionar Vehículo</label>
                    <asp:DropDownList ID="ddlVehiculos" runat="server" CssClass="input" OnSelectedIndexChanged="ddlVehiculos_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="-- Selecciona un vehículo --" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </div>


                <div class="grid-form">

                    <div class="form-group">
                        <label>Número de Serie</label>
                        <asp:TextBox ID="txtNumSerie" runat="server" CssClass="input" placeholder="VIN / Número de serie"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Placas</label>
                        <asp:TextBox ID="txtPlacas" runat="server" CssClass="input" placeholder="Ej: ABC-123"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Marca</label>
                        <asp:TextBox ID="txtMarca" runat="server" CssClass="input" placeholder="Ej: Nissan"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Modelo</label>
                        <asp:TextBox ID="txtModelo" runat="server" CssClass="input" placeholder="Ej: Versa"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Año</label>
                        <asp:TextBox ID="txtAnio" runat="server" CssClass="input" placeholder="Ej: 2020"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Color</label>
                        <asp:TextBox ID="txtColor" runat="server" CssClass="input" placeholder="Ej: Blanco"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Kilometraje Actual</label>
                        <asp:TextBox ID="txtKilometraje" runat="server" CssClass="input" placeholder="Ej: 55000"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Tipo</label>
                        <asp:DropDownList ID="ddlTipo" runat="server" CssClass="input">
                            <asp:ListItem Text="Sedán" Value="Sedán"></asp:ListItem>
                            <asp:ListItem Text="SUV" Value="SUV"></asp:ListItem>
                            <asp:ListItem Text="Pickup" Value="Pickup"></asp:ListItem>
                            <asp:ListItem Text="Hatchback" Value="Hatchback"></asp:ListItem>
                            <asp:ListItem Text="Camioneta" Value="Camioneta"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                </div>
            </div>


            <!-- ==========================
                 SECCIÓN SERVICIOS
            =========================== -->
            <div class="card">
                <div class="card-title">🛠 Servicios a Realizar</div>

                <div class="row-service-add">

                    <div class="form-group" style="flex: 1;">
                        <label>Seleccionar Servicio</label>
                        <asp:DropDownList ID="ddlServicios" runat="server" CssClass="input" AutoPostBack="true" OnSelectedIndexChanged="ddlServicios_SelectedIndexChanged">
                            <asp:ListItem Text="-- Selecciona un servicio --" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div>
                        <asp:Button ID="btnAgregarServicio" runat="server" Text="➕ Agregar Servicio" CssClass="btn btn-primary" OnClick="btnAgregarServicio_Click" />
                    </div>

                </div>

                <!-- GRIDVIEW -->
                <asp:GridView ID="gvServicios" runat="server" CssClass="gridview"
                    AutoGenerateColumns="False">
            
                    <Columns>

                        <asp:BoundField DataField="Clave_servicio" HeaderText="Clave" />
                        <asp:BoundField DataField="Nombre_servicio" HeaderText="Servicio" />
                        <asp:BoundField DataField="Costo_base" HeaderText="Costo ($)" />
                        <asp:BoundField DataField="Tiempo_estimado_horas" HeaderText="Horas" />

                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button ID="btnQuitar" runat="server" Text="Quitar" CssClass="btn btn-danger" CommandArgument='<%# Eval("Clave_servicio") %>' OnClick="btnQuitar_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>

                 <!-- CONTENEDOR DE TOTALES -->
                <div style="background:#121111; padding:15px 20px; border-radius:12px; width:280px; box-shadow:0px 2px 8px rgba(0,0,0,0.08);">

                    <h4 style="margin:0 0 10px 0; font-size:16px; font-weight:bold; color:#FFFFFF;">
                        💳 Resumen de Pago
                    </h4>

                    <div style="display:flex; justify-content:space-between; margin-bottom:8px; font-size:14px;">
                        <span style="color:#FFFFFF;">Subtotal:</span>
                        <asp:Label ID="lblSubtotal" runat="server" Text="$0.00" style="font-weight:bold; color:#FFFFFF;"></asp:Label>
                    </div>

                    <div style="display:flex; justify-content:space-between; margin-bottom:8px; font-size:14px;">
                        <span style="color:#FFFFFF;">IVA (16%):</span>
                        <asp:Label ID="lblIVA" runat="server" Text="$0.00" style="font-weight:bold; color:#FFFFFF;"></asp:Label>
                    </div>

                    <hr style="margin:10px 0; border:0; border-top:1px solid #ccc; color:#FFFFFF;" />

                    <div style="display:flex; justify-content:space-between; font-size:16px;">
                        <span style="font-weight:bold; color:#FFFFFF;">Total:</span>
                        <asp:Label ID="lblTotal" runat="server" Text="$0.00" style="font-weight:bold; color:#09DE7B;"></asp:Label>
                    </div>

                </div>

                <div class="row-actions">
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" />
                    <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar Registro" CssClass="btn btn-success" OnClick="btnConfirmar_Click" />
                </div>
                <asp:Label ID="lblResumen" runat="server" EnableViewState="false" CssClass="Resumen"/>

            </div>

        </div>
    </main>

</asp:Content>
