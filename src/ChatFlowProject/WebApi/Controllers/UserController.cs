using Application.Features.Users.Commands.Update;
using Application.Features.Users.Commands.Update.UpdatePassword;
using Application.Features.Users.Commands.Update.UpdateStatus;
using Application.Features.Users.Queries.Get;
using Application.Features.Users.Queries.GetList;
using Core.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class UserController:BaseController
{
    [HttpPut("UpdatedUserPassword")]
    public async Task<IActionResult> UpdatedUserPassword([FromBody] UpdateUserPasswordCommand updateUserPasswordCommand)
    {
        UpdatedUserPasswordResponse? response = await Mediator.Send(updateUserPasswordCommand);
        return Ok(response);
    }

    [HttpPut("UpdatedUserStatus")]
    public async Task<IActionResult> UpdatedUserStatus([FromBody] UpdateUserStatusCommand updateUserStatusCommand)
    {
        await Mediator.Send(updateUserStatusCommand);
        return Ok();
    }

    [HttpPut("UpdateUser")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand updateUserCommand)
    {
        UpdatedUserResponse? response = await Mediator.Send(updateUserCommand);
        return Ok(response);
    }


    [HttpGet("GetUser")]
    public async Task<IActionResult> GetUser([FromQuery] GetUserQuery getUserQuery)
    {
        GetUserResponse? response = await Mediator.Send(getUserQuery);
        return Ok(response);
    }
    [HttpGet("GetListUser")]
    public async Task<IActionResult> GetListUser([FromQuery] GetListUserQuery getListUserQuery)
    {
        GetListResponse<GetListUserResponse>? response = await Mediator.Send(getListUserQuery);
        return Ok(response);
    }
}
