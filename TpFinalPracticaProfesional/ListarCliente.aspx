<%@ Page Title="" Language="C#" MasterPageFile="~/MasterCliente.Master" AutoEventWireup="true" CodeBehind="~/ListarCliente.aspx.cs" Inherits="TpFinalPracticaProfesional.ListarCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenedorPrincipal" runat="server">
            <script type="text/javascript">
        function erroralert() {
            swal({
                title: '',
                text: 'Lista de clientes vacía ',
                icon: 'error'
            });
            }
            </script>
        <title>Proar</title>
    <link rel="stylesheet" href="https://cdn.metroui.org.ua/v4/css/metro-all.min.css"/>
    <link rel="stylesheet" type="text/css" href="StyleCliente.css" />
    <asp:Panel ID="Panel1" CssClass="PanelPersonalizado scrolling-table-container" runat="server">
    <h1>Lista de Clientes</h1>
    <p>&nbsp;</p>
        <asp:Button ID="BtnConvtWord" CssClass="BotonWord BWord" runat="server" Text="" BackColor="#284775" OnClick="BtnConvtWord_Click" Height="50px" Width="50px" ToolTip="Exportar a Word" />
        <asp:Button ID="BtnConvtExcel" CssClass="BotonExcel BExcel" runat="server" Text="" BackColor="#284775" OnClick="BtnConvtExcel_Click" Height="50px" Width="50px" ToolTip="Exportar a Excel"  />
        <asp:Button ID="BtnConvtPDF" CssClass="BotonPDF BPDF" runat="server" Text="" BackColor="#284775" OnClick="BtnConvtPDF_Click" Height="50px" Width="50px" ToolTip="Exportar a PDF"  />
        <asp:GridView ID="TodosClientes" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" CssClass="table table-border cell-border" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="TodosClientes_RowDataBound" AllowPaging="True" OnPageIndexChanging="TodosClientes_PageIndexChanging">

            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="clienteid" HeaderText="Nro Cliente" ItemStyle-HorizontalAlign="Center" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="razonsocial" HeaderText="Razón Social" ItemStyle-HorizontalAlign="Center" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="nombre" HeaderText="Nombre"  ItemStyle-HorizontalAlign="Center" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="apellido" HeaderText="Apellido"  ItemStyle-HorizontalAlign="Center" >
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
                <asp:BoundField DataField="tipo" HeaderText="Tipo de Cliente" ItemStyle-HorizontalAlign="Center" >
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
            <asp:Label ID="Label1" CssClass="LabelsSombra" runat="server" Text="No hay clientes ingresados" Visible="False" Font-Bold="True" Font-Size="XX-Large" ForeColor="Red" BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" ></asp:Label>
        </center>
        &nbsp;
    </asp:Panel>
</asp:Content>
