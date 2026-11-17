using System.ComponentModel.DataAnnotations;

namespace ClinicaAmanecer.Models
{
    public class Permiso
    {
        [Key]
        public int PermisoID { get; set; }
        [Required, MaxLength(60)]
        public string CodigoPermiso { get; set; } = string.Empty;
        [Required, MaxLength(45)]
        public string Modulo { get; set; } = string.Empty;
        [Required, MaxLength(100)]
        public string NombreVisible { get; set; } = string.Empty;

        public virtual ICollection<RolPermiso> RolPermisos { get; set; } = [];
        public virtual ICollection<UsuarioPermiso> UsuarioPermisos { get; set; } = [];
    }
}