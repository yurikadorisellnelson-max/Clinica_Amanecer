using ClinicaAmanecer.Models;

namespace ClinicaAmanecer.Repositories
{
    public interface IConsultaMedicaRepository
    {
        Task<ConsultaMedica?> GetByCitaIdAsync(int citaId);
        Task<ConsultaMedica> CreateTransaccionalAsync(
            ConsultaMedica consulta,
            List<DetalleConsulta_Diagnostico> diagnosticos,
            List<DetalleReceta_Medicamento> recetas);
        Task<IReadOnlyList<CatalogoDiagnosticoCIE10>> GetCatalogoCIE10Async();
        Task<IReadOnlyList<CatalogoMedicamento>> GetCatalogoMedicamentosAsync();
    }
}