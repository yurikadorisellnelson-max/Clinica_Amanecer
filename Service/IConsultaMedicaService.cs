using ClinicaAmanecer.DTOS;
using ClinicaAmanecer.Models;

namespace ClinicaAmanecer.Service
{
    public interface IConsultaMedicaService
    {
        Task<ConsultaMedica?> GetByCitaIdAsync(int citaId);
        Task<IReadOnlyList<CatalogoDiagnosticoCIE10>> GetCatalogoCIE10Async();
        Task<IReadOnlyList<CatalogoMedicamento>> GetCatalogoMedicamentosAsync();
        Task<(bool Success, string? ErrorMessage)> RegistrarConsultaCompletaAsync(SaveConsultaMedicaDto dto);
    }
}
