using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Configuracion
    {

        public int NroMaxPaginas { get; set; }
        public int ProximafacturaA { get; set; }
        public int ProximafacturaB { get; set; }
        public int Proximopedido { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
    }
}
