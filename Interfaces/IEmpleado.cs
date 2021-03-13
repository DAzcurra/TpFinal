using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IEmpleado
    {
        bool Agregar(int empleadoid, string nombre, string apellido, string telefono, string email, string direccion, string cuil);
        List<Empleado> Listar();
        Empleado Buscar(string cuil);
        List<Empleado> BuscarEmp(string Apellido);
        bool Eliminar(string cuil);
        bool Modificar(string cuil,Empleado Aux);
        Empleado BuscarID(int id);
        bool DeshabilitarHabilitarEmpleados(int id, string estado);
        List<Empleado> BuscarEmpActivo(string apellido);
        Empleado BuscarActivos(string cuil);
        List<Empleado> ListarActivos();
        List<Empleado> DependeDeFactura(int id);
    }
}
