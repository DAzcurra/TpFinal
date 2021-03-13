using Controladoras;
using Entidades;
using EntidadesDTO;
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
    public partial class ListaArticulo : System.Web.UI.Page
    {
        C_Articulo ControlArticulo;
        C_Configuracion ControlConfig;
        List<ArticuloxProveedorDTO> Aux = null;
        List<ArticuloxProveedorDTO> ListaDTO = new List<ArticuloxProveedorDTO>();
        DataTable dt = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            ControlArticulo = (C_Articulo)Session["ControlArticulo"];
            ControlConfig = (C_Configuracion)Session["ControlConfig"];
            ListaDTO = ControlArticulo.Listar();
            TodosArticulos.DataSource = Aux;
            TodosArticulos.DataBind();
            if (ListaDTO.Count != 0)
            {
                dt = new DataTable();
                dt.Columns.Add(new DataColumn("articuloid", System.Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("nombre", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("descripcion", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("marca", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("precioactual", System.Type.GetType("System.Double")));
                dt.Columns.Add(new DataColumn("cantidad", System.Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("stockmin", System.Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("costo", System.Type.GetType("System.Double")));
                dt.Columns.Add(new DataColumn("nombrefantasia", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("estado", System.Type.GetType("System.String")));
                foreach (ArticuloxProveedorDTO Auxi in ListaDTO)
                {
                    dt.Rows.Add(Auxi.Articuloid.Articuloid, Auxi.Articuloid.Nombre, Auxi.Articuloid.Descripcion, Auxi.Articuloid.Marca, Auxi.Articuloid.Precioactual.ToString("#,##0.00"), Auxi.Articuloid.Cantidad, Auxi.Articuloid.Stockmin, Auxi.Costo.ToString("#,##0.00"), Auxi.Proveedorid.Nombrefantasia, Auxi.Articuloid.Estado);
                }
                TodosArticulos.DataSource = dt;
                TodosArticulos.DataBind();
            }
             else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert();", true);
                TodosArticulos.DataSource = Aux;
                TodosArticulos.DataBind();
                BtnConvtExcel.Enabled = false;
                BtnConvtPDF.Enabled = false;
                BtnConvtWord.Enabled = false;
            }

            if(!IsPostBack)
            {
                TodosArticulos.PageSize = ControlConfig.DevolverNroMaxPaginacion();
                TodosArticulos.DataSource = dt;
                TodosArticulos.DataBind();
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
            "attachment;filename=ListaArticuloExcel.xls");
            Response.Charset = "";
            Response.ContentType = "application/ms-excel ";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            TodosArticulos.AllowPaging = false;
            TodosArticulos.DataBind();
            TodosArticulos.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        public void ConvertWord()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=ListaArticuloWord.doc");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-word ";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            TodosArticulos.AllowPaging = false;
            TodosArticulos.DataBind();
            TodosArticulos.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        public void ConvertPDF()
        {
            Response.AddHeader("content-disposition", "attachment;filename=ListaArticuloPDF.pdf");
            Response.ContentType = "application/pdf ";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            TodosArticulos.AllowPaging = false;
            TodosArticulos.DataBind();
            TodosArticulos.RenderControl(hw);
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

        protected void TodosArticulos_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void TodosArticulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            TodosArticulos.PageIndex = e.NewPageIndex;
            TodosArticulos.DataSource = dt;
            TodosArticulos.DataBind();
            TodosArticulos.PageSize = ControlConfig.DevolverNroMaxPaginacion();
        }
    }
}