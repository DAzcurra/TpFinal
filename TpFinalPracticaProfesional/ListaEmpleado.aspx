<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListaEmpleado.aspx.cs" Inherits="TpFinalPracticaProfesional.ListaEmpleado" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    
           <script type="text/javascript">
        function erroralert() {
            swal({
                title: '',
                text: 'Lista de empleados vacía ',
                icon: 'error'
            });
            }
            </script><title>Proar</title>
    <link rel="stylesheet" href="https://cdn.metroui.org.ua/v4/css/metro-all.min.css"/>
    <link rel="stylesheet" type="text/css" href="StyleCliente.css" />
</head>
 
<body>

    <form id="form1" runat="server">
        <div>
            <ul class="sidenav-m3">
              <li class="title"><H2>PROAR</H2></li>
              <li>
                 <asp:LinkButton class="dropdown-toggle" ID="LBCliente"  runat="server" Text="Empleado" Width="220px" ></asp:LinkButton> </li>
                    <li ><asp:LinkButton ID="BotonAgregar" runat="server" Text="Agregar" Width="220px" CausesValidation="false" OnClick="BotonAgregar_Click"/></li>
                    <li ><asp:LinkButton ID="BotonEditar" runat="server" Text="Editar/Eliminar" Width="220px" CausesValidation="false" OnClick="BotonEditar_Click" /></li>
                    <li ><asp:LinkButton ID="BotonListar" runat="server" Text="Listar" Width="220px" CausesValidation="false" OnClick="BotonListar_Click"></asp:LinkButton></li>
            
             <li ><asp:LinkButton ID="LBAtras" runat="server" Text="Volver al Menu" Width="220px" OnClick="LBAtras_Click" CausesValidation="false"></asp:LinkButton></li>
          </ul>
        </div>
        <div id="ContenedorCentral">
            <asp:Panel ID="Panel1" CssClass="PanelPersonalizado scrolling-table-container" runat="server">
            <h1>Lista de Empleados</h1>
            <p>&nbsp;</p>                
                <asp:Button ID="BtnConvtWord" CssClass="BotonWordEmpleado BWord" runat="server" Text="" BackColor="#284775" OnClick="BtnConvtWord_Click" Height="50px" Width="50px" ToolTip="Exportar a Word" />
                <asp:Button ID="BtnConvtExcel" CssClass="BotonExcelEmpleado BExcel" runat="server" Text="" BackColor="#284775" OnClick="BtnConvtExcel_Click" Height="50px" Width="50px" ToolTip="Exportar a Excel"  />
                <asp:Button ID="BtnConvtPDF" CssClass="BotonPDFEmpleado BPDF" runat="server" Text="" BackColor="#284775" OnClick="BtnConvtPDF_Click" Height="50px" Width="50px" ToolTip="Exportar a PDF" />
                <asp:GridView ID="TodosEmpleados" CssClass="table table-border cell-border" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" OnPageIndexChanging="TodosEmpleados_PageIndexChanging" ShowHeaderWhenEmpty="true" OnRowDataBound="TodosEmpleados_RowDataBound">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" ItemStyle-HorizontalAlign="Center" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="apellido" HeaderText="Apellido" ItemStyle-HorizontalAlign="Center" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="telefono" HeaderText="Telefono" ItemStyle-HorizontalAlign="Center" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="email" HeaderText="E-mail" ItemStyle-HorizontalAlign="Center" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="direccion" HeaderText="Dirección" ItemStyle-HorizontalAlign="Center" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cuil" HeaderText="Cuil" ItemStyle-HorizontalAlign="Center" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="estado" HeaderText="Estado" ItemStyle-HorizontalAlign="Center" >
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
    &nbsp;&nbsp;&nbsp;
        <center>
         <asp:Label ID="Label1" CssClass="LabelsSombra" runat="server" Text="No hay empleados ingresados" Visible="False" Font-Bold="True" Font-Size="XX-Large" ForeColor="Red" BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" ></asp:Label>
        </center>
        &nbsp;
    </asp:Panel>

        </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.3.1.min.js"></script>
    <script src="https://cdn.metroui.org.ua/v4/js/metro.min.js"></script>
</body>
</html>
