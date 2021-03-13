using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Facturacion_Venta
    {
        public int Cod_factura { get; set; }
        public DateTime Fecha { get; set; }
        public double Importetotal { get; set; }
        public int Clienteid { get; set; }
        public int Empleadoid { get; set; }
        public string Metododepago { get; set; }
        public int Pedidoid { get; set; }
        public char Tipodefactura { get; set; }
    }
}
