using System.ComponentModel.DataAnnotations;

namespace ClinicaAmanecer.Models
{
    public class Consultorio
    {
        [Key]
        public int ConsultorioID { get; set; }
        [Required, MaxLength(15)]
        public string CodigoSala { get; set; } = string.Empty;
        [Required, MaxLength(45)]
        public string NombreSala { get; set; } = string.Empty;
        public int Piso { get; set; } = 1;
        public bool Estado { get; set; } = true;

        public virtual ICollection<Cita> Citas { get; set; } = [];
    }
}