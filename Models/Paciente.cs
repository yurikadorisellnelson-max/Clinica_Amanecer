using System.ComponentModel.DataAnnotations;

namespace ClinicaAmanecer.Models
{
    public class Paciente
    {
        [Key]
        public int PacienteID { get; set; }
        [Required, MaxLength(20)]
        public string NumeroExpediente { get; set; } = string.Empty;
        [Required, MaxLength(20)]
        public string Cedula { get; set; } = string.Empty;
        [Required, MaxLength(45)]
        public string Nombres { get; set; } = string.Empty;
        [Required, MaxLength(45)]
        public string Apellidos { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        [Required, MaxLength(1)]
        public string Sexo { get; set; } = "F";
        [Required, MaxLength(5)]
        public string TipoSangre { get; set; } = "O+";
        [Required, MaxLength(20)]
        public string Telefono { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? Direccion { get; set; }
        public bool Estado { get; set; } = true;

        public virtual ICollection<DetallePaciente_Alergia> DetalleAlergias { get; set; } = [];
        public virtual ICollection<Cita> Citas { get; set; } = [];
        public virtual ICollection<Factura> Facturas { get; set; } = [];
    }
}