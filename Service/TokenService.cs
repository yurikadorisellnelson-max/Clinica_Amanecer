using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ClinicaAmanecer.Extenciones;
using ClinicaAmanecer.Helpers;
using ClinicaAmanecer.Models;
using ClinicaAmanecer.Models.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ClinicaAmanecer.Service
{
    public class TokenService(IOptions<JwtSettings> jwtSettings) : ITokenService
    {
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;

        public string GenerateAccessToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, usuario.UsuarioID.ToString()),
                new(ClaimTypes.Name, usuario.UserName),
                new("NombreCompleto", usuario.NombreCompleto),
                new(ClaimTypes.Email, usuario.Email),
                new(ClaimTypes.Role, usuario.Role?.NombreRol ?? "Recepcionista")
            };

            foreach (var permiso in EffectivePermission.FromUser(usuario))
            {
                claims.Add(new Claim(ClamsPrincipalExtension.PermissionClaimType, permiso));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenValidateInMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }

        public string GeneratePasswordResetToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
    }
}

