using Controladoras;
using EntidadesDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;


namespace TpFinalPracticaProfesional
{
    public partial class Stock : System.Web.UI.Page
    {
        C_Articulo ControlArticulo;
        C_Configuracion ControlConfig;
        DataTable dt = null;
        DataTable dt2 =null;
        List<ArticuloxProveedorDTO> ListaDTO = new List<ArticuloxProveedorDTO>();
        private bool _refreshState;
        private bool _isRefresh;
        static int Artid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            ControlArticulo = (C_Articulo)Session["ControlArticulo"];
            ControlConfig = (C_Configuracion)Session["ControlConfig"];
            if(!IsPostBack)
            {
                dt = new DataTable();
                dt2 = new DataTable();
                TodosArticulos.PageSize = ControlConfig.DevolverNroMaxPaginacion();
                TodosArticulos.DataSource = dt;
                TodosArticulos.DataBind();
                GridCritico.PageSize = ControlConfig.DevolverNroMaxPaginacion();
                GridCritico.DataSource = dt2;
                GridCritico.DataBind();

                GridCritico.DataSource = ListaDTO;
                GridCritico.DataBind();

                TodosArticulos.DataSource = ListaDTO;
                TodosArticulos.DataBind();

                RefrescarArticulos();
                RefrescarCritico();
            }
            RefrescarArticulos2(TxtFiltro.Text);
        }

        protected override void LoadViewState(object savedState)
        {
            object[] AllStates = (object[])savedState;
            base.LoadViewState(AllStates[0]);
            _refreshState = bool.Parse(AllStates[1].ToString());
            _isRefresh = _refreshState == bool.Parse(Session["__ISREFRESH"].ToString());
        }

