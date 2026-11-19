using ClinicaAmanecer.DTOS;
using ClinicaAmanecer.Models;

namespace ClinicaAmanecer.Repositories
{
    public interface IPacienteRepository
    {
        Task<DataTableResponseDto<PacienteTableDto>> GetPagedAsync(DataTablesRequestDto request);
        Task<IReadOnlyList<Paciente>> GetAllActivosAsync();
        Task<Paciente?> GetByIdWithAlergiasAsync(int id);
        Task<bool> CedulaExisteAsync(string cedula, int? excludeId = null);
        Task<bool> ExpedienteExisteAsync(string expediente, int? excludeId = null);
        Task<Paciente> CreateWithAlergiasAsync(Paciente paciente, List<DetallePaciente_Alergia> alergias);
        Task<bool> UpdateWithAlergiasAsync(Paciente paciente, List<DetallePaciente_Alergia> nuevasAlergias);
        Task<IReadOnlyList<CatalogoAlergia>> GetCatalogoAlergiasAsync();
    }
}