using System.ComponentModel.DataAnnotations;

namespace ClinicaAmanecer.DTOS
{
    public class SavePacienteDto
    {
        public int? PacienteID { get; set; }
        [Required]
        public string NumeroExpediente { get; set; } = string.Empty;
        [Required]
        public string Cedula { get; set; } = string.Empty;
        [Required]
        public string Nombres { get; set; } = string.Empty;
        [Required]
        public string Apellidos { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        [Required]
        public string Sexo { get; set; } = "F";
        [Required]
        public string TipoSangre { get; set; } = "O+";
        [Required]
        public string Telefono { get; set; } = string.Empty;
        public string? Direccion { get; set; }
        public bool Estado { get; set; } = true;

        
        public List<DetalleAlergiaItemDto> Alergias { get; set; } = [];
    }
}