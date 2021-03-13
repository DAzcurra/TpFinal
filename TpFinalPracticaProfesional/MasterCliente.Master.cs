using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpFinalPracticaProfesional
{
    public partial class MasterCliente : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BotonAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarCliente.aspx");
        }

        protected void BotonEditar_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditarCliente.aspx");
        }

        protected void BotonListar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListarCliente.aspx");
        }

        protected void LBAtras_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }
    }
}