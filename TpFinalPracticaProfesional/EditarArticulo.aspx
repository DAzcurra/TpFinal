<%@ Page Title="" Language="C#" MasterPageFile="~/MasterArticulo.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="EditarArticulo.aspx.cs" Inherits="TpFinalPracticaProfesional.EditarArticulo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenedorPrincipal" runat="server">

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<%--    <script src="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert.min.js"></script>--%>
   <script src="https://cdn.jsdelivr.net/npm/sweetalert2@7.22.2/dist/sweetalert2.all.min.js"></script>
<%--   <script src="https://cdn.jsdelivr.net/npm/sweetalert2@7.22.2/package.json"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@7.22.2/sweetalert2.d.ts"></script>--%>
    <script type="text/javascript">
        function successalert() {
            swal({
                title: '',
                text: 'El articulo se modifico correctamente',
                type: 'success'
            });
        }
        function erroralert() {
            swal({
                title: '',
                text: 'No se encontro el articulo',
                type: 'error'
            });
        }
        function errorelim() {
            swal({
                title: '',
                text: 'Articulo eliminado',
                type: 'success'
            });
        }
        function errorcanelelim() {
            swal({
                title: '',
                text: 'Se cancelo la eliminación',
                type: 'error'
            });
        }

        function errorDepende() {
            swal({
                title: '',
                text: 'No se pudo eliminar. Ha sido desactivado. ',
                type: 'error'
            });
        }
    </script>
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Panel ID="PanelBuscar" CssClass="PanelPersonalizado" runat="server">
    <h1>Editar Articulo</h1>
   <p>&nbsp;</p>
        <asp:Label ID="LbBuscar" CssClass="LabelsT" runat="server" Text="Buscar Articulo: " ></asp:Label>
        <asp:TextBox ID="TxtBuscarNombre" runat="server" placeholder="Ingrese Nombre" CssClass="TextosEditArt" Width="350px"></asp:TextBox><asp:RequiredFieldValidator ValidationGroup="ValiNom" ID="RequiredFieldValidator1" CssClass="ValiEditArt" ControlToValidate="TxtBuscarNombre" runat="server" ErrorMessage="Campo vacio: ingrese nombre"></asp:RequiredFieldValidator>
        <asp:TextBox ID="TxtBuscarMarca" runat="server" placeholder="Ingrese Marca" CssClass="TextosEditArt" Width="350px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="ValiMar" CssClass="ValiEditArt" ControlToValidate="TxtBuscarMarca" runat="server" ErrorMessage="Campo vacio: ingrese marca"></asp:RequiredFieldValidator>
        <asp:TextBox ID="TxtBuscarCod" runat="server" placeholder="Ingrese Código de Articulo" CssClass="TextosEditArt" Width="350px" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="ValiCod" CssClass="ValiEditArt" ControlToValidate="TxtBuscarCod" runat="server" ErrorMessage="Campo vacio: ingrese código de articulo"></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeValidator6" runat="server" CssClass="ValiEditArt" ErrorMessage="Error: solo numeros positivos en este campo" ControlToValidate="TxtBuscarCod" MinimumValue="0" MaximumValue="99999999" ValidationGroup="ValiCod">Error: solo numeros positivos en este campo</asp:RangeValidator>
        <asp:Button ID="BtnBuscarNombre" runat="server" ValidationGroup="ValiNom" CssClass="BotonBusEditArt Boton" Height="35px" Text="Buscar" Width="180px" OnClick="BtnBuscarNombre_Click" />
        <asp:Button ID="BtnBuscarMarca" runat="server" CssClass="BotonBusEditArt Boton" Height="35px" ValidationGroup="ValiMar" Text="Buscar" Width="180px" OnClick="BtnBuscarMarca_Click" />
        <asp:Button ID="BtnBuscarCod" runat="server" CssClass="BotonBusEditArt Boton" Height="35px" ValidationGroup="ValiCod" Text="Buscar" Width="180px" OnClick="BtnBuscarCod_Click" />
        &nbsp;&nbsp;&nbsp;
        <asp:RadioButtonList ID="RadioButtonListBusqueda" CssClass="RadioListEditArt" runat="server" RepeatDirection="Horizontal" AutoPostBack="True">
            <asp:ListItem Value="nombre" Selected="True">Nombre</asp:ListItem>
            <asp:ListItem Value="marca">Marca</asp:ListItem>
            <asp:ListItem Value="cod">Código de Articulo</asp:ListItem>
        </asp:RadioButtonList>
    </asp:Panel>
    &nbsp;
    <asp:Panel ID="PanelGrilla" CssClass="PanelPersonalizado" runat="server" Visible=" false">
     &nbsp;&nbsp;

        <asp:GridView ID="TodosArticulos" AllowPaging="True"  AutoGenerateColumns="False" CssClass="table table-border cell-border" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="TodosArticulos_RowDataBound" OnPageIndexChanging="TodosArticulos_PageIndexChanging">

            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
           <Columns>
               <asp:BoundField DataField="articuloid" HeaderText="Codigo Articulo" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
               <ItemStyle HorizontalAlign="Center" Width="40px" />
               </asp:BoundField>
               <asp:BoundField DataField="nombre" HeaderText="Nombre" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center" >
                <ItemStyle HorizontalAlign="Center" Width="40px" />
               </asp:BoundField>
                <asp:BoundField DataField="descripcion" HeaderText="Descripción" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="40px" />
               </asp:BoundField>
                <asp:BoundField DataField="marca" HeaderText="Marca" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="40px" />
               </asp:BoundField>
                <asp:BoundField DataField="precioactual" HeaderText="Precio Actual" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center" >
                <ItemStyle HorizontalAlign="Center" Width="40px" />
               </asp:BoundField>
                <asp:BoundField DataField="cantidad" HeaderText="Cantidad" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center" >
                <ItemStyle HorizontalAlign="Center" Width="40px" />
               </asp:BoundField>
                <asp:BoundField DataField="stockmin" HeaderText="Stock Minimo"  ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="40px" />
               </asp:BoundField>
                <asp:BoundField DataField="costo" HeaderText="Precio de Costo"  ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="40px" />
               </asp:BoundField>
                <asp:BoundField DataField="nombrefantasia" HeaderText="Proveedor" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center" >
                <ItemStyle HorizontalAlign="Center" Width="40px" />
               </asp:BoundField>
                <asp:BoundField DataField="estado" HeaderText="Estado" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center"  >
                <ItemStyle HorizontalAlign="Center" Width="40px" />
               </asp:BoundField>
                <asp:TemplateField HeaderText="Editar" ShowHeader="False">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Recursos/EditarNegro48.png" Text="Botón" BorderWidth="2px" CausesValidation="false" OnClick="ImageButton1_Click"/>   
                    </ItemTemplate>
                    <ControlStyle BorderColor="Black" CssClass="BotoDataGrid" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Eliminar" ShowHeader="False">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton2"  runat="server" ImageUrl="~/Recursos/EliminarProveedor.png" Text="Botón" BorderWidth="2px" CausesValidation="false" OnClick="ImageButton2_Click"/>   
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
    &nbsp;&nbsp;
        <br />
    <asp:LinkButton Text="" ID = "btnPopup" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" CancelControlID="BtnEditCancelar" TargetControlID="btnPopup" PopupControlID="PanelPopUp" runat="server"></ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelPopUp" style="display:none" CssClass="PanelPop" runat="server">
    <div class="BordePop">
        <div class="Titulo" >Editar</div>
        <hr />   
        </div>       
        <center>
