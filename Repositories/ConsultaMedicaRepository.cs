using ClinicaAmanecer.Data;
using ClinicaAmanecer.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ClinicaAmanecer.Repositories
{
    public class ConsultaMedicaRepository(AppDBContext context) : IConsultaMedicaRepository
    {
        public async Task<ConsultaMedica?> GetByCitaIdAsync(int citaId)
        {
            return await context.ConsultasMedicas
                .Include(cm => cm.DetalleDiagnosticos)
                    .ThenInclude(d => d.DiagnosticoCIE10)
                .Include(cm => cm.DetalleRecetas)
                    .ThenInclude(r => r.Medicamento)
                .FirstOrDefaultAsync(cm => cm.Citas_CitaID == citaId);
        }

        public async Task<ConsultaMedica> CreateTransaccionalAsync(
            ConsultaMedica consulta,
            List<DetalleConsulta_Diagnostico> diagnosticos,
            List<DetalleReceta_Medicamento> recetas)
        {
            await using var transaction = await context.Database.BeginTransactionAsync();

            await context.ConsultasMedicas.AddAsync(consulta);
            await context.SaveChangesAsync();

            foreach (var diag in diagnosticos)
            {
                diag.ConsultasMedicas_ConsultasID = consulta.ConsultasID;
                context.DetalleConsulta_Diagnosticos.Add(diag);
            }

            foreach (var rec in recetas)
            {
                rec.ConsultasMedicas_ConsultasID = consulta.ConsultasID;
                context.DetalleReceta_Medicamentos.Add(rec);
            }

            var cita = await context.Citas.FindAsync(consulta.Citas_CitaID);
            if (cita != null)
            {
                cita.EstadoCita = "Atendida";
            }

            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return consulta;
        }

        public async Task<IReadOnlyList<CatalogoDiagnosticoCIE10>> GetCatalogoCIE10Async()
        {
            return await context.CatalogoDiagnosticosCIE10.OrderBy(c => c.CodigoCIE10).ToListAsync();
        }

        public async Task<IReadOnlyList<CatalogoMedicamento>> GetCatalogoMedicamentosAsync()
        {
            return await context.CatalogoMedicamentos.OrderBy(m => m.NombreGenerico).ToListAsync();
        }
    }
}