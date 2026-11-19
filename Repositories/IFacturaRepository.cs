using ClinicaAmanecer.Models;

namespace ClinicaAmanecer.Repositories
{
    public interface IFacturaRepository
    {
        Task<IReadOnlyList<Factura>> GetHistorialFacturasAsync();
        Task<Factura?> GetByIdWithDetallesAsync(int facturaId);
        Task<Factura> CreateTransaccionalAsync(Factura factura, List<DetalleFactura_Servicio> detalles);
        Task<bool> AnularFacturaAsync(int facturaId);
        Task<IReadOnlyList<ServicioClinico>> GetServiciosActivosAsync();
    }
}