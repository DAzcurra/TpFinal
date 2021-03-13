using Conexiones;
using Entidades;
using EntidadesDTO;
using Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorios
{
    public class R_DetallePedido : IDetallePedido
    {
        Conexion conex = new Conexion();

        public bool Agregar(DetallePedido Nuevo)
        {
            bool Agre = false;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "INSERT INTO detallepedido(articuloid,pedidoid,cantidad,preciovendido) VALUES (@articuloid,@pedidoid,@cantidad,@preciovendido)";
                Comando.Parameters.Add("@articuloid", MySqlDbType.Int32);
                Comando.Parameters["@articuloid"].Value = Nuevo.Articuloid;
                Comando.Parameters.Add("@pedidoid", MySqlDbType.Int32);
                Comando.Parameters["@pedidoid"].Value = Nuevo.Pedidoid;
                Comando.Parameters.Add("@cantidad", MySqlDbType.Int32);
                Comando.Parameters["@cantidad"].Value = Nuevo.Cantidad;
                Comando.Parameters.Add("@preciovendido", MySqlDbType.Double);
                Comando.Parameters["@preciovendido"].Value = Nuevo.Preciovendido;
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

        public bool Eliminar(int pedidoid, int articuloid)
        {
            bool Exit = false;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "DELETE FROM detallepedido WHERE pedidoid = @pedidoid AND articuloid = @articuloid";
                Comando.Parameters.Add("@pedidoid", MySqlDbType.Int32);
                Comando.Parameters["@pedidoid"].Value = pedidoid;
                Comando.Parameters.Add("@articuloid", MySqlDbType.Int32);
                Comando.Parameters["@articuloid"].Value = articuloid;
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

        public List<DetallePedido> DevolverTodoxID(int id)
        {
            List<DetallePedido> Lista = new List<DetallePedido>();
            DetallePedido DPdd = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM detallepedido WHERE pedidoid = @idPedido";
                Comando.Parameters.Add("@idPedido", MySqlDbType.Int32);
                Comando.Parameters["@idPedido"].Value = id;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    DPdd = new DetallePedido()
                    {
                        Articuloid = Dr.GetInt32(0),
                        Pedidoid = Dr.GetInt32(1),
                        Cantidad = Dr.GetInt32(2),
                        Preciovendido = Dr.GetDouble(3)
                    };
                    Lista.Add(DPdd);
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

        public DetallePedido DevolverDetalle(int pedidoid,int articuloid)
        {
            DetallePedido DPdd = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM detallepedido WHERE pedidoid = @idPedido AND articuloid = @idArticulo";
                Comando.Parameters.Add("@idPedido", MySqlDbType.Int32);
                Comando.Parameters["@idPedido"].Value = pedidoid;
                Comando.Parameters.Add("@idArticulo", MySqlDbType.Int32);
                Comando.Parameters["@idArticulo"].Value = articuloid;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                if (Dr.Read())
                {
                    DPdd = new DetallePedido()
                    {
                        Articuloid = Dr.GetInt32(0),
                        Pedidoid = Dr.GetInt32(1),
                        Cantidad = Dr.GetInt32(2),
                        Preciovendido = Dr.GetDouble(3)
                    };
                }
                Dr.Close();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally { conex.CerrarConexion(); }
            return DPdd;
        }

        public bool ModificarCant(DetallePedido det,int Cantidad)
        {
            bool Modif = false;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "UPDATE detallepedido SET cantidad = @Cantidad WHERE articuloid = @idArticulo AND pedidoid = @idPedido ";
                Comando.Parameters.Add("@Cantidad", MySqlDbType.Int32);
                Comando.Parameters["@Cantidad"].Value = Cantidad;
                Comando.Parameters.Add("@idPedido", MySqlDbType.Int32);
                Comando.Parameters["@idPedido"].Value = det.Pedidoid;
                Comando.Parameters.Add("@idArticulo", MySqlDbType.Int32);
                Comando.Parameters["@idArticulo"].Value = det.Articuloid;
                Comando.Prepare();
                Comando.ExecuteNonQuery();
                Modif = true;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally { conex.CerrarConexion(); }
            return Modif;
        }

        public List<DetallePedido> DetallePedidoFactura(int cod,char tipo)
        {
            List<DetallePedido> Lista = new List<DetallePedido>();
            DetallePedido DPdd = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT dep.* FROM facturaciones AS fac INNER JOIN pedidos AS ped ON (fac.pedidoid = ped.pedidoid) INNER JOIN detallepedido AS dep ON (ped.pedidoid = dep.pedidoid) WHERE fac.cod_factura = @cod AND fac.tipofactura = @tipo";
                Comando.Parameters.Add("@cod", MySqlDbType.Int32);
                Comando.Parameters["@cod"].Value = cod;
                Comando.Parameters.Add("@tipo", MySqlDbType.VarChar);
                Comando.Parameters["@tipo"].Value = tipo;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    DPdd = new DetallePedido()
                    {
                        Articuloid = Dr.GetInt32(0),
                        Pedidoid = Dr.GetInt32(1),
                        Cantidad = Dr.GetInt32(2),
                        Preciovendido = Dr.GetDouble(3)
                    }; Lista.Add(DPdd);
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

        public bool PerteneceAPedido(int idpedido,int idarticuloBase)
        {
            bool auxbool = false;
            DetallePedido DPdd = null;

            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT* FROM detallepedido AS dp INNER JOIN pedidos AS p ON(dp.pedidoid = p.pedidoid) WHERE dp.articuloid =@IdArticuloBase AND dp.pedidoid =@IdPedido";
                Comando.Parameters.Add("@IdPedido", MySqlDbType.Int32);
                Comando.Parameters["@IdPedido"].Value = idpedido;
                Comando.Parameters.Add("@IdArticuloBase", MySqlDbType.Int32);
                Comando.Parameters["@IdArticuloBase"].Value = idarticuloBase;

                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                if (Dr.Read())
                {
                    DPdd = new DetallePedido()
                    {
                        Articuloid = Dr.GetInt32(0),
                        Pedidoid = Dr.GetInt32(1),
                        Cantidad = Dr.GetInt32(2),
                        Preciovendido = Dr.GetDouble(3)
                    }; auxbool = true;
                }
                Dr.Close();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally { conex.CerrarConexion(); }
            return auxbool;
        }
    }
}
