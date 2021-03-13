<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/index.aspx.cs" Inherits="TpFinalPracticaProfesional.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Proar</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>

    <link rel="stylesheet" type="text/css" href="StyleMenu.css"/>
    <!--<link rel="stylesheet" href="https://cdn.metroui.org.ua/v4/css/metro-all.min.css"/>-->
    <link rel="stylesheet" href="https://cdn.metroui.org.ua/v4/css/metro-all.min.css">
    <!--<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.0/jquery.min.js"></script>-->
    <link rel="stylesheet" type="text/css" href="jdigiclock-master/jquery.jdigiclock.css"/>


    <!-- UIkit CSS -->
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/uikit@3.6.18/dist/css/uikit.min.css" />

<!-- UIkit JS -->
<script src="https://cdn.jsdelivr.net/npm/uikit@3.6.18/dist/js/uikit.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/uikit@3.6.18/dist/js/uikit-icons.min.js"></script>
</head>
<body style="background-color:gainsboro">   
    <div id="digiclock" style="position:absolute; top:20%; left:75%"></div>         
        <script type="text/javascript" src="jdigiclock-master/jquery.jdigiclock.js"></script>
        <script>
            $('#digiclock').jdigiclock({
                imagesPath:'', 
                lang: 'en', 
                am_pm: false,
                weatherLocationCode: '12814767',
                weatherMetric: 'C', 
                weatherUpdate: '3', 
                svrOffset: 0  
            });
            function AbreVentana() {
                window.open("MostrarManual.aspx");
            }
        </script>
        <form id="form1" runat="server">
            <asp:Button ID="BtnSalir" runat="server" Text="" CssClass="BotonSalir BtnSalir" Width="50px" Height="50px" OnClick="BtnSalir_Click" ToolTip="Cerrar Sesión" />
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                   <div style="width: 80%; margin: 0 auto; text-align:left; top: 50px; left: 0px; height: 165px;">
                       <h1>
                         <asp:Label ID="LbProar" runat="server" Text="Proar" ForeColor="Red" Font-Names="Arial"></asp:Label>
                     </h1>
                    </div>
            <div class="" style="width: 62%; margin: 0 auto; top: -63px; left: 32px;" >             
                <asp:Button class="uk-button uk-button-primary uk-button-large bg-pink TextoAbajoIzq" style=" width: 150px; height: 150px;" ID="LBtnEmpleados" runat="server" Text="Empleados" OnClick="LBtnEmpleados_Click" Font-Size="Small" ForeColor="White"></asp:Button>
                <asp:Button class="uk-button uk-button-primary uk-button-large bg-red TextoAbajoIzq" style="width: 150px; height: 150px;" ID="LBtnFacturacion" runat="server" Text="Facturación" OnClick="LBtnFacturacion_Click" Font-Size="Small"></asp:Button>
                <asp:Button class="uk-button uk-button-primary uk-button-large bg-blue TextoAbajoIzq" style=" width: 150px; height: 150px;" ID="LBtnProveedores" runat="server" Text="Proveedores" OnClick="LBtnProveedores_Click" Font-Size="Small"></asp:Button>
                <asp:Button class="uk-button uk-button-primary uk-button-large bg-orange TextoAbajoIzq" style="width: 150px; height: 150px;" ID="LBtnPedidos" runat="server" Text="Pedidos" OnClick="LBtnPedidos_Click" Font-Size="Small"></asp:Button>
                <asp:Button class="uk-button uk-button-primary uk-button-large bg-violet TextoAbajoIzq" style=" width: 150px; height: 150px;" ID="LBtnArticulos" runat="server" Text="Articulos" OnClick="LBtnArticulos_Click" Font-Size="Small"></asp:Button>
                <div > <br /> </div>
                <asp:Button class="uk-button uk-button-primary uk-button-large bg-green TextoAbajoIzq" style=" width: 150px; height: 150px;" ID="LBtnClientes" runat="server" Text="Clientes" OnClick="LBtnClientes_Click" Font-Size="Small"></asp:Button>
                <asp:Button class="uk-button uk-button-primary uk-button-large bg-grayBlue TextoAbajoIzq" style="width: 150px; height: 150px;" ID="LBtnManual" runat="server" OnClientClick="AbreVentana()" Text="Manual de Usuario" Font-Size="Small"></asp:Button>
                <asp:Button class="uk-button uk-button-primary uk-button-large TextoAbajoIzq" style="background-color: #4a00b3; width: 150px; height: 150px;" ID="LBtnUsuario" runat="server" Text="Usuario" OnClick="LBtnUsuario_Click" Font-Size="Small"></asp:Button>
            </div>
            <asp:LinkButton ID="LinkOculto" runat="server"></asp:LinkButton>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderUno" CancelControlID="Button1" TargetControlID="LinkOculto" PopupControlID="StockPanel" runat="server"></ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="StockPanel" Style="display: none" runat="server" CssClass="PanelPop2 " >
                <div style="background-color: #6677BC">
                <div class="Titulo">Articulo/s con Stock Crítico  </div>
               &nbsp;</div>
                <asp:GridView ID="GridStock" AutoGenerateColumns="False" AllowPaging="True" CssClass="table table-border cell-border" runat="server" CellPadding="1" ForeColor="#333333" GridLines="None" Font-Size="Larger" BorderStyle="Solid" BorderWidth="2px" CellSpacing="4" OnPageIndexChanging="GridStock_PageIndexChanging">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="articuloid" HeaderText="Cod Articulo" ItemStyle-HorizontalAlign="Center" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" ItemStyle-HorizontalAlign="Center" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripcion" ItemStyle-HorizontalAlign="Center"  >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="marca" HeaderText="Marca" ItemStyle-HorizontalAlign="Center"  >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cantidad" HeaderText="Stock" ItemStyle-HorizontalAlign="Center"  >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" Font-Bold="True" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
                  <hr />
                 <div style="left:70%"><asp:Button ID="Button1" runat="server" CausesValidation="false" CssClass="Boton " Text="Aceptar" Height="35px" Width="150px" /></div> 
              <br /><br />
            </asp:Panel>                

        </form>
             <script src="https://code.jquery.com/jquery-3.3.1.min.js"></script>
             <script src="https://cdn.metroui.org.ua/v4/js/metro.min.js"></script>
</body>
</html>
