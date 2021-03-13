using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpFinalPracticaProfesional
{
    public partial class MasterFacturacion : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BotonAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarFactura.aspx");
        }

        protected void LBCliente_Click(object sender, EventArgs e)
        {
            Response.Redirect("MenuFacturacion.aspx");
        }


        protected void BotonListar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListarFactura.aspx");
        }

        protected void LBAtras_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }
    }
}