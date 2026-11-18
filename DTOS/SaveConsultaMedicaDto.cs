using System.ComponentModel.DataAnnotations;

namespace ClinicaAmanecer.DTOS
{
    public class SaveConsultaMedicaDto
    {
        public int? ConsultasID { get; set; }
        [Required]
        public int CitaID { get; set; }
        [Required]
        public string PresionArterial { get; set; } = string.Empty;
        public decimal TemperaturaCorporal { get; set; }
        public decimal PesoKG { get; set; }
        public decimal TallaCM { get; set; }
        [Required]
        public string Anamnesis { get; set; } = string.Empty;
        [Required]
        public string ExploracionFisica { get; set; } = string.Empty;
        [Required]
        public string PlanTratamiento { get; set; } = string.Empty;

        public List<DetalleDiagnosticoItemDto> Diagnosticos { get; set; } = [];
        public List<DetalleRecetaItemDto> RecetaMedicamentos { get; set; } = [];
    }

    public class DetalleDiagnosticoItemDto
    {
        public int DiagnosticoID { get; set; }
        public string TipoCriterio { get; set; } = "Definitivo";
        public string? Observacion { get; set; }
    }

    public class DetalleRecetaItemDto
    {
        public int MedicamentoID { get; set; }
        public string Dosis { get; set; } = string.Empty;
        public string Frecuencia { get; set; } = string.Empty;
        public int DuracionDias { get; set; }
        public string ViaAdministracion { get; set; } = "Oral";
    }
}