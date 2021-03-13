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
    public partial class AgregarCliente : System.Web.UI.Page
    {
        C_Cliente ControlCliente;
        Cliente AuxCliente = null;
        private bool _refreshState;
        private bool _isRefresh;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            ControlCliente = (C_Cliente)Session["ControlCliente"];
            TxtRazonS.Focus();
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
                if (Convert.ToInt32(TextTelefono.Text) >= 0)
                {
                    string AuxCliente = DropDownList1.SelectedValue;
                    if (ControlCliente.Buscarcuil(TxtCuil.Text)==null)
                    {
                        if (!_isRefresh)
                        {
                            ControlCliente.Agregar(TxtNombre.Text.ToLower(), TxtApellido.Text.ToLower(), TextTelefono.Text, TxtEmail.Text.ToLower(), TxtDirecion.Text.ToLower(), TxtCuil.Text, TxtRazonS.Text.ToLower(), AuxCliente.ToLower());
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert();", true);
                            Limpiar();
                        }
                        else
                        {
                            Response.Redirect("AgregarCliente.aspx");
                        }
                    }

                }
                else
                {
                    TextTelefono.Text = "";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "NegativoError();", true);
                }
            }
        }

        public void Limpiar()
        {
            TxtApellido.Text = "";
            TxtNombre.Text = "";
            TextTelefono.Text = "";
            TxtEmail.Text = "";
            TxtDirecion.Text = "";
            TxtRazonS.Text = "";
            TxtCuil.Text = "";
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
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
       
        protected void CustomValidator2_ServerValidate1(object source, ServerValidateEventArgs args)
        {
            if (Convert.ToInt64(TxtCuil.Text) >= 0)
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
            if (ControlCliente.Buscarcuil(TxtCuil.Text) != null)
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