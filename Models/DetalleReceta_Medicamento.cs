using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaAmanecer.Models
{
    public class DetalleReceta_Medicamento
    {
        [Key]
        public int DetalleRecetaID { get; set; }
        [Required, MaxLength(60)]
        public string Dosis { get; set; } = string.Empty;
        [Required, MaxLength(60)]
        public string Frecuencia { get; set; } = string.Empty;
        public int DuracionDias { get; set; } = 3;
        [Required, MaxLength(45)]
        public string ViaAdministracion { get; set; } = "Oral";

        public int CatalogoMedicamentos_MedicamentoID { get; set; }
        [ForeignKey(nameof(CatalogoMedicamentos_MedicamentoID))]
        public virtual CatalogoMedicamento Medicamento { get; set; } = null!;

        public int ConsultasMedicas_ConsultasID { get; set; }
        [ForeignKey(nameof(ConsultasMedicas_ConsultasID))]
        public virtual ConsultaMedica ConsultaMedica { get; set; } = null!;
    }
}