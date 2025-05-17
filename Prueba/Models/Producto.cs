using System;
using System.Collections.Generic;

namespace Tienda_Virtual.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public int IdUsuario { get; set; }

    public string NombreProducto { get; set; } = null!;

    public decimal? Precio { get; set; }

    public string Descripcion { get; set; } = null!;

    public string? RutaImagen { get; set; }

    public string? Categoria { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
