using Controladoras;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.tool.xml;
using Entidades;

namespace TpFinalPracticaProfesional
{
    public partial class ListaEmpleado : System.Web.UI.Page
    {
        C_Empleado ControlEmpleado;
        C_Configuracion ControlConfig;
        List<Empleado> Aux = new List<Empleado>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            ControlConfig = (C_Configuracion)Session["ControlConfig"];
            ControlEmpleado = (C_Empleado)Session["ControlEmpleado"];
            TodosEmpleados.DataSource = ControlEmpleado.Listar();
            TodosEmpleados.DataBind();
            TodosEmpleados.Columns[1].Visible = true;
            if (ControlEmpleado.Listar().Count == 0)
            {
                BtnConvtExcel.Enabled = false;
                BtnConvtPDF.Enabled = false;
                BtnConvtWord.Enabled = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert();", true);
            }

            if (!IsPostBack)
            {
                TodosEmpleados.PageSize = ControlConfig.DevolverNroMaxPaginacion();
                TodosEmpleados.DataSource = ControlEmpleado.Listar();
                TodosEmpleados.DataBind();
            }
        }

        public void ConvertExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=ListaEmpleadoExcel.xls");
            Response.Charset = "";
            Response.ContentType = "application/ms-excel ";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            TodosEmpleados.AllowPaging = false;
            TodosEmpleados.DataBind();
            TodosEmpleados.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            Response.Write("<script type='text/javascript'> alert(' Se Exporto a Excel Correctamente !!') </script>");
        }

        public void ConvertWord()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=ListaEmpleadoWord.doc");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-word ";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            TodosEmpleados.AllowPaging = false;
            TodosEmpleados.DataBind();
            TodosEmpleados.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            Response.Write("<script type='text/javascript'> alert(' Se Exporto a Word Correctamente !!') </script>");
        }

        public void ConvertPDF()
        {
            Response.AddHeader("content-disposition", "attachment;filename=ListaEmpleadoPDF.pdf");
            Response.ContentType = "application/pdf ";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            TodosEmpleados.AllowPaging = false;
            TodosEmpleados.DataBind();
            TodosEmpleados.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document DocPDF = new Document(PageSize.A3, 10f, 10f, 10f, 0f);
            PdfWriter writer = PdfWriter.GetInstance(DocPDF, Response.OutputStream);
            DocPDF.Open();
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, DocPDF, sr);
            DocPDF.Close();
            Response.Write(DocPDF);
            Response.End();
            Response.Write("<script type='text/javascript'> alert(' Se Exporto a PDF Correctamente !!') </script>");
        }


        protected void BotonAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarEmpleado.aspx");
        }

        protected void BotonEditar_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditarEmpleado.aspx");
        }

        protected void BotonListar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListaEmpleado.aspx");
        }

        protected void LBAtras_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }

        protected void BtnConvtWord_Click(object sender, EventArgs e)
        {
            ConvertWord();
        }

        protected void BtnConvtExcel_Click(object sender, EventArgs e)
        {
            ConvertExcel();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }

        protected void BtnConvtPDF_Click(object sender, EventArgs e)
        {
            ConvertPDF();
        }

        protected void TodosEmpleados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            TodosEmpleados.PageIndex = e.NewPageIndex;
            TodosEmpleados.DataSource = ControlEmpleado.Listar();
            TodosEmpleados.DataBind();
            TodosEmpleados.PageSize = ControlConfig.DevolverNroMaxPaginacion();
        }

        protected void TodosEmpleados_RowDataBound(object sender, GridViewRowEventArgs e)
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
    }
}