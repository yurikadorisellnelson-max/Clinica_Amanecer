namespace ClinicaAmanecer.DTOS
{
    public class UserListItemDto
    {
        public int UsuarioID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int RoleID { get; set; }
        public string NombreRol { get; set; } = string.Empty;
        public bool Activo { get; set; }
    }
}