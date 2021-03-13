using Entidades;
using Interfaces;
using Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services;

namespace Controladoras
{
    public class C_Cliente
    {
        Cliente AuxCli = null;
        List<Cliente> Lista = new List<Cliente>();
        R_Cliente R_Cli = new R_Cliente(); 
        public bool Agregar(string nombre, string apellido, string telefono, string email, string direccion,string cuil, string razonsocial, string tipo)
        {
            bool Agre = false;
            try
            {
                Agre = R_Cli.Agregar(0,nombre,apellido,telefono,email,direccion,cuil,razonsocial,tipo);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Agre;
        }
        
        public List<Cliente> BuscarxApellidoActivos(string apellido)
        {
            try
            {
                Lista = R_Cli.BuscarxApellidoActivos(apellido);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Lista;
        }

        public List<Cliente> BuscarxApellido(string apellido)
        {
            try
            {
                Lista = R_Cli.BuscarxApellido(apellido);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Lista;
        }


        public List<Cliente> BuscarxRazonS(string razonsocial)
        {
            try
            {
                Lista = R_Cli.BuscarxRazonS(razonsocial);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Lista;
        }

        public List<Cliente> BuscarxTipo(string tipo)
        {
            try
            {
                Lista = R_Cli.BuscarxTipo(tipo);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Lista;
        }

        public bool Eliminar(string cuil)
        {
            bool Elim = false;
            try
            {
                Elim = R_Cli.Eliminar(cuil);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Elim;
        }

        public List<Cliente> Listar()
        {
            try
            {
                Lista = R_Cli.Listar();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Lista;
        }

        public bool Modificar(string cuil, Cliente Aux)
        {
            bool Modi = false;
            try
            {
                Modi = R_Cli.Modificar(cuil, Aux);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Modi;
        }

        public Cliente BuscarID(int CliID)
        {
            try
            {
                AuxCli = R_Cli.BuscarID(CliID);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return AuxCli;
        }

        public Cliente BuscarIDActivo(int CliID)
        {
            try
            {
                AuxCli = R_Cli.BuscarIDActivos(CliID);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return AuxCli;
        }

        public Cliente Buscarcuil(string cuil)
        {
            try
            {
                AuxCli = R_Cli.Buscarcuil(cuil);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return AuxCli;
        }
        
        public Cliente BuscarcuilActivo(string cuil)
        {
            try
            {
                AuxCli = R_Cli.BuscarcuilActivo(cuil);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return AuxCli;
        }
        public List<Cliente> DependeDeFacturayPedido(string cuil)
        {
            List<Cliente> AuxDeP = new List<Cliente>();
            try
            {
                Cliente id = R_Cli.Buscarcuil(cuil); ;
                if (id != null)
                {
                    AuxDeP = R_Cli.DependeDeFacturayPedido(id.Clienteid);
                }

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return AuxDeP;
        }

        public bool DeshabilitarHabilitarCliente(string cuil, string estado)
        {
            bool DH = false;
            try
            {
                Cliente id = R_Cli.BuscarcuilActivo(cuil);
                if (id != null)
                {
                    DH = R_Cli.DeshabilitarHabilitarCliente(id.Clienteid, estado);
                }

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return DH;
        }
    }
}
