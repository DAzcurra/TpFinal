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
    public partial class EditarPedido : System.Web.UI.Page
    {
        double AuxTotal = 0;
        Pedido AuxPedido = null;
        private bool _refreshState;
        private bool _isRefresh;
        List<Articulo> Lista = new List<Articulo>();
        List<PedidoDTO> ListaDTO = null;
        static List<DetallePedidoDTO> ListaDetalleDTO = null;
        C_Articulo ControlArticulo;
        C_Cliente ControlCliente;
        C_Pedido ControlPedido;
        C_Configuracion ControlConfig;
        C_DetallePedido ControlDetallePedido;
        DataTable dt = null;
        static Articulo ArticuloId = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            ControlCliente = (C_Cliente)Session["ControlCliente"];
            ControlArticulo = (C_Articulo)Session["ControlArticulo"];
            ControlPedido = (C_Pedido)Session["ControlPedido"];
            ControlConfig = (C_Configuracion)Session["ControlConfig"];
            ControlDetallePedido = (C_DetallePedido)Session["ControlDetallePedido"];

            if (!IsPostBack)
            {
                GrillaArticulos.DataSource = Lista;
                GrillaArticulos.DataBind();
                ListaDetalleDTO = new List<DetallePedidoDTO>();
                ListaDTO = new List<PedidoDTO>();
                BusquedaClientePedido.PageSize = ControlConfig.DevolverNroMaxPaginacion();
                RefrescarTablaNombreApellido();
            }

            if (RadioButtonListBusqueda.SelectedValue == "nro")
            {
                TxtNro.Visible = true;
                BtnCli.Visible = true;
                TxtCli.Visible = false;
                BtnCli.Visible = false;
                TxtDesde.Visible = false;
                TxtHasta.Visible = false;
                BtnRango.Visible = false;
                TxtNro.Focus();
            }
            else
            {
                if (RadioButtonListBusqueda.SelectedValue == "cliente")
                {
                    TxtNro.Visible = false;
                    BtnCli.Visible = false;
                    TxtCli.Visible = true;
                    BtnCli.Visible = true;
                    TxtDesde.Visible = false;
                    TxtHasta.Visible = false;
                    BtnRango.Visible = false;
                    TxtCli.Focus();
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

        protected void BtnNro_Click(object sender, EventArgs e)
        {
            List<Articulo> ListaAux = new List<Articulo>();
            if (IsValid)
            {
                if (RadioButtonListBusqueda.SelectedValue == "nro")
                {
                    if (!_isRefresh)
                    {
                        AuxPedido = ControlPedido.BuscarNroPedidoEstadoEntregado(Convert.ToInt32(TxtNro.Text));
                        if (AuxPedido != null)
                        {
                            Cliente cli = ControlCliente.BuscarID(AuxPedido.Clienteid);
                            TxtNombre.Text = cli.Apellido + " " + cli.Nombre;
                            txtnropedidomostrar.Text = Convert.ToString(AuxPedido.Nropedido);
                            txtfechamostrar.Text = Convert.ToString(AuxPedido.Fecha.ToString("yyyy/MM/dd"));
                            ListaDetalleDTO = ControlDetallePedido.DevolverTodoxID(AuxPedido.Pedidoid);
                            RefrescarTabla();
                            BtnGuardar.Enabled = true;
                            BtnBuscarNombre.Enabled = true;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "errorpedido();", true);
                        }
                    }
                    else
                    {
                        Response.Redirect("EditarPedido.aspx");
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
                                RefrescarTablaNombreApellido();
                                ModalPopupExtenderUno.Show();
                            }
                            else
                            {
                                Response.Redirect("EditarPedido.aspx");
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
                            AuxPedido = ControlPedido.BuscarID(Convert.ToInt32(hdnIDCli.Value));
                            if (AuxPedido != null)
                            {
                                hdnIDCli.Value = null;
                                Cliente cli = ControlCliente.BuscarID(AuxPedido.Clienteid);
                                TxtNombre.Text = cli.Apellido + " " + cli.Nombre;
                                txtnropedidomostrar.Text = Convert.ToString(AuxPedido.Nropedido);
                                txtfechamostrar.Text = Convert.ToString(AuxPedido.Fecha.ToString("yyyy/MM/dd"));
                                ListaDetalleDTO = ControlDetallePedido.DevolverTodoxID(AuxPedido.Pedidoid);
                                RefrescarTabla();
                                BtnGuardar.Enabled = true;
                                BtnBuscarNombre.Enabled = true;
                            }
                        }
                        else
                        {
                            Response.Redirect("EditarPedido.aspx");
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
                    ListaDTO = ControlPedido.Rango(Convert.ToDateTime(TxtDesde.Text), Convert.ToDateTime(TxtHasta.Text));
                    if (ListaDTO.Count != 0)
                    {
                        if (!_isRefresh)
                        {
                            RefrescarTablaNombreApellido();
                            ModalPopupExtenderUno.Show();
                        }
                        else
                        {
                            Response.Redirect("EditarPedido.aspx");
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

        protected void GrillaArticulos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (!_isRefresh)
            {
                ListaDetalleDTO.RemoveAt(e.RowIndex);
                RefrescarTabla();
                if (ListaDetalleDTO.Count == 0)
                {
                    GrillaArticulos.DataSource = Lista;
                    GrillaArticulos.DataBind();
                    ListaDetalleDTO = new List<DetallePedidoDTO>();
                }
            }
            else
            {
                Response.Redirect("EditarPedido.aspx");
            }
        }

        protected void GrillaArticulos_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    AuxTotal += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "totalxArt"));
                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[4].Text = "Total: $";
                    e.Row.Cells[5].Text = Convert.ToDouble(AuxTotal).ToString("#,##0.00");
                    e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Font.Bold = true;
                }
            }
            catch (Exception err)
            {
                string error = err.Message.ToString() + " - " + err.Source.ToString();
            }
        }

        protected void GrillaArticulos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GrillaArticulos.EditIndex = e.NewEditIndex;
            RefrescarTabla();
        }

        protected void GrillaArticulos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (!_isRefresh)
            {
                GridViewRow row = GrillaArticulos.Rows[e.RowIndex];
                TextBox Txtc = (TextBox)row.Cells[4].Controls[0];
                Txtc.Focus();
                int CodArt = Convert.ToInt32(row.Cells[0].Text);
                int nropedido = Convert.ToInt32(txtnropedidomostrar.Text);

                Articulo ArticuloAux = ControlArticulo.BuscarXId(CodArt);
                if (ArticuloAux != null)
                {
                    if ((!string.IsNullOrEmpty(Txtc.Text)))
                    {
                        if (Int32.TryParse(Txtc.Text, out int result))
                        {
                            if (Convert.ToInt32(Txtc.Text) > 0)
                            {

                                DetallePedidoDTO AuxiDetalle = ListaDetalleDTO[e.RowIndex];

                                if (AuxiDetalle.Cantidad > Convert.ToInt32(Txtc.Text))//cantidad sdisminuye
                                {
                                    AuxiDetalle.Cantidad = Convert.ToInt32(Txtc.Text);
                                    GrillaArticulos.EditIndex = -1;
                                    RefrescarTabla();
                                }
                                else
                                {
                                    int cant = Convert.ToInt32(Txtc.Text) - AuxiDetalle.Cantidad;

                                    if (StockInsuficiente(ArticuloAux, cant))
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "errorStockInsu();", true);
                                    }
                                    else
                                    {
                                        AuxiDetalle.Cantidad = AuxiDetalle.Cantidad + cant;
                                        GrillaArticulos.EditIndex = -1;
                                        RefrescarTabla();
                                    }
                                }

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "errornega();", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "errornvalorinco();", true);
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "errorvacio();", true);

                    }
                }
            }
            else
            {
                Response.Redirect("EditarPedido.aspx");
            }
        }

        public bool StockInsuficiente(Articulo aux, int cant)
        {
            bool Cant = false;
            int Stock = 0;
            Stock = ControlArticulo.ObtenerCantidad(aux);
            if (Stock < Convert.ToInt32(cant))
            {
                Cant = true;
            }
            return Cant;
        }

        protected void GrillaArticulos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GrillaArticulos.EditIndex = -1;
            RefrescarTabla();
        }

        public void RefrescarTabla()
        {
            if (ListaDetalleDTO.Count != 0)
            {
                dt = new DataTable();
                dt.Columns.Add(new DataColumn("articuloid", System.Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("nombre", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("descripcion", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("marca", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("cantidad", System.Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("precioactual", System.Type.GetType("System.Double")));
                dt.Columns.Add(new DataColumn("totalxArt", System.Type.GetType("System.Double")));

                foreach (var Auxi in ListaDetalleDTO)
                {
                    dt.Rows.Add(Auxi.Articuloid.Articuloid, Auxi.Articuloid.Nombre, Auxi.Articuloid.Descripcion, Auxi.Articuloid.Marca, Auxi.Cantidad, Auxi.Preciovendido.ToString("#,##0.00"), (Auxi.Preciovendido * Auxi.Cantidad).ToString("#,##0.00"));

                }
                GrillaArticulos.DataSource = dt;
                GrillaArticulos.DataBind();
            }
        }

        public void RefrescarTablaNombreApellido()
        {
            if (ListaDTO.Count != 0)
            {
                dt = new DataTable();
                dt.Columns.Add(new DataColumn("clienteid", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("fecha", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("nropedido", System.Type.GetType("System.String")));

                foreach (var Auxi in ListaDTO)
                {
                    dt.Rows.Add(Auxi.Clienteid.Apellido + " " + Auxi.Clienteid.Nombre, Auxi.Fecha.ToString("yyy/MM/dd"), Auxi.Nropedido);

                }
                BusquedaClientePedido.DataSource = dt;
                BusquedaClientePedido.DataBind();
            }
        }

        protected void GrillaArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {

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
            cmd.CommandText = "SELECT ped.pedidoid,ped.fecha,ped.nropedido,cli.nombre,cli.apellido FROM clientes AS cli INNER JOIN pedidos AS ped ON (cli.clienteid=ped.clienteid) WHERE (cli.apellido LIKE @SearchText or cli.nombre LIKE @SearchText) and ped.estado = 'activo' and ped.entregado = 'en espera'";
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

        protected void ImageButtonP_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ImgB = sender as ImageButton;
            GridViewRow gvRow = (GridViewRow)ImgB.NamingContainer;
            AuxPedido = ControlPedido.BuscarNro(Convert.ToInt32(gvRow.Cells[2].Text));
            if (AuxPedido != null)
            {
                Cliente cli = ControlCliente.BuscarID(AuxPedido.Clienteid);
                TxtNombre.Text = cli.Apellido + " " + cli.Nombre;
                txtnropedidomostrar.Text = Convert.ToString(AuxPedido.Nropedido);
                txtfechamostrar.Text = Convert.ToString(AuxPedido.Fecha.ToString("yyyy/MM/dd"));
                ListaDetalleDTO = ControlDetallePedido.DevolverTodoxID(AuxPedido.Pedidoid);
                RefrescarTabla();
                BtnGuardar.Enabled = true;
                BtnBuscarNombre.Enabled = true;
                TextCant.Focus();
            }
        }

        protected void BusquedaClientePedido_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BusquedaClientePedido.PageIndex = e.NewPageIndex;
            RefrescarTablaNombreApellido();
            BusquedaClientePedido.PageSize = ControlConfig.DevolverNroMaxPaginacion();
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            bool Existe = false;
            if (!_isRefresh)
            {
                List<DetallePedidoDTO> ListaBase = ControlDetallePedido.DevolverTodoxID(Convert.ToInt32(txtnropedidomostrar.Text));

                if (ListaDetalleDTO.Count != 0)
                {
                    int i = 0;
                    int x = 0;

                    foreach (DetallePedidoDTO Det in ControlDetallePedido.DevolverTodoxID(Convert.ToInt32(txtnropedidomostrar.Text)))
                    {

                        for (int y = 1; y <= ListaDetalleDTO.Count; y++)
                        {
                            if (Det.Articuloid.Articuloid == ListaDetalleDTO[x].Articuloid.Articuloid)
                            {
                                Existe = true;
                            }
                            x++;
                        }

                        if (Existe == false)
                        {
                            ControlDetallePedido.Eliminar(Det.Pedidoid.Pedidoid, Det.Articuloid.Articuloid);
                            ControlArticulo.SumarStock(Det.Cantidad, Det.Articuloid.Articuloid);

                        }
                        Existe = false;
                        x = 0;
                    }


                    foreach (DetallePedidoDTO dett in ListaDetalleDTO)
                    {
                        if(ControlDetallePedido.PerteneceAPedido(Convert.ToInt32(txtnropedidomostrar.Text),dett.Articuloid.Articuloid)==false)
                        {
                            ControlDetallePedido.Agregar(dett);
                            ControlArticulo.RestarStock(dett.Cantidad, dett.Articuloid.Articuloid);
                        }
                    }

                    foreach (DetallePedidoDTO Det in ControlDetallePedido.DevolverTodoxID(Convert.ToInt32(txtnropedidomostrar.Text)))
                    {
                        for (int y = 1; y <= ListaDetalleDTO.Count; y++)
                        {
                            if ((Det.Articuloid.Articuloid == ListaDetalleDTO[i].Articuloid.Articuloid))
                            {
                                if (Det.Cantidad > ListaDetalleDTO[i].Cantidad)
                                {
                                    int Restar = Det.Cantidad - ListaDetalleDTO[i].Cantidad;
                                    ControlArticulo.SumarStock(Restar, Det.Articuloid.Articuloid);
                                    ControlDetallePedido.ModificarCant(Det.Pedidoid.Pedidoid, Det.Articuloid.Articuloid, ListaDetalleDTO[i].Cantidad);
                                }
                                else
                                {
                                    if (Det.Cantidad < ListaDetalleDTO[i].Cantidad)
                                    {
                                        int Sumar = ListaDetalleDTO[i].Cantidad - Det.Cantidad;
                                        ControlArticulo.RestarStock(Sumar, Det.Articuloid.Articuloid);
                                        ControlDetallePedido.ModificarCant(Det.Pedidoid.Pedidoid, Det.Articuloid.Articuloid, ListaDetalleDTO[i].Cantidad);
                                    }
                                }

                            }
                            i++;
                        }
                        i = 0;
                    }
                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "PedidoModificado();", true);
                    Response.AddHeader("REFRESH", "2;URL=EditarPedido.aspx");
                }
                else
                {

                    foreach (DetallePedidoDTO i in ListaBase)
                    {
                        ControlArticulo.SumarStock(i.Cantidad, i.Articuloid.Articuloid);
                    }
                    if (ControlPedido.Eliminar(Convert.ToInt32(txtnropedidomostrar.Text)))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "errorpedElim();", true);
                        Response.AddHeader("REFRESH", "2;URL=EditarPedido.aspx");
                    }

                }
            }
            else
            {
                Response.Redirect("EditarPedido.aspx");
            }
        }

        protected void BtnBuscarNombre_Click(object sender, EventArgs e)
        {
            ArticuloId = null;
            List<Articulo> ListaNombre = new List<Articulo>();
            if (IsValid)
            {
                if (!_isRefresh)
                {
                        if ((HdfArt.Value == null) || (string.IsNullOrEmpty(HdfArt.Value)))
                        {
                            ListaNombre = ControlArticulo.BuscarXNombreMarcaActivo(TxtBuscarNombre.Text.ToLower());
                            if (ListaNombre.Count != 0)
                            {
                                if (!_isRefresh)
                                {
                                    GrillaBuscarArt.DataSource = ListaNombre;
                                    GrillaBuscarArt.DataBind();
                                    ModalPopupExtender2.Show();
                                }
                                else
                                {
                                    Response.Redirect("EditarPedido.aspx");
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralertart();", true);
                            }
                        }
                        else
                        {
                            ArticuloId = ControlArticulo.BuscarXIdActivo(Convert.ToInt32(HdfArt.Value));
                            if (ControlDetallePedido.ArtRepetidosEnElPedidoDetalle(ListaDetalleDTO, ArticuloId) == false)
                            {
                                HdfArt.Value = null;
                                if (!_isRefresh)
                                {
                                    ModalPopupExtender3.Show();
                                    TextCant.Focus();
                                }
                                else
                                {
                                    //Limpiar();
                                    Response.Redirect("EditarPedido.aspx");
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ArtIngresado();", true);
                                TxtBuscarNombre.Text = "";
                            }
                        }
                   
                }
                else
                {
                    //Limpiar();
                    Response.Redirect("EditarPedido.aspx");
                }

            }
        }
        [WebMethod]
        [ScriptMethod]
        public static List<string> GetNomMar(string prefixText)
        {
            MySqlConnection cn = new MySqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            String strCn = "Server=localhost; Database= baseProar; Uid=root";
            cn.ConnectionString = strCn;
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT nombre,descripcion,marca,articuloid FROM articulos WHERE (nombre LIKE @SearchText or marca LIKE @SearchText) and estado = 'activo'";
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
                dbValues = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["nombre"].ToString().ToLower() + ", " + row["descripcion"].ToString().ToLower() + ", " + row["marca"].ToString().ToLower(), Convert.ToString(row["articuloid"]));
                txtItems.Add(dbValues);
            }

            return txtItems;
        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            ArticuloId = null;
            ImageButton ImgB = sender as ImageButton;
            GridViewRow gvRow = (GridViewRow)ImgB.NamingContainer;
            if (!_isRefresh)
            {
                ArticuloId = ControlArticulo.BuscarXId(Convert.ToInt32(gvRow.Cells[0].Text));
                if (ControlDetallePedido.ArtRepetidosEnElPedidoDetalle(ListaDetalleDTO, ArticuloId) == false)
                {
                    ModalPopupExtender3.Show();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ArtIngresado();", true);
                    TxtBuscarNombre.Text = "";
                }
            }
            else
            {
                //Limpiar();
                Response.Redirect("EditarPedido.aspx");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (!_isRefresh)
            {
                if ((HdfArt.Value == null) || (string.IsNullOrEmpty(HdfArt.Value)))
                {
                    if (StockInsuficiente(ArticuloId, Convert.ToInt32(TextCant.Text)))
                    {
                        LblError.Visible = true;
                        ModalPopupExtender3.Show();
                    }
                    else
                    {
                        if (!_isRefresh)
                        {
                            DetallePedidoDTO DetalleAux = new DetallePedidoDTO(ArticuloId, ControlPedido.BuscarNro(Convert.ToInt32(txtnropedidomostrar.Text)), Convert.ToInt32(TextCant.Text), ArticuloId.Precioactual);
                            ListaDetalleDTO.Add(DetalleAux);
                            RefrescarTabla();
                            TextCant.Text = "1";
                            LblError.Visible = false;
                            TxtBuscarNombre.Text = "";
                            TxtBuscarNombre.Focus();
                        }
                        else
                        {
                            //Limpiar();
                            TextCant.Text = "1";
                            LblError.Visible = false;
                            Response.Redirect("EditarPedido.aspx");
                        }
                    }
                }
                else
                {
                    if (ArticuloId != null)
                    {
                        if (StockInsuficiente(ArticuloId, Convert.ToInt32(TextCant.Text)))
                        {
                            LblError.Visible = true;
                            ModalPopupExtender3.Show();
                        }
                        else
                        {
                            if (!_isRefresh)
                            {
                                DetallePedidoDTO DetalleAux = new DetallePedidoDTO(ArticuloId, ControlPedido.BuscarNro(Convert.ToInt32(txtnropedidomostrar.Text)), Convert.ToInt32(TextCant.Text), ArticuloId.Cantidad);
                                ListaDetalleDTO.Add(DetalleAux);
                                RefrescarTabla();
                                HdfArt.Value = null;
                                TxtBuscarNombre.Text = "";
                                TxtBuscarNombre.Focus();
                                TextCant.Text = "1";
                                LblError.Visible = false;

                            }
                            else
                            {
                                //Limpiar();
                                TextCant.Text = "1";
                                LblError.Visible = false;
                                Response.Redirect("EditarPedido.aspx");
                            }
                        }
                    }
                }
                }
                else
                {
                    //Limpiar();
                    TextCant.Text = "1";
                    LblError.Visible = false;
                    Response.Redirect("EditarPedido.aspx");
                }
            }

        protected void Button3_Click(object sender, EventArgs e)
        {
            ModalPopupExtender3.Hide();
            TextCant.Text = "1";
            LblError.Visible = false;
            HdfArt.Value = null;
            ArticuloId = null;
            TxtBuscarNombre.Text = null;
        }

        public void Limpiar()
        {
            
            Response.Redirect("EditarPedido.aspx");
        }
    }
}