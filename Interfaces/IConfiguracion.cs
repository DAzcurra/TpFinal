using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IConfiguracion
    {
        int NroMaxPaginar();
        void ProximoPedido(int num);
        void ProximaFacturaA(int num);
        void ProximaFacturaB(int num);
        int DevolverUltimoPedido();
        int DevolverUltimaFacturaA();
        int DevolverUltimaFacturaB();
        Configuracion BuscarUsuario();
        bool ModificarUsuario(string user, string password);
    }
}
