<%@ Page Title="" Language="C#" MasterPageFile="~/MasterArticulo.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Stock.aspx.cs" Inherits="TpFinalPracticaProfesional.Stock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenedorPrincipal" runat="server">
    <title>Proar</title>
    <link rel="stylesheet" href="https://cdn.metroui.org.ua/v4/css/metro-all.min.css"/>
    <link rel="stylesheet" type="text/css" href="StyleArticulo.css" />
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:Panel ID="PnlStock" CssClass="PanelPersonalizado scrolling-table-container2" runat="server">
            <br />
            <div class="inline-form" style="left:30%">
            <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Filtrar Articulos: "></asp:Label>
            <asp:TextBox ID="TxtFiltro" placeholder="nombre/marca" AutoPostBack="true" runat="server" Width="30%" OnTextChanged="TxtFiltro_TextChanged"></asp:TextBox>
            </div>
            <hr style="border-color: #6677BC;background-color:#6677BC" />
            <hr style="background-color:#6677BC" />
            <h1>Articulos</h1>
             <p>&nbsp;</p> 
            <hr style="background-color:#6677BC" />
            <asp:GridView ID="TodosArticulos" AutoGenerateColumns="False" CssClass="table table-border cell-border" runat="server" ShowHeaderWhenEmpty="True" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" OnPageIndexChanging="TodosArticulos_PageIndexChanging" OnRowDataBound="TodosArticulos_RowDataBound" PageSize="5">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                  <Columns>
                        <asp:BoundField DataField="articuloid" HeaderText="Cod Articulo" ItemStyle-HorizontalAlign="Center" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" ItemStyle-HorizontalAlign="Center" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripcion" ItemStyle-HorizontalAlign="Center"  >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="marca" HeaderText="Marca" ItemStyle-HorizontalAlign="Center"  >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cantidad" HeaderText="Stock" ItemStyle-HorizontalAlign="Center"  >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombrefantasia" HeaderText="Proveedor" ItemStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Aumentar Stock">
                            <ItemTemplate>
                            <asp:ImageButton ID="ImgBtnAddCantidad" BorderColor="Black" BorderWidth="2px" ImageUrl="~/Recursos/AgregarProveedor.png" runat="server" OnClick="ImgBtnAddCantidad_Click"></asp:ImageButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            
            </asp:GridView>
        </asp:Panel>
    <br />
            <asp:Panel ID="PnlCritico" CssClass="PanelPersonalizado scrolling-table-container2" runat="server">
               <h1>Stock Crítico</h1>
                <p>&nbsp;</p> 
                <hr style="background-color:#6677BC" />
                <asp:GridView ID="GridCritico" AutoGenerateColumns="False" CssClass="table table-border cell-border" runat="server" CellPadding="4" ShowHeaderWhenEmpty="true" AllowPaging="True" OnPageIndexChanging="GridCritico_PageIndexChanging" OnRowDataBound="GridCritico_RowDataBound" PageSize="5" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px">
                      <Columns>
                        <asp:BoundField DataField="articuloid" HeaderText="Cod Articulo" ItemStyle-HorizontalAlign="Center" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" ItemStyle-HorizontalAlign="Center" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripcion" ItemStyle-HorizontalAlign="Center"  >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="marca" HeaderText="Marca" ItemStyle-HorizontalAlign="Center"  >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cantidad" HeaderText="Stock" ItemStyle-HorizontalAlign="Center"  >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombrefantasia" HeaderText="Proveedor" ItemStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                       <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                       <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                       <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                       <RowStyle BackColor="White" ForeColor="#330099" />
                       <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                       <SortedAscendingCellStyle BackColor="#FEFCEB" />
                       <SortedAscendingHeaderStyle BackColor="#AF0101" />
                       <SortedDescendingCellStyle BackColor="#F6F0C0" />
                       <SortedDescendingHeaderStyle BackColor="#7E0000" />
                   </asp:GridView>
                <br />
            </asp:Panel>
<br />

    <asp:LinkButton Text="" ID="LkBCantidad" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" CancelControlID="" TargetControlID="LkBCantidad" PopupControlID="PanelCantidad" runat="server"></ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelCantidad" Style="display: none" Height="200px" Width="300px" BorderColor="Black" BorderWidth="2px" BorderStyle="Double" BackColor="#FAFAE7" runat="server" HorizontalAlign="Center">
        <div style="position: absolute; top: 28px; left: -105px;">
            <asp:Label ID="Label9" CssClass="Labels" runat="server" Text="Cantidad: "></asp:Label>
        </div>
        <div style="position: absolute; top: 66px; left: 65px; text-align: center;">
            <asp:TextBox ID="TextCant" Text="1" CssClass="Textos" runat="server" Width="150px" TextMode="Number"></asp:TextBox>
            <div style="position: absolute">
                <asp:RangeValidator ID="RangeValidator1" CssClass="ValiAgregarGridCant" runat="server" Display="Static" ControlToValidate="TextCant" ErrorMessage=" Error: solo numeros positivos en este campo" MinimumValue="0" MaximumValue="9999999999999">Error: solo numeros positivos en este campo</asp:RangeValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass=" ValiAgregarGridCant" ControlToValidate="TextCant" runat="server" ErrorMessage="Campo vacio: ingrese cantidad">Campo vacio: ingrese cantidad</asp:RequiredFieldValidator>
            </div>
        </div>
        <asp:Button ID="Button2" CssClass="Boton CantB" runat="server" Width="125px" Text="Aceptar" OnClick="Button2_Click" />
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" CssClass="Boton CantCancelarB" Text="Cancelar" Width="125px" />
    </asp:Panel>
</asp:Content>
