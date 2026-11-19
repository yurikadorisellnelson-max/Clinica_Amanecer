using ClinicaAmanecer.Data;
using ClinicaAmanecer.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ClinicaAmanecer.Repositories
{
    public class UsuarioRepository(AppDBContext context) : IUsuarioRepository
    {
        private IQueryable<Usuario> UsersWithPermissions =>
            context.Usuarios
                .Include(u => u.Role)
                    .ThenInclude(r => r.RolPermisos)
                        .ThenInclude(rp => rp.Permiso)
                .Include(u => u.UsuarioPermisos)
                    .ThenInclude(up => up.Permiso);

        public async Task<Usuario?> GetByUserNameOrEmailAsync(string userNameOrEmail)
        {
            var term = userNameOrEmail.Trim();
            return await UsersWithPermissions
                .FirstOrDefaultAsync(u => u.UserName == term || u.Email == term);
        }

        public async Task<Usuario?> GetByIdWithPermissionsAsync(int id)
        {
            return await UsersWithPermissions.FirstOrDefaultAsync(u => u.UsuarioID == id);
        }

        public async Task<Usuario?> GetByRefreshTokenAsync(string refreshToken)
        {
            return await UsersWithPermissions.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }

        public async Task<IReadOnlyList<Usuario>> GetAllWithRolesAsync()
        {
            return await context.Usuarios
                .Include(u => u.Role)
                .OrderBy(u => u.NombreCompleto)
                .ToListAsync();
        }

        public async Task<Usuario> CreateAsync(Usuario usuario)
        {
            var entry = await context.Usuarios.AddAsync(usuario);
            await context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<bool> UpdateRoleAsync(int usuarioId, int roleId)
        {
            var user = await context.Usuarios.FindAsync(usuarioId);
            if (user == null) return false;

            if (!await context.Roles.AnyAsync(r => r.RoleID == roleId)) return false;

            user.Roles_RoleID = roleId;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AssignPermissionToUsuarioAsync(int usuarioId, int permisoId, bool esPermitido, string? motivo = null)
        {
            var existing = await context.UsuarioPermisos
                .FirstOrDefaultAsync(up => up.Usuarios_UsuarioID == usuarioId && up.Permisos_PermisoID == permisoId);

            if (existing != null)
            {
                existing.EsPermitido = esPermitido;
                existing.MotivoCambio = motivo;
                existing.FechaModificacion = DateTime.Now;
            }
            else
            {
                context.UsuarioPermisos.Add(new UsuarioPermiso
                {
                    Usuarios_UsuarioID = usuarioId,
                    Permisos_PermisoID = permisoId,
                    EsPermitido = esPermitido,
                    MotivoCambio = motivo,
                    FechaModificacion = DateTime.Now
                });
            }

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemovePermissionFromUsuarioAsync(int usuarioId, int permisoId)
        {
            var existing = await context.UsuarioPermisos
                .FirstOrDefaultAsync(up => up.Usuarios_UsuarioID == usuarioId && up.Permisos_PermisoID == permisoId);
            if (existing == null) return false;

            context.UsuarioPermisos.Remove(existing);
            await context.SaveChangesAsync();
            return true;
        }

        public bool ValidatePassword(Usuario usuario, string password)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, usuario.PasswordHash);
        }

        public async Task SaveAsync() => await context.SaveChangesAsync();
    }
}