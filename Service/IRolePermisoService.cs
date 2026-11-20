using ClinicaAmanecer.DTOS;

namespace ClinicaAmanecer.Service
{
    public interface IRolePermisoService
    {
        Task<IReadOnlyList<UserListItemDto>> GetAllUsersAsync();
        Task<IReadOnlyList<RoleWithPermisosDto>> GetAllRolesWithPermissionsAsync();
        Task<IReadOnlyList<PermisoDto>> GetAllPermissionsAsync();
        Task<(bool Success, string? ErrorMessage)> ToggleRolePermissionAsync(int roleId, int permisoId, bool otorgar);
        Task<(bool Success, string? ErrorMessage)> SetUserPermissionOverrideAsync(AssignUserPermissionDto dto);
        Task<(bool Success, string? ErrorMessage)> UpdateUserRoleAsync(int usuarioId, int nuevoRoleId);
    }
}
