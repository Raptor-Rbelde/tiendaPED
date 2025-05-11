using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tienda_Virtual.Models;

namespace Tienda_Virtual.Estructuras
{
    // Este servicio se encarga de consultar la base de datos
    // para obtener los productos vistos o comprados por un usuario
    // y generar una lista de productos recomendados con base en ellos.
    public class ServicioDeRecomendacion
    {
        private readonly TiendaPedContext _context;
        private readonly GenerarRecomendaciones _recomendador;

        // Constructor que recibe el contexto de base de datos y el grafo construido.
        public ServicioDeRecomendacion(TiendaPedContext context, GrafoProducto grafo)
        {
            _context = context;
            _recomendador = new GenerarRecomendaciones(grafo);
        }

        // Obtiene recomendaciones para un usuario específico, usando los productos que él ya registró.
        public List<NodoGrafo> ObtenerRecomendacionesParaUsuario(int idUsuario)
        {
            // Se obtienen los productos que este usuario ha subido (o compró, dependiendo del modelo)
            var productosDelUsuario = _context.Productos
                .Where(p => p.IdUsuario == idUsuario)
                .Select(p => p.IdProducto)
                .ToList();

            // Se generan recomendaciones excluyendo los productos que ya están en su historial
            return _recomendador.RecomendarProducto(productosDelUsuario);
        }
    }
}