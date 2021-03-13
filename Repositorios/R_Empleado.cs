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
    public class R_Empleado : IEmpleado
    {
        Conexion conex = new Conexion();
        public bool Agregar(int empleadoid, string nombre, string apellido, string telefono, string email, string direccion, string cuil)
        {
            bool Exit = false;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "INSERT INTO empleados(empleadoid,nombre,apellido,telefono,email,direccion,cuil) VALUES (DEFAULT,@nombre,@apellido,@telefono,@email,@direccion,@cuil)";
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

                Comando.Prepare();
                Comando.ExecuteNonQuery();
                Exit = true;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally { conex.CerrarConexion(); }
            return Exit;
        }

        public bool DeshabilitarHabilitarEmpleados(int id, string estado)
        {
            bool DH = false;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "UPDATE empleados SET estado=@estado WHERE empleadoid = @AuxId";
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

        public Empleado Buscar(string cuil)
        {
            Empleado Aux = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM empleados WHERE cuil = @cuil";
                Comando.Parameters.Add("@cuil", MySqlDbType.VarChar);
                Comando.Parameters["@cuil"].Value = cuil;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                if (Dr.Read())
                {
                    Aux = new Empleado()
                    {
                        Empleadoid = Dr.GetInt32(0),
                        Nombre = Dr.GetString(1),
                        Apellido = Dr.GetString(2),
                        Telefono = Dr.GetString(3),
                        Email = Dr.GetString(4),
                        Direccion = Dr.GetString(5),
                        Cuil = Dr.GetString(6),
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
            return Aux;
        }

        public Empleado BuscarActivos(string cuil)
        {
            Empleado Aux = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM empleados WHERE cuil = @cuil AND estado = 'activo'";
                Comando.Parameters.Add("@cuil", MySqlDbType.VarChar);
                Comando.Parameters["@cuil"].Value = cuil;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                if (Dr.Read())
                {
                    Aux = new Empleado()
                    {
                        Empleadoid = Dr.GetInt32(0),
                        Nombre = Dr.GetString(1),
                        Apellido = Dr.GetString(2),
                        Telefono = Dr.GetString(3),
                        Email = Dr.GetString(4),
                        Direccion = Dr.GetString(5),
                        Cuil = Dr.GetString(6),
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
            return Aux;
        }

        public Empleado BuscarID(int id)
        {
            Empleado Aux = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM empleados WHERE empleadoid = @empleadoid";
                Comando.Parameters.Add("@empleadoid", MySqlDbType.VarChar);
                Comando.Parameters["@empleadoid"].Value = id;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                if (Dr.Read())
                {
                    Aux = new Empleado()
                    {
                        Empleadoid = Dr.GetInt32(0),
                        Nombre = Dr.GetString(1),
                        Apellido = Dr.GetString(2),
                        Telefono = Dr.GetString(3),
                        Email = Dr.GetString(4),
                        Direccion = Dr.GetString(5),
                        Cuil = Dr.GetString(6),
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
            return Aux;
        }

        public List<Empleado> BuscarEmp(string apellido)
        {
            List<Empleado> Aux = new List<Empleado>();
            Empleado Emp = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM empleados WHERE apellido = @apellido";
                Comando.Parameters.Add("@apellido", MySqlDbType.VarChar);
                Comando.Parameters["@apellido"].Value = apellido;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    Emp = new Empleado()
                    {
                        Empleadoid = Dr.GetInt32(0),
                        Nombre = Dr.GetString(1),
                        Apellido = Dr.GetString(2),
                        Telefono = Dr.GetString(3),
                        Email = Dr.GetString(4),
                        Direccion = Dr.GetString(5),
                        Cuil = Dr.GetString(6),
                        Estado = Dr.GetString(7)
                    };
                    Aux.Add(Emp);
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

        public List<Empleado> BuscarEmpActivo(string apellido)
        {
            List<Empleado> Aux = new List<Empleado>();
            Empleado Emp = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM empleados WHERE apellido = @apellido AND estado = 'activo'";
                Comando.Parameters.Add("@apellido", MySqlDbType.VarChar);
                Comando.Parameters["@apellido"].Value = apellido;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    Emp = new Empleado()
                    {
                        Empleadoid = Dr.GetInt32(0),
                        Nombre = Dr.GetString(1),
                        Apellido = Dr.GetString(2),
                        Telefono = Dr.GetString(3),
                        Email = Dr.GetString(4),
                        Direccion = Dr.GetString(5),
                        Cuil = Dr.GetString(6),
                        Estado = Dr.GetString(7)
                    };
                    Aux.Add(Emp);
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
            bool Exit = false;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "DELETE FROM empleados WHERE cuil=@cuil";
                Comando.Parameters.Add("@cuil", MySqlDbType.VarChar);
                Comando.Parameters["@cuil"].Value = cuil;
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

        public List<Empleado> Listar()
        {
            List<Empleado> ListaEmpleados = new List<Empleado>();
            Empleado Aux = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM empleados ORDER BY apellido ASC";
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    Aux = new Empleado()
                    {
                        Empleadoid = Dr.GetInt32(0),
                        Nombre = Dr.GetString(1),
                        Apellido = Dr.GetString(2),
                        Telefono = Dr.GetString(3),
                        Email = Dr.GetString(4),
                        Direccion = Dr.GetString(5),
                        Cuil = Dr.GetString(6),
                        Estado = Dr.GetString(7)
                    };
                    ListaEmpleados.Add(Aux);
                }
                Dr.Close();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally
            { conex.CerrarConexion(); }
            return ListaEmpleados;

        }

        public List<Empleado> ListarActivos()
        {
            List<Empleado> ListaEmpleados = new List<Empleado>();
            Empleado Aux = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM empleados WHERE estado = 'activo' ORDER BY apellido ASC";
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    Aux = new Empleado()
                    {
                        Empleadoid = Dr.GetInt32(0),
                        Nombre = Dr.GetString(1),
                        Apellido = Dr.GetString(2),
                        Telefono = Dr.GetString(3),
                        Email = Dr.GetString(4),
                        Direccion = Dr.GetString(5),
                        Cuil = Dr.GetString(6),
                        Estado = Dr.GetString(7)
                    };
                    ListaEmpleados.Add(Aux);
                }
                Dr.Close();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally
            { conex.CerrarConexion(); }
            return ListaEmpleados;

        }

        public bool Modificar(string cuil, Empleado Aux)
        {
            Empleado EAux;
            EAux = Buscar(cuil);
            Empleado EAux2;
            EAux2 = Buscar(Aux.Cuil);
            bool Exit = false;

            if ((EAux != null && EAux2 == null) || (EAux.Cuil == EAux2.Cuil))
            {
                try
                {
                    conex.AbrirConexion();
                    MySqlCommand Comando = conex.CrearComando();
                    Comando.CommandText = "UPDATE empleados SET nombre=@nombre,apellido=@apellido,telefono=@telefono,email=@email,direccion=@direccion,cuil=@cuil, estado=@estado WHERE cuil = @AuxCuil";
                    Comando.Parameters.Add("@AuxCuil", MySqlDbType.VarChar);
                    Comando.Parameters["@AuxCuil"].Value = cuil;
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
                    Comando.Parameters.Add("@estado", MySqlDbType.VarChar);
                    Comando.Parameters["@estado"].Value = Aux.Estado;
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

            }
            return Exit;
        }

        public List<Empleado> DependeDeFactura(int id)
        {
            List<Empleado> LisAux = new List<Empleado>();
            Empleado EmpAux = null;
            try
            {
                conex.AbrirConexion();
                MySqlCommand Comando = conex.CrearComando();
                Comando.CommandText = "SELECT * FROM empleados AS emp INNER JOIN facturaciones as fac ON(emp.empleadoid=fac.empleadoid) WHERE emp.empleadoid=@idp";
                Comando.Parameters.Add("@idp", MySqlDbType.Int32);
                Comando.Parameters["@idp"].Value = id;
                Comando.Prepare();
                MySqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    EmpAux = new Empleado()
                    {
                        Empleadoid = Dr.GetInt32(0),
                        Nombre = Dr.GetString(1),
                        Apellido = Dr.GetString(2),
                        Telefono = Dr.GetString(3),
                        Email = Dr.GetString(4),
                        Direccion = Dr.GetString(5),
                        Cuil = Dr.GetString(6),
                        Estado = Dr.GetString(7)
                    };
                    LisAux.Add(EmpAux);
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
