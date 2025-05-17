using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda_Virtual.Estructuras
{
    public class SesionActual
    {
        public static ListaEnlazada HistorialBusquedas { get; set; } = new ListaEnlazada();
        public static List<int> CarritoUsuario { get; set; } = new List<int>();
        public static GrafoProducto Grafo { get; set; } = new GrafoProducto();
    }
}
