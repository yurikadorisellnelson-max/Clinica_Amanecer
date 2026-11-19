using ClinicaAmanecer.Models;

namespace ClinicaAmanecer.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> GetByUserNameOrEmailAsync(string userNameOrEmail);
        Task<Usuario?> GetByIdWithPermissionsAsync(int id);
        Task<Usuario?> GetByRefreshTokenAsync(string refreshToken);
        Task<IReadOnlyList<Usuario>> GetAllWithRolesAsync();
        Task<Usuario> CreateAsync(Usuario usuario);
        Task<bool> UpdateRoleAsync(int usuarioId, int roleId);
        Task<bool> AssignPermissionToUsuarioAsync(int usuarioId, int permisoId, bool esPermitido, string? motivo = null);
        Task<bool> RemovePermissionFromUsuarioAsync(int usuarioId, int permisoId);
        bool ValidatePassword(Usuario usuario, string password);
        Task SaveAsync();
    }
}