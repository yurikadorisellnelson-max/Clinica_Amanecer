using ClinicaAmanecer.Data;
using ClinicaAmanecer.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ClinicaAmanecer.Repositories
{
    public class CitaRepository(AppDBContext context) : ICitaRepository
    {
        public async Task<IReadOnlyList<Cita>> GetCitasPorFechaAsync(DateTime fecha)
        {
            var fechaSolo = fecha.Date;
            return await context.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Medico)
                    .ThenInclude(m => m.Especialidad)
                .Include(c => c.Consultorio)
                .Where(c => c.FechaCita.Date == fechaSolo)
                .OrderBy(c => c.HoraCita)
                .ToListAsync();
        }

        public async Task<Cita?> GetByIdWithDetailsAsync(int citaId)
        {
            return await context.Citas
                .Include(c => c.Paciente)
                    .ThenInclude(p => p.DetalleAlergias)
                        .ThenInclude(da => da.CatalogoAlergia)
                .Include(c => c.Medico)
                .Include(c => c.Consultorio)
                .FirstOrDefaultAsync(c => c.CitaID == citaId);
        }

        public async Task<bool> ExisteChoqueHorarioAsync(int medicoId, int consultorioId, DateTime fecha, TimeSpan hora, int? excludeCitaId = null)
        {
            var fechaSolo = fecha.Date;
            return await context.Citas.AnyAsync(c =>
                c.FechaCita.Date == fechaSolo &&
                c.HoraCita == hora &&
                c.EstadoCita != "Cancelada" &&
                (!excludeCitaId.HasValue || c.CitaID != excludeCitaId.Value) &&
                (c.Medicos_MedicoID == medicoId || c.Consultorios_ConsultorioID == consultorioId));
        }

        public async Task<Cita> CreateAsync(Cita cita)
        {
            var entry = await context.Citas.AddAsync(cita);
            await context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<bool> UpdateEstadoAsync(int citaId, string nuevoEstado)
        {
            var cita = await context.Citas.FindAsync(citaId);
            if (cita == null) return false;
            cita.EstadoCita = nuevoEstado;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<IReadOnlyList<Medico>> GetMedicosConEspecialidadAsync()
        {
            return await context.Medicos
                .Include(m => m.Especialidad)
                .Where(m => m.Estado)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Consultorio>> GetConsultoriosActivosAsync()
        {
            return await context.Consultorios.Where(c => c.Estado).ToListAsync();
        }
    }
}