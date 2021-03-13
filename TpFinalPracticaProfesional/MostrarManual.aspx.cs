using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpFinalPracticaProfesional
{
    public partial class MostrarManual : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            var dataFile = Server.MapPath("~/App_Data/ManualDelUsuario.pdf");
            byte[] bytes = System.IO.File.ReadAllBytes(dataFile);
            
            byte[] ArchivoPDF = bytes;
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = false;
            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("content-disposition", "inline");
            HttpContext.Current.Response.AddHeader("Content-Length", ArchivoPDF.Length.ToString());
            HttpContext.Current.Response.Charset = "UTF-8";
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.Private);
            HttpContext.Current.Response.BinaryWrite(ArchivoPDF);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.End();
        }
    }
}