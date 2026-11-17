using System.ComponentModel.DataAnnotations;

namespace ClinicaAmanecer.Models
{
    public class Especialidad
    {
        [Key]
        public int EspecialidadID { get; set; }
        [Required, MaxLength(80)]
        public string NombreEspecialidad { get; set; } = string.Empty;
        public int DuracionCitaMin { get; set; } = 30;
        public bool Estado { get; set; } = true;

        public virtual ICollection<Medico> Medicos { get; set; } = [];
    }
}