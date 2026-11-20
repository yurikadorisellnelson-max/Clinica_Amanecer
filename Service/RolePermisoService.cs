using ClinicaAmanecer.DTOS;
using ClinicaAmanecer.Repositories;

namespace ClinicaAmanecer.Service
{
        public class RolePermisoService(
        IUsuarioRepository usuarioRepo,
        IRoleRepository roleRepo,
        IPermissionRepository permisoRepo) : IRolePermisoService
        {
            public async Task<IReadOnlyList<UserListItemDto>> GetAllUsersAsync()
            {
                var users = await usuarioRepo.GetAllWithRolesAsync();
                return users.Select(u => new UserListItemDto
                {
                    UsuarioID = u.UsuarioID,
                    UserName = u.UserName,
                    NombreCompleto = u.NombreCompleto,
                    Email = u.Email,
                    RoleID = u.Roles_RoleID,
                    NombreRol = u.Role?.NombreRol ?? string.Empty,
                    Activo = u.Activo
                }).ToList();
            }

            public async Task<IReadOnlyList<RoleWithPermisosDto>> GetAllRolesWithPermissionsAsync()
            {
                var roles = await roleRepo.GetAllWithPermissionsAsync();
                return roles.Select(r => new RoleWithPermisosDto
                {
                    RoleID = r.RoleID,
                    NombreRol = r.NombreRol,
                    Descripcion = r.Descripcion,
                    Permisos = r.RolPermisos
                        .Where(rp => rp.Permiso != null)
                        .Select(rp => new PermisoDto
                        {
                            PermisoID = rp.Permiso.PermisoID,
                            CodigoPermiso = rp.Permiso.CodigoPermiso,
                            Modulo = rp.Permiso.Modulo,
                            NombreVisible = rp.Permiso.NombreVisible
                        }).ToList()
                }).ToList();
            }

            public async Task<IReadOnlyList<PermisoDto>> GetAllPermissionsAsync()
            {
                var permisos = await permisoRepo.GetAllAsync();
                return permisos.Select(p => new PermisoDto
                {
                    PermisoID = p.PermisoID,
                    CodigoPermiso = p.CodigoPermiso,
                    Modulo = p.Modulo,
                    NombreVisible = p.NombreVisible
                }).ToList();
            }

            public async Task<(bool Success, string? ErrorMessage)> ToggleRolePermissionAsync(int roleId, int permisoId, bool otorgar)
            {
                if (otorgar)
                {
                    await roleRepo.AssignPermissionToRoleAsync(roleId, permisoId);
                    return (true, null);
                }
                else
                {
                    await roleRepo.RemovePermissionFromRoleAsync(roleId, permisoId);
                    return (true, null);
                }
            }

            public async Task<(bool Success, string? ErrorMessage)> SetUserPermissionOverrideAsync(AssignUserPermissionDto dto)
            {
                var user = await usuarioRepo.GetByIdWithPermissionsAsync(dto.UsuarioID);
                if (user == null) return (false, "Usuario no encontrado.");

                var perm = await permisoRepo.GetByIdAsync(dto.PermisoID);
                if (perm == null) return (false, "Permiso no encontrado.");

                await usuarioRepo.AssignPermissionToUsuarioAsync(
                    dto.UsuarioID,
                    dto.PermisoID,
                    dto.EsPermitido,
                    dto.MotivoCambio);

                return (true, null);
            }

            public async Task<(bool Success, string? ErrorMessage)> UpdateUserRoleAsync(int usuarioId, int nuevoRoleId)
            {
                var updated = await usuarioRepo.UpdateRoleAsync(usuarioId, nuevoRoleId);
                return updated
                    ? (true, null)
                    : (false, "No se pudo actualizar el rol del usuario.");
            }
        }
    }

