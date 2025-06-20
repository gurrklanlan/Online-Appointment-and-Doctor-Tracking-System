using App.Application.Helpers;
using App.Application.Interfaces.User;
using App.Application.Interfaces.User.Service;
using App.Application.Models;
using MediatR;

namespace App.Application.Handlers
{
    public class RegisterHandler : IRequestHandler<RegisterRequest, ServiceResult<TokenResponse>>
    {
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        public RegisterHandler(ITokenService tokenService, IUserRepository userRepository)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
        }
        public async Task<ServiceResult<TokenResponse>> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if(existingUser is not null && existingUser.Email == request.Email)
            {
                return ServiceResult<TokenResponse>.Fail("User with this email already exists.");
            }
            var hashedPassword = PasswordHasher.HashPassword(request.Password);

            var user = new Domain.Entites.User
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = hashedPassword
            };
            var token = _tokenService.CreateToken(user);
            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpiryTime = token.RefreshtTokenExtTime;

            await _userRepository.AddUserAsync(user);
            return ServiceResult<TokenResponse>.Success(token);

        }
    }
}
