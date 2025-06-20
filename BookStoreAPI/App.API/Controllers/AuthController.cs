using App.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    
    public class AuthController : CustomBaseController
    {
        private readonly IMediator _medaitor;
        public AuthController(IMediator medaitor)
        {
            _medaitor = medaitor;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest command)
        {
            var result = await _medaitor.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest command)
        {
            var result = await _medaitor.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest command)
        {
            var result = await _medaitor.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnly()
        {
            return Ok("This is an admin-only endpoint.");
        }


    }
}
