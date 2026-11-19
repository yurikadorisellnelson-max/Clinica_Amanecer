using ClinicaAmanecer.Data;
using ClinicaAmanecer.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ClinicaAmanecer.Repositories
{
    public class RolRepository(AppDBContext context) : IRoleRepository
    {
        public async Task<IReadOnlyList<Role>> GetAllWithPermissionsAsync()
        {
            return await context.Roles
                .Include(r => r.RolPermisos)
                    .ThenInclude(rp => rp.Permiso)
                .OrderBy(r => r.RoleID)
                .ToListAsync();
        }

        public async Task<Role?> GetByIdWithPermissionsAsync(int id)
        {
            return await context.Roles
                .Include(r => r.RolPermisos)
                    .ThenInclude(rp => rp.Permiso)
                .FirstOrDefaultAsync(r => r.RoleID == id);
        }

        public async Task<Role?> GetByNameAsync(string name)
        {
            return await context.Roles.FirstOrDefaultAsync(r => r.NombreRol == name);
        }

        public async Task<bool> AssignPermissionToRoleAsync(int roleId, int permisoId)
        {
            bool exists = await context.RolPermisos
                .AnyAsync(rp => rp.Roles_RoleID == roleId && rp.Permisos_PermisoID == permisoId);
            if (exists) return false;

            context.RolPermisos.Add(new RolPermiso
            {
                Roles_RoleID = roleId,
                Permisos_PermisoID = permisoId,
                FechaRegistro = DateTime.Now
            });
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemovePermissionFromRoleAsync(int roleId, int permisoId)
        {
            var rp = await context.RolPermisos
                .FirstOrDefaultAsync(x => x.Roles_RoleID == roleId && x.Permisos_PermisoID == permisoId);
            if (rp == null) return false;

            context.RolPermisos.Remove(rp);
            await context.SaveChangesAsync();
            return true;
        }
    }
}