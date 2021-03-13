using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class DetallePedido
    {
        public int Articuloid { get; set; }
        public int Pedidoid { get; set; }
        public int Cantidad { get; set; }
        public double Preciovendido { get; set; }
    }
}
