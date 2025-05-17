using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Tienda_Virtual.Estructuras;
using Tienda_Virtual.Models;

namespace Tienda_Virtual.Estructuras
{
    public class ListaEnlazada
    {
        public NodoLista inicio;
        public NodoLista fin; // Para agregar al final de forma eficiente
        public int totalNodos;

        public ListaEnlazada()
        {
            inicio = null;
            fin = null;
            totalNodos = 0;
        }

        public bool EstaVacia()
        {
            return inicio == null;
        }

        public void Agregar(Models.Producto producto)
        {
            NodoLista actual = inicio;
            NodoLista anterior = null;
            bool encontrado = false;

            // Buscar si ya existe
            while (actual != null)
            {
                if (actual.producto.IdProducto == producto.IdProducto)
                {
                    actual.vistas++;
                    encontrado = true;
                    break;
                }
                anterior = actual;
                actual = actual.siguiente;
            }

            // Si no se encontró, agregar al final
            if (!encontrado)
            {
                NodoLista nuevo = new NodoLista(producto);
                if (EstaVacia())
                {
                    inicio = nuevo;
                    fin = nuevo;
                }
                else
                {
                    fin.siguiente = nuevo;
                    fin = nuevo;
                }
                totalNodos++;
            }

            // Reordenar lista por vistas (descendente)
            OrdenarPorVistas();
        }
        private void OrdenarPorVistas()
        {
            if (inicio == null || inicio.siguiente == null)
                return;

            List<NodoLista> nodos = new List<NodoLista>();

            // Guardar nodos en una lista
            NodoLista actual = inicio;
            while (actual != null)
            {
                nodos.Add(actual);
                actual = actual.siguiente;
            }

            // Ordenar por cantidad de vistas descendente
            nodos = nodos.OrderByDescending(n => n.vistas).ToList();

            // Reconstruir la lista enlazada
            inicio = nodos[0];
            actual = inicio;
            for (int i = 1; i < nodos.Count; i++)
            {
                actual.siguiente = nodos[i];
                actual = actual.siguiente;
            }
            actual.siguiente = null;
            fin = actual;
        }


        public void Eliminar(int productoId)
        {
            if (inicio == null) return;

            if (inicio.producto.IdProducto == productoId)
            {
                inicio = inicio.siguiente;
                if (inicio == null)
                    fin = null;
                totalNodos--;
                return;
            }

            NodoLista actual = inicio;
            while (actual.siguiente != null && actual.siguiente.producto.IdProducto != productoId)
            {
                actual = actual.siguiente;
            }

            if (actual.siguiente != null)
            {
                if (actual.siguiente == fin)
                    fin = actual;
                actual.siguiente = actual.siguiente.siguiente;
                totalNodos--;
            }
        }

        // Método para obtener la lista de productos en orden para WPF
        public List<Models.Producto> ObtenerProductos()
        {
            List<Models.Producto> productos = new List<Models.Producto>();
            NodoLista actual = inicio;
            while (actual != null)
            {
                productos.Add(actual.producto);
                actual = actual.siguiente;
            }
            return productos;
        }

        public List<int> ObtenerIdsProductos()
        {
            List<int> ids = new List<int>();
            NodoLista actual = inicio;

            while (actual != null)
            {
                ids.Add(actual.producto.IdProducto);
                actual = actual.siguiente;
            }

            return ids;
        }


    }

}
