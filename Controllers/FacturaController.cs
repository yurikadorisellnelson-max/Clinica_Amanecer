using System.Security.Claims;
using ClinicaAmanecer.DTOS;
using ClinicaAmanecer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaAmanecer.Controllers
{
    [Authorize(Policy = "factura.ver")]
    public class FacturaController(
        IFacturaService facturaService,
        IPacienteService pacienteService,
        ICitaService citaService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            ViewBag.Pacientes = await pacienteService.GetAllActivosAsync();
            ViewBag.CitasHoy = await citaService.GetCitasDelDiaAsync(DateTime.Today);
            ViewBag.ServiciosClinicos = await facturaService.GetServiciosActivosAsync();
            ViewBag.NumeroFacturaSugerido = $"FAC-2026-{DateTime.Now:HHmmss}";

            var historial = await facturaService.GetHistorialFacturasAsync();
            return View(historial);
        }

        [Authorize(Policy = "factura.crear")]
        [HttpPost]
        public async Task<IActionResult> Emitir([FromBody] SaveFacturaDto dto)
        {
            if (int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int cajeroId))
            {
                dto.UsuarioCajeroID = cajeroId;
            }

            var (success, error) = await facturaService.EmitirFacturaAsync(dto);
            return success
                ? Ok(new { success = true, message = "Factura emitida y registrada en caja." })
                : BadRequest(new { success = false, message = error });
        }

        [Authorize(Policy = "factura.anular")]
        [HttpPost]
        public async Task<IActionResult> Anular(int id)
        {
            var (success, error) = await facturaService.AnularFacturaAsync(id);
            return success
                ? Ok(new { success = true, message = "Factura anulada correctamente." })
                : BadRequest(new { success = false, message = error });
        }
    }
}
