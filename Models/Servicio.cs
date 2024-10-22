using System;
using System.Collections.Generic;

namespace MantOrdAPI.Models;

public partial class Servicio
{
    public int ServicioId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public int DuracionMinutos { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();
}
