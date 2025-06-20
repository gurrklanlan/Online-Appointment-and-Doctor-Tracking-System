using App.Application.Models;
using App.Domain.Entites;

namespace App.Application.Interfaces.User.Service
{
    public interface ITokenService
    {
        TokenResponse CreateToken(App.Domain.Entites.User user);

        string GenerateRefreshToken();

    }
}
