<%@ Page Title="" Language="C#" MasterPageFile="~/MasterCliente.Master" AutoEventWireup="true" CodeBehind="~/AgregarCliente.aspx.cs" Inherits="TpFinalPracticaProfesional.AgregarCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenedorPrincipal" runat="server">

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@7.22.2/dist/sweetalert2.all.min.js"></script>
   <script src="https://cdn.jsdelivr.net/npm/sweetalert2@7.22.2/package.json"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@7.22.2/LICENSE"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@7.22.2/README.md"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@7.22.2/sweetalert2.d.ts"></script>
    <script type="text/javascript">
        function successalert() {
            swal({
                title: '',
                text: 'El cliente se agrego correctamente',
                type: 'success'
            });
        }
    </script>
    <asp:Panel ID="Panel1" CssClass="PanelPersonalizado" runat="server">
    <h1>Nuevo Cliente </h1>
    &nbsp;
    <asp:Label ID="LbRazonSocial" CssClass="Labels" runat="server" Text="Razón Social: " Font-Size="Large"></asp:Label>
    <asp:TextBox ID="TxtRazonS" CssClass="Textos" runat="server" Width="60%" BackColor="White"></asp:TextBox> <div style="position: absolute; top: 75px; left: 862px;"><asp:RequiredFieldValidator CssClass="Vali" ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtRazonS" Display="Dynamic" ErrorMessage="ingrese razón social">ingrese razón social</asp:RequiredFieldValidator></div>
    <asp:Label ID="lbNombre" CssClass="Labels" runat="server" Text="Nombre: " Font-Size="Large"></asp:Label>
    <asp:TextBox ID="TxtNombre" CssClass="Textos" runat="server" Width="60%" BackColor="White"></asp:TextBox> <div style="position: absolute; top: 165px; left: 862px;"><asp:RequiredFieldValidator CssClass="Vali" ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtNombre" Display="Dynamic" ErrorMessage="ingrese nombre" >ingrese nombre</asp:RequiredFieldValidator></div>
    <asp:Label ID="lbApellido" CssClass="Labels" runat="server" Text="Apellido: " Font-Size="Large"></asp:Label>
    <asp:TextBox ID="TxtApellido" CssClass="Textos" runat="server" Width="60%"></asp:TextBox> <div style="position: absolute; top: 240px; left: 862px;"><asp:RequiredFieldValidator CssClass="Vali" ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtApellido" Display="Dynamic" ErrorMessage="ingrese apellido">ingrese apellido</asp:RequiredFieldValidator></div>
    <asp:Label ID="lbTelefono" CssClass="Labels" runat="server" Text="Telefono:" Font-Size="Large"></asp:Label>
    <asp:TextBox ID="TextTelefono" CssClass="Textos" runat="server" Width="60%"></asp:TextBox> <div style="position: absolute; top: 311px; left: 862px;">
        <asp:CustomValidator ID="CustomValidator1" CssClass="Vali" OnServerValidate="CustomValidator1_ServerValidate" runat="server" ErrorMessage="Error: solo numeros positivos en este campo" ControlToValidate="TextTelefono">Error: solo numeros positivos en este campo</asp:CustomValidator><asp:RequiredFieldValidator CssClass="Vali" ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextTelefono" Display="Dynamic" ErrorMessage="Ingrese telefono" >Ingrese telefono</asp:RequiredFieldValidator></div> 
    <asp:Label ID="LbEmail" CssClass="Labels" runat="server" Text="E-mail: " Font-Size="Large"></asp:Label>
    <asp:TextBox ID="TxtEmail" CssClass="Textos"  runat="server" Width="60%" TextMode="Email"></asp:TextBox> <div style="position: absolute; top: 385px; left: 862px;"><asp:RequiredFieldValidator CssClass="Vali" ID="RequiredFieldValidator5" runat="server" ControlToValidate="TxtEmail" Display="Dynamic" ErrorMessage="ingrese e-mail" >ingrese e-mail</asp:RequiredFieldValidator></div>
    <asp:Label ID="LbDireccion" CssClass="Labels" runat="server" Text="Dirección: " Font-Size="Large"></asp:Label>
    <asp:TextBox ID="TxtDirecion" CssClass="Textos" runat="server" Width="60%"></asp:TextBox><div style="position: absolute; top: 457px; left: 862px;"><asp:RequiredFieldValidator CssClass="Vali" ID="RequiredFieldValidator6" runat="server" ControlToValidate="TxtDirecion" Display="Dynamic" ErrorMessage="ingrese dirección">ingrese dirección</asp:RequiredFieldValidator></div>
        <asp:Label ID="LbCuil" CssClass="Labels" runat="server" Text="Cuil (sin guiones): " Font-Size="Large"></asp:Label> 
    <asp:TextBox ID="TxtCuil" runat="server" CssClass="Textos" Width="60%" ControlToValidate="TxtCuil" TextMode="Number"></asp:TextBox><div style="position: absolute; top: 529px; left: 862px;">
        <asp:CustomValidator ID="CustomValidator3" CssClass="Vali" OnServerValidate="CustomValidator3_ServerValidate" runat="server" ErrorMessage="ERROR: Cuil ingresado en otro cliente !!">ERROR: Cuil ingresado en otro cliente!!</asp:CustomValidator>  <asp:CustomValidator ID="CustomValidator2" CssClass="Vali" runat="server" ErrorMessage="Error: solo numeros positivos en este campo"  OnServerValidate="CustomValidator2_ServerValidate1">Error: solo numeros positivos en este campo</asp:CustomValidator><asp:RequiredFieldValidator CssClass="Vali" ID="RequiredFieldValidator7" runat="server" ControlToValidate="TxtCuil" Display="Dynamic" ErrorMessage="ingrese cuil">ingrese cuil</asp:RequiredFieldValidator></div>
    <asp:Label ID="Label1" CssClass="Labels" runat="server" Text="Tipo de cliente:" Font-Size="Large"></asp:Label>
    <asp:DropDownList CssClass="Textos" ID="DropDownList1" runat="server" Width="60%">
        <asp:ListItem Value="consumidor final">Consumidor Final</asp:ListItem>
        <asp:ListItem Value="monotributista">Monotributista</asp:ListItem>
        <asp:ListItem Value="responsable inscripto">Responsable Inscripto</asp:ListItem>
        <asp:ListItem Value=" sujeto exento"> Sujeto Exento</asp:ListItem>
        </asp:DropDownList>
        <br />
    <center>
    <asp:Button ID="BtnNuevoCliente" CssClass="BotonAgregar" runat="server" Text="Agregar" Width="180px" Height="50px" OnClick="BtnNuevoCliente_Click" />
    &nbsp;&nbsp;
    </center>
    </asp:Panel>
</asp:Content>
