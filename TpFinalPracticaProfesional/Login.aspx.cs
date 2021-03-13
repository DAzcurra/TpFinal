using Controladoras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpFinalPracticaProfesional
{
    public partial class Login : System.Web.UI.Page
    {
        C_Configuracion ControlConfig;

        protected void Page_Load(object sender, EventArgs e)
        {
            ControlConfig = (C_Configuracion)Session["ControlConfig"];
        }

        protected void BotonIngresar_Click(object sender, EventArgs e)
        {
            if(IsValid)
            {
                if(ControlConfig.BuscarUsuario(TxtUsuario.Text.ToLower(),TxtContraseña.Text.ToLower()))
                {
                    Session["Login"] = 1;
                    Response.Redirect("index.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert();", true);
                }
            }
            
        }
    }
}