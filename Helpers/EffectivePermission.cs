using ClinicaAmanecer.Models;

namespace ClinicaAmanecer.Helpers
{
    public static class EffectivePermission
    {
        public static IReadOnlyList<string> FromUser(Usuario usuario)
        {
            var permissions = usuario.Role?.RolPermisos?
                .Where(rp => rp.Permiso != null && !string.IsNullOrWhiteSpace(rp.Permiso.CodigoPermiso))
                .Select(rp => rp.Permiso.CodigoPermiso)
                .ToHashSet(StringComparer.OrdinalIgnoreCase) ?? [];

            if (usuario.UsuarioPermisos != null)
            {
                foreach (var overridePerm in usuario.UsuarioPermisos)
                {
                    var code = overridePerm.Permiso?.CodigoPermiso;
                    if (string.IsNullOrWhiteSpace(code)) continue;

                    if (overridePerm.EsPermitido)
                        permissions.Add(code);
                    else
                        permissions.Remove(code);
                }
            }

            return permissions.OrderBy(p => p, StringComparer.OrdinalIgnoreCase).ToList();
        }
    }
}