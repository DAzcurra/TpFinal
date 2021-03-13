<%@ Page Title="" Language="C#" MasterPageFile="~/MasterFacturacion.Master" AutoEventWireup="true" CodeBehind="AgregarFactura.aspx.cs" Inherits="TpFinalPracticaProfesional.AgregarFactura" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenedorPrincipal" runat="server">
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
   <script src="https://cdn.jsdelivr.net/npm/sweetalert2@7.22.2/dist/sweetalert2.all.min.js"></script>
    <script type="text/javascript">
        function errorpedido() {
            swal({
                title: '',
                text: 'No se encontro el pedido!',
                type: 'error'
            });
        }
        function PedidoModificado() {
            swal({
                title: '',
                text: 'Pedido modificado !',
                type: 'success'
            });
        }
        function AgregarFactura() {
            swal({
                title: '',
                text: 'El factura se agregado correctamente !',
                type: 'success'
            });
        }
        function errorcliente() {
            swal({
                title: '',
                text: 'No se encontro el cliente!',
                type: 'error'
            });
        }
        function GetIDCli(source, eventArgs) {
            var HdnKeyCli = eventArgs.get_value();
            document.getElementById('<%=hdnIDCli.ClientID %>').value = HdnKeyCli;
            $('#<%= BtnCli.ClientID %>').click();
       }
    </script>
    <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="server"></asp:ScriptManager>
    <asp:Panel ID="PanelBuscar" CssClass="PanelPersonalizado" runat="server">
    <h1>Agregar Factura</h1>
     <p>&nbsp;</p>
        <asp:TextBox ID="TxtNro" CssClass="Textos" placeholder="Ingrese nro pedido" runat="server" Width="350px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="ValiAgregarFactura" runat="server" ValidationGroup="ValiNro" ControlToValidate="TxtNro" ErrorMessage="ingrese nro pedido">ingrese nro pedido</asp:RequiredFieldValidator><asp:CustomValidator ID="CustomValidator1" CssClass="ValiAgregarFactura" ValidationGroup="ValiNro" OnServerValidate="CustomValidator1_ServerValidate" ControlToValidate="TxtNro" runat="server" ErrorMessage="Error: solo numeros positivos en este campo">Error: solo numeros positivos en este campo</asp:CustomValidator>
        <asp:HiddenField ID="hdnIDCli" runat="server" />
        <asp:TextBox ID="TxtCli" CssClass="Textos" placeholder="Ingrese nombre/apellido " runat="server" Width="350px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="ValiAgregarFactura" runat="server" ControlToValidate="TxtCli"  ValidationGroup="ValiCli" ErrorMessage="ingrese nombre/apellido">ingrese nombre/apellido</asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="TxtDesde" CssClass="ValiAgregarFactura" runat="server" ValidationGroup="ValiFecha" ErrorMessage="ingrese fecha inicial">ingrese fecha inicial</asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="TxtHasta" CssClass="ValiHasta" runat="server" ValidationGroup="ValiFecha" ErrorMessage="ingrese fecha final">ingrese fecha final</asp:RequiredFieldValidator>
        <ajaxToolkit:AutoCompleteExtender ID="TxtCli_AutoCompleteExtender" runat="server"
            CompletionInterval="10" DelimiterCharacters="" Enabled="True"
            MinimumPrefixLength="1" ServiceMethod="GetClientesPedido" TargetControlID="TxtCli" CompletionListCssClass="CompletionList"
            CompletionListHighlightedItemCssClass="CompletionListHighlightedItem"
            CompletionListItemCssClass="CompletionListItem" OnClientItemSelected="GetIDCli">
        </ajaxToolkit:AutoCompleteExtender>
        
        <div class="inline-form">
        <asp:TextBox ID="TxtDesde" runat="server" CssClass="Textos" placeholder="Desde" Width="175px"></asp:TextBox>
        <asp:TextBox ID="TxtHasta" runat="server" CssClass="Textos" placeholder="Hasta" Width="175px"></asp:TextBox>
        </div>
        <ajaxToolkit:CalendarExtender ID="CDesde" runat="server" PopupButtonID="" TargetControlID="TxtDesde" Format="yyyy/MM/dd"/>
        <ajaxToolkit:CalendarExtender ID="CHasta" runat="server" PopupButtonID="" TargetControlID="TxtHasta" Format="yyyy/MM/dd"/>

      &nbsp;&nbsp;&nbsp;
        <asp:RadioButtonList ID="RadioButtonListBusqueda" CssClass="RadioList" runat="server" RepeatDirection="Horizontal" AutoPostBack="True">
            <asp:ListItem Value="nro">Nro Pedido</asp:ListItem>
            <asp:ListItem Value="cliente" Selected="True">Cliente</asp:ListItem>
            <asp:ListItem Value="rango">Rango de Fecha</asp:ListItem>
        </asp:RadioButtonList>
        <asp:Button ID="BtnNro" runat="server" Height="35px" Text="Buscar Pedido" Width="180px" CssClass="BotonBus Boton " ValidationGroup="ValiNro" OnClick="BtnNro_Click" />
        <asp:Button ID="BtnCli" runat="server" Height="35px" Text="Buscar Pedido" Width="180px" CssClass="BotonBus Boton "  ValidationGroup="ValiCli" OnClick="BtnCli_Click" />
        <asp:Button ID="BtnRango" runat="server" Height="35px" Text="Buscar Pedido" Width="180px" ValidationGroup="ValiFecha" CssClass="BotonBus Boton " OnClick="BtnRango_Click" />
   <br />
   </asp:Panel>
    <br />
    <asp:LinkButton Text="" ID="btnPopup2" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderUno" CancelControlID="Oculto" TargetControlID="btnPopup2" PopupControlID="PanelPopUpCli" runat="server"></ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelPopUpCli" CssClass="PanelPop2 scrolling-table-container2" runat="server" Height="500px" Style="display: none">
        <div class="BordePop">
           <div class="Titulo">Elegir Pedido</div>
                <asp:LinkButton Height="30px" style="TEXT-DECORATION: none" Text="X" Font-Bold="true" CssClass="btonX" Width="30px" ID="Oculto" runat="server" />

               <hr class="hrclassPop" />
        </div>
            <asp:GridView ID="BusquedaClientePedido" AllowPaging="True" runat="server" AutoGenerateColumns="False" CssClass="table table-border cell-border" CellPadding="4" ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True" OnPageIndexChanging="BusquedaClientePedido_PageIndexChanging">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="clienteid" HeaderText="Cliente" />
                <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                <asp:BoundField DataField="nropedido" HeaderText="Nro Pedido" />
                <asp:TemplateField HeaderText="" ShowHeader="False">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButtonP" runat="server" ImageUrl="~/Recursos/Check.png" Text="Botón" BorderWidth="2px" CausesValidation="false" OnClick="ImageButtonP_Click" />
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
        &nbsp;&nbsp;
    </asp:Panel>

    <asp:Panel ID="PanelFactura" CssClass="PanelPersonalizado" runat="server">
        <br />
        <div class="inline-form">
           <div style="margin-left:11%"><asp:Label ID="lblTipoFactura" CssClass=" Labels" runat="server" Text="Tipo de Factura: " Font-Size="Small" Font-Bold="true"></asp:Label></div>
           <asp:TextBox ID="TxtTipoFactura" CssClass="Textos" runat="server" Enabled="false" Width="5%"></asp:TextBox>
           <div style="margin-left:28.7%"><asp:Label ID="LblnroFactura" runat="server" Font-Bold="true" CssClass="LabelsSdaFila" Text="N° Factura:" Font-Size="Small"></asp:Label></div>
           <asp:TextBox ID="TxtnroFactura" CssClass="Textos" runat="server" Enabled="False" Width="20%"></asp:TextBox>
        </div>
           <br />
       <div class="inline-form">
            <div style="margin-left:14%"><asp:Label ID="Label1" runat="server" CssClass="Labels" Text="N° Pedido:" Font-Size="Small"></asp:Label></div>
            <asp:TextBox ID="txtnropedidomostrar" CssClass="Textos" runat="server" Enabled="False" Width="20%"></asp:TextBox>
            <div style="margin-left:14.2%"><asp:Label ID="Label5" runat="server" CssClass="LabelsSdaFila" Text="Vendedor: " Font-Bold="true" Font-Size="Small"></asp:Label></div>
            <asp:DropDownList ID="ddlEmpleado" runat="server" CssClass="LabelsSdaFila" Width="25%" ></asp:DropDownList>
       </div>
        <br />
            <div class="inline-form">
           <div style="margin-left:10%"><asp:Label ID="Label2" runat="server" Font-Bold="true" CssClass="Labels" Text="Apellido, Nombre:" Font-Size="Small"></asp:Label></div> 
           <asp:TextBox ID="TxtNombre" CssClass="Textos" runat="server" Enabled="False" Width="25%"></asp:TextBox>
            <div style="margin-left:5%"><asp:Label ID="Label4" runat="server" Text="Métodos de pago: " CssClass="LabelsSdaFila" Font-Size="Small" Font-Bold="true"></asp:Label></div>
            <asp:DropDownList ID="DdlMetododePago" runat="server" Width="25%">
                <asp:ListItem Selected="True" Value="efectivo/contado">Efectivo/Contado</asp:ListItem>
                <asp:ListItem Value="tarjeta de credito">Tarjeta de credito</asp:ListItem>
                <asp:ListItem Value="cheque">Cheque</asp:ListItem>
            </asp:DropDownList></div>
        <br />
        <div class="inline-form">
           <div style="margin-left:16.2%"><asp:Label ID="lblfecha" runat="server" CssClass="Labels" Font-Bold="true" Text="Fecha:" Font-Size="Small"></asp:Label></div>
           <asp:TextBox ID="txtfechamostrar" CssClass="Textos" runat="server" Enabled="False" Width="25%"></asp:TextBox>
        </div>
            <br />
           <asp:GridView ID="GrillaArticulos" runat="server"  AutoGenerateColumns="False" CellPadding="4" CssClass="table table-border cell-border" ForeColor="#333333" GridLines="None" ShowFooter="True" ShowHeaderWhenEmpty="True" OnRowDataBound="GrillaArticulos_RowDataBound"  >
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField ReadOnly="true" DataField="articuloid" HeaderText="Código de Articulo" />
                <asp:BoundField ReadOnly="true" DataField="nombre" HeaderText="Nombre" />
                <asp:BoundField ReadOnly="true" DataField="descripcion" HeaderText="Descripcion" />
                <asp:BoundField ReadOnly="true" DataField="marca" HeaderText="Marca" />
                <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                <asp:BoundField ReadOnly="true" DataField="precioactual" HeaderText="Precio" />
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
            <br />
            <center>
            <asp:Button ID="BtnGuardar" runat="server" Width="180px" Height="50px" CssClass="Boton BotonHover" Text="Guardar" OnClick="BtnGuardar_Click" />
         </center>
       </asp:Panel>
</asp:Content>
