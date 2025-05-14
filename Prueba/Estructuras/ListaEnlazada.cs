using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda_Virtual.Estructuras
{
   public class ListaEnlazada
    {
        public NodoLista inicio;
        public int totalNodos;

        public ListaEnlazada()
        {
            inicio = null;
            totalNodos = 0;
        }

        public bool EstaVacia()
        {
            return inicio == null;
        }

        public void Agregar(Models.Producto producto)
        {
            //Evitar duplicados(ya visto antes)
            if(Contiene(producto.IdProducto)) 
                return;

            NodoLista nuevo = new NodoLista(producto);
            nuevo.siguiente = inicio;
            inicio = nuevo;
            totalNodos++;
        }

        public void Eliminar(int productoId)
        {
            if(inicio == null) return;

            if(inicio.producto.IdProducto == productoId)
            {
                inicio = inicio.siguiente;
                totalNodos--;
                return;
            }

            NodoLista actual = inicio;
            while (actual.siguiente != null && actual.siguiente.producto.IdProducto!=productoId)
            {
                actual = actual.siguiente;
            }

            if (actual.siguiente !=null)
            {
                actual.siguiente = actual.siguiente.siguiente;
                totalNodos--;
            }

        }

        //visualiza si el producto ya fue visto y se guarda en el historial
        public bool Contiene(int productoId)
        {
            NodoLista actual = inicio;
            while(actual !=null)
            {
                if (actual.producto.IdProducto == productoId)
                    return true;

                actual = actual.siguiente;
            }

            return false;
        }

        public void MostrarHistorial()
        {
            NodoLista actual = inicio;
            while (actual !=null)
            {
                Console.WriteLine($"Producto: {actual.producto.NombreProducto} - Precio: {actual.producto.Precio}"); 
                actual = actual.siguiente;
            }
        }







    }
}
