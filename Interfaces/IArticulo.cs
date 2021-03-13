using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IArticulo
    {
        bool Agregar(int articuloid, string nombre, string descripcion, string marca, double precioactual, int cantidad, int stockmin);
        bool Eliminar(int articuloid);
        List<Articulo> Listar();
        List<Articulo> BuscarXNombre(string nombre);
        List<Articulo> BuscarXNombreMarca(string nombre);
        List<Articulo> BuscarXMarca(string marca);
        Articulo BuscarXId(int articuloid);
        bool Modificar(int articuloid, Articulo aux, ArticuloxProveedor ArtxP);
        List<Articulo> DependeDeProveedor(int id);
        bool DeshabilitarHabilitarProveedor(int id, string estado);
        int ObtenerCantidad(Articulo Aux);
        bool RestarStock(int cantidad, int id);
        bool SumarStock(int cantidad, int id);
        List<Articulo> ListarStockBajo();
        List<Articulo> BuscarXNombreMarcaActivo(string nombre);
        Articulo BuscarXIdActivo(int articuloid);
        List<Articulo> ListarActivos();

    }
}
