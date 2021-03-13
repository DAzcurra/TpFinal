using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Articulo
    {

        public int Articuloid { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Marca { get; set; }
        public double Precioactual { get; set; }
        public int Cantidad { get; set; }
        public int Stockmin { get; set; }
        public string Estado { get; set; }
    }
}
