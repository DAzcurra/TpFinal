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
    public class C_DetallePedido
    {
        R_Articulo R_Art = new R_Articulo ();
        R_DetallePedido R_DetPe = new R_DetallePedido();
        R_Pedido R_Pe = new R_Pedido();

        public bool Agregar(DetallePedidoDTO Nuevo)
        {
            bool Exit = false;
            try
            {

                DetallePedido nuevo = new DetallePedido()
                {
                    Articuloid = Nuevo.Articuloid.Articuloid,
                    Pedidoid = Nuevo.Pedidoid.Pedidoid,
                    Cantidad = Nuevo.Cantidad,
                    Preciovendido = Nuevo.Preciovendido
                };
                Exit = R_DetPe.Agregar(nuevo);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Exit;
        }

        public List<DetallePedidoDTO> DevolverTodoxID(int id)
        {
            List<DetallePedidoDTO> ListaDevolver = new List<DetallePedidoDTO>();
            Pedido pId = R_Pe.BuscarNro(id);
            List<DetallePedido> Lista = new List<DetallePedido>();
            Lista = R_DetPe.DevolverTodoxID(pId.Pedidoid) ;
            try
            {
                foreach (DetallePedido DP in Lista)
                {
                    Articulo Cli = R_Art.BuscarXId(DP.Articuloid);
                    Pedido P = R_Pe.BuscarID(DP.Pedidoid);
                    DetallePedidoDTO Aux = new DetallePedidoDTO(Cli, P, DP.Cantidad, DP.Preciovendido);
                    ListaDevolver.Add(Aux);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return ListaDevolver;
        }

        public bool ModificarCant(int pedidoid,int articuloid, int Cantidad)
        {
            bool Cant = false;
            try
            {
                DetallePedido det = R_DetPe.DevolverDetalle(pedidoid,articuloid);
                Cant = R_DetPe.ModificarCant(det,Cantidad);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Cant;
        }

        public bool Eliminar(int pedidoid, int articuloid)
        {
            bool Exit = false;
            try
            {

                Exit = R_DetPe.Eliminar(pedidoid,articuloid);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Exit;
        }

        public List<DetallePedidoDTO> DetallePedidoFactura(int cod,char tipo)
        {
            List<DetallePedidoDTO> ListaDevolver = new List<DetallePedidoDTO>();
            try
            {
                foreach (DetallePedido DP in R_DetPe.DetallePedidoFactura(cod, tipo))
                {
                    Articulo Cli = R_Art.BuscarXId(DP.Articuloid);
                    Pedido P = R_Pe.BuscarID(DP.Pedidoid);
                    DetallePedidoDTO Aux = new DetallePedidoDTO(Cli, P, DP.Cantidad, DP.Preciovendido);
                    ListaDevolver.Add(Aux);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return ListaDevolver;
        }

        public bool PerteneceAPedido(int pedidoid, int articuloidbase)
        {
            bool Exit = false;
            try
            {

                Exit = R_DetPe.PerteneceAPedido(pedidoid, articuloidbase);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Exit;
        }

        public bool ArtRepetidosEnElPedidoDetalle(List<DetallePedidoDTO> Total, Articulo nuevo)
        {
            bool Repetido = false;
            foreach (DetallePedidoDTO item in Total)
            {
                if (item.Articuloid.Articuloid == nuevo.Articuloid)
                {
                    Repetido = true;
                }
            }
            return Repetido;
        }
    }
}
