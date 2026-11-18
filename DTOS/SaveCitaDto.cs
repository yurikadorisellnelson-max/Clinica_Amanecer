using System.ComponentModel.DataAnnotations;

namespace ClinicaAmanecer.DTOS
{
    public class SaveCitaDto
    {
        public int? CitaID { get; set; }
        [Required]
        public int PacienteID { get; set; }
        [Required]
        public int MedicoID { get; set; }
        [Required]
        public int ConsultorioID { get; set; }
        [Required]
        public DateTime FechaCita { get; set; }
        [Required]
        public TimeSpan HoraCita { get; set; }
        [Required, MaxLength(200)]
        public string MotivoConsulta { get; set; } = string.Empty;
        public string EstadoCita { get; set; } = "Programada";
    }
}