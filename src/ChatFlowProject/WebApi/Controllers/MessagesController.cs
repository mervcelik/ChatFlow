using Application.Features.Messages.Commands.Create;
using Application.Features.Messages.Commands.Delete;
using Application.Features.Messages.Commands.Update;
using Application.Features.Messages.Queries.GetListByRoom;
using Core.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MessagesController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListMessageResponse>>> GetMessagesByRoom([FromQuery] GetListMessagesByRoomQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }
    [HttpPost]
    public async Task<ActionResult<CreatedMessageResponse>> CreateMessage([FromBody] CreateMessageCommand command)
    {
        var result = await Mediator.Send(command);
        return Created(nameof(GetMessagesByRoom), result);
    }
    [HttpPut]
    public async Task<ActionResult<UpdatedMessageResponse>> UpdateMessage([FromBody] UpdateMessageCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }
    [HttpDelete]
    public async Task<ActionResult<DeletedMessageResponse>> DeleteMessage([FromBody] DeleteMessageCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }
}