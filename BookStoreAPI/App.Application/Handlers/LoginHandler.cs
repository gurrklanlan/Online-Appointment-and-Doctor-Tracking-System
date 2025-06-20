using App.Application.Helpers;
using App.Application.Interfaces.User;
using App.Application.Interfaces.User.Service;
using App.Application.Models;
using MediatR;

namespace App.Application.Handlers
{
    public class LoginHandler : IRequestHandler<LoginRequest, ServiceResult<TokenResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        public LoginHandler(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }
        public async Task<ServiceResult<TokenResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if(user is null || !PasswordHasher.VerifyPassword(request.Password, user.PasswordHash))
            {
                return ServiceResult<TokenResponse>.Fail("Invalid email or password.");
            }
            var token = _tokenService.CreateToken(user);
            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpiryTime = token.RefreshtTokenExtTime;

             _userRepository.UpdateAsync(user);
             return ServiceResult<TokenResponse>.Success(token);

        }
    }
}
