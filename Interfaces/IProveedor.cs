using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IProveedor
    {
        bool Agregar(int proveedorid,string nombrefantasia, string razonsocial, string nombre, string apellido, string telefono, string email, string cuit, string direccion);
        List<Proveedor> Listar();
        List<Proveedor> ListarActivo();
        List<Proveedor> BuscarxNombreFantasia(string NFantasia);
        List<Proveedor> BuscarxRazonSocial(string RazonS);
        Proveedor BuscarxCuit(string cuit);
        Proveedor BuscarxId(int id);
        List<Proveedor> BuscarxApellido(string apellido);
        bool Eliminar(string cuit);
        bool Modificar(string cuit,Proveedor Aux);
        List<Proveedor> DependeDeProveedor(int id);
        bool DeshabilitarHabilitarProveedor(int id, string estado);
    }
}
