using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaAmanecer.Models
{
    public class Medico
    {
        [Key]
        public int MedicoID { get; set; }
        [Required, MaxLength(50)]
        public string CodigoMINSA { get; set; } = string.Empty;
        [Required, MaxLength(80)]
        public string Nombres { get; set; } = string.Empty;
        [Required, MaxLength(80)]
        public string Apellidos { get; set; } = string.Empty;
        [Required, MaxLength(20)]
        public string Telefono { get; set; } = string.Empty;
        [Required, MaxLength(60)]
        public string TurnoHorario { get; set; } = string.Empty;
        public bool Estado { get; set; } = true;

        public int Usuarios_UsuarioID { get; set; }
        [ForeignKey(nameof(Usuarios_UsuarioID))]
        public virtual Usuario Usuario { get; set; } = null!;

        public int Especialidades_EspecialidadID { get; set; }
        [ForeignKey(nameof(Especialidades_EspecialidadID))]
        public virtual Especialidad Especialidad { get; set; } = null!;

        public virtual ICollection<Cita> Citas { get; set; } = [];
    }
}