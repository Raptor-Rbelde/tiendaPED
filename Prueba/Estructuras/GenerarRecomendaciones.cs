using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda_Virtual.Estructuras
{
    // Esta clase se encarga de generar recomendaciones de productos
    // utilizando el grafo que contiene las relaciones entre productos.
    public class GenerarRecomendaciones
    {
        private GrafoProducto grafo;

        // Constructor que recibe una instancia del grafo de productos ya construido.
        public GenerarRecomendaciones(GrafoProducto grafoDeProductos)
        {
            grafo = grafoDeProductos;
        }

        // Genera una lista de productos recomendados para un usuario
        // en base a los productos que ya ha visto o agregado al carrito.
        public List<NodoGrafo> RecomendarProducto(List<int> productosVistos)
        {
            var recomendados = new HashSet<NodoGrafo>(); // Para evitar duplicados
            var vistosSet = new HashSet<int>(productosVistos); // Para buscar eficientemente si un producto ya fue visto

            foreach (var id in productosVistos)
            {
                // Obtener los productos relacionados al producto actual, excluyendo los ya vistos
                var vecinos = grafo.ObtenerRecomendaciones(id, vistosSet);

                // Agregar los productos recomendados al conjunto final
                foreach (var vecino in vecinos)
                {
                    recomendados.Add(vecino);
                }
            }

            return recomendados.ToList();
        }
    }
}
