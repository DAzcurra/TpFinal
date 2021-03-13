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
    public partial class AgregarEmpleado : System.Web.UI.Page
    {
        C_Empleado ControlEmpleado;
        Empleado AuxEmp = null;
        private bool _refreshState;
        private bool _isRefresh;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            ControlEmpleado = (C_Empleado)Session["ControlEmpleado"];
            TxtNombre.Focus();
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
            AuxEmp = ControlEmpleado.Buscar(TxtCuil.Text);
            if (IsValid)
            {
                if(AuxEmp == null)
                {
                    if (!_isRefresh)
                    {
                        ControlEmpleado.Agregar(TxtNombre.Text.ToLower(), TxtApellido.Text.ToLower(), TxtTelefono.Text.ToLower(), TxtEmail.Text.ToLower(), TxtDirecion.Text.ToLower(), TxtCuil.Text.ToLower());
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert();", true);
                        Limpiar();
                    }
                    else
                    {
                        Response.Redirect("AgregarEmpleado.aspx");
                    }
                } 
            }
        }

        public void Limpiar()
        {
            TxtApellido.Text = "";
            TxtNombre.Text = "";
            TxtTelefono.Text = "";
            TxtEmail.Text = "";
            TxtDirecion.Text = "";
            TxtCuil.Text = "";
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            AuxEmp = ControlEmpleado.Buscar(TxtCuil.Text);
            if (AuxEmp != null)
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
            
            if (Convert.ToInt64(TxtCuil.Text)>=0)
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
            if (Convert.ToInt32(TxtTelefono.Text) >= 0)
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