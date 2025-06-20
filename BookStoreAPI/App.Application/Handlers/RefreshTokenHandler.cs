using App.Application.Interfaces.User;
using App.Application.Interfaces.User.Service;
using App.Application.Models;
using MediatR;

namespace App.Application.Handlers
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenRequest, ServiceResult<TokenResponse>>
    {
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        public RefreshTokenHandler(ITokenService tokenService, IUserRepository userRepository)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
        }
        public async Task<ServiceResult<TokenResponse>> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
           var user= await _userRepository.GetByRefreshTokenAsync(request.RefreshToken);
            if(user is null || user.Email != request.Email || user.RefreshTokenExpiryTime <DateTime.UtcNow)
            {
                return ServiceResult<TokenResponse>.Fail("Invalid refresh token or email.");
            }

            var tokenResponse = _tokenService.CreateToken(user);
            user.RefreshToken = tokenResponse.RefreshToken;
            user.RefreshTokenExpiryTime = tokenResponse.RefreshtTokenExtTime;

             _userRepository.UpdateAsync(user);
            return ServiceResult<TokenResponse>.Success(tokenResponse);
        }
    }
}
