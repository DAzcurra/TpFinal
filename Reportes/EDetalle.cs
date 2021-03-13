using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Informes
{
    public class EDetalle
    {
        private Articulo articuloid;
        private Pedido pedidoid;
        private int cantidad;
        private double preciovendido;

        //public EDetallePedido(Articulo articuloid, Pedido pedidoid, int cantidad, double preciovendido)
        //{
        //    Articuloid = articuloid;
        //    Pedidoid = pedidoid;
        //    Cantidad = cantidad;
        //    Preciovendido = preciovendido;
        //}

        public Articulo Articuloid
        {
            get
            {
                return articuloid;
            }

            set
            {
                articuloid = value;
            }
        }

        public Pedido Pedidoid
        {
            get
            {
                return pedidoid;
            }

            set
            {
                pedidoid = value;
            }
        }

        public int Cantidad
        {
            get
            {
                return cantidad;
            }

            set
            {
                cantidad = value;
            }
        }

        public double Preciovendido
        {
            get
            {
                return preciovendido;
            }

            set
            {
                preciovendido = value;
            }
        }
    }
}
