using CustomIdentityAuth.Dtos;
using CustomIdentityAuth.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomIdentityAuth.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto requestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.Register(requestDto);

        if (result.Succeeded)
            return Ok(result);

        return BadRequest(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto requestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.Login(requestDto);

        if (result.Succeeded)
            return Ok(result);

        return BadRequest(result);
    }
    
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto requestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.RefreshToken(requestDto);

        if (result.Succeeded)
            return Ok(result);

        return BadRequest(result);
    }
}