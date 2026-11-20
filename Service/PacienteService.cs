using ClinicaAmanecer.DTOS;
using ClinicaAmanecer.Models;
using ClinicaAmanecer.Repositories;

namespace ClinicaAmanecer.Service
{
    public class PacienteService(IPacienteRepository repo) : IPacienteService
    {
        public async Task<(bool Success, string? ErrorMessage)> CreateAsync(SavePacienteDto dto)
        {
            if (await repo.ExpedienteExisteAsync(dto.NumeroExpediente.Trim()))
                return (false, "El número de expediente ya se encuentra registrado.");

            if (await repo.CedulaExisteAsync(dto.Cedula.Trim()))
                return (false, "La cédula ya pertenece a otro paciente.");

            var paciente = new Paciente
            {
                NumeroExpediente = dto.NumeroExpediente.Trim(),
                Cedula = dto.Cedula.Trim(),
                Nombres = dto.Nombres.Trim(),
                Apellidos = dto.Apellidos.Trim(),
                FechaNacimiento = dto.FechaNacimiento,
                Sexo = dto.Sexo,
                TipoSangre = dto.TipoSangre,
                Telefono = dto.Telefono.Trim(),
                Direccion = dto.Direccion?.Trim(),
                Estado = dto.Estado
            };

            var detalles = dto.Alergias.Select(a => new DetallePaciente_Alergia
            {
                CatalogoAlergias_AlergiaID = a.AlergiaID,
                NivelSeveridad = a.NivelSeveridad,
                NotasReaccion = a.NotasReaccion
            }).ToList();

            await repo.CreateWithAlergiasAsync(paciente, detalles);
            return (true, null);
        }


        public Task<IReadOnlyList<Paciente>> GetAllActivosAsync()
        => repo.GetAllActivosAsync();

        public async Task<SavePacienteDto?> GetByIdAsync(int id)
        {
            var p = await repo.GetByIdWithAlergiasAsync(id);
            if (p == null) return null;

            return new SavePacienteDto
            {
                PacienteID = p.PacienteID,
                NumeroExpediente = p.NumeroExpediente,
                Cedula = p.Cedula,
                Nombres = p.Nombres,
                Apellidos = p.Apellidos,
                FechaNacimiento = p.FechaNacimiento,
                Sexo = p.Sexo,
                TipoSangre = p.TipoSangre,
                Telefono = p.Telefono,
                Direccion = p.Direccion,
                Estado = p.Estado,
                Alergias = p.DetalleAlergias.Select(a => new DetalleAlergiaItemDto
                {
                    DetalleAlergiaID = a.DetalleAlergiaID,
                    AlergiaID = a.CatalogoAlergias_AlergiaID,
                    NombreAlergeno = a.CatalogoAlergia?.NombreAlergeno ?? string.Empty,
                    CategoriaAlergia = a.CatalogoAlergia?.CategoriaAlergia ?? string.Empty,
                    NivelSeveridad = a.NivelSeveridad,
                    NotasReaccion = a.NotasReaccion
                }).ToList()
            };
        }



        public Task<IReadOnlyList<CatalogoAlergia>> GetCatalogoAlergiasAsync()
        => repo.GetCatalogoAlergiasAsync();


        public Task<DataTableResponseDto<PacienteTableDto>> GetPagedAsync(DataTablesRequestDto request)
        => repo.GetPagedAsync(request);



        public async Task<(bool Success, string? ErrorMessage)> UpdateAsync(SavePacienteDto dto)
        {
            if (!dto.PacienteID.HasValue)
                return (false, "ID de paciente inválido.");

            var paciente = await repo.GetByIdWithAlergiasAsync(dto.PacienteID.Value);
            if (paciente == null)
                return (false, "Paciente no encontrado.");

            if (await repo.CedulaExisteAsync(dto.Cedula.Trim(), dto.PacienteID))
                return (false, "La cédula ya pertenece a otro paciente.");

            paciente.NumeroExpediente = dto.NumeroExpediente.Trim();
            paciente.Cedula = dto.Cedula.Trim();
            paciente.Nombres = dto.Nombres.Trim();
            paciente.Apellidos = dto.Apellidos.Trim();
            paciente.FechaNacimiento = dto.FechaNacimiento;
            paciente.Sexo = dto.Sexo;
            paciente.TipoSangre = dto.TipoSangre;
            paciente.Telefono = dto.Telefono.Trim();
            paciente.Direccion = dto.Direccion?.Trim();
            paciente.Estado = dto.Estado;

            var detalles = dto.Alergias.Select(a => new DetalleAlergiaItemDto
            {
                AlergiaID = a.AlergiaID,
                NivelSeveridad = a.NivelSeveridad,
                NotasReaccion = a.NotasReaccion
            }).Select(a => new DetallePaciente_Alergia
            {
                CatalogoAlergias_AlergiaID = a.AlergiaID,
                NivelSeveridad = a.NivelSeveridad,
                NotasReaccion = a.NotasReaccion
            }).ToList();

            await repo.UpdateWithAlergiasAsync(paciente, detalles);
            return (true, null);
        }
    }
}