        protected override object SaveViewState()
        {
            Session["__ISREFRESH"] = _refreshState;
            object[] AllStates = new object[2];
            AllStates[0] = base.SaveViewState();
            AllStates[1] = !(_refreshState);
            return AllStates;
        }
        protected void TodosArticulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            TodosArticulos.PageIndex = e.NewPageIndex;
            TodosArticulos.DataSource = dt;
            TodosArticulos.DataBind();
            TodosArticulos.PageSize = ControlConfig.DevolverNroMaxPaginacion();
        }

        protected void TodosArticulos_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GridCritico_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridCritico.PageIndex = e.NewPageIndex;
            GridCritico.DataSource = dt2;
            GridCritico.DataBind();
            GridCritico.PageSize = ControlConfig.DevolverNroMaxPaginacion();
        }

        protected void GridCritico_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        public void RefrescarArticulos()
        {
            if (ControlArticulo.ListarActivos().Count != 0)
            {
                dt = new DataTable();
                dt.Columns.Add(new DataColumn("articuloid", System.Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("nombre", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("descripcion", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("marca", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("cantidad", System.Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("nombrefantasia", System.Type.GetType("System.String")));
                foreach (ArticuloxProveedorDTO Auxi in ControlArticulo.Listar())
                {
                    dt.Rows.Add(Auxi.Articuloid.Articuloid, Auxi.Articuloid.Nombre, Auxi.Articuloid.Descripcion, Auxi.Articuloid.Marca, Auxi.Articuloid.Cantidad, Auxi.Proveedorid.Nombrefantasia);
                }
                TodosArticulos.DataSource = dt;
                TodosArticulos.DataBind();
            }
        }

        public void RefrescarArticulos2(string TxtFiltro)
        {
            if (ControlArticulo.PruebaFiltrar(TxtFiltro).Count != 0)
            {
                dt = new DataTable();
                dt.Columns.Add(new DataColumn("articuloid", System.Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("nombre", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("descripcion", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("marca", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("cantidad", System.Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("nombrefantasia", System.Type.GetType("System.String")));
                foreach (ArticuloxProveedorDTO Auxi in ControlArticulo.PruebaFiltrar(TxtFiltro))
                {
                    dt.Rows.Add(Auxi.Articuloid.Articuloid, Auxi.Articuloid.Nombre, Auxi.Articuloid.Descripcion, Auxi.Articuloid.Marca, Auxi.Articuloid.Cantidad, Auxi.Proveedorid.Nombrefantasia);

                }
                TodosArticulos.DataSource = dt;
                TodosArticulos.DataBind();
            }
        }
        public void RefrescarCritico()
            {
                if (ControlArticulo.ListarXProveedorStockBajo().Count != 0)
                {
                        dt2 = new DataTable();
                        dt2.Columns.Add(new DataColumn("articuloid", System.Type.GetType("System.Int32")));
                        dt2.Columns.Add(new DataColumn("nombre", System.Type.GetType("System.String")));
                        dt2.Columns.Add(new DataColumn("descripcion", System.Type.GetType("System.String")));
                        dt2.Columns.Add(new DataColumn("marca", System.Type.GetType("System.String")));
                        dt2.Columns.Add(new DataColumn("cantidad", System.Type.GetType("System.Int32")));
                        dt2.Columns.Add(new DataColumn("nombrefantasia", System.Type.GetType("System.String")));
                        foreach (ArticuloxProveedorDTO Auxi in ControlArticulo.ListarXProveedorStockBajo())
                        {
                            dt2.Rows.Add(Auxi.Articuloid.Articuloid, Auxi.Articuloid.Nombre, Auxi.Articuloid.Descripcion, Auxi.Articuloid.Marca, Auxi.Articuloid.Cantidad, Auxi.Proveedorid.Nombrefantasia);
                        }
                        GridCritico.DataSource = dt2;
                        GridCritico.DataBind();
                }
                else
                {
                    GridCritico.DataSource = dt2;
                    GridCritico.DataBind();
                    GridCritico.DataSource = ListaDTO;
                    GridCritico.DataBind();
                }
            }

        protected void TxtFiltro_TextChanged(object sender, EventArgs e)
        {
            if (!_isRefresh)
            {
                TodosArticulos.DataSource = ListaDTO;
                TodosArticulos.DataBind();
                if (ControlArticulo.PruebaFiltrar(TxtFiltro.Text).Count !=0)
                {
                    dt = new DataTable();
                    dt.Columns.Add(new DataColumn("articuloid", System.Type.GetType("System.Int32")));
                    dt.Columns.Add(new DataColumn("nombre", System.Type.GetType("System.String")));
                    dt.Columns.Add(new DataColumn("descripcion", System.Type.GetType("System.String")));
                    dt.Columns.Add(new DataColumn("marca", System.Type.GetType("System.String")));
                    dt.Columns.Add(new DataColumn("cantidad", System.Type.GetType("System.Int32")));
                    dt.Columns.Add(new DataColumn("nombrefantasia", System.Type.GetType("System.String")));
                    foreach (ArticuloxProveedorDTO Auxi in ControlArticulo.PruebaFiltrar(TxtFiltro.Text))
                    {
                        dt.Rows.Add(Auxi.Articuloid.Articuloid, Auxi.Articuloid.Nombre, Auxi.Articuloid.Descripcion, Auxi.Articuloid.Marca, Auxi.Articuloid.Cantidad, Auxi.Proveedorid.Nombrefantasia);

                    }
                    TodosArticulos.DataSource = dt;
                    TodosArticulos.DataBind();
                }
                else
                {
                    RefrescarArticulos();
                }
            }
            else
            {
                Response.Redirect("Stock.aspx");
            }
        }

        protected void ImgBtnAddCantidad_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ImgB = sender as ImageButton;
            GridViewRow gvRow = (GridViewRow)ImgB.NamingContainer;
            Artid = Convert.ToInt32(gvRow.Cells[0].Text);
            ModalPopupExtender3.Show();

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (!_isRefresh)
            {

                if (ControlArticulo.SumarStock(Convert.ToInt32(TextCant.Text),Artid))
                {
                    TodosArticulos.DataSource = dt;
                    TodosArticulos.DataBind();
                    RefrescarArticulos2(TxtFiltro.Text);
                    RefrescarCritico();
                    TextCant.Text = "1";
                }

            }
            else
            {
                TextCant.Text = "1";
                Response.Redirect("Stock.aspx");
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            TextCant.Text = "1";
            ModalPopupExtender3.Hide();
        }
    }
}