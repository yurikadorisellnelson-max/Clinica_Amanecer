using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaAmanecer.Models
{
    public class Factura
    {
        [Key]
        public int FacturaID { get; set; }
        [Required, MaxLength(30)]
        public string NumeroFactura { get; set; } = string.Empty;
        public DateTime FechaEmision { get; set; } = DateTime.Now;
        [Required, MaxLength(45)]
        public string MetodoPago { get; set; } = "Efectivo";
        [Column(TypeName = "decimal(10,2)")]
        public decimal SubTotal { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Descuento { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalNeto { get; set; }
        [Required, MaxLength(45)]
        public string EstadoFactura { get; set; } = "Pagada";

        public int Usuarios_UsuarioID { get; set; }
        [ForeignKey(nameof(Usuarios_UsuarioID))]
        public virtual Usuario UsuarioCajero { get; set; } = null!;

        public int Citas_CitaID { get; set; }
        [ForeignKey(nameof(Citas_CitaID))]
        public virtual Cita Cita { get; set; } = null!;

        public int Pacientes_PacienteID { get; set; }
        [ForeignKey(nameof(Pacientes_PacienteID))]
        public virtual Paciente Paciente { get; set; } = null!;

        public virtual ICollection<DetalleFactura_Servicio> DetalleServicios { get; set; } = [];
    }
}