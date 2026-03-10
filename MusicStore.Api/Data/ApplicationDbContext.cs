namespace MusicStore.Api.Data;

using Microsoft.EntityFrameworkCore;
using MusicStore.Api.Models;

public class ApplicationDbContext : DbContext
{
    // Constructor que recibe las opciones de conexión
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // Aquí le decimos qué modelos se van a convertir en tablas
    public DbSet<Rol> Roles { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Carrito> Carritos { get; set; }
    public DbSet<DetalleCarrito> DetallesCarrito { get; set; }
    public DbSet<Venta> Ventas { get; set; }
    public DbSet<DetalleVenta> DetallesVenta { get; set; }
}