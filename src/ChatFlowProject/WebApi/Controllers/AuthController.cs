using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Logout;
using Application.Features.Auth.Commands.Refresh;
using Application.Features.Auth.Commands.Register;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class AuthController : BaseController
{
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand)
    {
        RegisterResponse? response = await Mediator.Send(registerCommand);
        return Ok(response);
    }
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand LoginCommand)
    {
        LoginResponse? response = await Mediator.Send(LoginCommand);
        return Ok(response);
    }
    [HttpPost("Refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshCommand RefreshCommand)
    {
        RefreshResponse? response = await Mediator.Send(RefreshCommand);
        return Ok(response);
    }
    [HttpPost("Logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutCommand LogoutCommand)
    {
        LogoutResponse? response = await Mediator.Send(LogoutCommand);
        return Ok(response);
    }
}
