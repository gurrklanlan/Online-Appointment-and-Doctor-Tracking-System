using MediatR;
using Microsoft.AspNetCore.Http.Features;

namespace App.Application.Models
{
    public class LoginRequest:IRequest<ServiceResult<TokenResponse>>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        
    }
}
