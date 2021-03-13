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
    public partial class AgregarFactura : System.Web.UI.Page
    {
        Pedido AuxPedido = null;
        double AuxTotal = 0;
        double AuxTotal2 = 0;
        List<PedidoDTO> ListaDTO = null;
        private bool _refreshState;
        private bool _isRefresh;
        List<DetallePedidoDTO> ListaDetalleDTO;
        C_Articulo ControlArticulo;
        C_Cliente ControlCliente;
        C_Pedido ControlPedido;
        C_Configuracion ControlConfig;
        C_DetallePedido ControlDetallePedido;
        C_Empleado ControlEmpleado;
        C_Facturacion ControlFacturacion;
        DataTable dt = null;
        Cliente cli = null;
        char Tipo;
        int NroFacturaA = 0, NroFacturaB = 0;
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
            ControlEmpleado = (C_Empleado)Session["ControlEmpleado"];
            ControlFacturacion = (C_Facturacion)Session["ControlFacturacion"]; 
            ListaDetalleDTO = (List<DetallePedidoDTO>)Session["ListaDetalleDTO"];
            AuxPedido = (Pedido)Session["PedidoSession"];
            NroFacturaA = ControlConfig.DevolverUltimaFacturaA() + 1;
            NroFacturaB = ControlConfig.DevolverUltimaFacturaB() + 1;
            if (!IsPostBack)
            {
                ListaDTO = new List<PedidoDTO>();
                Session["ListaDetalleDTO"] = new List<DetallePedidoDTO>();
            }


            if (ddlEmpleado.Items.Count == 0)
            {
                var EmpleadosQuery = ControlEmpleado.ListarActivos().Select(emp => new { empleadoid = emp.Empleadoid , DisplayText = emp.Apellido.ToString() + " " + emp.Nombre });

                ddlEmpleado.DataSource = null;
                ddlEmpleado.DataSource = EmpleadosQuery ;
                ddlEmpleado.DataValueField = "empleadoid";
                ddlEmpleado.DataTextField = "DisplayText";
                ddlEmpleado.DataBind();
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

            ListaDetalleDTO = (List<DetallePedidoDTO>)Session["ListaDetalleDTO"];
            if (IsValid)
            {
                if (RadioButtonListBusqueda.SelectedValue == "nro")
                {
                    if (!_isRefresh)
                    {
                        AuxPedido = ControlPedido.BuscarNroPedidoEstadoEntregado(Convert.ToInt32(TxtNro.Text));
                        if (AuxPedido != null)
                        {
                            Session["PedidoSession"] = AuxPedido;
                            cli = ControlCliente.BuscarID(AuxPedido.Clienteid);
                            TxtNombre.Text = cli.Apellido + " " + cli.Nombre;
                            txtnropedidomostrar.Text = Convert.ToString(AuxPedido.Nropedido);
                            txtfechamostrar.Text = Convert.ToString(DateTime.Now.ToString("yyyy/MM/dd"));
                            TxtTipoFactura.Text = Convert.ToString(TipoFactura(cli.Tipo));
                            Tipo = TipoFactura(cli.Tipo);
                            ListaDetalleDTO = ControlDetallePedido.DevolverTodoxID(AuxPedido.Pedidoid);
                            Session["ListaDetalleDTO"] = ListaDetalleDTO;
                            RefrescarTabla();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "errorpedido();", true);
                        }
                    }
                    else
                    {
                        Response.Redirect("AgregarFactura.aspx");
                    }
                }
            }
        }

        protected void BtnCli_Click(object sender, EventArgs e)
        {

            ListaDetalleDTO = (List<DetallePedidoDTO>)Session["ListaDetalleDTO"];
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
                                Response.Redirect("AgregarFactura.aspx");
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
                                Session["PedidoSession"] = AuxPedido;
                                hdnIDCli.Value = null;
                                cli = ControlCliente.BuscarID(AuxPedido.Clienteid);
                                TxtNombre.Text = cli.Apellido + " " + cli.Nombre;
                                txtnropedidomostrar.Text = Convert.ToString(AuxPedido.Nropedido);
                                txtfechamostrar.Text = Convert.ToString(DateTime.Now.ToString("yyyy/MM/dd"));
                                Tipo = TipoFactura(cli.Tipo);
                                TxtTipoFactura.Text = Convert.ToString(TipoFactura(cli.Tipo));
                                ListaDetalleDTO = ControlDetallePedido.DevolverTodoxID(AuxPedido.Pedidoid);
                                Session["ListaDetalleDTO"] = ListaDetalleDTO;
                                RefrescarTabla();
                            }
                        }
                        else
                        {
                            Response.Redirect("AgregarFactura.aspx");
                        }
                    }
                }
            }
        }

        protected void BtnRango_Click(object sender, EventArgs e)
        {

            ListaDetalleDTO = (List<DetallePedidoDTO>)Session["ListaDetalleDTO"];
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
                Session["PedidoSession"] = AuxPedido;
                cli = ControlCliente.BuscarID(AuxPedido.Clienteid);
                TxtNombre.Text = cli.Apellido + " " + cli.Nombre;
                txtnropedidomostrar.Text = Convert.ToString(AuxPedido.Nropedido);
                txtfechamostrar.Text = Convert.ToString(DateTime.Now.ToString("yyyy/MM/dd"));
                TxtTipoFactura.Text = Convert.ToString(TipoFactura(cli.Tipo));
                Tipo = TipoFactura(cli.Tipo);
                ListaDetalleDTO = ControlDetallePedido.DevolverTodoxID(AuxPedido.Pedidoid);
                Session["ListaDetalleDTO"] = ListaDetalleDTO;
                RefrescarTabla();
            }
        }

        protected void BusquedaClientePedido_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BusquedaClientePedido.PageIndex = e.NewPageIndex;
            RefrescarTablaNombreApellido();
            BusquedaClientePedido.PageSize = ControlConfig.DevolverNroMaxPaginacion();
        }

        public char TipoFactura(string TipoCliente)
        {
            char Tipo;
            if (TipoCliente == "responsable inscripto")
            {
                Tipo = 'A';
                TxtnroFactura.Text = Convert.ToString(NroFacturaA.ToString("00000000"));
            }
            else
            {
                Tipo = 'B';
                TxtnroFactura.Text = Convert.ToString(NroFacturaB.ToString("00000000"));
            }
            return Tipo;
        }

        public void RefrescarTabla()
        {
                                            Session["ImporteTotal"] = null;

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
                    AuxTotal2 = AuxTotal2 + (Auxi.Preciovendido * Auxi.Cantidad);
                    if (Tipo == 'A')
                    {
                        double PrecioSinIva = Auxi.Preciovendido - (Auxi.Preciovendido * 0.21);
                        dt.Rows.Add(Auxi.Articuloid.Articuloid, Auxi.Articuloid.Nombre, Auxi.Articuloid.Descripcion, Auxi.Articuloid.Marca, Auxi.Cantidad, PrecioSinIva.ToString("#,##0.00"), (PrecioSinIva * Auxi.Cantidad).ToString("#,##0.00"));
                    }
                    else
                    {
                        dt.Rows.Add(Auxi.Articuloid.Articuloid, Auxi.Articuloid.Nombre, Auxi.Articuloid.Descripcion, Auxi.Articuloid.Marca, Auxi.Cantidad, Auxi.Preciovendido.ToString("#,##0.00"), (Auxi.Preciovendido * Auxi.Cantidad).ToString("#,##0.00"));
                    }
                }
                GrillaArticulos.DataSource = dt;
                GrillaArticulos.DataBind();
                Session["ImporteTotal"] = AuxTotal2;
            }
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                    if (!_isRefresh)
                    {
                        AuxPedido = (Pedido)Session["PedidoSession"];
                        Facturacion_Venta Nuevo = new Facturacion_Venta()
                        {
                            Cod_factura = Convert.ToInt32(TxtnroFactura.Text),
                            Fecha = Convert.ToDateTime(txtfechamostrar.Text),
                            Importetotal = Convert.ToDouble(Session["ImporteTotal"]),
                            Clienteid = AuxPedido.Clienteid,
                            Empleadoid = Convert.ToInt32(ddlEmpleado.SelectedValue),
                            Metododepago = DdlMetododePago.SelectedValue,
                            Pedidoid = Convert.ToInt32(txtnropedidomostrar.Text),
                            Tipodefactura = Convert.ToChar(TxtTipoFactura.Text)
                        };
                        Pedido AuxiP = ControlPedido.BuscarNro(Convert.ToInt32(txtnropedidomostrar.Text));
                        if (ControlFacturacion.Agregar(Nuevo, "entregado", AuxiP.Pedidoid))
                        {
                                if(Convert.ToChar(TxtTipoFactura.Text) == 'A')
                                {
                                    ControlConfig.ProximoFacturaA(NroFacturaA);
                                }
                                else
                                {
                                    ControlConfig.ProximaFacturaB(NroFacturaB);
                                }
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "AgregarFactura();", true);

                                Response.AddHeader("REFRESH", "2;URL=AgregarFactura.aspx");
                                Session["ImporteTotal"] = null;
                        }

                    }
                    else
                    {
                        Response.Redirect("AgregarFactura.aspx");
                    }

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

        protected void GrillaArticulos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (Tipo == 'A')
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        AuxTotal += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "totalxArt"));
                    }
                    else if (e.Row.RowType == DataControlRowType.Footer)
                    {
                        e.Row.Cells[3].Text = "SubTotal: $"+ Convert.ToDouble(AuxTotal).ToString("#,##0.00");
                        e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                        e.Row.Cells[4].Text = "IVA%: $"+ Convert.ToDouble(AuxTotal2 * 0.21).ToString("#,##0.00");
                        e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                        e.Row.Cells[5].Text = "Total: $"+Convert.ToDouble(AuxTotal2).ToString("#,##0.00");
                        e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                        e.Row.Font.Bold = true;
                    }

                }
                else
                { 

                    if (e.Row.RowType == DataControlRowType.Footer)
                    {
                        AuxTotal = AuxTotal2;
                        e.Row.Cells[5].Text = "Total: $"+Convert.ToDouble(AuxTotal2).ToString("#,##0.00");
                        e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                        e.Row.Font.Bold = true;
                    }
                }
            }
            catch (Exception err)
            {
                string error = err.Message.ToString() + " - " + err.Source.ToString();
            }
        }
    }
}