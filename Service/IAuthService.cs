using ClinicaAmanecer.DTOS;

namespace ClinicaAmanecer.Service
{
    public interface IAuthService
    {
        Task<(bool Success, LoginResponseDto? Result, string? ErrorMessage)> LoginAsync(LoginRequestDto dto);
        Task<(bool Success, string? ErrorMessage)> RegisterUserAsync(RegisterUserDto dto);
        Task<LoginResponseDto?> RefreshAccessTokenAsync(string refreshToken);
    }
}
