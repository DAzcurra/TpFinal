using Conexiones;
using Entidades;
using Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Repositorios
{
    public class R_Configuracion: IConfiguracion
    {
        Conexion conex = new Conexion();

        public int DevolverUltimoPedido()
        {
            int Max = 0;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT MAX(proximopedido) FROM configuracion";
                Comando.Prepare();
                Max = (Int32)Comando.ExecuteScalar();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally { conex.CerrarConexion(); }
            return Max;
        }

        public int DevolverUltimaFacturaA()
        {
            int Max = 0;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT MAX(proximafacturaA) FROM configuracion";
                Comando.Prepare();
                Max = (Int32)Comando.ExecuteScalar();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally { conex.CerrarConexion(); }
            return Max;
        }


        public int DevolverUltimaFacturaB()
        {
            int Max = 0;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT MAX(proximafacturaB) FROM configuracion";
                Comando.Prepare();
                Max = (Int32)Comando.ExecuteScalar();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally { conex.CerrarConexion(); }
            return Max;
        }

        public int NroMaxPaginar()
        {
            int Max = 0;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT NroMaxPaginas FROM configuracion";
                Comando.Prepare();
                Max = (Int32)Comando.ExecuteScalar();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
               }
            finally { conex.CerrarConexion(); }
            return Max;
        }

        public void ProximoPedido(int num)
        {
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "INSERT INTO configuracion(proximopedido) VALUES (@NroPedido)";
                Comando.Parameters.Add("@NroPedido", MySqlDbType.Int32);
                Comando.Parameters["@NroPedido"].Value = num;
                Comando.Prepare();
                Comando.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally { conex.CerrarConexion(); }
        }

        public void ProximaFacturaA(int num)
        {
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "INSERT INTO configuracion(proximafacturaA) VALUES (@proximafacturaA)";
                Comando.Parameters.Add("@proximafacturaA", MySqlDbType.Int32);
                Comando.Parameters["@proximafacturaA"].Value = num;
                Comando.Prepare();
                Comando.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally { conex.CerrarConexion(); }
        }

        public void ProximaFacturaB(int num)
        {
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "INSERT INTO configuracion(proximafacturaB) VALUES (@proximafacturaB)";
                Comando.Parameters.Add("@proximafacturaB", MySqlDbType.Int32);
                Comando.Parameters["@proximafacturaB"].Value = num;
                Comando.Prepare();
                Comando.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally { conex.CerrarConexion(); }
        }

        public Configuracion BuscarUsuario()
        {
            Configuracion Aux = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM configuracion";
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                if (Dr.Read())
                {
                    Aux = new Configuracion()
                    {
                        NroMaxPaginas = Dr.GetInt32(0),
                        ProximafacturaA = Dr.GetInt32(1),
                        ProximafacturaB = Dr.GetInt32(2),
                        Proximopedido = Dr.GetInt32(3),
                        Usuario = Dr.GetString(4),
                        Contraseña = Dr.GetString(5)
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

        public bool ModificarUsuario(string user, string password)
        {
            bool Modif = false;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "UPDATE configuracion SET usuario=@usuario,contraseña=@contraseña";
                Comando.Parameters.AddWithValue("@usuario", user.Trim());
                Comando.Parameters.AddWithValue("@contraseña", Encrypt(password.Trim()));
                Comando.Prepare();
                Comando.ExecuteNonQuery();
                Modif= true;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally { conex.CerrarConexion(); }
            return Modif;
        }

        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

    }
}
