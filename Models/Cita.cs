using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaAmanecer.Models
{
    public class Cita
    {
        [Key]
        public int CitaID { get; set; }
        public DateTime FechaCita { get; set; }
        public TimeSpan HoraCita { get; set; }
        [Required, MaxLength(200)]
        public string MotivoConsulta { get; set; } = string.Empty;
        [Required, MaxLength(20)]
        public string EstadoCita { get; set; } = "Programada";

        public int Consultorios_ConsultorioID { get; set; }
        [ForeignKey(nameof(Consultorios_ConsultorioID))]
        public virtual Consultorio Consultorio { get; set; } = null!;

        public int Pacientes_PacienteID { get; set; }
        [ForeignKey(nameof(Pacientes_PacienteID))]
        public virtual Paciente Paciente { get; set; } = null!;

        public int Medicos_MedicoID { get; set; }
        [ForeignKey(nameof(Medicos_MedicoID))]
        public virtual Medico Medico { get; set; } = null!;

        public virtual ConsultaMedica? ConsultaMedica { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; } = [];
    }
}