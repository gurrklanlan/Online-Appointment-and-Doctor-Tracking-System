using MediatR;

namespace App.Application.Models
{
    public class RefreshTokenRequest:IRequest<ServiceResult<TokenResponse>>
    {
        public string Email { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
