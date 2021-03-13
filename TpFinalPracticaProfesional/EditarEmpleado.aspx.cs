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
    public partial class EditarEmpleado : System.Web.UI.Page
    {
        static string AuxCuil2;
        static string Cuil;
        C_Empleado ControlEmpleado;
        C_Configuracion  ControlConfig;
        Empleado Aux = null;
        List<Empleado> Lista = new List<Empleado>();
        private bool _refreshState;
        private bool _isRefresh;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            ControlEmpleado = (C_Empleado)Session["ControlEmpleado"];
            ControlConfig = (C_Configuracion)Session["ControlConfig"];

            if (!IsPostBack)
            {
                RefrecarBusqueda();
                ListaEmpleados.PageSize = ControlConfig.DevolverNroMaxPaginacion();
            }

            if (RadioButtonCuil.Checked == true)
            {
                TxtBuscarXCuil.Visible = true;
                TxtBuscarXApellido.Visible = false;
                BtnBuscarApellido.Visible = false;
                BtnBuscarCuil.Visible = true;
                PanelPopUp.Attributes["display"] = "none";
                MensajeConfirmacion.Attributes["display"] = "none";
                TxtBuscarXCuil.Focus();
            }
            else
            {
                TxtBuscarXCuil.Visible = false;
                TxtBuscarXApellido.Visible = true;
                BtnBuscarApellido.Visible = true;
                BtnBuscarCuil.Visible = false;
                PanelPopUp.Attributes["display"] = "none";
                MensajeConfirmacion.Attributes["display"] = "none";
                TxtBuscarXApellido.Focus();
            }
            MensajeConfirmacion.Attributes["display"] = "none";
            PanelPopUp.Attributes["display"] = "none";
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

        protected void BtnBuscarApellido_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (RadioButtonApellido.Checked)
                {
                    Lista = ControlEmpleado.BuscarEmp(TxtBuscarXApellido.Text.ToLower());
                    if (Lista.Count != 0)
                    {
                        ListaEmpleados.DataSource = Lista;
                        ListaEmpleados.DataBind();
                        ListaEmpleados.Visible = true;
                        PanelPopUp.Attributes["display"] = "none";
                        MensajeConfirmacion.Attributes["display"] = "none";
                        PanelGrilla.Visible = true;
                    }
                    else
                    {
                        ListaEmpleados.Visible = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert();", true);
                        PanelPopUp.Attributes["display"] = "none";
                        MensajeConfirmacion.Attributes["display"] = "none";

                    }
                }
            }
        }
        protected void BtnBuscarCuil_Click(object sender, EventArgs e)
        {
            List<Empleado> ListaAux = new List<Empleado>();
            if(IsValid)
            {
                if(RadioButtonCuil.Checked)
                {
                    Aux = ControlEmpleado.Buscar(TxtBuscarXCuil.Text.ToLower());
                    if (Aux != null)
                    {
                        ListaAux.Add(Aux);
                        ListaEmpleados.DataSource = ListaAux;
                        ListaEmpleados.DataBind();
                        ListaEmpleados.Visible = true;
                        PanelPopUp.Attributes["display"] = "none";
                        MensajeConfirmacion.Attributes["display"] = "none";
                        PanelGrilla.Visible = true;
                    }
                    else
                    {
                        ListaEmpleados.Visible = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert();", true);
                        PanelPopUp.Attributes["display"] = "none";
                        MensajeConfirmacion.Attributes["display"] = "none";
                    }
                }
            }
        }

        public void RefrecarBusqueda()
        {
            if (RadioButtonApellido.Checked)
            {
                Lista = ControlEmpleado.BuscarEmp(TxtBuscarXApellido.Text.ToLower());
                ListaEmpleados.DataSource = Lista;
                ListaEmpleados.DataBind();
                ListaEmpleados.Visible = true;
            }
            else
            {
                Empleado Auxi = ControlEmpleado.Buscar(TxtBuscarXCuil.Text.ToLower());
                if(Auxi != null)
                {
                    Lista.Add(Auxi);
                    ListaEmpleados.DataSource = Lista;
                    ListaEmpleados.DataBind();
                    ListaEmpleados.Visible = true;
                }

            }

        }

        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(RadioButtonCuil.Checked==true)
            {
                RadioButtonApellido.Checked = false;
            }
            else
            {
                RadioButtonApellido.Checked = true;
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ImgB = sender as ImageButton;
            GridViewRow gvRow = (GridViewRow)ImgB.NamingContainer;
            TxtNombre.Text = gvRow.Cells[0].Text;
            TxtNombre.Focus();
            TxtApellido.Text = gvRow.Cells[1].Text;
            TextTelefono.Text = gvRow.Cells[2].Text;
            TxtEmail.Text = gvRow.Cells[3].Text;
            TxtDirecion.Text = gvRow.Cells[4].Text;
            Txtcuil.Text = gvRow.Cells[5].Text;
            Cuil = gvRow.Cells[5].Text;
            int i = 0;
            foreach (var item in DropDownList2.Items)
            {
                if (item.ToString().ToLower() == gvRow.Cells[6].Text)
                {
                    i = DropDownList2.Items.IndexOf((ListItem)item);
                    break;
                }
            }
            DropDownList2.SelectedIndex = i;
            DropDownList2.Items[i].Selected = true;
            this.ModalPopupExtender1.Show();
            Labelmgg.Visible = false;

        }

        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            Empleado Aux = null;
            if (Page.IsValid)
            {
                Aux = new Empleado() {
                    Empleadoid = 0, 
                    Nombre = TxtNombre.Text.ToLower(), 
                    Apellido = TxtApellido.Text.ToLower(),
                    Telefono = TextTelefono.Text, 
                    Email = TxtEmail.Text.ToLower(),
                    Direccion = TxtDirecion.Text.ToLower(), 
                    Cuil = Txtcuil.Text,
                    Estado = Convert.ToString(DropDownList2.SelectedValue) };
                if (!_isRefresh)
                {
                    if (ControlEmpleado.Modificar(Cuil, Aux))
                    {
                        RefrecarBusqueda();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert();", true);
                        Labelmgg.Visible = false;
                    }
                    else
                    {
                        ModalPopupExtender1.Show();
                        Labelmgg.Visible = true;
                    }
                }
                else
                {
                    Response.Redirect("EditarEmpleado.aspx");
                }
            }
        }

        public void LimpiarTxtBuscar()
        {
            TxtBuscarXApellido.Text = "";
            TxtBuscarXCuil.Text = "";
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (Convert.ToInt64(TxtBuscarXCuil.Text) >= 0)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ImgB = sender as ImageButton;
            GridViewRow gvRow = (GridViewRow)ImgB.NamingContainer;
            AuxCuil2 = gvRow.Cells[5].Text;
            ModalPopupExtender2.Show();

        }

        protected void BtnAceptarEliminar_Click(object sender, EventArgs e)
        {
            bool Elim = false;
            if (!_isRefresh)
            {
                if (ControlEmpleado.DependeDeFactura(AuxCuil2).Count == 0)
                {
                    Elim = ControlEmpleado.Eliminar(AuxCuil2);

                    if (Elim)
                    {
                        RefrecarBusqueda();
                        TxtBuscarXCuil.Text = "";
                        TxtBuscarXApellido.Text = "";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "errorelim();", true);
                    }
                    else
                    {
                        Response.Write("<script type='text/javascript'> alert('No se pudo eliminar el proveedor !!') </script>");
                    }
                }
                else
                {
                    if (ControlEmpleado.DeshabilitarHabilitarEmpleado(AuxCuil2, "no activo"))
                    {
                        RefrecarBusqueda();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "errorDepende();", true);
                    }
                }
            }
            else
            {
                Response.Redirect("EditarEmpleado.aspx");
            }
        }

        protected void ListaEmpleados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ListaEmpleados.PageIndex = e.NewPageIndex;
            RefrecarBusqueda();
            ListaEmpleados.PageSize = ControlConfig.DevolverNroMaxPaginacion();
        }

        protected void ListaEmpleados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string _estado = DataBinder.Eval(e.Row.DataItem, "estado").ToString();
                ImageButton btnFinalizar = (ImageButton)e.Row.FindControl("ImageButton2");

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
    }
}