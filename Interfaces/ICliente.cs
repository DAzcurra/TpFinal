using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ICliente
    {
        bool Agregar(int clienteid,string nombre, string apellido, string telefono, string email, string direccion, string cuil, string razonsocial, string tipo);
        List<Cliente> Listar();
        List<Cliente> BuscarxRazonS(string apellido);
        List<Cliente> BuscarxApellido(string razonsocial);
        List<Cliente> BuscarxTipo(string tipo);
        Cliente BuscarID(int CliID);
        Cliente Buscarcuil(string CliID);
        bool Eliminar(string cuil);
        bool Modificar(string cuil, Cliente Aux);
        List<Cliente> BuscarxApellidoActivos(string apellido);
        Cliente BuscarIDActivos(int CliID);
        Cliente BuscarcuilActivo(string cuil);
        bool DeshabilitarHabilitarCliente(int id, string estado);
        List<Cliente> DependeDeFacturayPedido(int id);

    }
}
