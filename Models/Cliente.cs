using System;
using System.Collections.Generic;

namespace MantOrdAPI.Models;

public partial class Cliente
{
    public int ClienteId { get; set; }

    public int UsuarioId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Apellido { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual ICollection<Ordenadore> Ordenadores { get; set; } = new List<Ordenadore>();

    public virtual Usuario? Usuario { get; set; } = null!;
}
