using System.ComponentModel.DataAnnotations;

namespace ClinicaAmanecer.DTOS
{
    public class SaveFacturaDto
    {
        [Required]
        public string NumeroFactura { get; set; } = string.Empty;
        [Required]
        public int PacienteID { get; set; }
        [Required]
        public int CitaID { get; set; }
        [Required]
        public int UsuarioCajeroID { get; set; }
        [Required]
        public string MetodoPago { get; set; } = "Efectivo";
        public decimal Descuento { get; set; }

        public List<DetalleFacturaItemDto> Servicios { get; set; } = [];
    }

    public class DetalleFacturaItemDto
    {
        public int ServicioID { get; set; }
        public int Cantidad { get; set; } = 1;
        public decimal PrecioUnitario { get; set; }
    }
}