using ClinicaAmanecer.DTOS;
using ClinicaAmanecer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaAmanecer.Controllers
{
    public class AuthController(IAuthService authService) : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (Request.Cookies.ContainsKey("access_token"))
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, message = "Complete su usuario y contraseña." });

            var (success, result, error) = await authService.LoginAsync(dto);
            if (!success || result == null)
                return Unauthorized(new { success = false, message = error ?? "Credenciales inválidas." });

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = result.ExpiresAt
            };

            Response.Cookies.Append("access_token", result.AccessToken, cookieOptions);
            Response.Cookies.Append("refresh_token", result.RefreshToken, cookieOptions);

            return Ok(new { success = true, message = "Bienvenido a Clínica Amanecer" });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("access_token");
            Response.Cookies.Delete("refresh_token");
            return RedirectToAction("Login", "Auth");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied() => View();
    }
}


