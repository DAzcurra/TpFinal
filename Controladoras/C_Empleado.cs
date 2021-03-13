using Entidades;
using Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controladoras
{
    public class C_Empleado
    {
        Empleado Aux = null;
        R_Empleado R_Emp = new R_Empleado();
        List<Empleado> ListaEmpleados = new List<Empleado>();
        public bool Agregar(string nombre, string apellido, string telefono, string email, string direccion, string cuil)
        {
            bool Exit = false;
            try
            {
                Exit = R_Emp.Agregar(0, nombre, apellido, telefono, email, direccion, cuil);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Exit;
        }

        public List<Empleado> DependeDeFactura(string cuil)
        {
            List<Empleado> AuxDeP = new List<Empleado>();
            try
            {
                Empleado id = R_Emp.Buscar(cuil); ;
                if (id != null)
                {
                    AuxDeP = R_Emp.DependeDeFactura(id.Empleadoid);
                }

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return AuxDeP;
        }

        public bool DeshabilitarHabilitarEmpleado(string cuil, string estado)
        {
            bool DH = false;
            try
            {
                Empleado id = R_Emp.BuscarActivos(cuil);
                if (id != null)
                {
                    DH = R_Emp.DeshabilitarHabilitarEmpleados(id.Empleadoid,estado);
                }

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return DH;
        }

        public List<Empleado> Listar()
        {
            try
            {
                ListaEmpleados = R_Emp.Listar();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return ListaEmpleados;
        }
        public List<Empleado> ListarActivos()
        {
            try
            {
                ListaEmpleados = R_Emp.ListarActivos();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return ListaEmpleados;
        }
        public Empleado Buscar(string cuil)
        {
            try
            {
                Aux = R_Emp.Buscar(cuil);
            }
            catch (Exception Ex)
            { throw new Exception(Ex.Message); }
            return Aux;
        }

        public Empleado BuscarID(int id)
        {
            try
            {
                Aux = R_Emp.BuscarID(id);
            }
            catch (Exception Ex)
            { throw new Exception(Ex.Message); }
            return Aux;
        }

        public List<Empleado> BuscarEmp(string apellido)
        {
            try
            {
                ListaEmpleados = R_Emp.BuscarEmp(apellido);
            }
            catch (Exception Ex)
            { throw new Exception(Ex.Message); }
            return ListaEmpleados;
        }

        public bool Modificar(string cuil,Empleado Aux)
        {
            bool Exit = false;
            try
            {
                Exit =  R_Emp.Modificar(cuil, Aux);
            }
            catch(Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Exit;
        }

        public bool Eliminar(string Cuil)
        {
            bool Exit = false;
            try
            {
               Exit=R_Emp.Eliminar(Cuil);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Exit;
        }
    }
}
