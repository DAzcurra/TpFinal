using Controladoras;
using Entidades;
using EntidadesDTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpFinalPracticaProfesional
{
    public partial class EstadoPedido : System.Web.UI.Page
    {
        C_Articulo ControlArticulo;
        C_Cliente ControlCliente;
        C_Pedido ControlPedido;
        C_Configuracion ControlConfig;
        C_DetallePedido ControlDetallePedido;
        C_Facturacion ControlFacturacion;
        DataTable dt = new DataTable();
        static int IdPedido;
        List<PedidoDTO> Lista = new List<PedidoDTO>();
        static List<PedidoDTO> ListaDTO = null;
        List<DetallePedidoDTO> ListaDetalleDTO = null;
        private bool _refreshState;
        private bool _isRefresh;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            ControlFacturacion = (C_Facturacion)Session["ControlFacturacion"];
            ControlCliente = (C_Cliente)Session["ControlCliente"];
            ControlArticulo = (C_Articulo)Session["ControlArticulo"];
            ControlPedido = (C_Pedido)Session["ControlPedido"];
            ControlConfig = (C_Configuracion)Session["ControlConfig"];
            ControlDetallePedido = (C_DetallePedido)Session["ControlDetallePedido"];
            if (!IsPostBack)
            {
                GridPedidos.DataSource = Lista;
                GridPedidos.DataBind();
                ListaDTO = new List<PedidoDTO>();
                ListaDetalleDTO = new List<DetallePedidoDTO>();
                GridPedidos.PageSize = ControlConfig.DevolverNroMaxPaginacion();
                
            }
            if (RadioButtonListBusqueda.SelectedValue == "nro")
            {
                TxtNro.Visible = true;
                BtnCli.Visible = true;
                TxtNro.Focus();
                TxtCli.Visible = false;
                BtnCli.Visible = false;
                TxtDesde.Visible = false;
                TxtHasta.Visible = false;
                BtnRango.Visible = false;
            }
            else
            {
                if (RadioButtonListBusqueda.SelectedValue == "cliente")
                {
                    TxtNro.Visible = false;
                    BtnCli.Visible = false;
                    TxtCli.Visible = true;
                    BtnCli.Visible = true;
                    TxtCli.Focus();
                    TxtDesde.Visible = false;
                    TxtHasta.Visible = false;
                    BtnRango.Visible = false;
                }
                else
                {
                    TxtNro.Visible = false;
                    BtnCli.Visible = false;
                    TxtCli.Visible = false;
                    BtnCli.Visible = false;
                    TxtDesde.Visible = true;
                    TxtHasta.Visible = true;
                    BtnRango.Visible = true;
                    TxtDesde.Focus();
                }
            }

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

        [WebMethod]
        [ScriptMethod]
        public static List<string> GetClientesPedido(string prefixText)
        {
            MySqlConnection cn = new MySqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            String strCn = "Server=localhost; Database= baseProar; Uid=root";
            cn.ConnectionString = strCn;
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ped.pedidoid,ped.fecha,ped.nropedido,cli.nombre,cli.apellido FROM clientes AS cli INNER JOIN pedidos AS ped ON (cli.clienteid=ped.clienteid) WHERE (cli.apellido LIKE @SearchText or cli.nombre LIKE @SearchText)";
            cmd.Parameters.AddWithValue("@SearchText", "%" + prefixText + "%");

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            List<string> txtItems = new List<string>();
            string dbValues;

            foreach (DataRow row in dt.Rows)
            {
                dbValues = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["apellido"].ToString().ToLower() + " " + row["nombre"].ToString().ToLower() + " - " + Convert.ToDateTime(row["fecha"].ToString()).ToString("yyyy/MM/dd") + " - Nro Pedido: " + row["nropedido"].ToString(), Convert.ToString(row["pedidoid"]));
                txtItems.Add(dbValues);
            }

            return txtItems;
        }
        
        public void Refrescar()
        {
            if (RadioButtonListBusqueda.SelectedValue == "nro")
            {
                if (!string.IsNullOrEmpty(TxtNro.Text))
                {
                    ListaDTO = ControlPedido.BuscarNroLista(Convert.ToInt32(TxtNro.Text));
                    if(ListaDTO.Count == 0)
                    {
                        ListaDTO = new List<PedidoDTO>();
                    }
                }

            }
            else
            {
                if (RadioButtonListBusqueda.SelectedValue == "cliente")
                {
                    if(!string.IsNullOrEmpty(TxtCli.Text))
                    {
                        if ((hdnIDCli.Value == null) || (string.IsNullOrEmpty(hdnIDCli.Value)))
                        {
                            ListaDTO = ControlPedido.PedidoPorNombreApellido(TxtCli.Text.ToLower());
                            if (ListaDTO.Count == 0)
                            {
                                ListaDTO = new List<PedidoDTO>();
                            }
                        }
                        else
                        {
                            ListaDTO = ControlPedido.BuscarIDTotalLista(Convert.ToInt32(hdnIDCli.Value));
                            if (ListaDTO.Count == 0)
                            {
                                ListaDTO = new List<PedidoDTO>();
                            }
                        
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(TxtDesde.Text)&& !string.IsNullOrEmpty(TxtHasta.Text))
                    {
                        if (RadioButtonListBusqueda.SelectedValue == "rango")
                        {
                            ListaDTO = ControlPedido.RangoTotal(Convert.ToDateTime(TxtDesde.Text), Convert.ToDateTime(TxtHasta.Text));
                            if (ListaDTO.Count == 0)
                            {
                                ListaDTO = new List<PedidoDTO>();
                            }
                        }
                    }
                }
            }

            if (ListaDTO.Count != 0)
            {
                dt = new DataTable();
                dt.Columns.Add(new DataColumn("pedidoid", System.Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("nropedido", System.Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("fecha", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("entregado", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("clienteid", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("estado", System.Type.GetType("System.String")));

                foreach (PedidoDTO Auxi in ListaDTO)
                {

                    dt.Rows.Add(Auxi.Pedidoid, Auxi.Nropedido, Auxi.Fecha.ToString("yyyy/MM/dd"), Auxi.Entregado,Auxi.Clienteid.Apellido + " " + Auxi.Clienteid.Nombre, Auxi.Estado);
                }
            }
            GridPedidos.DataSource = dt;
            GridPedidos.DataBind();
            //hdnIDCli.Value = null;
        }

        protected void ImageButtonDett_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ImgB = sender as ImageButton;
            GridViewRow gvRow = (GridViewRow)ImgB.NamingContainer;
            ListaDetalleDTO = ControlDetallePedido.DevolverTodoxID(Convert.ToInt32(gvRow.Cells[0].Text));
            dt = new DataTable();
            dt.Columns.Add(new DataColumn("articuloid", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("cantidad", System.Type.GetType("System.Int32")));
            dt.Columns.Add(new DataColumn("preciovendido", System.Type.GetType("System.Double")));
            dt.Columns.Add(new DataColumn("totalxArt", System.Type.GetType("System.Double")));

            foreach (DetallePedidoDTO Auxi in ListaDetalleDTO)
            {

                dt.Rows.Add(Auxi.Articuloid.Nombre + " - " + Auxi.Articuloid.Marca, Auxi.Cantidad, Auxi.Preciovendido.ToString("#,##0.00"), (Auxi.Preciovendido * Auxi.Cantidad).ToString("#,##0.00"));
            }
            GrillaDetalle.DataSource = dt;
            GrillaDetalle.DataBind();
            ModalPopupExtender2.Show();
        }

        protected void ImageButtonElim_Click(object sender, ImageClickEventArgs e)
        {
            
            ImageButton ImgB = sender as ImageButton;
            GridViewRow gvRow = (GridViewRow)ImgB.NamingContainer;
            IdPedido = Convert.ToInt32(gvRow.Cells[0].Text);
            ModalPopupExtender1.Show();
        }

        protected void GridPedidos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridPedidos.PageIndex = e.NewPageIndex;
            Refrescar();
            GridPedidos.PageSize = ControlConfig.DevolverNroMaxPaginacion();
        }

        protected void GridPedidos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string _estado = DataBinder.Eval(e.Row.DataItem, "estado").ToString();
                ImageButton btnFinalizar = (ImageButton)e.Row.FindControl("ImageButtonElim");

                if (_estado == "no activo")
                {
                    e.Row.BackColor = System.Drawing.Color.LightCoral;
                    btnFinalizar.Enabled = false;
                }
                else
                {
                    btnFinalizar.Enabled = true;
                }

            }
        }

        protected void BtnNro_Click(object sender, EventArgs e)
        {
            ListaDTO = null;

            if (IsValid)
            {
                if (RadioButtonListBusqueda.SelectedValue == "nro")
                {
                    if (!_isRefresh)
                    {
                        ListaDTO = ControlPedido.BuscarNroLista(Convert.ToInt32(TxtNro.Text));
                        if (ListaDTO.Count != 0)
                        {
                            Refrescar();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "errorpedido();", true);
                        }
                    }
                    else
                    {
                        Response.Redirect("EstadoPedido.aspx");
                    }
                }
            }
        }

        protected void BtnCli_Click(object sender, EventArgs e)
        {
            ListaDTO = null;
            if (IsValid)
            {
                if (RadioButtonListBusqueda.SelectedValue == "cliente")
                {
                    if ((hdnIDCli.Value == null) || (string.IsNullOrEmpty(hdnIDCli.Value)))
                    {
                        ListaDTO = ControlPedido.PedidoPorNombreApellido(TxtCli.Text.ToLower());
                        if (ListaDTO.Count != 0)
                        {
                            if (!_isRefresh)
                            {
                                Refrescar();
                            }
                            else
                            {
                                Response.Redirect("EstadoPedido.aspx");
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "errorcliente();", true);
                        }
                    }
                    else
                    {
                        if (!_isRefresh)
                        {
                            ListaDTO = ControlPedido.BuscarIDTotalLista(Convert.ToInt32(hdnIDCli.Value));
                            if (ListaDTO.Count != 0)
                            {
                                Refrescar();
                            }
                            //hdnIDCli.Value = null;
                        }
                        else
                        {
                            Response.Redirect("EstadoPedido.aspx");
                        }
                    }
                }
            }
        }

        protected void BtnRango_Click(object sender, EventArgs e)
        {
            ListaDTO = null;

            if (IsValid)
            {
                if (RadioButtonListBusqueda.SelectedValue == "rango")
                {
                    ListaDTO = ControlPedido.RangoTotal(Convert.ToDateTime(TxtDesde.Text), Convert.ToDateTime(TxtHasta.Text));
                    if (ListaDTO.Count != 0)
                    {
                        if (!_isRefresh)
                        {
                            Refrescar();
                        }
                        else
                        {
                            Response.Redirect("EstadoPedido.aspx");
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "errorpedido();", true);
                    }
                }
            }
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (Convert.ToInt64(TxtNro.Text) >= 0)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void GrillaDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GridPedidos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void GridPedidos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (!_isRefresh)
            {
                GridViewRow row = GridPedidos.Rows[e.RowIndex];
                DropDownList ddlEstadoFila = (DropDownList)GridPedidos.Rows[e.RowIndex].FindControl("ddlestado");
                int nro = Convert.ToInt32(row.Cells[0].Text);
                ControlPedido.DeshabilitarHabilitarPedido(nro, ddlEstadoFila.SelectedItem.Text.ToLower());
                GridPedidos.EditIndex = -1;
                Refrescar();
            }
            else
            {
                Response.Redirect("EstadoPedido.aspx");
            }
        }

        protected void GridPedidos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridPedidos.EditIndex = -1;
            Refrescar();
        }

        protected void GridPedidos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridPedidos.EditIndex = e.NewEditIndex;
            Refrescar();
        }

        protected void BtnAceptarEliminar_Click(object sender, EventArgs e)
        {
            if (!_isRefresh)
            {
                if (ControlFacturacion.FacturadoPedido(IdPedido) == false)
                {
                    foreach(DetallePedidoDTO i in ControlDetallePedido.DevolverTodoxID(IdPedido))
                    {
                        ControlArticulo.SumarStock(i.Cantidad,i.Articuloid.Articuloid);
                    }
                    if (ControlPedido.Eliminar(IdPedido))
                    {
                        Refrescar();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "errorelim();", true);

                    }
                    else
                    {
                        Response.Write("<script type='text/javascript'> alert('No se pudo eliminar el cliente !!') </script>");
                    }
                }
                else
                {
                    if (ControlPedido.DeshabilitarHabilitarPedido(IdPedido, "no activo"))
                    {
                        Refrescar();
                       
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "errorDepende();", true);
                    }
                }
            }
            else
            {
                Response.Redirect("EstadoArticulo.aspx");
                IdPedido = 0;
            }
        }
    }
}