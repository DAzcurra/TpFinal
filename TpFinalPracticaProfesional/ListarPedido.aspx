<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPedido.Master" AutoEventWireup="true" CodeBehind="ListarPedido.aspx.cs" Inherits="TpFinalPracticaProfesional.ListarPedido" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenedorPrincipal" runat="server">
                    <script type="text/javascript">
        function erroralert() {
            swal({
                title: '',
                text: 'Lista de pedidos vacía ',
                icon: 'error'
            });
            }
        function erroralertFecha() {
            swal({
                title: '',
                text: 'No se encontron pedidos en ese rango de fecha',
                icon: 'error'
            });
          }
           function erroralertCliente() {
            swal({
                title: '',
                text: 'No se encontraron pedidos a nombre de ese cliente',
                icon: 'error'
            });
            }
            </script>
       <title>Proar</title>
    <link rel="stylesheet" href="https://cdn.metroui.org.ua/v4/css/metro-all.min.css"/>
        <link rel="stylesheet" type="text/css" href="StylePedido.css" />
    <script type="text/javascript">
        </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true" EnableScriptLocalization="true"></asp:ScriptManager>
    <asp:Panel ID="Panel1" CssClass="PanelPersonalizado scrolling-table-container" runat="server">
    <h1>Lista De Pedidos</h1>
        <div class="inline-form">
            <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Filtrar Pedidos: "></asp:Label>
            <asp:TextBox ID="TxtFiltroNombre" placeholder="nombre/apellido" AutoPostBack="true"  runat="server" Width="30%" OnTextChanged="TxtFiltroNombre_TextChanged"></asp:TextBox>
            <div class="inline-form">
        <asp:TextBox ID="TxtDesde" runat="server" CssClass="Textos" placeholder="Desde" Width="175px" Height="30px" TextMode="Date"></asp:TextBox>
        <asp:TextBox ID="TxtHasta" runat="server" CssClass="Textos" placeholder="Hasta" Width="175px" Height="30px" TextMode="Date"></asp:TextBox>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="TxtDesde" CssClass="ValiListar" runat="server" ValidationGroup="ValiFecha" ErrorMessage="ingrese fecha inicial">ingrese fecha inicial</asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="TxtHasta" CssClass="ValiHastaListar" runat="server" ValidationGroup="ValiFecha" ErrorMessage="ingrese fecha final">ingrese fecha final</asp:RequiredFieldValidator>
       </div>
            &nbsp;
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" Font-Bold="true">
                <asp:ListItem Selected="True">Cliente</asp:ListItem>
                <asp:ListItem>Fecha</asp:ListItem>
            </asp:RadioButtonList>
            <asp:Button ID="BtnFiltroFecha" Width="180px" Height="35px" ValidationGroup="ValiFecha" runat="server" Text="Filtrar" OnClick="BtnFiltroFecha_Click" CssClass="BusFiltroPedido Boton" />

        </div>
    <p>&nbsp;</p> 
    <asp:GridView ID="GridPedidos" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" CssClass="table table-border cell-border" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" OnPageIndexChanging="GridPedidos_PageIndexChanging" OnRowDataBound="GridPedidos_RowDataBound" PageSize="5">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="nropedido" HeaderText="Nro Pedido" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="fecha" HeaderText="Fecha" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="entregado" HeaderText="Entregado" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="clienteid" HeaderText="Cliente" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="estado" HeaderText="Estado" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Detalle del pedido" ShowHeader="False">
             <ItemTemplate>
                  <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Recursos/PedidoMini.png" Text="Botón" BorderWidth="2px" CausesValidation="false"  OnClick="ImageButton2_Click" />
             </ItemTemplate>
             <ControlStyle BorderColor="Black" CssClass="BotoDataGrid" />
             <ItemStyle HorizontalAlign="Center" />
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Descargar" ShowHeader="False">
             <ItemTemplate>
                  <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Recursos/descarga.png" Text="Botón" BorderWidth="2px" CausesValidation="false"  OnClick="ImageButton2_Click1" />
             </ItemTemplate>
             <ControlStyle BorderColor="Black" CssClass="BotoDataGrid" />
             <ItemStyle HorizontalAlign="Center" />
           </asp:TemplateField>
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
            &nbsp;
</asp:Panel>
    <asp:LinkButton Text="" ID="LinkButton1" runat="server" />
    <ajaxToolkit:ModalPopupExtender CancelControlID="Oculto" ID="ModalPopupExtender2" TargetControlID="LinkButton1" PopupControlID="PanelPedidoPop" runat="server"></ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelPedidoPop" Style="display: none" CssClass="PanelPop scrolling-table-container" runat="server" BorderColor="Black" BorderWidth="0px">
        <div class="BordePop">
            <div class="TituloDetalle">Detalle del Pedido</div>
            <asp:LinkButton Height="30px" style="TEXT-DECORATION: none" Text="X" Font-Bold="true" CssClass="btonX" Width="30px" ID="Oculto" runat="server" />

            <hr class="hrclassPop" />
        </div>
        <asp:GridView ID="GrillaDetalle" AllowPaging ="true" AutoGenerateColumns="False" ShowFooter="True" CssClass="table table-border cell-border" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"  ShowHeaderWhenEmpty="True" OnRowDataBound="GrillaDetalle_RowDataBound" OnPageIndexChanging="GrillaDetalle_PageIndexChanging">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="articuloid" HeaderText="Articulo" ItemStyle-HorizontalAlign="Center" >
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="cantidad" HeaderText="Cantidad" ItemStyle-HorizontalAlign="Center" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="preciovendido" HeaderText="Precio" ItemStyle-HorizontalAlign="Center" >
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
        <p>&nbsp;</p>
    </asp:Panel>
</asp:Content>
