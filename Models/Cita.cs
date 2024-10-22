using System;
using System.Collections.Generic;

namespace MantOrdAPI.Models;

public partial class Cita
{
    public int CitaId { get; set; }

    public int ClienteId { get; set; }

    public int OrdenadorId { get; set; }

    public int TecnicoId { get; set; }

    public int ServicioId { get; set; }

    public DateTime FechaCita { get; set; }

    public string? Estado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual Ordenadore Ordenador { get; set; } = null!;

    public virtual Servicio Servicio { get; set; } = null!;

    public virtual Tecnico Tecnico { get; set; } = null!;
}
