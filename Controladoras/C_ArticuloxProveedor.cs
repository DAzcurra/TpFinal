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
    public class C_ArticuloxProveedor 
    {
        R_ArticuloxProveedor R_ArtxP = new R_ArticuloxProveedor();
        R_Articulo R_Art = new R_Articulo();
        R_Proveedor R_Prov = new R_Proveedor();
        Articulo AuxArt = null;
        ArticuloxProveedor AuxArtxP = null;
        Proveedor AuxProv = null;
        public bool Agregar(int articuloid, int proveedorid,double costo)
        {
            bool Agre = false;
            try
            {
                AuxArt = R_Art.BuscarXId(articuloid);
                AuxProv = R_Prov.BuscarxId(proveedorid);
                if(AuxArt != null && AuxProv != null)
                {
                    ArticuloxProveedor Nuevo = new ArticuloxProveedor()
                    {
                        Articuloid = AuxArt.Articuloid,
                        Proveedorid = AuxProv.Proveedorid,
                        Costo = costo

                    };
                    Agre = R_ArtxP.Agregar(Nuevo);
                }
                
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Agre;
        }

        public ArticuloxProveedorDTO Buscar(int cod)
        {
            ArticuloxProveedorDTO AuxDTO = null;
            try
            {
                ArticuloxProveedor Aux = R_ArtxP.Buscar(cod);
                if(Aux != null)
                {
                    Articulo ArtAux = R_Art.BuscarXId(Aux.Articuloid);
                    Proveedor ProvAux = R_Prov.BuscarxId(Aux.Proveedorid);
                    AuxDTO = new ArticuloxProveedorDTO(ArtAux,AuxProv,Aux.Costo);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return AuxDTO;
        }

        public List<ArticuloxProveedorDTO> DevolverTodo()
        {
            List<ArticuloxProveedorDTO> ListaDevolver = new List<ArticuloxProveedorDTO>();
            List<ArticuloxProveedor> Lista = R_ArtxP.DevolverTodo();
            try
            {
                foreach (ArticuloxProveedor AP in Lista)
                {
                    Articulo ArtAux = R_Art.BuscarXId(AP.Articuloid);
                    Proveedor ProvAux = R_Prov.BuscarxId(AP.Proveedorid);
                    ArticuloxProveedorDTO Aux = new ArticuloxProveedorDTO(AuxArt,AuxProv,AP.Costo);
                    ListaDevolver.Add(Aux);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return ListaDevolver;
        }

        public bool Eliminar(ArticuloxProveedor ArtxP)
        {
            bool Elim = false;
            try
            {
                ArticuloxProveedor Aux = R_ArtxP.Buscar(ArtxP.Articuloid);
               Elim = R_ArtxP.Eliminar(Aux);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Elim;
        }

        public bool Modificar(int cod, ArticuloxProveedorDTO ArtxP)
        {
            bool modif = false;
            try
            {
                ArticuloxProveedor Aux = R_ArtxP.Buscar(ArtxP.Articuloid.Articuloid);
                if(Aux != null)
                {
                modif = R_ArtxP.Modificar(cod, Aux);

                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return modif;
        }

        public ArticuloxProveedorDTO ListaEspecial(int id)
        {
            ArticuloxProveedorDTO Aux1 = null;
            ArticuloxProveedor Aux = null;
            Articulo Art = null;
            Proveedor Prov = null;
            try
            {
                Aux = R_ArtxP.ListaEspecial(id);
                if(Aux != null)
                {
                    Prov = R_Prov.BuscarxId(Aux.Proveedorid);
                    Art = R_Art.BuscarXId(Aux.Articuloid);
                    Aux1 = new ArticuloxProveedorDTO(Art,Prov,Aux.Costo);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Aux1;
        }
    }
}
