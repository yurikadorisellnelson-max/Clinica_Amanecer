using ClinicaAmanecer.DTOS;
using ClinicaAmanecer.Models;

namespace ClinicaAmanecer.Service
{
    public interface IPacienteService
    {
        Task<DataTableResponseDto<PacienteTableDto>> GetPagedAsync(DataTablesRequestDto request);
        Task<IReadOnlyList<Paciente>> GetAllActivosAsync();
        Task<SavePacienteDto?> GetByIdAsync(int id);
        Task<IReadOnlyList<CatalogoAlergia>> GetCatalogoAlergiasAsync();
        Task<(bool Success, string? ErrorMessage)> CreateAsync(SavePacienteDto dto);
        Task<(bool Success, string? ErrorMessage)> UpdateAsync(SavePacienteDto dto);
    }
}
