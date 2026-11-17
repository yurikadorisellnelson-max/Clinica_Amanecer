using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaAmanecer.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioID { get; set; }
        [Required, MaxLength(45)]
        public string UserName { get; set; } = string.Empty;
        [Required, MaxLength(120)]
        public string NombreCompleto { get; set; } = string.Empty;
        [Required, MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required, MaxLength(250)]
        public string PasswordHash { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;

        public bool EmailConfirmed { get; set; } = true;
        public string? EmailConfirmToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpiryTime { get; set; }

        public int Roles_RoleID { get; set; }
        [ForeignKey(nameof(Roles_RoleID))]
        public virtual Role Role { get; set; } = null!;

        public virtual ICollection<UsuarioPermiso> UsuarioPermisos { get; set; } = [];
        public virtual Medico? Medico { get; set; }
        public virtual ICollection<Factura> FacturasEmitidas { get; set; } = [];
    }
}