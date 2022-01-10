using Carguero.FeatureFlag.Abstrations;
using Carguero.FeatureFlag.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Carguero.FeatureFlag.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AccountController: ControllerBase
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Authenticate(UserLogin login)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var token = await _userService.AuthenticateAsync(login);
        if (token == default)
            return Unauthorized();

        return Ok(token);
    }
}