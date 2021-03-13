using Entidades;
using EntidadesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IPedido
    {
        bool Agregar(Pedido Nuevo, List<DetallePedido> NuevoDetalle);
        bool Eliminar(int pedidoid);
        Pedido BuscarID(int id);
        List<Pedido> DevolverTodo();
        List<Pedido> ListaClienteAPellidoNombre(string nombre);
        Pedido BuscarNro(int nropedido);
        Pedido BuscarNroPedidoEstadoEntregado(int nro);
        List<Pedido> RangoTotal(DateTime Desde, DateTime Hasta);
        List<Pedido> ListaClienteAPellidoNombreTotal(string nombre);
        Pedido BuscarIDTotal(int id);
        bool DeshabilitarHabilitarPedido(int id, string estado);
        void PedidoEntregado(int id, string entregado, int cod);
        List<Pedido> ListarPedidoFiltroCliente(string TxtFiltro);
    }
}
