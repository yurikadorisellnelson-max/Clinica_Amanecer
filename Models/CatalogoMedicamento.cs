using System.ComponentModel.DataAnnotations;

namespace ClinicaAmanecer.Models
{
    public class CatalogoMedicamento
    {
        [Key]
        public int MedicamentoID { get; set; }
        [Required, MaxLength(120)]
        public string NombreGenerico { get; set; } = string.Empty;
        [Required, MaxLength(45)]
        public string Concentracion { get; set; } = string.Empty;
        [Required, MaxLength(45)]
        public string Presentacion { get; set; } = string.Empty;

        public virtual ICollection<DetalleReceta_Medicamento> DetalleRecetas { get; set; } = [];
    }
}