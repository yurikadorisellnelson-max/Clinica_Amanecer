using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaAmanecer.Models
{
    public class RolPermiso
    {
        [Key]
        public int RolPermisoID { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public int Roles_RoleID { get; set; }
        [ForeignKey(nameof(Roles_RoleID))]
        public virtual Role Role { get; set; } = null!;

        public int Permisos_PermisoID { get; set; }
        [ForeignKey(nameof(Permisos_PermisoID))]
        public virtual Permiso Permiso { get; set; } = null!;
    }
}