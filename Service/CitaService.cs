using ClinicaAmanecer.DTOS;
using ClinicaAmanecer.Models;
using ClinicaAmanecer.Repositories;

namespace ClinicaAmanecer.Service
{
    public class CitaService(ICitaRepository citaRepo) : ICitaService
    {
        public async Task<(bool Success, string? ErrorMessage)> CambiarEstadoCitaAsync(int citaId, string nuevoEstado)
        {
            var ok = await citaRepo.UpdateEstadoAsync(citaId, nuevoEstado);
            return ok ? (true, null) : (false, "No se encontró la cita médica.");
        }

        public Task<Cita?> GetByIdWithDetailsAsync(int citaId)
        => citaRepo.GetByIdWithDetailsAsync(citaId);

        public Task<IReadOnlyList<Cita>> GetCitasDelDiaAsync(DateTime fecha)
        => citaRepo.GetCitasPorFechaAsync(fecha);

        public Task<IReadOnlyList<Consultorio>> GetConsultoriosActivosAsync()
        => citaRepo.GetConsultoriosActivosAsync();

        public Task<IReadOnlyList<Medico>> GetMedicosDisponiblesAsync()
        => citaRepo.GetMedicosConEspecialidadAsync();
        

        public async Task<(bool Success, string? ErrorMessage)> ProgramarCitaAsync(SaveCitaDto dto)
        {
            bool hayChoque = await citaRepo.ExisteChoqueHorarioAsync(
                dto.MedicoID,
                dto.ConsultorioID,
                dto.FechaCita,
                dto.HoraCita,
                dto.CitaID);

            if (hayChoque)
            {
                return (false, "Choque de horario detectado: El médico o el consultorio ya tienen una cita programada en esa fecha y hora.");
            }

            var cita = new Cita
            {
                Pacientes_PacienteID = dto.PacienteID,
                Medicos_MedicoID = dto.MedicoID,
                Consultorios_ConsultorioID = dto.ConsultorioID,
                FechaCita = dto.FechaCita.Date,
                HoraCita = dto.HoraCita,
                MotivoConsulta = dto.MotivoConsulta.Trim(),
                EstadoCita = "Programada"
            };

            await citaRepo.CreateAsync(cita);
            return (true, null);
        }
    }
}
