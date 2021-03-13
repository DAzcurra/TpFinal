<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/Login.aspx.cs" Inherits="TpFinalPracticaProfesional.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
           <script type="text/javascript">
        function erroralert() {
            swal({
                title: '',
                text: 'Usuario o contraseña incorrectos',
                icon: 'error'
            });
           }
            </script>
    <title>Proar</title>
    <link rel="stylesheet" type="text/css" href="StyleLogin.css" />
         <link rel="stylesheet" href="https://cdn.metroui.org.ua/v4/css/metro-all.min.css"/>
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert.min.js"></script>
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
</head>
<body>
    <div class="LoginP">
        <h1>Bienvenido</h1>
        <form id="form1" runat="server">
            <p>Usuario</p>
            <asp:TextBox ID="TxtUsuario" runat="server" placeholder="Ingrese Usuario"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="ValiUser" runat="server" ErrorMessage="campo vacio" ControlToValidate="TxtUsuario"></asp:RequiredFieldValidator>
            <p>Contraseña</p>
            <asp:TextBox ID="TxtContraseña" runat="server" TextMode="Password" placeholder="Ingrese Contraseña"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="ValiPass" runat="server" ErrorMessage="campo vacio" ControlToValidate="TxtContraseña"></asp:RequiredFieldValidator>
            <asp:Button ID="BotonIngresar" runat="server" Text="Ingresar" OnClick="BotonIngresar_Click" />
        </form>
    </div>
</body>
</html>
