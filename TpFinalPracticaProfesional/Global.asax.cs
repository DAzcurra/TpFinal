using Controladoras;
using Entidades;
using EntidadesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace TpFinalPracticaProfesional
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            C_Empleado ControlEmpleado = new C_Empleado();
            Session["ControlEmpleado"] = ControlEmpleado;
            C_Cliente ControlCliente = new C_Cliente();
            Session["ControlCliente"] = ControlCliente;
            C_Proveedor ControlProveedor = new C_Proveedor();
            Session["ControlProveedor"] = ControlProveedor;
            C_Articulo ControlArticulo = new C_Articulo();
            Session["ControlArticulo"] = ControlArticulo;
            C_ArticuloxProveedor ControlArticuloxProveedor = new C_ArticuloxProveedor();
            Session["ControlArticuloxProveedor"] = ControlArticuloxProveedor;
            C_Pedido ControlPedido = new C_Pedido();
            Session["ControlPedido"] = ControlPedido;
            C_Configuracion ControlConfig = new C_Configuracion();
            Session["ControlConfig"] = ControlConfig;
            C_DetallePedido ControlDetallePedido = new C_DetallePedido();
            Session["ControlDetallePedido"] = ControlDetallePedido;
            List<DetallePedidoDTO> ListaDetalleDTO = null;
            Session["ListaDetalleDTO"] = ListaDetalleDTO;
            Pedido PedidoSession = null;
            Session["PedidoSession"] = PedidoSession;
            C_Facturacion ControlFacturacion = new C_Facturacion();
            Session["ControlFacturacion"] = ControlFacturacion;
            string StockMin = null;
            Session["StockMin"] = StockMin;
            List<int> ListaCant = null;
            Session["ListaCant"] = ListaCant;
            List<Articulo> ListaArticulo = null;
            Session["ListaArticulo"] = ListaArticulo;
            double Auxdouble = 0;
            Session["Auxdouble"] = Auxdouble;
            Session["Login"] = null;
            Session["ImporteTotal"] = null;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}