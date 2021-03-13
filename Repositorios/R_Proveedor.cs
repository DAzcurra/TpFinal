using Conexiones;
using Entidades;
using Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorios
{
    public class R_Proveedor : IProveedor
    {
        Conexion conex = new Conexion();

        public bool Agregar(int proveedorid, string nombrefantasia, string razonsocial, string nombre, string apellido, string telefono, string email, string cuit, string direccion)
        {
            bool Agre = false;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "INSERT INTO proveedores(proveedorid,nombrefantasia,razonsocial,nombre,apellido,telefono,email,cuit,direccion) VALUES (DEFAULT,@nombrefantasia,@razonsocial,@nombre,@apellido,@telefono,@email,@cuit,@direccion)";
                Comando.Parameters.Add("@proveedorid", MySqlDbType.Int32);
                Comando.Parameters["@proveedorid"].Value = proveedorid;
                Comando.Parameters.Add("@nombrefantasia", MySqlDbType.VarChar);
                Comando.Parameters["@nombrefantasia"].Value = nombrefantasia;
                Comando.Parameters.Add("@razonsocial", MySqlDbType.VarChar);
                Comando.Parameters["@razonsocial"].Value = razonsocial;
                Comando.Parameters.Add("@nombre", MySqlDbType.VarChar);
                Comando.Parameters["@nombre"].Value = nombre;
                Comando.Parameters.Add("@apellido", MySqlDbType.VarChar);
                Comando.Parameters["@apellido"].Value = apellido;
                Comando.Parameters.Add("@telefono", MySqlDbType.VarChar);
                Comando.Parameters["@telefono"].Value = telefono;
                Comando.Parameters.Add("@email", MySqlDbType.VarChar);
                Comando.Parameters["@email"].Value = email;
                Comando.Parameters.Add("@cuit", MySqlDbType.VarChar);
                Comando.Parameters["@cuit"].Value = cuit;
                Comando.Parameters.Add("@direccion", MySqlDbType.VarChar);
                Comando.Parameters["@direccion"].Value = direccion;

                Comando.Prepare();
                Comando.ExecuteNonQuery();
                Agre = true;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally { conex.CerrarConexion(); }
            return Agre;
        }

        public List<Proveedor> BuscarxNombreFantasia(string NFantasia)
        {
            Proveedor Aux = null;
            List<Proveedor> Lista = new List<Proveedor>();
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM proveedores WHERE nombrefantasia = @nombrefantasia";
                Comando.Parameters.Add("@nombrefantasia", MySqlDbType.VarChar);
                Comando.Parameters["@nombrefantasia"].Value = NFantasia;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    Aux = new Proveedor()
                    {

                        Proveedorid = Dr.GetInt32(0),
                        Nombrefantasia = Dr.GetString(1),
                        Razonsocial = Dr.GetString(2),
                        Nombre = Dr.GetString(3),
                        Apellido = Dr.GetString(4),
                        Telefono = Dr.GetString(5),
                        Email = Dr.GetString(6),
                        Cuit = Dr.GetString(7),
                        Direccion = Dr.GetString(8),
                        Estado = Dr.GetString(9)
                    };
                    Lista.Add(Aux);
                }
                Dr.Close();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally { conex.CerrarConexion(); }
            return Lista;
        }
        public Proveedor BuscarxCuit(string cuit)
        {
            Proveedor Aux = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM proveedores WHERE cuit = @cuit";
                Comando.Parameters.Add("@cuit", MySqlDbType.VarChar);
                Comando.Parameters["@cuit"].Value = cuit;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                if (Dr.Read())
                {
                    Aux = new Proveedor()
                    {

                        Proveedorid = Dr.GetInt32(0),
                        Nombrefantasia = Dr.GetString(1),
                        Razonsocial = Dr.GetString(2),
                        Nombre = Dr.GetString(3),
                        Apellido = Dr.GetString(4),
                        Telefono = Dr.GetString(5),
                        Email = Dr.GetString(6),
                        Cuit = Dr.GetString(7),
                        Direccion = Dr.GetString(8),
                        Estado = Dr.GetString(9)
                    };
                }
                Dr.Close();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally { conex.CerrarConexion(); }
            return Aux;
        }

        public List<Proveedor> BuscarxRazonSocial(string RazonS)
        {
            Proveedor Aux = null;
            List<Proveedor> Lista = new List<Proveedor>();
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM proveedores WHERE razonsocial = @razonsocial";
                Comando.Parameters.Add("@razonsocial", MySqlDbType.VarChar);
                Comando.Parameters["@razonsocial"].Value = RazonS;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    Aux = new Proveedor()
                    {

                        Proveedorid = Dr.GetInt32(0),
                        Nombrefantasia = Dr.GetString(1),
                        Razonsocial = Dr.GetString(2),
                        Nombre = Dr.GetString(3),
                        Apellido = Dr.GetString(4),
                        Telefono = Dr.GetString(5),
                        Email = Dr.GetString(6),
                        Cuit = Dr.GetString(7),
                        Direccion = Dr.GetString(8),
                        Estado = Dr.GetString(9)
                    }; Lista.Add(Aux);
                }
                Dr.Close();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally { conex.CerrarConexion(); }
            return Lista;
        }

        public List<Proveedor> BuscarxApellido(string apellido)
        {
            Proveedor Aux = null;
            List<Proveedor> Lista = new List<Proveedor>();
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM proveedores WHERE apellido = @apellido";
                Comando.Parameters.Add("@apellido", MySqlDbType.VarChar);
                Comando.Parameters["@apellido"].Value = apellido;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    Aux = new Proveedor()
                    {

                        Proveedorid = Dr.GetInt32(0),
                        Nombrefantasia = Dr.GetString(1),
                        Razonsocial = Dr.GetString(2),
                        Nombre = Dr.GetString(3),
                        Apellido = Dr.GetString(4),
                        Telefono = Dr.GetString(5),
                        Email = Dr.GetString(6),
                        Cuit = Dr.GetString(7),
                        Direccion = Dr.GetString(8),
                        Estado = Dr.GetString(9)
                    }; Lista.Add(Aux);
                }
                Dr.Close();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally { conex.CerrarConexion(); }
            return Lista;
        }

        public bool Eliminar(string cuit)
        {
            bool Elim = false;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "DELETE FROM proveedores WHERE cuit=@cuit";
                Comando.Parameters.Add("@cuit", MySqlDbType.VarChar);
                Comando.Parameters["@cuit"].Value = cuit;
                Comando.Prepare();
                Comando.ExecuteNonQuery();
                Elim = true;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally { conex.CerrarConexion(); }
            return Elim;
        }

        public List<Proveedor> Listar()
        {
            Proveedor Aux = null;
            List<Proveedor> Lista = new List<Proveedor>();
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM proveedores ORDER BY apellido ASC";
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    Aux = new Proveedor()
                    {

                        Proveedorid = Dr.GetInt32(0),
                        Nombrefantasia = Dr.GetString(1),
                        Razonsocial = Dr.GetString(2),
                        Nombre = Dr.GetString(3),
                        Apellido = Dr.GetString(4),
                        Telefono = Dr.GetString(5),
                        Email = Dr.GetString(6),
                        Cuit = Dr.GetString(7),
                        Direccion = Dr.GetString(8),
                        Estado = Dr.GetString(9)
                    }; Lista.Add(Aux);
                }
                Dr.Close();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally { conex.CerrarConexion(); }
            return Lista;
        }

        public List<Proveedor> ListarActivo()
        {
            Proveedor Aux = null;
            List<Proveedor> Lista = new List<Proveedor>();
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM proveedores WHERE estado = 'activo' ORDER BY apellido ASC";
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    Aux = new Proveedor()
                    {

                        Proveedorid = Dr.GetInt32(0),
                        Nombrefantasia = Dr.GetString(1),
                        Razonsocial = Dr.GetString(2),
                        Nombre = Dr.GetString(3),
                        Apellido = Dr.GetString(4),
                        Telefono = Dr.GetString(5),
                        Email = Dr.GetString(6),
                        Cuit = Dr.GetString(7),
                        Direccion = Dr.GetString(8),
                        Estado = Dr.GetString(9)
                    }; Lista.Add(Aux);
                }
                Dr.Close();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally { conex.CerrarConexion(); }
            return Lista;
        }

        public bool Modificar(string cuit, Proveedor Aux)
        {
            Proveedor PAux;
            PAux = BuscarxCuit(cuit);
            Proveedor PAux2;
            PAux2 = BuscarxCuit(Aux.Cuit);
            bool Modif = false;
            if (PAux != null && PAux2 == null || PAux.Cuit == PAux2.Cuit)
            {
                try
                {
                    conex.AbrirConexion();
                    MySqlCommand Comando = conex.CrearComando();
                    Comando.CommandText = "UPDATE proveedores SET nombrefantasia=@nombrefantasia,razonsocial=@razonsocial,nombre=@nombre,apellido=@apellido,telefono=@telefono,email=@email,cuit=@cuit,direccion=@direccion,estado=@estado WHERE cuit = @AuxCuit";
                    Comando.Parameters.Add("@AuxCuit", MySqlDbType.VarChar);
                    Comando.Parameters["@AuxCuit"].Value = cuit;
                    Comando.Parameters.Add("@nombrefantasia", MySqlDbType.VarChar);
                    Comando.Parameters["@nombrefantasia"].Value = Aux.Nombrefantasia;
                    Comando.Parameters.Add("@razonsocial", MySqlDbType.VarChar);
                    Comando.Parameters["@razonsocial"].Value = Aux.Razonsocial;
                    Comando.Parameters.Add("@nombre", MySqlDbType.VarChar);
                    Comando.Parameters["@nombre"].Value = Aux.Nombre;
                    Comando.Parameters.Add("@apellido", MySqlDbType.VarChar);
                    Comando.Parameters["@apellido"].Value = Aux.Apellido;
                    Comando.Parameters.Add("@telefono", MySqlDbType.VarChar);
                    Comando.Parameters["@telefono"].Value = Aux.Telefono;
                    Comando.Parameters.Add("@email", MySqlDbType.VarChar);
                    Comando.Parameters["@email"].Value = Aux.Email;
                    Comando.Parameters.Add("@cuit", MySqlDbType.VarChar);
                    Comando.Parameters["@cuit"].Value = Aux.Cuit;
                    Comando.Parameters.Add("@direccion", MySqlDbType.VarChar);
                    Comando.Parameters["@direccion"].Value = Aux.Direccion;
                    Comando.Parameters.Add("@estado", MySqlDbType.VarChar);
                    Comando.Parameters["@estado"].Value = Aux.Estado;
                    Comando.Prepare();
                    Comando.ExecuteNonQuery();
                    Modif = true;
                }
                catch (Exception Ex)
                {
                    throw new Exception(Ex.Message);
                }
                finally { conex.CerrarConexion(); }
            }

            return Modif;
        }

        public Proveedor BuscarxId(int id)
        {
            Proveedor Aux = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM proveedores WHERE proveedorid = @proveedorid";
                Comando.Parameters.Add("@proveedorid", MySqlDbType.Int32);
                Comando.Parameters["@proveedorid"].Value = id;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                if (Dr.Read())
                {
                    Aux = new Proveedor()
                    {

                        Proveedorid = Dr.GetInt32(0),
                        Nombrefantasia = Dr.GetString(1),
                        Razonsocial = Dr.GetString(2),
                        Nombre = Dr.GetString(3),
                        Apellido = Dr.GetString(4),
                        Telefono = Dr.GetString(5),
                        Email = Dr.GetString(6),
                        Cuit = Dr.GetString(7),
                        Direccion = Dr.GetString(8),
                        Estado = Dr.GetString(9)
                    };
                }
                Dr.Close();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally { conex.CerrarConexion(); }
            return Aux;
        }

        public List<Proveedor> DependeDeProveedor(int id)
        {
            List<Proveedor> LisAux = new List<Proveedor>();
            Proveedor ProvAux = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM proveedores AS Pv INNER JOIN articuloxproveedor as Ap ON(Pv.proveedorid=Ap.proveedorid) WHERE Pv.proveedorid=@idp";
                Comando.Parameters.Add("@idp", MySqlDbType.Int32);
                Comando.Parameters["@idp"].Value = id;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    ProvAux = new Proveedor()
                    {

                        Proveedorid = Dr.GetInt32(0),
                        Nombrefantasia = Dr.GetString(1),
                        Razonsocial = Dr.GetString(2),
                        Nombre = Dr.GetString(3),
                        Apellido = Dr.GetString(4),
                        Telefono = Dr.GetString(5),
                        Email = Dr.GetString(6),
                        Cuit = Dr.GetString(7),
                        Direccion = Dr.GetString(8),
                        Estado = Dr.GetString(9)
                    }; LisAux.Add(ProvAux);
                }
                Dr.Close();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally { conex.CerrarConexion(); }
            return LisAux;

        }
        
        public bool DeshabilitarHabilitarProveedor(int id,string estado)
        {
            bool DH = false;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "UPDATE proveedores SET estado=@estado WHERE proveedorid = @AuxId";
                Comando.Parameters.Add("@AuxId", MySqlDbType.Int32);
                Comando.Parameters["@AuxId"].Value = id;
                Comando.Parameters.Add("@estado", MySqlDbType.VarChar);
                Comando.Parameters["@estado"].Value = estado;
                Comando.Prepare();
                Comando.ExecuteNonQuery();
                DH = true;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally { conex.CerrarConexion(); }
            return DH;
        }

    }
}

