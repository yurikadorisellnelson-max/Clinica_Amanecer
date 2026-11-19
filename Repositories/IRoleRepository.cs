using ClinicaAmanecer.Models;

namespace ClinicaAmanecer.Repositories
{
    public interface IRoleRepository
    {
        Task<IReadOnlyList<Role>> GetAllWithPermissionsAsync();
        Task<Role?> GetByIdWithPermissionsAsync(int id);
        Task<Role?> GetByNameAsync(string name);
        Task<bool> AssignPermissionToRoleAsync(int roleId, int permisoId);
        Task<bool> RemovePermissionFromRoleAsync(int roleId, int permisoId);
    }
}