using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesDTO
{
    public class ArticuloxProveedorDTO
    {
        private Articulo articuloid;
        private Proveedor proveedorid;
        private double costo;

        public ArticuloxProveedorDTO(Articulo articuloid, Proveedor proveedorid, double costo)
        {
            Articuloid = articuloid;
            Proveedorid = proveedorid;
            Costo = costo;
        }

        public Articulo Articuloid
        {
            get
            {
                return articuloid;
            }

            set
            {
                articuloid = value;
            }
        }

        public Proveedor Proveedorid
        {
            get
            {
                return proveedorid;
            }

            set
            {
                proveedorid = value;
            }
        }

        public double Costo
        {
            get
            {
                return costo;
            }

            set
            {
                costo = value;
            }
        }
    }
}
