using Controladoras;
using EntidadesDTO;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.parser;
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
    public partial class ListarPedido : System.Web.UI.Page
    {
        double AuxTotal = 0;
        C_Articulo ControlArticulo;
        C_Configuracion ControlConfig;
        C_Pedido ControlPedido;
        C_DetallePedido ControlDetallePedido;
        DataTable dt = null;
        DataTable dt2 = null;
        private bool _refreshState;
        private bool _isRefresh;
        List<PedidoDTO> ListaDTO = new List<PedidoDTO>();
        List<PedidoDTO> ListaDTO2 = new List<PedidoDTO>();

        List<DetallePedidoDTO> ListaDetalleDTO = new List<DetallePedidoDTO>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            ControlArticulo = (C_Articulo)Session["ControlArticulo"];
            ControlConfig = (C_Configuracion)Session["ControlConfig"];
            ControlPedido = (C_Pedido)Session["ControlPedido"];
            ControlDetallePedido = (C_DetallePedido)Session["ControlDetallePedido"];
            if (!IsPostBack)
            {
                dt = new DataTable();
                dt2 = new DataTable();
                GridPedidos.PageSize = ControlConfig.DevolverNroMaxPaginacion();
                GridPedidos.DataSource = dt;
                GridPedidos.DataBind();
                GrillaDetalle.PageSize = ControlConfig.DevolverNroMaxPaginacion();
                GrillaDetalle.DataSource = dt2;
                GrillaDetalle.DataBind();
                Refrescar();
            }

            if (RadioButtonList1.SelectedValue == "Cliente")
            {
                TxtFiltroNombre.Visible = true;
                TxtDesde.Visible = false;
                TxtHasta.Visible = false;
                BtnFiltroFecha.Visible = false;
                Refrescar2(TxtFiltroNombre.Text);
            }
            else
            {
                TxtFiltroNombre.Visible = false;
                TxtDesde.Visible = true;
                TxtHasta.Visible = true;
                BtnFiltroFecha.Visible = true;
                RefrescarRango(TxtDesde.Text,TxtHasta.Text);
            }

        }

        public void Refrescar()
        {
            ListaDTO = ControlPedido.DevolverTodo();

            if (ListaDTO.Count != 0)
            {
                dt = new DataTable();
                dt.Columns.Add(new DataColumn("pedidoid", System.Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("nropedido", System.Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("fecha", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("entregado", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("clienteid", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("estado", System.Type.GetType("System.String")));

                foreach (PedidoDTO Auxi in ListaDTO)
                {

                    dt.Rows.Add(Auxi.Pedidoid, Auxi.Nropedido, Auxi.Fecha.ToString("yyyy/MM/dd"), Auxi.Entregado,Auxi.Clienteid.Apellido + " " + Auxi.Clienteid.Nombre, Auxi.Estado);
                }
                GridPedidos.DataSource = dt;
                GridPedidos.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert();", true);
            }
        }

        public void Refrescar2(string TxtFiltro)
        {
            if (ControlPedido.ListarPedidoFiltroCliente(TxtFiltro).Count != 0)
            {
                dt = new DataTable();
                dt.Columns.Add(new DataColumn("pedidoid", System.Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("nropedido", System.Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("fecha", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("entregado", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("clienteid", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("estado", System.Type.GetType("System.String")));

                foreach (PedidoDTO Auxi in ControlPedido.ListarPedidoFiltroCliente(TxtFiltro))
                {

                    dt.Rows.Add(Auxi.Pedidoid, Auxi.Nropedido, Auxi.Fecha.ToString("yyyy/MM/dd"), Auxi.Entregado,Auxi.Clienteid.Apellido + " " + Auxi.Clienteid.Nombre, Auxi.Estado);
                }
                GridPedidos.DataSource = dt;
                GridPedidos.DataBind();
            }
            else
            {
                Refrescar();
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralertCliente();", true);

            }
        }

        public void RefrescarRango(string txtDesde,string txtHasta)
        {
           if (!string.IsNullOrEmpty(txtDesde) && !string.IsNullOrEmpty(txtHasta))
            { 
                if (ControlPedido.RangoTotal(Convert.ToDateTime(txtDesde),Convert.ToDateTime(txtHasta)).Count != 0)
                {
                    dt = new DataTable();
                    dt.Columns.Add(new DataColumn("pedidoid", System.Type.GetType("System.Int32")));
                    dt.Columns.Add(new DataColumn("nropedido", System.Type.GetType("System.Int32")));
                    dt.Columns.Add(new DataColumn("fecha", System.Type.GetType("System.String")));
                    dt.Columns.Add(new DataColumn("entregado", System.Type.GetType("System.String")));
                    dt.Columns.Add(new DataColumn("clienteid", System.Type.GetType("System.String")));
                    dt.Columns.Add(new DataColumn("estado", System.Type.GetType("System.String")));

                    foreach (PedidoDTO Auxi in ControlPedido.RangoTotal(Convert.ToDateTime(txtDesde), Convert.ToDateTime(txtHasta)))
                    {

                        dt.Rows.Add(Auxi.Pedidoid, Auxi.Nropedido, Auxi.Fecha.ToString("yyyy/MM/dd"), Auxi.Entregado,Auxi.Clienteid.Apellido + " " + Auxi.Clienteid.Nombre, Auxi.Estado);
                    }
                    GridPedidos.DataSource = dt;
                    GridPedidos.DataBind();
                }
                else
                {
                    Refrescar();
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralertFecha();", true);
                }
            }
            else
            {
                Refrescar();
            }
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

        protected void GridPedidos_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void GridPedidos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridPedidos.PageIndex = e.NewPageIndex;
            GridPedidos.DataSource = dt;
            GridPedidos.DataBind();
            GridPedidos.PageSize = ControlConfig.DevolverNroMaxPaginacion();
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ImgB = sender as ImageButton;
            GridViewRow gvRow = (GridViewRow)ImgB.NamingContainer;
            RefrescarDetalle(Convert.ToInt32(gvRow.Cells[0].Text));
            ModalPopupExtender2.Show();
        }

        protected void GrillaDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    AuxTotal += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "totalxArt"));
                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[2].Text ="Total:  $"+ Convert.ToDouble(AuxTotal).ToString("#,##0.00");
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Font.Bold = true;
                }
            }
            catch (Exception err)
            {
                string error = err.Message.ToString() + " - " + err.Source.ToString();
            }
        }

        protected void GrillaDetalle_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GrillaDetalle.PageIndex = e.NewPageIndex;
            GrillaDetalle.DataSource = dt2;
            GrillaDetalle.DataBind();
            GrillaDetalle.PageSize = ControlConfig.DevolverNroMaxPaginacion();
        }

        protected void ImageButton2_Click1(object sender, ImageClickEventArgs e)
        {
            ImageButton ImgB = sender as ImageButton;
            GridViewRow gvRow = (GridViewRow)ImgB.NamingContainer;
            ExportarPDF(Convert.ToInt32(gvRow.Cells[0].Text));

        }

        public void ExportarPDF(int nro)
        {
            PedidoDTO PedidoDes = ControlPedido.BuscarNroDTO(nro);
            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                    {
                        GrillaDetalle.AllowPaging = false;
                        GrillaDetalle.Width = 660;
                        GrillaDetalle.CellSpacing = 3;
                        RefrescarDetalle(nro);
                        GrillaDetalle.RenderControl(hw);
                        StringReader sr = new StringReader(sw.ToString());
                        Document pdfDoc = new Document(PageSize.A4, 50f, 50f, 10f, 0f);
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                        pdfDoc.Open();
                        var FontColour = new BaseColor(126, 151, 173);
                        Font LineBreak = FontFactory.GetFont("Cambria", size: 10, color: BaseColor.BLACK);
                        Font LineBreak2 = FontFactory.GetFont("Cambria", size: 13,style: Font.BOLD, color: BaseColor.WHITE);
                        Font LineBreak3 = FontFactory.GetFont("Cambria", size: 15, color: FontColour);
                        Font LineBreak4 = FontFactory.GetFont("Cambria", size: 10, color: FontColour); 
                        var dataFilelogop = Server.MapPath("~/App_Data/pedidotrasp.png");

                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(dataFilelogop);
                        Paragraph parrafo2 = new Paragraph(string.Format(""), LineBreak);
                        parrafo2.SpacingBefore = 200;
                        parrafo2.SpacingAfter = 0;
                        parrafo2.Alignment = 1; 
                        pdfDoc.Add(parrafo2);
                        pdfDoc.Add(Chunk.NEWLINE);
                        img.SetAbsolutePosition(430, 720);
                        img.ScaleToFit(130f, 130F);
                        img.SpacingBefore = 10f;
                        pdfDoc.Add(img);
                        pdfDoc.Add(new Paragraph(string.Format("Dirección: San Martin 7500"), LineBreak));
                        pdfDoc.Add(new Paragraph(string.Format("Tel. 470-0000"), LineBreak));
                        pdfDoc.Add(new Paragraph(string.Format("Email: Proar@gmail.com"), LineBreak));
                        pdfDoc.Add(new Paragraph("\n\n", LineBreak));
                        PdfPTable table = new PdfPTable(2);
                        PdfPCell cell1 = new PdfPCell(new Phrase("Pedido N.º: " + PedidoDes.Nropedido, LineBreak2));
                        PdfPCell cell2 = new PdfPCell(new Phrase(PedidoDes.Fecha.ToString("dd/MM/yyyy"), LineBreak2));
                        cell1.BackgroundColor = new iTextSharp.text.BaseColor(93, 123, 157);
                        cell2.BackgroundColor = new iTextSharp.text.BaseColor(93, 123, 157);
                        cell2.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cell1.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell1.BorderWidth = 0;
                        cell2.BorderWidth = 0;
                        table.WidthPercentage = 100;
                        table.AddCell(cell1);
                        table.AddCell(cell2);
                        pdfDoc.Add(table);
                        pdfDoc.Add(new Paragraph("\n", LineBreak));
                        Paragraph ph = new Paragraph(new Phrase("CLIENTE", LineBreak4));
                        PdfPCell cellcliente = new PdfPCell(ph);
                        cellcliente.Border = Rectangle.BOTTOM_BORDER;
                        cellcliente.BorderColor = new BaseColor(126, 151, 173);
                        cellcliente.BorderWidth = 0.5f;
                        PdfPTable tablecliente = new PdfPTable(1);
                        tablecliente.AddCell(cellcliente);
                        tablecliente.HorizontalAlignment = Element.ALIGN_LEFT;
                        tablecliente.WidthPercentage = 100f;
                        pdfDoc.Add(tablecliente);
                        pdfDoc.Add(new Paragraph(string.Format("Apellido, Nombre: " + PedidoDes.Clienteid.Apellido + " " + PedidoDes.Clienteid.Nombre), LineBreak));
                        pdfDoc.Add(new Paragraph(string.Format("Telefono: " + PedidoDes.Clienteid.Telefono), LineBreak));
                        pdfDoc.Add(new Paragraph(string.Format("Direccion: " + PedidoDes.Clienteid.Direccion), LineBreak));
                        pdfDoc.Add(new Paragraph(string.Format("Email: " + PedidoDes.Clienteid.Email), LineBreak));
                        pdfDoc.Add(Chunk.NEWLINE);
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                        pdfDoc.Close();
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", "attachment;filename=pdfExport.pdf");
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.Write(pdfDoc);
                        Response.End();
                    }
                }
            }
            catch (DocumentException ex)
            {
                throw new DocumentException(ex.Message);
            }
            catch (IOException ex)
            {
                throw new IOException(ex.Message);
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        
        public void RefrescarDetalle(int nro)
        {
            ListaDetalleDTO = ControlDetallePedido.DevolverTodoxID(nro);
            dt2 = new DataTable();
            dt2.Columns.Add(new DataColumn("articuloid", System.Type.GetType("System.String")));
            dt2.Columns.Add(new DataColumn("cantidad", System.Type.GetType("System.Int32")));
            dt2.Columns.Add(new DataColumn("preciovendido", System.Type.GetType("System.Double")));
            dt2.Columns.Add(new DataColumn("totalxArt", System.Type.GetType("System.Double")));

            foreach (DetallePedidoDTO Auxi in ListaDetalleDTO)
            {

                dt2.Rows.Add(Auxi.Articuloid.Nombre + " - " + Auxi.Articuloid.Marca, Auxi.Cantidad, Auxi.Preciovendido.ToString("#,##0.00"), (Auxi.Preciovendido * Auxi.Cantidad).ToString("#,##0.00"));
            }
            GrillaDetalle.DataSource = dt2;
            GrillaDetalle.DataBind();
        }

        protected void TxtFiltroNombre_TextChanged(object sender, EventArgs e)
        {
            if (!_isRefresh)
            {
                GridPedidos.DataSource = ListaDTO2;
                GridPedidos.DataBind();
                if (ControlPedido.ListarPedidoFiltroCliente(TxtFiltroNombre.Text).Count != 0)
                {
                    dt = new DataTable();
                    dt.Columns.Add(new DataColumn("pedidoid", System.Type.GetType("System.Int32")));
                    dt.Columns.Add(new DataColumn("nropedido", System.Type.GetType("System.Int32")));
                    dt.Columns.Add(new DataColumn("fecha", System.Type.GetType("System.String")));
                    dt.Columns.Add(new DataColumn("entregado", System.Type.GetType("System.String")));
                    dt.Columns.Add(new DataColumn("clienteid", System.Type.GetType("System.String")));
                    dt.Columns.Add(new DataColumn("estado", System.Type.GetType("System.String")));

                    foreach (PedidoDTO Auxi in ControlPedido.ListarPedidoFiltroCliente(TxtFiltroNombre.Text))
                    {

                        dt.Rows.Add(Auxi.Pedidoid, Auxi.Nropedido, Auxi.Fecha.ToString("yyyy/MM/dd"), Auxi.Entregado,Auxi.Clienteid.Apellido + " " + Auxi.Clienteid.Nombre, Auxi.Estado);
                    }
                    GridPedidos.DataSource = dt;
                    GridPedidos.DataBind();
                }
                else
                {
                    Refrescar();
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralertCliente();", true);

                }
            }
            else
            {
                Response.Redirect("ListarPedido.aspx");
            }
        }

        protected void BtnFiltroFecha_Click(object sender, EventArgs e)
        {
            if(IsValid)
            { 
                if (!_isRefresh)
                {
                    GridPedidos.DataSource = ListaDTO2;
                    GridPedidos.DataBind();
                    if (ControlPedido.RangoTotal(Convert.ToDateTime(TxtDesde.Text), Convert.ToDateTime(TxtHasta.Text)).Count != 0)
                    {
                        dt = new DataTable();
                        dt.Columns.Add(new DataColumn("pedidoid", System.Type.GetType("System.Int32")));
                        dt.Columns.Add(new DataColumn("nropedido", System.Type.GetType("System.Int32")));
                        dt.Columns.Add(new DataColumn("fecha", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("entregado", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("clienteid", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("estado", System.Type.GetType("System.String")));

                        foreach (PedidoDTO Auxi in ControlPedido.RangoTotal(Convert.ToDateTime(TxtDesde.Text), Convert.ToDateTime(TxtHasta.Text)))
                        {

                            dt.Rows.Add(Auxi.Pedidoid, Auxi.Nropedido, Auxi.Fecha.ToString("yyyy/MM/dd"), Auxi.Entregado,Auxi.Clienteid.Apellido + " " + Auxi.Clienteid.Nombre, Auxi.Estado);
                        }
                        GridPedidos.DataSource = dt;
                        GridPedidos.DataBind();
                    }
                    else
                    {
                        Refrescar();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralertFecha();", true);
                    }
                }
                else
                {
                    Response.Redirect("ListarPedido.aspx");
                }
            }
        }
    }

}
