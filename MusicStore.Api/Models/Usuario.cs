namespace MusicStore.Api.Models
{
    public class Usuario
    {
        public string Id { get; set; } = string.Empty; // UID de Firebase
        public int RolId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        // Navegación
        public Rol? Rol { get; set; }
        public Carrito? Carrito { get; set; }
        public ICollection<Venta> Ventas { get; set; } = new List<Venta>();
    }
}