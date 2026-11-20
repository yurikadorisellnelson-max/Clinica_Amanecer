using ClinicaAmanecer.DTOS;
using ClinicaAmanecer.Models;

namespace ClinicaAmanecer.Service
{
    public interface IFacturaService
    {
        Task<IReadOnlyList<Factura>> GetHistorialFacturasAsync();
        Task<Factura?> GetByIdWithDetallesAsync(int facturaId);
        Task<IReadOnlyList<ServicioClinico>> GetServiciosActivosAsync();
        Task<(bool Success, string? ErrorMessage)> EmitirFacturaAsync(SaveFacturaDto dto);
        Task<(bool Success, string? ErrorMessage)> AnularFacturaAsync(int facturaId);
    }
}
