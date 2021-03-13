<%@ Page Title="" Language="C#" Culture="Auto" UICulture="Auto" MasterPageFile="~/MasterPedido.Master" AutoEventWireup="true" CodeBehind="EditarPedido.aspx.cs" Inherits="TpFinalPracticaProfesional.EditarPedido" %>
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

       function erroralertart() {
            swal({
                title: '',
                text: 'No se encontro el articulo',
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

        function errorcliente() {
            swal({
                title: '',
                text: 'No se encontro el cliente!',
                type: 'error'
            });
        }
        function errorStockInsu() {
            swal({
                title: '',
                text: 'Stock insuficiente',
                type: 'error'
            });
        }
        function errorvacio() {
            swal({
                title: '',
                text: 'Campo vacio !',
                type: 'error'
            });
        }
        function errornega() {
            swal({
                title: '',
                text: 'Valor negativo !',
                type: 'error'
            });
        }

        function errornvalorinco() {
            swal({
                title: '',
                text: 'Valor incompatible! , Solo numeros positivos',
                type: 'error'
            });
        }
        function errorpedElim() {
            swal({
                title: '',
                text: 'El pedido se encontraba vacio, fue eliminado',
                type: 'error'
            });
        }
        function GetIDCli(source, eventArgs) {
            var HdnKeyCli = eventArgs.get_value();
            document.getElementById('<%=hdnIDCli.ClientID %>').value = HdnKeyCli;
            $('#<%= BtnCli.ClientID %>').click();
        }
       function GetIDART(source, eventArgs) {
            var HdnKeyArt = eventArgs.get_value();
            document.getElementById('<%=HdfArt.ClientID %>').value = HdnKeyArt;
            $('#<%= BtnBuscarNombre.ClientID %>').click();
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="server"></asp:ScriptManager>
    <asp:Panel ID="PanelBuscar" CssClass="PanelPersonalizado" runat="server">
    <h1>Editar Pedido</h1>
     <p>&nbsp;</p>
        <asp:TextBox ID="TxtNro" CssClass="Textos" TextMode="Number" placeholder="Ingrese nro pedido" runat="server" Width="350px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="Vali" runat="server" ValidationGroup="ValiNro" ControlToValidate="TxtNro" ErrorMessage="ingrese nro pedido">ingrese nro pedido</asp:RequiredFieldValidator><asp:CustomValidator ID="CustomValidator1" CssClass="Vali" ValidationGroup="ValiNro" OnServerValidate="CustomValidator1_ServerValidate" ControlToValidate="TxtNro" runat="server" ErrorMessage="Error: solo numeros positivos en este campo">Error: solo numeros positivos en este campo</asp:CustomValidator>
        <asp:HiddenField ID="hdnIDCli" runat="server" />
        <asp:TextBox ID="TxtCli" CssClass="Textos" placeholder="Ingrese nombre/apellido " runat="server" Width="350px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="Vali" runat="server" ControlToValidate="TxtCli"  ValidationGroup="ValiCli" ErrorMessage="ingrese nombre/apellido">ingrese nombre/apellido</asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="TxtDesde"  CssClass="Vali" runat="server" ValidationGroup="ValiFecha" ErrorMessage="ingrese fecha inicial">ingrese fecha inicial</asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="TxtHasta" CssClass="ValiHasta" runat="server" ValidationGroup="ValiFecha" ErrorMessage="ingrese fecha final">ingrese fecha final</asp:RequiredFieldValidator>
        <ajaxToolkit:AutoCompleteExtender ID="TxtCli_AutoCompleteExtender" runat="server"
            CompletionInterval="10" DelimiterCharacters="" Enabled="True"
            MinimumPrefixLength="1" ServiceMethod="GetClientesPedido" TargetControlID="TxtCli" CompletionListCssClass="CompletionList"
            CompletionListHighlightedItemCssClass="CompletionListHighlightedItem"
            CompletionListItemCssClass="CompletionListItem" OnClientItemSelected="GetIDCli">
        </ajaxToolkit:AutoCompleteExtender>
        
        <div class="inline-form">
        <asp:TextBox ID="TxtDesde" Height="30px" runat="server" CssClass="Textos" placeholder="Desde" Width="175px" TextMode="Date"></asp:TextBox>
        <asp:TextBox ID="TxtHasta" Height="30px" runat="server" CssClass="Textos" placeholder="Hasta" Width="175px" TextMode="Date"></asp:TextBox>
        </div>

      &nbsp;&nbsp;&nbsp;
        <asp:RadioButtonList ID="RadioButtonListBusqueda" CssClass="RadioList" runat="server" RepeatDirection="Horizontal" AutoPostBack="True">
            <asp:ListItem Value="nro">Nro Pedido</asp:ListItem>
            <asp:ListItem Value="cliente" Selected="True">Cliente</asp:ListItem>
            <asp:ListItem Value="rango">Rango de Fecha</asp:ListItem>
        </asp:RadioButtonList>
        <asp:Button ID="BtnNro" runat="server" Height="35px" Text="Buscar" Width="180px" CssClass="BotonBus Boton " ValidationGroup="ValiNro" OnClick="BtnNro_Click" />
        <asp:Button ID="BtnCli" runat="server" Height="35px" Text="Buscar" Width="180px" CssClass="BotonBus Boton "  ValidationGroup="ValiCli" OnClick="BtnCli_Click" />
        <asp:Button ID="BtnRango" runat="server" Height="35px" Text="Buscar" Width="180px" ValidationGroup="ValiFecha" CssClass="BotonBus Boton " OnClick="BtnRango_Click" />
   <br />
   </asp:Panel>
    <br />
        <asp:Panel ID="Panel1" CssClass="PanelPersonalizado" runat="server">
            <br />
       <div class="inline-form">
           <div style="margin-left: 4.2%"><asp:Label ID="Label1" runat="server" CssClass="Labels" Text="N° Pedido:" Font-Size="Small"></asp:Label></div>
            <asp:TextBox ID="txtnropedidomostrar" CssClass="Textos" runat="server" Enabled="False" Width="20%"></asp:TextBox>
       </div><br />
            <div class="inline-form">
           <asp:Label ID="Label2" runat="server" CssClass="Labels" Text="Apellido, Nombre:" Font-Size="Small"></asp:Label>
           <asp:TextBox ID="TxtNombre" CssClass="Textos" runat="server" Enabled="False" Width="25%"></asp:TextBox>
           <asp:Label ID="Label3" runat="server" CssClass="Labels" Text="Fecha:" Font-Size="Small"></asp:Label>
            <asp:TextBox ID="txtfechamostrar" CssClass="Textos" runat="server" Enabled="False" Width="25%"></asp:TextBox>
       </div>
            <br />
        <hr class="hrclass" />
        <asp:HiddenField ID="HdfArt" runat="server" />
        <asp:Label ID="LbBuscar" CssClass="LabelsT" runat="server" Font-Size="Medium" Text="Buscar Articulo: "></asp:Label>
        <asp:TextBox ID="TxtBuscarNombre" runat="server" placeholder="Ingrese Nombre/Marca" CssClass="Textos" Width="350px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TxtBuscarNombre" CssClass="ValiAgregarBusquedaArticuloEdit" ErrorMessage="Campo vacio: ingrese nombre/marca" ValidationGroup="ValiNom"></asp:RequiredFieldValidator>
        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
            CompletionInterval="10" DelimiterCharacters="" Enabled="True"
            MinimumPrefixLength="1" ServiceMethod="GetNomMar" TargetControlID="TxtBuscarNombre" CompletionListCssClass="CompletionList"
            CompletionListHighlightedItemCssClass="CompletionListHighlightedItem"
            CompletionListItemCssClass="CompletionListItem" OnClientItemSelected="GetIDART">
        </ajaxToolkit:AutoCompleteExtender>
            
       <asp:Button ID="BtnBuscarNombre" runat="server" BorderColor="Black" CssClass="Boton BusArtBAgrePedidoEdit" Width="180px" Height="40px" Text="Buscar" ValidationGroup="ValiNom" OnClick="BtnBuscarNombre_Click" Enabled="False" />
  
            <br />
           <asp:GridView ID="GrillaArticulos" runat="server"  AutoGenerateColumns="False" CellPadding="4" CssClass="table table-border cell-border" ForeColor="#333333" GridLines="None" OnRowDataBound="GrillaArticulos_RowDataBound" ShowFooter="True" ShowHeaderWhenEmpty="True" OnRowDeleting="GrillaArticulos_RowDeleting" OnRowEditing="GrillaArticulos_RowEditing" OnRowUpdating="GrillaArticulos_RowUpdating" OnRowCancelingEdit="GrillaArticulos_RowCancelingEdit" OnSelectedIndexChanged="GrillaArticulos_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField ReadOnly="true" DataField="articuloid" HeaderText="Código de Articulo" />
                <asp:BoundField ReadOnly="true" DataField="nombre" HeaderText="Nombre" />
                <asp:BoundField ReadOnly="true" DataField="descripcion" HeaderText="Descripcion" />
                <asp:BoundField ReadOnly="true" DataField="marca" HeaderText="Marca" />
                <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                <asp:BoundField ReadOnly="true" DataField="precioactual" HeaderText="Precio" />
                <asp:CommandField ButtonType="Image" EditImageUrl="~/Recursos/EditarProveedor.png" ShowEditButton="True" CancelImageUrl="~/Recursos/Cancel-32.png" UpdateImageUrl="~/Recursos/Check-32.png" />
                <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Recursos/EliminarProveedor.png" ShowDeleteButton="True" />
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
            <asp:Button ID="BtnGuardar" runat="server" Width="180px" Height="50px" CssClass="Boton BotonHover" Text="Guardar" OnClick="BtnGuardar_Click" Enabled="False" />
         </center>
       </asp:Panel>

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
    <asp:LinkButton Text="" ID="LinkButton2" runat="server" />
    <ajaxToolkit:ModalPopupExtender CancelControlID="Oculto" ID="ModalPopupExtender2" TargetControlID="LinkButton1" PopupControlID="PanelPedidoPop" runat="server"></ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelPedidoPop" Style="display: none" CssClass="PanelPop scrolling-table-container" runat="server" BorderColor="Black" BorderWidth="0px">
        <div class="BordePop">
            <%--PanelPersonalizado --%>
            <div class="Titulo">Elegir articulo</div>
            <asp:LinkButton Height="30px" style="TEXT-DECORATION: none" Text="X" Font-Bold="true" CssClass="btonX" Width="30px" ID="LinkButton1" runat="server" />
            <hr class="hrclassPop" />
        </div>
        <asp:GridView ID="GrillaBuscarArt" AutoGenerateColumns="False" CssClass="table table-border cell-border" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="articuloid" HeaderText="Código de Articulo" ItemStyle-HorizontalAlign="Center" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="nombre" HeaderText="Nombre" ItemStyle-HorizontalAlign="Center" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="descripcion" HeaderText="Descripcion" ItemStyle-HorizontalAlign="Center" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="marca" HeaderText="Marca" ItemStyle-HorizontalAlign="Center" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="precioactual" HeaderText="Precio" ItemStyle-HorizontalAlign="Center" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Recursos/Check.png" Text="Botón" BorderWidth="2px" CausesValidation="false" OnClick="ImageButton3_Click" />
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
        <p>&nbsp;</p>
    </asp:Panel>
    <asp:LinkButton Text="" ID="LkBCantidad" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" CancelControlID="" TargetControlID="LkBCantidad" PopupControlID="PanelCantidad" runat="server"></ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelCantidad" Style="display: none" Height="200px" Width="300px" BorderColor="Black" BorderWidth="2px" BorderStyle="Double" BackColor="#FAFAE7" runat="server" HorizontalAlign="Center">
        <div style="position: absolute; top: 25px; left: 11px;">
            <asp:Label ID="Label9" CssClass="Labels" runat="server" Text="Cantidad: "></asp:Label>
        </div>
        <div style="position: absolute; top: 66px; left: 65px; text-align: center;">
            <asp:TextBox ID="TextCant" Text="1" CssClass="Textos" runat="server" Width="150px" TextMode="Number"></asp:TextBox>
            <div style="position: absolute">
                <asp:Label ID="LblError" runat="server" CssClass="ValiAgregarGridCant" Text="No hay stock suficiente" Visible="false"></asp:Label><asp:RangeValidator ID="RangeValidator1" CssClass="ValiAgregarGridCant" runat="server" Display="Static" ControlToValidate="TextCant" ErrorMessage=" Error: solo numeros positivos en este campo" MinimumValue="0" MaximumValue="9999999999999">Error: solo numeros positivos en este campo</asp:RangeValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass=" ValiAgregarGridCant" ControlToValidate="TextCant" runat="server" ErrorMessage="Campo vacio: ingrese cantidad">Campo vacio: ingrese cantidad</asp:RequiredFieldValidator>
            </div>
        </div>
        <asp:Button ID="Button2" CssClass="Boton CantBAgrePedido" runat="server" Width="125px" Text="Aceptar" OnClick="Button2_Click" />
        <asp:Button ID="Button3" runat="server" CssClass="Boton CantCancelarBAgrePedido" CausesValidation="false" OnClick="Button3_Click" Text="Cancelar" Width="125px" />
    </asp:Panel>
</asp:Content>
