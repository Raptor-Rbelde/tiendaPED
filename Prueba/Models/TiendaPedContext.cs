using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tienda_Virtual.Models;

public partial class TiendaPedContext : DbContext
{
    public TiendaPedContext()
    {
    }

    public TiendaPedContext(DbContextOptions<TiendaPedContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost; Database=TiendaPED; Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PK_IdFactura");

            entity.ToTable("Factura");

            entity.Property(e => e.IdFactura).ValueGeneratedNever();
            entity.Property(e => e.FechaCompra)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Producto).WithMany(p => p.Facturas)
                .HasPrincipalKey(p => new { p.IdProducto, p.NombreProducto })
                .HasForeignKey(d => new { d.IdProducto, d.NombreProducto })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_DatosFactura");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK_IdProducto");

            entity.ToTable("Producto");

            entity.HasIndex(e => new { e.IdProducto, e.NombreProducto }, "UQ_IdProducto_NombreProducto").IsUnique();

            entity.Property(e => e.IdProducto).ValueGeneratedNever();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_IdUsuarioProducto");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK_IdUsuario");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.CorreoUsuario, "UC_CorreoUser").IsUnique();

            entity.Property(e => e.IdUsuario).ValueGeneratedNever();
            entity.Property(e => e.Contrasenia)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CorreoUsuario)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