<%--            <asp:ValidationSummary ID="ValidationSummary2" CssClass="VSM2" runat="server" Font-Bold="True" ForeColor="#BC1010" HeaderText="Falta completar campo/s" Font-Underline="True" DisplayMode="List"/>--%>
            <div class="inline-form">
            <div style="margin-left: 0.2%"><asp:Label ID="Label1" CssClass="LabelsEditArt" runat="server" Text="Código de Articulo: " Font-Size="Medium"></asp:Label></div>
            <asp:TextBox ID="TxtCodArt" CssClass="TextosEditArt" runat="server" Width="40%" BackColor="White" TextMode="Number"></asp:TextBox><div style="color:red; font-size:xx-large" ><asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="TxtCodArt" Display="Static" ErrorMessage="• Ingrese código de articulo">*</asp:RequiredFieldValidator></div><asp:RangeValidator ID="RangeValidator4" runat="server" ErrorMessage="• Error: solo numeros positivos en este campo"  ControlToValidate="TxtCodArt" CssClass="Vali2EditArt" MinimumValue="0" MaximumValue="99999999">Error: solo numeros positivos en este campo</asp:RangeValidator>
            <asp:Label ID="Labelmgg" runat="server" CssClass="Vali2EditArt" Text="ERROR: Codigo ingresado en otro articulo" Visible ="false"></asp:Label>
            </div>
            <div class="inline-form">
            <div style="margin-left: 14%"><asp:Label ID="lbNombre" CssClass="LabelsEditArt" runat="server" Text="Nombre:" Font-Size="Medium"></asp:Label></div>
            <asp:TextBox ID="TxtNombre" CssClass="TextosEditArt " runat="server" Width="40%" BackColor="White"></asp:TextBox><div style="color:red; font-size:xx-large" ><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TxtNombre" Display="Static" ErrorMessage="• Ingrese nombre">*</asp:RequiredFieldValidator></div>
            </div>
            <div class="inline-form">
            <div style="margin-left: 7%"><asp:Label ID="LbDescripcion" CssClass="LabelsEditArt" Font-Size="Large" runat="server" Text="Descripción: " ></asp:Label></div>
            <asp:TextBox ID="TxtDescripcion" CssClass="TextosEditArt "  runat="server" Width="40%"></asp:TextBox> <div style="color:red; font-size:xx-large" ><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="TxtDescripcion" Display="Static" ErrorMessage="• Ingrese descripción" >*</asp:RequiredFieldValidator></div>
             </div>
            <div class="inline-form">
            <div style="margin-left: 16.5%"><asp:Label ID="lbMarca" CssClass="LabelsEditArt" runat="server" Text="Marca: " Font-Size="Medium"></asp:Label></div>
            <asp:TextBox ID="TxtMarca" CssClass="TextosEditArt " runat="server" Width="40%"></asp:TextBox> <div style="color:red; font-size:xx-large"><asp:RequiredFieldValidator  ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtMarca" Display="Dynamic" ErrorMessage="• Ingrese marca">*</asp:RequiredFieldValidator></div>
            </div>
            <div class="inline-form">
            <div style="margin-left: 7%"><asp:Label ID="lbPrecioAc" CssClass="LabelsEditArt" runat="server" Text="Precio Actual:" Font-Size="Medium"></asp:Label></div>
            <asp:TextBox ID="TxtPrecioAc" CssClass="TextosEditArt" runat="server" Width="40%"></asp:TextBox> <div style="color:red; font-size:xx-large">
            <asp:RangeValidator ID="RangeValidator3" runat="server" ErrorMessage="• Error: solo numeros positivos en este campo"  ControlToValidate="TxtPrecioAc" MinimumValue="0" MaximumValue="999999">*</asp:RangeValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtPrecioAc" Display="Dynamic" ErrorMessage="• Ingrese precio actual" >*</asp:RequiredFieldValidator></div> 
            </div>
            <div class="inline-form">
            <div style="margin-left: 13%"><asp:Label ID="LbCantidad" CssClass="LabelsEditArt" runat="server" Text="Cantidad: " Font-Size="Medium"></asp:Label></div>
            <asp:TextBox ID="TxtCantidad" CssClass="TextosEditArt" runat="server" Width="40%" TextMode="Number"></asp:TextBox> <div style="color:red; font-size:xx-large">
            <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="• Error: solo numeros positivos en este campo"  ControlToValidate="TxtCantidad" MinimumValue="0" MaximumValue="999999">*</asp:RangeValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TxtCantidad" Display="Dynamic" ErrorMessage="• Ingrese cantidad" >*</asp:RequiredFieldValidator></div>
            </div>
            <div class="inline-form">
            <div style="margin-left: 7%"><asp:Label ID="LbStockMin" CssClass="LabelsEditArt" runat="server" Text="Stock Minimo: " Font-Size="Medium"></asp:Label></div>
            <asp:TextBox ID="TxtStockMin" CssClass="TextosEditArt" runat="server" Width="40%" TextMode="Number"></asp:TextBox><div style="color:red; font-size:xx-large"><asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="• Error: solo numeros positivos en este campo" ControlToValidate="TxtStockMin" MinimumValue="0" MaximumValue="999999">*</asp:RangeValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="TxtStockMin" Display="Dynamic" ErrorMessage="• Ingrese stock minimo">*</asp:RequiredFieldValidator></div>
            </div>
            <div class="inline-form">
            <div style="margin-left: 11%"><asp:Label ID="Label3" CssClass="LabelsEditArt" runat="server" Text="Proveedor: " Font-Size="Medium"></asp:Label></div>
            <asp:DropDownList ID="DdlProveedor" runat="server" Width="40%" AutoPostBack="False"></asp:DropDownList>
            </div>
            <div class="inline-form">
            <div style="margin-left:4%"><asp:Label ID="Label2" CssClass="LabelsEditArt" runat="server" Text="Precio de Costo: " Font-Size="Medium"></asp:Label></div>
            <asp:TextBox ID="TxtCosto" CssClass="TextosEditArt" runat="server" Width="40%" BackColor="White" TextMode="SingleLine"></asp:TextBox><div style="color:red; font-size:xx-large" ><asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="TxtCosto" Display="Static" ErrorMessage="• Ingrese precio de costo">*</asp:RequiredFieldValidator><asp:RangeValidator ID="RangeValidator5" runat="server" ErrorMessage="• Error: solo numeros positivos en este campo"  ControlToValidate="TxtCosto" MinimumValue="0" MaximumValue="99999999">*</asp:RangeValidator></div>
            </div>
            <div class="inline-form">
                <div style="margin-left: 15.5%"><asp:Label ID="Label4" CssClass="LabelsEditArt" runat="server" Text="Estado: " Font-Size="Medium"></asp:Label></div>
               <asp:DropDownList ID="ddlestado" runat="server" Width="40%" AutoPostBack="False">
                   <asp:ListItem Value="activo">Activo</asp:ListItem>
                   <asp:ListItem Value="no activo">No Activo</asp:ListItem>
                </asp:DropDownList>
            </div>
            <hr />
            </center>
            <center>
             <asp:Button ID="BtnEditAceptar" CssClass="Boton BtnEditAceptarEditArt" Width="180px" Height="35px" runat="server" Text="Aceptar" OnClick="BtnEditAceptar_Click" />
              <asp:Button ID="BtnEditCancelar" CssClass="Boton BtnEditCancelarEditArt" Width="180px" Height="35px" runat="server" Text="Cancelar" />
            </center>
        &nbsp;
    </asp:Panel>
    &nbsp;   
    <asp:LinkButton Text="" ID="LinkOculto" runat="server" />
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" CancelControlID="BtnCancelarEliminar" TargetControlID="LinkOculto" PopupControlID="MensajeConfirmacion" runat="server"></ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="MensajeConfirmacion" runat="server" style="display:none" CssClass="PanelEliminarPop">
            <div class="swal-icon swal-icon--warning">
                <span class="swal-icon--warning__body">
                    <span class="swal-icon--warning__dot"> </span>
                </span>
            </div>
            <asp:Button ID="BtnAceptarEliminar" runat="server" Text="Aceptar" CausesValidation="false" CssClass="BtnAcepPopElim" OnClick="BtnAceptarEliminar_Click" />
            <asp:Button ID="BtnCancelarEliminar" runat="server" Text="Cancelar" CssClass="BtnElimPopElim"/>
            <asp:Label ID="Mensaje" runat="server" CssClass="textMensaje" Text="¿Estás seguro?" Font-Bold="True"></asp:Label>
            <asp:Label ID="Label5" runat="server" CssClass="textMensaje1" Text="Se eliminará de manera permanente"></asp:Label>
        </asp:Panel>
</asp:Content>
