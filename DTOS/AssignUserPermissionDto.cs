namespace ClinicaAmanecer.DTOS
{
    public class AssignUserPermissionDto
    {
        public int UsuarioID { get; set; }
        public int PermisoID { get; set; }
        public bool EsPermitido { get; set; } // true = Permitido (✓), false = Denegado (✗)
        public string? MotivoCambio { get; set; }
    }
}