using System;
using System.Collections.Generic;

namespace MantOrdAPI.Models;

public partial class Tecnico
{
    public int TecnicoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Especialidad { get; set; }

    public DateTime? FechaContratacion { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();
}
