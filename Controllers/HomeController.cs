
using ClinicaAmanecer.Models;
using ClinicaAmanecer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ClinicaAmanecer.Controllers
{
    [Authorize(Policy = "dashboard.ver")]
    public class HomeController(
        ICitaService citaService,
        IFacturaService facturaService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var citasHoy = await citaService.GetCitasDelDiaAsync(DateTime.Today);
            var facturas = await facturaService.GetHistorialFacturasAsync();

            ViewBag.CitasDeHoy = citasHoy.Count;
            ViewBag.ConsultasAtendidas = citasHoy.Count(c => c.EstadoCita == "Atendida");
            ViewBag.PacientesEnEspera = citasHoy.Count(c => c.EstadoCita == "Programada");
            ViewBag.IngresosDelDia = facturas
                .Where(f => f.FechaEmision.Date == DateTime.Today && f.EstadoFactura == "Pagada")
                .Sum(f => f.TotalNeto);

            return View(citasHoy);
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
