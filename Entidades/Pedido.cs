using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Pedido
    {
        public int Pedidoid { get; set; }
        public int Nropedido { get; set; }
        public DateTime Fecha { get; set; }
        public string Entregado { get; set; }
        public int Clienteid { get; set; }
        public string Estado { get; set; }
    }
}
