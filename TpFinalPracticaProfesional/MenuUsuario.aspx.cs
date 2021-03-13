using Controladoras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpFinalPracticaProfesional
{
    public partial class MenuUsuario : System.Web.UI.Page
    {
        private bool _refreshState;
        private bool _isRefresh;
        C_Configuracion ControlConfig;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            ControlConfig =(C_Configuracion)Session["ControlConfig"];
            txtcontraseñaNuevaR.Enabled = false;
            txtcontraseñaNueva.Enabled = false;
            txtusernuevo.Enabled = false;
            BtnModificar.Enabled = false;
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

        protected void BtnModificar_Click(object sender, EventArgs e)
        {

            if (IsValid)
            {
                if (ControlConfig.ModificarUsuario(txtusernuevo.Text.ToLower(),txtcontraseñaNueva.Text.ToLower()))
                {
                    if (!_isRefresh)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert();", true);
                        Response.AddHeader("REFRESH", "2;URL=MenuUsuario.aspx");
                    }
                    else
                    {
                        Response.Redirect("MenuUsuario.aspx");
                    }
                }
            }
        }

        protected void btnVerificar_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if(ControlConfig.BuscarUsuario(txtUsuarioActual.Text.ToLower(), txtcontraseñaactual.Text.ToLower()))
                {
                        if (!_isRefresh)
                        {
                            txtcontraseñaNuevaR.Enabled = true;
                            txtcontraseñaNueva.Enabled = true;
                            txtusernuevo.Enabled = true;
                            BtnModificar.Enabled = true;
                            txtcontraseñaNuevaR.Focus();
                        }
                        else
                        {
                            Response.Redirect("MenuUsuario.aspx");
                        }
                    }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert();", true);
                }
            }
        }
    }
}