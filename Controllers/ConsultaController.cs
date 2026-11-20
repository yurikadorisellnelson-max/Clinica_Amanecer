using ClinicaAmanecer.DTOS;
using ClinicaAmanecer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaAmanecer.Controllers
{
    [Authorize(Policy = "consulta.ver")]
    public class ConsultaController(
        IConsultaMedicaService consultaService,
        ICitaService citaService) : Controller
    {
        public async Task<IActionResult> Index(int? citaId)
        {
            var citasHoy = await citaService.GetCitasDelDiaAsync(DateTime.Today);
            var citaSeleccionada = citaId.HasValue
                ? await citaService.GetByIdWithDetailsAsync(citaId.Value)
                : citasHoy.FirstOrDefault() != null
                    ? await citaService.GetByIdWithDetailsAsync(citasHoy.First().CitaID)
                    : null;

            ViewBag.CitasDelDia = citasHoy;
            ViewBag.CitaSeleccionada = citaSeleccionada;
            ViewBag.CatalogoCIE10 = await consultaService.GetCatalogoCIE10Async();
            ViewBag.CatalogoMedicamentos = await consultaService.GetCatalogoMedicamentosAsync();

            return View();
        }

        [Authorize(Policy = "consulta.crear")]
        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] SaveConsultaMedicaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, message = "Complete los signos vitales y datos clínicos." });

            var (success, error) = await consultaService.RegistrarConsultaCompletaAsync(dto);
            return success
                ? Ok(new { success = true, message = "Consulta médica, diagnósticos CIE-10 y receta guardados exitosamente." })
                : BadRequest(new { success = false, message = error });
        }
    }
}