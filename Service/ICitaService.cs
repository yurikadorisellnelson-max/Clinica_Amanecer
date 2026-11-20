using ClinicaAmanecer.DTOS;
using ClinicaAmanecer.Models;

namespace ClinicaAmanecer.Service
{
    public interface ICitaService
    {
        Task<IReadOnlyList<Cita>> GetCitasDelDiaAsync(DateTime fecha);
        Task<Cita?> GetByIdWithDetailsAsync(int citaId);
        Task<IReadOnlyList<Medico>> GetMedicosDisponiblesAsync();
        Task<IReadOnlyList<Consultorio>> GetConsultoriosActivosAsync();
        Task<(bool Success, string? ErrorMessage)> ProgramarCitaAsync(SaveCitaDto dto);
        Task<(bool Success, string? ErrorMessage)> CambiarEstadoCitaAsync(int citaId, string nuevoEstado);
    }
}
