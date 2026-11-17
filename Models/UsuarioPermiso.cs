using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaAmanecer.Models
{
    public class UsuarioPermiso
    {
        [Key]
        public int UsuarioPermisoID { get; set; }
        public bool EsPermitido { get; set; }
        [MaxLength(150)]
        public string? MotivoCambio { get; set; }
        public DateTime FechaModificacion { get; set; } = DateTime.Now;

        public int Permisos_PermisoID { get; set; }
        [ForeignKey(nameof(Permisos_PermisoID))]
        public virtual Permiso Permiso { get; set; } = null!;

        public int Usuarios_UsuarioID { get; set; }
        [ForeignKey(nameof(Usuarios_UsuarioID))]
        public virtual Usuario Usuario { get; set; } = null!;
    }
}