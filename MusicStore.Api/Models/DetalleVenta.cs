namespace MusicStore.Api.Models
{
    public class DetalleVenta
    {
        public int Id { get; set; }
        public int VentaId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; } // El precio histórico al momento de la compra

        // Navegación
        public Venta? Venta { get; set; }
        public Producto? Producto { get; set; }
    }
}