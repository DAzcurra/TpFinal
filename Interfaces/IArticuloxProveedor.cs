using Entidades;
using EntidadesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IArticuloxProveedor
    {
        bool Agregar(ArticuloxProveedor Nuevo);
        bool Eliminar(ArticuloxProveedor ArtxP);
        bool Modificar(int cod, ArticuloxProveedor ArtxP);
        ArticuloxProveedor Buscar(int cod);
        List<ArticuloxProveedor> DevolverTodo();
        ArticuloxProveedor ListaEspecial(int id);

    }
}
