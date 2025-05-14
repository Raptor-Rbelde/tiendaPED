using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda_Virtual.Estructuras
{
    public class NodoLista
    {
        public Models.Producto producto;
        public NodoLista siguiente;

        public NodoLista(Models.Producto producto)
        {
            this.producto = producto;
            this.siguiente = null;
        }

    }
}
