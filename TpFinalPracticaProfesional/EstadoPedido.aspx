<%@ Page Title="" Language="C#" Culture="Auto" UICulture="Auto" MasterPageFile="~/MasterPedido.Master" AutoEventWireup="true" CodeBehind="EstadoPedido.aspx.cs" Inherits="TpFinalPracticaProfesional.EstadoPedido" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenedorPrincipal" runat="server">

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
   <script src="https://cdn.jsdelivr.net/npm/sweetalert2@7.22.2/dist/sweetalert2.all.min.js"></script>
    <script type="text/javascript">
        function errorcliente() {
            swal({
                title: '',
                text: 'No se encontro el cliente!',
                type: 'error'
            });
        }
        function errorpedido() {
            swal({
                title: '',
                text: 'No se encontro el pedido!',
                type: 'error'
            });
        }
        function GetIDCli(source, eventArgs) {
            var HdnKeyCli = eventArgs.get_value();
            document.getElementById('<%=hdnIDCli.ClientID %>').value = HdnKeyCli;
            $('#<%= BtnCli.ClientID %>').click();
        }
        function errorDepende() {
            swal({
                title: '',
                text: 'No se pudo eliminar. Ha sido desactivado. ',
                type: 'error'
            });
        }
        function errorelim() {
            swal({
                title: '',
                text: 'Pedido eliminado',
                type: 'success'
            });
        }
    </script>
        <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="server"></asp:ScriptManager>
    <asp:Panel ID="PanelBuscar" CssClass="PanelPersonalizado" runat="server">
    <h1>Eliminar Pedido</h1>
     <p>&nbsp;</p>
        <asp:TextBox ID="TxtNro" CssClass="Textos" placeholder="Ingrese nro pedido" runat="server" Width="350px" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="ValiEstado" runat="server" ValidationGroup="ValiNro" ControlToValidate="TxtNro" ErrorMessage="ingrese nro pedido">ingrese nro pedido</asp:RequiredFieldValidator><asp:CustomValidator ID="CustomValidator1" CssClass="ValiEstado" ValidationGroup="ValiNro" OnServerValidate="CustomValidator1_ServerValidate" ControlToValidate="TxtNro" runat="server" ErrorMessage="Error: solo numeros positivos en este campo">Error: solo numeros positivos en este campo</asp:CustomValidator>
        <asp:HiddenField ID="hdnIDCli" runat="server" />
        <asp:TextBox ID="TxtCli" CssClass="Textos" placeholder="Ingrese nombre/apellido " runat="server" Width="350px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="ValiEstado" runat="server" ControlToValidate="TxtCli"  ValidationGroup="ValiCli" ErrorMessage="ingrese nombre/apellido">ingrese nombre/apellido</asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="TxtDesde" CssClass="ValiEstado" runat="server" ValidationGroup="ValiFecha" ErrorMessage="ingrese fecha inicial">ingrese fecha inicial</asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="TxtHasta" CssClass="ValiHastaEstado" runat="server" ValidationGroup="ValiFecha" ErrorMessage="ingrese fecha final">ingrese fecha final</asp:RequiredFieldValidator>
        <ajaxToolkit:AutoCompleteExtender ID="TxtCli_AutoCompleteExtender" runat="server"
            CompletionInterval="10" DelimiterCharacters="" Enabled="True"
            MinimumPrefixLength="1" ServiceMethod="GetClientesPedido" TargetControlID="TxtCli" CompletionListCssClass="CompletionList"
            CompletionListHighlightedItemCssClass="CompletionListHighlightedItem"
            CompletionListItemCssClass="CompletionListItem" OnClientItemSelected="GetIDCli">
        </ajaxToolkit:AutoCompleteExtender>
        
        <div class="inline-form">
        <asp:TextBox ID="TxtDesde" Height="30px" runat="server" TextMode ="Date" CssClass="Textos" placeholder="Desde" Width="175px"></asp:TextBox>
        <asp:TextBox ID="TxtHasta" Height="30px" runat="server" TextMode="Date" CssClass="Textos" placeholder="Hasta" Width="175px"></asp:TextBox>
        </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="TxtDesde"  CssClass="Vali" runat="server" ValidationGroup="ValiFecha" ErrorMessage="ingrese fecha inicial">ingrese fecha inicial</asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="TxtHasta" CssClass="ValiHasta" runat="server" ValidationGroup="ValiFecha" ErrorMessage="ingrese fecha final">ingrese fecha final</asp:RequiredFieldValidator>
      &nbsp;&nbsp;&nbsp;
        <asp:RadioButtonList ID="RadioButtonListBusqueda" CssClass="RadioListEstado" runat="server" RepeatDirection="Horizontal" AutoPostBack="True">
            <asp:ListItem Selected="True" Value="nro">Nro Pedido</asp:ListItem>
            <asp:ListItem Value="cliente">Cliente</asp:ListItem>
            <asp:ListItem Value="rango">Rango de Fecha</asp:ListItem>
        </asp:RadioButtonList>
        <asp:Button ID="BtnNro" runat="server" Height="35px" Text="Buscar" Width="180px" CssClass="BotonBus Boton BotonHover" ValidationGroup="ValiNro" OnClick="BtnNro_Click" />
        <asp:Button ID="BtnCli" runat="server" Height="35px" Text="Buscar" Width="180px" CssClass="BotonBus Boton BotonHover"  ValidationGroup="ValiCli" OnClick="BtnCli_Click" />
        <asp:Button ID="BtnRango" runat="server" Height="35px" Text="Buscar" Width="180px" ValidationGroup="ValiFecha" CssClass="BotonBus Boton BotonHover" OnClick="BtnRango_Click"/>
   <br />
   </asp:Panel>
    <br />
    <asp:Panel ID="PanelPedidoGrilla" CssClass="PanelPersonalizado scrolling-table-container" runat="server">
    <p>&nbsp;</p> 
    <asp:GridView ID="GridPedidos" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CssClass="table table-border cell-border" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" OnPageIndexChanging="GridPedidos_PageIndexChanging" OnRowDataBound="GridPedidos_RowDataBound" PageSize="5" OnRowCancelingEdit="GridPedidos_RowCancelingEdit" OnRowDeleting="GridPedidos_RowDeleting" OnRowEditing="GridPedidos_RowEditing" OnRowUpdating="GridPedidos_RowUpdating">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="nropedido" ReadOnly="true" HeaderText="Nro Pedido" />
            <asp:BoundField DataField="fecha" ReadOnly="true" HeaderText="Fecha" />
            <asp:BoundField DataField="entregado" ReadOnly="true" HeaderText="Entregado" />
            <asp:BoundField DataField="clienteid" ReadOnly="true" HeaderText="Cliente" />
             <asp:TemplateField HeaderText="Estado">
             <EditItemTemplate >
             <asp:DropDownList ID="ddlEstado" CssClass="Textos" SelectedValue='<%# Bind("estado") %>' AppendDataBoundItems="true" runat="server">
                <asp:ListItem Value="activo">Activo</asp:ListItem>
                <asp:ListItem Value="no activo">No Activo</asp:ListItem>
             </asp:DropDownList>
             </EditItemTemplate>
            <ItemTemplate>
                 <asp:Label ID="Label1" runat="server" Text='<%# Bind("estado") %>'></asp:Label>
             </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Detalle del pedido" ShowHeader="False">
             <ItemTemplate>
                  <asp:ImageButton ID="ImageButtonDett" runat="server" ImageUrl="~/Recursos/PedidoMini.png" Text="Botón" BorderWidth="2px" CausesValidation="false"  OnClick="ImageButtonDett_Click" />
             </ItemTemplate>
             <ControlStyle BorderColor="Black" CssClass="BotoDataGrid" />
             <ItemStyle HorizontalAlign="Center" />
           </asp:TemplateField>
            <asp:CommandField ButtonType="Image" HeaderText="Editar Estado" EditImageUrl="~/Recursos/EditarProveedor.png" CausesValidation ="false" ShowEditButton="True" CancelImageUrl="~/Recursos/Cancel-32.png" UpdateImageUrl="~/Recursos/Check-32.png" DeleteText="" >
             <ControlStyle BorderColor="Black" CssClass="BotoDataGrid" BorderWidth="2px" />
             <ItemStyle HorizontalAlign="Center" />
            </asp:CommandField>
            <asp:TemplateField HeaderText="Eliminar" ShowHeader="False">
            <ItemTemplate>
                  <asp:ImageButton ID="ImageButtonElim" runat="server" ImageUrl="~/Recursos/EliminarProveedor.png" Text="Botón" BorderWidth="2px" CausesValidation="false" OnClick="ImageButtonElim_Click"/>
             </ItemTemplate>
                <ControlStyle BorderColor="Black" BorderWidth="2px" />
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
            <%--PanelPersonalizado --%>
            <div class="TituloDetalle">Detalle del Pedido</div>
             <asp:LinkButton Height="30px" style="TEXT-DECORATION: none" Text="X" Font-Bold="true" CssClass="btonX" Width="30px" ID="Oculto" runat="server" />

