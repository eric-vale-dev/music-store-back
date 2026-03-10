namespace MusicStore.Api.Models
{
    public class Carrito
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; } = string.Empty;

        // Navegación
        public Usuario? Usuario { get; set; }
        public ICollection<DetalleCarrito> Detalles { get; set; } = new List<DetalleCarrito>();
    }
}