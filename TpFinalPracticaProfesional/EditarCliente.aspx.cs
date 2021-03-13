using Controladoras;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpFinalPracticaProfesional
{
    public partial class EditarCliente : System.Web.UI.Page
    {
        C_Cliente ControlCliente;
        C_Configuracion ControlConfig;

        List<Cliente> Lista = new List<Cliente>();
        Cliente Aux = null;
        static string AuxCUIL;
        private bool _refreshState;
        private bool _isRefresh;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            ControlCliente = (C_Cliente)Session["ControlCliente"];
            ControlConfig = (C_Configuracion)Session["ControlConfig"];


            if (!IsPostBack)
            {
                BusquedaCliente.PageSize = ControlConfig.DevolverNroMaxPaginacion();
                RefrecarBusqueda();
            }
            if (RadioApellido.Checked)
            {
                TxtBuscarApellido.Visible = true;
                BtnBuscarApellido.Visible = true;
                TxtBuscarRSocial.Visible = false;
                BtnBuscarRSocial.Visible = false;
                TxtBuscarcuil.Visible = false;
                BntBuscarCuil.Visible = false;
                DropTipo.Visible = false;
                BtnBuscarTipo.Visible = false;
                PanelPopUp.Attributes["display"] = "none";
                MensajeConfirmacion.Attributes["display"] = "none";
                TxtBuscarApellido.Focus();
            }
            else
            {
                if (RadioRSocial.Checked)
                {
                    TxtBuscarApellido.Visible = false;
                    BtnBuscarApellido.Visible = false;
                    TxtBuscarRSocial.Visible = true;
                    BtnBuscarRSocial.Visible = true;
                    TxtBuscarcuil.Visible = false;
                    BntBuscarCuil.Visible = false;
                    DropTipo.Visible = false;
                    BtnBuscarTipo.Visible = false;
                    PanelPopUp.Attributes["display"] = "none";
                    MensajeConfirmacion.Attributes["display"] = "none";
                    TxtBuscarRSocial.Focus();
                }
                else
                {
                    if (RadioTipo.Checked)
                    {
                        TxtBuscarApellido.Visible = false;
                        BtnBuscarApellido.Visible = false;
                        TxtBuscarRSocial.Visible = false;
                        BtnBuscarRSocial.Visible = false;
                        DropTipo.Visible = true;
                        BtnBuscarTipo.Visible = true;
                        TxtBuscarcuil.Visible = false;
                        BntBuscarCuil.Visible = false;
                        PanelPopUp.Attributes["display"] = "none";
                        MensajeConfirmacion.Attributes["display"] = "none";
                    }
                    else
                    {
                        TxtBuscarApellido.Visible = false;
                        BtnBuscarApellido.Visible = false;
                        TxtBuscarRSocial.Visible = false;
                        BtnBuscarRSocial.Visible = false;
                        DropTipo.Visible = false;
                        BtnBuscarTipo.Visible = false;
                        TxtBuscarcuil.Visible = true;
                        BntBuscarCuil.Visible = true;
                        PanelPopUp.Attributes["display"] = "none";
                        MensajeConfirmacion.Attributes["display"] = "none";
                        TxtBuscarcuil.Focus();
                    }
                }
            }

        }

        protected void BtnBuscarRSocial_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (RadioRSocial.Checked)
                {
                    Lista = ControlCliente.BuscarxRazonS(TxtBuscarRSocial.Text.ToLower());
                    if (Lista.Count != 0)
                    {
                        BusquedaCliente.DataSource = Lista;
                        BusquedaCliente.DataBind();
                        BusquedaCliente.Visible = true;
                        PanelGrilla.Visible = true;
                        //PanelPopUp.Visible = true;
                        PanelPopUp.Attributes["display"] = "none";
                        MensajeConfirmacion.Attributes["display"] = "none";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert();", true);
                        BusquedaCliente.Visible = false;
                        PanelGrilla.Visible = false;
                        PanelPopUp.Attributes["display"] = "none";
                        MensajeConfirmacion.Attributes["display"] = "none";
                    }
                }
            }
        }

        protected void BtnBuscarApellido_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (RadioApellido.Checked)
                {
                    Lista = ControlCliente.BuscarxApellido(TxtBuscarApellido.Text.ToLower());
                    if (Lista.Count != 0)
                    {
                        BusquedaCliente.DataSource = Lista;
                        BusquedaCliente.DataBind();
                        BusquedaCliente.Visible = true;
                        PanelGrilla.Visible = true;
                        //PanelPopUp.Visible = true;
                        
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert();", true);
                        BusquedaCliente.Visible = false;
                        PanelGrilla.Visible = false;
                        PanelPopUp.Attributes["display"] = "none";
                        MensajeConfirmacion.Attributes["display"] = "none";
                    }
                }
            }
        }

        protected void BtnBuscarTipo_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (RadioTipo.Checked)
                {
                    Lista = ControlCliente.BuscarxTipo(DropTipo.SelectedValue.ToLower());
                    if (Lista.Count != 0)
                    {
                        BusquedaCliente.DataSource = Lista;
                        BusquedaCliente.DataBind();
                        BusquedaCliente.Visible = true;
                        PanelGrilla.Visible = true;
                        //PanelPopUp.Visible = true;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert();", true);
                        BusquedaCliente.Visible = false;
                        PanelGrilla.Visible = false;
                        PanelPopUp.Attributes["display"] = "none";
                        MensajeConfirmacion.Attributes["display"] = "none";
                    }
                }
            }
        }

        protected void BtnEditAceptar_Click(object sender, EventArgs e)
        {
            Cliente Aux = null;
                if (IsValid)
                {
                    string AuxCliente = DropDownList1.SelectedValue;
                     Aux = new Cliente()
                    {
                        Clienteid = 0,
                        Nombre = TxtNombre.Text.ToLower(),
                        Apellido = TxtApellido.Text.ToLower(),
                        Telefono = TextTelefono.Text,
                        Email = TxtEmail.Text.ToLower(),
                        Direccion = TxtDirecion.Text.ToLower(),
                        Cuil = Txtcuil.Text,
                        Razonsocial = TxtRazonS.Text.ToLower(),
                        Tipo = AuxCliente.ToLower(),
                        Estado = Convert.ToString(DropDownList2.SelectedValue)

                     };

                    if (Convert.ToInt32(TextTelefono.Text) >= 0)
                    {
                        if (!_isRefresh)
                        {
                            if (ControlCliente.Modificar(AuxCUIL, Aux))
                            {
                                Labelmgg.Visible = false;
                                RefrecarBusqueda();
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert();", true);

                            }
                            else
                            {
                                ModalPopupExtender1.Show();
                                Labelmgg.Visible = true;
                            }
                        }
                        else
                        {
                            Response.Redirect("EditarCliente.aspx");
                        }
                }
                    else
                    {
                        TextTelefono.Focus();
                        TextTelefono.Text = "";
                    }
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

        protected void BtnEditCancelar_Click(object sender, EventArgs e)
        {

        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ImgB = sender as ImageButton;
            GridViewRow gvRow = (GridViewRow)ImgB.NamingContainer;
            TxtRazonS.Text = gvRow.Cells[1].Text;
            TxtNombre.Text = gvRow.Cells[2].Text;
            TxtApellido.Text = gvRow.Cells[3].Text;
            TextTelefono.Text = gvRow.Cells[4].Text;
            TxtEmail.Text = gvRow.Cells[5].Text;
            TxtDirecion.Text = gvRow.Cells[6].Text;
            Txtcuil.Text = gvRow.Cells[7].Text;
            
            int i = 0;
            foreach (var item in DropDownList1.Items)
            {
                if(item.ToString().ToLower() == gvRow.Cells[8].Text)
                {
                     i = DropDownList1.Items.IndexOf((ListItem)item);
                    break;
                }
            }
            DropDownList1.SelectedIndex = i;
            DropDownList1.Items[i].Selected=true;

            int x = 0;
            foreach (var item2 in DropDownList2.Items)
            {
                if (item2.ToString().ToLower() == gvRow.Cells[9].Text)
                {
                    x = DropDownList2.Items.IndexOf((ListItem)item2);
                    break;
                }
            }
            DropDownList2.SelectedIndex = x;
            DropDownList2.Items[x].Selected = true;
            AuxCUIL = gvRow.Cells[7].Text;
            this.ModalPopupExtender1.Show();
            Labelmgg.Visible = false;
            
        }
        public void LimpiarTxtBuscar()
        {
            TxtBuscarRSocial.Text = "";
            TxtBuscarApellido.Text = "";
        }
        public void RefrecarBusqueda()
        {
            if (RadioApellido.Checked)
            {
                Lista = ControlCliente.BuscarxApellido(TxtBuscarApellido.Text.ToLower());
                BusquedaCliente.DataSource = Lista;
                BusquedaCliente.DataBind();
                BusquedaCliente.Visible = true;
            }
            else
            {
                if (RadioRSocial.Checked)
                {
                    Lista = ControlCliente.BuscarxRazonS(TxtBuscarRSocial.Text.ToLower());
                    BusquedaCliente.DataSource = Lista;
                    BusquedaCliente.DataBind();
                    BusquedaCliente.Visible = true;
                }
                else
                {
                    if (RadioTipo.Checked)
                    {
                        Lista = ControlCliente.BuscarxTipo(DropTipo.SelectedValue.ToLower());
                        BusquedaCliente.DataSource = Lista;
                        BusquedaCliente.DataBind();
                        BusquedaCliente.Visible = true;
                    }
                    else
                    {
                        Cliente cli= ControlCliente.Buscarcuil(TxtBuscarcuil.Text.ToLower());
                        if (cli != null)
                        {
                            Lista.Add(cli);
                            BusquedaCliente.DataSource = Lista;
                            BusquedaCliente.DataBind();
                            BusquedaCliente.Visible = true;
                        }
                    }
                }
            }
        }

        protected void ImageButton12_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ImgB = sender as ImageButton;
            GridViewRow gvRow = (GridViewRow)ImgB.NamingContainer;
            AuxCUIL = gvRow.Cells[7].Text;
            ModalPopupExtender2.Show();
        }

        protected void BusquedaCliente_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string _estado = DataBinder.Eval(e.Row.DataItem, "estado").ToString();
                ImageButton btnFinalizar =(ImageButton)e.Row.FindControl("ImageButton12");

                if (_estado == "no activo")
                {
                    e.Row.BackColor = System.Drawing.Color.LightCoral;
                    btnFinalizar.Enabled = false;
                }
                else
                {
                    btnFinalizar.Enabled = true;
                }

            }
        }

        protected void BusquedaCliente_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BusquedaCliente.PageIndex = e.NewPageIndex;
            BusquedaCliente.DataSource = ControlCliente.Listar();
            BusquedaCliente.DataBind();
            BusquedaCliente.PageSize = ControlConfig.DevolverNroMaxPaginacion();
        }

        protected void BtnAceptarEliminar_Click(object sender, EventArgs e)
        {
            bool Elim = false;
            if (!_isRefresh)
            {
                if (ControlCliente.DependeDeFacturayPedido(AuxCUIL).Count == 0)
                {
                    Elim = ControlCliente.Eliminar(AuxCUIL);

                    if (Elim)
                    {
                        RefrecarBusqueda();
                        LimpiarTxtBuscar();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "errorelim();", true);
                    }
                    else
                    {
                        Response.Write("<script type='text/javascript'> alert('No se pudo eliminar el proveedor !!') </script>");
                    }
                }
                else
                {
                    if (ControlCliente.DeshabilitarHabilitarCliente(AuxCUIL, "no activo"))
                    {
                        RefrecarBusqueda();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "errorDepende();", true);
                    }
                }
            }
            else
            {
                Response.Redirect("EditarCliente.aspx");
            }
        }

        protected void BntBuscarCuil_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (RadioCuil.Checked)
                {
                    Cliente cli = ControlCliente.Buscarcuil(TxtBuscarcuil.Text.ToLower());
                    if (cli != null)
                    {
                        Lista.Add(cli);
                        if (Lista.Count != 0)
                        {
                            BusquedaCliente.DataSource = Lista;
                            BusquedaCliente.DataBind();
                            BusquedaCliente.Visible = true;
                            PanelGrilla.Visible = true;
                            //PanelPopUp.Visible = true;
                            PanelPopUp.Attributes["display"] = "none";
                            MensajeConfirmacion.Attributes["display"] = "none";
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert();", true);
                            BusquedaCliente.Visible = false;
                            PanelGrilla.Visible = false;
                            PanelPopUp.Attributes["display"] = "none";
                            MensajeConfirmacion.Attributes["display"] = "none";
                        }
                    }
                }
            }
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ControlCliente.Buscarcuil(TxtBuscarcuil.Text) == null)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (Convert.ToInt64(TxtBuscarcuil.Text) >= 0)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }
    }
}