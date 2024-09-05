using CustomIdentityAuth.Models;
using CustomIdentityAuth.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomIdentityAuth.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAuthService _authService;

    public AccountController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.Register(model);

        if (result.Succeeded)
            return Ok(result);

        return BadRequest(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.Login(model);

        if (result.Succeeded)
            return Ok(result);

        return BadRequest(result);
    }
    
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.RefreshToken(model);

        if (result.Succeeded)
            return Ok(result);

        return BadRequest(result);
    }
}