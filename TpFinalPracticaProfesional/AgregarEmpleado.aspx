<%@ Page Title="" Language="C#" MasterPageFile="~/MasterEmpleado.Master" AutoEventWireup="true" CodeBehind="AgregarEmpleado.aspx.cs" Inherits="TpFinalPracticaProfesional.AgregarEmpleado" %>
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
                text: 'El empleado se agrego correctamente',
                type: 'success'
            });
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:Panel ID="Panel1" CssClass="PanelPersonalizado" runat="server">
    <h1>Nuevo Empleado </h1>
    <asp:Label ID="lbNombre" Font-Size="Large" CssClass="LabelsAgregarEmp" runat="server" Text="Nombre: "></asp:Label>
    <asp:TextBox ID="TxtNombre" CssClass="TextosAgregarEmp" runat="server" Width="50%" BackColor="White"></asp:TextBox><div style="position: absolute; top: 94px; left: 862px;"><asp:RequiredFieldValidator CssClass="ValiAgregarEmp" ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtNombre" Display="Dynamic" ErrorMessage="ingrese nombre">ingrese nombre</asp:RequiredFieldValidator></div>
    <asp:Label ID="lbApellido" runat="server" CssClass="LabelsAgregarEmp" Font-Size="Large" Text="Apellido: "></asp:Label>
    <asp:TextBox ID="TxtApellido" runat="server" CssClass="TextosAgregarEmp" Width="50%"></asp:TextBox><div style="position: absolute; top: 165px; left: 862px;"><asp:RequiredFieldValidator CssClass="ValiAgregarEmp" ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtApellido" Display="Dynamic" ErrorMessage="ingrese apellido" >Ingreseapellido</asp:RequiredFieldValidator></div>
    <asp:Label ID="lbTelefono" CssClass="LabelsAgregarEmp" runat="server" Text="Telefono:" Font-Size="Large"></asp:Label>
    <asp:TextBox ID="TxtTelefono" CssClass="TextosAgregarEmp" runat="server" Width="50%" TextMode="Number"></asp:TextBox> <div style="position: absolute; top: 240px; left: 862px;">
    <asp:CustomValidator ID="CustomValidator3" OnServerValidate="CustomValidator3_ServerValidate" ControlToValidate="TxtTelefono" runat="server" CssClass="ValiAgregarEmp" ErrorMessage="Error: solo numeros positivos en este campo">Error: solo numeros positivos en este campo</asp:CustomValidator><asp:RequiredFieldValidator CssClass="ValiAgregarEmp" ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtTelefono" Display="Dynamic" ErrorMessage="Ingrese Telefono">Ingrese Telefono</asp:RequiredFieldValidator></div>
    <asp:Label ID="LbEmail" CssClass="LabelsAgregarEmp" runat="server" Text="E-mail: " Font-Size="Large"></asp:Label>
    <asp:TextBox ID="TxtEmail" CssClass="TextosAgregarEmp" runat="server" Width="50%" TextMode="Email"></asp:TextBox> <div style="position: absolute; top: 311px; left: 862px;"><asp:RequiredFieldValidator CssClass="ValiAgregarEmp" ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtEmail" Display="Dynamic" ErrorMessage="ingrese e-mail" >ingrese e-mail</asp:RequiredFieldValidator></div>
    <asp:Label ID="LbDireccion" CssClass="LabelsAgregarEmp" runat="server" Text="Dirección: " Font-Size="Large"></asp:Label>
    <asp:TextBox ID="TxtDirecion" CssClass="TextosAgregarEmp" runat="server" Width="50%"></asp:TextBox> <div style="position: absolute; top: 385px; left: 862px;"><asp:RequiredFieldValidator CssClass="ValiAgregarEmp" ID="RequiredFieldValidator5" runat="server" ControlToValidate="TxtDirecion" Display="Dynamic" ErrorMessage="ingrese direcion" >ingrese direcion</asp:RequiredFieldValidator></div>
    <asp:Label ID="LbCuil" CssClass="LabelsAgregarEmp" runat="server" Text="Cuil (sin guiones): " Font-Size="Large"></asp:Label> 
    <asp:TextBox ID="TxtCuil" runat="server" CssClass="TextosAgregarEmp" Width="50%" ControlToValidate="TxtCuil" TextMode="Number"></asp:TextBox><div style="position: absolute; top: 457px; left: 862px;"><asp:RequiredFieldValidator CssClass="ValiAgregarEmp" ID="RequiredFieldValidator6" runat="server" ControlToValidate="TxtCuil" Display="Dynamic" ErrorMessage="ingrese cuil">Ingrese cuil</asp:RequiredFieldValidator></div>
        <div style="position: absolute; top: 457px; left: 862px;"><asp:CustomValidator CssClass="ValiAgregarEmp" ID="CustomValidator1" runat="server" ErrorMessage="ERROR: Cuil ingresado en otro empleado" ControlToValidate="TxtCuil" OnServerValidate="CustomValidator1_ServerValidate">ERROR: Cuil ingresado en otro empleado</asp:CustomValidator><asp:CustomValidator CssClass="ValiAgregarEmp" ID="CustomValidator2" runat="server" ErrorMessage="Error: solo numeros positivos en este campo" ControlToValidate="TxtCuil" OnServerValidate="CustomValidator2_ServerValidate">Error: solo numeros positivos en este campo</asp:CustomValidator></div>
        <br />
        <center>
            <asp:Button ID="BtnNuevoCliente" runat="server" CssClass="BotonAgregar" Height="50px" OnClick="BtnNuevoCliente_Click" Text="Agregar" Width="270px" />
        </center>
        &nbsp;
    </asp:Panel>
</asp:Content>
