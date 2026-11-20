using ClinicaAmanecer.DTOS;
using ClinicaAmanecer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaAmanecer.Controllers
{
    [Authorize(Policy = "paciente.ver")]
    public class PacienteController(IPacienteService pacienteService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            ViewBag.CatalogoAlergias = await pacienteService.GetCatalogoAlergiasAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetData([FromForm] DataTablesRequestDto request)
        {
            var result = await pacienteService.GetPagedAsync(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var paciente = await pacienteService.GetByIdAsync(id);
            if (paciente == null)
                return NotFound(new { success = false, message = "Paciente no encontrado." });
            return Ok(paciente);
        }

        [Authorize(Policy = "paciente.crear")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SavePacienteDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, message = "Verifique los campos obligatorios del paciente." });

            var (success, error) = await pacienteService.CreateAsync(dto);
            return success
                ? Ok(new { success = true, message = "Paciente y alergias registrados correctamente." })
                : BadRequest(new { success = false, message = error });
        }

        [Authorize(Policy = "paciente.editar")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] SavePacienteDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, message = "Datos inválidos." });

            var (success, error) = await pacienteService.UpdateAsync(dto);
            return success
                ? Ok(new { success = true, message = "Expediente del paciente actualizado correctamente." })
                : BadRequest(new { success = false, message = error });
        }
    }
}

