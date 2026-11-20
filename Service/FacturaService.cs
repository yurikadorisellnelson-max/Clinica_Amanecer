using ClinicaAmanecer.DTOS;
using ClinicaAmanecer.Models;
using ClinicaAmanecer.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ClinicaAmanecer.Service
{
    public class FacturaService(IFacturaRepository facturaRepo) : IFacturaService
    {
        public async Task<(bool Success, string? ErrorMessage)> AnularFacturaAsync(int facturaId)
        {
            var ok = await facturaRepo.AnularFacturaAsync(facturaId);
            return ok ? (true, null) : (false, "Factura no encontrada.");
            
        }

        public async Task<(bool Success, string? ErrorMessage)> EmitirFacturaAsync(SaveFacturaDto dto)
        {
            if (dto.Servicios == null || dto.Servicios.Count == 0)
                return (false, "Debe agregar al menos un servicio clínico a la factura.");

            var detalles = dto.Servicios.Select(s => new DetalleFactura_Servicio
            {
                ServicioClinico_ServicioID = s.ServicioID,
                Cantidad = s.Cantidad > 0 ? s.Cantidad : 1,
                PrecioUnitario = s.PrecioUnitario,
                SubTotalLinea = (s.Cantidad > 0 ? s.Cantidad : 1) * s.PrecioUnitario
            }).ToList();

            decimal subTotal = detalles.Sum(d => d.SubTotalLinea);
            decimal totalNeto = Math.Max(0, subTotal - dto.Descuento);

            var factura = new Factura
            {
                NumeroFactura = dto.NumeroFactura.Trim(),
                FechaEmision = DateTime.Now,
                Pacientes_PacienteID = dto.PacienteID,
                Citas_CitaID = dto.CitaID,
                Usuarios_UsuarioID = dto.UsuarioCajeroID,
                MetodoPago = dto.MetodoPago,
                SubTotal = subTotal,
                Descuento = dto.Descuento,
                TotalNeto = totalNeto,
                EstadoFactura = "Pagada"
            };

            await facturaRepo.CreateTransaccionalAsync(factura, detalles);
            return (true, null);
        }

        public Task<Factura?> GetByIdWithDetallesAsync(int facturaId)
        => facturaRepo.GetByIdWithDetallesAsync(facturaId);

        public Task<IReadOnlyList<Factura>> GetHistorialFacturasAsync()
        => facturaRepo.GetHistorialFacturasAsync();

        public Task<IReadOnlyList<ServicioClinico>> GetServiciosActivosAsync()
        => facturaRepo.GetServiciosActivosAsync();
    }
}
