using Entidades;
using EntidadesDTO;
using Interfaces;
using Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controladoras
{
    public class C_Articulo
    {
        Articulo AuxArt = null;
        List<Articulo> Lista = new List<Articulo>();
        R_Articulo R_Art = new R_Articulo();
        R_ArticuloxProveedor R_ArtxP = new R_ArticuloxProveedor();
        R_Proveedor R_Prov = new R_Proveedor();
        public bool Agregar(int articuloid, string nombre, string descripcion, string marca, double precioactual, int cantidad, int stockmin)
        {
            bool Agre = false;
            try
            {
                Agre = R_Art.Agregar(articuloid, nombre, descripcion, marca, precioactual, cantidad, stockmin);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Agre;
        }

        public Articulo BuscarXId(int articuloid)
        {
            try
            {
                AuxArt = R_Art.BuscarXId(articuloid);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return AuxArt;
        }

        public Articulo BuscarXIdActivo(int articuloid)
        {
            try
            {
                AuxArt = R_Art.BuscarXIdActivo(articuloid);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return AuxArt;
        }

        public List<ArticuloxProveedorDTO> BuscarXMarca(string marca)
        {
            List<ArticuloxProveedorDTO> ListaAuxDTO = new List<ArticuloxProveedorDTO>();

            try
            {
                Lista = R_Art.BuscarXMarca(marca);
                foreach (Articulo Aux in Lista)
                {
                    ArticuloxProveedor ArtpAux = R_ArtxP.Buscar(Aux.Articuloid);
                    Articulo ArtAux = R_Art.BuscarXId(ArtpAux.Articuloid);
                    Proveedor ProvAux = R_Prov.BuscarxId(ArtpAux.Proveedorid);
                    ArticuloxProveedorDTO DTOAux = new ArticuloxProveedorDTO(ArtAux, ProvAux, ArtpAux.Costo);
                    ListaAuxDTO.Add(DTOAux);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return ListaAuxDTO;
        }

        public List<ArticuloxProveedorDTO> BuscarXNombre(string nombre)
        {
            List<ArticuloxProveedorDTO> ListaAuxDTO = new List<ArticuloxProveedorDTO>();
            try
            {
                Lista = R_Art.BuscarXNombre(nombre);
                foreach (Articulo Aux in Lista)
                {
                    ArticuloxProveedor ArtpAux = R_ArtxP.Buscar(Aux.Articuloid);
                    Articulo ArtAux = R_Art.BuscarXId(ArtpAux.Articuloid);
                    Proveedor ProvAux = R_Prov.BuscarxId(ArtpAux.Proveedorid);
                    ArticuloxProveedorDTO DTOAux = new ArticuloxProveedorDTO(ArtAux, ProvAux, ArtpAux.Costo);
                    ListaAuxDTO.Add(DTOAux);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return ListaAuxDTO;
        }

        public List<Articulo> BuscarXNombreMarca(string nombre)
        {
            List<Articulo> ListaAux = new List<Articulo>();
            try
            {
                ListaAux = R_Art.BuscarXNombreMarca(nombre);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return ListaAux;
        }



        public List<Articulo> BuscarXNombreMarcaActivo(string nombre)
        {
            List<Articulo> ListaAux = new List<Articulo>();
            try
            {
                ListaAux = R_Art.BuscarXNombreMarcaActivo(nombre);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return ListaAux;
        }

        public List<Articulo> BuscarArtXNombre(string nombre)
        {
            List<Articulo> ListaAux = new List<Articulo>();
            try
            {
                ListaAux = R_Art.BuscarXNombre(nombre);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return ListaAux;
        }

        public List<Articulo> BuscarArtXMarca(string marca)
        {
            List<Articulo> ListaAux = new List<Articulo>();
            try
            {
                ListaAux = R_Art.BuscarXMarca(marca);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return ListaAux;
        }

        public bool Eliminar(int articuloid)
        {
            bool Elim = false;
            try
            {
                Elim = R_Art.Eliminar(articuloid);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Elim;
        }

        public List<ArticuloxProveedorDTO> Listar()
        {
            List<ArticuloxProveedorDTO> ListaAuxDTO = new List<ArticuloxProveedorDTO>();
            try
            {
                Lista = R_Art.Listar();
                foreach (Articulo Aux in Lista)
                {
                    ArticuloxProveedor ArtpAux = R_ArtxP.Buscar(Aux.Articuloid);
                    Articulo ArtAux = R_Art.BuscarXId(ArtpAux.Articuloid);
                    Proveedor ProvAux = R_Prov.BuscarxId(ArtpAux.Proveedorid);
                    ArticuloxProveedorDTO DTOAux = new ArticuloxProveedorDTO(ArtAux, ProvAux, ArtpAux.Costo);
                    ListaAuxDTO.Add(DTOAux);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return ListaAuxDTO;
        }

        public List<ArticuloxProveedorDTO> ListarActivos()
        {
            List<ArticuloxProveedorDTO> ListaAuxDTO = new List<ArticuloxProveedorDTO>();
            try
            {
                Lista = R_Art.ListarActivos();
                foreach (Articulo Aux in Lista)
                {
                    ArticuloxProveedor ArtpAux = R_ArtxP.Buscar(Aux.Articuloid);
                    Articulo ArtAux = R_Art.BuscarXId(ArtpAux.Articuloid);
                    Proveedor ProvAux = R_Prov.BuscarxId(ArtpAux.Proveedorid);
                    ArticuloxProveedorDTO DTOAux = new ArticuloxProveedorDTO(ArtAux, ProvAux, ArtpAux.Costo);
                    ListaAuxDTO.Add(DTOAux);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return ListaAuxDTO;
        }

        public List<ArticuloxProveedorDTO> ListarXProveedorStockBajo()
        {
            List<ArticuloxProveedorDTO> ListaAuxDTO = new List<ArticuloxProveedorDTO>();
            try
            {
                Lista = R_Art.ListarStockBajo();
                foreach (Articulo Aux in Lista)
                {
                    ArticuloxProveedor ArtpAux = R_ArtxP.Buscar(Aux.Articuloid);
                    Articulo ArtAux = R_Art.BuscarXId(ArtpAux.Articuloid);
                    Proveedor ProvAux = R_Prov.BuscarxId(ArtpAux.Proveedorid);
                    ArticuloxProveedorDTO DTOAux = new ArticuloxProveedorDTO(ArtAux, ProvAux, ArtpAux.Costo);
                    ListaAuxDTO.Add(DTOAux);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return ListaAuxDTO;
        }
        public List<ArticuloxProveedorDTO> PruebaFiltrar(string TxtFiltro)
        {
            List<ArticuloxProveedorDTO> ListaAuxDTO = new List<ArticuloxProveedorDTO>();
            try
            {
                Lista = R_Art.ListarPrueba(TxtFiltro);
                foreach (Articulo Aux in Lista)
                {
                    ArticuloxProveedor ArtpAux = R_ArtxP.Buscar(Aux.Articuloid);
                    Articulo ArtAux = R_Art.BuscarXId(ArtpAux.Articuloid);
                    Proveedor ProvAux = R_Prov.BuscarxId(ArtpAux.Proveedorid);
                    ArticuloxProveedorDTO DTOAux = new ArticuloxProveedorDTO(ArtAux, ProvAux, ArtpAux.Costo);
                    ListaAuxDTO.Add(DTOAux);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return ListaAuxDTO;
        }
        public bool Modificar(int articuloid, Articulo aux, ArticuloxProveedorDTO aux2)
        {
            bool Modi = false;
            try
            {
                ArticuloxProveedor aux1 = new ArticuloxProveedor()
                {
                    Articuloid = aux2.Articuloid.Articuloid,
                    Proveedorid = aux2.Proveedorid.Proveedorid,
                    Costo = aux2.Costo

                };
                Modi = R_Art.Modificar(articuloid, aux, aux1);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Modi;
        }

        public List<Articulo> DependeDeProveedor(int cod)
        {
            List<Articulo> AuxDeP = new List<Articulo>();
            try
            {
                AuxDeP = R_Art.DependeDeProveedor(cod);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return AuxDeP;
        }

        public List<Articulo> ListarStockBajo()
        {
            List<Articulo> Aux = new List<Articulo>();
            try
            {
                Aux = R_Art.ListarStockBajo();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Aux;
        }

        public bool DeshabilitarHabilitarProveedor(int cod, string estado)
        {
            bool DH = false;
            try
            {
                DH = R_Art.DeshabilitarHabilitarProveedor(cod, estado);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return DH;
        }
        public int ObtenerCantidad(Articulo Aux)
        {
            int Cant = 0;
            try
            {
                Cant = R_Art.ObtenerCantidad(Aux);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Cant;
        }

        public bool RestarStock(int cantidad, int id)
        {
            bool RStock = false;
            try
            {
                Articulo Ar = BuscarXId(id);
                if (ObtenerCantidad(Ar) >= cantidad && ObtenerCantidad(Ar) > 0)
                {
                    RStock = R_Art.RestarStock(cantidad, id);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return RStock;
        }

        public bool SumarStock(int cantidad, int id)
        {
            bool SStock = false;
            try
            {
                SStock = R_Art.SumarStock(cantidad, id);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return SStock;
        }

        public bool ArtRepetidosEnElPedido(List<Articulo> Total, Articulo nuevo)
        {
            bool Repetido = false;
            foreach (Articulo item in Total)
            {
                if (item.Articuloid == nuevo.Articuloid)
                {
                    Repetido = true;
                }
            }
            return Repetido;
        }
    }
}
