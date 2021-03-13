<%@ Page Title="" Language="C#" MasterPageFile="~/MasterProveedor.Master" AutoEventWireup="true" CodeBehind="EditarProveedor.aspx.cs" Inherits="TpFinalPracticaProfesional.EditarProveedor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenedorPrincipal" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
   <script src="https://cdn.jsdelivr.net/npm/sweetalert2@7.22.2/dist/sweetalert2.all.min.js"></script>

    <script type="text/javascript">
        function successalert() {
            swal({
                title: '',
                text: 'El proveedor se modifico correctamente',
                type: 'success'
            });
        }
        function erroralert() {
            swal({
                title: '',
                text: 'No se encontro el proveedor',
                type: 'error'
            });
        }
        function errorelim() {
            swal({
                title: '',
                text: 'Proveedor eliminado',
                type: 'success'
            });
        }
        function errorcancelelim() {
            swal({
                title: '',
                text: 'Se cancelo la eliminación',
                type: 'error'
            });
        }

        function errorDepende() {
            swal({
                title: '',
                text: 'No se pudo eliminar. Ha sido desactivado. Aún hay articulos cargados de éste proveedor. ',
                type: 'error'
            });
        }
    </script>

            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Panel ID="PanelBuscar" CssClass="PanelPersonalizado" runat="server">
    <h1>Editar Proveedor</h1>
   <p>&nbsp;</p>
        <asp:Label ID="LbBuscar" CssClass="LabelsTEditPro" runat="server" Text="Buscar Proveedor: " ></asp:Label>
        <asp:TextBox ID="TxtBuscarCuit" runat="server" placeholder="Ingrese Cuit (sin guiones)" CssClass="TextosEditPro" Width="350px" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ValidationGroup="ValiCuit" ID="RequiredFieldValidator1" CssClass="ValiEditPro" ControlToValidate="TxtBuscarCuit" runat="server" ErrorMessage="Campo vacio: ingrese cuit"></asp:RequiredFieldValidator><asp:CustomValidator ID="CustomValidator1" CssClass="ValiEditPro"  ValidationGroup="ValiCuit" OnServerValidate="CustomValidator1_ServerValidate" ControlToValidate="TxtBuscarCuit" runat="server" ErrorMessage="Error: solo numeros positivos en este campo">Error: solo numeros positivos en este campo</asp:CustomValidator>
        <asp:TextBox ID="TxtBuscarApellido" runat="server" placeholder="Ingrese Apellido" CssClass="TextosEditPro" Width="350px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="ValiApe" CssClass="ValiEditPro" ControlToValidate="TxtBuscarApellido" runat="server" ErrorMessage="Campo vacio: ingrese apellido"></asp:RequiredFieldValidator>
        <asp:TextBox ID="TxtBuscarNFantasia" runat="server" placeholder="Ingrese Nombre de Fantasia" CssClass="TextosEditPro" Width="350px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="ValiNF" CssClass="ValiEditPro" ControlToValidate="TxtBuscarNFantasia" runat="server" ErrorMessage="Campo vacio: ingrese nombre de fantasia"></asp:RequiredFieldValidator>
        <asp:TextBox ID="TxtBuscarRSocial" runat="server" placeholder="Ingrese Razón Social" CssClass="TextosEditPro" Width="350px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="ValiEditPro" ValidationGroup="ValiRS" ControlToValidate="TxtBuscarRSocial" runat="server" ErrorMessage="Campo vacio: ingrese razón social"></asp:RequiredFieldValidator>

        <asp:Button ID="BtnBuscarCuit" runat="server" ValidationGroup="ValiCuit" CssClass="BotonBusEditPro Boton" Height="35px" Text="Buscar" Width="180px" OnClick="BtnBuscarCuit_Click" />
        <asp:Button ID="BtnBuscarApellido" runat="server" CssClass="BotonBusEditPro Boton" Height="35px" ValidationGroup="ValiApe" Text="Buscar" Width="180px" OnClick="BtnBuscarApellido_Click" />
        <asp:Button ID="BtnBuscarNFantasia" runat="server" CssClass="BotonBusEditPro Boton" Height="35px" Text="Buscar" Width="180px" ValidationGroup="ValiNF" OnClick="BtnBuscarNFantasia_Click" />
        <asp:Button ID="BtnBuscarRSocial" ValidationGroup="ValiRS" runat="server" CssClass="BotonBusEditPro Boton" Height="35px" Text="Buscar" Width="180px" OnClick="BtnBuscarRSocial_Click" />

        &nbsp;&nbsp;&nbsp;
        <asp:RadioButtonList ID="RadioButtonListBusqueda" CssClass="RadioListEditPro" runat="server" RepeatDirection="Horizontal" AutoPostBack="True">
            <asp:ListItem Value="Cuit" >Cuit</asp:ListItem>
            <asp:ListItem Value="Apellido" >Apellido</asp:ListItem>
            <asp:ListItem Value="NombredeFantasia" Selected="True">Nombre de Fantasia</asp:ListItem>
            <asp:ListItem Value="RazonSocial">Razón Social</asp:ListItem>
        </asp:RadioButtonList>
    </asp:Panel>
    &nbsp;
    <asp:Panel ID="PanelGrilla" CssClass="PanelPersonalizado scrolling-table-container" runat="server" Visible="false">
        &nbsp;&nbsp;
    <asp:GridView ID="TodosProveedores" AutoGenerateColumns="False" CssClass="table table-border cell-border" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="TodosProveedores_RowDataBound" AllowPaging="True" OnPageIndexChanging="TodosProveedores_PageIndexChanging">

            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="nombrefantasia" HeaderText="Nombre de Fantasia" />
                <asp:BoundField DataField="razonsocial" HeaderText="Razón Social" />
                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="apellido" HeaderText="Apellido" />
                <asp:BoundField DataField="telefono" HeaderText="Telefono" />
                <asp:BoundField DataField="email" HeaderText="E-mail" />
                <asp:BoundField DataField="cuit" HeaderText="Cuit" />
                <asp:BoundField DataField="direccion" HeaderText="Dirección" />
                <asp:BoundField DataField="estado" HeaderText="Estado" />
                <asp:TemplateField HeaderText="Editar" ShowHeader="False">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Recursos/EditarNegro48.png" Text="Botón" BorderWidth="2px" CausesValidation="false" OnClick="ImageButton1_Click"/>   
                    </ItemTemplate>
                    <ControlStyle BorderColor="Black" CssClass="BotoDataGrid" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Eliminar" ShowHeader="False">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Recursos/EliminarProveedor.png" Text="Botón" BorderWidth="2px" CausesValidation="false" OnClick="ImageButton2_Click"/>   
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
    <br />

    <asp:LinkButton Text="" ID = "btnPopup" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" OkControlID="" CancelControlID="BtnEditCancelar" TargetControlID="btnPopup" PopupControlID="PanelPopUp" runat="server"></ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelPopUp" style="display:none" CssClass="PanelPop" runat="server">
    <div class="BordePop">
        <div class="Titulo" >Editar</div>
        <hr />   
        </div>     
        <center>
            <div class ="inline-form">
            <div style="margin-left: 2.1%"><asp:Label ID="lbNFantasia" CssClass="LabelsEditPro" runat="server" Text="Nombre Fantasía: " Font-Size="Medium"></asp:Label></div>
            <asp:TextBox ID="TxtNFantasia" CssClass="TextosEditPro" runat="server" Width="40%" BackColor="White"></asp:TextBox><div style="color:red; font-size:xx-large" ><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TxtNFantasia" Display="Static" ErrorMessage="• Ingrese nombre de fantasia">*</asp:RequiredFieldValidator></div>
            </div>
            <div class ="inline-form">
           <div style="margin-left: 7%"><asp:Label ID="LbRazonSol" CssClass="LabelsEditPro" Font-Size="Medium" runat="server" Text="Razón Social: " ></asp:Label></div>
            <asp:TextBox ID="TxtRazonS" CssClass="TextosEditPro"  runat="server" Width="40%"></asp:TextBox> <div style="color:red; font-size:xx-large" ><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="TxtRazonS" Display="Static" ErrorMessage="• Ingrese razón social" >*</asp:RequiredFieldValidator></div>
            </div>
            <div class ="inline-form">
            <div style="margin-left: 13.5%"><asp:Label ID="lbNombre" CssClass="LabelsEditPro" Font-Size="Medium" runat="server" Text="Nombre: " ></asp:Label></div>
            <asp:TextBox ID="TxtNombre"  runat="server" BackColor="White" Width="40%" CssClass="TextosEditPro"></asp:TextBox><div style="color:red; font-size:xx-large" ><asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="TxtNombre" Display="Static" ErrorMessage="• Ingrese nombre" >*</asp:RequiredFieldValidator></div>
            </div>
            <div class ="inline-form">
            <div style="margin-left: 13.2%"><asp:Label ID="lbApellido" runat="server" Font-Size="Medium" CssClass="LabelsEditPro" Text="Apellido: "></asp:Label></div>
            <asp:TextBox ID="TxtApellido" runat="server" CssClass="TextosEditPro" Width="40%"></asp:TextBox><div style="color:red; font-size:xx-large" ><asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="TxtApellido" Display="Static" ErrorMessage="• Ingrese apellido" >*</asp:RequiredFieldValidator></div>
            </div>
            <div class ="inline-form">
            <div style="margin-left: 15.5%"><asp:Label ID="LbEmail"  runat="server" Font-Size="Medium" CssClass="LabelsEditPro" Text="E-mail: "></asp:Label></div>
            <asp:TextBox ID="TxtEmail" TextMode="Email" runat="server" CssClass="TextosEditPro" Width="40%"></asp:TextBox><div style="color:red; font-size:xx-large" ><asp:RequiredFieldValidator  ID="RequiredFieldValidator8" runat="server" ControlToValidate="TxtEmail" Display="Static" ErrorMessage="• Ingrese e-mail" >*</asp:RequiredFieldValidator></div>
            </div>
            <div class ="inline-form">
            <div style="margin-left: 12.5%"><asp:Label ID="lbTelefono" runat="server" Font-Size="Medium" CssClass="LabelsEditPro"  Text="Telefono:"></asp:Label></div>
            <asp:TextBox ID="TextTelefono" runat="server" CssClass="TextosEditPro" Width="40%"></asp:TextBox><div style="color:red; font-size:xx-large" ><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextTelefono" Display="Static" ErrorMessage="• Ingrese telefono" >*</asp:RequiredFieldValidator>
            <asp:RangeValidator ID="RangeValidator2" runat="server" Font-Size="XX-Large" Font-Bold="True" Display="Static" ForeColor="#BC1010" ControlToValidate="TextTelefono" ErrorMessage="• Error: solo numeros positivos en este campo" MinimumValue="0" MaximumValue="9999999999999">*</asp:RangeValidator></div>
            </div>
            <div class ="inline-form">
            <div style="margin-left: 18.5%"><asp:Label ID="LbCuit" CssClass="LabelsEditPro" Font-Size="Medium" runat="server" Text="Cuit: "></asp:Label></div>
            <asp:TextBox ID="TxtCuit" CssClass="TextosEditPro" runat="server" Width="40%" TextMode="Number"></asp:TextBox><div style="color:red; font-size:xx-large" ><asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="TxtCuit" Display="Static" ErrorMessage="• Ingrese cuit" >*</asp:RequiredFieldValidator>
            <asp:RangeValidator ID="RangeValidator1" runat="server" Font-Size="XX-Large" Font-Bold="True" Display="Static" ForeColor="#BC1010" ControlToValidate="TxtCuit" ErrorMessage="• Error: solo numeros positivos en este campo" MinimumValue="0" MaximumValue="99999999999">*</asp:RangeValidator></div>
            <div style="position: absolute; top: 455px; left: 502px;"><asp:Label ID="Labelmgg" runat="server" CssClass="Vali2EditPro" Text="ERROR: Cuit ingresado en otro proveedor" Visible ="false"></asp:Label></div>
            </div>
            <div class ="inline-form">
            <div style="margin-left: 11.5%"><asp:Label ID="LbDireccion" CssClass="LabelsEditPro" Font-Size="Medium" runat="server" Text="Dirección: "></asp:Label></div>
            <asp:TextBox ID="TxtDirecion" CssClass="TextosEditPro" runat="server" Width="40%"></asp:TextBox><div style="color:red; font-size:xx-large" ><asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="TxtDirecion" Display="Dynamic" ErrorMessage="• Ingrese dirección" >*</asp:RequiredFieldValidator></div>
            </div>
            <div class ="inline-form">
           <div style="margin-left: 14.7%"><asp:Label ID="Label1" CssClass="LabelsEditPro" Font-Size="Medium" runat="server" Text="Estado: "></asp:Label></div>
            <asp:DropDownList ID="ddlestado" CssClass="TextosEditPro" Width="40%" runat="server">
                <asp:ListItem Value="activo">Activo</asp:ListItem>
                <asp:ListItem Value="no activo">No Activo</asp:ListItem>
                </asp:DropDownList>
            </div>
           <hr />
            </center>
            <center>
              <asp:Button ID="BtnEditAceptar" CssClass="Boton BtnEditAceptarEditPro " Width="180px" Height="35px" runat="server" Text="Aceptar" OnClick="BtnEditAceptar_Click"/>
              <asp:Button ID="BtnEditCancelar" CssClass="Boton BtnEditCancelarEditPro " Width="180px" Height="35px" runat="server" Text="Cancelar" />
            </center>
    </asp:Panel>
            <br />
        <br />
        <asp:LinkButton Text="" ID="LinkOculto" runat="server" /> 
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" CancelControlID="BtnCancelarEliminar" TargetControlID="LinkOculto" PopupControlID="MensajeConfirmacion" runat="server"></ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="MensajeConfirmacion" runat="server" style="display:none" CssClass="PanelEliminarPop">
            <div class="swal-icon swal-icon--warning">
                <span class="swal-icon--warning__body">
                    <span class="swal-icon--warning__dot"> </span>
                </span>
            </div>
            <asp:Button ID="BtnAceptarEliminar" runat="server" Text="Aceptar" CausesValidation="false" CssClass="BtnAcepPopElim" OnClick="BtnAceptarEliminar_Click1" />
            <asp:Button ID="BtnCancelarEliminar" runat="server" Text="Cancelar" CssClass="BtnElimPopElim"/>
            <asp:Label ID="Mensaje" runat="server" CssClass="textMensaje" Text="¿Estás seguro?" Font-Bold="True"></asp:Label>
            <asp:Label ID="Label3" runat="server" CssClass="textMensaje1" Text="Se eliminará de manera permanente"></asp:Label>
        </asp:Panel>
</asp:Content>