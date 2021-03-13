using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Informes
{
    public class EPedido
    {
        private int pedidoid;
        private int nropedido;
        private DateTime fecha;
        private string entregado;
        private int cod_factura;
        private Cliente clienteid;
        private string estado;
        public EPedido(int pedidoid, int nropedido, DateTime fecha, string entregado, int cod_factura, Cliente clienteid, string estado)
        {
            Pedidoid = pedidoid;
            Nropedido = nropedido;
            Fecha = fecha;
            Entregado = entregado;
            Cod_factura = cod_factura;
            Clienteid = clienteid;
            Estado = estado;
        }

        public int Pedidoid { get => pedidoid; set => pedidoid = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public string Entregado { get => entregado; set => entregado = value; }
        public int Cod_factura { get => cod_factura; set => cod_factura = value; }
        public Cliente Clienteid { get => clienteid; set => clienteid = value; }
        public string Estado { get => estado; set => estado = value; }
        public int Nropedido { get => nropedido; set => nropedido = value; }
    }

    }
}
