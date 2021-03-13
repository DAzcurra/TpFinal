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
    public class R_Facturacion:IFacturacion
    {
        Conexion conex = new Conexion();
        MySqlTransaction Transaccion;
        public bool Agregar(Facturacion_Venta Nuevo, string entregado,int idpedido)
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
                SelectComand.CommandText = "INSERT INTO facturaciones(cod_factura, fecha, importetotal, clienteid, empleadoid, metodopago, pedidoid,tipofactura) VALUES (@cod_factura,@fecha,@importetotal,@clienteid,@empleadoid,@metodopago,@pedidoid,@tipofactura)";
                SelectComand.Parameters.Add("@cod_factura", MySqlDbType.Int32);
                SelectComand.Parameters["@cod_factura"].Value = Nuevo.Cod_factura;
                SelectComand.Parameters.Add("@fecha", MySqlDbType.Date);
                SelectComand.Parameters["@fecha"].Value = Nuevo.Fecha;
                SelectComand.Parameters.Add("@importetotal", MySqlDbType.Double);
                SelectComand.Parameters["@importetotal"].Value = Nuevo.Importetotal;
                SelectComand.Parameters.Add("@clienteid", MySqlDbType.Int32);
                SelectComand.Parameters["@clienteid"].Value = Nuevo.Clienteid;
                SelectComand.Parameters.Add("@empleadoid", MySqlDbType.Int32);
                SelectComand.Parameters["@empleadoid"].Value = Nuevo.Empleadoid;
                SelectComand.Parameters.Add("@pedidoid", MySqlDbType.Int32);
                SelectComand.Parameters["@pedidoid"].Value = Nuevo.Pedidoid;
                SelectComand.Parameters.Add("@metodopago", MySqlDbType.String);
                SelectComand.Parameters["@metodopago"].Value = Nuevo.Metododepago;
                SelectComand.Parameters.Add("@tipofactura", MySqlDbType.VarChar);
                SelectComand.Parameters["@tipofactura"].Value = Nuevo.Tipodefactura;
                SelectComand.Prepare();
                SelectComand.Transaction = Transaccion;
                SelectComand.ExecuteNonQuery();


                    Comando.Parameters.Clear();
                    Comando.CommandText = "UPDATE pedidos SET entregado=@entregado WHERE pedidoid = @idp";
                    Comando.Parameters.Add("@idp", MySqlDbType.Int32);
                    Comando.Parameters["@idp"].Value = idpedido ;
                    Comando.Parameters.Add("@entregado", MySqlDbType.VarChar);
                    Comando.Parameters["@entregado"].Value = entregado;
                    Comando.Prepare();
                    Comando.ExecuteNonQuery();

                Transaccion.Commit();

                commited = true;
                Agre = true;
            }
            catch (Exception Ex)
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

        public List<Facturacion_Venta> DevolverTodo()
        {
            List<Facturacion_Venta> Lista = new List<Facturacion_Venta>();
            Facturacion_Venta Fac = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM facturaciones";
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    Fac = new Facturacion_Venta()
                    {
                        Cod_factura = Dr.GetInt32(0),
                        Fecha = Dr.GetDateTime(1),
                        Importetotal = Dr.GetDouble(2),
                        Clienteid = Dr.GetInt32(3),
                        Empleadoid = Dr.GetInt32(4),
                        Metododepago = Dr.GetString(5),
                        Pedidoid = Dr.GetInt32(6),
                        Tipodefactura = Dr.GetChar(7)
                    };
                Lista.Add(Fac);
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

        public Facturacion_Venta Buscar(int cod ,char tipofactura)
        {
            Facturacion_Venta Fac = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM facturaciones WHERE cod_factura = @cod_factura AND tipofactura = @tipofactura";
                Comando.Parameters.Add("@cod_factura", MySqlDbType.Int32);
                Comando.Parameters["@cod_factura"].Value = cod;
                Comando.Parameters.Add("@tipofactura", MySqlDbType.VarChar);
                Comando.Parameters["@tipofactura"].Value = tipofactura;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                if (Dr.Read())
                {
                    Fac = new Facturacion_Venta()
                    {
                        Cod_factura = Dr.GetInt32(0),
                        Fecha = Dr.GetDateTime(1),
                        Importetotal = Dr.GetDouble(2),
                        Clienteid = Dr.GetInt32(3),
                        Empleadoid = Dr.GetInt32(4),
                        Metododepago = Dr.GetString(5),
                        Pedidoid = Dr.GetInt32(6),
                        Tipodefactura = Dr.GetChar(7)
                    };
                }
                Dr.Close();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally { conex.CerrarConexion(); }
            return Fac;
        }

        public List<Facturacion_Venta> ListarFacturaFiltroCliente(string TxtFiltro)
        {
            List<Facturacion_Venta> Lista = new List<Facturacion_Venta>();
            Facturacion_Venta Fact = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT fac.* FROM facturaciones AS fac INNER JOIN clientes AS cli ON(fac.clienteid = cli.clienteid) WHERE(cli.apellido LIKE @SearchText or cli.nombre LIKE @SearchText) ";
                Comando.Prepare();
                Comando.Parameters.AddWithValue("@SearchText", "%" + TxtFiltro + "%");

                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    Fact = new Facturacion_Venta()
                    {
                        Cod_factura = Dr.GetInt32(0),
                        Fecha = Dr.GetDateTime(1),
                        Importetotal = Dr.GetDouble(2),
                        Clienteid = Dr.GetInt32(3),
                        Empleadoid = Dr.GetInt32(4),
                        Metododepago = Dr.GetString(5),
                        Pedidoid = Dr.GetInt32(6),
                        Tipodefactura = Dr.GetChar(7)
                    }; Lista.Add(Fact);
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

        public List<Facturacion_Venta> RangoTotal(DateTime Desde, DateTime Hasta)
        {
            List<Facturacion_Venta> Lista = new List<Facturacion_Venta>();
            Facturacion_Venta Fact = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM facturaciones WHERE (fecha BETWEEN CAST(@Desde AS DATE) AND CAST(@Hasta AS DATE))";
                Comando.Parameters.Add("@Desde", MySqlDbType.Date);
                Comando.Parameters["@Desde"].Value = Desde;
                Comando.Parameters.Add("@Hasta", MySqlDbType.Date);
                Comando.Parameters["@Hasta"].Value = Hasta;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    Fact = new Facturacion_Venta()
                    {
                        Cod_factura = Dr.GetInt32(0),
                        Fecha = Dr.GetDateTime(1),
                        Importetotal = Dr.GetDouble(2),
                        Clienteid = Dr.GetInt32(3),
                        Empleadoid = Dr.GetInt32(4),
                        Metododepago = Dr.GetString(5),
                        Pedidoid = Dr.GetInt32(6),
                        Tipodefactura = Dr.GetChar(7)
                    }; Lista.Add(Fact);
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
    }
}
