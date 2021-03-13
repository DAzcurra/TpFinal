using Entidades;
using EntidadesDTO;
using Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controladoras
{
    public class C_Facturacion
    {
        R_Facturacion R_Factura = new R_Facturacion();
        R_Cliente R_Cli = new R_Cliente();
        R_Empleado R_Emp = new R_Empleado();
        R_Pedido R_Ped = new R_Pedido(); 
        public bool Agregar(Facturacion_Venta Nuevo, string entregado, int idpedido)
        {
            bool Agre = false;
            try
            {
                Agre = R_Factura.Agregar(Nuevo, entregado, idpedido);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Agre;
        }

        public List<Facturacion_VentaDTO> DevolverTodo()
        {
            List<Facturacion_VentaDTO> ListaDTO = new List<Facturacion_VentaDTO>();
            
            try
            {
                foreach(Facturacion_Venta Aux in R_Factura.DevolverTodo())
                {
                    Cliente AuxCli = R_Cli.BuscarID(Aux.Clienteid);
                    Empleado AuxEmp = R_Emp.BuscarID(Aux.Empleadoid);
                    Pedido AuxP = R_Ped.BuscarIDTotal(Aux.Pedidoid);

                    Facturacion_VentaDTO AuxFacDTO = new Facturacion_VentaDTO(Aux.Cod_factura,Aux.Fecha,Aux.Importetotal,AuxCli,AuxEmp,Aux.Metododepago, AuxP, Aux.Tipodefactura);
                    ListaDTO.Add(AuxFacDTO);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return ListaDTO;
        }

        public Facturacion_VentaDTO Buscar(int cod,char tipofactura)
        {
            Facturacion_Venta Fact = null;
            Facturacion_VentaDTO FactDTO = null;

            try
            {
                Fact = R_Factura.Buscar(cod, tipofactura);
                if (Fact != null)
                {
                    Cliente AuxCli = R_Cli.BuscarID(Fact.Clienteid);
                    Empleado AuxEmp = R_Emp.BuscarID(Fact.Empleadoid);
                    Pedido AuxP = R_Ped.BuscarIDTotal(Fact.Pedidoid);

                    FactDTO = new Facturacion_VentaDTO(Fact.Cod_factura, Fact.Fecha, Fact.Importetotal, AuxCli, AuxEmp, Fact.Metododepago, AuxP, Fact.Tipodefactura);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return FactDTO;
        }

        public List<Facturacion_VentaDTO> ListarFacturaFiltroCliente(string TxtFiltro)
        {
            List<Facturacion_VentaDTO> ListaAuxDTO = new List<Facturacion_VentaDTO>();

            try
            {
                List<Facturacion_Venta> ListaF = R_Factura.ListarFacturaFiltroCliente(TxtFiltro);
                if (ListaF.Count != 0)
                {
                    foreach (Facturacion_Venta Aux in ListaF)
                    {
                        Cliente AuxCli = R_Cli.BuscarID(Aux.Clienteid);
                        Empleado AuxEmp = R_Emp.BuscarID(Aux.Empleadoid);
                        Pedido AuxP = R_Ped.BuscarIDTotal(Aux.Pedidoid);

                        Facturacion_VentaDTO AuxFacDTO = new Facturacion_VentaDTO(Aux.Cod_factura, Aux.Fecha, Aux.Importetotal, AuxCli, AuxEmp, Aux.Metododepago, AuxP, Aux.Tipodefactura);
                        ListaAuxDTO.Add(AuxFacDTO);
                    }
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return ListaAuxDTO;
        }
        public List<Facturacion_VentaDTO> RangoTotal(DateTime Desde, DateTime Hasta)
        {
            List<Facturacion_VentaDTO> ListaDevolver = new List<Facturacion_VentaDTO>();
            List<Facturacion_Venta> Lista = R_Factura.RangoTotal(Desde, Hasta);
            try
            {
                foreach (Facturacion_Venta Aux in Lista)
                {
                    Cliente AuxCli = R_Cli.BuscarID(Aux.Clienteid);
                    Empleado AuxEmp = R_Emp.BuscarID(Aux.Empleadoid);
                    Pedido AuxP = R_Ped.BuscarIDTotal(Aux.Pedidoid);
                    Facturacion_VentaDTO AuxFacDTO = new Facturacion_VentaDTO(Aux.Cod_factura, Aux.Fecha, Aux.Importetotal, AuxCli, AuxEmp, Aux.Metododepago,AuxP,Aux.Tipodefactura);
                    ListaDevolver.Add(AuxFacDTO);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return ListaDevolver;
        }

        public bool FacturadoPedido(int id)
        {
            bool facturado = false;
            try
            {
                foreach(Facturacion_VentaDTO fc in DevolverTodo())
                {
                    if(fc.Pedidoid.Pedidoid == id)
                    {
                        facturado = true;
                    }
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return facturado;
        }
    }
}
