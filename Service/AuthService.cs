using ClinicaAmanecer.DTOS;
using ClinicaAmanecer.Models;
using ClinicaAmanecer.Repositories;

namespace ClinicaAmanecer.Service
{
    public class AuthService(
        IUsuarioRepository userRepo,
        ITokenService tokenService) : IAuthService
    {
        public async Task<(bool Success, LoginResponseDto? Result, string? ErrorMessage)> LoginAsync(LoginRequestDto dto)
        {
            var user = await userRepo.GetByUserNameOrEmailAsync(dto.UserNameOrEmail);
            if (user == null || !userRepo.ValidatePassword(user, dto.Password))
                return (false, null, "Usuario o contraseña incorrectos.");

            if (!user.Activo)
                return (false, null, "Esta cuenta de usuario se encuentra inactiva.");

            var accessToken = tokenService.GenerateAccessToken(user);
            var refreshToken = tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await userRepo.SaveAsync();

            var result = new LoginResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(60),
                RefreshTokenExpiresAt = user.RefreshTokenExpiryTime
            };

            return (true, result, null);
        }

        public async Task<LoginResponseDto?> RefreshAccessTokenAsync(string refreshToken)
        {
            var user = await userRepo.GetByRefreshTokenAsync(refreshToken);
            if (user == null || user.RefreshTokenExpiryTime < DateTime.UtcNow)
                return null;

            var newAccess = tokenService.GenerateAccessToken(user);
            var newRefresh = tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefresh;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await userRepo.SaveAsync();

            return new LoginResponseDto
            {
                AccessToken = newAccess,
                RefreshToken = newRefresh,
                ExpiresAt = DateTime.UtcNow.AddMinutes(60),
                RefreshTokenExpiresAt = user.RefreshTokenExpiryTime
            };
        }

        public async Task<(bool Success, string? ErrorMessage)> RegisterUserAsync(RegisterUserDto dto)
        {
            var existingUser = await userRepo.GetByUserNameOrEmailAsync(dto.UserName);
            if (existingUser != null)
                return (false, "El nombre de usuario ya está registrado.");

            var existingEmail = await userRepo.GetByUserNameOrEmailAsync(dto.Email);
            if (existingEmail != null)
                return (false, "El correo electrónico ya está registrado.");

            var nuevoUsuario = new Usuario
            {
                UserName = dto.UserName.Trim(),
                NombreCompleto = dto.NombreCompleto.Trim(),
                Email = dto.Email.Trim(),
                PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(dto.Password),
                Roles_RoleID = dto.RoleID,
                Activo = dto.Activo,
                EmailConfirmed = true
            };

            await userRepo.CreateAsync(nuevoUsuario);
            return (true, null);
        }
    }
}
