using ClinicaAmanecer.Data;
using ClinicaAmanecer.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ClinicaAmanecer.Repositories
{
    public class PermisoRepository(AppDBContext context) : IPermissionRepository
    {
        public async Task<IReadOnlyList<Permiso>> GetAllAsync()
        {
            return await context.Permisos.OrderBy(p => p.PermisoID).ToListAsync();
        }

        public async Task<Permiso?> GetByIdAsync(int id)
        {
            return await context.Permisos.FindAsync(id);
        }

        public async Task<Permiso?> GetByCodigoAsync(string codigo)
        {
            return await context.Permisos.FirstOrDefaultAsync(p => p.CodigoPermiso == codigo);
        }
    }
}