using ClinicaAmanecer.Data;
using ClinicaAmanecer.DTOS;
using ClinicaAmanecer.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ClinicaAmanecer.Repositories
{
    public class PacienteRepository(AppDBContext context) : IPacienteRepository
    {
        public async Task<DataTableResponseDto<PacienteTableDto>> GetPagedAsync(DataTablesRequestDto request)
        {
            var query = context.Pacientes
                .Include(p => p.DetalleAlergias)
                    .ThenInclude(da => da.CatalogoAlergia)
                .AsQueryable();

            var totalRecords = await query.CountAsync();
            var search = request.Search?.Value?.Trim();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p =>
                    p.NumeroExpediente.Contains(search) ||
                    p.Cedula.Contains(search) ||
                    p.Nombres.Contains(search) ||
                    p.Apellidos.Contains(search));
            }

            var filteredRecords = await query.CountAsync();

            var data = await query
                .OrderByDescending(p => p.PacienteID)
                .Skip(request.Start)
                .Take(request.Length > 0 ? request.Length : 10)
                .Select(p => new PacienteTableDto
                {
                    PacienteID = p.PacienteID,
                    NumeroExpediente = p.NumeroExpediente,
                    Cedula = p.Cedula,
                    NombreCompleto = p.Nombres + " " + p.Apellidos,
                    TipoSangre = p.TipoSangre,
                    Telefono = p.Telefono,
                    ResumenAlergias = p.DetalleAlergias.Any()
                        ? string.Join(", ", p.DetalleAlergias.Select(a => a.CatalogoAlergia.NombreAlergeno))
                        : "Ninguno",
                    Estado = p.Estado
                })
                .ToListAsync();

            return new DataTableResponseDto<PacienteTableDto>
            {
                Draw = request.Draw,
                RecordsTotal = totalRecords,
                RecordsFiltered = filteredRecords,
                Data = data
            };
        }

        public async Task<IReadOnlyList<Paciente>> GetAllActivosAsync()
        {
            return await context.Pacientes
                .Where(p => p.Estado)
                .OrderBy(p => p.Nombres)
                .ToListAsync();
        }

        public async Task<Paciente?> GetByIdWithAlergiasAsync(int id)
        {
            return await context.Pacientes
                .Include(p => p.DetalleAlergias)
                    .ThenInclude(da => da.CatalogoAlergia)
                .FirstOrDefaultAsync(p => p.PacienteID == id);
        }

        public async Task<bool> CedulaExisteAsync(string cedula, int? excludeId = null)
        {
            return await context.Pacientes.AnyAsync(p =>
                p.Cedula == cedula && (!excludeId.HasValue || p.PacienteID != excludeId.Value));
        }

        public async Task<bool> ExpedienteExisteAsync(string expediente, int? excludeId = null)
        {
            return await context.Pacientes.AnyAsync(p =>
                p.NumeroExpediente == expediente && (!excludeId.HasValue || p.PacienteID != excludeId.Value));
        }

        public async Task<Paciente> CreateWithAlergiasAsync(Paciente paciente, List<DetallePaciente_Alergia> alergias)
        {
            await using var transaction = await context.Database.BeginTransactionAsync();
            await context.Pacientes.AddAsync(paciente);
            await context.SaveChangesAsync();

            foreach (var alergia in alergias)
            {
                alergia.Pacientes_PacienteID = paciente.PacienteID;
                context.DetallePaciente_Alergias.Add(alergia);
            }

            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return paciente;
        }

        public async Task<bool> UpdateWithAlergiasAsync(Paciente paciente, List<DetallePaciente_Alergia> nuevasAlergias)
        {
            await using var transaction = await context.Database.BeginTransactionAsync();

            var existentes = await context.DetallePaciente_Alergias
                .Where(d => d.Pacientes_PacienteID == paciente.PacienteID)
                .ToListAsync();

            context.DetallePaciente_Alergias.RemoveRange(existentes);
            context.Pacientes.Update(paciente);

            foreach (var alergia in nuevasAlergias)
            {
                alergia.Pacientes_PacienteID = paciente.PacienteID;
                context.DetallePaciente_Alergias.Add(alergia);
            }

            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }

        public async Task<IReadOnlyList<CatalogoAlergia>> GetCatalogoAlergiasAsync()
        {
            return await context.CatalogoAlergias.OrderBy(a => a.NombreAlergeno).ToListAsync();
        }
    }
}