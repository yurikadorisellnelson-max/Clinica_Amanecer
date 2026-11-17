using System.ComponentModel.DataAnnotations;

namespace ClinicaAmanecer.Models
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }
        [Required, MaxLength(45)]
        public string NombreRol { get; set; } = string.Empty;
        [MaxLength(150)]
        public string? Descripcion { get; set; }
        public bool Activo { get; set; } = true;

        public virtual ICollection<Usuario> Usuarios { get; set; } = [];
        public virtual ICollection<RolPermiso> RolPermisos { get; set; } = [];
    }
}