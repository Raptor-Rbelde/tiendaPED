using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda_Virtual.Estructuras
{
    // Esta clase representa una relación entre dos productos dentro del grafo.
    // Guarda un peso que indica cuán fuerte o frecuente es la relación.
    public class RelacionProducto
    {
        // ID del primer producto relacionado.
        public int IdProducto1 { get; set; }

        // ID del segundo producto relacionado.
        public int IdProducto2 { get; set; }

        // Peso o intensidad de la relación (por defecto inicia en 1).
        public int Peso { get; set; } = 1;

        // Constructor que recibe los dos IDs de productos a relacionar.
        public RelacionProducto(int id1, int id2)
        {
            IdProducto1 = id1;
            IdProducto2 = id2;
        }

        // Incrementa el peso de la relación en uno,
        // indicando que esta conexión ha ocurrido nuevamente.
        public void IncrementarPeso()
        {
            Peso++;
        }

        // Verifica si esta relación corresponde a los dos IDs dados,
        // sin importar el orden en que se entreguen.
        public bool EsRelacionEntre(int id1, int id2)
        {
            return (IdProducto1 == id1 && IdProducto2 == id2) ||
                   (IdProducto1 == id2 && IdProducto2 == id1);
        }
    }
}
