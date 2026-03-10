namespace MusicStore.Api.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; } = string.Empty;
        public DateTime FechaVenta { get; set; } = DateTime.UtcNow;
        public decimal Total { get; set; }
        public string Estatus { get; set; } = "Pendiente"; // Pendiente, Pagado, Cancelado

        // Navegación
        public Usuario? Usuario { get; set; }
        public ICollection<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();
    }
}