using ClinicaAmanecer.Models;

namespace ClinicaAmanecer.Repositories
{
    public interface IPermissionRepository
    {
        Task<IReadOnlyList<Permiso>> GetAllAsync();
        Task<Permiso?> GetByIdAsync(int id);
        Task<Permiso?> GetByCodigoAsync(string codigo);
    }
}