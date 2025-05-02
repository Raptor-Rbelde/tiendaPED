using System;
using System.Collections.Generic;

namespace Tienda_Virtual.Models;

public partial class Factura
{
    public int IdFactura { get; set; }

    public DateTime? FechaCompra { get; set; }

    public int? IdProducto { get; set; }

    public string? NombreProducto { get; set; }

    public virtual Producto? Producto { get; set; }
}
