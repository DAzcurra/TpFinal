using Controladoras;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace TpFinalPracticaProfesional
{
    public partial class AgregarArticulo : System.Web.UI.Page
    {
        C_Articulo ControlArticulo;
        C_Proveedor ControlProveedor;
        C_ArticuloxProveedor ControlArticuloxProveedor;
        private bool _refreshState;
        private bool _isRefresh;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            ControlArticulo = (C_Articulo)Session["ControlArticulo"];
            ControlProveedor = (C_Proveedor)Session["ControlProveedor"];
            ControlArticuloxProveedor = (C_ArticuloxProveedor)Session["ControlArticuloxProveedor"];
            TxtPrecioAc.Attributes.Add("onkeypress", "javascript:return SoloNum(event); ");
            txtcosto.Attributes.Add("onkeypress", "javascript:return SoloNum(event); ");
            if(!IsPostBack)
            { 
                DdlProveedores.DataSource = null;
                DdlProveedores.DataValueField = "proveedorid";
                DdlProveedores.DataTextField = "nombrefantasia";
                DdlProveedores.DataSource = ControlProveedor.ListarActivos();
                DdlProveedores.DataBind();
                txtcod.Focus();
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

        protected void BtnNuevoArticulo_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if(!_isRefresh)
                { 
                int cod = Convert.ToInt32(DdlProveedores.SelectedValue);
                ControlArticulo.Agregar(Convert.ToInt32( txtcod.Text), TxtNombre.Text.ToLower(), TxtDescripcion.Text.ToLower(), TxtMarca.Text.ToLower(), Convert.ToDouble(TxtPrecioAc.Text), Convert.ToInt32(TxtCantidad.Text), Convert.ToInt32(TxtStockMin.Text));
                ControlArticuloxProveedor.Agregar(Convert.ToInt32(txtcod.Text), cod, Convert.ToDouble(txtcosto.Text));
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert();", true);
                Limpiar();
                }
                else
                {
                    Response.Redirect("AgregarArticulo.aspx");
                }
            }
        }
        public void Limpiar()
        {
            txtcosto.Text = "";
            TxtCantidad.Text = "";
            TxtNombre.Text = "";
            TxtDescripcion.Text = "";
            TxtMarca.Text = "";
            TxtPrecioAc.Text = "";
            TxtStockMin.Text = "";
            txtcod.Text = "";
        }

        protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (Convert.ToInt32(TxtStockMin.Text) >= 0)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (Convert.ToInt32(TxtCantidad.Text) >= 0)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (Convert.ToDouble(TxtPrecioAc.Text) >= 0)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void CustomValidator4_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (Convert.ToDouble(txtcosto.Text) >= 0)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void CustomValidator5_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (Convert.ToInt32(txtcod.Text) >= 0)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void CustomValidator6_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ControlArticulo.BuscarXId(Convert.ToInt32(txtcod.Text)) !=null)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
    }
}