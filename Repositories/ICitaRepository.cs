using ClinicaAmanecer.Models;

namespace ClinicaAmanecer.Repositories
{
    public interface ICitaRepository
    {
        Task<IReadOnlyList<Cita>> GetCitasPorFechaAsync(DateTime fecha);
        Task<Cita?> GetByIdWithDetailsAsync(int citaId);
        Task<bool> ExisteChoqueHorarioAsync(int medicoId, int consultorioId, DateTime fecha, TimeSpan hora, int? excludeCitaId = null);
        Task<Cita> CreateAsync(Cita cita);
        Task<bool> UpdateEstadoAsync(int citaId, string nuevoEstado);
        Task<IReadOnlyList<Medico>> GetMedicosConEspecialidadAsync();
        Task<IReadOnlyList<Consultorio>> GetConsultoriosActivosAsync();
    }
}