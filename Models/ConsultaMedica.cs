using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaAmanecer.Models
{
    public class ConsultaMedica
    {
        [Key]
        public int ConsultasID { get; set; }
        public DateTime FechaAtencion { get; set; } = DateTime.Now;
        [Required, MaxLength(45)]
        public string PresionArterial { get; set; } = string.Empty;
        [Column(TypeName = "decimal(4,1)")]
        public decimal TemperaturaCorporal { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal PesoKG { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal TallaCM { get; set; }
        [Required, MaxLength(500)]
        public string Anamnesis { get; set; } = string.Empty;
        [Required, MaxLength(500)]
        public string ExploracionFisica { get; set; } = string.Empty;
        [Required, MaxLength(500)]
        public string PlanTratamiento { get; set; } = string.Empty;

        public int Citas_CitaID { get; set; }
        [ForeignKey(nameof(Citas_CitaID))]
        public virtual Cita Cita { get; set; } = null!;

        public virtual ICollection<DetalleConsulta_Diagnostico> DetalleDiagnosticos { get; set; } = [];
        public virtual ICollection<DetalleReceta_Medicamento> DetalleRecetas { get; set; } = [];
    }
}