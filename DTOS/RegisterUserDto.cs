using System.ComponentModel.DataAnnotations;

namespace ClinicaAmanecer.DTOS
{
    public class RegisterUserDto
    {
        [Required, MaxLength(120)]
        public string NombreCompleto { get; set; } = string.Empty;
        [Required, MaxLength(45)]
        public string UserName { get; set; } = string.Empty;
        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;
        [Required]
        public int RoleID { get; set; }
        public bool Activo { get; set; } = true;
    }
}