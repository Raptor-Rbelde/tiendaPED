using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda_Virtual.Estructuras
{
    // Esta clase representa el grafo completo de productos y sus relaciones.
    // Permite registrar productos, establecer relaciones entre ellos y generar recomendaciones.
    public class GrafoProducto
    {
        // Diccionario que almacena los nodos del grafo, indexados por ID de producto.
        private Dictionary<int, NodoGrafo> nodos;

        // Lista de relaciones entre productos, cada una con un peso que representa su relevancia.
        private List<RelacionProducto> relaciones;

        // Constructor: inicializa las estructuras internas del grafo.
        public GrafoProducto()
        {
            nodos = new Dictionary<int, NodoGrafo>();
            relaciones = new List<RelacionProducto>();
        }

        // Agrega un nuevo producto al grafo si aún no existe.
        public void AgregarProducto(int id, string nombre)
        {
            if (!nodos.ContainsKey(id))
            {
                nodos[id] = new NodoGrafo(id, nombre);
            }
        }

        // Crea o fortalece la relación entre dos productos.
        public void RelacionarProductos(int id1, int id2)
        {
            if (!nodos.ContainsKey(id1) || !nodos.ContainsKey(id2)) return;

            var existente = relaciones.FirstOrDefault(r => r.EsRelacionEntre(id1, id2));
            if (existente != null)
            {
                existente.IncrementarPeso();
            }
            else
            {
                relaciones.Add(new RelacionProducto(id1, id2));
                nodos[id1].ConectarCon(nodos[id2]);
                nodos[id2].ConectarCon(nodos[id1]);
            }
        }

        // Devuelve una lista de productos relacionados con uno dado, excluyendo los ya vistos.
        // Se ordena por peso para dar prioridad a las recomendaciones más fuertes.
        public List<NodoGrafo> ObtenerRecomendaciones(int idProducto, HashSet<int> productosYaVistos)
        {
            var relacionesConPeso = relaciones
                .Where(r => r.IdProducto1 == idProducto || r.IdProducto2 == idProducto)
                .OrderByDescending(r => r.Peso)
                .Select(r => r.IdProducto1 == idProducto ? r.IdProducto2 : r.IdProducto1)
                .ToList();

            // No filtramos arriba, lo hacemos acá
            var relacionados = relacionesConPeso
                .Where(id => nodos.ContainsKey(id))
                .Select(id => nodos[id])
                .Where(n => !productosYaVistos.Contains(n.ID))
                .ToList();

            return relacionados;
        }


        public void RegistrarInteraccion(int idActual, string nombreActual, List<int> idsRelacionados)
        {
            // Agrega o actualiza el producto actual
            AgregarProducto(idActual, nombreActual);

            foreach (var idRelacionado in idsRelacionados)
            {
                if (!nodos.ContainsKey(idRelacionado))
                {
                   
                    nodos[idRelacionado] = new NodoGrafo(idRelacionado, $"Producto {idRelacionado}");
                }

                RelacionarProductos(idActual, idRelacionado);
            }
        }

        public void AgregarOActualizarProducto(int id, string nombre)
        {
            if (!nodos.ContainsKey(id))
            {
                nodos[id] = new NodoGrafo(id, nombre);
            }
            else if (string.IsNullOrWhiteSpace(nodos[id].Nombre))
            {
                nodos[id].Nombre = nombre;
            }
        }




        // Devuelve la lista completa de productos registrados en el grafo.
        public List<NodoGrafo> ObtenerTodosLosProductos()
        {
            return nodos.Values.ToList();
        }
    }
}
