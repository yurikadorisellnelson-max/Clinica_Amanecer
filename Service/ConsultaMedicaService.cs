using ClinicaAmanecer.DTOS;
using ClinicaAmanecer.Models;
using ClinicaAmanecer.Repositories;

namespace ClinicaAmanecer.Service
{
    public class ConsultaMedicaService(
        IConsultaMedicaRepository consultaRepo,
        ICitaRepository citaRepo) : IConsultaMedicaService
    {
        public Task<ConsultaMedica?> GetByCitaIdAsync(int citaId)
        => consultaRepo.GetByCitaIdAsync(citaId);

        public Task<IReadOnlyList<CatalogoDiagnosticoCIE10>> GetCatalogoCIE10Async()
        => consultaRepo.GetCatalogoCIE10Async();

        public Task<IReadOnlyList<CatalogoMedicamento>> GetCatalogoMedicamentosAsync()
        => consultaRepo.GetCatalogoMedicamentosAsync();

        public async Task<(bool Success, string? ErrorMessage)> RegistrarConsultaCompletaAsync(SaveConsultaMedicaDto dto)
        {
            var cita = await citaRepo.GetByIdWithDetailsAsync(dto.CitaID);
            if (cita == null)
                return (false, "La cita médica asociada no existe.");

            // Validación de Farmacovigilancia (RF-05): Alergias del paciente vs Medicamentos recetados
            var alergiasPaciente = cita.Paciente.DetalleAlergias
                .Select(a => a.CatalogoAlergia.NombreAlergeno)
                .ToList();

            if (alergiasPaciente.Count > 0 && dto.RecetaMedicamentos.Count > 0)
            {
                var catalogoMeds = await consultaRepo.GetCatalogoMedicamentosAsync();
                foreach (var itemReceta in dto.RecetaMedicamentos)
                {
                    var med = catalogoMeds.FirstOrDefault(m => m.MedicamentoID == itemReceta.MedicamentoID);
                    if (med == null) continue;

                    var alergiaConflicto = alergiasPaciente.FirstOrDefault(alergeno =>
                        med.NombreGenerico.Contains(alergeno, StringComparison.OrdinalIgnoreCase) ||
                        alergeno.Contains(med.NombreGenerico, StringComparison.OrdinalIgnoreCase));

                    if (alergiaConflicto != null)
                    {
                        return (false, $"ALERTA DE FARMACOVIGILANCIA: El paciente presenta alergia registrada a '{alergiaConflicto}'. No se puede recetar '{med.NombreGenerico}'.");
                    }
                    var consulta = new ConsultaMedica
                    {
                        Citas_CitaID = dto.CitaID,
                        FechaAtencion = DateTime.Now,
                        PresionArterial = dto.PresionArterial.Trim(),
                        TemperaturaCorporal = dto.TemperaturaCorporal,
                        PesoKG = dto.PesoKG,
                        TallaCM = dto.TallaCM,
                        Anamnesis = dto.Anamnesis.Trim(),
                        ExploracionFisica = dto.ExploracionFisica.Trim(),
                        PlanTratamiento = dto.PlanTratamiento.Trim()
                    };

                    var diagnosticos = dto.Diagnosticos.Select(d => new DetalleConsulta_Diagnostico
                    {
                        CatalogoDiagnostico_DiagnosticoID = d.DiagnosticoID,
                        TipoCriterio = d.TipoCriterio,
                        Observacion = d.Observacion
                    }).ToList();

                    var recetas = dto.RecetaMedicamentos.Select(r => new DetalleReceta_Medicamento
                    {
                        CatalogoMedicamentos_MedicamentoID = r.MedicamentoID,
                        Dosis = r.Dosis,
                        Frecuencia = r.Frecuencia,
                        DuracionDias = r.DuracionDias,
                        ViaAdministracion = r.ViaAdministracion
                    }).ToList();

                    await consultaRepo.CreateTransaccionalAsync(consulta, diagnosticos, recetas);
                    return (true, null);
                }
            }
        }
    }
}