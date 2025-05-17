using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tienda_Virtual.Estructuras;
using Tienda_Virtual.Models;

namespace Tienda_Virtual.Estructuras
{
    public class NodoLista
    {
        public Models.Producto producto;
        public int vistas; // contador de vistas
        public NodoLista siguiente;


        public NodoLista(Models.Producto producto)
        {
            this.producto = producto;
            this.vistas = 1;
            this.siguiente = null;
        }
    }

}
