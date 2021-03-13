<%@ Page Title="" Language="C#" MasterPageFile="~/MasterCliente.Master" AutoEventWireup="true" CodeBehind="~/EditarCliente.aspx.cs" Inherits="TpFinalPracticaProfesional.EditarCliente" %>
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
                text: 'El cliente se modifico correctamente',
                type: 'success'
            });
        }
        function erroralert() {
            swal({
                title: '',
                text: 'No se encontro el cliente',
                type: 'error'
            });
        }
        function erroralertModif() {
            swal({
                title: '',
                text: 'No se modifico el cliente!!',
                type: 'error'
            });
        }
        function errorelim() {
            swal({
                title: '',
                text: 'Cliente eliminado',
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
                text: 'No se pudo eliminar. Ha sido desactivado. Aún hay facturas o pedidos cargadas con este cliente ',
                type: 'error'
            });
        }
    </script>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:Panel ID="Panel1" CssClass="PanelPersonalizado" runat="server">
    <h1>Editar Cliente</h1>
    <p>&nbsp;</p>

        <asp:Label ID="LbBuscar" CssClass="LabelsTEditClient" runat="server" Text="Buscar Cliente: " ></asp:Label>
         <asp:TextBox ID="TxtBuscarRSocial" CssClass="TextosEditClient" runat="server" placeholder="Ingrese razón social" Width="350px"></asp:TextBox>
        <asp:RadioButton ID="RadioRSocial" runat="server" AutoPostBack=" true" CssClass="RadioRSocial" GroupName="RadioBusqueda" Text="Razón Social" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtBuscarRSocial" CssClass="ValiEditClient" ErrorMessage="Campo vacio: ingrese razón social" ValidationGroup="ValiRSocial">Campo vacio: ingrese razón social</asp:RequiredFieldValidator>
        <asp:TextBox ID="TxtBuscarApellido" runat="server" CssClass="TextosEditClient" placeholder="Ingrese apellido" Width="350px"></asp:TextBox>
        <asp:RadioButton ID="RadioApellido" AutoPostBack="true" CssClass="RadioApellido" GroupName="RadioBusqueda" runat="server" Text="Apellido" Checked="True" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtBuscarApellido" CssClass="ValiEditClient" ErrorMessage="Campo vacio: ingrese Apellido" ValidationGroup="ValiApellido">Campo vacio: ingrese apellido</asp:RequiredFieldValidator>
        <asp:TextBox ID="TxtBuscarcuil" runat="server" CssClass="TextosEditClient" Width="350px" placeholder="Ingrese cuil" TextMode="Number"></asp:TextBox>
        <asp:RequiredFieldValidator CssClass="ValiEditClient" ID="RequiredFieldValidator3" ValidationGroup="ValiRCuil" runat="server" ControlToValidate="TxtBuscarcuil" Display="Dynamic" ErrorMessage="Campo vacio: ingrese cuil">Campo vacio:ingrese cuil</asp:RequiredFieldValidator>
        <asp:CustomValidator ID="CustomValidator1" CssClass="ValiEditClient" ValidationGroup="ValiRCuil" runat="server" ControlToValidate="TxtBuscarcuil" ErrorMessage="ERROR: cuil no encontrado!!" OnServerValidate="CustomValidator1_ServerValidate">ERROR: cuil no encontrado!!</asp:CustomValidator><asp:CustomValidator ID="CustomValidator2" ControlToValidate="TxtBuscarcuil" ValidationGroup="ValiRCuil" CssClass="ValiEditClient" runat="server" ErrorMessage="Error: solo numeros positivos en este campo" OnServerValidate="CustomValidator2_ServerValidate">Error: solo numeros positivos en este campo</asp:CustomValidator>
        <asp:DropDownList ID="DropTipo" runat="server" CssClass="TextosEditClient" ValidationGroup="ValiTipo" Visible="False" Width="350px">
        <asp:ListItem Value="consumidor final">Consumidor Final</asp:ListItem>
        <asp:ListItem Value="monotributista">Monotributista</asp:ListItem>
        <asp:ListItem Value="responsable inscripto">Responsable Inscripto</asp:ListItem>
        <asp:ListItem Value=" sujeto exento"> Sujeto Exento</asp:ListItem>
        </asp:DropDownList>
        <asp:RadioButton ID="RadioTipo" runat="server" AutoPostBack="true" CssClass="RadioTipo" GroupName="RadioBusqueda" Text="Tipo de Cliente" />
        <asp:RadioButton ID="RadioCuil" runat="server" AutoPostBack="true" CssClass="RadioCuill" GroupName="RadioBusqueda" Text="Cuil" />
    <asp:Button ID="BntBuscarCuil" ValidationGroup="ValiRCuil" CssClass="BotonBusEditClient BotonAgregar" runat="server" Text="Buscar" Width="180px" Height="35px" OnClick="BntBuscarCuil_Click" />
    <asp:Button ID="BtnBuscarRSocial" ValidationGroup="ValiRSocial" CssClass="BotonBusEditClient BotonAgregar" runat="server" Text="Buscar" Width="180px" Height="35px" OnClick="BtnBuscarRSocial_Click" />
    <asp:Button ID="BtnBuscarApellido" ValidationGroup="ValiApellido" CssClass="BotonBusEditClient BotonAgregar" runat="server" Text="Buscar" Width="180px" Height="35px" OnClick="BtnBuscarApellido_Click" />
    <asp:Button ID="BtnBuscarTipo" ValidationGroup="ValiTipo" CssClass="BotonBusEditClient BotonAgregar" runat="server" Text="Buscar" Width="180px" Height="35px" OnClick="BtnBuscarTipo_Click" />
    &nbsp;&nbsp;&nbsp;
    </asp:Panel>
    &nbsp;
    <asp:Panel ID="PanelGrilla" CssClass="PanelPersonalizado scrolling-table-container" runat="server" Visible=" false"  Width="100%">
    &nbsp;&nbsp;
    <asp:GridView ID="BusquedaCliente" AutoGenerateColumns="False" CssClass="table table-border cell-border" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="BusquedaCliente_RowDataBound" AllowPaging="True" OnPageIndexChanging="BusquedaCliente_PageIndexChanging">
       <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="clienteid" HeaderText="NroCliente"  ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="razonsocial" HeaderText="Razón Social"  ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="nombre" HeaderText="Nombre"  ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="apellido" HeaderText="Apellido" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center" >
                <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="telefono" HeaderText="Telefono" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center" >
                <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="email" HeaderText="E-mail"  ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="direccion" HeaderText="Dirección"  ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="cuil" HeaderText="Cuil"  ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="tipo" HeaderText="Tipo de Cliente"  ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="estado" HeaderText="Estado" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
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
                        <asp:ImageButton ID="ImageButton12" runat="server" ImageUrl="~/Recursos/EliminarProveedor.png" Text="Botón" BorderWidth="2px" CausesValidation="false" OnClick="ImageButton12_Click"/>   
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
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" CancelControlID="BtnEditCancelar" TargetControlID="btnPopup" PopupControlID="PanelPopUp" OkControlID="" runat="server"></ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="PanelPopUp" runat="server" style="display:none" CssClass="PanelPop">   
    <div class="BordePop">
        <div class="Titulo" >Editar</div>
        <hr />   
        </div>        
            <center>
            <div class ="inline-form">
           <div style="margin-left: 7.5%"><asp:Label ID="LbRazonSocial" CssClass="LabelsEditClient" runat="server" Text="Razón Social: " Font-Size="Medium"></asp:Label></div>
            <asp:TextBox ID="TxtRazonS" CssClass="TextosEditClient" runat="server" Width="40%" BackColor="White"></asp:TextBox><div style="color:red; font-size:xx-large" ><asp:RequiredFieldValidator  ID="RequiredFieldValidator4" ValidationGroup="ValiEDIT" runat="server" ControlToValidate="TxtRazonS" Display="Static" ErrorMessage="• Ingrese Razón Social">*</asp:RequiredFieldValidator></div>
            </div>
            <div class ="inline-form">
            <div style="margin-left: 14%"><asp:Label ID="lbNombre" CssClass="LabelsEditClient" runat="server" Text="Nombre: " Font-Size="Medium"></asp:Label></div>
            <asp:TextBox ID="TxtNombre" CssClass="TextosEditClient" runat="server" Width="40%" BackColor="White"></asp:TextBox><div style="color:red; font-size:xx-large" ><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TxtNombre" ValidationGroup="ValiEDIT" Display="Static" ErrorMessage="•Ingrese Nombre" >*</asp:RequiredFieldValidator></div>
            </div>
            <div class ="inline-form">
            <div style="margin-left: 13.8%"><asp:Label ID="lbApellido" CssClass="LabelsEditClient" runat="server" Text="Apellido: " Font-Size="Medium"></asp:Label></div>
            <asp:TextBox ID="TxtApellido" CssClass="TextosEditClient" runat="server" Width="40%"></asp:TextBox><div style="color:red; font-size:xx-large" ><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TxtApellido" Display="Static" ValidationGroup="ValiEDIT" ErrorMessage="• Ingrese Apellido">*</asp:RequiredFieldValidator></div>
            </div>
            <div class ="inline-form">
            <div style="margin-left: 13.5%"><asp:Label ID="lbTelefono" CssClass="LabelsEditClient" runat="server" Text="Telefono:" Font-Size="Medium"></asp:Label></div>
            <asp:TextBox ID="TextTelefono" CssClass="TextosEditClient" runat="server" Width="40%"></asp:TextBox><div style="color:red; font-size:xx-large" >
            <asp:RangeValidator ID="RangeValidator1" runat="server" Font-Size="XX-Large" Font-Bold="True" Display="Static" ForeColor="#BC1010" ControlToValidate="TextTelefono" ValidationGroup="ValiEDIT" ErrorMessage="• Error: solo numeros positivos en este campo" MinimumValue="0" MaximumValue="9999999999999">*</asp:RangeValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="ValiEDIT" runat="server" ControlToValidate="TextTelefono" Display="Static" ErrorMessage="• Ingrese Telefono" >*</asp:RequiredFieldValidator> </div>
            </div>
            <div class ="inline-form">
             <div style="margin-left: 16.5%"><asp:Label ID="LbEmail" CssClass="LabelsEditClient" runat="server" Text="E-mail: " Font-Size="Medium"></asp:Label></div>
             <asp:TextBox ID="TxtEmail" TextMode="Email" CssClass="TextosEditClient" runat="server" Width="40%"></asp:TextBox> <div style="color:red; font-size:xx-large" ><asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="ValiEDIT" runat="server" ControlToValidate="TxtEmail" Display="Static" ErrorMessage="• Ingrese E-mail" >*</asp:RequiredFieldValidator></div>
             </div>
             <div class ="inline-form">
             <div style="margin-left: 12.5%"><asp:Label ID="LbDireccion" CssClass="LabelsEditClient" runat="server" Text="Dirección: " Font-Size="Medium"></asp:Label></div>
             <asp:TextBox ID="TxtDirecion" CssClass="TextosEditClient" runat="server" Width="40%"></asp:TextBox><div style="color:red; font-size:xx-large" ><asp:RequiredFieldValidator  ID="RequiredFieldValidator9" runat="server" ControlToValidate="TxtDirecion" ValidationGroup="ValiEDIT" Display="Static" ErrorMessage="• Ingrese Dirección">*</asp:RequiredFieldValidator></div>
             </div>
             <div class ="inline-form">
            <div style="margin-left: 19.5%"><asp:Label ID="LbCuil" CssClass="LabelsEditClient" runat="server" Text="Cuil: " Font-Size="Medium" Font-Bold="true"></asp:Label></div>
             <asp:TextBox ID="Txtcuil" runat="server" CssClass="TextosEditClient"  Width="40%" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator99" runat="server" ValidationGroup="ValiEDIT" ControlToValidate="Txtcuil" Font-Size="XX-Large" ErrorMessage="• ingrese cuil" Font-Bold="True" Display="Static" ForeColor="#BC1010">*</asp:RequiredFieldValidator><div style="position: absolute; top: 457px; left: 502px;">
               <asp:RangeValidator ID="RangeValidator2" runat="server" Font-Bold="True" Display="Static" ControlToValidate="Txtcuil"  ErrorMessage="• Error: solo numeros positivos en este campo" ValidationGroup="ValiEDIT" MinimumValue="0" CssClass="Vali2EditClient" MaximumValue="99999999999">• Error: solo numeros positivos en este campo</asp:RangeValidator>
                <asp:Label ID="Labelmgg" runat="server" CssClass="Vali2EditClient" Text="ERROR: Cuil ingresado en otro cliente" Visible ="false"></asp:Label></div>
             </div>
            <div class ="inline-form">
             <div style="margin-left: 5.5%"><asp:Label ID="Label1" CssClass="LabelsEditClient" runat="server" Text="Tipo de Cliente: " Font-Size="Medium"></asp:Label></div>
                <asp:DropDownList CssClass="TextosEditClient" ID="DropDownList1" runat="server" Width="40%">
            <asp:ListItem>Consumidor Final</asp:ListItem>
            <asp:ListItem>Monotributista</asp:ListItem>
            <asp:ListItem>Responsable Inscripto</asp:ListItem>
            <asp:ListItem> Sujeto Exento</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class ="inline-form">
               <div style="margin-left: 15.8%"><asp:Label ID="Label2" CssClass="LabelsEditClient" runat="server" Text="Estado: " Font-Size="Medium"></asp:Label></div>
                <asp:DropDownList CssClass="TextosEditClient" ID="DropDownList2" runat="server" Width="40%">
                    <asp:ListItem Value="activo">Activo</asp:ListItem>
                    <asp:ListItem Value="no activo">No Activo</asp:ListItem>
                </asp:DropDownList>
            </div>
            <hr />
            </center>
            <center>
              <asp:Button ID="BtnEditAceptar" CssClass="BotonAgregar BtnEditAceptarEditClient" Width="180px" Height="35px" runat="server" Text="Aceptar" OnClick="BtnEditAceptar_Click" ValidationGroup="ValiEDIT" />
             <asp:Button ID="BtnEditCancelar" CssClass="BotonAgregar BtnEditCancelarEditClient" Width="180px" Height="35px" runat="server" Text="Cancelar" OnClick="BtnEditCancelar_Click" />
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
            <asp:Button ID="BtnAceptarEliminar" runat="server" Text="Aceptar" CssClass="BtnAcepPopElim" OnClick="BtnAceptarEliminar_Click" />
            <asp:Button ID="BtnCancelarEliminar" runat="server" Text="Cancelar" CssClass="BtnElimPopElim"/>
            <asp:Label ID="Mensaje" runat="server" CssClass="textMensaje" Text="¿Estás seguro?" Font-Bold="True"></asp:Label>
            <asp:Label ID="Label3" runat="server" CssClass="textMensaje1" Text="Se eliminará de manera permanente"></asp:Label>
        </asp:Panel>
</asp:Content>
