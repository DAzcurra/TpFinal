using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conexiones
{
    public class Conexion
    {
        public MySqlConnection conexion;

        public void AbrirConexion()
        {
            try
            {
                conexion = new MySqlConnection("Server=localhost; Database= baseProar; Uid=root");
                conexion.Open();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CerrarConexion()
        {
            conexion.Close();
        }

        public MySqlCommand CrearComando()
        {
            MySqlCommand comando = conexion.CreateCommand();
            return comando;
        }
    }
}
