using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaAmanecer.Models
{
    public class DetalleConsulta_Diagnostico
    {
        [Key]
        public int DetalleDiagID { get; set; }
        [Required, MaxLength(45)]
        public string TipoCriterio { get; set; } = "Definitivo";
        [MaxLength(200)]
        public string? Observacion { get; set; }

        public int CatalogoDiagnostico_DiagnosticoID { get; set; }
        [ForeignKey(nameof(CatalogoDiagnostico_DiagnosticoID))]
        public virtual CatalogoDiagnosticoCIE10 DiagnosticoCIE10 { get; set; } = null!;

        public int ConsultasMedicas_ConsultasID { get; set; }
        [ForeignKey(nameof(ConsultasMedicas_ConsultasID))]
        public virtual ConsultaMedica ConsultaMedica { get; set; } = null!;
    }
}