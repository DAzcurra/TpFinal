<%@ Page Title="" Language="C#" MasterPageFile="~/MasterArticulo.Master" AutoEventWireup="true" CodeBehind="AgregarArticulo.aspx.cs" Inherits="TpFinalPracticaProfesional.AgregarArticulo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenedorPrincipal" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert.min.js"></script>
    <script type="text/javascript">
        function successalert() {
            swal({
                title: '',
                text: 'El articulo se agrego correctamente',
                type: 'success'
            });
        }
        function SoloNum(e) {
            var key_press = document.all ? key_press = e.keyCode : key_press = e.which;
            return ((key_press > 47 && key_press < 58) || key_press == 46 || key_press == 44);
        }
</script> 
    <asp:Panel ID="Panel1" CssClass="PanelPersonalizado" runat="server">
    <h1>Nuevo Articulo </h1>
    &nbsp;
    <asp:Label ID="Label1" CssClass="Labels" runat="server" Text="Código de Articulo: " Font-Size="Large"></asp:Label>
    <asp:TextBox ID="txtcod" CssClass="Textos" runat="server" Width="60%" BackColor="White" TextMode="Number"></asp:TextBox> <div style="position: absolute; top: 80px; left: 862px;"><asp:RequiredFieldValidator CssClass="Vali" ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtcod" Display="Dynamic" ErrorMessage="Ingrese código de articulo">Ingrese código de articulo</asp:RequiredFieldValidator><asp:CustomValidator ID="CustomValidator5" runat="server" CssClass="Vali" ErrorMessage="Error: solo numeros positivos en este campo" OnServerValidate="CustomValidator5_ServerValidate" ControlToValidate="txtcod">Error: solo numeros positivos en este campo</asp:CustomValidator><asp:CustomValidator ID="CustomValidator6" runat="server" CssClass="Vali" ErrorMessage="Error: solo numeros positivos en este campo" OnServerValidate="CustomValidator6_ServerValidate" ControlToValidate="txtcod">Error: codigo ingresado en otro articulo</asp:CustomValidator></div>
    <asp:Label ID="lbNombre" CssClass="Labels" runat="server" Text="Nombre: " Font-Size="Large"></asp:Label>
    <asp:TextBox ID="TxtNombre" CssClass="Textos" runat="server" Width="60%" BackColor="White"></asp:TextBox> <div style="position: absolute; top: 150px; left: 862px;"><asp:RequiredFieldValidator CssClass="Vali" ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtNombre" Display="Dynamic" ErrorMessage="Ingrese nombre">Ingrese nombre</asp:RequiredFieldValidator></div>
    <asp:Label ID="lbDescripcion" CssClass="Labels" runat="server" Text="Descripción: " Font-Size="Large"></asp:Label>
    <asp:TextBox ID="TxtDescripcion" CssClass="Textos" runat="server" Width="60%" BackColor="White"></asp:TextBox> <div style="position: absolute; top: 220px; left: 862px;"><asp:RequiredFieldValidator CssClass="Vali" ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtDescripcion" Display="Dynamic" ErrorMessage="Ingrese descripción" >Ingrese descripción</asp:RequiredFieldValidator></div>
    <asp:Label ID="lbMarca" CssClass="Labels" runat="server" Text="Marca: " Font-Size="Large"></asp:Label>
    <asp:TextBox ID="TxtMarca" CssClass="Textos" runat="server" Width="60%"></asp:TextBox> <div style="position: absolute; top: 300px; left: 862px;"><asp:RequiredFieldValidator CssClass="Vali" ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtMarca" Display="Dynamic" ErrorMessage="Ingrese marca">Ingrese marca</asp:RequiredFieldValidator></div>
    <asp:Label ID="lbPrecioAc" CssClass="Labels" runat="server" Text="Precio Actual:" Font-Size="Large"></asp:Label>
    <asp:TextBox ID="TxtPrecioAc" CssClass="Textos" runat="server" Width="60%"></asp:TextBox> <div style="position: absolute; top: 370px; left: 862px;">
    <asp:CustomValidator ID="CustomValidator1" runat="server" CssClass="Vali" ErrorMessage="Error: solo numeros positivos en este campo" OnServerValidate="CustomValidator1_ServerValidate" ControlToValidate="TxtPrecioAc">Error: solo numeros positivos en este campo</asp:CustomValidator><asp:RequiredFieldValidator CssClass="Vali" ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtPrecioAc" Display="Dynamic" ErrorMessage="Ingrese precio actual" >Ingrese precio actual</asp:RequiredFieldValidator></div> 
    <asp:Label ID="LbCantidad" CssClass="Labels" runat="server" Text="Cantidad: " Font-Size="Large"></asp:Label>
    <asp:TextBox ID="TxtCantidad" CssClass="Textos" runat="server" Width="60%" TextMode="Number"></asp:TextBox> <div style="position: absolute; top: 440px; left: 862px;"><asp:CustomValidator ID="CustomValidator2" runat="server" CssClass="Vali" OnServerValidate="CustomValidator2_ServerValidate" ErrorMessage="Error: solo numeros positivos en este campo" ControlToValidate="TxtCantidad">Error: solo numeros positivos en este campo</asp:CustomValidator><asp:RequiredFieldValidator CssClass="Vali" ID="RequiredFieldValidator5" runat="server" ControlToValidate="TxtCantidad" Display="Dynamic" ErrorMessage="Ingrese cantidad" >Ingrese cantidad</asp:RequiredFieldValidator></div>
    <asp:Label ID="LbStockMin" CssClass="Labels" runat="server" Text="Stock Minimo: " Font-Size="Large"></asp:Label>
    <asp:TextBox ID="TxtStockMin" CssClass="Textos" runat="server" Width="60%" TextMode="Number"></asp:TextBox><div style="position: absolute; top: 520px; left: 862px;"><asp:CustomValidator ID="CustomValidator3" CssClass="Vali" OnServerValidate="CustomValidator3_ServerValidate" runat="server" ErrorMessage="Error: solo numeros positivos en este campo" ControlToValidate="TxtStockMin">Error: solo numeros positivos en este campo</asp:CustomValidator><asp:RequiredFieldValidator CssClass="Vali" ID="RequiredFieldValidator6" runat="server" ControlToValidate="TxtStockMin" Display="Dynamic" ErrorMessage="Ingrese stock minimo">Ingrese stock minimo</asp:RequiredFieldValidator></div>
    <asp:Label ID="LbCosto" CssClass="Labels" runat="server" Text="Precio de Costo:" Font-Size="Large"></asp:Label>
    <asp:TextBox ID="txtcosto" CssClass="Textos" runat="server" Width="60%"></asp:TextBox> <div style="position: absolute; top: 590px; left: 862px;">
    <asp:CustomValidator ID="CustomValidator4" runat="server" CssClass="Vali" ErrorMessage="Error: solo numeros positivos en este campo" OnServerValidate="CustomValidator4_ServerValidate" ControlToValidate="txtcosto">Error: solo numeros positivos en este campo</asp:CustomValidator><asp:RequiredFieldValidator CssClass="Vali" ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtcosto" Display="Dynamic" ErrorMessage="Ingrese precio costo" >Ingrese precio costo</asp:RequiredFieldValidator></div> 
    <asp:Label ID="lbproveedor" CssClass="Labels" runat="server" Text="Proveedor:" Font-Size="Large"></asp:Label>
    <asp:DropDownList ID="DdlProveedores" CssClass="Textos" Width="60%" runat="server" DataTextField="nombrefantasia" DataValueField="proveedorid"></asp:DropDownList>
     <br />
    <center>
    <asp:Button ID="BtnNuevoArticulo" CssClass="Boton" runat="server" Text="Agregar" Width="180px" Height="50px" OnClick="BtnNuevoArticulo_Click"  />
    &nbsp;&nbsp;
    </center>
    </asp:Panel>
</asp:Content>
