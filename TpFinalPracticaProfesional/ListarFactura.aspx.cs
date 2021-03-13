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
    public partial class ListarFactura : System.Web.UI.Page
    {
        List<Facturacion_VentaDTO> ListaDTO = new List<Facturacion_VentaDTO>();
        List<DetallePedidoDTO> ListaDetalleDTO = new List<DetallePedidoDTO>();
        Facturacion_VentaDTO AuxFactDTO = null;
        DataTable dt = null;
        DataTable dt2 = null;
        C_Configuracion ControlConfig;
        C_Facturacion ControlFacturacion;
        C_DetallePedido ControlDetallePedido;
        double AuxTotal = 0;
        double AuxTotal2 = 0;
        char Tipo;

        List<Facturacion_VentaDTO> ListaDTO2 = new List<Facturacion_VentaDTO>();

        private bool _refreshState;
        private bool _isRefresh;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            ControlConfig = (C_Configuracion)Session["ControlConfig"];
            ControlFacturacion = (C_Facturacion)Session["ControlFacturacion"];
            ControlDetallePedido = (C_DetallePedido)Session["ControlDetallePedido"];
            
            if (!IsPostBack)
            {
                dt = new DataTable();
                dt2 = new DataTable();
                GridFactura.PageSize = ControlConfig.DevolverNroMaxPaginacion();
                GridFactura.DataSource = dt;
                GridFactura.DataBind();
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
                RefrescarRango(TxtDesde.Text, TxtHasta.Text);
            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ImgB = sender as ImageButton;
            GridViewRow gvRow = (GridViewRow)ImgB.NamingContainer;
            Tipo = Convert.ToChar(gvRow.Cells[6].Text);
            RefrescarDetalle(Convert.ToInt32(gvRow.Cells[0].Text), Convert.ToChar(gvRow.Cells[6].Text));
            ModalPopupExtender2.Show();
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

        protected void GridFactura_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridFactura.PageIndex = e.NewPageIndex;
            GridFactura.DataSource = dt;
            GridFactura.DataBind();
            GridFactura.PageSize = ControlConfig.DevolverNroMaxPaginacion();
        }

        protected void GrillaDetalle_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GrillaDetalle.PageIndex = e.NewPageIndex;
            GrillaDetalle.DataSource = dt2;
            GrillaDetalle.DataBind();
            GrillaDetalle.PageSize = ControlConfig.DevolverNroMaxPaginacion();
        }

        protected void ImageButtonDes_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ImgB = sender as ImageButton;
            GridViewRow gvRow = (GridViewRow)ImgB.NamingContainer;
            Tipo = Convert.ToChar(gvRow.Cells[6].Text);
            ExportarPDF(Convert.ToInt32(gvRow.Cells[0].Text),Convert.ToChar(gvRow.Cells[6].Text));
        }

        public void ExportarPDF(int nro,char tipof)
        {
            AuxFactDTO = ControlFacturacion.Buscar(nro, tipof);
            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                    {
                        GrillaDetalle.AllowPaging = false;
                        GrillaDetalle.Width = 660;
                        GrillaDetalle.CellSpacing = 3;
                        RefrescarDetalle(nro,tipof);
                        GrillaDetalle.RenderControl(hw);
                        StringReader sr = new StringReader(sw.ToString());
                        Document pdfDoc = new Document(PageSize.A4, 50f, 50f, 10f, 0f);
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                        pdfDoc.Open();
                        var FontColour = new BaseColor(126, 151, 173);
                        Font LineBreak = FontFactory.GetFont("Cambria", size: 10, color: BaseColor.BLACK);
                        Font LineBreak2 = FontFactory.GetFont("Cambria", size: 13, style: Font.BOLD, color: BaseColor.WHITE);
                        Font LineBreak3 = FontFactory.GetFont("Cambria", size: 15, color: FontColour);
                        Font LineBreak4 = FontFactory.GetFont("Cambria", size: 10, color: FontColour);
                        //logo local
                        var dataFilelogo = Server.MapPath("~/App_Data/pedidotrasp.png");
                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(dataFilelogo);
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
                        float[] columnWidths = { 5, 1, 5 };
                        PdfPTable table = new PdfPTable(columnWidths);
                        PdfPCell cell1 = new PdfPCell(new Phrase("Factura N.º: " + AuxFactDTO.Cod_factura.ToString("00000000"), LineBreak2));
                        PdfPCell cell2 = new PdfPCell(new Phrase(AuxFactDTO.Tipodefactura.ToString(), LineBreak2));
                        PdfPCell cell3 = new PdfPCell(new Phrase(AuxFactDTO.Fecha.ToString("dd/MM/yyyy"), LineBreak2));
                        cell1.BackgroundColor = new iTextSharp.text.BaseColor(93, 123, 157);
                        cell2.BackgroundColor = new iTextSharp.text.BaseColor(93, 123, 157);
                        cell3.BackgroundColor = new iTextSharp.text.BaseColor(93, 123, 157);
                        cell3.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell1.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell1.BorderWidth = 0;
                        cell2.BorderWidth = 1;
                        cell2.BorderColor = BaseColor.DARK_GRAY;
                        cell3.BorderWidth = 0;
                        table.WidthPercentage = 100;
                        table.AddCell(cell1);
                        table.AddCell(cell2);
                        table.AddCell(cell3);
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
                        float[] columnWidths2 = {6,5};
                        PdfPTable tablaDatosCliente = new PdfPTable(columnWidths2);
                        tablaDatosCliente.WidthPercentage = 100f;
                        PdfPCell cellDatosClienteLeft = new PdfPCell();
                        PdfPCell cellDatosClienteRight = new PdfPCell();
                        Paragraph nombrecompleto = new Paragraph(string.Format("Apellido, Nombre: " + AuxFactDTO.Clienteid.Apellido + " " + AuxFactDTO.Clienteid.Nombre), LineBreak);
                        Paragraph telefono = new Paragraph(string.Format("Telefono: " + AuxFactDTO.Clienteid.Telefono), LineBreak);
                        Paragraph direccion = new Paragraph(string.Format("Direccion: " + AuxFactDTO.Clienteid.Direccion), LineBreak);
                        Paragraph email = new Paragraph(string.Format("Email: " + AuxFactDTO.Clienteid.Email), LineBreak);
                        Paragraph condicion = new Paragraph(string.Format(AuxFactDTO.Clienteid.Tipo), LineBreak);
                        Paragraph metodo = new Paragraph(string.Format("Metodo de pago: " + AuxFactDTO.Metododepago), LineBreak);
                        Paragraph empleado = new Paragraph(string.Format("Vendedor: " + AuxFactDTO.Empleadoid.Apellido+" "+ AuxFactDTO.Empleadoid.Nombre), LineBreak);

                        cellDatosClienteLeft.HorizontalAlignment = Element.ALIGN_LEFT;
                        cellDatosClienteLeft.AddElement(nombrecompleto);
                        cellDatosClienteLeft.AddElement(telefono);
                        cellDatosClienteLeft.AddElement(direccion);
                        cellDatosClienteLeft.AddElement(email);
                        cellDatosClienteLeft.AddElement(condicion);
                        cellDatosClienteRight.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellDatosClienteRight.AddElement(metodo);
                        cellDatosClienteRight.AddElement(empleado);
                        cellDatosClienteRight.BorderWidth = 0;
                        cellDatosClienteLeft.BorderWidth = 0;
                        tablaDatosCliente.AddCell(cellDatosClienteLeft);
                        tablaDatosCliente.AddCell(cellDatosClienteRight);
                        pdfDoc.Add(tablaDatosCliente);
                        pdfDoc.Add(Chunk.NEWLINE);
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                        pdfDoc.Close();
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", "attachment;filename=pdfFactura.pdf");
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

        protected void GrillaDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (Tipo == 'A')
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        AuxTotal += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "totalxArt"));
                    }
                    else if (e.Row.RowType == DataControlRowType.Footer)
                    {
                        e.Row.Cells[1].Text = "SubTotal: $" + Convert.ToDouble(AuxTotal).ToString("#,##0.00");
                        e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                        e.Row.Cells[2].Text = "IVA%: $" + Convert.ToDouble(AuxTotal2 * 0.21).ToString("#,##0.00");
                        e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                        e.Row.Cells[3].Text = "Total: $" + Convert.ToDouble(AuxTotal2).ToString("#,##0.00");
                        e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                        e.Row.Font.Bold = true;
                    }
                }
                else
                {

                    if (e.Row.RowType == DataControlRowType.Footer)
                    {
                        e.Row.Cells[3].Text = "Total: $" + Convert.ToDouble(AuxTotal2).ToString("#,##0.00");
                        e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                        e.Row.Font.Bold = true;
                    }
                }
            }
            catch (Exception err)
            {
                string error = err.Message.ToString() + " - " + err.Source.ToString();
            }
        }

        public void RefrescarDetalle(int nro,char tipof)
        {
            ListaDetalleDTO = ControlDetallePedido.DetallePedidoFactura(nro,tipof);
            dt2 = new DataTable();
            dt2.Columns.Add(new DataColumn("articuloid", System.Type.GetType("System.Int32")));
            dt2.Columns.Add(new DataColumn("nombre", System.Type.GetType("System.String")));
            dt2.Columns.Add(new DataColumn("cantidad", System.Type.GetType("System.Int32")));
            dt2.Columns.Add(new DataColumn("preciovendido", System.Type.GetType("System.Double")));
            dt2.Columns.Add(new DataColumn("totalxArt", System.Type.GetType("System.Double")));

            foreach (DetallePedidoDTO Auxi in ListaDetalleDTO)
            {
                AuxTotal2 = AuxTotal2 + (Auxi.Preciovendido * Auxi.Cantidad);
                if (Tipo == 'A')
                {
                    double PrecioSinIva = Auxi.Preciovendido - (Auxi.Preciovendido * 0.21);
                    dt2.Rows.Add(Auxi.Articuloid.Articuloid, Auxi.Articuloid.Nombre + " - " + Auxi.Articuloid.Marca, Auxi.Cantidad, Auxi.Preciovendido.ToString("#,##0.00"), (PrecioSinIva * Auxi.Cantidad).ToString("#,##0.00"));

                }
                else
                {
                    dt2.Rows.Add(Auxi.Articuloid.Articuloid, Auxi.Articuloid.Nombre + " - " + Auxi.Articuloid.Marca, Auxi.Cantidad, Auxi.Preciovendido.ToString("#,##0.00"), (Auxi.Preciovendido * Auxi.Cantidad).ToString("#,##0.00"));
                }
            }
            GrillaDetalle.DataSource = dt2;
            GrillaDetalle.DataBind();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void TxtFiltroNombre_TextChanged(object sender, EventArgs e)
        {
            if (!_isRefresh)
            {
                GridFactura.DataSource = ListaDTO2;
                GridFactura.DataBind();
                if (ControlFacturacion.ListarFacturaFiltroCliente(TxtFiltroNombre.Text).Count != 0)
                {
                    dt = new DataTable();
                    dt.Columns.Add(new DataColumn("cod_factura", System.Type.GetType("System.Int32")));
                    dt.Columns.Add(new DataColumn("fecha", System.Type.GetType("System.String")));
                    dt.Columns.Add(new DataColumn("importetotal", System.Type.GetType("System.Double")));
                    dt.Columns.Add(new DataColumn("clienteid", System.Type.GetType("System.String")));
                    dt.Columns.Add(new DataColumn("empleadoid", System.Type.GetType("System.String")));
                    dt.Columns.Add(new DataColumn("metododepago", System.Type.GetType("System.String")));
                    dt.Columns.Add(new DataColumn("tipofactura", System.Type.GetType("System.Char")));
                    foreach (Facturacion_VentaDTO Auxi in ControlFacturacion.ListarFacturaFiltroCliente(TxtFiltroNombre.Text))
                    {

                        dt.Rows.Add(Auxi.Cod_factura, Auxi.Fecha.ToString("yyyy/MM/dd"), Auxi.Importetotal.ToString("#,##0.00"), Auxi.Clienteid.Apellido + " " + Auxi.Clienteid.Nombre, Auxi.Empleadoid.Apellido + " " + Auxi.Empleadoid.Nombre, Auxi.Metododepago, Auxi.Tipodefactura);
                    }
                    GridFactura.DataSource = dt;
                    GridFactura.DataBind();
                }
                else
                {
                    Refrescar();
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralertCliente();", true);

                }
            }
            else
            {
                Response.Redirect("ListarFactura.aspx");
            }
        }

        protected void BtnFiltroFecha_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (!_isRefresh)
                {
                    GridFactura.DataSource = ListaDTO2;
                    GridFactura.DataBind();
                    if (ControlFacturacion.RangoTotal(Convert.ToDateTime(TxtDesde.Text), Convert.ToDateTime(TxtHasta.Text)).Count != 0)
                    {
                        dt = new DataTable();
                        dt.Columns.Add(new DataColumn("cod_factura", System.Type.GetType("System.Int32")));
                        dt.Columns.Add(new DataColumn("fecha", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("importetotal", System.Type.GetType("System.Double")));
                        dt.Columns.Add(new DataColumn("clienteid", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("empleadoid", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("metododepago", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("tipofactura", System.Type.GetType("System.Char")));
                        foreach (Facturacion_VentaDTO Auxi in ControlFacturacion.RangoTotal(Convert.ToDateTime(TxtDesde.Text), Convert.ToDateTime(TxtHasta.Text)))
                        {

                            dt.Rows.Add(Auxi.Cod_factura, Auxi.Fecha.ToString("yyyy/MM/dd"), Auxi.Importetotal.ToString("#,##0.00"), Auxi.Clienteid.Apellido + " " + Auxi.Clienteid.Nombre, Auxi.Empleadoid.Apellido + " " + Auxi.Empleadoid.Nombre, Auxi.Metododepago, Auxi.Tipodefactura);
                        }
                        GridFactura.DataSource = dt;
                        GridFactura.DataBind();
                    }
                    else
                    {
                        Refrescar();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralertFecha();", true);
                    }
                }
                else
                {
                    Response.Redirect("ListarFactura.aspx");
                }
            }
        }

        public void Refrescar2(string TxtFiltro)
        {
            if (ControlFacturacion.ListarFacturaFiltroCliente(TxtFiltro).Count != 0)
            {
                dt = new DataTable();
                dt.Columns.Add(new DataColumn("cod_factura", System.Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("fecha", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("importetotal", System.Type.GetType("System.Double")));
                dt.Columns.Add(new DataColumn("clienteid", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("empleadoid", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("metododepago", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("tipofactura", System.Type.GetType("System.Char")));

                foreach (Facturacion_VentaDTO Auxi in ControlFacturacion.ListarFacturaFiltroCliente(TxtFiltro))
                {

                    dt.Rows.Add(Auxi.Cod_factura, Auxi.Fecha.ToString("yyyy/MM/dd"), Auxi.Importetotal.ToString("#,##0.00"), Auxi.Clienteid.Apellido + " " + Auxi.Clienteid.Nombre, Auxi.Empleadoid.Apellido + " " + Auxi.Empleadoid.Nombre, Auxi.Metododepago, Auxi.Tipodefactura);
                }
                GridFactura.DataSource = dt;
                GridFactura.DataBind();
            }
            else
            {
                Refrescar();
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralertCliente();", true);

            }
        }

        public void RefrescarRango(string txtDesde, string txtHasta)
        {
            if (!string.IsNullOrEmpty(txtDesde) && !string.IsNullOrEmpty(txtHasta))
            {
                if (ControlFacturacion.RangoTotal(Convert.ToDateTime(txtDesde), Convert.ToDateTime(txtHasta)).Count != 0)
                {
                    dt = new DataTable();
                    dt.Columns.Add(new DataColumn("cod_factura", System.Type.GetType("System.Int32")));
                    dt.Columns.Add(new DataColumn("fecha", System.Type.GetType("System.String")));
                    dt.Columns.Add(new DataColumn("importetotal", System.Type.GetType("System.Double")));
                    dt.Columns.Add(new DataColumn("clienteid", System.Type.GetType("System.String")));
                    dt.Columns.Add(new DataColumn("empleadoid", System.Type.GetType("System.String")));
                    dt.Columns.Add(new DataColumn("metododepago", System.Type.GetType("System.String")));
                    dt.Columns.Add(new DataColumn("tipofactura", System.Type.GetType("System.Char")));

                    foreach (Facturacion_VentaDTO Auxi in ControlFacturacion.RangoTotal(Convert.ToDateTime(txtDesde), Convert.ToDateTime(txtHasta)))
                    {

                        dt.Rows.Add(Auxi.Cod_factura, Auxi.Fecha.ToString("yyyy/MM/dd"), Auxi.Importetotal.ToString("#,##0.00"), Auxi.Clienteid.Apellido + " " + Auxi.Clienteid.Nombre, Auxi.Empleadoid.Apellido + " " + Auxi.Empleadoid.Nombre, Auxi.Metododepago, Auxi.Tipodefactura);
                    }
                    GridFactura.DataSource = dt;
                    GridFactura.DataBind();
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

        public void Refrescar()
        {
            ListaDTO = ControlFacturacion.DevolverTodo();

            if (ListaDTO.Count != 0)
            {
                dt = new DataTable();
                dt.Columns.Add(new DataColumn("cod_factura", System.Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("fecha", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("importetotal", System.Type.GetType("System.Double")));
                dt.Columns.Add(new DataColumn("clienteid", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("empleadoid", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("metododepago", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("tipofactura", System.Type.GetType("System.Char")));

                foreach (Facturacion_VentaDTO Auxi in ListaDTO)
                {

                    dt.Rows.Add(Auxi.Cod_factura, Auxi.Fecha.ToString("yyyy/MM/dd"), Auxi.Importetotal.ToString("#,##0.00"), Auxi.Clienteid.Apellido + " " + Auxi.Clienteid.Nombre, Auxi.Empleadoid.Apellido + " " + Auxi.Empleadoid.Nombre, Auxi.Metododepago, Auxi.Tipodefactura);
                }
                GridFactura.DataSource = dt;
                GridFactura.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert();", true);
            }
        }
    }
}