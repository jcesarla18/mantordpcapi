using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MantOrdAPI.Models;

public partial class DbmantordContext : DbContext
{
    public DbmantordContext()
    {
    }

    public DbmantordContext(DbContextOptions<DbmantordContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cita> Citas { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Ordenadore> Ordenadores { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Tecnico> Tecnicos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cita>(entity =>
        {
            entity.HasKey(e => e.CitaId).HasName("PK__Citas__F0E2D9F23F3C4025");

            entity.HasIndex(e => e.FechaCita, "IDX_Citas_FechaCita");

            entity.Property(e => e.CitaId).HasColumnName("CitaID");
            entity.Property(e => e.ClienteId).HasColumnName("ClienteID");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .HasDefaultValue("Pendiente");
            entity.Property(e => e.FechaCita).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.OrdenadorId).HasColumnName("OrdenadorID");
            entity.Property(e => e.ServicioId).HasColumnName("ServicioID");
            entity.Property(e => e.TecnicoId).HasColumnName("TecnicoID");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Cita)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Citas__ClienteID__60A75C0F");

            entity.HasOne(d => d.Ordenador).WithMany(p => p.Cita)
                .HasForeignKey(d => d.OrdenadorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Citas__Ordenador__619B8048");

            entity.HasOne(d => d.Servicio).WithMany(p => p.Cita)
                .HasForeignKey(d => d.ServicioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Citas__ServicioI__6383C8BA");

            entity.HasOne(d => d.Tecnico).WithMany(p => p.Cita)
                .HasForeignKey(d => d.TecnicoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Citas__TecnicoID__628FA481");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("PK__Clientes__71ABD0A71A6CC1B4");

            entity.HasIndex(e => e.Email, "IDX_Clientes_Email");

            entity.HasIndex(e => e.Email, "UQ__Clientes__A9D105342C96E35F").IsUnique();

            entity.Property(e => e.ClienteId).HasColumnName("ClienteID");
            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(15);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Clientes__Usuari__4E88ABD4");
        });

        modelBuilder.Entity<Ordenadore>(entity =>
        {
            entity.HasKey(e => e.OrdenadorId).HasName("PK__Ordenado__D94484F6CFDBD74D");

            entity.Property(e => e.OrdenadorId).HasColumnName("OrdenadorID");
            entity.Property(e => e.ClienteId).HasColumnName("ClienteID");
            entity.Property(e => e.FechaCompra).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Marca).HasMaxLength(50);
            entity.Property(e => e.Modelo).HasMaxLength(50);
            entity.Property(e => e.NumeroSerie).HasMaxLength(50);

            entity.HasOne(d => d.Cliente).WithMany(p => p.Ordenadores)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ordenador__Clien__52593CB8");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.ServicioId).HasName("PK__Servicio__D5AEEC22CBA4B997");

            entity.HasIndex(e => e.Nombre, "IDX_Servicios_Nombre");

            entity.Property(e => e.ServicioId).HasColumnName("ServicioID");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Tecnico>(entity =>
        {
            entity.HasKey(e => e.TecnicoId).HasName("PK__Tecnicos__C866A07D84FCE63E");

            entity.HasIndex(e => e.Email, "IDX_Tecnicos_Email");

            entity.HasIndex(e => e.Email, "UQ__Tecnicos__A9D10534D8033F93").IsUnique();

            entity.Property(e => e.TecnicoId).HasColumnName("TecnicoID");
            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Especialidad).HasMaxLength(100);
            entity.Property(e => e.FechaContratacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(15);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE798A0DE061B");

            entity.HasIndex(e => e.Email, "UQ__Usuarios__A9D10534A1F3772F").IsUnique();

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.Contrasena).HasMaxLength(256);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Rol).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
