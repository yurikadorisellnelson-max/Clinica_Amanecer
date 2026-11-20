using ClinicaAmanecer.Models;

namespace ClinicaAmanecer.Service
{
    public interface ITokenService
    {
        string GenerateAccessToken(Usuario usuario);
        string GenerateRefreshToken();
        string GeneratePasswordResetToken();
    }
}
