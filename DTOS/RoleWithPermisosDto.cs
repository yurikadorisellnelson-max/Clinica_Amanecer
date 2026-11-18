namespace ClinicaAmanecer.DTOS
{
    public class RoleWithPermisosDto
    {
        public int RoleID { get; set; }
        public string NombreRol { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public List<PermisoDto> Permisos { get; set; } = [];
    }
}