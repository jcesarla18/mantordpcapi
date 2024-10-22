using System;
using System.Collections.Generic;

namespace MantOrdAPI.Models;

public partial class HistorialMantenimiento
{
    public int MantenimientoId { get; set; }

    public int OrdenadorId { get; set; }

    public DateOnly FechaMantenimiento { get; set; }

    public string? DescripcionTrabajo { get; set; }

    public decimal? Costo { get; set; }

    public int UsuarioId { get; set; }

    public virtual Ordenadore Ordenador { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
