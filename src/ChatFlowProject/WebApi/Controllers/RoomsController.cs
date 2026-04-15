using Application.Features.Rooms.Commands.Create;
using Application.Features.Rooms.Commands.Delete;
using Application.Features.Rooms.Commands.PermanentDelete;
using Application.Features.Rooms.Commands.Update;
using Application.Features.Rooms.Queries.Get;
using Application.Features.Rooms.Queries.GetList;
using Core.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Authorize]
public class RoomsController : BaseController
{
    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] CreateRoomCommand createRoomCommand)
    {
        createRoomCommand.CreateUserId = GetCurrentUserId();
        CreatedRoomResponse? response = await Mediator.Send(createRoomCommand);
        return Ok(response);
    }

    [HttpGet("GetList")]
    public async Task<IActionResult> GetList()
    {
        GetListRoomQuery query = new GetListRoomQuery { UserId = GetCurrentUserId() };
        List<GetListRoomResponse> response = await Mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("Get/{roomId}")]
    public async Task<IActionResult> Get([FromRoute] Guid roomId)
    {
        var query = new GetRoomQuery { RoomId = roomId, UserId = GetCurrentUserId() };
        GetRoomResponse? response = await Mediator.Send(query);
        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] UpdateRoomCommand updateRoomCommand)
    {
        updateRoomCommand.UserId = GetCurrentUserId();
        UpdatedRoomResponse? response = await Mediator.Send(updateRoomCommand);
        return Ok(response);
    }

    [HttpDelete("Delete/{roomId}")]
    public async Task<IActionResult> Delete([FromRoute] Guid roomId)
    {
        var command = new DeleteRoomCommand { RoomId = roomId, UserId = GetCurrentUserId() };
        await Mediator.Send(command);
        return Ok();
    }
}

