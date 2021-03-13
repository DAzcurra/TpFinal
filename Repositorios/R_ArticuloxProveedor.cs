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
    public class R_ArticuloxProveedor : IArticuloxProveedor
    {
        Conexion conex = new Conexion();

        public bool Agregar(ArticuloxProveedor Nuevo)
        {
            bool Agre = false;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "INSERT INTO articuloxproveedor(articuloid,proveedorid,costo) VALUES (@articuloid,@proveedorid,@costo)";
                Comando.Parameters.Add("@articuloid", MySqlDbType.Int32);
                Comando.Parameters["@articuloid"].Value = Nuevo.Articuloid;
                Comando.Parameters.Add("@proveedorid", MySqlDbType.Int32);
                Comando.Parameters["@proveedorid"].Value = Nuevo.Proveedorid;
                Comando.Parameters.Add("@costo", MySqlDbType.Double);
                Comando.Parameters["@costo"].Value = Nuevo.Costo;
                Comando.Prepare();
                Comando.ExecuteNonQuery();
                Agre = true;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally
            {
                conex.CerrarConexion();
            }
            return Agre;
        }



        public ArticuloxProveedor Buscar(int cod)
        {
            ArticuloxProveedor Aux = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM articuloxproveedor WHERE articuloid = @articuloid";
                Comando.Parameters.Add("@articuloid", MySqlDbType.Int32);
                Comando.Parameters["@articuloid"].Value = cod;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                if (Dr.Read())
                {
                    Aux = new ArticuloxProveedor() {
                     Articuloid = Dr.GetInt32(0),
                     Proveedorid = Dr.GetInt32(1),
                    Costo = Dr.GetDouble(2)
                    
                    };
                }
                Dr.Close();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally
            {
                conex.CerrarConexion();
            }
            return Aux;
        }

        public List<ArticuloxProveedor> DevolverTodo()
        {
            List<ArticuloxProveedor> Lista = new List<ArticuloxProveedor>();
            ArticuloxProveedor Art = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM articuloxproveedor";
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    Art = new ArticuloxProveedor()
                    {
                        Articuloid = Dr.GetInt32(0),
                        Proveedorid = Dr.GetInt32(1),
                        Costo = Dr.GetDouble(2)

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

        public bool Eliminar(ArticuloxProveedor ArtxP)
        {
            bool Elim = false;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "DELETE FROM articuloxproveedor WHERE articuloid=@articuloid";
                Comando.Parameters.Add("@articuloid", MySqlDbType.Int32);
                Comando.Parameters["@articuloid"].Value = ArtxP.Articuloid;
                Comando.Prepare();
                Comando.ExecuteNonQuery();
                Elim = true;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally
            {
                conex.CerrarConexion();
            }
            return Elim;
        }

        public bool Modificar(int cod, ArticuloxProveedor ArtxP)
        {
            bool Modif = false;

            ArticuloxProveedor ArtAux;
            ArtAux = Buscar(cod);
            if (ArtAux != null)
            {
                try
                {
                    conex.AbrirConexion();
                    MySqlCommand Comando = conex.CrearComando();
                    Comando.CommandText = "UPDATE articuloxproveedor SET articuloid=@articuloid,proveedorid=@proveedorid,costo=@costo WHERE articuloid = @AuxId";
                    Comando.Parameters.Add("@AuxId", MySqlDbType.Int32);
                    Comando.Parameters["@AuxId"].Value = cod;
                    Comando.Parameters.Add("@articuloid", MySqlDbType.Int32);
                    Comando.Parameters["@articuloid"].Value = ArtxP.Articuloid;
                    Comando.Parameters.Add("@proveedorid", MySqlDbType.Int32);
                    Comando.Parameters["@proveedorid"].Value = ArtxP.Proveedorid;
                    Comando.Parameters.Add("@costo", MySqlDbType.Double);
                    Comando.Parameters["@costo"].Value = ArtxP.Costo;
                    Comando.Prepare();
                    Comando.ExecuteNonQuery();
                    Modif = true;

                }
                catch (Exception Ex)
                {
                    throw new Exception(Ex.Message);
                }
                finally
                {
                    conex.CerrarConexion();
                }
            }
            return Modif;
        }
        public ArticuloxProveedor ListaEspecial(int id)
        {
            ArticuloxProveedor Aux = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM articuloxproveedor artp INNER JOIN articulos art ON (artp.articuloid=art.articuloid)WHERE artp.articuloid = @artid";
                Comando.Parameters.Add("@artid", MySqlDbType.Int32);
                Comando.Parameters["@artid"].Value = id;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                if (Dr.Read())
                {
                    Aux = new ArticuloxProveedor()
                    {
                        Articuloid = Dr.GetInt32(0),
                        Proveedorid = Dr.GetInt32(1),
                        Costo = Dr.GetDouble(2)
                    };
                }
                Dr.Close();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally
            {
                conex.CerrarConexion();
            }
            return Aux;
        }
     }
}
