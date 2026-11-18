namespace ClinicaAmanecer.DTOS
{
    public class PermisoDto
    {
        public int PermisoID { get; set; }
        public string CodigoPermiso { get; set; } = string.Empty;
        public string Modulo { get; set; } = string.Empty;
        public string NombreVisible { get; set; } = string.Empty;
    }
}