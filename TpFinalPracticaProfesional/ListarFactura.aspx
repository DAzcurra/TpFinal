<%@ Page Title="" Language="C#" MasterPageFile="~/MasterFacturacion.Master" AutoEventWireup="true" CodeBehind="ListarFactura.aspx.cs" Inherits="TpFinalPracticaProfesional.ListarFactura" EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenedorPrincipal" runat="server">
                <script type="text/javascript">
        function erroralert() {
            swal({
                title: '',
                text: 'Lista de facturas vacía ',
                type: 'error'
            });
                    }
           function erroralertFecha() {
            swal({
                title: '',
                text: 'No se encontron facturas en ese rango de fecha',
                type: 'error'
            });
          }
           function erroralertCliente() {
            swal({
                title: '',
                text: 'No se encontraron facturas a nombre de ese cliente',
                type: 'error'
            });
            }
            </script>
    <title>Proar</title>
     <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert.css" />
    <link rel="stylesheet" type="text/css" href="StyleFacturacion.css"/>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert.min.js"></script>
    <script type="text/javascript">
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Panel ID="Panel1" CssClass="PanelPersonalizado scrolling-table-container" runat="server">
    <h1>Lista De Factura</h1>
     <div class="inline-form">
            <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Filtrar Facturas: "></asp:Label>
            <asp:TextBox ID="TxtFiltroNombre" placeholder="nombre/apellido" AutoPostBack="true"  runat="server" Width="30%" OnTextChanged="TxtFiltroNombre_TextChanged"></asp:TextBox>
            <div class="inline-form">
        <asp:TextBox ID="TxtDesde" runat="server" CssClass="Textos2" placeholder="Desde" Width="175px" Height="30px" TextMode="Date"></asp:TextBox>
        <asp:TextBox ID="TxtHasta" runat="server" CssClass="Textos2" placeholder="Hasta" Width="175px" Height="30px" TextMode="Date"></asp:TextBox>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="TxtDesde" CssClass="ValiListar" runat="server" ValidationGroup="ValiFecha" ErrorMessage="ingrese fecha inicial">ingrese fecha inicial</asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="TxtHasta" CssClass="ValiHastaListar" runat="server" ValidationGroup="ValiFecha" ErrorMessage="ingrese fecha final">ingrese fecha final</asp:RequiredFieldValidator>
       </div>
            &nbsp;
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" Font-Bold="true">
                <asp:ListItem Selected="True">Cliente</asp:ListItem>
                <asp:ListItem>Fecha</asp:ListItem>
            </asp:RadioButtonList>
            <asp:Button ID="BtnFiltroFecha" Width="180px" Height="35px" ValidationGroup="ValiFecha" runat="server" Text="Filtrar" OnClick="BtnFiltroFecha_Click" CssClass="BusFiltroFactura Boton" />

        </div>
    <p>&nbsp;</p> 
    <asp:GridView ID="GridFactura" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" CssClass="table table-border cell-border" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" OnPageIndexChanging="GridFactura_PageIndexChanging" PageSize="5">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="cod_factura" HeaderText="Nro Factura">
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="fecha" HeaderText="Fecha" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="importetotal" HeaderText="Importe Total" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="clienteid" HeaderText="Cliente" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="empleadoid" HeaderText="Empleado" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="metododepago" HeaderText="Metodo de pago" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="tipofactura" HeaderText="Tipo factura" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Detalle de la Factura" ShowHeader="False">
             <ItemTemplate>
                  <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Recursos/PedidoMini.png" Text="Botón" BorderWidth="2px" CausesValidation="false"  OnClick="ImageButton2_Click" />
             </ItemTemplate>
             <ControlStyle BorderColor="Black" CssClass="BotoDataGrid" />
             <ItemStyle HorizontalAlign="Center" />
           </asp:TemplateField>
            <asp:TemplateField HeaderText="Descargar" ShowHeader="False">
             <ItemTemplate>
                  <asp:ImageButton ID="ImageButtonDes" runat="server" ImageUrl="~/Recursos/descarga.png" Text="Botón" BorderWidth="2px" CausesValidation="false"  OnClick="ImageButtonDes_Click" />
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
            <div class="TituloDetalle">Detalle de la Factura</div>
            <asp:LinkButton Height="30px" style="TEXT-DECORATION: none" Text="X" Font-Bold="true" CssClass="btonX" Width="30px" ID="Oculto" runat="server" />

            <hr class="hrclassPop" />
        </div>
        <asp:GridView ID="GrillaDetalle" AllowPaging="true" AutoGenerateColumns="False" ShowFooter="True" CssClass="table table-border cell-border" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GrillaDetalle_PageIndexChanging" OnRowDataBound="GrillaDetalle_RowDataBound" >
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="articuloid" HeaderText="Articulo" ItemStyle-HorizontalAlign="Center" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="nombre" HeaderText="Nombre/Marca" ItemStyle-HorizontalAlign="Center" >
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
