using MediatR;

namespace App.Application.Models
{
    public class RegisterRequest: IRequest<ServiceResult<TokenResponse>>
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
       
    }
}
