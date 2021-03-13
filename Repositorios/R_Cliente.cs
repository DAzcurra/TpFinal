using Conexiones;
using Entidades;
using Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Web.Services;
using System.Data;

namespace Repositorios
{

    public class R_Cliente :ICliente
    {
        Conexion conex = new Conexion();

        public bool Agregar(int clienteid, string nombre, string apellido, string telefono, string email, string direccion, string cuil, string razonsocial, string tipo)
        {
            bool Agre = false;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "INSERT INTO clientes(clienteid,nombre,apellido,telefono,email,direccion,cuil,razonsocial,tipo) VALUES (DEFAULT,@nombre,@apellido,@telefono,@email,@direccion,@cuil,@razonsocial,@tipo)";
                Comando.Parameters.Add("@nombre", MySqlDbType.VarChar);
                Comando.Parameters["@nombre"].Value = nombre;
                Comando.Parameters.Add("@apellido", MySqlDbType.VarChar);
                Comando.Parameters["@apellido"].Value = apellido;
                Comando.Parameters.Add("@telefono", MySqlDbType.VarChar);
                Comando.Parameters["@telefono"].Value = telefono;
                Comando.Parameters.Add("@email", MySqlDbType.VarChar);
                Comando.Parameters["@email"].Value = email;
                Comando.Parameters.Add("@direccion", MySqlDbType.VarChar);
                Comando.Parameters["@direccion"].Value = direccion;
                Comando.Parameters.Add("@cuil", MySqlDbType.VarChar);
                Comando.Parameters["@cuil"].Value = cuil;
                Comando.Parameters.Add("@razonsocial", MySqlDbType.VarChar);
                Comando.Parameters["@razonsocial"].Value = razonsocial;
                Comando.Parameters.Add("@tipo", MySqlDbType.VarChar);
                Comando.Parameters["@tipo"].Value = tipo;
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

        public List<Cliente> BuscarxApellido(string apellido)
        {
            List<Cliente> Aux = new List<Cliente>();
            Cliente Cli = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM clientes WHERE apellido = @apellido ORDER BY apellido ASC";
                Comando.Parameters.Add("@apellido", MySqlDbType.VarChar);
                Comando.Parameters["@apellido"].Value = apellido;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    Cli = new Cliente()
                    {
                        Clienteid = Dr.GetInt32(0),
                        Nombre = Dr.GetString(1),
                        Apellido = Dr.GetString(2),
                        Telefono = Dr.GetString(3),
                        Email = Dr.GetString(4),
                        Direccion = Dr.GetString(5),
                        Cuil = Dr.GetString(6),
                        Razonsocial = Dr.GetString(7),
                        Tipo = Dr.GetString(8),
                        Estado = Dr.GetString(9)

                    };
                    Aux.Add(Cli);
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

        public List<Cliente> BuscarxApellidoActivos(string apellido)
        {
            List<Cliente> Aux = new List<Cliente>();
            Cliente Cli = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM clientes WHERE apellido = @apellido AND estado = 'activo' ORDER BY apellido ASC";
                Comando.Parameters.Add("@apellido", MySqlDbType.VarChar);
                Comando.Parameters["@apellido"].Value = apellido;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    Cli = new Cliente()
                    {
                        Clienteid = Dr.GetInt32(0),
                        Nombre = Dr.GetString(1),
                        Apellido = Dr.GetString(2),
                        Telefono = Dr.GetString(3),
                        Email = Dr.GetString(4),
                        Direccion = Dr.GetString(5),
                        Cuil = Dr.GetString(6),
                        Razonsocial = Dr.GetString(7),
                        Tipo = Dr.GetString(8),
                        Estado = Dr.GetString(9)

                    }; Aux.Add(Cli);
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
        public List<Cliente> BuscarxRazonS(string razonsocial)
        {
            List<Cliente> Aux = new List<Cliente>();
            Cliente Cli = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM clientes WHERE razonsocial = @razonsocial ORDER BY apellido ASC";
                Comando.Parameters.Add("@razonsocial", MySqlDbType.VarChar);
                Comando.Parameters["@razonsocial"].Value = razonsocial;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    Cli = new Cliente()
                    {
                        Clienteid = Dr.GetInt32(0),
                        Nombre = Dr.GetString(1),
                        Apellido = Dr.GetString(2),
                        Telefono = Dr.GetString(3),
                        Email = Dr.GetString(4),
                        Direccion = Dr.GetString(5),
                        Cuil = Dr.GetString(6),
                        Razonsocial = Dr.GetString(7),
                        Tipo = Dr.GetString(8),
                        Estado = Dr.GetString(9)

                    }; Aux.Add(Cli);
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

        public List<Cliente> BuscarxTipo(string tipo)
        {
            List<Cliente> Aux = new List<Cliente>();
            Cliente Cli = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM clientes WHERE tipo = @tipo";
                Comando.Parameters.Add("@tipo", MySqlDbType.VarChar);
                Comando.Parameters["@tipo"].Value = tipo;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    Cli = new Cliente()
                    {
                        Clienteid = Dr.GetInt32(0),
                        Nombre = Dr.GetString(1),
                        Apellido = Dr.GetString(2),
                        Telefono = Dr.GetString(3),
                        Email = Dr.GetString(4),
                        Direccion = Dr.GetString(5),
                        Cuil = Dr.GetString(6),
                        Razonsocial = Dr.GetString(7),
                        Tipo = Dr.GetString(8),
                        Estado = Dr.GetString(9)

                    }; Aux.Add(Cli);
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

        public bool Eliminar(string cuil)
        {
            bool Elim = false;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "DELETE FROM clientes WHERE cuil=@cuil";
                Comando.Parameters.Add("@cuil", MySqlDbType.VarChar);
                Comando.Parameters["@cuil"].Value = cuil;
                Comando.Prepare();
                Comando.ExecuteNonQuery();
                Elim = true;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally
            { conex.CerrarConexion(); }
            return Elim;
        }

        public List<Cliente> Listar()
        {
            List<Cliente> Aux = new List<Cliente>();
            Cliente Cli = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM clientes ORDER BY apellido ASC";
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    Cli = new Cliente()
                    {
                        Clienteid = Dr.GetInt32(0),
                        Nombre = Dr.GetString(1),
                        Apellido = Dr.GetString(2),
                        Telefono = Dr.GetString(3),
                        Email = Dr.GetString(4),
                        Direccion = Dr.GetString(5),
                        Cuil = Dr.GetString(6),
                        Razonsocial = Dr.GetString(7),
                        Tipo = Dr.GetString(8),
                        Estado = Dr.GetString(9)

                    }; Aux.Add(Cli);
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

        public bool Modificar(string cuil, Cliente Aux)
        {
            Cliente CAux2;
            Cliente CAux;
            CAux2 = Buscarcuil(cuil);
            CAux = Buscarcuil(Aux.Cuil);
            bool Actu = false;

            if ((CAux2 != null && CAux == null) || (CAux.Cuil == CAux2.Cuil))
            {
                try
                {
                    conex.AbrirConexion();
                    MySqlCommand Comando = conex.CrearComando();
                    Comando.CommandText = "UPDATE clientes SET nombre=@nombre,apellido=@apellido,telefono=@telefono,email=@email,direccion=@direccion,cuil=@cuil,razonsocial=@razonsocial,tipo=@tipo, estado=@estado WHERE cuil = @AuxId";
                    Comando.Parameters.Add("@AuxId", MySqlDbType.VarChar);
                    Comando.Parameters["@AuxId"].Value = cuil;
                    Comando.Parameters.Add("@nombre", MySqlDbType.VarChar);
                    Comando.Parameters["@nombre"].Value = Aux.Nombre;
                    Comando.Parameters.Add("@apellido", MySqlDbType.VarChar);
                    Comando.Parameters["@apellido"].Value = Aux.Apellido;
                    Comando.Parameters.Add("@telefono", MySqlDbType.VarChar);
                    Comando.Parameters["@telefono"].Value = Aux.Telefono;
                    Comando.Parameters.Add("@email", MySqlDbType.VarChar);
                    Comando.Parameters["@email"].Value = Aux.Email;
                    Comando.Parameters.Add("@direccion", MySqlDbType.VarChar);
                    Comando.Parameters["@direccion"].Value = Aux.Direccion;
                    Comando.Parameters.Add("@cuil", MySqlDbType.VarChar);
                    Comando.Parameters["@cuil"].Value = Aux.Cuil;
                    Comando.Parameters.Add("@razonsocial", MySqlDbType.VarChar);
                    Comando.Parameters["@razonsocial"].Value = Aux.Razonsocial;
                    Comando.Parameters.Add("@tipo", MySqlDbType.VarChar);
                    Comando.Parameters["@tipo"].Value = Aux.Tipo;
                    Comando.Parameters.Add("@estado", MySqlDbType.VarChar);
                    Comando.Parameters["@estado"].Value = Aux.Estado;
                    Comando.Prepare();
                    Comando.ExecuteNonQuery();
                    Actu = true;
                }
                catch (Exception Ex)
                {
                    throw new Exception(Ex.Message);
                }
                finally
                { conex.CerrarConexion(); }

            }
            return Actu;
        }

        public Cliente BuscarID(int CliID)
        {
            Cliente Aux = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM clientes WHERE clienteid = @clienteid";
                Comando.Parameters.Add("@clienteid", MySqlDbType.Int32);
                Comando.Parameters["@clienteid"].Value = CliID;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                if (Dr.Read())
                {
                    Aux = new Cliente()
                    {
                        Clienteid = Dr.GetInt32(0),
                        Nombre = Dr.GetString(1),
                        Apellido = Dr.GetString(2),
                        Telefono = Dr.GetString(3),
                        Email = Dr.GetString(4),
                        Direccion = Dr.GetString(5),
                        Cuil = Dr.GetString(6),
                        Razonsocial = Dr.GetString(7),
                        Tipo = Dr.GetString(8),
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

        public bool DeshabilitarHabilitarCliente(int id, string estado)
        {
            bool DH = false;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "UPDATE clientes SET estado=@estado WHERE clienteid = @AuxId";
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

        public Cliente BuscarIDActivos(int CliID)
        {
            Cliente Aux = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM clientes WHERE clienteid = @clienteid AND estado = 'activo'";
                Comando.Parameters.Add("@clienteid", MySqlDbType.Int32);
                Comando.Parameters["@clienteid"].Value = CliID;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                if (Dr.Read())
                {
                    Aux = new Cliente()
                    {
                        Clienteid = Dr.GetInt32(0),
                        Nombre = Dr.GetString(1),
                        Apellido = Dr.GetString(2),
                        Telefono = Dr.GetString(3),
                        Email = Dr.GetString(4),
                        Direccion = Dr.GetString(5),
                        Cuil = Dr.GetString(6),
                        Razonsocial = Dr.GetString(7),
                        Tipo = Dr.GetString(8),
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

        public Cliente Buscarcuil(string cuil)
        {
            Cliente Aux = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM clientes WHERE cuil = @cuil";
                Comando.Parameters.Add("@cuil", MySqlDbType.VarChar);
                Comando.Parameters["@cuil"].Value = cuil;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                if (Dr.Read())
                {
                    Aux = new Cliente()
                    {
                        Clienteid = Dr.GetInt32(0),
                        Nombre = Dr.GetString(1),
                        Apellido = Dr.GetString(2),
                        Telefono = Dr.GetString(3),
                        Email = Dr.GetString(4),
                        Direccion = Dr.GetString(5),
                        Cuil = Dr.GetString(6),
                        Razonsocial = Dr.GetString(7),
                        Tipo = Dr.GetString(8),
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

        public Cliente BuscarcuilActivo(string cuil)
        {
            Cliente Aux = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM clientes WHERE cuil = @cuil AND estado = 'activo'";
                Comando.Parameters.Add("@cuil", MySqlDbType.VarChar);
                Comando.Parameters["@cuil"].Value = cuil;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                if (Dr.Read())
                {
                    Aux = new Cliente()
                    {
                        Clienteid = Dr.GetInt32(0),
                        Nombre = Dr.GetString(1),
                        Apellido = Dr.GetString(2),
                        Telefono = Dr.GetString(3),
                        Email = Dr.GetString(4),
                        Direccion = Dr.GetString(5),
                        Cuil = Dr.GetString(6),
                        Razonsocial = Dr.GetString(7),
                        Tipo = Dr.GetString(8),
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

        public List<Cliente> DependeDeFacturayPedido(int id)
        {
            List<Cliente> LisAux = new List<Cliente>();
            Cliente CliAux = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM clientes AS cli INNER JOIN facturaciones as fac ON(cli.clienteid=fac.clienteid) INNER JOIN pedidos as ped ON(cli.clienteid=ped.clienteid) WHERE cli.clienteid=@idp";
                Comando.Parameters.Add("@idp", MySqlDbType.Int32);
                Comando.Parameters["@idp"].Value = id;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    CliAux = new Cliente()
                    {
                        Clienteid = Dr.GetInt32(0),
                        Nombre = Dr.GetString(1),
                        Apellido = Dr.GetString(2),
                        Telefono = Dr.GetString(3),
                        Email = Dr.GetString(4),
                        Direccion = Dr.GetString(5),
                        Cuil = Dr.GetString(6),
                        Razonsocial = Dr.GetString(7),
                        Tipo = Dr.GetString(8),
                        Estado = Dr.GetString(9)

                    }; ;
                    LisAux.Add(CliAux);
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

    }
}
