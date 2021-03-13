using Controladoras;
using Entidades;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpFinalPracticaProfesional
{
    public partial class ListarProveedor : System.Web.UI.Page
    {
        C_Proveedor ControlProveedor;
        C_Configuracion ControlConfig;
        List<Proveedor> Aux = new List<Proveedor>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            ControlProveedor = (C_Proveedor)Session["ControlProveedor"];
            ControlConfig = (C_Configuracion)Session["ControlConfig"];
            TodosProveedores.DataSource = ControlProveedor.Listar();
            TodosProveedores.DataBind();
            Aux = ControlProveedor.Listar();
            if (Aux.Count == 0)
            {
                BtnConvtExcel.Enabled = false;
                BtnConvtPDF.Enabled = false;
                BtnConvtWord.Enabled = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert();", true);
            }
            if (!IsPostBack)
            {
                TodosProveedores.PageSize = ControlConfig.DevolverNroMaxPaginacion();
                TodosProveedores.DataSource = ControlProveedor.Listar();
                TodosProveedores.DataBind();
            }
        }

        protected void BtnConvtWord_Click(object sender, EventArgs e)
        {
            ConvertWord();
        }

        protected void BtnConvtExcel_Click(object sender, EventArgs e)
        {
            ConvertExcel();
        }

        protected void BtnConvtPDF_Click(object sender, EventArgs e)
        {
            ConvertPDF();
        }
        public void ConvertExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=ListaProveedorExcel.xls");
            Response.Charset = "";
            Response.ContentType = "application/ms-excel ";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            TodosProveedores.AllowPaging = false;
            TodosProveedores.DataBind();
            TodosProveedores.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        public void ConvertWord()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=ListaProveedorWord.doc");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-word ";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            TodosProveedores.AllowPaging = false;
            TodosProveedores.DataBind();
            TodosProveedores.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        public void ConvertPDF()
        {
            Response.AddHeader("content-disposition", "attachment;filename=ListaProveedorPDF.pdf");
            Response.ContentType = "application/pdf ";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            TodosProveedores.AllowPaging = false;
            TodosProveedores.DataBind();
            TodosProveedores.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document DocPDF = new Document(PageSize.A3, 10f, 10f, 10f, 0f);
            PdfWriter writer = PdfWriter.GetInstance(DocPDF, Response.OutputStream);
            DocPDF.Open();
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, DocPDF, sr);
            DocPDF.Close();
            Response.Write(DocPDF);
            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }

        protected void TodosProveedores_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string _estado = DataBinder.Eval(e.Row.DataItem, "estado").ToString();

                if (_estado == "no activo")
                {
                    e.Row.BackColor = System.Drawing.Color.LightCoral;
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
    }
}