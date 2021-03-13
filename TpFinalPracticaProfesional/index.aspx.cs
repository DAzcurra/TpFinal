using Controladoras;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpFinalPracticaProfesional
{
    public partial class index : System.Web.UI.Page
    {
        C_Configuracion ControlConfig;
        C_Articulo ControlArticulo;
        DataTable dt = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            ControlConfig = (C_Configuracion)Session["ControlConfig"];

            ControlArticulo = (C_Articulo)Session["ControlArticulo"];
            List<Articulo> Lista = ControlArticulo.ListarStockBajo();
            if(Lista.Count != 0)
            {

                    dt = new DataTable();
                    dt.Columns.Add(new DataColumn("articuloid", System.Type.GetType("System.Int32")));
                    dt.Columns.Add(new DataColumn("nombre", System.Type.GetType("System.String")));
                    dt.Columns.Add(new DataColumn("descripcion", System.Type.GetType("System.String")));
                    dt.Columns.Add(new DataColumn("marca", System.Type.GetType("System.String")));
                    dt.Columns.Add(new DataColumn("cantidad", System.Type.GetType("System.Int32")));
                   

                    foreach (var Auxi in Lista)
                    {
                        dt.Rows.Add(Auxi.Articuloid, Auxi.Nombre, Auxi.Descripcion, Auxi.Marca, Auxi.Cantidad);

                    }
                    GridStock.DataSource = dt;
                    GridStock.DataBind();
                if(Session["StockMin"] == null)
                { 
                    ModalPopupExtenderUno.Show();
                    Session["StockMin"] = "1";
                }
            }
            BtnSalir.Attributes.Add("onmouseover", "this.style.color = 'white'");
            BtnSalir.Attributes.Add("onmouseout", "this.style.color = 'red'");
            LBtnClientes.Attributes.Add("onmouseover", "this.style.color = 'black'");
            LBtnClientes.Attributes.Add("onmouseout", "this.style.color = 'white'");
            LBtnUsuario.Attributes.Add("onmouseover", "this.style.color = 'black'");
            LBtnUsuario.Attributes.Add("onmouseout", "this.style.color = 'white'");
            LBtnManual.Attributes.Add("onmouseover", "this.style.color = 'black'");
            LBtnManual.Attributes.Add("onmouseout", "this.style.color = 'white'");
            LBtnArticulos.Attributes.Add("onmouseover", "this.style.color = 'black'");
            LBtnArticulos.Attributes.Add("onmouseout", "this.style.color = 'white'");
            LBtnEmpleados.Attributes.Add("onmouseover", "this.style.color = 'black'");
            LBtnEmpleados.Attributes.Add("onmouseout", "this.style.color = 'white'");
            LBtnFacturacion.Attributes.Add("onmouseover", "this.style.color = 'black'");
            LBtnFacturacion.Attributes.Add("onmouseout", "this.style.color = 'white'");
            LBtnPedidos.Attributes.Add("onmouseover", "this.style.color = 'black'");
            LBtnPedidos.Attributes.Add("onmouseout", "this.style.color = 'white'");
            LBtnProveedores.Attributes.Add("onmouseover", "this.style.color = 'black'");
            LBtnProveedores.Attributes.Add("onmouseout", "this.style.color = 'white'");

            if (!IsPostBack)
            {
                GridStock.PageSize = ControlConfig.DevolverNroMaxPaginacion();
                GridStock.DataSource = ControlArticulo.ListarStockBajo();
                GridStock.DataBind();
            }

        }

        protected void LBtnClientes_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListarCliente.aspx");
        }

        protected void LBtnEmpleados_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListaEmpleado.aspx");
        }

        protected void LBtnProveedores_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListarProveedor.aspx");
        }

        protected void LBtnPedidos_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListarPedido.aspx");
        }

        protected void LBtnArticulos_Click(object sender, EventArgs e)
        {
            Response.Redirect("Stock.aspx");
        }

        protected void LBtnFacturacion_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListarFactura.aspx");
        }

        protected void GridStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ModalPopupExtenderUno.Show();
            GridStock.PageIndex = e.NewPageIndex;
            GridStock.PageSize = ControlConfig.DevolverNroMaxPaginacion();
            GridStock.DataSource = ControlArticulo.ListarStockBajo();
            GridStock.DataBind();
        }

        protected void LBtnUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("MenuUsuario.aspx");
        }

        protected void BtnSalir_Click(object sender, EventArgs e)
        {
            Session["Login"] = null;
            Response.Redirect("Login.aspx");
        }
    }
}