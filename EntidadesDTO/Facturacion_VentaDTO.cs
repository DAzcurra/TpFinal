using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesDTO
{
    public class Facturacion_VentaDTO
    {
        private int cod_factura;
        private DateTime fecha;
        private double importetotal;
        private Cliente clienteid;
        private Empleado empleadoid;
        private string metododepago;
        private char tipodefactura;
        private Pedido pedidoid;
        public Facturacion_VentaDTO(int cod_factura, DateTime fecha, double importetotal, Cliente clienteid, Empleado empleadoid, string metododepago, Pedido pedidoid, char tipodefactura)
        {
            Cod_factura = cod_factura;
            Fecha = fecha;
            Importetotal = importetotal;
            Clienteid = clienteid;
            Empleadoid = empleadoid;
            Metododepago = metododepago;
            Pedidoid = pedidoid;
            Tipodefactura = tipodefactura;
        }

        public int Cod_factura
        {
            get
            {
                return cod_factura;
            }

            set
            {
                cod_factura = value;
            }
        }

        public DateTime Fecha
        {
            get
            {
                return fecha;
            }

            set
            {
                fecha = value;
            }
        }

        public double Importetotal
        {
            get
            {
                return importetotal;
            }

            set
            {
                importetotal = value;
            }
        }

        public Cliente Clienteid
        {
            get
            {
                return clienteid;
            }

            set
            {
                clienteid = value;
            }
        }

        public Empleado Empleadoid
        {
            get
            {
                return empleadoid;
            }

            set
            {
                empleadoid = value;
            }
        }

        public string Metododepago { get => metododepago; set => metododepago = value; }
        public char Tipodefactura { get => tipodefactura; set => tipodefactura = value; }
        public Pedido Pedidoid { get => pedidoid; set => pedidoid = value; }
    }
}
