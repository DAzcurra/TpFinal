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
    public class R_Articulo : IArticulo
    {
        Conexion conex = new Conexion();
        R_ArticuloxProveedor R_ArtP = new R_ArticuloxProveedor();

        public bool Agregar(int articuloid, string nombre, string descripcion, string marca, double precioactual, int cantidad, int stockmin)
        {
            bool Agre = false;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "INSERT INTO articulos(articuloid,nombre,descripcion,marca,precioactual,cantidad,stockmin) VALUES (@articuloid,@nombre,@descripcion,@marca,@precioactual,@cantidad,@stockmin)";
                Comando.Parameters.Add("@articuloid", MySqlDbType.Int32);
                Comando.Parameters["@articuloid"].Value = articuloid;
                Comando.Parameters.Add("@nombre", MySqlDbType.VarChar);
                Comando.Parameters["@nombre"].Value = nombre;
                Comando.Parameters.Add("@descripcion", MySqlDbType.VarChar);
                Comando.Parameters["@descripcion"].Value = descripcion;
                Comando.Parameters.Add("@marca", MySqlDbType.VarChar);
                Comando.Parameters["@marca"].Value = marca;
                Comando.Parameters.Add("@precioactual", MySqlDbType.Double);
                Comando.Parameters["@precioactual"].Value = precioactual;
                Comando.Parameters.Add("@cantidad", MySqlDbType.Int32);
                Comando.Parameters["@cantidad"].Value = cantidad;
                Comando.Parameters.Add("@stockmin", MySqlDbType.Int32);
                Comando.Parameters["@stockmin"].Value = stockmin;
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

        public Articulo BuscarXId(int articuloid)
        {
            Articulo Art = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM articulos WHERE articuloid = @articuloid ORDER BY nombre ASC";
                Comando.Parameters.Add("@articuloid", MySqlDbType.Int32);
                Comando.Parameters["@articuloid"].Value = articuloid;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                if (Dr.Read())
                {
                    Art = new Articulo(){
                        Articuloid = Dr.GetInt32(0),
                        Nombre= Dr.GetString(1),
                        Descripcion = Dr.GetString(2),
                        Marca = Dr.GetString(3),
                        Precioactual = Dr.GetDouble(4),
                        Cantidad = Dr.GetInt32(5),
                        Stockmin = Dr.GetInt32(6),
                        Estado= Dr.GetString(7)
                     };
                }
                Dr.Close();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally { conex.CerrarConexion(); }
            return Art;
        }
        public Articulo BuscarXIdActivo(int articuloid)
        {
            Articulo Art = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM articulos WHERE articuloid = @articuloid AND estado = 'activo' ORDER BY nombre ASC";
                Comando.Parameters.Add("@articuloid", MySqlDbType.Int32);
                Comando.Parameters["@articuloid"].Value = articuloid;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                if (Dr.Read())
                {
                    Art = new Articulo()
                    {
                        Articuloid = Dr.GetInt32(0),
                        Nombre = Dr.GetString(1),
                        Descripcion = Dr.GetString(2),
                        Marca = Dr.GetString(3),
                        Precioactual = Dr.GetDouble(4),
                        Cantidad = Dr.GetInt32(5),
                        Stockmin = Dr.GetInt32(6),
                        Estado = Dr.GetString(7)
                    };
                }
                Dr.Close();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally { conex.CerrarConexion(); }
            return Art;
        }

        public List<Articulo> BuscarXMarca(string marca)
        {
            List<Articulo> Lista = new List<Articulo>();
            Articulo Art = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM articulos WHERE marca = @marca ORDER BY nombre ASC";
                Comando.Parameters.Add("@marca", MySqlDbType.VarChar);
                Comando.Parameters["@marca"].Value = marca;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    Art = new Articulo()
                    {
                        Articuloid = Dr.GetInt32(0),
                        Nombre = Dr.GetString(1),
                        Descripcion = Dr.GetString(2),
                        Marca = Dr.GetString(3),
                        Precioactual = Dr.GetDouble(4),
                        Cantidad = Dr.GetInt32(5),
                        Stockmin = Dr.GetInt32(6),
                        Estado = Dr.GetString(7)
                    };
                    Lista.Add(Art);
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

        public List<Articulo> BuscarXNombre(string nombre)
        {
            List<Articulo> Lista = new List<Articulo>();
            Articulo Art = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM articulos WHERE nombre = @nombre ORDER BY nombre ASC";
                Comando.Parameters.Add("@nombre", MySqlDbType.VarChar);
                Comando.Parameters["@nombre"].Value = nombre;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    Art = new Articulo()
                    {
                        Articuloid = Dr.GetInt32(0),
                        Nombre = Dr.GetString(1),
                        Descripcion = Dr.GetString(2),
                        Marca = Dr.GetString(3),
                        Precioactual = Dr.GetDouble(4),
                        Cantidad = Dr.GetInt32(5),
                        Stockmin = Dr.GetInt32(6),
                        Estado = Dr.GetString(7)
                    }; 
                    Lista.Add(Art);
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

        public List<Articulo> BuscarXNombreMarca(string nombre)
        {
            List<Articulo> Lista = new List<Articulo>();
            Articulo Art = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM articulos WHERE nombre = @nombre or marca = @nombre ORDER BY nombre ASC";
                Comando.Parameters.Add("@nombre", MySqlDbType.VarChar);
                Comando.Parameters["@nombre"].Value = nombre;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    Art = new Articulo()
                    {
                        Articuloid = Dr.GetInt32(0),
                        Nombre = Dr.GetString(1),
                        Descripcion = Dr.GetString(2),
                        Marca = Dr.GetString(3),
                        Precioactual = Dr.GetDouble(4),
                        Cantidad = Dr.GetInt32(5),
                        Stockmin = Dr.GetInt32(6),
                        Estado = Dr.GetString(7)
                    }; 
                    Lista.Add(Art);
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

       
        public List<Articulo> BuscarXNombreMarcaActivo(string nombre)
        {
            List<Articulo> Lista = new List<Articulo>();
            Articulo Art = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM articulos WHERE (nombre = @nombre or marca = @nombre) AND estado = 'activo' ORDER BY nombre ASC";
                Comando.Parameters.Add("@nombre", MySqlDbType.VarChar);
                Comando.Parameters["@nombre"].Value = nombre;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    Art = new Articulo()
                    {
                        Articuloid = Dr.GetInt32(0),
                        Nombre = Dr.GetString(1),
                        Descripcion = Dr.GetString(2),
                        Marca = Dr.GetString(3),
                        Precioactual = Dr.GetDouble(4),
                        Cantidad = Dr.GetInt32(5),
                        Stockmin = Dr.GetInt32(6),
                        Estado = Dr.GetString(7)
                    }; 
                    Lista.Add(Art);
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

        public bool Eliminar(int articuloid)
        {
            bool Elim = false;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "DELETE articulos,articuloxproveedor FROM articulos INNER JOIN articuloxproveedor ON (articulos.articuloid=articuloxproveedor.articuloid) WHERE articulos.articuloid=@articuloid";
                Comando.Parameters.Add("@articuloid", MySqlDbType.Int32);
                Comando.Parameters["@articuloid"].Value = articuloid;
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

        public List<Articulo> Listar()
        {
            List<Articulo> Lista = new List<Articulo>();
            Articulo Art = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM articulos ORDER BY nombre ASC";
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    Art = new Articulo()
                    {
                        Articuloid = Dr.GetInt32(0),
                        Nombre = Dr.GetString(1),
                        Descripcion = Dr.GetString(2),
                        Marca = Dr.GetString(3),
                        Precioactual = Dr.GetDouble(4),
                        Cantidad = Dr.GetInt32(5),
                        Stockmin = Dr.GetInt32(6),
                        Estado = Dr.GetString(7)
                    };
                    Lista.Add(Art);
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

        public List<Articulo> ListarActivos()
        {
            List<Articulo> Lista = new List<Articulo>();
            Articulo Art = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM articulos WHERE estado = 'activo' ORDER BY nombre ASC";
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    Art = new Articulo()
                    {
                        Articuloid = Dr.GetInt32(0),
                        Nombre = Dr.GetString(1),
                        Descripcion = Dr.GetString(2),
                        Marca = Dr.GetString(3),
                        Precioactual = Dr.GetDouble(4),
                        Cantidad = Dr.GetInt32(5),
                        Stockmin = Dr.GetInt32(6),
                        Estado = Dr.GetString(7)
                    }; 
                    Lista.Add(Art);
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

        public List<Articulo> ListarPrueba(string TxtFiltro)
        {
            List<Articulo> Lista = new List<Articulo>();
            Articulo Art = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM articulos WHERE (nombre LIKE @SearchText or marca LIKE @SearchText) and estado = 'activo'";
                Comando.Prepare();
                Comando.Parameters.AddWithValue("@SearchText", "%" + TxtFiltro + "%");

                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    Art = new Articulo()
                    {
                        Articuloid = Dr.GetInt32(0),
                        Nombre = Dr.GetString(1),
                        Descripcion = Dr.GetString(2),
                        Marca = Dr.GetString(3),
                        Precioactual = Dr.GetDouble(4),
                        Cantidad = Dr.GetInt32(5),
                        Stockmin = Dr.GetInt32(6),
                        Estado = Dr.GetString(7)
                    }; Lista.Add(Art);
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
        
        public bool Modificar(int artid, Articulo aux, ArticuloxProveedor ArtxP)
        {
            bool Modif = false;
            Articulo AuxArtUno = null;
            Articulo AuxArt2 = null;
            AuxArtUno = BuscarXId(artid);
            AuxArt2 = BuscarXId(aux.Articuloid);
            if ((AuxArtUno != null && AuxArt2 == null) || (AuxArtUno.Articuloid == AuxArt2.Articuloid))
            {
                try
                {
                    conex.AbrirConexion();
                    MySqlCommand Comando = conex.CrearComando();
                    Comando.CommandText = "UPDATE articulos INNER JOIN articuloxproveedor ON articulos.articuloid = articuloxproveedor.articuloid SET articulos.articuloid=@articuloid,articulos.nombre=@nombre,articulos.descripcion=@descripcion,articulos.marca=@marca,articulos.precioactual=@precioactual,articulos.cantidad=@cantidad,articulos.stockmin=@stockmin,articuloxproveedor.articuloid=@Particuloid,articuloxproveedor.proveedorid= @proveedorid,articuloxproveedor.costo=@costo,articulos.estado=@estado WHERE articulos.articuloid = @AuxId";
                    Comando.Parameters.Add("@AuxId", MySqlDbType.Int32);
                    Comando.Parameters["@AuxId"].Value = artid;
                    Comando.Parameters.Add("@articuloid", MySqlDbType.Int32);
                    Comando.Parameters["@articuloid"].Value = aux.Articuloid;
                    Comando.Parameters.Add("@nombre", MySqlDbType.VarChar);
                    Comando.Parameters["@nombre"].Value = aux.Nombre;
                    Comando.Parameters.Add("@descripcion", MySqlDbType.VarChar);
                    Comando.Parameters["@descripcion"].Value = aux.Descripcion;
                    Comando.Parameters.Add("@marca", MySqlDbType.VarChar);
                    Comando.Parameters["@marca"].Value = aux.Marca;
                    Comando.Parameters.Add("@precioactual", MySqlDbType.Double);
                    Comando.Parameters["@precioactual"].Value = aux.Precioactual;
                    Comando.Parameters.Add("@cantidad", MySqlDbType.Int32);
                    Comando.Parameters["@cantidad"].Value = aux.Cantidad;
                    Comando.Parameters.Add("@stockmin", MySqlDbType.Int32);
                    Comando.Parameters["@stockmin"].Value = aux.Stockmin;
                    Comando.Parameters.Add("@Particuloid", MySqlDbType.Int32);
                    Comando.Parameters["@Particuloid"].Value = aux.Articuloid;
                    Comando.Parameters.Add("@proveedorid", MySqlDbType.Int32);
                    Comando.Parameters["@proveedorid"].Value = ArtxP.Proveedorid;
                    Comando.Parameters.Add("@costo", MySqlDbType.Double);
                    Comando.Parameters["@costo"].Value = ArtxP.Costo;
                    Comando.Parameters.Add("@estado", MySqlDbType.VarChar);
                    Comando.Parameters["@estado"].Value = aux.Estado;
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

        public List<Articulo> DependeDeProveedor(int id)
        {
            List<Articulo> LisAux = new List<Articulo>();
            Articulo Art = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM articulos AS art INNER JOIN detallepedido as dt ON(art.articuloid=dt.articuloid) WHERE art.articuloid=@idp";
                Comando.Parameters.Add("@idp", MySqlDbType.Int32);
                Comando.Parameters["@idp"].Value = id;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    Art = new Articulo()
                    {
                        Articuloid = Dr.GetInt32(0),
                        Nombre = Dr.GetString(1),
                        Descripcion = Dr.GetString(2),
                        Marca = Dr.GetString(3),
                        Precioactual = Dr.GetDouble(4),
                        Cantidad = Dr.GetInt32(5),
                        Stockmin = Dr.GetInt32(6),
                        Estado = Dr.GetString(7)
                    }; 
                    LisAux.Add(Art);
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

        public bool DeshabilitarHabilitarProveedor(int id, string estado)
        {
            bool DH = false;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "UPDATE articulos SET estado=@estado WHERE articuloid = @AuxId";
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

        public int ObtenerCantidad(Articulo Aux)
        {
            int Cant = 0;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT cantidad FROM articulos WHERE articuloid = @ArtId";
                Comando.Parameters.Add("@ArtId", MySqlDbType.Int32);
                Comando.Parameters["@ArtId"].Value = Aux.Articuloid;
                Comando.Prepare();
                Cant = (int) Comando.ExecuteScalar();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally { conex.CerrarConexion(); }
            return Cant;
        }

        public bool RestarStock(int cantidad,int id)
        {
            bool RStock = false;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "UPDATE articulos SET cantidad = cantidad - @cant WHERE articuloid = @AuxId";
                Comando.Parameters.Add("@AuxId", MySqlDbType.Int32);
                Comando.Parameters["@AuxId"].Value = id;
                Comando.Parameters.Add("@cant", MySqlDbType.VarChar);
                Comando.Parameters["@cant"].Value = cantidad;
                Comando.Prepare();
                Comando.ExecuteNonQuery();
                RStock = true;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally { conex.CerrarConexion(); }
            return RStock;
        }

        public bool SumarStock(int cantidad, int id)
        {
            bool RStock = false;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "UPDATE articulos SET cantidad = cantidad + @cant WHERE articuloid = @AuxId";
                Comando.Parameters.Add("@AuxId", MySqlDbType.Int32);
                Comando.Parameters["@AuxId"].Value = id;
                Comando.Parameters.Add("@cant", MySqlDbType.Int32);
                Comando.Parameters["@cant"].Value = cantidad;
                Comando.Prepare();
                Comando.ExecuteNonQuery();
                RStock = true;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally { conex.CerrarConexion(); }
            return RStock;
        }

        public List<Articulo> ListarStockBajo()
        {
            List<Articulo> Lista = new List<Articulo>();
            Articulo Art = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM articulos WHERE estado = 'activo' AND cantidad <= stockmin ORDER BY nombre ASC";
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    Art = new Articulo()
                    {
                        Articuloid = Dr.GetInt32(0),
                        Nombre = Dr.GetString(1),
                        Descripcion = Dr.GetString(2),
                        Marca = Dr.GetString(3),
                        Precioactual = Dr.GetDouble(4),
                        Cantidad = Dr.GetInt32(5),
                        Stockmin = Dr.GetInt32(6),
                        Estado = Dr.GetString(7)
                    };
                    Lista.Add(Art);
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
