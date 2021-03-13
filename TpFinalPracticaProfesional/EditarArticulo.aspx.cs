using Controladoras;
using Entidades;
using EntidadesDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpFinalPracticaProfesional
{
    public partial class EditarArticulo : System.Web.UI.Page
    {
        C_Proveedor ControlProveedor;
        C_Configuracion ControlConfig;
        C_ArticuloxProveedor ControlArticuloxProveedor;
        C_Articulo ControlArticulo;
        List<Articulo> Lista = new List<Articulo>();
        ArticuloxProveedorDTO AuxDTO = null;
        static int ArtId;
        static int CodBuscado;
        static string MarcaST;
        static string NombreST;
        private bool _refreshState;
        private bool _isRefresh;
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            ControlArticulo = (C_Articulo)Session["ControlArticulo"];
            ControlProveedor = (C_Proveedor)Session["ControlProveedor"];
            ControlConfig = (C_Configuracion)Session["ControlConfig"];
            ControlArticuloxProveedor = (C_ArticuloxProveedor)Session["ControlArticuloxProveedor"];

            if (!IsPostBack)
            {
                ArtId = 0;
                CodBuscado = 0;
                MarcaST = "";
                NombreST = "";
                DdlProveedor.DataSource = null;
                DdlProveedor.DataValueField = "proveedorid";
                DdlProveedor.DataTextField = "nombrefantasia";
                DdlProveedor.DataSource = ControlProveedor.Listar();
                DdlProveedor.DataBind();
                //RadioSelecionado();
                TodosArticulos.PageSize = ControlConfig.DevolverNroMaxPaginacion();
                TodosArticulos.DataSource = dt;
                TodosArticulos.DataBind();
                //RefrescarGrillaNombre(NombreST);
                //RefrescarGrillaMarca(MarcaST);
                //RefrescarGrillaCod(CodBuscado);
            }
            RadioSelecionado();
            RefrescarGrillaNombre(NombreST);
            RefrescarGrillaMarca(MarcaST);
            RefrescarGrillaCod(CodBuscado);
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

        protected void BtnBuscarMarca_Click(object sender, EventArgs e)
        {
            List<ArticuloxProveedorDTO> ListaDTO = new List<ArticuloxProveedorDTO>();
            if (IsValid)
            {
                MarcaST = TxtBuscarMarca.Text;
                if (RadioButtonListBusqueda.SelectedValue == "marca")
                {
                    ListaDTO = ControlArticulo.BuscarXMarca(TxtBuscarMarca.Text.ToLower());
                    if (ListaDTO.Count != 0)
                    {
                        dt = new DataTable();
                        dt.Columns.Add(new DataColumn("articuloid", System.Type.GetType("System.Int32")));
                        dt.Columns.Add(new DataColumn("nombre", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("descripcion", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("marca", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("precioactual", System.Type.GetType("System.Double")));
                        dt.Columns.Add(new DataColumn("cantidad", System.Type.GetType("System.Int32")));
                        dt.Columns.Add(new DataColumn("stockmin", System.Type.GetType("System.Int32")));
                        dt.Columns.Add(new DataColumn("costo", System.Type.GetType("System.Double")));
                        dt.Columns.Add(new DataColumn("nombrefantasia", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("estado", System.Type.GetType("System.String")));

                        foreach (ArticuloxProveedorDTO Auxi in ListaDTO)
                        {
                            dt.Rows.Add(Auxi.Articuloid.Articuloid, Auxi.Articuloid.Nombre, Auxi.Articuloid.Descripcion, Auxi.Articuloid.Marca, Auxi.Articuloid.Precioactual.ToString("#,##0.00"), Auxi.Articuloid.Cantidad, Auxi.Articuloid.Stockmin, Auxi.Costo.ToString("#,##0.00"), Auxi.Proveedorid.Nombrefantasia, Auxi.Articuloid.Estado);
                        }
                        //TodosArticulos.PageSize = ControlConfig.DevolverNroMaxPaginacion();
                        TodosArticulos.DataSource = dt;
                        TodosArticulos.DataBind();
                        TodosArticulos.Visible = true;
                        PanelGrilla.Visible = true;
                        PanelPopUp.Attributes["display"] = "none";
                    }
                    else
                    {
                        TodosArticulos.Visible = false;
                        PanelGrilla.Visible = false;
                        PanelPopUp.Attributes["display"] = "none";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert();", true);
                    }

                }
            }
        }

        protected void BtnBuscarNombre_Click(object sender, EventArgs e)
        {
            List<ArticuloxProveedorDTO> ListaDTO = new List<ArticuloxProveedorDTO>();
            if (IsValid)
            {
                NombreST = TxtBuscarNombre.Text;
                if (RadioButtonListBusqueda.SelectedValue == "nombre")
                {
                    ListaDTO = ControlArticulo.BuscarXNombre(TxtBuscarNombre.Text.ToLower());
                    if (ListaDTO.Count != 0)
                    {
                        dt = new DataTable();
                        dt.Columns.Add(new DataColumn("articuloid", System.Type.GetType("System.Int32")));
                        dt.Columns.Add(new DataColumn("nombre", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("descripcion", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("marca", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("precioactual", System.Type.GetType("System.Double")));
                        dt.Columns.Add(new DataColumn("cantidad", System.Type.GetType("System.Int32")));
                        dt.Columns.Add(new DataColumn("stockmin", System.Type.GetType("System.Int32")));
                        dt.Columns.Add(new DataColumn("costo", System.Type.GetType("System.Double")));
                        dt.Columns.Add(new DataColumn("nombrefantasia", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("estado", System.Type.GetType("System.String")));

                        foreach (ArticuloxProveedorDTO Auxi in ListaDTO)
                        {
                            dt.Rows.Add(Auxi.Articuloid.Articuloid, Auxi.Articuloid.Nombre, Auxi.Articuloid.Descripcion, Auxi.Articuloid.Marca, Auxi.Articuloid.Precioactual.ToString("#,##0.00"), Auxi.Articuloid.Cantidad, Auxi.Articuloid.Stockmin, Auxi.Costo.ToString("#,##0.00"), Auxi.Proveedorid.Nombrefantasia,Auxi.Articuloid.Estado);
                        }
                        //TodosArticulos.PageSize = ControlConfig.DevolverNroMaxPaginacion();
                        TodosArticulos.DataSource = dt;
                        TodosArticulos.DataBind();
                        TodosArticulos.Visible = true;
                        PanelGrilla.Visible = true;
                        PanelPopUp.Attributes["display"] = "none";

                    }
                    else
                    {
                        TodosArticulos.Visible = false;
                        PanelGrilla.Visible = false;
                        PanelPopUp.Attributes["display"] = "none";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert();", true);
                    }

                }
            }
        }
        public void LimpiarTxtBuscar()
        {
            TxtBuscarNombre.Text = "";
            TxtBuscarMarca.Text = "";
            TxtBuscarCod.Text = "";
        }
        

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ImgB = sender as ImageButton;
            GridViewRow gvRow = (GridViewRow)ImgB.NamingContainer;
            TxtCodArt.Text = gvRow.Cells[0].Text;
            TxtCodArt.Focus();
            TxtNombre.Text = gvRow.Cells[1].Text;
            TxtDescripcion.Text = gvRow.Cells[2].Text;
            TxtMarca.Text = gvRow.Cells[3].Text;
            TxtPrecioAc.Text = gvRow.Cells[4].Text;
            TxtCantidad.Text = gvRow.Cells[5].Text;
            TxtStockMin.Text = gvRow.Cells[6].Text;
            TxtCosto.Text = gvRow.Cells[7].Text;
            int i = 0;
            foreach (var item in DdlProveedor.Items)
            {
                if (item.ToString().ToLower() == gvRow.Cells[8].Text)
                {
                    i = DdlProveedor.Items.IndexOf((ListItem)item);
                    break;
                }
            }
            DdlProveedor.SelectedIndex = i;
            DdlProveedor.Items[i].Selected = true;

            int x = 0;
            foreach (var item2 in ddlestado.Items)
            {
                if (item2.ToString().ToLower() == gvRow.Cells[9].Text)
                {
                    x = ddlestado.Items.IndexOf((ListItem)item2);
                    break;
                }
            }
            ddlestado.SelectedIndex = x;
            ddlestado.Items[x].Selected = true;
            ArtId = Convert.ToInt32(gvRow.Cells[0].Text);
            this.ModalPopupExtender1.Show();
        }

        protected void BtnEditAceptar_Click(object sender, EventArgs e)
        {
            Articulo Aux = null;
            ArticuloxProveedorDTO Aux2 = null;
            if (IsValid)
            {
                int cod = Convert.ToInt32(DdlProveedor.SelectedValue);
                string estado = Convert.ToString(ddlestado.SelectedValue);

                Aux = new Articulo()
                {
                    Articuloid = Convert.ToInt32(TxtCodArt.Text),
                    Nombre = TxtNombre.Text.ToLower(),
                    Descripcion = TxtDescripcion.Text.ToLower(),
                    Marca = TxtMarca.Text.ToLower(),
                    Precioactual = Convert.ToDouble(TxtPrecioAc.Text),
                    Cantidad = Convert.ToInt32(TxtCantidad.Text),
                    Stockmin = Convert.ToInt32(TxtStockMin.Text),
                    Estado = estado
                };

                Proveedor Prov = ControlProveedor.BuscarxId(cod);
                Aux2 = new ArticuloxProveedorDTO(Aux, Prov, Convert.ToDouble(TxtCosto.Text));
                if (ControlArticulo.Modificar(ArtId,Aux,Aux2))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert();", true);
                    LimpiarTxtBuscar();
                    Labelmgg.Visible = false;
                    switch (RadioButtonListBusqueda.SelectedValue)
                    {
                        case "nombre":
                            RefrescarGrillaNombre(NombreST);
                            break;
                        case "marca":
                            RefrescarGrillaMarca(MarcaST);
                            break;
                        case "cod":
                            RefrescarGrillaCod(CodBuscado);
                            break;
                    }
                }
                else
                {
                    ModalPopupExtender1.Show();
                    Labelmgg.Visible = true;
                }
            }
        }

        public void RefrescarGrillaCod(int id)
        {
            ArticuloxProveedorDTO AXPAux = null;
            AXPAux = ControlArticuloxProveedor.ListaEspecial(Convert.ToInt32(id));

            if (AXPAux != null)
            {
                //dt = new DataTable();
                dt.Columns.Add(new DataColumn("articuloid", System.Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("nombre", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("descripcion", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("marca", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("precioactual", System.Type.GetType("System.Double")));
                dt.Columns.Add(new DataColumn("cantidad", System.Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("stockmin", System.Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("costo", System.Type.GetType("System.Double")));
                dt.Columns.Add(new DataColumn("nombrefantasia", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("estado", System.Type.GetType("System.String")));

                dt.Rows.Add(AXPAux.Articuloid.Articuloid, AXPAux.Articuloid.Nombre, AXPAux.Articuloid.Descripcion, AXPAux.Articuloid.Marca, AXPAux.Articuloid.Precioactual, AXPAux.Articuloid.Cantidad, AXPAux.Articuloid.Stockmin, AXPAux.Costo, AXPAux.Proveedorid.Nombrefantasia,AXPAux.Articuloid.Estado);
                //TodosArticulos.PageSize = ControlConfig.DevolverNroMaxPaginacion();
                TodosArticulos.DataSource = dt;
                TodosArticulos.DataBind();
                TodosArticulos.Visible = true;
                PanelGrilla.Visible = true;
                PanelPopUp.Attributes["display"] = "none";

            }
        }

        public void RefrescarGrillaNombre(string nombre)
        {
            List<ArticuloxProveedorDTO> ListaGrillaDTO = null;
            ListaGrillaDTO = ControlArticulo.BuscarXNombre(nombre);
            if (ListaGrillaDTO.Count != 0)
            {
                dt = new DataTable();
                dt.Columns.Add(new DataColumn("articuloid", System.Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("nombre", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("descripcion", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("marca", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("precioactual", System.Type.GetType("System.Double")));
                dt.Columns.Add(new DataColumn("cantidad", System.Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("stockmin", System.Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("costo", System.Type.GetType("System.Double")));
                dt.Columns.Add(new DataColumn("nombrefantasia", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("estado", System.Type.GetType("System.String")));

                foreach (ArticuloxProveedorDTO Auxi in ListaGrillaDTO)
                {
                    dt.Rows.Add(Auxi.Articuloid.Articuloid, Auxi.Articuloid.Nombre, Auxi.Articuloid.Descripcion, Auxi.Articuloid.Marca, Auxi.Articuloid.Precioactual.ToString("#,##0.00"), Auxi.Articuloid.Cantidad, Auxi.Articuloid.Stockmin, Auxi.Costo.ToString("#,##0.00"), Auxi.Proveedorid.Nombrefantasia,Auxi.Articuloid.Estado);
                }
                //TodosArticulos.PageSize = ControlConfig.DevolverNroMaxPaginacion();
                TodosArticulos.DataSource = dt;
                TodosArticulos.DataBind();
                TodosArticulos.Visible = true;
                PanelGrilla.Visible = true;
                PanelPopUp.Attributes["display"] = "none";

            }
        }

        public void RefrescarGrillaMarca(string marca)
        {
            List<ArticuloxProveedorDTO> ListaGrillaDTO = null;
            ListaGrillaDTO = ControlArticulo.BuscarXMarca(marca);
            if (ListaGrillaDTO.Count != 0)
            {
                dt = new DataTable();
                dt.Columns.Add(new DataColumn("articuloid", System.Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("nombre", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("descripcion", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("marca", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("precioactual", System.Type.GetType("System.Double")));
                dt.Columns.Add(new DataColumn("cantidad", System.Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("stockmin", System.Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("costo", System.Type.GetType("System.Double")));
                dt.Columns.Add(new DataColumn("nombrefantasia", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("estado", System.Type.GetType("System.String")));

                foreach (ArticuloxProveedorDTO Auxi in ListaGrillaDTO)
                {
                    dt.Rows.Add(Auxi.Articuloid.Articuloid, Auxi.Articuloid.Nombre, Auxi.Articuloid.Descripcion, Auxi.Articuloid.Marca, Auxi.Articuloid.Precioactual.ToString("#,##0.00"), Auxi.Articuloid.Cantidad, Auxi.Articuloid.Stockmin, Auxi.Costo.ToString("#,##0.00"), Auxi.Proveedorid.Nombrefantasia,Auxi.Articuloid.Estado);
                }
                //TodosArticulos.PageSize = ControlConfig.DevolverNroMaxPaginacion();
                TodosArticulos.DataSource = dt;
                TodosArticulos.DataBind();
                TodosArticulos.Visible = true;
                PanelGrilla.Visible = true;
                PanelPopUp.Attributes["display"] = "none";

            }
        }
        protected void BtnBuscarCod_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                CodBuscado = Convert.ToInt32(TxtBuscarCod.Text);

                if (RadioButtonListBusqueda.SelectedValue == "cod")
                {
                    AuxDTO = ControlArticuloxProveedor.ListaEspecial(Convert.ToInt32(TxtBuscarCod.Text));
                    if (AuxDTO != null)
                    {
                        dt = new DataTable();
                        dt.Columns.Add(new DataColumn("articuloid", System.Type.GetType("System.Int32")));
                        dt.Columns.Add(new DataColumn("nombre", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("descripcion", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("marca", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("precioactual", System.Type.GetType("System.Double")));
                        dt.Columns.Add(new DataColumn("cantidad", System.Type.GetType("System.Int32")));
                        dt.Columns.Add(new DataColumn("stockmin", System.Type.GetType("System.Int32")));
                        dt.Columns.Add(new DataColumn("costo", System.Type.GetType("System.Double")));
                        dt.Columns.Add(new DataColumn("nombrefantasia", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("estado", System.Type.GetType("System.String")));

                        dt.Rows.Add(AuxDTO.Articuloid.Articuloid, AuxDTO.Articuloid.Nombre, AuxDTO.Articuloid.Descripcion, AuxDTO.Articuloid.Marca, AuxDTO.Articuloid.Precioactual, AuxDTO.Articuloid.Cantidad, AuxDTO.Articuloid.Stockmin,AuxDTO.Costo,AuxDTO.Proveedorid.Nombrefantasia,AuxDTO.Articuloid.Estado);
                        //TodosArticulos.PageSize = ControlConfig.DevolverNroMaxPaginacion();
                        TodosArticulos.DataSource = dt;
                        TodosArticulos.DataBind();
                        TodosArticulos.Visible = true;
                        PanelGrilla.Visible = true;
                        PanelPopUp.Attributes["display"] = "none";

                    }
                    else
                    {
                        TodosArticulos.Visible = false;
                        PanelGrilla.Visible = false;
                        PanelPopUp.Attributes["display"] = "none";

                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert();", true);
                    }

                }
            }
        }
        public void RadioSelecionado()
        {
            PanelGrilla.Visible = false;
            if (RadioButtonListBusqueda.SelectedValue == "nombre")
            {
                TxtBuscarMarca.Visible = false;
                BtnBuscarMarca.Visible = false;
                TxtBuscarNombre.Visible = true;
                BtnBuscarNombre.Visible = true;
                TxtBuscarCod.Visible = false;
                BtnBuscarCod.Visible = false;
                PanelPopUp.Attributes["display"] = "none";
                TxtBuscarNombre.Focus();

            }
            else
            {
                if (RadioButtonListBusqueda.SelectedValue == "marca")
                {
                    TxtBuscarMarca.Visible = true;
                    BtnBuscarMarca.Visible = true;
                    TxtBuscarNombre.Visible = false;
                    BtnBuscarNombre.Visible = false;
                    TxtBuscarCod.Visible = false;
                    BtnBuscarCod.Visible = false;
                    PanelPopUp.Attributes["display"] = "none";
                    TxtBuscarMarca.Focus();


                }
                else
                {
                    if (RadioButtonListBusqueda.SelectedValue == "cod")
                    {
                        TxtBuscarMarca.Visible = false;
                        BtnBuscarMarca.Visible = false;
                        TxtBuscarNombre.Visible = false;
                        BtnBuscarNombre.Visible = false;
                        TxtBuscarCod.Visible = true;
                        BtnBuscarCod.Visible = true;
                        PanelPopUp.Attributes["display"] = "none";
                        TxtBuscarCod.Focus();

                    }
                }
            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            ModalPopupExtender2.Show();
            ImageButton ImgB = sender as ImageButton;
            GridViewRow gvRow = (GridViewRow)ImgB.NamingContainer;
            ArtId = Convert.ToInt32(gvRow.Cells[0].Text);
        }

        protected void TodosArticulos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton btnFinalizar = (ImageButton)e.Row.FindControl("ImageButton2");

                string _estado = DataBinder.Eval(e.Row.DataItem, "estado").ToString();

                if (_estado == "no activo") { 
                    e.Row.BackColor = System.Drawing.Color.LightCoral;
                    btnFinalizar.Enabled = false;

                }
                else
                {
                    btnFinalizar.Enabled = true;
                }

            }
        }

        protected void BtnAceptarEliminar_Click(object sender, EventArgs e)
        {
            bool Elim = false;
            if (!_isRefresh)
            {
                if (ControlArticulo.DependeDeProveedor(ArtId).Count == 0)
                {
                    Elim = ControlArticulo.Eliminar(ArtId);
                    if (Elim)
                    {
                        switch (RadioButtonListBusqueda.SelectedValue)
                        {
                            case "nombre":
                                RefrescarGrillaNombre(NombreST);
                                break;
                            case "marca":
                                RefrescarGrillaMarca(MarcaST);
                                break;
                            case "cod":
                                RefrescarGrillaCod(CodBuscado);
                                break;
                        }
                        LimpiarTxtBuscar();

                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "errorelim();", true);
                        Response.Redirect("EditarArticulo.aspx");

                    }
                    else
                    {
                        Response.Write("<script type='text/javascript'> alert('No se pudo eliminar el cliente !!') </script>");
                    }
                }
                else
                {
                    if (ControlArticulo.DeshabilitarHabilitarProveedor(ArtId, "no activo"))
                    {
                        switch (RadioButtonListBusqueda.SelectedValue)
                        {
                            case "nombre":
                                RefrescarGrillaNombre(NombreST);
                                break;
                            case "marca":
                                RefrescarGrillaMarca(MarcaST);
                                break;
                            case "cod":
                                RefrescarGrillaCod(CodBuscado);
                                break;
                        }
                        LimpiarTxtBuscar();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "errorDepende();", true);
                    }
                }
            }
            else
            {
                 Response.Redirect("EditarArticulo.aspx");
            }
        }

        protected void TodosArticulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            TodosArticulos.PageIndex = e.NewPageIndex;
            RefrescarGrillaNombre(NombreST);
            RefrescarGrillaMarca(MarcaST);
            RefrescarGrillaCod(CodBuscado);
            TodosArticulos.DataSource = dt;
            TodosArticulos.DataBind();
            TodosArticulos.PageSize = ControlConfig.DevolverNroMaxPaginacion();
        }
    }
}