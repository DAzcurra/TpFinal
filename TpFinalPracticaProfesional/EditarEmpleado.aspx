<%@ Page Title="" Language="C#" MasterPageFile="~/MasterEmpleado.Master" AutoEventWireup="true" CodeBehind="EditarEmpleado.aspx.cs" Inherits="TpFinalPracticaProfesional.EditarEmpleado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenedorPrincipal" runat="server">

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
   <script src="https://cdn.jsdelivr.net/npm/sweetalert2@7.22.2/dist/sweetalert2.all.min.js"></script>

    <script type="text/javascript">
               function errorDepende() {
            swal({
                title: '',
                text: 'No se pudo eliminar. Ha sido desactivado. Aún hay facturas cargadas con este empleado ',
                type: 'error'
            });
        }
        function successalert() {
            swal({
                title: '',
                text: 'El empleado se modifico correctamente',
                type: 'success'
            });
        }
        function erroralert() {
            swal({
                title: '',
                text: 'No se encontro el empleado',
                type: 'error'
            });
        }
        function errorelim() {
            swal({
                title: '',
                text: 'Empleado eliminado',
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
    </script>
    
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="PanelPersonalizado">
    <h1>Editar Empleado</h1>
    <p>&nbsp;</p>
    <asp:Label ID="LbBuscar" CssClass="LabelsTEditEmp" runat="server" Text="Buscar Empleado: " ></asp:Label>
    <asp:RadioButton ID="RadioButtonCuil" CssClass="CheckPesonalizadosEditEmp" Text="Cuil" runat="server" Checked="True" GroupName="GrupoRadio" OnCheckedChanged="RadioButton1_CheckedChanged" AutoPostBack="True" />
    <asp:RadioButton ID="RadioButtonApellido" CssClass="CheckPesonalizadosAPEditEmp" Text="Apellido" runat="server" GroupName="GrupoRadio" AutoPostBack="True" />
    <asp:TextBox ID="TxtBuscarXCuil" CssClass="TextosEditEmp" placeholder="Ingrese Cuil (sin guiones)" runat="server" Width="350px" TextMode="Number"></asp:TextBox><asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Error: solo numeros positivos en este campo" ValidationGroup="ValiGroupCuil" CssClass="ValiEditEmp" ControlToValidate="TxtBuscarXCuil" OnServerValidate="CustomValidator1_ServerValidate">Error: solo numeros positivos en este campo</asp:CustomValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator7"  ValidationGroup="ValiGroupCuil" CssClass="ValiEditEmp" ControlToValidate="TxtBuscarXCuil" runat="server" ErrorMessage="Campo vacio, ingrese cuil"></asp:RequiredFieldValidator>
    <asp:TextBox ID="TxtBuscarXApellido" CssClass="TextosEditEmp" placeholder="Ingrese Apellido" runat="server"  Width="350px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator8" CssClass="ValiEditEmp"  ValidationGroup="ValiGroupAp" runat="server" ControlToValidate="TxtBuscarXApellido" ErrorMessage="Campo vacio, ingrese apellido"></asp:RequiredFieldValidator>
    <asp:Button ID="BtnBuscarCuil" CssClass="BotonBusEditEmp BotonAgregar" ValidationGroup="ValiGroupCuil" runat="server" Text="Buscar" Width="180px" Height="35px" OnClick="BtnBuscarCuil_Click" />
    <asp:Button ID="BtnBuscarApellido" CssClass="BotonBusEditEmp BotonAgregar" runat="server" ValidationGroup="ValiGroupAp" Text="Buscar" Width="180px" Height="35px" OnClick="BtnBuscarApellido_Click" />
    &nbsp;&nbsp;
    </div>
    &nbsp;
    <asp:Panel ID="PanelGrilla" CssClass="PanelPersonalizado scrolling-table-container" runat="server" Visible=" false" >
           &nbsp;&nbsp;
        <asp:GridView ID="ListaEmpleados" runat="server" CssClass="table table-border cell-border" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" OnPageIndexChanging="ListaEmpleados_PageIndexChanging" PageSize="2" OnRowDataBound="ListaEmpleados_RowDataBound" >
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="nombre" HeaderText="Nombre" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="apellido" HeaderText="Apellido" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="telefono" HeaderText="Telefono" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="email" HeaderText="E-mail" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="direccion" HeaderText="Dirección" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="cuil" HeaderText="Cuil" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center" >
                <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="estado" HeaderText="Estado" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Editar" ShowHeader="False">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Recursos/EditarNegro48.png" Text="Botón" BorderWidth="2px" OnClick="ImageButton1_Click" CausesValidation="false"/>   
                    </ItemTemplate>
                    <ControlStyle BorderColor="Black" CssClass="BotoDataGrid" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Eliminar" ShowHeader="False">
                     <ItemTemplate>
                         <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Recursos/EliminarProveedor.png" Text="Botón" BorderWidth="2px" OnClick="ImageButton2_Click" />
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

        <asp:LinkButton Text="" ID = "btnPopup" runat="server" />
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" CancelControlID="BtnCancelar" TargetControlID="btnPopup" PopupControlID="PanelPopUp" runat="server"></ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="PanelPopUp" runat="server" style="display:none" CssClass="PanelPEditEmpleado">
    <div class="BordePop">
        <div class="Titulo" >Editar</div>
        <hr />   
        </div>       
        <center>
            <div class ="inline-form">
            <div style="margin-left: 14%"><asp:Label ID="lbNombre" CssClass="LabelsEditEmp" runat="server" Text="Nombre: " Font-Size="Medium" Font-Bold="true"></asp:Label></div>
            <asp:TextBox ID="TxtNombre" runat="server" CssClass="TextosEditEmp" Width="40%" BackColor="White"></asp:TextBox> <asp:RequiredFieldValidator  ValidationGroup="ValidacionEditPopUp"  runat="server" ControlToValidate="TxtNombre" ErrorMessage="• Ingrese nombre" Font-Size="XX-Large" Font-Bold="True" Display="Static" ForeColor="#BC1010">*</asp:RequiredFieldValidator>
            </div>
            <div class ="inline-form">
            <div style="margin-left: 13.5%"><asp:Label ID="lbApellido" runat="server" CssClass="LabelsEditEmp" Font-Bold="true" Font-Size="Medium" Text="Apellido: "></asp:Label></div>
            <asp:TextBox ID="TxtApellido" runat="server" CssClass="TextosEditEmp" Width="40%"></asp:TextBox><asp:RequiredFieldValidator ValidationGroup="ValidacionEditPopUp" runat="server" ControlToValidate="TxtApellido" Font-Size="XX-Large" Font-Bold="True" Display="Static" ForeColor="#BC1010" ErrorMessage="• Ingrese apellido">*</asp:RequiredFieldValidator>
            </div>
            <div class ="inline-form">
            <div style="margin-left: 13%"><asp:Label ID="lbTelefono" CssClass="LabelsEditEmp" runat="server" Text="Telefono:" Font-Size="Medium" Font-Bold="true"></asp:Label></div>
            <asp:TextBox ID="TextTelefono" runat="server" CssClass="TextosEditEmp" Width="40%"></asp:TextBox><asp:RequiredFieldValidator ValidationGroup="ValidacionEditPopUp" runat="server" ControlToValidate="TextTelefono" ErrorMessage="• Ingrese telefono" Font-Size="XX-Large" Font-Bold="True" Display="Static" ForeColor="#BC1010">*</asp:RequiredFieldValidator><asp:RangeValidator ID="RangeValidator2" runat="server" Font-Size="XX-Large" Font-Bold="True" Display="Static" ForeColor="#BC1010" ValidationGroup="ValidacionEditPopUp" ControlToValidate="TextTelefono" ErrorMessage="• Error: solo numeros positivos en este campo" MinimumValue="0" MaximumValue="9999999999999">*</asp:RangeValidator>
            </div>
            <div class ="inline-form">            
            <div style="margin-left: 16%"><asp:Label ID="LbEmail" CssClass="LabelsEditEmp" runat="server" Text="E-mail: " Font-Size="Medium" Font-Bold="true"></asp:Label> </div>
            <asp:TextBox ID="TxtEmail" runat="server" CssClass="TextosEditEmp" Width="40%" TextMode="Email"></asp:TextBox> <asp:RequiredFieldValidator ValidationGroup="ValidacionEditPopUp" runat="server" ControlToValidate="TxtEmail" ErrorMessage="• Ingrese e-mail" Font-Size="XX-Large" Font-Bold="True" Display="Static" ForeColor="#BC1010">*</asp:RequiredFieldValidator>
            </div>
            <div class ="inline-form">
            <div style="margin-left: 12%"><asp:Label ID="LbDireccion" CssClass="LabelsEditEmp" runat="server" Text="Dirección: " Font-Size="Medium" Font-Bold="true"></asp:Label></div>
             <asp:TextBox ID="TxtDirecion" runat="server" CssClass="TextosEditEmp" Width="40%"></asp:TextBox><asp:RequiredFieldValidator  runat="server" ValidationGroup="ValidacionEditPopUp" ControlToValidate="TxtDirecion" ErrorMessage="• Ingrese dirección" Font-Size="XX-Large" Font-Bold="True" Display="Static" ForeColor="#BC1010">*</asp:RequiredFieldValidator>
            </div>
            <div class ="inline-form">
             <div style="margin-left: 19%"><asp:Label ID="LbCuil" CssClass="LabelsEditEmp" runat="server" Text="Cuil: " Font-Size="Medium" Font-Bold="true"></asp:Label></div>
             <asp:TextBox ID="Txtcuil" runat="server" CssClass="TextosEditEmp"  Width="40%" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator runat="server" ValidationGroup="ValidacionEditPopUp" ControlToValidate="Txtcuil" ErrorMessage="• Ingrese cuil" Font-Size="XX-Large" Font-Bold="True" Display="Static" ForeColor="#BC1010">*</asp:RequiredFieldValidator><asp:RangeValidator ID="RangeValidator1" runat="server" Font-Size="XX-Large" Font-Bold="True" Display="Static" ForeColor="#BC1010" ValidationGroup="ValidacionEditPopUp" ControlToValidate="Txtcuil" ErrorMessage="• Error: solo numeros positivos en este campo" MinimumValue="0" MaximumValue="99999999999">*</asp:RangeValidator>
            <div style="position: absolute; top: 457px; left: 502px;"><asp:Label ID="Labelmgg" runat="server" CssClass="Vali2EditEmp" Text="ERROR: Cuil ingresado en otro empleado" Visible ="false"></asp:Label></div>
            </div>
            <div class ="inline-form">
               <div style="margin-left: 15.6%"><asp:Label ID="Label2" CssClass="LabelsEditClient" runat="server" Text="Estado: " Font-Size="Medium"></asp:Label></div>
                <asp:DropDownList CssClass="TextosEditClient" ID="DropDownList2" runat="server" Width="40%">
                    <asp:ListItem Value="activo">Activo</asp:ListItem>
                    <asp:ListItem Value="no activo">No Activo</asp:ListItem>
                </asp:DropDownList>
            </div>
            <hr />
            </center>
            <center>
            <asp:Button ID="BtnAceptar" CssClass="BotonAgregar BtnEditAceptarEditEmp" ValidationGroup="ValidacionEditPopUp" Width="180px" Height="35px" runat="server" Text="Aceptar" OnClick="BtnAceptar_Click" />
             <asp:Button ID="BtnCancelar" CssClass="BotonAgregar BtnEditCancelarEditEmp" Width="180px" Height="35px" runat="server" Text="Cancelar" CausesValidation="false" />
            </center>
            &nbsp;
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
            <asp:Label ID="Label1" runat="server" CssClass="textMensaje1" Text="Se eliminará de manera permanente"></asp:Label>
        </asp:Panel>
</asp:Content>
