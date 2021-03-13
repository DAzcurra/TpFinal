using Controladoras;
using Entidades;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpFinalPracticaProfesional
{
    public partial class ListarCliente : System.Web.UI.Page
    {
        C_Cliente ControlCliente;
        C_Configuracion ControlConfig;
        List<Cliente> Aux = new List<Cliente>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            ControlCliente = (C_Cliente)Session["ControlCliente"];
            ControlConfig = (C_Configuracion)Session["ControlConfig"];
            TodosClientes.DataSource = ControlCliente.Listar();
            TodosClientes.DataBind();
            Aux = ControlCliente.Listar();
            if (Aux.Count == 0)
            {
                BtnConvtExcel.Enabled = false;
                BtnConvtPDF.Enabled = false;
                BtnConvtWord.Enabled = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert();", true);

            }
            if (!IsPostBack)
            {
                TodosClientes.PageSize = ControlConfig.DevolverNroMaxPaginacion();
                TodosClientes.DataSource = ControlCliente.Listar();
                TodosClientes.DataBind();
            }
        }
        public void ConvertExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=ListaClienteExcel.xls");
            Response.Charset = "";
            Response.ContentType = "application/ms-excel ";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            TodosClientes.AllowPaging = false;
            TodosClientes.DataBind();
            TodosClientes.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        public void ConvertWord()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=ListaClienteWord.doc");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-word ";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            TodosClientes.AllowPaging = false;
            TodosClientes.DataBind();
            TodosClientes.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        public void ConvertPDF()
        {
            Response.AddHeader("content-disposition", "attachment;filename=ListaClientePDF.pdf");
            Response.ContentType = "application/pdf ";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            TodosClientes.AllowPaging = false;
            TodosClientes.DataBind();
            TodosClientes.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document DocPDF = new Document(PageSize.A3, 10f, 10f, 10f, 0f);
            PdfWriter writer = PdfWriter.GetInstance(DocPDF, Response.OutputStream);
            DocPDF.Open();
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, DocPDF, sr);
            DocPDF.Close();
            Response.Write(DocPDF);
            Response.End();
        }

        protected void BtnConvtWord_Click(object sender, EventArgs e)
        {
            ConvertWord();
        }

        protected void BtnConvtPDF_Click(object sender, EventArgs e)
        {
            ConvertPDF();
        }

        protected void BtnConvtExcel_Click(object sender, EventArgs e)
        {
            ConvertExcel();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }

        protected void TodosClientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string _estado = DataBinder.Eval(e.Row.DataItem, "estado").ToString();

                if (_estado == "no activo") { 
                    e.Row.BackColor = System.Drawing.Color.LightCoral;
                }
            }
            
        }

        protected void TodosClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            TodosClientes.PageIndex = e.NewPageIndex;
            TodosClientes.DataSource = ControlCliente.Listar();
            TodosClientes.DataBind();
            TodosClientes.PageSize = ControlConfig.DevolverNroMaxPaginacion();
        }
    }
}