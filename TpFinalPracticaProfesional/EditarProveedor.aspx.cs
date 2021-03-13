using Controladoras;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpFinalPracticaProfesional
{
    public partial class EditarProveedor : System.Web.UI.Page
    {
        C_Proveedor ControlProveedor;
        C_Configuracion ControlConfig;
        List<Proveedor> Lista = new List<Proveedor>();
        Proveedor Aux = null;
        static string ProvCuit;
        private bool _refreshState;
        private bool _isRefresh;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            ControlProveedor = (C_Proveedor)Session["ControlProveedor"];
            ControlConfig = (C_Configuracion)Session["ControlConfig"];

            if (!IsPostBack)
            {
                TodosProveedores.PageSize = ControlConfig.DevolverNroMaxPaginacion();
                RefrecarBusqueda();
            }

            if (RadioButtonListBusqueda.SelectedValue == "Cuit")
            {
                TxtBuscarApellido.Visible = false;
                BtnBuscarApellido.Visible = false;
                TxtBuscarCuit.Visible = true;
                BtnBuscarCuit.Visible = true;
                TxtBuscarNFantasia.Visible = false;
                BtnBuscarNFantasia.Visible = false;
                TxtBuscarRSocial.Visible = false;
                BtnBuscarRSocial.Visible = false;
                TxtBuscarCuit.Focus();
            }
            else
            {
                if (RadioButtonListBusqueda.SelectedValue == "Apellido")
                {
                    TxtBuscarApellido.Visible = true;
                    BtnBuscarApellido.Visible = true;
                    TxtBuscarCuit.Visible = false;
                    BtnBuscarCuit.Visible = false;
                    TxtBuscarNFantasia.Visible = false;
                    BtnBuscarNFantasia.Visible = false;
                    TxtBuscarRSocial.Visible = false;
                    BtnBuscarRSocial.Visible = false;
                    TxtBuscarApellido.Focus();
                }
                else
                {
                    if (RadioButtonListBusqueda.SelectedValue == "NombredeFantasia")
                    {
                        TxtBuscarApellido.Visible = false;
                        BtnBuscarApellido.Visible = false;
                        TxtBuscarCuit.Visible = false;
                        BtnBuscarCuit.Visible = false;
                        TxtBuscarNFantasia.Visible = true;
                        BtnBuscarNFantasia.Visible = true;
                        TxtBuscarRSocial.Visible = false;
                        BtnBuscarRSocial.Visible = false;
                        TxtBuscarNFantasia.Focus();
                    }
                    else
                    {
                        TxtBuscarApellido.Visible = false;
                        BtnBuscarApellido.Visible = false;
                        TxtBuscarCuit.Visible = false;
                        BtnBuscarCuit.Visible = false;
                        TxtBuscarNFantasia.Visible = false;
                        BtnBuscarNFantasia.Visible = false;
                        TxtBuscarRSocial.Visible = true;
                        BtnBuscarRSocial.Visible = true;
                        TxtBuscarRSocial.Focus();
                    }
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

        protected void BtnBuscarCuit_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (RadioButtonListBusqueda.SelectedValue == "Cuit")
                {
                    Aux = ControlProveedor.BuscarxCuit(TxtBuscarCuit.Text.ToLower());
                    if (Aux != null)
                    {
                        Lista.Add(Aux);
                        TodosProveedores.DataSource = Lista;
                        TodosProveedores.DataBind();
                        TodosProveedores.Visible = true;
                        PanelGrilla.Visible = true;
                    }
                    else
                    {
                        TodosProveedores.Visible = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert();", true);
                    }

                }
            }
        }

        protected void BtnBuscarApellido_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (RadioButtonListBusqueda.SelectedValue == "Apellido")
                {
                    Lista = ControlProveedor.BuscarxApellido(TxtBuscarApellido.Text.ToLower());

                    if (Lista.Count != 0)
                    {
                        TodosProveedores.DataSource = Lista;
                        TodosProveedores.DataBind();
                        TodosProveedores.Visible = true;
                        PanelGrilla.Visible = true;
                    }
                    else
                    {
                        TodosProveedores.Visible = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert();", true);
                    }
                }
            }
        }

        protected void BtnBuscarNFantasia_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (RadioButtonListBusqueda.SelectedValue == "NombredeFantasia")
                {
                    Lista = ControlProveedor.BuscarxNombreFantasia(TxtBuscarNFantasia.Text.ToLower());
                    if (Lista.Count != 0)
                    {
                        TodosProveedores.DataSource = Lista;
                        TodosProveedores.DataBind();
                        TodosProveedores.Visible = true;
                        PanelGrilla.Visible = true;
                    }
                    else
                    {
                        TodosProveedores.Visible = false;
                        PanelGrilla.Visible = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert();", true);
                    }

                }
            }
        }

        protected void BtnBuscarRSocial_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (RadioButtonListBusqueda.SelectedValue == "RazonSocial")
                {
                    Lista = ControlProveedor.BuscarxRazonSocial(TxtBuscarRSocial.Text.ToLower());
                    if (Lista.Count != 0)
                    {
                        TodosProveedores.DataSource = Lista;
                        TodosProveedores.DataBind();
                        TodosProveedores.Visible = true;
                        PanelGrilla.Visible = true;
                    }
                    else
                    {
                        TodosProveedores.Visible = false;
                        PanelGrilla.Visible = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert();", true);
                    }

                }
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ImgB = sender as ImageButton;
            GridViewRow gvRow = (GridViewRow)ImgB.NamingContainer;
            TxtNFantasia.Focus();
            TxtNFantasia.Text = gvRow.Cells[0].Text;
            TxtRazonS.Text = gvRow.Cells[1].Text;
            TxtNombre.Text = gvRow.Cells[2].Text;
            TxtApellido.Text = gvRow.Cells[3].Text;
            TextTelefono.Text = gvRow.Cells[4].Text;
            TxtEmail.Text = gvRow.Cells[5].Text;
            TxtCuit.Text = gvRow.Cells[6].Text;
            TxtDirecion.Text = gvRow.Cells[7].Text;
            int i = 0;
            foreach (var item in ddlestado.Items)
            {
                if (item.ToString().ToLower() == gvRow.Cells[8].Text)
                {
                    i = ddlestado.Items.IndexOf((ListItem)item);
                    break;
                }
            }
            ddlestado.SelectedIndex = i;
            ddlestado.Items[i].Selected = true;
            ProvCuit = gvRow.Cells[6].Text;
            this.ModalPopupExtender1.Show();
            Labelmgg.Visible = false;

        }

        protected void BtnEditAceptar_Click(object sender, EventArgs e)
        {
            Proveedor Aux = null;
            if (IsValid)
            {

                Aux = new Proveedor()
                {

                    Proveedorid = 0,
                    Nombrefantasia = TxtNFantasia.Text.ToLower(),
                    Razonsocial = TxtRazonS.Text.ToLower(),
                    Nombre = TxtNombre.Text.ToLower(),
                    Apellido = TxtApellido.Text.ToLower(),
                    Telefono = TextTelefono.Text,
                    Email = TxtEmail.Text.ToLower(),
                    Cuit = TxtCuit.Text,
                    Direccion = TxtDirecion.Text.ToLower(),
                    Estado = Convert.ToString(ddlestado.SelectedValue)
                };

                if (!_isRefresh)
                {
                    if (ControlProveedor.Modificar(ProvCuit, Aux))
                    {
                        RefrecarBusqueda();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert();", true);
                        Labelmgg.Visible = false;
                    }
                    else
                    {
                        ModalPopupExtender1.Show();
                        Labelmgg.Visible = true;
                    }
                }
                else
                {
                    Response.Redirect("EditarProveedor.aspx");
                }
            }
        }
        public void LimpiarTxtBuscar()
        {
            TxtBuscarRSocial.Text = "";
            TxtBuscarApellido.Text = "";
            TxtBuscarCuit.Text = "";
            TxtBuscarNFantasia.Text = "";
        }
        public void RefrecarBusqueda()
        {
            if (RadioButtonListBusqueda.SelectedValue == "Apellido")
            {
                Lista = ControlProveedor.BuscarxApellido(TxtBuscarApellido.Text.ToLower());
                TodosProveedores.DataSource = Lista;
                TodosProveedores.DataBind();
                TodosProveedores.Visible = true;
            }
            else
            {
                if (RadioButtonListBusqueda.SelectedValue == "RazonSocial")
                {
                    Lista = ControlProveedor.BuscarxRazonSocial(TxtBuscarRSocial.Text.ToLower());
                    TodosProveedores.DataSource = Lista;
                    TodosProveedores.DataBind();
                    TodosProveedores.Visible = true;
                }
                else
                {
                    if (RadioButtonListBusqueda.SelectedValue == "NombredeFantasia")
                    {
                        Lista = ControlProveedor.BuscarxNombreFantasia (TxtBuscarNFantasia.Text.ToLower());
                        TodosProveedores.DataSource = Lista;
                        TodosProveedores.DataBind();
                        TodosProveedores.Visible = true;
                    }
                    else
                    {
                        Aux = ControlProveedor.BuscarxCuit(TxtBuscarCuit.Text.ToLower());
                        if(Aux != null)
                        {
                           Lista.Add(Aux);
                           TodosProveedores.DataSource = Lista;
                           TodosProveedores.DataBind();
                           TodosProveedores.Visible = true;
                        }
                    }
                }
            }
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (Convert.ToInt64(TxtBuscarCuit.Text) >= 0)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ImgB = sender as ImageButton;
            GridViewRow gvRow = (GridViewRow)ImgB.NamingContainer;
            ProvCuit = gvRow.Cells[6].Text;
            ModalPopupExtender2.Show();
        }

        protected void TodosProveedores_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string _estado = DataBinder.Eval(e.Row.DataItem, "estado").ToString();
                ImageButton btnFinalizar = (ImageButton)e.Row.FindControl("ImageButton2");

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

        protected void TodosProveedores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            TodosProveedores.PageIndex = e.NewPageIndex;
            TodosProveedores.DataSource = ControlProveedor.Listar();
            TodosProveedores.DataBind();
            TodosProveedores.PageSize = ControlConfig.DevolverNroMaxPaginacion();
        }

        protected void BtnAceptarEliminar_Click1(object sender, EventArgs e)
        {
            bool Elim = false;
            if (!_isRefresh)
            {
                if(ControlProveedor.DependeDeProveedor(ProvCuit).Count == 0)
                {
                    Elim = ControlProveedor.Eliminar(ProvCuit);

                    if (Elim)
                    {
                        RefrecarBusqueda();
                        LimpiarTxtBuscar();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "errorelim();", true);
                    }
                    else
                    {
                        Response.Write("<script type='text/javascript'> alert('No se pudo eliminar el proveedor !!') </script>");
                    }
                }
                else
                {
                    if(ControlProveedor.DeshabilitarHabilitarProveedor(ProvCuit, "no activo"))
                    {
                        RefrecarBusqueda();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "errorDepende();", true);
                    }
                }
            }
            else
            {
                Response.Redirect("EditarProveedor.aspx");
            }
        }
    }
}