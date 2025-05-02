using System;
using System.Collections.Generic;

namespace Tienda_Virtual.Models;

public partial class CorreosUsuario
{
    public int IdUsuario { get; set; }

    public string? CorreoUsuario { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
