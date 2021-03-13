using Conexiones;
using Entidades;
using EntidadesDTO;
using Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorios
{
    public class R_Pedido : IPedido
    {
        Conexion conex = new Conexion();
        MySqlTransaction Transaccion;
        public bool Agregar(Pedido Nuevo,List<DetallePedido> NuevoDetalle)
        {
            bool Agre = false;
            bool commited = false;

            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Transaccion = conex.conexion.BeginTransaction(IsolationLevel.Serializable);
                MySqlCommand SelectComand = conex.CrearComando();
                SelectComand.Connection = conex.conexion;
                SelectComand.CommandText = "INSERT INTO pedidos(pedidoid,nropedido,fecha,clienteid) VALUES (DEFAULT,@nropedido,CURRENT_DATE,@clienteid)";
                SelectComand.Parameters.Add("@clienteid", MySqlDbType.Int32);
                SelectComand.Parameters["@clienteid"].Value = Nuevo.Clienteid;
                SelectComand.Parameters.Add("@nropedido", MySqlDbType.Int32);
                SelectComand.Parameters["@nropedido"].Value = Nuevo.Nropedido;
                SelectComand.Prepare();
                SelectComand.Transaction = Transaccion;
                SelectComand.ExecuteNonQuery();

                foreach (DetallePedido Aux in NuevoDetalle)
                {
                    Comando.Parameters.Clear();
                    Comando.CommandText= "INSERT INTO detallepedido(articuloid,pedidoid,cantidad,preciovendido) VALUES (@artid,LAST_INSERT_ID(),@cantidad,@preciovendido)";
                    Comando.Parameters.Add("@artid", MySqlDbType.Int32);
                    Comando.Parameters["@artid"].Value = Aux.Articuloid ;
                    Comando.Parameters.Add("@cantidad", MySqlDbType.Int32);
                    Comando.Parameters["@cantidad"].Value = Aux.Cantidad;
                    Comando.Parameters.Add("@preciovendido", MySqlDbType.Double);
                    Comando.Parameters["@preciovendido"].Value = Aux.Preciovendido;
                    Comando.Prepare();
                    Comando.ExecuteNonQuery();
                }
                Transaccion.Commit();

                commited = true;
                Agre = true;
            }
            catch(Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally
            {
                if (!commited)
                {
                    if (Transaccion != null)
                    {
                        Transaccion.Rollback();
                    }
                }

                if (conex.conexion.State.Equals(ConnectionState.Open))
                {
                    conex.CerrarConexion();
                }
            }
            return Agre;
        }

        public bool Eliminar(int pedidoid)
        {
            bool Exit = false;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "DELETE ped,dep FROM pedidos AS ped INNER JOIN detallepedido as dep ON (ped.pedidoid = dep.pedidoid) WHERE ped.pedidoid = @pedidoid";
                Comando.Parameters.Add("@pedidoid", MySqlDbType.Int32);
                Comando.Parameters["@pedidoid"].Value = pedidoid;
                Comando.Prepare();
                Comando.ExecuteNonQuery();
                Exit = true;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally
            { conex.CerrarConexion(); }
            return Exit;
        }

        public Pedido BuscarID(int id)
        {
            Pedido Pdd = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM pedidos WHERE pedidoid = @pedidoid AND estado = 'activo' AND entregado = 'en espera'";
                Comando.Parameters.Add("@pedidoid", MySqlDbType.Int32);
                Comando.Parameters["@pedidoid"].Value = id;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                if (Dr.Read())
                {
                    Pdd = new Pedido()
                    {
                        Pedidoid = Dr.GetInt32(0),
                        Nropedido = Dr.GetInt32(1),
                        Fecha = Dr.GetDateTime(2),
                        Entregado = Dr.GetString(3),
                        Clienteid = Dr.GetInt32(4),
                        Estado = Dr.GetString(5)
                    };
                }
                Dr.Close();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally { conex.CerrarConexion(); }
            return Pdd;
        }

        public Pedido BuscarIDTotal(int id)
        {
            Pedido Pdd = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM pedidos WHERE pedidoid = @pedidoid";
                Comando.Parameters.Add("@pedidoid", MySqlDbType.Int32);
                Comando.Parameters["@pedidoid"].Value = id;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                if (Dr.Read())
                {
                    Pdd = new Pedido()
                    {
                        Pedidoid = Dr.GetInt32(0),
                        Nropedido = Dr.GetInt32(1),
                        Fecha = Dr.GetDateTime(2),
                        Entregado = Dr.GetString(3),
                        Clienteid = Dr.GetInt32(4),
                        Estado = Dr.GetString(5)
                    };
                }
                Dr.Close();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally { conex.CerrarConexion(); }
            return Pdd;
        }

        public Pedido BuscarNroPedidoEstadoEntregado(int Nro)
        {
            Pedido Pdd = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM pedidos WHERE nropedido = @nropedido AND estado = 'activo' AND entregado = 'en espera'";
                Comando.Parameters.Add("@nropedido", MySqlDbType.Int32);
                Comando.Parameters["@nropedido"].Value = Nro;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                if (Dr.Read())
                {
                    Pdd = new Pedido()
                    {
                        Pedidoid = Dr.GetInt32(0),
                        Nropedido = Dr.GetInt32(1),
                        Fecha = Dr.GetDateTime(2),
                        Entregado = Dr.GetString(3),
                        Clienteid = Dr.GetInt32(4),
                        Estado = Dr.GetString(5)
                    };
                }
                Dr.Close();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally { conex.CerrarConexion(); }
            return Pdd;
        }

        public List<Pedido> DevolverTodo()
        {
            List<Pedido> Lista = new List<Pedido>();
            Pedido Pdd = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM pedidos";
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    Pdd = new Pedido()
                    {
                        Pedidoid = Dr.GetInt32(0),
                        Nropedido = Dr.GetInt32(1),
                        Fecha = Dr.GetDateTime(2),
                        Entregado = Dr.GetString(3),
                        Clienteid = Dr.GetInt32(4),
                        Estado = Dr.GetString(5)
                    }; Lista.Add(Pdd);
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

        public List<Pedido> Rango(DateTime Desde,DateTime Hasta)
        {
            List<Pedido> Lista = new List<Pedido>();
            Pedido Pdd = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM pedidos WHERE (fecha BETWEEN CAST(@Desde AS DATE) AND CAST(@Hasta AS DATE)) AND entregado = 'en espera' AND estado = 'activo'";
                Comando.Parameters.Add("@Desde", MySqlDbType.Date);
                Comando.Parameters["@Desde"].Value = Desde;
                Comando.Parameters.Add("@Hasta", MySqlDbType.Date);
                Comando.Parameters["@Hasta"].Value = Hasta;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    Pdd = new Pedido()
                    {
                        Pedidoid = Dr.GetInt32(0),
                        Nropedido = Dr.GetInt32(1),
                        Fecha = Dr.GetDateTime(2),
                        Entregado = Dr.GetString(3),
                        Clienteid = Dr.GetInt32(4),
                        Estado = Dr.GetString(5)
                    }; Lista.Add(Pdd);
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

        public List<Pedido> RangoTotal(DateTime Desde, DateTime Hasta)
        {
            List<Pedido> Lista = new List<Pedido>();
            Pedido Pdd = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM pedidos WHERE (fecha BETWEEN CAST(@Desde AS DATE) AND CAST(@Hasta AS DATE))";
                Comando.Parameters.Add("@Desde", MySqlDbType.Date);
                Comando.Parameters["@Desde"].Value = Desde;
                Comando.Parameters.Add("@Hasta", MySqlDbType.Date);
                Comando.Parameters["@Hasta"].Value = Hasta;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    Pdd = new Pedido()
                    {
                        Pedidoid = Dr.GetInt32(0),
                        Nropedido = Dr.GetInt32(1),
                        Fecha = Dr.GetDateTime(2),
                        Entregado = Dr.GetString(3),
                        Clienteid = Dr.GetInt32(4),
                        Estado = Dr.GetString(5)
                    }; Lista.Add(Pdd);
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

        public List<Pedido> ListaClienteAPellidoNombre(string nombre)
        {
            List<Pedido> Lista = new List<Pedido>();
            Pedido Pdd = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM pedidos AS ped INNER JOIN  clientes AS cli ON (ped.clienteid = cli.clienteid) WHERE(cli.apellido = @nombre or cli.nombre = @nombre) and ped.estado = 'activo' AND ped.entregado = 'en espera'";
                Comando.Parameters.Add("@nombre", MySqlDbType.String);
                Comando.Parameters["@nombre"].Value = nombre;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    Pdd = new Pedido()
                    {
                        Pedidoid = Dr.GetInt32(0),
                        Nropedido = Dr.GetInt32(1),
                        Fecha = Dr.GetDateTime(2),
                        Entregado = Dr.GetString(3),
                        Clienteid = Dr.GetInt32(4),
                        Estado = Dr.GetString(5)
                    }; Lista.Add(Pdd);
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

        public List<Pedido> ListarPedidoFiltroCliente(string TxtFiltro)
        {
            List<Pedido> Lista = new List<Pedido>();
            Pedido Pdd = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT ped.* FROM pedidos AS ped INNER JOIN clientes AS cli ON(ped.clienteid = cli.clienteid) WHERE(cli.apellido LIKE @SearchText or cli.nombre LIKE @SearchText) ";
                Comando.Prepare();
                Comando.Parameters.AddWithValue("@SearchText", "%" + TxtFiltro + "%");

                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    Pdd = new Pedido()
                    {
                        Pedidoid = Dr.GetInt32(0),
                        Nropedido = Dr.GetInt32(1),
                        Fecha = Dr.GetDateTime(2),
                        Entregado = Dr.GetString(3),
                        Clienteid = Dr.GetInt32(4),
                        Estado = Dr.GetString(5)
                    }; Lista.Add(Pdd);
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

        public List<Pedido> ListaClienteAPellidoNombreTotal(string nombre)
        {
            List<Pedido> Lista = new List<Pedido>();
            Pedido Pdd = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM pedidos AS ped INNER JOIN  clientes AS cli ON (ped.clienteid = cli.clienteid) WHERE(cli.apellido = @nombre or cli.nombre = @nombre) ";
                Comando.Parameters.Add("@nombre", MySqlDbType.String);
                Comando.Parameters["@nombre"].Value = nombre;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    Pdd = new Pedido()
                    {
                        Pedidoid = Dr.GetInt32(0),
                        Nropedido = Dr.GetInt32(1),
                        Fecha = Dr.GetDateTime(2),
                        Entregado = Dr.GetString(3),
                        Clienteid = Dr.GetInt32(4),
                        Estado = Dr.GetString(5)
                    }; Lista.Add(Pdd);
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

        public Pedido BuscarNro(int nropedido)
        {
            Pedido Pdd = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM pedidos WHERE nropedido = @nropedido";
                Comando.Parameters.Add("@nropedido", MySqlDbType.Int32);
                Comando.Parameters["@nropedido"].Value = nropedido;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                if (Dr.Read())
                {
                    Pdd = new Pedido()
                    {
                        Pedidoid = Dr.GetInt32(0),
                        Nropedido = Dr.GetInt32(1),
                        Fecha = Dr.GetDateTime(2),
                        Entregado = Dr.GetString(3),
                        Clienteid = Dr.GetInt32(4),
                        Estado = Dr.GetString(5)
                    };
                    Dr.Close();
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally { conex.CerrarConexion(); }
            return Pdd;
        }

        public Pedido BuscarNroActivo(int nropedido)
        {
            Pedido Pdd = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM pedidos WHERE nropedido = @nropedido AND estado = 'activo'";
                Comando.Parameters.Add("@nropedido", MySqlDbType.Int32);
                Comando.Parameters["@nropedido"].Value = nropedido;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                if (Dr.Read())
                {
                    Pdd = new Pedido()
                    {
                        Pedidoid = Dr.GetInt32(0),
                        Nropedido = Dr.GetInt32(1),
                        Fecha = Dr.GetDateTime(2),
                        Entregado = Dr.GetString(3),
                        Clienteid = Dr.GetInt32(4),
                        Estado = Dr.GetString(5)
                    };
                }
                Dr.Close();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally { conex.CerrarConexion(); }
            return Pdd;
        }


        public bool DeshabilitarHabilitarPedido(int id, string estado)
        {
            bool DH = false;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "UPDATE pedidos SET estado=@estado WHERE pedidoid = @AuxId";
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

        public void PedidoEntregado(int id, string entregado,int cod)
        {
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "UPDATE pedidos SET entregado=@entregado, WHERE pedidoid = @idp";
                Comando.Parameters.Add("@idp", MySqlDbType.Int32);
                Comando.Parameters["@idp"].Value = id;
                Comando.Parameters.Add("@entregado", MySqlDbType.VarChar);
                Comando.Parameters["@entregado"].Value = entregado;

                Comando.Prepare();
                Comando.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally { conex.CerrarConexion(); }
        }
    }
}
