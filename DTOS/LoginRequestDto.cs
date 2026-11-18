using System.ComponentModel.DataAnnotations;

namespace ClinicaAmanecer.DTOS
{
    public class LoginRequestDto
    {
        [Required]
        public string UserNameOrEmail { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public bool RecordarUsuario { get; set; }
    }
}