<%--            <asp:Button ID="Button1" runat="server" CausesValidation="false" Height="30px" CssClass="btonX" Text="X" Width="30px" />--%>
            <hr class="hrclassPop" />
        </div>
        <asp:GridView ID="GrillaDetalle" AutoGenerateColumns="False" ShowFooter="True" CssClass="table table-border cell-border" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"  ShowHeaderWhenEmpty="True" OnRowDataBound="GrillaDetalle_RowDataBound">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="articuloid" HeaderText="Articulo" ItemStyle-HorizontalAlign="Center" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="cantidad" HeaderText="Cantidad" ItemStyle-HorizontalAlign="Center" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="preciovendido" HeaderText="Precio" ItemStyle-HorizontalAlign="Center" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
<%--                <asp:BoundField DataField="totalxArt" HeaderText="" ItemStyle-HorizontalAlign="Center" />--%>
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
            <br />
        <br />
        <asp:LinkButton Text="" ID="LinkOculto" runat="server" /> 
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" CancelControlID="BtnCancelarEliminar" TargetControlID="LinkOculto" PopupControlID="MensajeConfirmacion" runat="server"></ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="MensajeConfirmacion" runat="server" style="display:none" CssClass="PanelEliminarPop">
            <div class="swal-icon swal-icon--warning">
                <span class="swal-icon--warning__body">
                    <span class="swal-icon--warning__dot"> </span>
                </span>
            </div>
            <asp:Button ID="BtnAceptarEliminar" runat="server" Text="Aceptar" CausesValidation="false" CssClass="BtnAcepPopElim" OnClick="BtnAceptarEliminar_Click" />
            <asp:Button ID="BtnCancelarEliminar" runat="server" Text="Cancelar" CssClass="BtnElimPopElim"/>
            <asp:Label ID="Mensaje" runat="server" CssClass="textMensaje" Text="¿Estás seguro?" Font-Bold="True"></asp:Label>
            <asp:Label ID="Label3" runat="server" CssClass="textMensaje1" Text="Se eliminará de manera permanente"></asp:Label>
        </asp:Panel>
</asp:Content>
