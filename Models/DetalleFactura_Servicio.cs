using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaAmanecer.Models
{
    public class DetalleFactura_Servicio
    {
        [Key]
        public int DetalleFacturaID { get; set; }
        public int Cantidad { get; set; } = 1;
        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecioUnitario { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal SubTotalLinea { get; set; }

        public int Facturas_FacturaID { get; set; }
        [ForeignKey(nameof(Facturas_FacturaID))]
        public virtual Factura Factura { get; set; } = null!;

        public int ServicioClinico_ServicioID { get; set; }
        [ForeignKey(nameof(ServicioClinico_ServicioID))]
        public virtual ServicioClinico ServicioClinico { get; set; } = null!;
    }
}