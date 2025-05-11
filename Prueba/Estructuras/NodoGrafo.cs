using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda_Virtual.Estructuras
{
    // Esta clase representa un nodo dentro del grafo de productos.
    // Cada nodo corresponde a un producto específico.
    public class NodoGrafo
    {
        // Identificador único del producto (se corresponde con el ID en la base de datos).
        public int ID { get; set; }

        // Nombre del producto, útil para mostrar en la interfaz.
        public string Nombre { get; set; }

        // Lista de productos relacionados directamente con este (nodos adyacentes).
        public List<NodoGrafo> Adyacentes { get; set; }

        // Constructor que inicializa un nodo del grafo con su ID y nombre.
        public NodoGrafo(int id, string nombre)
        {
            ID = id;
            Nombre = nombre;
            Adyacentes = new List<NodoGrafo>();
        }

        // Agrega una conexión a otro producto relacionado si no existe ya.
        public void ConectarCon(NodoGrafo otroProducto)
        {
            if (!Adyacentes.Contains(otroProducto))
            {
                Adyacentes.Add(otroProducto);
            }
        }
    }
}
