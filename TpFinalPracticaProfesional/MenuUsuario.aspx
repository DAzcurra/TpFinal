<%@ Page Title="" Language="C#" MasterPageFile="~/MasterUsuario.Master" AutoEventWireup="true" CodeBehind="MenuUsuario.aspx.cs" Inherits="TpFinalPracticaProfesional.MenuUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenedorPrincipal" runat="server">
       <script type="text/javascript">
        function erroralert() {
            swal({
                title: '',
                text: 'Usuario o contraseña incorrectos',
                type: 'error'
            });
           }
            function successalert() {
            swal({
                title: '',
                text: 'Usuario modificado',
                type: 'success'
            });
            }
            </script>
    <title>Proar</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
   <script src="https://cdn.jsdelivr.net/npm/sweetalert2@7.22.2/dist/sweetalert2.all.min.js"></script>
    <%--     <link rel="stylesheet" href="https://cdn.metroui.org.ua/v4/css/metro-all.min.css"/>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert.min.js"></script>--%>
    <asp:Panel ID="Panel1" CssClass="PanelPersonalizado" runat="server">
    <h1>Modificar Usuario</h1>
            &nbsp;
    <hr class ="hrclass" />
    <h2 style="color:black">Datos Actuales: </h2>
            &nbsp;
    <center>
    <div class="inline-form">
    <div style="margin-left: 15%"><asp:Label ID="Label1" runat="server" CssClass="Labels" Text="Usuario: " Font-Bold="true"></asp:Label></div>
    <asp:TextBox ID="txtUsuarioActual" runat="server" Width="40%" CssClass="Textos"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass=" Vali1" runat="server" ErrorMessage="campo vacio" ValidationGroup="VtGp" ControlToValidate="txtUsuarioActual"></asp:RequiredFieldValidator>
    </div>
    <br />
    <div class="inline-form">
    <div style="margin-left: 12.5%"><asp:Label ID="Label2" runat="server" CssClass="Labels" Text="Contraseña: " Font-Bold="true"></asp:Label></div>
    <asp:TextBox ID="txtcontraseñaactual" runat="server" Width="40%" CssClass="Textos" TextMode="Password"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass=" Vali1B" runat="server" ErrorMessage="campo vacio" ValidationGroup="VtGp" ControlToValidate="txtcontraseñaactual"></asp:RequiredFieldValidator>
    </div>
     <br />
    <asp:Button ID="btnVerificar" ValidationGroup="VtGp" CssClass="Boton" Width="170px" Height="40px" runat="server" Text="Verificar" OnClick="btnVerificar_Click" />
    &nbsp;
    <hr class ="hrclass" />
    <h2 style="color:black">Datos Nuevos: </h2>
    &nbsp;
    <div class="inline-form">
    <div style="margin-left: 11%"><asp:Label ID="Label5" runat="server" CssClass="Labels" Text="Usuario Nuevo: " Font-Bold="true"></asp:Label> </div>
    <asp:TextBox ID="txtusernuevo" runat="server" Width="40%" CssClass="Textos"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="campo vacio" CssClass="Vali2C" ValidationGroup="VtGpRep" ControlToValidate="txtusernuevo"></asp:RequiredFieldValidator>
    </div>
   &nbsp;
    <div class="inline-form">
    <div style="margin-left: 8.5%"><asp:Label ID="Label3" runat="server" CssClass="Labels" Text="Contraseña Nueva: " Font-Bold="true"></asp:Label> </div>
    <asp:TextBox ID="txtcontraseñaNueva" runat="server" Width="40%" CssClass="Textos" TextMode="Password"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="campo vacio" CssClass="Vali2" ValidationGroup="VtGpRep" ControlToValidate="txtcontraseñaNueva"></asp:RequiredFieldValidator>
    </div>
   <br />
    <div class="inline-form">
    <div style="margin-left: 8%"><asp:Label ID="Label4" runat="server" CssClass="Labels" Text=" Repetir Contraseña: " Font-Bold="true"></asp:Label> </div>
    <asp:TextBox ID="txtcontraseñaNuevaR" runat="server" Width="40%" CssClass="Textos" TextMode="Password"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass=" Vali2B" runat="server" ErrorMessage="campo vacio" ValidationGroup="VtGpRep" ControlToValidate="txtcontraseñaNuevaR"></asp:RequiredFieldValidator>
    </div>
    <asp:CompareValidator ID="CmpContraseña" runat="server" ErrorMessage="No coinciden las contraseñas" ControlToCompare="txtcontraseñaNueva" ControlToValidate="txtcontraseñaNuevaR" CssClass="ValiNoCoincide" ValidationGroup="VtGpRep"></asp:CompareValidator>
    <br />
    <asp:Button ID="BtnModificar" runat="server" Width="170px" Height="40px" CssClass="Boton" Text="Actualizar" OnClick="BtnModificar_Click" ValidationGroup="VtGpRep" />
    &nbsp;
    </center>
    </asp:Panel>
</asp:Content>
