using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IFacturacion
    {
        bool Agregar(Facturacion_Venta Nuevo, string entregado, int idpedido);
        List<Facturacion_Venta> DevolverTodo();
        Facturacion_Venta Buscar(int cod, char tipofactura);
        List<Facturacion_Venta> ListarFacturaFiltroCliente(string TxtFiltro);
        List<Facturacion_Venta> RangoTotal(DateTime Desde, DateTime Hasta);
    }
}
