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
    public class C_Pedido
    {
        R_Pedido R_Ped = new R_Pedido();
        R_Articulo R_Art = new R_Articulo();
        R_Cliente R_Cli = new R_Cliente();
        R_DetallePedido R_Det = new R_DetallePedido();
 
        public bool Agregar(Pedido Aux, List<DetallePedido> Lista)
        {
            bool Agre = false;
            try
            {
                Agre = R_Ped.Agregar(Aux, Lista);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Agre;
        }
        public Pedido BuscarNroPedidoEstadoEntregado(int nro)
        {
            Pedido Aux = null;
            try
            {
                Aux = R_Ped.BuscarID(nro);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Aux;
        }

        public Pedido BuscarID(int id)
        {
            Pedido Aux = null;
            try
            {
                Aux = R_Ped.BuscarID(id);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Aux;
        }

        public Pedido BuscarIDTotal(int id)
        {
            Pedido Aux = null;
            try
            {
                Aux = R_Ped.BuscarIDTotal(id);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Aux;
        }

        public List<PedidoDTO> BuscarIDTotalLista(int id)
        {
            List<PedidoDTO> Lista = new List<PedidoDTO>();

            Pedido Aux = null;
            try
            {
                Aux = R_Ped.BuscarIDTotal(id);

                if (Aux != null)
                {
                    Cliente AuxCli = R_Cli.BuscarID(Aux.Clienteid);
                    PedidoDTO AuxDTO = new PedidoDTO(Aux.Pedidoid, Aux.Nropedido, Aux.Fecha, Aux.Entregado,AuxCli, Aux.Estado);
                    Lista.Add(AuxDTO);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Lista;
        }

        public Pedido BuscarNro(int nro)
        {
            Pedido Aux = null;
            try
            {
                Aux = R_Ped.BuscarNro(nro);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Aux;
        }


        public PedidoDTO BuscarNroDTO(int nro)
        {
            Pedido P = null;
            PedidoDTO PedAux = null;
            try
            {
                P = R_Ped.BuscarNro(nro);
                Cliente Cli = R_Cli.BuscarID(P.Clienteid);
                PedAux = new PedidoDTO(P.Pedidoid, P.Nropedido, P.Fecha, P.Entregado,Cli, P.Estado);

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return PedAux;
        }

        public List<PedidoDTO> BuscarNroLista(int nro)
        {
            Pedido Aux = null;
            List<PedidoDTO> Lista = new List<PedidoDTO>();
            try
            {
                Aux = R_Ped.BuscarNro(nro);

                if (Aux != null)
                {
                    Cliente AuxCli = R_Cli.BuscarID(Aux.Clienteid);
                    PedidoDTO AuxDTO = new PedidoDTO(Aux.Pedidoid, Aux.Nropedido, Aux.Fecha, Aux.Entregado,AuxCli, Aux.Estado);
                    Lista.Add(AuxDTO);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Lista;
        }

        public List<PedidoDTO> DevolverTodo()
        {
            List<PedidoDTO> ListaDevolver = new List<PedidoDTO>();
            List<Pedido> Lista = R_Ped.DevolverTodo();
            try
            {
                foreach (Pedido P in Lista)
                {
                    Cliente Cli = R_Cli.BuscarID(P.Clienteid);
                    PedidoDTO Aux = new PedidoDTO(P.Pedidoid, P.Nropedido, P.Fecha, P.Entregado,Cli, P.Estado);
                    ListaDevolver.Add(Aux);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return ListaDevolver;
        }
        public List<PedidoDTO> Rango(DateTime Desde, DateTime Hasta)
        {
            List<PedidoDTO> ListaDevolver = new List<PedidoDTO>();
            List<Pedido> Lista = R_Ped.Rango(Desde, Hasta);
            try
            {
                foreach (Pedido P in Lista)
                {
                    Cliente Cli = R_Cli.BuscarID(P.Clienteid);
                    PedidoDTO Aux = new PedidoDTO(P.Pedidoid, P.Nropedido, P.Fecha, P.Entregado,Cli, P.Estado);
                    ListaDevolver.Add(Aux);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return ListaDevolver;
        }

        public List<PedidoDTO> RangoTotal(DateTime Desde, DateTime Hasta)
        {
            List<PedidoDTO> ListaDevolver = new List<PedidoDTO>();
            List<Pedido> Lista = R_Ped.RangoTotal(Desde, Hasta);
            try
            {
                foreach (Pedido P in Lista)
                {
                    Cliente Cli = R_Cli.BuscarID(P.Clienteid);
                    PedidoDTO Aux = new PedidoDTO(P.Pedidoid, P.Nropedido, P.Fecha, P.Entregado, Cli, P.Estado);
                    ListaDevolver.Add(Aux);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return ListaDevolver;
        }

        public bool ActualizarCantidad(DetallePedido Det, int cantidad)
        {
            bool Actu = false;
            int Dif = 0;
            try
            {
                Dif = cantidad - Det.Cantidad;

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

            return Actu;
        }

        public List<PedidoDTO> ListarPedidoFiltroCliente(string TxtFiltro)
        {
            List<PedidoDTO> ListaAuxDTO = new List<PedidoDTO>();

            try
            {
                List<Pedido> ListaP = R_Ped.ListarPedidoFiltroCliente(TxtFiltro);
                if (ListaP.Count != 0)
                {
                    foreach (Pedido item in ListaP)
                    {
                        Cliente cli = R_Cli.BuscarID(item.Clienteid);
                        PedidoDTO PedDTO = new PedidoDTO(item.Pedidoid, item.Nropedido, item.Fecha, item.Entregado, cli, item.Estado);
                        ListaAuxDTO.Add(PedDTO);
                    }
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return ListaAuxDTO;
        }

        public List<PedidoDTO> PedidoPorNombreApellido(string nombre)
        {
            List<PedidoDTO> Resultado = new List<PedidoDTO>();
            try
            {
                List<Pedido> ListaNombreApellido = R_Ped.ListaClienteAPellidoNombre(nombre);
                if (ListaNombreApellido.Count != 0)
                {
                    foreach (Pedido item in ListaNombreApellido)
                    {
                        Cliente cli = R_Cli.BuscarID(item.Clienteid);
                        PedidoDTO PedDTO = new PedidoDTO(item.Pedidoid, item.Nropedido, item.Fecha, item.Entregado,cli, item.Estado);
                        Resultado.Add(PedDTO);
                    }
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Resultado;
        }

        public List<PedidoDTO> PedidoPorNombreApellidoTotal(string nombre)
        {
            List<PedidoDTO> Resultado = new List<PedidoDTO>();
            try
            {
                List<Pedido> ListaNombreApellido = R_Ped.ListaClienteAPellidoNombreTotal(nombre);
                if (ListaNombreApellido.Count != 0)
                {
                    foreach (Pedido item in ListaNombreApellido)
                    {
                        Cliente cli = R_Cli.BuscarID(item.Clienteid);
                        PedidoDTO PedDTO = new PedidoDTO(item.Pedidoid, item.Nropedido, item.Fecha, item.Entregado,cli, item.Estado);
                        Resultado.Add(PedDTO);
                    }
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Resultado;
        }

        public bool DeshabilitarHabilitarPedido(int nro, string estado)
        {
            bool DH = false;
            try
            {
                Pedido id = R_Ped.BuscarNro(nro);
                if (id != null)
                {
                    DH = R_Ped.DeshabilitarHabilitarPedido(id.Pedidoid, estado);
                }

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return DH;
        }
        
        public bool Eliminar(int nro)
        {
            bool Elim = false;
            try
            {
                Pedido P = R_Ped.BuscarNro(nro);
                if (P != null)
                {
                    Elim = R_Ped.Eliminar(P.Pedidoid);
                }

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Elim;
        }

        public bool PedidoEntregado(int id, string entregado, int cod)
        {
            bool PedidoEntregado = false;
            try
            {
                Pedido idp = R_Ped.BuscarNro(id);
                if (idp != null)
                {
                    R_Ped.PedidoEntregado(idp.Pedidoid, entregado,cod);
                    PedidoEntregado = true;
                }

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return PedidoEntregado;
        }
    }
}
