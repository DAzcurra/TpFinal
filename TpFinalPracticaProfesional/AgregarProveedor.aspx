<%@ Page Title="" Language="C#" MasterPageFile="~/MasterProveedor.Master" AutoEventWireup="true" CodeBehind="AgregarProveedor.aspx.cs" Inherits="TpFinalPracticaProfesional.AgregarProveedor" %>
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
                text: 'El proveedor se agrego correctamente',
                type: 'success'
            });
        }
    </script>
    <asp:Panel ID="Panel1" CssClass="PanelPersonalizado" runat="server">
    <h1>Nuevo Proveedor </h1>
    <asp:Label ID="lbNFantasia" CssClass="Labels" runat="server" Text="Nombre Fantasía: " Font-Size="Large"></asp:Label>
    <asp:TextBox ID="TxtNFantasia" CssClass="Textos" runat="server" Width="60%" BackColor="White"></asp:TextBox><div style="position: absolute; top: 75px; left: 862px;"> <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="Vali" runat="server" ErrorMessage="Ingrese nombre fantasia" ControlToValidate="TxtNFantasia">Ingrese nombre fantasia</asp:RequiredFieldValidator></div>
    <asp:Label ID="LbRazonS" CssClass="Labels" runat="server" Text="Razón Social: " Font-Size="Large"></asp:Label>
    <asp:TextBox ID="TxtRazonS" CssClass="Textos" runat="server" Width="60%" BackColor="White"></asp:TextBox><div style="position: absolute; top: 165px; left: 862px;"> <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="Vali" ErrorMessage="Ingrese razón social" ControlToValidate="TxtRazonS">Ingrese razón social</asp:RequiredFieldValidator></div>
    <asp:Label ID="lbNombre" CssClass="Labels" runat="server" Text="Nombre: " Font-Size="Large"></asp:Label>
    <asp:TextBox ID="TxtNombre" CssClass="Textos" runat="server" Width="60%" BackColor="White"></asp:TextBox> <div style="position: absolute; top: 240px; left: 862px;"><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="Vali" ErrorMessage="Ingrese nombre" ControlToValidate="TxtNombre">Ingrese nombre</asp:RequiredFieldValidator></div>
    <asp:Label ID="lbApellido" CssClass="Labels" runat="server" Text="Apellido: " Font-Size="Large"></asp:Label>
    <asp:TextBox ID="TxtApellido" CssClass="Textos" runat="server" Width="60%"></asp:TextBox><div style="position: absolute; top: 311px; left: 862px;"> <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="Vali" ErrorMessage="Ingrese apellido" ControlToValidate="TxtApellido">Ingrese apellido</asp:RequiredFieldValidator></div>
    <asp:Label ID="lbTelefono" CssClass="Labels" runat="server" Text="Telefono:" Font-Size="Large"></asp:Label>
    <asp:TextBox ID="TextTelefono" CssClass="Textos" runat="server" Width="60%" TextMode="Number"></asp:TextBox><div style="position: absolute; top: 385px; left: 862px;">
        <asp:CustomValidator ID="CustomValidator3" CssClass="Vali" ControlToValidate="TextTelefono" OnServerValidate="CustomValidator3_ServerValidate" runat="server" ErrorMessage="Error: solo numeros positivos en este campo">Error: solo numeros positivos en este campo</asp:CustomValidator> <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="Vali" ErrorMessage="Ingrese telefono" ControlToValidate="TextTelefono">Ingrese telefono</asp:RequiredFieldValidator></div>
    <asp:Label ID="LbEmail" CssClass="Labels" runat="server" Text="E-mail: " Font-Size="Large"></asp:Label>
    <asp:TextBox ID="TxtEmail" CssClass="Textos" runat="server" Width="60%" TextMode="Email"></asp:TextBox><div style="position: absolute; top: 457px; left: 862px;"> <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="Vali" ErrorMessage="Ingrese e-mail" ControlToValidate="TxtEmail">Ingrese e-mail</asp:RequiredFieldValidator></div>
    <asp:Label ID="LbCuil" CssClass="Labels" runat="server" Text="Cuit (sin guiones): " Font-Size="Large"></asp:Label>
    <asp:TextBox ID="TxtCuit" CssClass="Textos" runat="server" Width="60%" TextMode="Number"></asp:TextBox> <div style="position: absolute; top: 529px; left: 862px;">
    <asp:CustomValidator ID="CustomValidator2" CssClass="Vali" runat="server" ErrorMessage="Error: solo numeros positivos en este campo" ControlToValidate="TxtCuit" OnServerValidate="CustomValidator2_ServerValidate">Error: solo numeros positivos en este campo</asp:CustomValidator><asp:CustomValidator ID="CustomValidator1" runat="server" CssClass="Vali" ErrorMessage="ERROR: Cuit ingresado en otro proveedor" ControlToValidate="TxtCuit" OnServerValidate="CustomValidator1_ServerValidate">ERROR: Cuit ingresado en otro proveedor</asp:CustomValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" CssClass="Vali" ErrorMessage="Ingrese cuit" ControlToValidate="TxtCuit">Ingrese cuit</asp:RequiredFieldValidator></div>
    <asp:Label ID="LbDireccion" CssClass="Labels" runat="server" Text="Dirección: " Font-Size="Large"></asp:Label>
    <asp:TextBox ID="TxtDirecion" CssClass="Textos" runat="server" Width="60%"></asp:TextBox><div style="position: absolute; top: 601px; left: 862px;"> <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" CssClass="Vali" ErrorMessage="Ingrese telefono" ControlToValidate="TextTelefono">Ingrese telefono</asp:RequiredFieldValidator></div>
    <br />
    <center>
    <asp:Button ID="BtnNuevoCliente" CssClass="Boton" runat="server" Text="Agregar" Width="180px" Height="50px" OnClick="BtnNuevoCliente_Click"/>
       
    </center>
         &nbsp;
    </asp:Panel>
</asp:Content>
