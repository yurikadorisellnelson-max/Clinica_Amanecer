namespace ClinicaAmanecer.DTOS
{
    public class DetalleAlergiaItemDto
    {
        public int? DetalleAlergiaID { get; set; }
        public int AlergiaID { get; set; }
        public string NombreAlergeno { get; set; } = string.Empty;
        public string CategoriaAlergia { get; set; } = string.Empty;
        public string NivelSeveridad { get; set; } = "Moderada";
        public string? NotasReaccion { get; set; }
    }
}