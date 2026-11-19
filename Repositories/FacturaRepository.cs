using ClinicaAmanecer.Data;
using ClinicaAmanecer.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ClinicaAmanecer.Repositories
{
    public class FacturaRepository(AppDBContext context) : IFacturaRepository
    {
        public async Task<IReadOnlyList<Factura>> GetHistorialFacturasAsync()
        {
            return await context.Facturas
                .Include(f => f.Paciente)
                .Include(f => f.UsuarioCajero)
                .OrderByDescending(f => f.FechaEmision)
                .ToListAsync();
        }

        public async Task<Factura?> GetByIdWithDetallesAsync(int facturaId)
        {
            return await context.Facturas
                .Include(f => f.Paciente)
                .Include(f => f.UsuarioCajero)
                .Include(f => f.DetalleServicios)
                    .ThenInclude(d => d.ServicioClinico)
                .FirstOrDefaultAsync(f => f.FacturaID == facturaId);
        }

        public async Task<Factura> CreateTransaccionalAsync(Factura factura, List<DetalleFactura_Servicio> detalles)
        {
            await using var transaction = await context.Database.BeginTransactionAsync();

            await context.Facturas.AddAsync(factura);
            await context.SaveChangesAsync();

            foreach (var item in detalles)
            {
                item.Facturas_FacturaID = factura.FacturaID;
                context.DetalleFactura_Servicios.Add(item);
            }

            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return factura;
        }

        public async Task<bool> AnularFacturaAsync(int facturaId)
        {
            var factura = await context.Facturas.FindAsync(facturaId);
            if (factura == null) return false;

            factura.EstadoFactura = "Anulada";
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<IReadOnlyList<ServicioClinico>> GetServiciosActivosAsync()
        {
            return await context.ServiciosClinicos.Where(s => s.Activo).ToListAsync();
        }
    }
}