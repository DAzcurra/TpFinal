using Entidades;
using EntidadesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IDetallePedido
    {
        bool Agregar(DetallePedido Nuevo);
        bool Eliminar(int pedidoid, int articuloid);
        List<DetallePedido> DevolverTodoxID(int id);
        bool ModificarCant(DetallePedido det, int Cantidad);
        List<DetallePedido> DetallePedidoFactura(int cod, char tipo);

    }
}
