using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda_Virtual.Models
{
    public static class CarritoCompartido
    {
        // Usar un diccionario para almacenar ID y nombre del producto.
        private static Dictionary<int, string> productosEnCarrito = new Dictionary<int, string>();

        public static IReadOnlyDictionary<int, string> ProductosEnCarrito => productosEnCarrito;

        // Método para agregar producto al carrito.
        public static void AgregarProducto(int idProducto, string nombreProducto)
        {
            if (!productosEnCarrito.ContainsKey(idProducto))
            {
                productosEnCarrito[idProducto] = nombreProducto;
            }
        }

        // Método para remover un producto del carrito.
        public static void RemoverProducto(int idProducto)
        {
            if (productosEnCarrito.ContainsKey(idProducto))
            {
                productosEnCarrito.Remove(idProducto);
            }
        }

        // Método para limpiar el carrito.
        public static void LimpiarCarrito()
        {
            productosEnCarrito.Clear();
        }

        // Verificar si un producto ya está en el carrito.
        public static bool EstaEnCarrito(int idProducto)
        {
            return productosEnCarrito.ContainsKey(idProducto);
        }
    }
}
