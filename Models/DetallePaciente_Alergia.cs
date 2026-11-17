using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaAmanecer.Models
{
    public class DetallePaciente_Alergia
    {
        [Key]
        public int DetalleAlergiaID { get; set; }
        [Required, MaxLength(45)]
        public string NivelSeveridad { get; set; } = "Moderada";
        [MaxLength(200)]
        public string? NotasReaccion { get; set; }

        public int CatalogoAlergias_AlergiaID { get; set; }
        [ForeignKey(nameof(CatalogoAlergias_AlergiaID))]
        public virtual CatalogoAlergia CatalogoAlergia { get; set; } = null!;

        public int Pacientes_PacienteID { get; set; }
        [ForeignKey(nameof(Pacientes_PacienteID))]
        public virtual Paciente Paciente { get; set; } = null!;
    }
}