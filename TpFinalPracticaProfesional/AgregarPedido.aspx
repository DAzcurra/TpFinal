<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPedido.Master" AutoEventWireup="true" CodeBehind="AgregarPedido.aspx.cs" Inherits="TpFinalPracticaProfesional.AgregarPedido" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

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
                text: 'El pedido se agregado correctamente',
                type: 'success'
            })
        }
        function erroralertcli() {
            swal({
                title: '',
                text: 'No se encontro clientes con el apellido ingresado',
                type: 'error'
            });
        }
        function erroralertFaltaCampos() {
            swal({
                title: '',
                text: 'ingrese cliente y/o articulo',
                type: 'error'
            });
        }
        function erroralertcliRS() {
            swal({
                title: '',
                text: 'No se cliente con el cuil ingresado ',
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
        function erroralertartCampoV() {
            swal({
                title: '',
                text: 'el campo cantidad esta vacio',
                type: 'error'
            });
        }
        function erroralertartNumP() {
            swal({
                title: '',
                text: 'Solo numeros positivos en este campo',
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
        function ArtIngresado()
        {
            swal({
                title: '',
                text: 'Articulo ya ingresado al pedido.',
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
        function redireccionar() {
           location.href="AgregarPedido.aspx";
        }
        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnID.ClientID %>').value = HdnKey;
            $('#<%= BtnBusCli.ClientID %>').click();
        }
        function GetIDART(source, eventArgs) {
            var HdnKeyArt = eventArgs.get_value();
            document.getElementById('<%=HdfArt.ClientID %>').value = HdnKeyArt;
            $('#<%= BtnBuscarNombre.ClientID %>').click();
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>

    <asp:Panel ID="PanelBCliente" CssClass="PanelPersonalizado" BorderWidth="3" runat="server">
        <h1>Agregar Pedido</h1>
        <p>&nbsp;</p>
        <asp:HiddenField ID="hdnID" runat="server" />
        <asp:TextBox ID="TxtBuscarApellido" CssClass="Textos" placeholder="Ingrese Apellido" runat="server" Width="350px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtBuscarApellido" CssClass="ValiAgregarBusqueda" ErrorMessage="Campo vacio: ingrese apellido" ValidationGroup="Valiap"></asp:RequiredFieldValidator>
        <ajaxToolkit:AutoCompleteExtender ID="TextBox1_AutoCompleteExtender" runat="server"
            CompletionInterval="10" DelimiterCharacters="" Enabled="True"
            MinimumPrefixLength="1" ServiceMethod="GetClientes" TargetControlID="TxtBuscarApellido" CompletionListCssClass="CompletionList"
            CompletionListHighlightedItemCssClass="CompletionListHighlightedItem"
            CompletionListItemCssClass="CompletionListItem" OnClientItemSelected="GetID">
        </ajaxToolkit:AutoCompleteExtender>

        <asp:TextBox ID="TxtBuscarRS" CssClass="Textos" placeholder="Ingrese Cuil" runat="server" Width="350px"></asp:TextBox>
<%--        <div style="position: absolute">--%>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtBuscarRS" CssClass="ValiAgregarBusqueda" ErrorMessage="Campo vacio: ingrese cuil" ValidationGroup="ValiRS"></asp:RequiredFieldValidator><asp:CustomValidator ID="CustomValidator1" ControlToValidate="TxtBuscarRS" CssClass="ValiAgregarBusqueda" ValidationGroup="ValiRS" runat="server" ErrorMessage="Error: solo numeros positivos en este campo" OnServerValidate="CustomValidator1_ServerValidate">Error: solo numeros positivos en este campo</asp:CustomValidator>
<%--        </div>--%>
        <asp:RadioButtonList ID="RadioButtonList1" AutoPostBack="true" CssClass="RadioListAgrePedido" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Selected="True" Value="apellido">Apellido</asp:ListItem>
            <asp:ListItem Value="cuil">Cuil </asp:ListItem>
        </asp:RadioButtonList>
        <asp:Button ID="BtnBusCli" runat="server" ValidationGroup="Valiap" CssClass="Boton BusCliAgrePedido" Width="180px" Height="40px" Text="Buscar Cliente" OnClick="BtnBusCli_Click" />
        <asp:Button ID="BtnBusCli1" runat="server" ValidationGroup="ValiRS" CssClass="Boton BusCliAgrePedido" Width="180px" Height="40px" Text="Buscar Cliente" OnClick="BtnBusCli1_Click" />
        &nbsp;
        <hr class="hrclass" />
        <asp:Label ID="Label1" runat="server" CssClass="LabelsT" Text="Datos del cliente:" Font-Size="Medium"></asp:Label><br />
        <div class="inline-form">
            <div style="margin-left: 12.6%"><asp:Label ID="Label8" runat="server" Text="N°"></asp:Label></div>
            <asp:TextBox ID="TxtNroPedido" runat="server" Enabled="false" Width="20%" OnTextChanged="TxtNroPedido_TextChanged"></asp:TextBox>
        </div>
        <div class="inline-form">
            &nbsp; 
            <asp:Label ID="Label2" runat="server" CssClass="Labels" Text="Nombre:" Font-Size="Small"></asp:Label>
            <asp:TextBox ID="TxtNombre" CssClass="Textos" runat="server" Enabled="False" Width="30%"></asp:TextBox>
            <div style="margin-left: 2.5%"><asp:Label ID="Label7" runat="server" CssClass="Labels" Text="Apellido:" Font-Size="Small"></asp:Label></div>
            <asp:TextBox ID="TxtApellido" CssClass="Textos" runat="server" Enabled="False" Width="30%"></asp:TextBox>
        </div>
        <div class="inline-form">
            <asp:Label ID="Label3" runat="server" CssClass="Labels" Text="Telefono:" Font-Size="Small"></asp:Label>
            <asp:TextBox ID="TxtTelefono" CssClass="Textos" runat="server" Enabled="False" Width="30%"></asp:TextBox>
            <div style="margin-left: 1.8%"><asp:Label ID="Label4" runat="server" CssClass="Labels" Text="Dirección:" Font-Size="Small"></asp:Label></div>
            <asp:TextBox ID="TxtDireccion" CssClass="Textos" runat="server" Enabled="False" Width="30%"></asp:TextBox>
        </div>
        <div class="inline-form">
            <div style="margin-left: 1.2%"><asp:Label ID="Label5" runat="server" CssClass="Labels" Text="E-mail:" Font-Size="Small"></asp:Label></div>
            <asp:TextBox ID="Txtemail" CssClass="Textos" runat="server" Enabled="False" Width="30%"></asp:TextBox>
            <asp:Label ID="Label6" runat="server" CssClass="Labels" Text="Razón Social:" Font-Size="Small"></asp:Label>
            <asp:TextBox ID="TxtRazonS" CssClass="Textos" runat="server" Enabled="False" Width="30%"></asp:TextBox>
        </div>
        <div class="inline-form">
            &nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp&nbsp;<asp:Label ID="LbCuil" CssClass="Labels" runat="server" Text="Cuil" Font-Size="Small"></asp:Label>
            <asp:TextBox ID="TxtCuil" runat="server" CssClass="Textos" Width="30%" ControlToValidate="TxtCuil" Enabled="false" TextMode="Number"></asp:TextBox>
        </div>
        &nbsp;
    </asp:Panel>
    <br />
    <asp:Panel ID="PanelBArticulo" CssClass="PanelPersonalizado" BorderWidth="3" runat="server">
        <br />
        <asp:HiddenField ID="HdfArt" runat="server" />
        <asp:Label ID="LbBuscar" CssClass="LabelsT" runat="server" Font-Size="Medium" Text="Buscar Articulo: "></asp:Label>
        <asp:TextBox ID="TxtBuscarNombre" runat="server" placeholder="Ingrese Nombre/Marca" CssClass="Textos" Width="350px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtBuscarNombre" CssClass="ValiAgregarBusquedaArticulo" ErrorMessage="Campo vacio: ingrese nombre/marca" ValidationGroup="ValiNom"></asp:RequiredFieldValidator>
        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
            CompletionInterval="10" DelimiterCharacters="" Enabled="True"
            MinimumPrefixLength="1" ServiceMethod="GetNomMar" TargetControlID="TxtBuscarNombre" CompletionListCssClass="CompletionList"
            CompletionListHighlightedItemCssClass="CompletionListHighlightedItem"
            CompletionListItemCssClass="CompletionListItem" OnClientItemSelected="GetIDART">
        </ajaxToolkit:AutoCompleteExtender>
        <asp:TextBox ID="TxtBuscarCod" runat="server" CssClass="Textos" placeholder="Ingrese Código de Articulo" TextMode="Number" Width="350px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="TxtBuscarCod" CssClass="ValiAgregarBusquedaArticulo" ErrorMessage="Campo vacio: ingrese código de articulo" ValidationGroup="ValiCod"></asp:RequiredFieldValidator>
            <asp:RangeValidator ID="RangeValidator6" runat="server" ControlToValidate="TxtBuscarCod" CssClass="ValiAgregarBusquedaArticulo" ErrorMessage="Error: solo numeros positivos en este campo" MaximumValue="99999999" MinimumValue="0" ValidationGroup="ValiCod">Error: solo numeros positivos en este campo</asp:RangeValidator>
        <asp:RadioButtonList ID="RadioButtonBuscarART" AutoPostBack="true" runat="server" CssClass="RadioLBAgrePedido" RepeatDirection="Horizontal">
            <asp:ListItem Selected="True" Value="nombre">Nombre/Marca</asp:ListItem>
            <asp:ListItem Value="cod">Código de Articulo</asp:ListItem>
        </asp:RadioButtonList>
        <asp:Button ID="BtnBuscarNombre" runat="server" BorderColor="Black" CssClass="Boton BusArtBAgrePedido" Width="180px" Height="40px" Text="Buscar" ValidationGroup="ValiNom" OnClick="BtnBuscarNombre_Click" />
        <asp:Button ID="BtnBuscarCod" runat="server" BorderColor="Black" CssClass="Boton BusArtBAgrePedido" Width="180px" Height="40px" Text="Buscar" ValidationGroup="ValiCod" OnClick="BtnBuscarCod_Click" />
        &nbsp;
        <hr class="hrclass" />
        &nbsp;
        <div class="scrolling-table-container">
        <br />
        <asp:GridView ID="GrillaArticulos" runat="server" AutoGenerateColumns="False" CellPadding="4" CssClass="table table-border cell-border" ForeColor="#333333" GridLines="None" OnRowDataBound="GrillaArticulos_RowDataBound" ShowFooter="True" ShowHeaderWhenEmpty="True" OnRowDeleting="GrillaArticulos_RowDeleting" OnRowEditing="GrillaArticulos_RowEditing" OnRowUpdating="GrillaArticulos_RowUpdating" OnRowCancelingEdit="GrillaArticulos_RowCancelingEdit">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField ReadOnly="true" DataField="articuloid" HeaderText="Código de Articulo" />
                <asp:BoundField ReadOnly="true" DataField="nombre" HeaderText="Nombre" />
                <asp:BoundField ReadOnly="true" DataField="descripcion" HeaderText="Descripcion" />
                <asp:BoundField ReadOnly="true" DataField="marca" HeaderText="Marca" />
                <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                <asp:BoundField ReadOnly="true" DataField="precioactual" HeaderText="Precio" />
                <asp:BoundField ReadOnly="true" DataField="totalxArt" HeaderText="" />
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
        <div style="left:850px">
            <asp:Button ID="Button4" CssClass="Boton " Height="50px" Width="200px" runat="server" Text="Guardar" OnClick="Button4_Click" />

        </div>
        &nbsp;    
        </div>
    </asp:Panel>
    &nbsp;
    <asp:LinkButton Text="" ID="LinkButton1" runat="server" />
    <ajaxToolkit:ModalPopupExtender CancelControlID="Oculto" ID="ModalPopupExtender2" TargetControlID="LinkButton1" PopupControlID="PanelPedidoPop" runat="server"></ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelPedidoPop" Style="display: none" CssClass="PanelPop scrolling-table-container" runat="server" BorderColor="Black" BorderWidth="0px">
        <div class="BordePop">
            <%--PanelPersonalizado --%>
            <div class="Titulo">Elegir articulo</div>
            <asp:LinkButton Height="30px" style="TEXT-DECORATION: none" Text="X" Font-Bold="true" CssClass="btonX" Width="30px" ID="Oculto" runat="server" />
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
    <p>&nbsp;</p>
    <asp:LinkButton Text="" ID="btnPopup" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" CancelControlID="LinkButton2" TargetControlID="btnPopup" PopupControlID="PanelPopUp" runat="server"></ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelPopUp" runat="server" CssClass="PanelPop scrolling-table-container" Style="display: none">
        <div class="BordePop">
            <div class="Titulo">Elegir cliente</div>
            <asp:LinkButton Height="30px" style="TEXT-DECORATION: none" Text="X" Font-Bold="true" CssClass="btonX" Width="30px" ID="LinkButton2" runat="server" />
            <hr class="hrclassPop" />
        </div>
        <asp:GridView ID="BusquedaClientePop" runat="server" AutoGenerateColumns="False" CssClass="table table-border cell-border" CellPadding="4" ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="clienteid" HeaderText="NroCliente" />
                <asp:BoundField DataField="razonsocial" HeaderText="Razón Social" />
                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="apellido" HeaderText="Apellido" />
                <asp:BoundField DataField="telefono" HeaderText="Telefono" />
                <asp:BoundField DataField="email" HeaderText="E-mail" />
                <asp:BoundField DataField="direccion" HeaderText="Dirección" />
                <asp:BoundField DataField="cuil" HeaderText="Cuil" />
                <asp:BoundField DataField="tipo" HeaderText="Tipo de Cliente" />
                <asp:TemplateField HeaderText="" ShowHeader="False">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Recursos/Check.png" Text="Botón" BorderWidth="2px" CausesValidation="false" OnClick="ImageButton1_Click" />
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
    <asp:LinkButton Text="" ID="LkBCantidad" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" CancelControlID="" TargetControlID="LkBCantidad" PopupControlID="PanelCantidad" runat="server"></ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelCantidad" Style="display: none" Height="200px" Width="300px" BorderColor="Black" BorderWidth="2px" BorderStyle="Double" BackColor="#FAFAE7" runat="server" HorizontalAlign="Center">
        <div style="position: absolute; top: 25px; left: 11px;">
            <asp:Label ID="Label9" CssClass="Labels" runat="server" Text="Cantidad: "></asp:Label>
        </div>
        <div style="position: absolute; top: 66px; left: 65px; text-align: center;">
            <asp:TextBox ID="TextCant" Text="1" CssClass="Textos" runat="server" Width="150px" TextMode="Number"></asp:TextBox>
            <div style="position: absolute">
                <asp:Label ID="LblError" runat="server" CssClass="ValiAgregarGridCant" Text="No hay stock suficiente" Visible="false"></asp:Label><asp:RangeValidator ID="RangeValidator1" CssClass="ValiAgregarGridCant" runat="server" Display="Static" ControlToValidate="TextCant" ErrorMessage=" Error: solo numeros positivos en este campo" MinimumValue="0" MaximumValue="9999999999999">Error: solo numeros positivos en este campo</asp:RangeValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass=" ValiAgregarGridCant" ControlToValidate="TextCant" runat="server" ErrorMessage="Campo vacio: ingrese cantidad">Campo vacio: ingrese cantidad</asp:RequiredFieldValidator>
            </div>
        </div>
        <asp:Button ID="Button2" CssClass="Boton CantBAgrePedido" runat="server" Width="125px" Text="Aceptar" OnClick="Button2_Click" />
        <asp:Button ID="Button3" runat="server" CssClass="Boton CantCancelarBAgrePedido" CausesValidation="false" OnClick="Button3_Click1" Text="Cancelar" Width="125px" />
    </asp:Panel>
</asp:Content>
