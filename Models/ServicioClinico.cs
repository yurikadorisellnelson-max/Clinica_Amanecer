using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaAmanecer.Models
{
    public class ServicioClinico
    {
        [Key]
        public int ServicioID { get; set; }
        [Required, MaxLength(45)]
        public string CodigoServicio { get; set; } = string.Empty;
        [Required, MaxLength(120)]
        public string NombreServicio { get; set; } = string.Empty;
        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecioUnitario { get; set; }
        public bool Activo { get; set; } = true;

        public virtual ICollection<DetalleFactura_Servicio> DetalleFacturas { get; set; } = [];
    }
}