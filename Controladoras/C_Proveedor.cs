using Entidades;
using Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controladoras
{
    public class C_Proveedor
    {
        Proveedor AuxProv = null;
        List<Proveedor> Lista = new List<Proveedor>();
        R_Proveedor R_Prov = new R_Proveedor();

        public bool Agregar(string nombrefantasia, string razonsocial, string nombre, string apellido, string telefono, string email, string cuit, string direccion)
        {
            bool Agre = false;
            try
            {
                Agre = R_Prov.Agregar(0,nombrefantasia,razonsocial,nombre,apellido,telefono,email,cuit,direccion);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Agre;
        }

        public List<Proveedor> Listar()
        {
            try
            {
                Lista = R_Prov.Listar();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Lista;
        }
        public List<Proveedor> ListarActivos()
        {
            try
            {
                Lista = R_Prov.ListarActivo();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Lista;
        }
        public Proveedor BuscarxCuit(string cuit)
        {
            try
            {
                AuxProv = R_Prov.BuscarxCuit(cuit);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return AuxProv;
        }

        public Proveedor BuscarxId(int id)
        {
            try
            {
                AuxProv = R_Prov.BuscarxId(id);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return AuxProv;
        }

        public List<Proveedor> BuscarxNombreFantasia(string NFantasia)
        {
            try
            {
                Lista = R_Prov.BuscarxNombreFantasia(NFantasia);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Lista;
        }

        public List<Proveedor> BuscarxRazonSocial(string RazonS)
        {
            try
            {
                Lista = R_Prov.BuscarxRazonSocial(RazonS);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Lista;
        }

        public List<Proveedor> BuscarxApellido(string apellido)
        {
            try
            {
                Lista = R_Prov.BuscarxApellido(apellido);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Lista;
        }

        public bool Modificar(string cuit,Proveedor Aux)
        {
            bool Modif = false;
            try
            {
                Modif = R_Prov.Modificar(cuit,Aux);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Modif;
        }
        public bool Eliminar(string Cuit)
        {
            bool Exit = false;
            try
            {
                Exit = R_Prov.Eliminar(Cuit);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Exit;
        }
        public List<Proveedor> DependeDeProveedor(string cuit)
        {
            List<Proveedor> AuxDeP = new List<Proveedor>(); 
            try
            {
                Proveedor id = R_Prov.BuscarxCuit(cuit);
                if(id!= null)
                {
                     AuxDeP = R_Prov.DependeDeProveedor(id.Proveedorid);
                }
               
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return AuxDeP;
        }

        public bool DeshabilitarHabilitarProveedor(string cuit, string estado)
        {
            bool DH = false;
            try
            {
                Proveedor id = R_Prov.BuscarxCuit(cuit);
                if (id != null)
                {
                    DH = R_Prov.DeshabilitarHabilitarProveedor(id.Proveedorid,estado);
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
