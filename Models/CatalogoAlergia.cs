using System.ComponentModel.DataAnnotations;

namespace ClinicaAmanecer.Models
{
    public class CatalogoAlergia
    {
        [Key]
        public int AlergiaID { get; set; }
        [Required, MaxLength(45)]
        public string NombreAlergeno { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string CategoriaAlergia { get; set; } = string.Empty;

        public virtual ICollection<DetallePaciente_Alergia> DetallePacientes { get; set; } = [];
    }
}