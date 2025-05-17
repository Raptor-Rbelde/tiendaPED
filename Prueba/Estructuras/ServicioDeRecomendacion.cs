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
            // Obtiene productos vistos
            var productosVistos = SesionActual.HistorialBusquedas.ObtenerProductos()
                                 .Concat(
                                     SesionActual.CarritoUsuario.Select(
                                         id => _context.Productos.FirstOrDefault(p => p.IdProducto == id)))
                                 .Where(p => p != null)
                                 .ToList();

            // Extraer categorías + ids para filtrar
            var categoriasVistas = productosVistos.Select(p => p.Categoria).Distinct().ToList();
            var idsVistos = productosVistos.Select(p => p.IdProducto).ToHashSet();

            //  Buscar otros productos CON esas categorías
            var candidatos = _context.Productos
                              .Where(p => categoriasVistas.Contains(p.Categoria!) &&
                                          !idsVistos.Contains(p.IdProducto))
                              .ToList();

            // Mapear a NodoGrafo
            var resultado = candidatos
                            .Select(p => new NodoGrafo(p.IdProducto, p.NombreProducto))
                            .ToList();

            return resultado;
        }


        public List<NodoGrafo> ObtenerRecomendacionesDesdeHistorial(List<int> ids)
        {
            return _recomendador.RecomendarProducto(ids);
        }

    }
}