using ClinicaAmanecer.DTOS;
using ClinicaAmanecer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaAmanecer.Controllers
{
    [Authorize(Policy = "cita.ver")]
    public class CitaController(
        ICitaService citaService,
        IPacienteService pacienteService) : Controller
    {
        public async Task<IActionResult> Index(DateTime? fecha)
        {
            var fechaFiltro = fecha ?? DateTime.Today;
            ViewBag.FechaActual = fechaFiltro.ToString("yyyy-MM-dd");
            ViewBag.Pacientes = await pacienteService.GetAllActivosAsync();
            ViewBag.Medicos = await citaService.GetMedicosDisponiblesAsync();
            ViewBag.Consultorios = await citaService.GetConsultoriosActivosAsync();

            var citasDelDia = await citaService.GetCitasDelDiaAsync(fechaFiltro);
            return View(citasDelDia);
        }

        [Authorize(Policy = "cita.crear")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SaveCitaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, message = "Complete todos los datos de la cita." });

            var (success, error) = await citaService.ProgramarCitaAsync(dto);
            return success
                ? Ok(new { success = true, message = "Cita programada sin choque de horario." })
                : BadRequest(new { success = false, message = error });
        }

        [Authorize(Policy = "cita.anular")]
        [HttpPost]
        public async Task<IActionResult> Cancelar(int id)
        {
            var (success, error) = await citaService.CambiarEstadoCitaAsync(id, "Cancelada");
            return success
                ? Ok(new { success = true, message = "Cita cancelada correctamente." })
                : BadRequest(new { success = false, message = error });
        }
    }
}
