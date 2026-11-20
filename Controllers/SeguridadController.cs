using ClinicaAmanecer.DTOS;
using ClinicaAmanecer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaAmanecer.Controllers
{
    [Authorize(Policy = "seguridad.ver")]
    public class SeguridadController(
        IRolePermisoService rolePermisoService,
        IAuthService authService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            ViewBag.Usuarios = await rolePermisoService.GetAllUsersAsync();
            ViewBag.Roles = await rolePermisoService.GetAllRolesWithPermissionsAsync();
            ViewBag.Permisos = await rolePermisoService.GetAllPermissionsAsync();
            return View();
        }

        [Authorize(Policy = "seguridad.administrar")]
        [HttpPost]
        public async Task<IActionResult> CrearUsuario([FromBody] RegisterUserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, message = "Complete todos los campos del usuario." });

            var (success, error) = await authService.RegisterUserAsync(dto);
            return success
                ? Ok(new { success = true, message = "Usuario creado y rol asignado correctamente." })
                : BadRequest(new { success = false, message = error });
        }

        [Authorize(Policy = "seguridad.administrar")]
        [HttpPost]
        public async Task<IActionResult> TogglePermisoRol(int roleId, int permisoId, bool otorgar)
        {
            var (success, error) = await rolePermisoService.ToggleRolePermissionAsync(roleId, permisoId, otorgar);
            return success
                ? Ok(new { success = true })
                : BadRequest(new { success = false, message = error });
        }

        [Authorize(Policy = "seguridad.administrar")]
        [HttpPost]
        public async Task<IActionResult> AsignarPermisoUsuario([FromBody] AssignUserPermissionDto dto)
        {
            var (success, error) = await rolePermisoService.SetUserPermissionOverrideAsync(dto);
            return success
                ? Ok(new { success = true, message = "Excepción de permiso aplicada al usuario." })
                : BadRequest(new { success = false, message = error });
        }
    }
}