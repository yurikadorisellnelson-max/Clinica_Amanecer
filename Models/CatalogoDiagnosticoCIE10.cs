using System.ComponentModel.DataAnnotations;

namespace ClinicaAmanecer.Models
{
    public class CatalogoDiagnosticoCIE10
    {
        [Key]
        public int DiagnosticoID { get; set; }
        [Required, MaxLength(10)]
        public string CodigoCIE10 { get; set; } = string.Empty;
        [Required, MaxLength(200)]
        public string DescripcionCIE10 { get; set; } = string.Empty;

        public virtual ICollection<DetalleConsulta_Diagnostico> DetalleConsultas { get; set; } = [];
    }
}