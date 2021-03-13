using Controladoras;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using EntidadesDTO;
using System.Threading;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace TpFinalPracticaProfesional
{

    public partial class AgregarPedido : System.Web.UI.Page
    {
        C_Articulo ControlArticulo;
        C_Cliente ControlCliente;
        C_Pedido ControlPedido;
        C_Configuracion ControlConfig;
        
        double AuxTotal = 0;
        Cliente MostrarCli = null;
        List<Articulo> Lista = new List<Articulo>();
        List<Cliente> ListaCliente = new List<Cliente>();
        List<Articulo> ListaArticulo;
        List<int> ListaCant;
        private bool _refreshState;
        private bool _isRefresh;
        DataTable dt = null;
        int NroPedido = 0;
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
            ListaCant = (List<int>)Session["ListaCant"];
            ListaArticulo = (List<Articulo>)Session["ListaArticulo"];
            RadioSelecionadoART();
            RadioSelecionadoCLI();
            NroPedido = ControlConfig.DevolverUltimoPedido() + 1;

            if (!IsPostBack)
            {
                GrillaArticulos.DataSource = Lista;
                GrillaArticulos.DataBind();
                Session["ListaArticulo"] = new List<Articulo>();
                Session["ListaCant"] = new List<int>();
                TxtNroPedido.Text = Convert.ToString(NroPedido);
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
        
        public void RadioSelecionadoART()
        {
            if (RadioButtonBuscarART.SelectedValue == "nombre")
            {
                TxtBuscarNombre.Visible = true;
                BtnBuscarNombre.Visible = true;
                TxtBuscarCod.Visible = false;
                BtnBuscarCod.Visible = false;
                TxtBuscarNombre.Focus();
            }
            else
            {
                if (RadioButtonBuscarART.SelectedValue == "cod")
                {
                  TxtBuscarNombre.Visible = false;
                  BtnBuscarNombre.Visible = false;
                  TxtBuscarCod.Visible = true;
                  BtnBuscarCod.Visible = true;
                    TxtBuscarCod.Focus();
                }
            }
        }

        public void RadioSelecionadoCLI()
        {
            if (RadioButtonList1.SelectedValue == "apellido")
            {
                TxtBuscarApellido.Visible = true;
                BtnBusCli.Visible = true;
                TxtBuscarRS.Visible = false;
                BtnBusCli1.Visible = false;
                TxtBuscarApellido.Focus();
            }
            else
            {
                TxtBuscarApellido.Visible = false;
                BtnBusCli.Visible = false;
                TxtBuscarRS.Visible = true;
                BtnBusCli1.Visible = true;
                TxtBuscarRS.Focus();
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
                    e.Row.Cells[5].Text = "Total: $";
                    e.Row.Cells[6].Text =Convert.ToDouble(AuxTotal).ToString("#,##0.00");
                    e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Font.Bold = true;
                }
            }
            catch (Exception err)
            {
                string error = err.Message.ToString() + " - " + err.Source.ToString();
            }
        }

        protected void BtnBusCli_Click(object sender, EventArgs e)
        {
            MostrarCli = null;
            if (IsValid)
            {
                if(!_isRefresh)
                { 
                    if (RadioButtonList1.SelectedValue == "apellido")
                    {
                          if((hdnID.Value == null) || (string.IsNullOrEmpty(hdnID.Value)))
                          { 
                                ListaCliente = ControlCliente.BuscarxApellidoActivos(TxtBuscarApellido.Text.ToLower());
                                if (ListaCliente.Count != 0)
                                {
                                    BusquedaClientePop.DataSource = ListaCliente;
                                    BusquedaClientePop.DataBind();
                                    this.ModalPopupExtender1.Show();
                                    PanelPopUp.Visible = true;
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralertcli();", true);
                                }
                          }
                          else
                          {
                            MostrarCli = ControlCliente.BuscarIDActivo(Convert.ToInt32(hdnID.Value));
                            if(MostrarCli!=null)
                            {
                                hdnID.Value = null;
                                TxtApellido.Text = MostrarCli.Apellido;
                                TxtNombre.Text = MostrarCli.Nombre;
                                TxtTelefono.Text = MostrarCli.Telefono;
                                TxtDireccion.Text = MostrarCli.Direccion;
                                Txtemail.Text = MostrarCli.Email;
                                TxtRazonS.Text = MostrarCli.Razonsocial;
                                TxtCuil.Text = MostrarCli.Cuil;
                                TxtBuscarNombre.Focus();
                            }
                          }
                    }
                }
                else
                {
                    Limpiar();
                    TextCant.Text = "1";
                    LblError.Visible = false;
                    Response.Redirect("AgregarPedido.aspx");
                }
            }
        }

        protected void BtnBusCli1_Click(object sender, EventArgs e)
        {
             MostrarCli = null;
            if (IsValid)
            {
                if (!_isRefresh)
                {
                    if (RadioButtonList1.SelectedValue == "cuil")
                    {
                        MostrarCli = ControlCliente.BuscarcuilActivo(TxtBuscarRS.Text.ToLower());
                        if (MostrarCli != null)
                        {
                            TxtApellido.Text = MostrarCli.Apellido;
                            TxtNombre.Text = MostrarCli.Nombre;
                            TxtTelefono.Text = MostrarCli.Telefono;
                            TxtDireccion.Text = MostrarCli.Direccion;
                            Txtemail.Text = MostrarCli.Email;
                            TxtRazonS.Text = MostrarCli.Razonsocial;
                            TxtCuil.Text = MostrarCli.Cuil;
                            TxtBuscarNombre.Focus();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralertcliRS();", true);
                        }
                    }
                }
                else
                {
                    Limpiar();
                    TextCant.Text = "1";
                    LblError.Visible = false;
                    Response.Redirect("AgregarPedido.aspx");
                }
                
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (!_isRefresh)
            {
                ImageButton ImgB = sender as ImageButton;
                GridViewRow gvRow = (GridViewRow)ImgB.NamingContainer;
                TxtRazonS.Text = gvRow.Cells[1].Text;
                TxtNombre.Text = gvRow.Cells[2].Text;
                TxtApellido.Text = gvRow.Cells[3].Text;
                TxtTelefono.Text = gvRow.Cells[4].Text;
                Txtemail.Text = gvRow.Cells[5].Text;
                TxtCuil.Text = gvRow.Cells[7].Text;
                TxtDireccion.Text = gvRow.Cells[6].Text;
            }
            else
            {
                Limpiar();
                TextCant.Text = "1";
                LblError.Visible = false;
                Response.Redirect("AgregarPedido.aspx");
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
                    if (RadioButtonBuscarART.SelectedValue == "nombre")
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
                                    Limpiar();
                                    Response.Redirect("AgregarPedido.aspx");
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
                            if (ControlArticulo.ArtRepetidosEnElPedido(ListaArticulo, ArticuloId) == false)
                            {
                                HdfArt.Value = null;
                                if (!_isRefresh)
                                {
                                    ModalPopupExtender3.Show();
                                }
                                else
                                {
                                    Limpiar();
                                    Response.Redirect("AgregarPedido.aspx");
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ArtIngresado();", true);
                                TxtBuscarNombre.Text = "";
                            }
                        }
                    }
                }
                else
                {
                    Limpiar();
                    Response.Redirect("AgregarPedido.aspx");
                }
                
            }
        }
      
        protected void BtnBuscarCod_Click(object sender, EventArgs e)
        {
            AuxTotal = 0;
            Session["Auxdouble"] = AuxTotal;
            ArticuloId = null;
            if (IsValid)
            {
                if (!_isRefresh)
                {
                        if (RadioButtonBuscarART.SelectedValue == "cod")
                        {
                            ArticuloId = ControlArticulo.BuscarXIdActivo(Convert.ToInt32(TxtBuscarCod.Text.ToLower()));
                            if (ControlArticulo.ArtRepetidosEnElPedido(ListaArticulo, ArticuloId) == false)
                            {
                                if (ArticuloId != null)
                                {
                                    if (!_isRefresh)
                                    {
                                        ModalPopupExtender3.Show();
                                    }
                                    else
                                    {
                                        Limpiar();
                                        Response.Redirect("AgregarPedido.aspx");
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralertart();", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ArtIngresado();", true);
                                TxtBuscarCod.Text = "";
                            }
                        }
                }
                else
                {
                    Limpiar();
                    TextCant.Text = "1";
                    LblError.Visible = false;
                    Response.Redirect("AgregarPedido.aspx");
                }
            }
        }
        
        public void RefrescarTabla()
        {
            if(!_isRefresh)
            { 
                if (ListaArticulo.Count != 0 && ListaCant.Count != 0)
                {
                    dt = new DataTable();
                    dt.Columns.Add(new DataColumn("articuloid", System.Type.GetType("System.Int32")));
                    dt.Columns.Add(new DataColumn("nombre", System.Type.GetType("System.String")));
                    dt.Columns.Add(new DataColumn("descripcion", System.Type.GetType("System.String")));
                    dt.Columns.Add(new DataColumn("marca", System.Type.GetType("System.String")));
                    dt.Columns.Add(new DataColumn("cantidad", System.Type.GetType("System.Int32")));
                    dt.Columns.Add(new DataColumn("precioactual", System.Type.GetType("System.Double")));
                    dt.Columns.Add(new DataColumn("totalxArt", System.Type.GetType("System.Double")));
                    int i = 0;
                    foreach (var Auxi in ListaArticulo)
                    {
                        int Cant = Convert.ToInt32(ListaCant[i].ToString());
                        dt.Rows.Add(Auxi.Articuloid, Auxi.Nombre, Auxi.Descripcion, Auxi.Marca, Cant, Auxi.Precioactual.ToString("#,##0.00"), (Auxi.Precioactual* Cant).ToString("#,##0.00"));
                        i++;   
                    }
                    GrillaArticulos.DataSource = dt;
                    GrillaArticulos.DataBind();
                }
            }
            else
            {
                Limpiar();
                TextCant.Text = "1";
                LblError.Visible = false;
                Response.Redirect("AgregarPedido.aspx");
            }
        }

        [WebMethod]
        [ScriptMethod]
        public static List<string> GetClientes(string prefixText)
        {
            MySqlConnection cn = new MySqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            String strCn = "Server=localhost; Database= baseProar; Uid=root";
            cn.ConnectionString = strCn;
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT apellido,nombre,clienteid FROM clientes WHERE (apellido LIKE @SearchText or nombre LIKE @SearchText) and estado = 'activo'";
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
                dbValues = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["apellido"].ToString().ToLower() + " " + row["nombre"].ToString().ToLower(), Convert.ToString(row["clienteid"]));
                txtItems.Add(dbValues);
            }

            return txtItems;
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
                if (ControlArticulo.ArtRepetidosEnElPedido(ListaArticulo, ArticuloId) == false)
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
                Limpiar();
                Response.Redirect("AgregarPedido.aspx");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (!_isRefresh)
            {
                if (RadioButtonBuscarART.SelectedValue == "nombre")
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
                                ListaArticulo.Add(ArticuloId);
                                Session["ListaArticulo"] = ListaArticulo;
                                ListaCant.Add(Convert.ToInt32(TextCant.Text));
                                Session["ListaCant"] = ListaCant;
                                RefrescarTabla();
                                TextCant.Text = "1";
                                LblError.Visible = false;
                                TxtBuscarNombre.Text = "";
                                TxtBuscarNombre.Focus();
                            }
                            else
                            {
                                Limpiar();
                                TextCant.Text = "1";
                                LblError.Visible = false;
                                Response.Redirect("AgregarPedido.aspx");
                            }
                        }
                    }
                    else
                    {
                        ////ArticuloId = ControlArticulo.BuscarXId(Convert.ToInt32(HdfArt.Value));
                            if(ArticuloId != null) 
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
                                        ListaArticulo.Add(ArticuloId);
                                        Session["ListaArticulo"] = ListaArticulo;
                                        ListaCant.Add(Convert.ToInt32(TextCant.Text));
                                        Session["ListaCant"] = ListaCant;
                                        RefrescarTabla();
                                        HdfArt.Value = null;
                                        TxtBuscarNombre.Text = "";
                                        TxtBuscarNombre.Focus();
                                        TextCant.Text = "1";
                                        LblError.Visible = false;

                                    }
                                    else
                                    {
                                        Limpiar();
                                        TextCant.Text = "1";
                                        LblError.Visible = false;
                                        Response.Redirect("AgregarPedido.aspx");
                                    }
                                }
                            }
                    }
                }
                else
                {

                    if (RadioButtonBuscarART.SelectedValue == "cod")
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
                                    ListaArticulo.Add(ArticuloId);
                                    Session["ListaArticulo"] = ListaArticulo;
                                    ListaCant.Add(Convert.ToInt32(TextCant.Text));
                                    Session["ListaCant"] = ListaCant;
                                    RefrescarTabla();
                                    HdfArt.Value = null;
                                    TextCant.Text = "1";
                                    LblError.Visible = false;
                                    TxtBuscarCod.Text = "";
                                    TxtBuscarCod.Focus();
                                }
                                else
                                {
                                    Limpiar();
                                    TextCant.Text = "1";
                                    LblError.Visible = false;
                                    Response.Redirect("AgregarPedido.aspx");
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Limpiar();
                TextCant.Text = "1";
                LblError.Visible = false;
                Response.Redirect("AgregarPedido.aspx");
            }
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (Convert.ToInt64(TxtBuscarRS.Text) >= 0)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        public bool StockInsuficiente(Articulo aux,int cant)
        {
            bool Cant = false;
            int Stock = 0;
            Stock = ControlArticulo.ObtenerCantidad(aux);
            if(Stock < Convert.ToInt32(cant))
            {
                Cant = true;
            }
            return Cant;
        }

        protected void Button3_Click1(object sender, EventArgs e)
        {
            ModalPopupExtender3.Hide();
            TextCant.Text = "1";
            LblError.Visible = false;
            HdfArt.Value = null;
            ArticuloId = null;
            TxtBuscarNombre.Text = null;
            TxtBuscarCod.Text = null;
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (!_isRefresh)
                {
                    List<DetallePedido> Lista = new List<DetallePedido>();
                    ListaCant =(List<int>) Session["ListaCant"];
                    ListaArticulo = (List<Articulo>)Session["ListaArticulo"];

                    int i = 0;
                    foreach (var item in ListaArticulo)
                    {
                        int auxcant = Convert.ToInt32(ListaCant[i].ToString());
                        i++;

                        DetallePedido Aux = new DetallePedido()
                        {
                            Articuloid = item.Articuloid,
                            Pedidoid = 0,
                            Cantidad = auxcant,
                            Preciovendido = item.Precioactual
                        };
                        ControlArticulo.RestarStock(auxcant, item.Articuloid);
                        Lista.Add(Aux); 
                    }
                    Cliente Cli = null;
                    Cli = ControlCliente.Buscarcuil(TxtCuil.Text);
                    if(Cli != null && Lista.Count != 0)
                    {
                        
                        Pedido Pd = new Pedido()
                        {
                            Pedidoid = 0,
                            Nropedido = NroPedido,
                            Fecha = DateTime.Now,
                            Entregado = "",
                            Clienteid = Cli.Clienteid,
                            Estado = ""
                        };
                        if (!_isRefresh)
                        {
                            if (ControlPedido.Agregar(Pd, Lista))
                            {
                                ControlConfig.ProximoPedido(NroPedido);
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert();", true);
                                Response.AddHeader("REFRESH", "2;URL=AgregarPedido.aspx");

                            }

                        }
                        else
                        {
                            Limpiar();
                            Response.Redirect("AgregarPedido.aspx");
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralertFaltaCampos()", true);
                    }
                }
                else
                {
                    Limpiar();
                    Response.Redirect("AgregarPedido.aspx");
                }
            }
        }
        
        protected void TxtNroPedido_TextChanged(object sender, EventArgs e)
        {

        }

        protected void GrillaArticulos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ListaCant = (List<int>)Session["ListaCant"];
            ListaArticulo =(List<Articulo>)Session["ListaArticulo"];
            ListaArticulo.RemoveAt(e.RowIndex);
            Session["ListaArticulo"] = ListaArticulo;
            ListaCant.RemoveAt(e.RowIndex);
            Session["ListaCant"] = ListaCant;
            RefrescarTabla();
            if (ListaArticulo.Count == 0)
            {

                GrillaArticulos.DataSource = Lista;
                GrillaArticulos.DataBind();
                ListaArticulo = new List<Articulo>();
                Session["ListaArticulo"] = ListaArticulo;
                ListaCant = new List<int>();
                Session["ListaCant"] = ListaCant;
            }
        }

        protected void GrillaArticulos_RowEditing(object sender, GridViewEditEventArgs e)
        {

            GrillaArticulos.EditIndex = e.NewEditIndex;
            RefrescarTabla();
        }

        protected void GrillaArticulos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            GridViewRow row = GrillaArticulos.Rows[e.RowIndex];
            TextBox Txtc = (TextBox)row.Cells[4].Controls[0];
            int CodArt = Convert.ToInt32(row.Cells[0].Text);
            if (Txtc != null)
            {
                Articulo ArticuloAux = ControlArticulo.BuscarXIdActivo(CodArt);
                if (ArticuloAux != null)
                {
                    if ((!string.IsNullOrEmpty(Txtc.Text)))
                    {
                        if (Int32.TryParse(Txtc.Text, out int result))
                        {
                            if (Convert.ToInt32(Txtc.Text) > 0)
                            {
                                if (StockInsuficiente(ArticuloAux, Convert.ToInt32(Txtc.Text)))
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "errorStockInsu();", true);
                                }
                                else
                                {
                                    ListaCant = (List<int>)Session["ListaCant"];
                                    ListaCant[e.RowIndex] = Convert.ToInt32(Txtc.Text);
                                    Session["ListaCant"] = ListaCant;
                                    GrillaArticulos.EditIndex = -1;
                                    RefrescarTabla();
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
        }

        protected void GrillaArticulos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            GrillaArticulos.EditIndex = -1;
            RefrescarTabla();
        }

        public void Limpiar()
        {
            ListaArticulo = null;
            Session["ListaArticulo"] = ListaArticulo;
            ListaCant = null;
            Session["ListaCant"] = ListaCant;
            AuxTotal = 0;
            Session["Auxdouble"] = AuxTotal;
            TxtApellido.Text = "";
            TxtBuscarApellido.Text = "";
            TxtBuscarCod.Text = "";
            TxtBuscarNombre.Text = "";
            TxtBuscarRS.Text = "";
            TxtCuil.Text = "";
            TxtDireccion.Text = "";
            Txtemail.Text = "";
            TxtNombre.Text = "";
            TxtRazonS.Text = "";
            TxtTelefono.Text = "";
            Response.Redirect("AgregarPedido.aspx");
        }

  
    }
}