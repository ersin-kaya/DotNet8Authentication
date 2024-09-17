using System.Security.Claims;
using CustomIdentityAuth.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CustomIdentityAuth.Filters;

public class UpdateLastActivityFilter : IAsyncActionFilter
{
    private readonly IUserService _userService;

    public UpdateLastActivityFilter(IUserService userService)
    {
        _userService = userService;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        string userId = null;
        
        var claimsIdentity = context.HttpContext.User.Identity as ClaimsIdentity;
        var userIdClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (userIdClaim != null)
            userId = userIdClaim.Value;

        if (!string.IsNullOrEmpty(userId))
            await _userService.UpdateLastActivityAsync(userId);
        
        await next();
    }
}