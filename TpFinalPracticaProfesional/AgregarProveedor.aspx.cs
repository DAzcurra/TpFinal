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
    public partial class AgregarProveedor : System.Web.UI.Page
    {
        C_Proveedor ControlProveedor;
        Proveedor AuxProv = null;
        List<Proveedor> Lista = new List<Proveedor>();
        private bool _refreshState;
        private bool _isRefresh;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            ControlProveedor = (C_Proveedor)Session["ControlProveedor"];
            TxtNFantasia.Focus();
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

        protected void BtnNuevoCliente_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (!_isRefresh)
                {
                    if (ControlProveedor.Agregar(TxtNFantasia.Text.ToLower(), TxtRazonS.Text.ToLower(), TxtNombre.Text.ToLower(), TxtApellido.Text.ToLower(), TextTelefono.Text, TxtEmail.Text.ToLower(), TxtCuit.Text, TxtDirecion.Text.ToLower()))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert();", true);
                        Limpiar();
                    }
                }
                else
                {
                    Response.Redirect("AgregarProveedor.aspx");
                }
            }

        }
        public void Limpiar()
        {
            TxtNFantasia.Text = "";
            TxtApellido.Text = "";
            TxtNombre.Text = "";
            TextTelefono.Text = "";
            TxtEmail.Text = "";
            TxtDirecion.Text = "";
            TxtRazonS.Text = "";
            TxtCuit.Text = "";
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            AuxProv = ControlProveedor.BuscarxCuit(TxtCuit.Text);
                if (AuxProv != null)
                {
                    args.IsValid = false;
                }
                else
                {
                    args.IsValid = true;
                }
        }

        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (Convert.ToInt64(TxtCuit.Text) >= 0)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (Convert.ToInt32(TextTelefono.Text) >= 0)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }
    }
}