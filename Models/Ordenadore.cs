using System;
using System.Collections.Generic;

namespace MantOrdAPI.Models;

public partial class Ordenadore
{
    public int OrdenadorId { get; set; }

    public int ClienteId { get; set; }

    public string? Marca { get; set; }

    public string? Modelo { get; set; }

    public string? NumeroSerie { get; set; }

    public DateTime? FechaCompra { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual Cliente? Cliente { get; set; } = null!;
}
