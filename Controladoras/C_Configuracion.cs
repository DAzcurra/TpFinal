using Entidades;
using Repositorios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Controladoras
{
    public class C_Configuracion
    {
        R_Configuracion R_Config = new R_Configuracion();
        public int DevolverNroMaxPaginacion()
        {
            int Max = 0;
            try
            {
                Max = R_Config.NroMaxPaginar();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Max;
        }

        public int DevolverUltimoPedido()
        {
            int Max = 0;
            try
            {
                Max = R_Config.DevolverUltimoPedido();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Max;
        }

        public int DevolverUltimaFacturaA()
        {
            int Max = 0;
            try
            {
                Max = R_Config.DevolverUltimaFacturaA();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Max;
        }

        public int DevolverUltimaFacturaB()
        {
            int Max = 0;
            try
            {
                Max = R_Config.DevolverUltimaFacturaB();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Max;
        }

        public void ProximoPedido(int num)
        {
            try
            {
              R_Config.ProximoPedido(num);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public void ProximoFacturaA(int num)
        {
            try
            {
                R_Config.ProximaFacturaA(num);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        public void ProximaFacturaB(int num)
        {
            try
            {
                R_Config.ProximaFacturaB(num);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        public bool BuscarUsuario(string user, string password)
        {
            bool Buscar = false;
            Configuracion Aux = null;
            try
            {
                Aux  = R_Config.BuscarUsuario();
                if(Aux.Usuario == user && R_Config.Decrypt(Aux.Contraseña) == password)
                {
                   Buscar = true;
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

            return Buscar;
        }

        public bool ModificarUsuario(string user, string password)
        {
            bool Modif = false;
            try
            {
                Modif = R_Config.ModificarUsuario(user,password);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

            return Modif;
        }

        public string Decrypt(string cipherText)
        {
            string Text;
            try
            {
                Text = R_Config.Decrypt(cipherText);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Text;
        }
     }
}
